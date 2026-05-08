namespace sitemaCompras.Controlers
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.IO;
    using System.Linq;
    using Telerik.Windows.Documents.Flow.FormatProviders.Docx;
    using Telerik.Windows.Documents.Flow.FormatProviders.Html;
    using Telerik.Windows.Documents.Flow.Model;
    using Telerik.Windows.Documents.Flow.Model.Editing;
    using Telerik.Windows.Documents.Flow.Model.Styles;
    using Telerik.Windows.Documents.Primitives;

    public class ReporteComprasDocx
    {
        public void GenerarDocumentoDesdePlantilla(DataSet ds, string templatePath, string outputHtmlPath)
        {
            DocxFormatProvider docxProvider = new DocxFormatProvider();
            RadFlowDocument document;
            using (FileStream input = File.OpenRead(templatePath))
            {
                document = docxProvider.Import(input);
            }

            // 1. Build replacements dictionary
            Dictionary<string, string> reemplazos = new Dictionary<string, string>();
            if (ds.Tables.Count > 1 && ds.Tables[1].Rows.Count > 0)
            {
                DataRow enc = ds.Tables[1].Rows[0];
                reemplazos["{{FECHA}}"] = Convert.ToDateTime(enc["fechas"]).ToString("dd-MM-yyyy");
                reemplazos["{{DIR_ADMIN}}"] = Convert.ToString(enc["diradmin"]).ToUpper();
                reemplazos["{{SOLICITANTE}}"] = Convert.ToString(enc["iddirectores"]).ToUpper();
                reemplazos["{{UNIDAD}}"] = Convert.ToString(enc["Unidad"]).ToUpper();
            }

            // 2. Checkboxes
            reemplazos["{{CHK_BIENES}}"] = "\u2B1C"; reemplazos["{{CHK_SERVICIOS}}"] = "\u2B1C";
            reemplazos["{{CHK_REEMPLAZO}}"] = "\u2B1C"; reemplazos["{{CHK_OBRA}}"] = "\u2B1C";
            reemplazos["{{CHK_NUEVO}}"] = "\u2B1C";
            if (ds.Tables.Count > 2)
            {
                foreach (DataRow dr in ds.Tables[2].Rows)
                {
                    int tipo = Convert.ToInt32(dr["idTipo"]);
                    switch (tipo)
                    {
                        case 1: reemplazos["{{CHK_BIENES}}"] = "\u25A3"; break;
                        case 2: reemplazos["{{CHK_SERVICIOS}}"] = "\u25A3"; break;
                        case 3: reemplazos["{{CHK_REEMPLAZO}}"] = "\u25A3"; break;
                        case 4: reemplazos["{{CHK_OBRA}}"] = "\u25A3"; break;
                        case 5: reemplazos["{{CHK_NUEVO}}"] = "\u25A3"; break;
                    }
                }
            }

            // 3. Replace text markers (excluding DESCRIPCION_USO and TABLA_RENGLONES)
            ReemplazarMarcadoresEnDocumento(document, reemplazos);

            // 4. Handle DESCRIPCION_USO with line breaks
            string descripcion = "";
            if (ds.Tables.Count > 1 && ds.Tables[1].Rows.Count > 0)
                descripcion = Convert.ToString(ds.Tables[1].Rows[0]["descripcion"]);
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
            string htmlCompleto = GenerarPaginaHtmlCompleta(htmlFragment);
            string dir = Path.GetDirectoryName(outputHtmlPath);
            if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);
            File.WriteAllText(outputHtmlPath, htmlCompleto);
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

        private void ReemplazarEnParrafo(Paragraph paragraph, Dictionary<string, string> reemplazos)
        {
            var runs = paragraph.Inlines.OfType<Run>().ToList();
            if (runs.Count == 0) return;
            string texto = string.Join("", runs.Select(r => r.Text));
            bool changed = false;
            foreach (var kvp in reemplazos)
            {
                if (texto.Contains(kvp.Key)) { texto = texto.Replace(kvp.Key, kvp.Value); changed = true; }
            }
            if (changed)
            {
                for (int i = runs.Count - 1; i > 0; i--) paragraph.Inlines.Remove(runs[i]);
                runs[0].Text = texto;
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
            double sharedFontSize = 10;
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
                    double fontSize = 10;
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
                    run.FontSize = 10;
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
                Run r = p.Inlines.AddRun(h); r.FontSize = 10; r.FontWeight = System.Windows.FontWeights.Bold;
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
                    r.FontSize = 10;
                }
            }
            return table;
        }

        // =============================================================
        // HTML WRAPPER
        // =============================================================
        private string GenerarPaginaHtmlCompleta(string htmlFragment)
        {
            return @"<!DOCTYPE html>
<html lang='es'>
<head>
    <meta charset='UTF-8'>
    <meta name='viewport' content='width=device-width, initial-scale=1.0'>
    <title>Solicitud de Bienes y Servicios</title>
    <style>
        @page { size: legal portrait; margin: 0.8cm; }
        body { font-family: 'Calibri','Arial',sans-serif; font-size: 11pt; margin: 10px auto; max-width: 1000px; background: #fff; color: #000; }
        table { border-collapse: collapse; }
        td, th { padding: 3px 5px; font-size: 10pt; vertical-align: top; }
        img { max-width: 100%; }
        @media print { body { margin: 0; max-width: none; } .no-print { display: none; } }
        .btn-print { position: fixed; top: 10px; right: 10px; padding: 8px 20px; background: #275EAD; color: white; border: none; border-radius: 4px; cursor: pointer; font-size: 14px; z-index: 9999; }
        .btn-print:hover { background: #1a4a8a; }
    </style>
</head>
<body>
    <button class='btn-print no-print' onclick='window.print()'>Imprimir / Guardar PDF</button>
    " + htmlFragment + @"
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
