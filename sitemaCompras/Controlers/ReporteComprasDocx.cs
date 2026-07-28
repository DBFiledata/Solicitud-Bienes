namespace sitemaCompras.Controlers
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.IO;
    using System.Linq;
    using System.Globalization;
    using System.Text;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Drawing.Imaging;
    using Telerik.Windows.Documents.Flow.FormatProviders.Docx;
    using Telerik.Windows.Documents.Flow.FormatProviders.Html;
    using Telerik.Windows.Documents.Flow.Model;
    using Telerik.Windows.Documents.Flow.Model.Editing;
    using Telerik.Windows.Documents.Flow.Model.Shapes;
    using Telerik.Windows.Documents.Flow.Model.Styles;
    using Telerik.Windows.Documents.Primitives;
    // Spreadsheet types are aliased to avoid clashing with Flow's Table/FloatingImage.
    using Ss = Telerik.Windows.Documents.Spreadsheet.Model;
    using SsXlsx = Telerik.Windows.Documents.Spreadsheet.FormatProviders.OpenXml.Xlsx;
    using SsPdf = Telerik.Windows.Documents.Spreadsheet.FormatProviders.Pdf;

    public class ReporteComprasDocx
    {
        // El area imprimible del formulario oficial es A1:U60 (fila 60, columna U = indices 0-based
        // 59 y 20). Todo lo que hay mas alla (p.ej. columnas AJ:AN con datos de versionamiento) es
        // "area de trabajo" de la plantilla Excel, fuera del area de impresion de la hoja, y no debe
        // formar parte de la grilla renderizada.
        private const int FormMaxRow = 59;
        private const int FormMaxCol = 20;

        /// <summary>
        /// Punto de entrada unico usado por todas las vistas. Detecta el tipo de plantilla
        /// por su extension y deriva a la ruta de procesamiento correspondiente (Word .docx
        /// o Excel .xlsx), manteniendo la misma interfaz de entrada y el mismo contrato de
        /// salida (un archivo HTML imprimible en outputHtmlPath).
        /// </summary>
        public void GenerarDocumentoDesdePlantilla(DataSet ds, string templatePath, string outputHtmlPath)
        {
            string ext = (Path.GetExtension(templatePath) ?? "").ToLowerInvariant();
            switch (ext)
            {
                case ".docx":
                    GenerarDocumentoWord(ds, templatePath, outputHtmlPath);
                    break;
                case ".xlsx":
                    GenerarDocumentoExcel(ds, templatePath, outputHtmlPath);
                    break;
                default:
                    throw new NotSupportedException(
                        "Tipo de plantilla no soportado: '" + ext + "'. Solo se admiten plantillas .docx y .xlsx.");
            }
        }

        /// <summary>
        /// Construye el diccionario de marcadores -> valor comun a ambos formatos de plantilla
        /// (Word y Excel): fecha (completa y desglosada en dia/mes/anio), director, solicitante,
        /// unidad y los checkboxes de tipo de solicitud (marcado U+25A3 / desmarcado U+2610). El
        /// texto de {{DESCRIPCION_USO}} se devuelve por separado (out) porque el path Word lo
        /// distribuye en varias filas mientras que el path Excel lo inyecta en una sola celda.
        /// </summary>
        private Dictionary<string, string> ConstruirReemplazos(DataSet ds, out string descripcionUso)
        {
            Dictionary<string, string> reemplazos = new Dictionary<string, string>();
            descripcionUso = "";

            if (ds.Tables.Count > 1 && ds.Tables[1].Rows.Count > 0)
            {
                DataRow enc = ds.Tables[1].Rows[0];
                DateTime fecha = Convert.ToDateTime(enc["fechas"]);
                // Fecha desglosada en tres celdas (DIA | MES | ANIO) del formulario oficial.
                reemplazos["{{FECHA_DIA}}"] = fecha.ToString("dd");
                reemplazos["{{FECHA_MES}}"] = fecha.ToString("MM");
                reemplazos["{{FECHA_ANIO}}"] = fecha.ToString("yyyy");
                // Compatibilidad: plantillas que aun usan el marcador unico.
                reemplazos["{{FECHA}}"] = fecha.ToString("dd-MM-yyyy");
                reemplazos["{{DIR_ADMIN}}"] = Convert.ToString(enc["diradmin"]).ToUpper();
                reemplazos["{{SOLICITANTE}}"] = Convert.ToString(enc["iddirectores"]).ToUpper();
                reemplazos["{{UNIDAD}}"] = Convert.ToString(enc["Unidad"]).ToUpper();
                descripcionUso = Convert.ToString(enc["descripcion"]);
            }

            // Checkboxes: U+2610 (BALLOT BOX) desmarcado; U+25A3 (WHITE SQUARE CONTAINING BLACK SMALL SQUARE) para el tipo elegido.
            reemplazos["{{CHK_BIENES}}"] = "☐"; reemplazos["{{CHK_SERVICIOS}}"] = "☐";
            reemplazos["{{CHK_REEMPLAZO}}"] = "☐"; reemplazos["{{CHK_OBRA}}"] = "☐";
            reemplazos["{{CHK_NUEVO}}"] = "☐";
            if (ds.Tables.Count > 2)
            {
                foreach (DataRow dr in ds.Tables[2].Rows)
                {
                    int tipo = Convert.ToInt32(dr["idTipo"]);
                    switch (tipo)
                    {
                        case 1: reemplazos["{{CHK_BIENES}}"] = "▣"; break;
                        case 2: reemplazos["{{CHK_SERVICIOS}}"] = "▣"; break;
                        case 3: reemplazos["{{CHK_REEMPLAZO}}"] = "▣"; break;
                        case 4: reemplazos["{{CHK_OBRA}}"] = "▣"; break;
                        case 5: reemplazos["{{CHK_NUEVO}}"] = "▣"; break;
                    }
                }
            }

            return reemplazos;
        }

        // =============================================================
        // WORD (.docx) PROCESSING PATH
        // =============================================================
        private void GenerarDocumentoWord(DataSet ds, string templatePath, string outputHtmlPath)
        {
            DocxFormatProvider docxProvider = new DocxFormatProvider();
            RadFlowDocument document;
            using (FileStream input = File.OpenRead(templatePath))
            {
                document = docxProvider.Import(input);
            }
            Section section = document.Sections.First();

            // 0. Detect and detach floating header elements (anchored logo image,
            //    floating versioning table) BEFORE any other processing. HtmlFormatProvider
            //    discards Word's absolute/floating positioning (DrawingML anchors and
            //    table tblpPr), so these are extracted manually here and re-injected
            //    later as absolutely-positioned HTML on top of the flowed export.
            EncabezadoFlotanteInfo encabezado = ExtraerYRemoverEncabezadoFlotante(document);

            // 1-2. Build replacements dictionary (shared with the Excel path).
            string descripcion;
            Dictionary<string, string> reemplazos = ConstruirReemplazos(ds, out descripcion);
            // 3. Replace text markers (excluding DESCRIPCION_USO and TABLA_RENGLONES)
            ReemplazarMarcadoresEnDocumento(document, reemplazos);

            // 4. Handle DESCRIPCION_USO with line breaks
            ReemplazarDescripcionConSaltos(document, "{{DESCRIPCION_USO}}", descripcion);

            // 5. Handle TABLA_RENGLONES - two strategies:
            //    A) If marker exists as text, replace it with table
            //    B) If no marker, find table by headers and populate empty rows
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                bool foundMarker = BuscarYReemplazarMarcadorTabla(document, ds.Tables[0]);
                if (!foundMarker)
                {
                    PoblarTablaExistentePorEncabezados(document, ds.Tables[0]);
                }
            }

            // 6. Export to HTML
            HtmlFormatProvider htmlProvider = new HtmlFormatProvider();
            string htmlFragment = htmlProvider.Export(document);

            // HtmlFormatProvider resuelve un borde "heredado" (Style=Inherit -- la plantilla nunca
            // define un borde propio en esa celda) como una linea SOLIDA visible en el HTML exportado,
            // aun cuando el .docx original no dibuja ninguna linea ahi. Se comprobo que esto ocurre en
            // un paso interno del exportador (no depende del valor de Cell.Borders en el momento de
            // exportar: mutar el modelo antes de exportar no tiene ningun efecto), por lo que se
            // corrige quitando puntualmente el "border-top" ya exportado en las celdas de los campos
            // de cabecera (se conserva el "border-bottom", que es el subrayado real de cada campo).
            htmlFragment = QuitarBordeSuperiorFantasma(htmlFragment, "or (a):");
            htmlFragment = QuitarBordeSuperiorFantasma(htmlFragment, "Funcionario Responsable:");
            htmlFragment = QuitarBordeSuperiorFantasma(htmlFragment, "Unidad Solicitante:");

            string encabezadoHtml = ConstruirHtmlEncabezadoFlotante(encabezado);
            string htmlCompleto = GenerarPaginaHtmlCompleta(htmlFragment, encabezadoHtml, section);
            string dir = Path.GetDirectoryName(outputHtmlPath);
            if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);
            File.WriteAllText(outputHtmlPath, htmlCompleto);
        }

        // =============================================================
        // EXCEL (.xlsx) PROCESSING PATH
        // =============================================================
        // Se inyectan los datos sobre el modelo del workbook (marcadores, renglones, descripcion) y
        // luego se exporta la hoja FORM a PDF con Telerik RadSpreadProcessing, respetando la
        // configuracion de pagina del propio Excel (carta, area de impresion, ajustar a una pagina).
        // Telerik renderiza nativamente celdas combinadas, bordes, fuentes, el logo y el cuadro de
        // versionamiento, por lo que NO se hace ningun render manual de la grilla. El PDF se embebe
        // en un HTML minimo para no alterar el contrato de salida (.html) que usan las vistas.
        private void GenerarDocumentoExcel(DataSet ds, string templatePath, string outputHtmlPath)
        {
            SsXlsx.XlsxFormatProvider xlsxProvider = new SsXlsx.XlsxFormatProvider();
            Ss.Workbook workbook;
            using (FileStream input = File.OpenRead(templatePath))
            {
                workbook = xlsxProvider.Import(input);
            }

            // Trabajar sobre la hoja FORM; quitar Instructivo para que no genere paginas extra.
            Ss.Worksheet form = workbook.Worksheets["FORM"];
            if (form == null) form = workbook.Worksheets.FirstOrDefault();
            if (form == null) throw new InvalidOperationException("La plantilla Excel no contiene hojas.");
            if (workbook.Worksheets.Contains("Instructivo")) workbook.Worksheets.Remove("Instructivo");

            // El cuadro de versionamiento (Codigo/Emision/Version/etc.) es una imagen EMF incrustada
            // en la plantilla; Telerik la rasteriza a baja resolucion al exportar a PDF (se ve
            // ilegible/borrosa) aunque el EMF en si es vectorial y nitido. Se reemplaza por un PNG
            // propio, renderizado en memoria a resolucion alta, ANTES de clonar la hoja para paginas
            // adicionales, asi todas las paginas heredan la version nitida.
            MejorarResolucionImagenesEmf(form);

            // 1. Inyectar los datos COMPARTIDOS (encabezado + descripcion de uso), identicos en todas
            //    las paginas: se hacen sobre FORM antes de clonar, para que cada copia los herede.
            string descripcion;
            Dictionary<string, string> reemplazos = ConstruirReemplazos(ds, out descripcion);
            ReemplazarMarcadoresExcel(form, reemplazos);
            InyectarDescripcionExcel(form, descripcion);

            // 2. Renglones: repartirlos en lotes (uno por pagina) segun la capacidad de la banda de
            //    renglones, y clonar el formulario una vez por pagina adicional. Cada renglon puede
            //    ocupar varias lineas (su fila crece con ajuste de texto), por lo que la reparticion
            //    es por "lineas" y no por conteo fijo, evitando que se corte el contenido.
            Ss.CellIndex renglonInicio;
            bool hayRenglones = LocalizarMarcadorRenglones(form, out renglonInicio);
            if (hayRenglones && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                int[] cols = MapearColumnasRenglones(form, renglonInicio.RowIndex, renglonInicio.ColumnIndex);
                int filasBanda = ContarFilasBanda(form, renglonInicio.RowIndex);
                List<List<LineaRenglon>> lotes = RepartirRenglones(form, ds.Tables[0], cols, renglonInicio.RowIndex, filasBanda);

                var hojas = new List<Ss.Worksheet> { form };
                for (int p = 1; p < lotes.Count; p++)
                {
                    Ss.Worksheet clon = workbook.Worksheets.Add();
                    clon.CopyFrom(form); // hereda encabezado + descripcion, area de renglones vacia
                    hojas.Add(clon);
                }
                for (int p = 0; p < lotes.Count; p++)
                    InyectarLoteRenglones(hojas[p], lotes[p], cols, renglonInicio, filasBanda);
            }
            else if (hayRenglones)
            {
                // Sin renglones: limpiar el marcador para que no quede {{TABLA_RENGLONES}} visible.
                form.Cells.GetCellSelection(renglonInicio).SetValueAsText("");
            }

            // 3. Configurar la pagina en TODAS las hojas: se respeta el tamaño de papel/orientacion de
            //    la plantilla (este formulario es tamaño Legal/oficio), area de impresion A1:U60,
            //    ajuste de ANCHO fijo a una pagina (el ancho de la hoja es constante) SIN forzar
            //    ajuste de ALTO: la paginacion (RepartirRenglones) ya presupuesta en px reales para
            //    que el contenido de cada hoja entre en una pagina fisica, asi que forzar
            //    FitToPagesTall=1 solo enmascararia un error de calculo encogiendo el contenido en
            //    vez de fallar, produciendo una escala distinta entre paginas segun cuanto contenido
            //    tenga cada una. Tambien se reemplaza el numero de pagina: el pie nativo de la
            //    plantilla (footer) queda fuera del margen seguro de impresion fisica de muchas
            //    impresoras (margen de pie = 0 en la plantilla), asi que se limpia y se inyecta como
            //    texto dentro del area imprimible, debajo del cuadro "PARA USO DEL DEPARTAMENTO...".
            var todasLasHojas = workbook.Worksheets.ToList();
            int totalPaginas = todasLasHojas.Count;
            for (int idx = 0; idx < todasLasHojas.Count; idx++)
            {
                Ss.Worksheet w = todasLasHojas[idx];
                var ps = w.WorksheetPageSetup;
                ps.PrintArea.SetPrintArea(new Ss.CellRange(0, 0, FormMaxRow, FormMaxCol));
                ps.Margins = new Ss.Printing.PageMargins(4);
                ps.FitToPages = true;
                ps.FitToPagesWide = 1;
                ps.FitToPagesTall = 0;
                ps.CenterHorizontally = true;
                ps.CenterVertically = true;

                if (ps.HeaderFooterSettings != null && ps.HeaderFooterSettings.Footer != null
                    && ps.HeaderFooterSettings.Footer.RightSection != null)
                    ps.HeaderFooterSettings.Footer.RightSection.Text = "";

                int filaPaginacion = LocalizarFilaPaginacion(w);
                if (filaPaginacion >= 0)
                    w.Cells.GetCellSelection(filaPaginacion, 1).SetValueAsText("Página " + (idx + 1) + " de " + totalPaginas);
            }

            string dir = Path.GetDirectoryName(outputHtmlPath);
            if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);

            // 4. Exportar TODO el libro a PDF (cada hoja = una pagina; el pie "Pagina &P de &N" del
            //    formulario numera solo a traves de las paginas). Calidad de imagen alta para el EMF.
            string pdfPath = Path.Combine(dir, Path.GetFileNameWithoutExtension(outputHtmlPath) + ".pdf");
            SsPdf.PdfFormatProvider pdfProvider = new SsPdf.PdfFormatProvider();
            var pdfSettings = new SsPdf.Export.PdfExportSettings(
                Telerik.Windows.Documents.Spreadsheet.FormatProviders.ExportWhat.EntireWorkbook, false);
            pdfSettings.PdfFileSettings.ImageQuality = Telerik.Windows.Documents.Fixed.FormatProviders.Pdf.Export.ImageQuality.High;
            pdfProvider.ExportSettings = pdfSettings;
            using (FileStream outPdf = File.Create(pdfPath))
            {
                pdfProvider.Export(workbook, outPdf);
            }

            // HTML minimo que embebe el PDF a pantalla completa (el sistema sigue apuntando al .html).
            File.WriteAllText(outputHtmlPath, GenerarWrapperPdf(Path.GetFileName(pdfPath)));
        }

        /// <summary>
        /// Reemplaza las imagenes EMF flotantes de la hoja (p.ej. el cuadro de versionamiento) por un
        /// PNG propio renderizado a alta resolucion. El EMF de la plantilla es vectorial y nitido en
        /// su tamaño original, pero Telerik lo rasteriza a baja resolucion al exportar a PDF; en vez
        /// de depender de ese rasterizado, se renderiza el EMF con GDI+ (que si lo trata como vector)
        /// a varias veces su tamaño nativo y se guarda como PNG, dandole a Telerik una fuente de sobra
        /// para que el resultado final se vea nitido sin importar como la escale internamente.
        /// </summary>
        private void MejorarResolucionImagenesEmf(Ss.Worksheet ws)
        {
            const int escala = 8;
            foreach (var img in ws.Images.ToList())
            {
                var origen = img.ImageSource;
                if (origen == null || !string.Equals(origen.Extension, "emf", StringComparison.OrdinalIgnoreCase)) continue;
                try
                {
                    using (var entrada = new MemoryStream(origen.Data))
                    using (var mf = new Metafile(entrada))
                    {
                        int w = Math.Max(1, mf.Width * escala);
                        int h = Math.Max(1, mf.Height * escala);
                        using (var bmp = new Bitmap(w, h))
                        {
                            using (var g = Graphics.FromImage(bmp))
                            {
                                g.Clear(Color.White);
                                g.SmoothingMode = SmoothingMode.HighQuality;
                                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                                g.DrawImage(mf, new Rectangle(0, 0, w, h));
                            }
                            using (var salida = new MemoryStream())
                            {
                                bmp.Save(salida, ImageFormat.Png);
                                img.ImageSource = new Telerik.Windows.Documents.Media.ImageSource(salida.ToArray(), "png");
                            }
                        }
                    }
                }
                catch { /* si el EMF no se puede rasterizar, se deja la imagen original */ }
            }
        }

        /// <summary>HTML minimo que embebe el PDF generado dentro del iframe de la vista.</summary>
        private string GenerarWrapperPdf(string pdfNombre)
        {
            string bust = DateTime.Now.Ticks.ToString(CultureInfo.InvariantCulture);
            return "<!DOCTYPE html><html lang='es'><head><meta charset='UTF-8'>" +
                "<title>Solicitud de Bienes y Servicios</title>" +
                "<style>html,body{margin:0;padding:0;height:100%;overflow:hidden;}" +
                "iframe{width:100%;height:100%;border:0;}</style></head>" +
                "<body><iframe src='" + pdfNombre + "?v=" + bust +
                "#toolbar=1&navpanes=0' type='application/pdf'></iframe></body></html>";
        }

        /// <summary>
        /// Inyecta la descripcion de uso distribuyendola por palabras a lo largo de los renglones
        /// disponibles del area de descripcion (la celda del marcador y las filas a ancho completo
        /// que le siguen, hasta la seccion "Partidas Presupuestarias"), una linea por renglon, tal
        /// como se llena el formulario fisico. Asi el texto NO se amontona en la primera celda: al
        /// terminar el ancho de una linea/celda, salta al siguiente renglon.
        /// </summary>
        private void InyectarDescripcionExcel(Ss.Worksheet ws, string descripcion)
        {
            Ss.CellRange used = ws.UsedCellRange;

            // 1. Localizar la celda del marcador {{DESCRIPCION_USO}}.
            int markerRow = -1, markerCol = -1;
            for (int r = used.FromIndex.RowIndex; r <= used.ToIndex.RowIndex && markerRow < 0; r++)
                for (int c = used.FromIndex.ColumnIndex; c <= used.ToIndex.ColumnIndex; c++)
                {
                    string t = TextoCeldaCrudo(ws, r, c);
                    if (t != null && t.Contains("{{DESCRIPCION_USO}}")) { markerRow = r; markerCol = c; break; }
                }
            if (markerRow < 0) return;

            // Preservar cualquier texto estatico que acompañe al marcador en su celda (p.ej. si el
            // marcador estuviera junto a una etiqueta), reemplazandolo por la primera linea luego.
            string prefijo = (TextoCeldaCrudo(ws, markerRow, markerCol) ?? "").Replace("{{DESCRIPCION_USO}}", "");

            // 2. Reunir los renglones destino: primero la propia celda del marcador, luego las filas
            //    de abajo (ancladas en la primera columna con contenido/merge, tipicamente B) hasta
            //    encontrar la seccion "Partidas Presupuestarias" o el fin del area de descripcion.
            var renglones = new List<int[]>(); // {fila, columnaAncla}
            renglones.Add(new[] { markerRow, markerCol });
            for (int r = markerRow + 1; r <= used.ToIndex.RowIndex; r++)
            {
                if (FilaContiene(ws, r, "PARTIDAS PRESUP")) break;
                renglones.Add(new[] { r, AnclaRenglon(ws, r, used.FromIndex.ColumnIndex) });
            }

            // 3. Word-wrap greedy: repartir palabras por renglon segun la capacidad (ancho) de cada uno.
            double fs = 10;
            try { double v = ws.Cells.GetCellSelection(markerRow, markerCol).GetFontSize().Value; if (v > 0) fs = v; } catch { }
            double charW = Math.Max(1, fs * 96.0 / 72.0 * 0.52); // ancho aprox. de caracter en px

            string[] palabras = (descripcion ?? "").Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            int pi = 0;
            for (int i = 0; i < renglones.Count; i++)
            {
                int fila = renglones[i][0], col = renglones[i][1];
                // Ancho disponible: para el renglon del marcador, el de su celda combinada (la parte
                // a la derecha de la etiqueta); para los renglones de continuacion, desde su ancla
                // hasta el borde derecho del contenido (asi las filas sin merge no calculan un ancho
                // angosto erroneo que desalinearia esa linea).
                double ancho = (i == 0)
                    ? AnchoCeldaEfectiva(ws, fila, col)
                    : SumaAnchoColumnas(ws, col, FormMaxCol + 1); // hasta el borde del formulario (col U)
                ancho *= 0.92; // margen de seguridad
                int capacidad = Math.Max(1, (int)(ancho / charW));

                // El primer renglon puede llevar un prefijo estatico (normalmente vacio).
                string linea = (i == 0) ? prefijo : "";
                bool ultimoRenglon = (i == renglones.Count - 1);

                while (pi < palabras.Length)
                {
                    string candidata = (linea.Length == 0) ? palabras[pi] : linea + " " + palabras[pi];
                    // En el ultimo renglon disponible se mete todo lo que quede (aunque exceda).
                    if (!ultimoRenglon && candidata.Length > capacidad && linea.Length > 0) break;
                    linea = candidata;
                    pi++;
                }

                Ss.CellSelection sel = ws.Cells.GetCellSelection(fila, col);
                sel.SetValueAsText(linea);
                // Alinear a la izquierda sin sangria para que todas las lineas arranquen en el mismo
                // borde (las filas sin celda combinada pueden heredar una alineacion/sangria distinta).
                try { sel.SetHorizontalAlignment(Ss.RadHorizontalAlignment.Left); sel.SetIndent(0); } catch { }
                if (ultimoRenglon) sel.SetIsWrapped(true); // por si sobra texto en el ultimo renglon

                if (pi >= palabras.Length && i < renglones.Count - 1)
                {
                    // Ya no queda texto: limpiar los renglones restantes por si tuvieran algo.
                    for (int j = i + 1; j < renglones.Count; j++)
                        ws.Cells.GetCellSelection(renglones[j][0], renglones[j][1]).SetValueAsText("");
                    break;
                }
            }
        }

        /// <summary>Indica si alguna celda de la fila contiene el texto dado (mayusculas).</summary>
        private bool FilaContiene(Ss.Worksheet ws, int fila, string textoMayus)
        {
            Ss.CellRange used = ws.UsedCellRange;
            for (int c = used.FromIndex.ColumnIndex; c <= used.ToIndex.ColumnIndex; c++)
            {
                string t = TextoCeldaCrudo(ws, fila, c);
                if (!string.IsNullOrEmpty(t) && t.ToUpperInvariant().Contains(textoMayus)) return true;
            }
            return false;
        }

        /// <summary>
        /// Columna de anclaje del renglon de descripcion: la esquina izquierda del rango combinado
        /// a ancho completo de esa fila (tipicamente B). Si la fila no tiene merge, cae a la primera
        /// columna con contenido despues del margen izquierdo.
        /// </summary>
        private int AnclaRenglon(Ss.Worksheet ws, int fila, int usedFromCol)
        {
            Ss.CellRange used = ws.UsedCellRange;
            for (int c = usedFromCol; c <= used.ToIndex.ColumnIndex; c++)
            {
                Ss.CellRange contain;
                if (ws.Cells.TryGetContainingMergedRange(new Ss.CellIndex(fila, c), out contain)
                    && contain.ToIndex.ColumnIndex - contain.FromIndex.ColumnIndex >= 3)
                    return contain.FromIndex.ColumnIndex;
            }
            // Sin merge ancho: saltar la columna-margen A (0) si aplica.
            return Math.Min(usedFromCol + 1, used.ToIndex.ColumnIndex);
        }

        /// <summary>Ancho efectivo de una celda: si esta combinada, la suma de sus columnas.</summary>
        private double AnchoCeldaEfectiva(Ss.Worksheet ws, int r, int c)
        {
            Ss.CellRange contain;
            if (ws.Cells.TryGetContainingMergedRange(new Ss.CellIndex(r, c), out contain))
                return SumaAnchoColumnas(ws, contain.FromIndex.ColumnIndex, contain.ToIndex.ColumnIndex + 1);
            return AnchoColumnaPx(ws, c);
        }

        /// <summary>Busca la celda que contiene {{TABLA_RENGLONES}} dentro del rango usado de FORM.</summary>
        private bool LocalizarMarcadorRenglones(Ss.Worksheet ws, out Ss.CellIndex inicio)
        {
            inicio = new Ss.CellIndex(0, 0);
            Ss.CellRange used = ws.UsedCellRange;
            for (int r = used.FromIndex.RowIndex; r <= used.ToIndex.RowIndex; r++)
            {
                for (int c = used.FromIndex.ColumnIndex; c <= used.ToIndex.ColumnIndex; c++)
                {
                    string txt = TextoCeldaCrudo(ws, r, c);
                    if (txt != null && txt.Contains("{{TABLA_RENGLONES}}"))
                    {
                        inicio = new Ss.CellIndex(r, c);
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>Reemplaza marcadores de texto simples celda por celda, preservando el formato.</summary>
        private void ReemplazarMarcadoresExcel(Ss.Worksheet ws, Dictionary<string, string> reemplazos)
        {
            Ss.CellRange used = ws.UsedCellRange;
            for (int r = used.FromIndex.RowIndex; r <= used.ToIndex.RowIndex; r++)
            {
                for (int c = used.FromIndex.ColumnIndex; c <= used.ToIndex.ColumnIndex; c++)
                {
                    string original = TextoCeldaCrudo(ws, r, c);
                    if (string.IsNullOrEmpty(original) || original.IndexOf("{{", StringComparison.Ordinal) < 0)
                        continue;

                    string nuevo = original;
                    foreach (var kvp in reemplazos)
                        if (nuevo.Contains(kvp.Key)) nuevo = nuevo.Replace(kvp.Key, kvp.Value);

                    if (!ReferenceEquals(nuevo, original) && nuevo != original)
                        ws.Cells.GetCellSelection(r, c).SetValueAsText(nuevo);
                }
            }
        }

        /// <summary>
        /// Cuenta las filas de la "banda" de renglones: desde la fila marcadora hasta (sin incluir)
        /// la fila de la etiqueta "Especifique el uso..." (inicio de la seccion de descripcion). Es
        /// la cantidad de renglones que caben visualmente en una pagina del formulario.
        /// </summary>
        private int ContarFilasBanda(Ss.Worksheet ws, int markerRow)
        {
            Ss.CellRange used = ws.UsedCellRange;
            for (int r = markerRow + 1; r <= used.ToIndex.RowIndex; r++)
                if (FilaContiene(ws, r, "ESPECIFIQUE EL USO"))
                    return r - markerRow;
            // Fallback razonable si no se encuentra la etiqueta.
            return Math.Max(1, Math.Min(24, used.ToIndex.RowIndex - markerRow));
        }

        private const string MarcadorContinuaRenglon = "***************Continua***************";

        /// <summary>
        /// Una linea fisica de la banda de renglones a inyectar: o bien una linea real de la
        /// descripcion de un renglon (con su renglon de origen, para saber si es la primera linea y
        /// hay que mostrar renglon/cantidad/unidad), o el marcador de continuacion cuando la
        /// descripcion de un renglon se corta al llegar al final de una pagina.
        /// </summary>
        private class LineaRenglon
        {
            public DataRow Renglon;
            public string Texto;
            public bool EsPrimeraLineaDelRenglon;
            public bool EsMarcadorContinua;
        }

        /// <summary>
        /// Reparte TODAS las lineas de descripcion de TODOS los renglones (aplanadas en una sola
        /// secuencia) en paginas de filasBanda filas cada una, llenando siempre las 24 filas de la
        /// banda antes de saltar de pagina -- no se dejan renglones enteros para la pagina siguiente
        /// solo porque no entran completos. Si la ultima fila disponible de una pagina caeria a mitad
        /// de la descripcion de un renglon (no es su ultima linea), esa fila se reemplaza por el
        /// marcador "***************Continua***************" y la linea que hubiera ido ahi se
        /// difiere intacta al principio de la pagina siguiente, retomando exactamente donde se corto.
        /// </summary>
        private List<List<LineaRenglon>> RepartirRenglones(Ss.Worksheet ws, DataTable dt, int[] cols, int markerRow, int filasBanda)
        {
            int descCol = cols[3];
            int descColFin = ColumnaFinDescripcion(cols);
            double anchoDesc = (descCol >= 0 && descColFin > descCol)
                ? SumaAnchoColumnas(ws, descCol, descColFin + 1) * 0.94
                : AnchoCeldaEfectiva(ws, markerRow, descCol) * 0.94;
            string fontFamily; double fontSizePx;
            ObtenerFuenteCelda(ws, markerRow, descCol, out fontFamily, out fontSizePx);

            var todasLasLineas = new List<LineaRenglon>();
            foreach (DataRow dr in dt.Rows)
            {
                string desc = (3 < dr.Table.Columns.Count) ? Convert.ToString(dr[3]) : "";
                List<string> lineas = DistribuirEnLineas(desc, fontFamily, fontSizePx, anchoDesc);
                for (int li = 0; li < lineas.Count; li++)
                    todasLasLineas.Add(new LineaRenglon { Renglon = dr, Texto = lineas[li], EsPrimeraLineaDelRenglon = (li == 0) });
            }

            var paginas = new List<List<LineaRenglon>>();
            int idx = 0;
            while (idx < todasLasLineas.Count)
            {
                var pagina = new List<LineaRenglon>();
                int filasRestantes = filasBanda;
                while (filasRestantes > 0 && idx < todasLasLineas.Count)
                {
                    LineaRenglon actual = todasLasLineas[idx];
                    bool esUltimaFilaDisponible = filasRestantes == 1;
                    bool hayMasLineasDelMismoRenglonDespues = idx + 1 < todasLasLineas.Count
                        && ReferenceEquals(todasLasLineas[idx + 1].Renglon, actual.Renglon);

                    if (esUltimaFilaDisponible && hayMasLineasDelMismoRenglonDespues)
                    {
                        // Cortaria la descripcion a mitad: se marca "Continua" en vez de la linea, que
                        // se difiere (no se avanza idx) para retomarla al inicio de la proxima pagina.
                        pagina.Add(new LineaRenglon { EsMarcadorContinua = true, Texto = MarcadorContinuaRenglon });
                    }
                    else
                    {
                        pagina.Add(actual);
                        idx++;
                    }
                    filasRestantes--;
                }
                paginas.Add(pagina);
            }
            if (paginas.Count == 0) paginas.Add(new List<LineaRenglon>());
            return paginas;
        }

        /// <summary>
        /// Inyecta las lineas de una pagina en la banda de renglones, tipo factura: cada fila fisica
        /// lleva una linea de descripcion, con renglon/cantidad/unidad/precios solo en la primera
        /// linea de cada renglon. Si la pagina llena las 24 filas y la descripcion de un renglon
        /// queda cortada, la ultima fila lleva el marcador "Continua" (ver RepartirRenglones) y las
        /// lineas restantes de ese mismo renglon llegan como una nueva "pagina" que retoma desde ahi,
        /// sin repetir renglon/cantidad/unidad (no es un renglon nuevo). NINGUNA fila cambia de alto.
        /// </summary>
        private void InyectarLoteRenglones(Ss.Worksheet ws, List<LineaRenglon> lineas, int[] cols, Ss.CellIndex inicio, int filasBanda)
        {
            int rowBase = inicio.RowIndex;
            int descCol = cols[3];
            int descColFin = ColumnaFinDescripcion(cols);
            int limite = rowBase + filasBanda; // primera fila fuera de la banda

            // Limpiar cualquier marcador {{...}} que haya quedado en toda la banda clonada (p.ej.
            // {{TABLA_RENGLONES}} en su fila original) ANTES de escribir: como ahora no todas las
            // filas de la banda reciben renglon/cantidad/unidad (las de continuacion de un renglon
            // cortado por "Continua" no), el marcador podria quedar visible si solo se limpiaran las
            // filas realmente sobrantes al final.
            for (int r = rowBase; r < limite; r++)
                for (int k = 0; k < 6; k++)
                    if (cols[k] >= 0)
                    {
                        string t = TextoCeldaCrudo(ws, r, cols[k]);
                        if (!string.IsNullOrEmpty(t) && t.Contains("{{"))
                            ws.Cells.GetCellSelection(r, cols[k]).SetValueAsText("");
                    }

            int fila = rowBase;
            foreach (LineaRenglon ln in lineas)
            {
                if (fila >= limite) break; // no deberia pasar si RepartirRenglones armo bien la pagina

                if (ln.EsPrimeraLineaDelRenglon)
                {
                    DataRow dr = ln.Renglon;
                    for (int k = 0; k < 6; k++)
                    {
                        if (k == 3 || cols[k] < 0) continue;
                        string valor = (k < dr.Table.Columns.Count) ? Convert.ToString(dr[k]) : "";
                        ws.Cells.GetCellSelection(fila, cols[k]).SetValueAsText(valor);
                    }
                }

                if (descCol >= 0)
                {
                    // La plantilla no combina la celda de descripcion de forma consistente en todas
                    // las filas de la banda (algunas filas quedan sin combinar, o combinadas a un
                    // ancho distinto): se fuerza aca la misma combinacion en cada fila para que el
                    // ancho real de render coincida con el anchoDesc usado para medir el texto.
                    if (descColFin > descCol)
                    {
                        Ss.CellSelection rangoDesc = ws.Cells.GetCellSelection(fila, descCol, fila, descColFin);
                        rangoDesc.Unmerge();
                        rangoDesc.Merge();
                    }

                    Ss.CellSelection descSel = ws.Cells.GetCellSelection(fila, descCol);
                    descSel.SetValueAsText(ln.Texto);
                    descSel.SetIsWrapped(false); // cada fila ya lleva exactamente una linea ajustada
                    try
                    {
                        descSel.SetHorizontalAlignment(ln.EsMarcadorContinua ? Ss.RadHorizontalAlignment.Center : Ss.RadHorizontalAlignment.Left);
                        descSel.SetIndent(0);
                    }
                    catch { }
                }
                fila++;
            }
        }

        /// <summary>
        /// Lee la fuente (familia y tamaño) de una celda, con fallback razonable. El tamaño se
        /// devuelve en PIXELES (96dpi): se verifico empiricamente contra el XML crudo del .xlsx que
        /// CellSelection.GetFontSize() en el modelo Spreadsheet de Telerik devuelve el tamaño ya
        /// convertido a px (p.ej. una fuente reelmente de 10pt devuelve 13.33 = 10*96/72), igual que
        /// el resto de las medidas de este modelo (PageSize, margenes, anchos de columna).
        /// </summary>
        private void ObtenerFuenteCelda(Ss.Worksheet ws, int row, int col, out string fontFamily, out double fontSizePx)
        {
            fontFamily = "Arial"; fontSizePx = 10 * 96.0 / 72.0;
            try
            {
                Ss.CellSelection sel = ws.Cells.GetCellSelection(row, col);
                var ff = sel.GetFontFamily().Value;
                string nombre = (ff != null && ff.LocalValue != null) ? ff.LocalValue.Source : null;
                if (!string.IsNullOrEmpty(nombre)) fontFamily = nombre;
                double fs = sel.GetFontSize().Value;
                if (fs > 0) fontSizePx = fs;
            }
            catch { }
        }

        /// <summary>
        /// Parte un texto en lineas ajustadas a un ancho disponible en px, tipo factura: reparto
        /// greedy palabra por palabra, midiendo cada linea candidata con GDI+ (Graphics.MeasureString)
        /// con la fuente y tamaño reales de la celda -- en vez de estimar por cantidad de caracteres,
        /// que sobre- o sub-estima segun mayusculas/fuente. Cada linea resultante ocupa exactamente
        /// una fila fisica de la banda de renglones (no una celda combinada con word-wrap interno).
        /// </summary>
        private List<string> DistribuirEnLineas(string texto, string fontFamily, double fontSizePx, double anchoDisponiblePx)
        {
            var lineas = new List<string>();
            if (string.IsNullOrEmpty(texto) || anchoDisponiblePx < 1) { lineas.Add(texto ?? ""); return lineas; }

            string[] palabras = texto.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            using (Bitmap bmp = new Bitmap(1, 1))
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.PageUnit = GraphicsUnit.Pixel;
                using (Font f = new Font(fontFamily, (float)fontSizePx, FontStyle.Regular, GraphicsUnit.Pixel))
                {
                    string linea = "";
                    foreach (string palabra in palabras)
                    {
                        string candidata = (linea.Length == 0) ? palabra : linea + " " + palabra;
                        double anchoCandidata = g.MeasureString(candidata, f).Width;
                        if (linea.Length > 0 && anchoCandidata > anchoDisponiblePx)
                        {
                            lineas.Add(linea);
                            linea = palabra;
                        }
                        else
                        {
                            linea = candidata;
                        }
                    }
                    if (linea.Length > 0 || lineas.Count == 0) lineas.Add(linea);
                }
            }
            return lineas;
        }

        /// <summary>
        /// Determina la columna real de cada una de las 6 columnas logicas del renglon buscando sus
        /// etiquetas en la fila de encabezado (hasta 4 filas arriba de la fila marcadora). Cada
        /// etiqueta se ubica en el anclaje (columna inicial) de su celda combinada, que coincide con
        /// el anclaje de la misma columna en las filas de datos. Si no se encuentra el encabezado,
        /// cae de vuelta a 6 columnas consecutivas desde el marcador.
        /// </summary>
        private int[] MapearColumnasRenglones(Ss.Worksheet ws, int rowBase, int colInicio)
        {
            string[] claves = { "RENGL", "CANTIDAD", "UNIDAD", "DESCRIPCI", "UNITARIO", "TOTAL" };
            int[] cols = { -1, -1, -1, -1, -1, -1 };

            Ss.CellRange used = ws.UsedCellRange;
            int desde = Math.Max(0, rowBase - 4);
            for (int r = desde; r < rowBase; r++)
            {
                for (int c = used.FromIndex.ColumnIndex; c <= used.ToIndex.ColumnIndex; c++)
                {
                    string t = TextoCeldaCrudo(ws, r, c);
                    if (string.IsNullOrEmpty(t)) continue;
                    string up = t.ToUpperInvariant();
                    for (int k = 0; k < 6; k++)
                        if (cols[k] < 0 && up.Contains(claves[k])) cols[k] = c;
                }
            }

            for (int k = 0; k < 6; k++)
                if (cols[k] < 0) cols[k] = colInicio + k; // fallback
            return cols;
        }

        /// <summary>
        /// Busca la primera fila completamente vacia justo debajo del cuadro "PARA USO DEL
        /// DEPARTAMENTO..." (hasta 8 filas mas abajo), para colocar ahi el numero de pagina dentro
        /// del area imprimible. Devuelve -1 si no encuentra el cuadro o no hay una fila libre.
        /// </summary>
        private int LocalizarFilaPaginacion(Ss.Worksheet ws)
        {
            Ss.CellRange used = ws.UsedCellRange;
            int filaCuadro = -1;
            for (int r = used.FromIndex.RowIndex; r <= used.ToIndex.RowIndex; r++)
                if (FilaContiene(ws, r, "PARA USO DEL DEPARTAMENTO")) { filaCuadro = r; break; }
            if (filaCuadro < 0) return -1;

            int limite = Math.Min(filaCuadro + 8, used.ToIndex.RowIndex);
            for (int r = filaCuadro + 1; r <= limite; r++)
            {
                string t = TextoCeldaCrudo(ws, r, 1);
                Ss.CellRange contain;
                bool combinada = ws.Cells.TryGetContainingMergedRange(new Ss.CellIndex(r, 1), out contain);
                if (string.IsNullOrEmpty(t) && !combinada) return r;
            }
            return -1;
        }

        /// <summary>
        /// Ultima columna del area de descripcion: la columna justo antes de donde empieza UNITARIO
        /// (limite real detectado en el encabezado), no una letra fija -- la plantilla combina la
        /// celda de descripcion con un ancho distinto segun la fila (algunas hasta la columna previa
        /// a UNITARIO, otras una columna antes, y otras sin combinar), asi que este limite se usa
        /// para forzar la misma combinacion/ancho en todas las filas de la banda.
        /// </summary>
        private int ColumnaFinDescripcion(int[] cols)
        {
            int descCol = cols[3];
            int unitarioCol = cols[4];
            return (descCol >= 0 && unitarioCol > descCol) ? unitarioCol - 1 : descCol;
        }

        private double AnchoColumnaPx(Ss.Worksheet ws, int col)
        {
            try
            {
                var w = ws.Columns.GetColumnSelection(col).GetWidth();
                if (!w.IsIndeterminate && w.Value != null) return w.Value.Value;
            }
            catch { }
            return ws.Columns.GetDefaultWidth().Value;
        }

        private double SumaAnchoColumnas(Ss.Worksheet ws, int desde, int hasta)
        {
            double total = 0;
            for (int c = desde; c < hasta; c++) total += AnchoColumnaPx(ws, c);
            return total;
        }

        private string TextoCeldaCrudo(Ss.Worksheet ws, int r, int c)
        {
            try
            {
                var val = ws.Cells.GetCellSelection(r, c).GetValue().Value;
                return val != null ? val.RawValue : null;
            }
            catch { return null; }
        }

        // =============================================================
        // TEXT REPLACEMENT
        // =============================================================
        private void ReemplazarMarcadoresEnDocumento(RadFlowDocument doc, Dictionary<string, string> reemplazos)
        {
            foreach (var section in doc.Sections)
            {
                ReemplazarEnContainer(section, reemplazos);
                if (section.Headers != null && section.Headers.Default != null)
                    ReemplazarEnContainer(section.Headers.Default, reemplazos);
                if (section.Footers != null && section.Footers.Default != null)
                    ReemplazarEnContainer(section.Footers.Default, reemplazos);
            }
        }

        private void ReemplazarEnContainer(BlockContainerBase container, Dictionary<string, string> reemplazos)
        {
            foreach (var block in container.Blocks.ToList())
            {
                if (block is Paragraph p) ReemplazarEnParrafo(p, reemplazos);
                else if (block is Table t)
                    foreach (var row in t.Rows)
                        foreach (var cell in row.Cells)
                            ReemplazarEnContainer(cell, reemplazos);
            }
        }

        // NOTA DE UNIDADES: Run.FontSize asignado por API se exporta a HTML como pixeles LITERALES
        // (verificado empiricamente: FontSize=6 exporta "font-size:6px", no "8px" como si fueran
        // puntos convertidos). Esto es distinto de los tamaños heredados del estilo del .docx
        // original, que si se convierten correctamente de puntos a pixeles (96/72) al exportar. Las
        // constantes de abajo ya estan expresadas en px, con su equivalente en puntos en el comentario,
        // para no fijar por error un tamaño mas chico que el que se busca reemplazar.

        // Campos de cabecera cuyo valor inyectado puede ser largo (nombres de directores,
        // funcionarios o unidades/direcciones institucionales). Se reduce su fuente para que
        // quepan en una sola linea sin desbordar la celda ni romper la alineacion con el
        // subrayado de cada campo.
        private static readonly string[] MarcadoresEncabezadoCompacto = { "{{DIR_ADMIN}}", "{{SOLICITANTE}}", "{{UNIDAD}}" };
        private const double FontSizeEncabezadoCompacto = 12; // ~9pt

        // Tamaño de fuente para el cuerpo de la solicitud (tabla de renglones y descripcion de uso),
        // usado como fallback cuando no se puede preservar el tamaño de una celda existente. Mayor
        // que el tamaño base del documento (11pt = 14.67px) para mejorar la legibilidad general.
        private const double FontSizeCuerpo = 16; // ~12pt

        private void ReemplazarEnParrafo(Paragraph paragraph, Dictionary<string, string> reemplazos)
        {
            var runs = paragraph.Inlines.OfType<Run>().ToList();
            if (runs.Count == 0) return;
            string texto = string.Join("", runs.Select(r => r.Text));
            bool changed = false;
            bool esCampoCompacto = false;
            foreach (var kvp in reemplazos)
            {
                if (texto.Contains(kvp.Key))
                {
                    texto = texto.Replace(kvp.Key, kvp.Value);
                    changed = true;
                    if (Array.IndexOf(MarcadoresEncabezadoCompacto, kvp.Key) >= 0) esCampoCompacto = true;
                }
            }
            if (changed)
            {
                for (int i = runs.Count - 1; i > 0; i--) paragraph.Inlines.Remove(runs[i]);
                runs[0].Text = texto;
                if (esCampoCompacto) runs[0].FontSize = FontSizeEncabezadoCompacto;
            }
        }

        // =============================================================
        // DESCRIPCION_USO WITH LINE BREAKS
        // Strategy: Find marker in table cell, clear it, put text in 
        // the full-width empty rows below (merged cells).
        // =============================================================
        private void ReemplazarDescripcionConSaltos(RadFlowDocument doc, string marcador, string texto)
        {
            foreach (var section in doc.Sections)
            {
                foreach (var block in section.Blocks.ToList())
                {
                    if (block is Table table)
                    {
                        if (BuscarDescripcionEnTabla(table, marcador, texto))
                            return;
                    }
                    else if (block is Paragraph p)
                    {
                        // Fallback: if marker is in a standalone paragraph (not in a table)
                        var runs = p.Inlines.OfType<Run>().ToList();
                        string concat = string.Join("", runs.Select(r => r.Text));
                        if (concat.Contains(marcador))
                        {
                            double fontSize = runs.Count > 0 ? runs[0].FontSize : 10;
                            string replaced = concat.Replace(marcador, texto);
                            for (int r = runs.Count - 1; r > 0; r--) p.Inlines.Remove(runs[r]);
                            if (runs.Count > 0) runs[0].Text = replaced;
                            return;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Searches a table for {{DESCRIPCION_USO}}, clears the marker cell,
        /// and fills the empty full-width rows below with the description text.
        /// </summary>
        private bool BuscarDescripcionEnTabla(Table table, string marcador, string texto)
        {
            int markerRowIdx = -1;

            // 1. Find which row contains the marker
            for (int r = 0; r < table.Rows.Count; r++)
            {
                foreach (var cell in table.Rows[r].Cells)
                {
                    foreach (var block in cell.Blocks)
                    {
                        if (block is Paragraph p)
                        {
                            string cellText = string.Join("", p.Inlines.OfType<Run>().Select(x => x.Text));
                            if (cellText.Contains(marcador))
                            {
                                // Clear the marker from this cell
                                var runs = p.Inlines.OfType<Run>().ToList();
                                string labelOnly = cellText.Replace(marcador, "").Trim();
                                for (int i = runs.Count - 1; i > 0; i--) p.Inlines.Remove(runs[i]);
                                if (runs.Count > 0) runs[0].Text = labelOnly;
                                markerRowIdx = r;
                                break;
                            }
                        }
                    }
                    if (markerRowIdx >= 0) break;
                }
                if (markerRowIdx >= 0) break;
            }

            if (markerRowIdx < 0) return false;

            // 2. Collect all available empty rows below the marker row
            var emptyRows = new List<TableCell>();
            double sharedFontSize = FontSizeCuerpo;
            bool fontDetected = false;

            for (int r = markerRowIdx + 1; r < table.Rows.Count; r++)
            {
                var row = table.Rows[r];
                if (row.Cells.Count == 0) continue;

                var targetCell = row.Cells[0];

                // Detect font size from first empty row
                if (!fontDetected)
                {
                    var existBlocks = targetCell.Blocks.ToList();
                    if (existBlocks.Count > 0 && existBlocks[0] is Paragraph ep)
                    {
                        var er = ep.Inlines.OfType<Run>().ToList();
                        if (er.Count > 0 && er[0].FontSize > 0)
                        { sharedFontSize = er[0].FontSize; fontDetected = true; }
                    }
                }

                // Check if cell is empty
                string existingText = "";
                foreach (var block in targetCell.Blocks)
                    if (block is Paragraph pp)
                        existingText += string.Join("", pp.Inlines.OfType<Run>().Select(x => x.Text));

                if (string.IsNullOrWhiteSpace(existingText))
                    emptyRows.Add(targetCell);
            }

            if (emptyRows.Count == 0) return true; // No empty rows, nothing to fill

            // 3. Split text into chunks: one chunk per available row
            //    Strategy: split at explicit \n first, then chunk by words if needed
            string[] rawLineas;
            if (texto.Contains("\r\n") || texto.Contains("\n"))
                rawLineas = texto.Split(new[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries);
            else
                rawLineas = new[] { texto };

            // If we have more explicit lines than rows, merge the overflow into the last row
            // If we have fewer lines than rows, distribute by splitting the longest lines by words
            List<string> chunks = DistribuirEnChunks(rawLineas, emptyRows.Count);

            // 4. Write one chunk per empty row
            for (int i = 0; i < emptyRows.Count; i++)
            {
                var cell = emptyRows[i];
                string chunkText = i < chunks.Count ? chunks[i] : "";

                // Clear existing content
                foreach (var b in cell.Blocks.ToList()) cell.Blocks.Remove(b);

                // Write chunk
                Paragraph np = cell.Blocks.AddParagraph();
                Run nr = np.Inlines.AddRun(chunkText);
                nr.FontSize = sharedFontSize;
            }

            return true;
        }

        /// <summary>
        /// Distributes lines of text into exactly targetCount chunks.
        /// If too few lines: splits longest lines by words to fill all slots.
        /// If too many lines: merges excess into the last slot.
        /// </summary>
        private List<string> DistribuirEnChunks(string[] lineas, int targetCount)
        {
            var chunks = new List<string>(lineas);

            // If text is a single long string with no breaks, split by words
            if (chunks.Count == 1 && targetCount > 1)
            {
                string full = chunks[0];
                string[] words = full.Split(' ');
                int wordsPerChunk = Math.Max(1, (int)Math.Ceiling((double)words.Length / targetCount));
                chunks.Clear();
                for (int i = 0; i < words.Length; i += wordsPerChunk)
                {
                    int take = Math.Min(wordsPerChunk, words.Length - i);
                    chunks.Add(string.Join(" ", words, i, take));
                }
            }

            // If still fewer chunks than rows, pad with empty strings
            while (chunks.Count < targetCount) chunks.Add("");

            // If more chunks than rows, merge excess into last slot
            if (chunks.Count > targetCount)
            {
                var merged = chunks.Take(targetCount - 1).ToList();
                merged.Add(string.Join(" ", chunks.Skip(targetCount - 1)));
                chunks = merged;
            }

            return chunks;
        }

        // =============================================================
        // TABLA_RENGLONES - Strategy A: Replace marker text
        // =============================================================
        private bool BuscarYReemplazarMarcadorTabla(RadFlowDocument doc, DataTable dt)
        {
            foreach (var section in doc.Sections)
                if (BuscarMarcadorTablaEnContainer(doc, section, dt)) return true;
            return false;
        }

        private bool BuscarMarcadorTablaEnContainer(RadFlowDocument doc, BlockContainerBase container, DataTable dt)
        {
            var blocks = container.Blocks.ToList();
            for (int i = 0; i < blocks.Count; i++)
            {
                if (blocks[i] is Paragraph p)
                {
                    string texto = string.Join("", p.Inlines.OfType<Run>().Select(r => r.Text));
                    if (texto.Contains("{{TABLA_RENGLONES}}"))
                    {
                        container.Blocks.Remove(p);
                        Table table = CrearTablaRenglones(doc, dt);
                        if (i < container.Blocks.Count) container.Blocks.Insert(i, table);
                        else container.Blocks.Add(table);
                        return true;
                    }
                }
                else if (blocks[i] is Table t)
                {
                    foreach (var row in t.Rows)
                        foreach (var cell in row.Cells)
                            if (BuscarMarcadorTablaEnContainer(doc, cell, dt)) return true;
                }
            }
            return false;
        }

        // =============================================================
        // TABLA_RENGLONES - Strategy B: Find table by headers, populate rows
        // =============================================================
        private void PoblarTablaExistentePorEncabezados(RadFlowDocument doc, DataTable dt)
        {
            foreach (var section in doc.Sections)
            {
                foreach (var block in section.Blocks.ToList())
                {
                    if (block is Table table)
                    {
                        if (EsTablaDeRenglones(table))
                        {
                            LlenarFilasDeTabla(table, dt, doc);
                            return;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Detects if this is the data table by looking for "RENGL" or "CANTIDAD" in first 2 rows.
        /// </summary>
        private bool EsTablaDeRenglones(Table table)
        {
            if (table.Rows.Count < 2) return false;
            string headerText = "";
            for (int r = 0; r < Math.Min(2, table.Rows.Count); r++)
            {
                foreach (var cell in table.Rows[r].Cells)
                {
                    foreach (var block in cell.Blocks)
                    {
                        if (block is Paragraph p)
                            headerText += string.Join("", p.Inlines.OfType<Run>().Select(x => x.Text));
                    }
                }
            }
            headerText = headerText.ToUpper();
            return headerText.Contains("RENGL") && headerText.Contains("CANTIDAD") && headerText.Contains("DESCRIPCI");
        }

        /// <summary>
        /// Fills the empty rows of an existing table with data.
        /// Skips header rows (first 2), then fills subsequent empty rows.
        /// Columns: [0]=renglon, [1]=cantidad, [2]=unidad, [3]=descripcion, [4]=unitario, [5]=total
        /// </summary>
        private void LlenarFilasDeTabla(Table table, DataTable dt, RadFlowDocument doc)
        {
            // Find first data row (skip headers - rows containing bold text or header keywords)
            int firstDataRow = 0;
            for (int r = 0; r < table.Rows.Count; r++)
            {
                string rowText = "";
                foreach (var cell in table.Rows[r].Cells)
                    foreach (var block in cell.Blocks)
                        if (block is Paragraph p)
                            rowText += string.Join("", p.Inlines.OfType<Run>().Select(x => x.Text));

                rowText = rowText.ToUpper().Trim();
                if (rowText.Contains("RENGL") || rowText.Contains("CANTIDAD") || rowText.Contains("UNITARIO") || rowText.Contains("TOTAL"))
                {
                    firstDataRow = r + 1;
                }
            }

            // Fill data into existing empty rows
            int dataIdx = 0;
            for (int r = firstDataRow; r < table.Rows.Count && dataIdx < dt.Rows.Count; r++)
            {
                var row = table.Rows[r];
                int cellCount = row.Cells.Count;
                if (cellCount < 6) continue; // Need at least 6 columns

                DataRow dr = dt.Rows[dataIdx];
                // Map: cell[0]=renglon, cell[1]=cantidad, cell[2]=unidad, cell[3]=descripcion, cell[4]=unitario, cell[5]=total
                for (int c = 0; c < 6 && c < cellCount; c++)
                {
                    string valor = (c < dr.Table.Columns.Count) ? dr[c].ToString() : "";
                    var cell = row.Cells[c];
                    // Clear existing content and set new value
                    var existingBlocks = cell.Blocks.ToList();
                    double fontSize = FontSizeCuerpo;
                    // Try to preserve font from existing paragraph
                    if (existingBlocks.Count > 0 && existingBlocks[0] is Paragraph existP)
                    {
                        var existRuns = existP.Inlines.OfType<Run>().ToList();
                        if (existRuns.Count > 0 && existRuns[0].FontSize > 0)
                            fontSize = existRuns[0].FontSize;
                    }
                    // Remove existing blocks
                    foreach (var b in existingBlocks) cell.Blocks.Remove(b);
                    // Add new paragraph with data
                    Paragraph np = cell.Blocks.AddParagraph();
                    Run nr = np.Inlines.AddRun(valor);
                    nr.FontSize = fontSize;
                }
                dataIdx++;
            }

            // If we still have data but ran out of rows, add new rows
            while (dataIdx < dt.Rows.Count)
            {
                DataRow dr = dt.Rows[dataIdx];
                TableRow newRow = table.Rows.AddTableRow();
                for (int c = 0; c < 6; c++)
                {
                    TableCell cell = newRow.Cells.AddTableCell();
                    cell.Borders = new TableCellBorders(new Border(BorderStyle.Single));
                    string valor = (c < dr.Table.Columns.Count) ? dr[c].ToString() : "";
                    Paragraph p = cell.Blocks.AddParagraph();
                    Run run = p.Inlines.AddRun(valor);
                    run.FontSize = FontSizeCuerpo;
                }
                dataIdx++;
            }
        }

        private Table CrearTablaRenglones(RadFlowDocument doc, DataTable dt)
        {
            Table table = new Table(doc);
            table.PreferredWidth = new TableWidthUnit(TableWidthUnitType.Percent, 100);
            var border = new Border(BorderStyle.Single);
            // Header
            TableRow hr = table.Rows.AddTableRow();
            foreach (string h in new[] { "RENGLÓN", "CANTIDAD", "UNIDAD", "DESCRIPCIÓN", "UNITARIO", "TOTAL" })
            {
                TableCell c = hr.Cells.AddTableCell(); c.Borders = new TableCellBorders(border);
                Paragraph p = c.Blocks.AddParagraph(); p.TextAlignment = Alignment.Center;
                Run r = p.Inlines.AddRun(h); r.FontSize = FontSizeCuerpo; r.FontWeight = System.Windows.FontWeights.Bold;
            }
            // Data
            foreach (DataRow dr in dt.Rows)
            {
                TableRow row = table.Rows.AddTableRow();
                for (int c = 0; c < 6; c++)
                {
                    TableCell cell = row.Cells.AddTableCell(); cell.Borders = new TableCellBorders(border);
                    Paragraph p = cell.Blocks.AddParagraph();
                    p.TextAlignment = (c == 3) ? Alignment.Left : Alignment.Center;
                    Run r = p.Inlines.AddRun((c < dr.Table.Columns.Count) ? dr[c].ToString() : "");
                    r.FontSize = FontSizeCuerpo;
                }
            }
            return table;
        }

        // =============================================================
        // FLOATING HEADER EXTRACTION (anchored logo + floating versioning table)
        // =============================================================

        /// <summary>
        /// Holds the data needed to re-render the header elements that Word
        /// positions via absolute/floating layout (DrawingML anchor, table tblpPr) —
        /// a layout HtmlFormatProvider does not preserve when exporting to flow HTML.
        /// </summary>
        private class EncabezadoFlotanteInfo
        {
            public bool TieneLogo;
            public string LogoDataUri;
            public double LogoLeft, LogoTop, LogoWidth, LogoHeight;

            public bool TieneTablaVersion;
            public string TablaVersionHtml;
        }

        /// <summary>
        /// Finds the anchored logo image and the floating "Código/Emisión/Versión..."
        /// table (if present), captures what is needed to redraw them with CSS
        /// absolute positioning, and detaches them from the document so the normal
        /// HtmlFormatProvider export of the rest of the body doesn't render them
        /// misplaced (or duplicated) in the flow.
        /// </summary>
        private EncabezadoFlotanteInfo ExtraerYRemoverEncabezadoFlotante(RadFlowDocument document)
        {
            var info = new EncabezadoFlotanteInfo();
            Section section = document.Sections.First();

            // --- Logo: anchored/floating image (DrawingML wp:anchor) ---
            FloatingImage logo = document.EnumerateChildrenOfType<FloatingImage>().FirstOrDefault();
            if (logo != null && logo.Image != null && logo.Image.ImageSource != null && logo.Image.ImageSource.Data != null)
            {
                byte[] data = logo.Image.ImageSource.Data;
                string ext = string.IsNullOrEmpty(logo.Image.ImageSource.Extension)
                    ? "png" : logo.Image.ImageSource.Extension.TrimStart('.').ToLowerInvariant();

                info.LogoWidth = logo.Image.Width;
                info.LogoHeight = logo.Image.Height;
                info.LogoLeft = ResolverPosicionHorizontalPx(logo.HorizontalPosition, section, info.LogoWidth);
                info.LogoTop = ResolverPosicionVerticalPx(logo.VerticalPosition, section, info.LogoHeight);
                info.LogoDataUri = "data:image/" + ext + ";base64," + Convert.ToBase64String(data);
                info.TieneLogo = true;

                if (logo.Paragraph != null)
                {
                    try { logo.Paragraph.Inlines.Remove(logo); }
                    catch { /* if it can't be detached, leave it - Telerik will render it however it can */ }
                }
            }

            // --- Versioning table: floating table (w:tblpPr), not modeled by Telerik's
            //     Table class, so its absolute position can't be read back - it's
            //     re-anchored to the top-right corner of the page margins instead. ---
            Table tablaVersion = null;
            string[] palabrasClave = { "CÓDIGO", "EMISIÓN", "VERSIÓN", "ACTUALIZACIÓN", "ELABORADO", "APROBADO" };
            foreach (var t in document.EnumerateChildrenOfType<Table>())
            {
                string headerText = ObtenerTextoTabla(t, 6).ToUpperInvariant();
                int matches = palabrasClave.Count(kw => headerText.Contains(kw));
                if (matches >= 3) { tablaVersion = t; break; }
            }

            if (tablaVersion != null)
            {
                RadFlowDocument tempDoc = new RadFlowDocument();
                RadFlowDocumentEditor editor = new RadFlowDocumentEditor(tempDoc);
                editor.InsertSection();
                Table clon = tablaVersion.Clone(tempDoc);
                tempDoc.Sections.First().Blocks.Add(clon);

                HtmlFormatProvider htmlProvider = new HtmlFormatProvider();
                string tablaHtmlCompleto = htmlProvider.Export(tempDoc);
                info.TablaVersionHtml = ExtraerCuerpoHtml(tablaHtmlCompleto);
                info.TieneTablaVersion = !string.IsNullOrEmpty(info.TablaVersionHtml);

                BlockContainerBase padre = tablaVersion.Parent as BlockContainerBase;
                if (padre != null)
                {
                    try { padre.Blocks.Remove(tablaVersion); }
                    catch { /* if it can't be detached, leave it in flow as a fallback */ }
                }
            }

            return info;
        }

        private string ObtenerTextoTabla(Table table, int maxRows)
        {
            string result = "";
            int rowIdx = 0;
            foreach (var row in table.Rows)
            {
                if (rowIdx >= maxRows) break;
                foreach (var cell in row.Cells)
                    foreach (var block in cell.Blocks)
                    {
                        Paragraph p = block as Paragraph;
                        if (p != null)
                            result += string.Join("", p.Inlines.OfType<Run>().Select(r => r.Text));
                    }
                rowIdx++;
            }
            return result;
        }

        /// <summary>
        /// Quita el "border-top" fantasma que HtmlFormatProvider agrega a la celda que contiene el
        /// texto estatico indicado y a la celda del valor inyectado inmediatamente siguiente en la
        /// misma fila (p.ej. "Señor (a):" + {{DIR_ADMIN}}), preservando el "border-bottom" real
        /// (el subrayado del campo) y el resto del estilo de la celda intacto.
        /// </summary>
        private string QuitarBordeSuperiorFantasma(string html, string etiquetaEstatica)
        {
            int idx = html.IndexOf(etiquetaEstatica, StringComparison.Ordinal);
            if (idx < 0) return html;

            int tdStart = html.LastIndexOf("<td", idx, StringComparison.OrdinalIgnoreCase);
            if (tdStart < 0) return html;
            int tdOpenEnd = html.IndexOf('>', tdStart);
            if (tdOpenEnd < 0) return html;

            html = QuitarBorderTopDeEtiqueta(html, tdStart, tdOpenEnd);

            // Celda del valor inyectado: el siguiente <td> de la misma fila (antes del </tr>).
            int tdCloseIdx = html.IndexOf("</td>", tdOpenEnd, StringComparison.OrdinalIgnoreCase);
            if (tdCloseIdx < 0) return html;
            int trCloseIdx = html.IndexOf("</tr>", tdCloseIdx, StringComparison.OrdinalIgnoreCase);
            int nextTdStart = html.IndexOf("<td", tdCloseIdx, StringComparison.OrdinalIgnoreCase);
            if (nextTdStart < 0 || (trCloseIdx >= 0 && nextTdStart > trCloseIdx)) return html;
            int nextTdOpenEnd = html.IndexOf('>', nextTdStart);
            if (nextTdOpenEnd < 0) return html;

            return QuitarBorderTopDeEtiqueta(html, nextTdStart, nextTdOpenEnd);
        }

        private string QuitarBorderTopDeEtiqueta(string html, int tagStart, int tagOpenEnd)
        {
            string tagAbierta = html.Substring(tagStart, tagOpenEnd - tagStart + 1);
            string sinBorde = System.Text.RegularExpressions.Regex.Replace(
                tagAbierta, "border-top:[^;\"]*;", "", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            if (sinBorde == tagAbierta) return html;
            return html.Substring(0, tagStart) + sinBorde + html.Substring(tagOpenEnd + 1);
        }

        /// <summary>Pulls the &lt;body&gt; inner content out of a full HTML document exported by Telerik.</summary>
        private string ExtraerCuerpoHtml(string htmlCompleto)
        {
            int start = htmlCompleto.IndexOf("<body>", StringComparison.OrdinalIgnoreCase);
            if (start < 0) return "";
            start += "<body>".Length;
            int end = htmlCompleto.IndexOf("</body>", start, StringComparison.OrdinalIgnoreCase);
            if (end < 0) return "";
            return htmlCompleto.Substring(start, end - start);
        }

        /// <summary>
        /// Resolves a Word floating image's HorizontalPosition to a pixel offset measured
        /// from the page's left edge (Telerik's Flow model already uses 96dpi pixels as its unit).
        /// </summary>
        private double ResolverPosicionHorizontalPx(HorizontalPosition hp, Section section, double elementWidth)
        {
            double pageWidth = section.PageSize.Width;
            double marginLeft = section.PageMargins.Left;
            double marginRight = section.PageMargins.Right;

            if (hp.ValueType == PositionValueType.Alignment)
            {
                switch (hp.Alignment)
                {
                    case RelativeHorizontalAlignment.Center: return (pageWidth - elementWidth) / 2;
                    case RelativeHorizontalAlignment.Right:
                    case RelativeHorizontalAlignment.Outside: return pageWidth - marginRight - elementWidth;
                    default: return marginLeft;
                }
            }

            double offset = hp.Offset;
            if (hp.RelativeFrom == HorizontalRelativeFrom.Page) return offset;
            return marginLeft + offset; // Margin, Column, LeftMargin, InsideMargin, etc.
        }

        /// <summary>
        /// Resolves a Word floating image's VerticalPosition to a pixel offset measured
        /// from the page's top edge.
        /// </summary>
        private double ResolverPosicionVerticalPx(VerticalPosition vp, Section section, double elementHeight)
        {
            double pageHeight = section.PageSize.Height;
            double marginTop = section.PageMargins.Top;
            double marginBottom = section.PageMargins.Bottom;

            if (vp.ValueType == PositionValueType.Alignment)
            {
                switch (vp.Alignment)
                {
                    case RelativeVerticalAlignment.Center: return (pageHeight - elementHeight) / 2;
                    case RelativeVerticalAlignment.Bottom: return pageHeight - marginBottom - elementHeight;
                    default: return marginTop;
                }
            }

            double offset = vp.Offset;
            if (vp.RelativeFrom == VerticalRelativeFrom.Page) return offset;
            return marginTop + offset; // Margin, TopMargin, etc.
        }

        /// <summary>
        /// Builds the absolutely-positioned overlay (logo + versioning table) that sits
        /// on top of the flowed body content within the '.pagina' container.
        /// </summary>
        private string ConstruirHtmlEncabezadoFlotante(EncabezadoFlotanteInfo info)
        {
            if (!info.TieneLogo && !info.TieneTablaVersion) return "";

            StringBuilder sb = new StringBuilder();
            sb.Append("<div class='encabezado-flotante'>");

            if (info.TieneLogo)
            {
                sb.Append("<img class='logo-institucional' alt='Logo' src='").Append(info.LogoDataUri)
                  .Append("' style='position:absolute; left:").Append(Px(info.LogoLeft))
                  .Append("px; top:").Append(Px(info.LogoTop))
                  .Append("px; width:").Append(Px(info.LogoWidth))
                  .Append("px; height:").Append(Px(info.LogoHeight))
                  .Append("px; max-width:none;' />");
            }

            if (info.TieneTablaVersion)
            {
                sb.Append("<div class='tabla-version-flotante'>").Append(info.TablaVersionHtml).Append("</div>");
            }

            sb.Append("</div>");
            return sb.ToString();
        }

        private static string Px(double value)
        {
            return Math.Round(value, 1).ToString(CultureInfo.InvariantCulture);
        }

        // =============================================================
        // HTML WRAPPER
        // =============================================================
        private string GenerarPaginaHtmlCompleta(string htmlFragment, string encabezadoFlotanteHtml, Section section)
        {
            return GenerarPaginaHtmlCompleta(
                htmlFragment, encabezadoFlotanteHtml,
                section.PageSize.Width, section.PageSize.Height,
                section.PageMargins.Top, section.PageMargins.Right,
                section.PageMargins.Bottom, section.PageMargins.Left);
        }

        /// <summary>
        /// Sobrecarga agnostica al formato: envuelve el fragmento HTML (Word o Excel) en la
        /// pagina imprimible con las dimensiones/margenes de pagina expresadas en pixeles 96dpi.
        /// </summary>
        private string GenerarPaginaHtmlCompleta(string htmlFragment, string encabezadoFlotanteHtml,
            double pageWidth, double pageHeight,
            double marginTop, double marginRight, double marginBottom, double marginLeft)
        {
            return @"<!DOCTYPE html>
<html lang='es'>
<head>
    <meta charset='UTF-8'>
    <meta name='viewport' content='width=device-width, initial-scale=1.0'>
    <title>Solicitud de Bienes y Servicios</title>
    <style>
        @page { size: " + Px(pageWidth) + "px " + Px(pageHeight) + @"px; margin: 0; }
        body { font-family: 'Calibri','Arial',sans-serif; font-size: 11pt; margin: 20px auto; background: #ddd; color: #000; }
        .pagina { position: relative; width: " + Px(pageWidth) + @"px; min-height: " + Px(pageHeight) + @"px;
                  margin: 0 auto; background: #fff; box-shadow: 0 0 8px rgba(0,0,0,.15); box-sizing: border-box; }
        .contenido-flotante { padding: " + Px(marginTop) + "px " + Px(marginRight) + "px " + Px(marginBottom) + "px " + Px(marginLeft) + @"px;
                               box-sizing: border-box; }
        .encabezado-flotante { position: relative; }
        .tabla-version-flotante { position: absolute; top: " + Px(marginTop) + "px; right: " + Px(marginRight) + @"px; }
        table { border-collapse: collapse; page-break-inside: avoid; }
        tr { page-break-inside: avoid; }
        td, th { padding: 3px 5px; font-size: 10pt; vertical-align: top; }
        img { max-width: 100%; }
        @media print {
            body { margin: 0; background: #fff; }
            .pagina { box-shadow: none; margin: 0 auto; }
            .no-print { display: none; }
            table { page-break-inside: avoid; }
            tr { page-break-inside: avoid; }
            .encabezado-flotante { page-break-after: avoid; }
        }
        .btn-print { position: fixed; top: 10px; right: 10px; padding: 8px 20px; background: #275EAD; color: white; border: none; border-radius: 4px; cursor: pointer; font-size: 14px; z-index: 9999; }
        .btn-print:hover { background: #1a4a8a; }
    </style>
</head>
<body>
    <button class='btn-print no-print' onclick='window.print()'>Imprimir / Guardar PDF</button>
    <div class='pagina'>
        " + encabezadoFlotanteHtml + @"
        <div class='contenido-flotante'>
            " + htmlFragment + @"
        </div>
    </div>
</body>
</html>";
        }

        // =============================================================
        // DEFAULT TEMPLATE GENERATION
        // =============================================================
        public void CrearPlantillaDefault(string outputPath, string logoAsambleaPath, string headerPath, string comprasPath)
        {
            RadFlowDocument document = new RadFlowDocument();
            RadFlowDocumentEditor editor = new RadFlowDocumentEditor(document);
            editor.InsertSection();
            var section = document.Sections.First();
            section.PageSize = new System.Windows.Size(612, 1008);
            section.PageMargins = new Padding(30, 30, 30, 30);

            // Header
            Table ht = new Table(document);
            ht.PreferredWidth = new TableWidthUnit(TableWidthUnitType.Percent, 100);
            TableRow hr = ht.Rows.AddTableRow();
            TableCell logoC = hr.Cells.AddTableCell();
            logoC.PreferredWidth = new TableWidthUnit(TableWidthUnitType.Fixed, 80);
            Paragraph logoP = logoC.Blocks.AddParagraph();
            if (File.Exists(logoAsambleaPath))
            {
                using (var s = File.OpenRead(logoAsambleaPath))
                { editor.MoveToParagraphStart(logoP); editor.InsertImageInline(s, "png", new System.Windows.Size(65, 65)); }
            }
            TableCell titC = hr.Cells.AddTableCell();
            titC.PreferredWidth = new TableWidthUnit(TableWidthUnitType.Fixed, 340);
            AddP(titC, "ASAMBLEA NACIONAL", 13, true, Alignment.Center);
            AddP(titC, "DIRECCIÓN GENERAL DE ADMINISTRACIÓN Y FINANZAS", 9, false, Alignment.Center);
            AddP(titC, "DEPARTAMENTO DE COMPRAS Y PROVEEDURÍA", 9, false, Alignment.Center);
            AddP(titC, "SOLICITUD DE BIENES Y SERVICIOS", 10, true, Alignment.Center);
            section.Blocks.Add(ht);

            AddP(section, "Fecha: {{FECHA}}", 10, false, Alignment.Left);
            AddP(section, "Señor (a): {{DIR_ADMIN}}                    solicitud de: {{CHK_BIENES}} BIENES  {{CHK_SERVICIOS}} SERVICIOS", 10, false, Alignment.Left);
            AddP(section, "Funcionario Responsable: {{SOLICITANTE}}                    {{CHK_REEMPLAZO}} REMPLAZO  {{CHK_OBRA}} OBRA", 10, false, Alignment.Left);
            AddP(section, "Unidad Solicitante: {{UNIDAD}}                                     {{CHK_NUEVO}} NUEVO", 10, false, Alignment.Left);
            section.Blocks.AddParagraph();
            AddP(section, "{{TABLA_RENGLONES}}", 1, false, Alignment.Left);
            section.Blocks.AddParagraph();
            AddP(section, "Especifique el uso de los bienes requeridos:", 9, true, Alignment.Left);
            AddP(section, "{{DESCRIPCION_USO}}", 10, false, Alignment.Left);
            section.Blocks.AddParagraph();

            if (File.Exists(comprasPath))
            {
                Paragraph fp = section.Blocks.AddParagraph();
                using (var s = File.OpenRead(comprasPath))
                { editor.MoveToParagraphStart(fp); editor.InsertImageInline(s, "png", new System.Windows.Size(550, 210)); }
            }

            DocxFormatProvider dp = new DocxFormatProvider();
            string d = Path.GetDirectoryName(outputPath);
            if (!Directory.Exists(d)) Directory.CreateDirectory(d);
            using (var o = File.Create(outputPath)) { dp.Export(document, o); }
        }

        private void AddP(BlockContainerBase c, string t, double fs, bool b, Alignment a)
        {
            Paragraph p = c.Blocks.AddParagraph(); p.TextAlignment = a;
            Run r = p.Inlines.AddRun(t); r.FontSize = fs;
            if (b) r.FontWeight = System.Windows.FontWeights.Bold;
        }
    }
}
