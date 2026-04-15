namespace sitemaCompras.Controlers
{
    using System;
    using System.Data;
    using System.IO;
    using System.Web;
    using Telerik.Windows.Documents.Media;
    using Telerik.Windows.Documents.Model;
    using Telerik.Windows.Documents.Spreadsheet.Model;
    using Telerik.Windows.Documents.Spreadsheet.Model.Printing;
    using Telerik.Windows.Documents.Spreadsheet.Model.Shapes;

    public partial class ReporteCompras
    {
    	private int hlen = 37;

    	public Workbook createexcellficha(DataSet ds, string DirAdmin, string Unidad, string Solicitante)
    	{
    		Workbook workbook = new Workbook();
    		PageMargins normalMargins = PageMargins.NarrowMargins;
    		workbook.Sheets.Add(SheetType.Worksheet);
    		Worksheet worksheet = workbook.ActiveWorksheet;
    		PrepareGeneralHeaderFicha(worksheet, DirAdmin, Unidad, Solicitante, ds, Convert.ToDateTime(ds.Tables["Table1"].Rows[0]["fechas"]).ToString("dd-MM-yyyy"));
    		PrepareHeaderFooterFicha(worksheet, 12);
    		WorksheetPageSetup pageSetup = workbook.ActiveWorksheet.WorksheetPageSetup;
    		pageSetup.PaperType = PaperTypes.Legal;
    		pageSetup.PageOrientation = PageOrientation.Portrait;
    		pageSetup.Margins = PageMargins.NarrowMargins;
    		pageSetup.PrintTitles.RepeatedRows = new RowRange(0, 12);
    		pageSetup.PrintTitles.RepeatedColumns = new ColumnRange(0, 5);
    		llenarSB(ds, worksheet);
    		return workbook;
    	}

    	private void llenarSB(DataSet ds, Worksheet worksheet)
    	{
    		int j = 13;
    		int len = 65;
    		foreach (DataRow dr in ds.Tables["Table"].Rows)
    		{
    			string str = dr[3].ToString();
    			string strPT = dr[5].ToString();
    			string strPU = dr[4].ToString();
    			string strUnidad = dr[2].ToString();
    			string strCantidad = dr[1].ToString();
    			string strRenglon = dr[0].ToString();
    			worksheet.Cells[j, 0].SetFontSize(12.0);
    			worksheet.Cells[j, 1].SetFontSize(12.0);
    			worksheet.Cells[j, 2].SetFontSize(12.0);
    			worksheet.Cells[j, 3].SetFontSize(13.0);
    			worksheet.Cells[j, 4].SetFontSize(12.0);
    			worksheet.Cells[j, 5].SetFontSize(12.0);
    			worksheet.Cells[j, 0].SetValue(" " + strRenglon);
    			worksheet.Cells[j, 1].SetValue(" " + strCantidad);
    			worksheet.Cells[j, 2].SetValue(" " + strUnidad);
    			worksheet.Cells[j, 4].SetValue(" " + strPU);
    			worksheet.Cells[j, 5].SetValue(" " + strPT);
    			if (str.Length > 65)
    			{
    				int cant = str.Length / 65;
    				if (str.Length % 65 > 0)
    				{
    					cant++;
    				}
    				string footer = (string)ds.Tables["Table1"].Rows[0]["descripcion"];
    				j = Recorrer_STR_desc(str, footer, 65, worksheet, j);
    			}
    			else
    			{
    				worksheet.Cells[j, 0].SetFontSize(12.0);
    				worksheet.Cells[j, 1].SetFontSize(12.0);
    				worksheet.Cells[j, 2].SetFontSize(12.0);
    				worksheet.Cells[j, 3].SetFontSize(13.0);
    				worksheet.Cells[j, 4].SetFontSize(12.0);
    				worksheet.Cells[j, 5].SetFontSize(12.0);
    				worksheet.Cells[j, 3].SetValue(" " + str);
    				j++;
    			}
    			string descripcion = (string)ds.Tables["Table1"].Rows[0]["descripcion"];
    			if (j > hlen)
    			{
    				worksheet.Cells[j, 3].SetFontSize(13.0);
    				worksheet.Cells[j, 3].SetValue("****************Continua*********************");
    				j++;
    				preparefooter(j, worksheet, descripcion);
    				hlen += 46;
    				PageBreaks pageBreaks = worksheet.WorksheetPageSetup.PageBreaks;
    				j += 20;
    				pageBreaks.TryInsertHorizontalPageBreak(j, 0);
    				PrepareHeaderFooterFicha(worksheet, j - 1);
    			}
    		}
    		preparefooter(hlen + 2, worksheet, (string)ds.Tables["Table1"].Rows[0]["descripcion"]);
    	}

    	private void PrepareGeneralHeaderFicha(Worksheet worksheet, string DirAdmin, string Solicitante, string Unidad, DataSet ds, string FSB)
    	{
    		CellIndex fRowFirstCellIndex = new CellIndex(0, 0);
    		CellIndex sRowFirstCellIndex = new CellIndex(1, 0);
    		CellIndex tRowFirstCellIndex = new CellIndex(2, 0);
    		CellIndex cRowFirstCellIndex = new CellIndex(3, 0);
    		CellIndex quRowFirstCellIndex = new CellIndex(5, 1);
    		CellIndex sxRowFirstCellIndex = new CellIndex(6, 0);
    		CellIndex spRowFirstCellIndex = new CellIndex(7, 0);
    		CellIndex octRowFirstCellIndex = new CellIndex(8, 0);
    		CellIndex fRowlastCellIndex = new CellIndex(0, 5);
    		CellIndex sRowlastCellIndex = new CellIndex(1, 5);
    		CellIndex tRowlasCellIndex = new CellIndex(2, 5);
    		CellIndex cRowlastCellIndex = new CellIndex(3, 5);
    		CellIndex quRowlastCellIndex = new CellIndex(5, 5);
    		CellIndex sxRowlasCellIndex = new CellIndex(6, 5);
    		CellIndex spRowlasCellIndex = new CellIndex(7, 5);
    		CellIndex octRowlasCellIndex = new CellIndex(8, 5);
    		CellIndex ccRowFirstCellIndex = new CellIndex(4, 0);
    		CellIndex ccRowlasCellIndex = new CellIndex(4, 2);
    		ThemableColor InvoiceHeaderBorderColor = ThemableColor.FromArgb(byte.MaxValue, 90, 90, 90);
    		CellBorder border = new CellBorder(CellBorderStyle.Medium, InvoiceHeaderBorderColor);
    		FloatingImage image = new FloatingImage(worksheet, new CellIndex(0, 0), 2.0, 2.0);
    		Stream stream = File.Open(HttpContext.Current.Server.MapPath("~/userscontent/img/logoasamblea.png"), FileMode.Open);
    		using (stream)
    		{
    			image.ImageSource = new ImageSource(stream, "png");
    		}
    		image.Width = 70.0;
    		image.Height = 70.0;
    		worksheet.Images.Add(image);
    		FloatingImage image2 = new FloatingImage(worksheet, new CellIndex(0, 4), 0.0, 0.0);
    		Stream stream2 = File.Open(HttpContext.Current.Server.MapPath("~/userscontent/img/header.png"), FileMode.Open);
    		using (stream2)
    		{
    			image2.ImageSource = new ImageSource(stream2, "png");
    		}
    		image2.Width = 123.0;
    		image2.Height = 41.0;
    		worksheet.Images.Add(image2);
    		worksheet.Cells[fRowFirstCellIndex, fRowlastCellIndex].MergeAcross();
    		worksheet.Cells[fRowFirstCellIndex].SetValue("ASAMBLEA NACIONAL");
    		worksheet.Cells[fRowFirstCellIndex].SetFontSize(16.0);
    		worksheet.Cells[fRowFirstCellIndex].SetHorizontalAlignment(RadHorizontalAlignment.Center);
    		worksheet.Cells[sRowFirstCellIndex, sRowlastCellIndex].MergeAcross();
    		worksheet.Cells[sRowFirstCellIndex].SetValue("DIRECCIÓN GENERAL DE ADMINISTRACIÓN Y FINANZAS");
    		worksheet.Cells[sRowFirstCellIndex].SetFontSize(16.0);
    		worksheet.Cells[sRowFirstCellIndex].SetHorizontalAlignment(RadHorizontalAlignment.Center);
    		worksheet.Cells[tRowFirstCellIndex, tRowlasCellIndex].MergeAcross();
    		worksheet.Cells[tRowFirstCellIndex].SetValue("DEPARTAMENTO DE COMPRAS Y PROVEEDURÍA");
    		worksheet.Cells[tRowFirstCellIndex].SetFontSize(16.0);
    		worksheet.Cells[tRowFirstCellIndex].SetHorizontalAlignment(RadHorizontalAlignment.Center);
    		worksheet.Cells[cRowFirstCellIndex, cRowlastCellIndex].MergeAcross();
    		worksheet.Cells[cRowFirstCellIndex].SetValue("SOLICITUD  DE   BIENES  Y   SERVICIOS");
    		worksheet.Cells[cRowFirstCellIndex].SetFontSize(16.0);
    		worksheet.Cells[cRowFirstCellIndex].SetHorizontalAlignment(RadHorizontalAlignment.Center);
    		worksheet.Cells[ccRowFirstCellIndex, ccRowlasCellIndex].MergeAcross();
    		worksheet.Cells[ccRowFirstCellIndex].SetValue("Día  Mes  Año");
    		worksheet.Cells[ccRowFirstCellIndex].SetFontSize(13.0);
    		worksheet.Cells[ccRowFirstCellIndex].SetHorizontalAlignment(RadHorizontalAlignment.Center);
    		worksheet.Cells[quRowFirstCellIndex, quRowlastCellIndex].MergeAcross();
    		worksheet.Cells[quRowFirstCellIndex].SetValue(FSB);
    		worksheet.Cells[quRowFirstCellIndex].SetFontSize(12.0);
    		worksheet.Cells[quRowFirstCellIndex].SetHorizontalAlignment(RadHorizontalAlignment.Left);
    		string strtipo = "Solicitud de     ⬜ Bienes          ⬜ Servicios";
    		string strtipo2 = " ⬜ Reemplazo   ⬜ Obra ";
    		string strtipo3 = "         ⬜ Nuevo            ";
    		DirAdmin = DirAdmin.PadRight(100);
    		worksheet.Cells[sxRowFirstCellIndex].SetValue("Señor (a): " + DirAdmin + strtipo);
    		worksheet.Cells[spRowFirstCellIndex].SetValue("Funcionario Responsable: " + Solicitante.PadRight(95) + strtipo2);
    		worksheet.Cells[octRowFirstCellIndex].SetValue("Unidad Solicitante: " + Unidad.PadRight(95) + strtipo3);
    		foreach (DataRow dr in ds.Tables["Table2"].Rows)
    		{
    			int tipo = (int)dr["idTipo"];
    			if (tipo == 1 || tipo == 2)
    			{
    				if (tipo == 1)
    				{
    					strtipo = strtipo.Replace("⬜ Bienes", "▣ Bienes");
    				}
    				if (tipo == 2)
    				{
    					strtipo = strtipo.Replace("⬜ Servicios", "▣ Servicios");
    				}
    				worksheet.Cells[sxRowFirstCellIndex].SetValue("Señor (a): " + DirAdmin + strtipo);
    			}
    			if (tipo == 3 || tipo == 4)
    			{
    				if (tipo == 3)
    				{
    					strtipo2 = strtipo2.Replace("⬜ Reemplazo", "▣ Reemplazo");
    				}
    				if (tipo == 4)
    				{
    					strtipo2 = strtipo2.Replace("⬜ Obra", "▣ Obra");
    				}
    				worksheet.Cells[spRowFirstCellIndex].SetValue("Funcionario Responsable: " + Solicitante.PadRight(95) + strtipo2);
    			}
    			if (tipo == 5)
    			{
    				if (tipo == 5)
    				{
    					strtipo3 = strtipo3.Replace("⬜ Nuevo", "▣ Nuevo");
    				}
    				worksheet.Cells[octRowFirstCellIndex, octRowlasCellIndex].MergeAcross();
    				worksheet.Cells[octRowFirstCellIndex].SetValue("Unidad Solicitante: " + Unidad.PadRight(95) + strtipo3);
    			}
    		}
    		int j = 11;
    		worksheet.Cells[j, 0].SetValue("RENGLÓN");
    		worksheet.Cells[new CellIndex(j, 0), new CellIndex(j + 1, 0)].Merge();
    		worksheet.Cells[j, 1].SetValue("CANTIDAD");
    		worksheet.Cells[new CellIndex(j, 1), new CellIndex(j + 1, 1)].Merge();
    		worksheet.Cells[j, 2].SetValue("UNIDAD");
    		worksheet.Cells[new CellIndex(j, 2), new CellIndex(j + 1, 2)].Merge();
    		worksheet.Cells[j, 3].SetValue("DESCRIPCIÓN");
    		worksheet.Cells[new CellIndex(j, 3), new CellIndex(j + 1, 3)].Merge();
    		worksheet.Cells[j, 4].SetValue("PRECIOS ESTIMADOS");
    		worksheet.Cells[new CellIndex(j, 4), new CellIndex(j, 5)].MergeAcross();
    		worksheet.Cells[j + 1, 4].SetValue("UNITARIO");
    		worksheet.Cells[j + 1, 5].SetValue("TOTAL");
    		worksheet.Cells[j, 0].SetFontSize(12.0);
    		worksheet.Cells[j, 1].SetFontSize(12.0);
    		worksheet.Cells[j, 2].SetFontSize(12.0);
    		worksheet.Cells[j, 3].SetFontSize(12.0);
    		worksheet.Cells[j, 4].SetFontSize(12.0);
    		worksheet.Cells[j, 5].SetFontSize(12.0);
    		worksheet.Cells[j + 1, 4].SetFontSize(12.0);
    		worksheet.Cells[j + 1, 5].SetFontSize(12.0);
    		worksheet.Cells[j, 0].SetIsBold(value: true);
    		worksheet.Cells[j, 1].SetIsBold(value: true);
    		worksheet.Cells[j, 2].SetIsBold(value: true);
    		worksheet.Cells[j, 3].SetIsBold(value: true);
    		worksheet.Cells[j, 4].SetIsBold(value: true);
    		worksheet.Cells[j, 5].SetIsBold(value: true);
    		worksheet.Cells[j + 1, 4].SetIsBold(value: true);
    		worksheet.Cells[j + 1, 5].SetIsBold(value: true);
    		worksheet.Cells[j, 0].SetHorizontalAlignment(RadHorizontalAlignment.Center);
    		worksheet.Cells[j, 1].SetHorizontalAlignment(RadHorizontalAlignment.Center);
    		worksheet.Cells[j, 2].SetHorizontalAlignment(RadHorizontalAlignment.Center);
    		worksheet.Cells[j, 3].SetHorizontalAlignment(RadHorizontalAlignment.Center);
    		worksheet.Cells[j, 4].SetHorizontalAlignment(RadHorizontalAlignment.Center);
    		worksheet.Cells[j, 5].SetHorizontalAlignment(RadHorizontalAlignment.Center);
    		worksheet.Cells[j + 1, 4].SetHorizontalAlignment(RadHorizontalAlignment.Center);
    		worksheet.Cells[j + 1, 5].SetHorizontalAlignment(RadHorizontalAlignment.Center);
    		worksheet.Cells[j, 0].SetVerticalAlignment(RadVerticalAlignment.Center);
    		worksheet.Cells[j, 1].SetVerticalAlignment(RadVerticalAlignment.Center);
    		worksheet.Cells[j, 2].SetVerticalAlignment(RadVerticalAlignment.Center);
    		worksheet.Cells[j, 3].SetVerticalAlignment(RadVerticalAlignment.Center);
    		worksheet.Cells[j, 0].SetBorders(new CellBorders(border, border, border, border, null, null, null, null));
    		worksheet.Cells[j, 1].SetBorders(new CellBorders(border, border, border, border, null, null, null, null));
    		worksheet.Cells[j, 2].SetBorders(new CellBorders(border, border, border, border, null, null, null, null));
    		worksheet.Cells[j, 3].SetBorders(new CellBorders(border, border, border, border, null, null, null, null));
    		worksheet.Cells[j, 4].SetBorders(new CellBorders(border, border, border, border, null, null, null, null));
    		worksheet.Cells[j, 5].SetBorders(new CellBorders(border, border, border, border, null, null, null, null));
    		worksheet.Cells[j + 1, 0].SetBorders(new CellBorders(border, border, border, border, null, null, null, null));
    		worksheet.Cells[j + 1, 1].SetBorders(new CellBorders(border, border, border, border, null, null, null, null));
    		worksheet.Cells[j + 1, 2].SetBorders(new CellBorders(border, border, border, border, null, null, null, null));
    		worksheet.Cells[j + 1, 3].SetBorders(new CellBorders(border, border, border, border, null, null, null, null));
    		worksheet.Cells[j + 1, 4].SetBorders(new CellBorders(border, border, border, border, null, null, null, null));
    		worksheet.Cells[j + 1, 5].SetBorders(new CellBorders(border, border, border, border, null, null, null, null));
    		worksheet.Columns[3].SetWidth(new ColumnWidth(378.0, isCustom: true));
    		worksheet.Columns[3].SetIsWrapped(value: true);
    	}

    	private void preparefooter(int j, Worksheet worksheet, string descripcion)
    	{
    		ThemableColor InvoiceHeaderBorderColor = ThemableColor.FromArgb(byte.MaxValue, 90, 90, 90);
    		CellBorder border = new CellBorder(CellBorderStyle.Medium, InvoiceHeaderBorderColor);
    		descripcion = "Especifique el uso de los bienes o servicios requeridos: " + descripcion;
    		worksheet.Cells[j, 0].SetBorders(new CellBorders(null, border, null, border, null, null, null, null));
    		worksheet.Cells[j + 1, 0].SetBorders(new CellBorders(null, border, null, border, null, null, null, null));
    		worksheet.Cells[j + 2, 0].SetBorders(new CellBorders(null, border, null, border, null, null, null, null));
    		worksheet.Cells[j + 3, 0].SetBorders(new CellBorders(null, border, null, border, null, null, null, null));
    		worksheet.Cells[j + 4, 0].SetBorders(new CellBorders(null, border, null, border, null, null, null, null));
    		worksheet.Cells[j + 5, 0].SetBorders(new CellBorders(null, null, null, null, null, null, null, null));
    		worksheet.Cells[new CellIndex(j, 0), new CellIndex(j + 5, 5)].MergeAcross();
    		Recorrer_STR(descripcion, 110, worksheet, j);
    		FloatingImage image1 = new FloatingImage(worksheet, new CellIndex(j + 6, 0), 0.0, 0.0);
    		Stream stream1 = File.Open(HttpContext.Current.Server.MapPath("~/userscontent/img/compras.png"), FileMode.Open);
    		using (stream1)
    		{
    			image1.ImageSource = new ImageSource(stream1, "png");
    		}
    		image1.Width = 713.0;
    		image1.Height = 275.0;
    		worksheet.Images.Add(image1);
    	}

    	private void PrepareHeaderFooterFicha(Worksheet worksheet, int j)
    	{
    		ThemableColor InvoiceHeaderBorderColor = ThemableColor.FromArgb(byte.MaxValue, 90, 90, 90);
    		CellBorder border = new CellBorder(CellBorderStyle.Medium, InvoiceHeaderBorderColor);
    		for (int c = 0; c < 26; c++)
    		{
    			j++;
    			worksheet.Cells[j, 0].SetFontSize(10.0);
    			worksheet.Cells[j, 1].SetFontSize(10.0);
    			worksheet.Cells[j, 2].SetFontSize(10.0);
    			worksheet.Cells[j, 3].SetFontSize(10.0);
    			worksheet.Cells[j, 4].SetFontSize(10.0);
    			worksheet.Cells[j, 5].SetFontSize(10.0);
    			worksheet.Cells[j, 0].SetHorizontalAlignment(RadHorizontalAlignment.Center);
    			worksheet.Cells[j, 0].SetVerticalAlignment(RadVerticalAlignment.Top);
    			worksheet.Cells[j, 1].SetHorizontalAlignment(RadHorizontalAlignment.Center);
    			worksheet.Cells[j, 1].SetVerticalAlignment(RadVerticalAlignment.Top);
    			worksheet.Cells[j, 2].SetHorizontalAlignment(RadHorizontalAlignment.Center);
    			worksheet.Cells[j, 2].SetVerticalAlignment(RadVerticalAlignment.Top);
    			worksheet.Cells[j, 3].SetHorizontalAlignment(RadHorizontalAlignment.Left);
    			worksheet.Cells[j, 3].SetVerticalAlignment(RadVerticalAlignment.Top);
    			worksheet.Cells[j, 4].SetHorizontalAlignment(RadHorizontalAlignment.Center);
    			worksheet.Cells[j, 4].SetVerticalAlignment(RadVerticalAlignment.Top);
    			worksheet.Cells[j, 5].SetHorizontalAlignment(RadHorizontalAlignment.Center);
    			worksheet.Cells[j, 5].SetVerticalAlignment(RadVerticalAlignment.Top);
    			worksheet.Cells[j, 0].SetBorders(new CellBorders(border, border, border, border, null, null, null, null));
    			worksheet.Cells[j, 1].SetBorders(new CellBorders(border, border, border, border, null, null, null, null));
    			worksheet.Cells[j, 2].SetBorders(new CellBorders(border, border, border, border, null, null, null, null));
    			worksheet.Cells[j, 3].SetBorders(new CellBorders(border, border, border, border, null, null, null, null));
    			worksheet.Cells[j, 4].SetBorders(new CellBorders(border, border, border, border, null, null, null, null));
    			worksheet.Cells[j, 5].SetBorders(new CellBorders(border, border, border, border, null, null, null, null));
    		}
    	}

    	private int Recorrer_STR_desc(string descripcion, string desF, int leng, Worksheet worksheet, int k)
    	{
    		int inicio = 0;
    		int len = leng;
    		descripcion += " ";
    		descripcion = descripcion.Replace("de 202", "del 202");
    		if (descripcion.Length > leng)
    		{
    			while (len > 0)
    			{
    				len = Buscar_espacio(descripcion.Substring(inicio, len));
    				worksheet.Cells[k, 3].SetFontSize(13.0);
    				worksheet.Cells[k, 3].SetValue(descripcion.Substring(inicio, len));
    				int tinicio = inicio;
    				inicio = inicio + len + 1;
    				len = ((descripcion.Length - (tinicio + len) >= leng) ? (leng - 1) : (descripcion.Length - (tinicio + len + 1)));
    				k++;
    				if (k > hlen)
    				{
    					worksheet.Cells[k, 3].SetFontSize(13.0);
    					worksheet.Cells[k, 3].SetValue("****************Continua*********************");
    					k++;
    					preparefooter(k, worksheet, desF);
    					hlen += 46;
    					PageBreaks pageBreaks = worksheet.WorksheetPageSetup.PageBreaks;
    					k += 20;
    					pageBreaks.TryInsertHorizontalPageBreak(k, 0);
    					PrepareHeaderFooterFicha(worksheet, k - 1);
    				}
    			}
    		}
    		else
    		{
    			worksheet.Cells[k, 3].SetValue(" " + descripcion);
    		}
    		return k;
    	}

    	private int Recorrer_STR(string descripcion, int leng, Worksheet worksheet, int k)
    	{
    		int inicio = 0;
    		int len = leng;
    		descripcion += " ";
    		descripcion = descripcion.Replace("de 202", "del 202");
    		if (descripcion.Length > leng)
    		{
    			while (len > 0)
    			{
    				len = Buscar_espacio(descripcion.Substring(inicio, len));
    				worksheet.Cells[k, 0].SetValue(descripcion.Substring(inicio, len));
    				int tinicio = inicio;
    				inicio = inicio + len + 1;
    				len = ((descripcion.Length - (tinicio + len) >= leng) ? leng : (descripcion.Length - (tinicio + len + 1)));
    				k++;
    			}
    		}
    		else
    		{
    			worksheet.Cells[k, 0].SetValue(" " + descripcion);
    		}
    		return k;
    	}

    	private int Buscar_espacio(string str)
    	{
    		if (str.Substring(str.Length - 1, 1) == " ")
    		{
    			return str.Length - 1;
    		}
    		bool space = true;
    		int i = 1;
    		while (space)
    		{
    			i++;
    			if (str.Length <= i)
    			{
    				return str.Length - 1;
    			}
    			if (str.Substring(str.Length - i, 1) == " ")
    			{
    				return str.Length - i;
    			}
    		}
    		return str.Length - 1;
    	}
    }

}

