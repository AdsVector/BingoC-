using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using iText.Kernel.Colors;
using iText.Layout.Borders;
using iText.Kernel.Events;
using iText.Kernel.Pdf.Canvas;
using iText.Kernel.Geom;
using iText.Kernel.Font;
using iText.IO.Font;

namespace GranBingo_Generator
{
    public class ArchivoPdf
    {
        float headerFont = 20f;
        float textFont = 16f;
        float height = 28f;

        PdfWriter pdf;
        PdfDocument pdfdoc;
        Document doc;

        string pathFile;
        int count = 0;
        int newValue = 482;
        int pagina;

        public ArchivoPdf(string file)
        {
            pathFile = file;

            pdf = new PdfWriter(pathFile);
            pdfdoc = new PdfDocument(pdf);
            doc = new Document(pdfdoc);
        }

        public void NuevaPagina()
        {                 
            pagina++;
            count = 0;
            newValue = 482;
            pdfdoc.AddNewPage();
        }

        public void CrearEncabezado(Header header)
        {
            Table table = new Table(2);                 
            table.SetFixedPosition(pagina, 36, 700, 522);
            table.SetHorizontalAlignment(HorizontalAlignment.CENTER);

            Paragraph pTitle = new Paragraph(header.Title).SetFontSize(16f).SetBold().SetTextAlignment(TextAlignment.CENTER);
            Paragraph pOrg = new Paragraph(header.Org).SetTextAlignment(TextAlignment.CENTER).SetFontSize(8f);

            Paragraph pCostTxt = new Paragraph("VALOR:").SetFontSize(8f).SetTextAlignment(TextAlignment.CENTER).SetBackgroundColor(new DeviceRgb(0, 0, 0)).SetFontColor(new DeviceRgb(255, 255, 255));
            Paragraph pCost = new Paragraph(header.Cost()).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetTextAlignment(TextAlignment.CENTER).SetFontSize(14f);

            Cell cellTO = new Cell(1, 2).Add(pTitle).Add(pOrg);
            Cell cellDes = new Cell(1, 2).Add(new Paragraph(header.Descrip).SetFontSize(10f));            
            Cell cellAdress = new Cell().Add(new Paragraph("DIRECCIÓN: " + header.Adress).SetFontSize(10f));
            Cell cellCost = new Cell(2, 1).Add(pCostTxt).Add(pCost);
            Cell cellDateTime = new Cell().Add(new Paragraph("FECHA: " + header.DateHour()).SetFontSize(10f));

            table.AddCell(cellTO);
            table.AddCell(cellDes);
            table.AddCell(cellAdress);
            table.AddCell(cellCost);
            table.AddCell(cellDateTime);

            doc.Add(table);
        }

        public void CrearTabla(int[] numeros)
        {
            Table table = new Table(5);
            table.SetFixedPosition(pagina, 36, newValue, 230);

            if ((count % 2) == 1)
            {
                table.SetFixedPosition(pagina, 329, newValue, 230);
                newValue -= 210;
            }

            table.AddCell(NewCell("B", style.Header));
            table.AddCell(NewCell("I", style.Header));
            table.AddCell(NewCell("N", style.Header));
            table.AddCell(NewCell("G", style.Header));
            table.AddCell(NewCell("O", style.Header));

            for (int i = 0; i < numeros.Length; i++)
            {                
                if (numeros[i] == 0)
                    table.AddCell(NewCell("  "));
                else if (i.Equals(12))
                    table.AddCell(NewCell("AV", style.Sello));
                else
                    table.AddCell(NewCell(numeros[i].ToString()));
            }

            count++;
            doc.Add(table);
        }

        public enum style { Header, Texto, Sello}

        private Cell NewCell(string text, style estilo = style.Texto)
        {
            Paragraph paragraph = new Paragraph(text).SetFontSize(textFont).SetVerticalAlignment(VerticalAlignment.MIDDLE)
                .SetTextAlignment(TextAlignment.CENTER).SetMinHeight(height);

            switch (estilo)
            {
                case style.Header:
                    paragraph.SetFontSize(headerFont).SetFontColor(ColorConstants.WHITE)
                        .SetBackgroundColor(ColorConstants.BLACK).SetBorder(Border.NO_BORDER);
                    break;
                case style.Texto:
                    
                    break;
                case style.Sello:
                    paragraph.SetFontSize(textFont).SetFontColor(ColorConstants.WHITE)
                        .SetBackgroundColor(ColorConstants.BLACK).SetBorder(Border.NO_BORDER);
                    break;
            }

            return new Cell().Add(paragraph);
        }

        public void Close()
        {
            doc.Close();
        }
    }
}
