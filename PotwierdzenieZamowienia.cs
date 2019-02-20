using AppKit;
using System;
using System.IO;
using System.Text;
using System.Linq;
using System.Reflection;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Collections;
using System.Collections.Generic;

using System;
using System.Linq;

using AppKit;
using Foundation;
namespace Oferta__
{
    public class PotwierdzenieZamowienia
    {
        public PotwierdzenieZamowienia()
        {
        }

        public static void Create()
        {
            FileStream fs = new FileStream("dokument.pdf", FileMode.Create, FileAccess.Write, FileShare.None);

            Document doc = new Document(PageSize.A4);
            doc.SetMargins(24.65f, 23.8f, doc.TopMargin, doc.BottomMargin);

            string path = Environment.CurrentDirectory;

            string nazwa1 = "Auftragsbestätigung " + MainClass.Oferta[1].Replace("/", "") + "_" + MainClass.Halledaten[1] + "x" + MainClass.Halledaten[2] + "m_" + MainClass.Halledaten[0] + "_" + MainClass.Personaldaten[7] + " " + MainClass.Personaldaten[0] + " " + MainClass.Personaldaten[1];
            //string nazwa1 = "test";
            PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location.Replace("Oferta+.app/Contents/MonoBundle", nazwa1 + ".pdf")), FileMode.Create));

            doc.Open();



            //string[] Tabela = AllManager.CreateReadyElement(bazaTabela1, bazaTabela1_x, bazaTabela1_y);
            //string[] Tabela2 = AllManager.CreateReadyElement(bazaTabela2, bazaTabela2_x, bazaTabela2_y);

            PdfPTable table;
            PdfPCell cell;
            Paragraph par;
            Phrase phrase;

            BaseFont impact = BaseFont.CreateFont(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location.Replace("Oferta+.app/Contents/MonoBundle", "Fonts/impact.ttf")), BaseFont.CP1252, BaseFont.EMBEDDED);
            BaseFont arial = BaseFont.CreateFont(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location.Replace("Oferta+.app/Contents/MonoBundle", "Fonts/Arial.ttf")), BaseFont.CP1252, BaseFont.EMBEDDED);
            BaseFont arialbd = BaseFont.CreateFont(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location.Replace("Oferta+.app/Contents/MonoBundle", "Fonts/ARIALBD.TTF")), BaseFont.CP1252, BaseFont.EMBEDDED);
            BaseFont verdana = BaseFont.CreateFont(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location.Replace("Oferta+.app/Contents/MonoBundle", "Fonts/verdana.TTF")), BaseFont.CP1252, BaseFont.EMBEDDED);
            BaseFont simsun = BaseFont.CreateFont(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location.Replace("Oferta+.app/Contents/MonoBundle", "Fonts/SIMSUN.ttf")), BaseFont.IDENTITY_H, BaseFont.EMBEDDED);

            Font logo1 = new Font(impact, 27f, Font.NORMAL, new BaseColor(102, 102, 102));
            Font logo2 = new Font(impact, 27f, Font.NORMAL, new BaseColor(0, 204, 204));
            Font logo1_small = new Font(impact, 10f, Font.NORMAL, new BaseColor(102, 102, 102));
            Font logo2_small = new Font(impact, 10f, Font.NORMAL, new BaseColor(0, 204, 204));

            Font title = new Font(arialbd, 12f, Font.NORMAL, BaseColor.BLACK);

            Font standard = new Font(arial, 9f, Font.NORMAL, BaseColor.BLACK);
            Font standard_bold = new Font(arialbd, 9f, Font.NORMAL, BaseColor.BLACK);
            Font standard_blue = new Font(arialbd, 9f, Font.NORMAL, new BaseColor(0, 69, 134));
            Font standard_lightblue = new Font(arial, 9f, Font.NORMAL, new BaseColor(0, 69, 134));

            Font chinese = new Font(simsun, 9f, Font.NORMAL, BaseColor.BLACK);

            Font small = new Font(verdana, 7f, Font.NORMAL, BaseColor.BLACK);

            Font medium = new Font(arial, 8f, Font.NORMAL, BaseColor.BLACK);
            Font medium_bold = new Font(arialbd, 8f, Font.NORMAL, BaseColor.BLACK);
            Font medium_green = new Font(arial, 8f, Font.NORMAL, new BaseColor(0, 120, 38));
            Font medium_blue = new Font(arial, 8f, Font.NORMAL, new BaseColor(51, 102, 153));
            Font medium_blue2 = new Font(arial, 8f, Font.NORMAL, new BaseColor(0, 102, 102));
            Font medium_blue2_bold = new Font(arialbd, 8f, Font.NORMAL, new BaseColor(0, 102, 102));

            Font small_blue = new Font(arial, 7f, Font.NORMAL, new BaseColor(0, 69, 134));

            #region Naglowek
            //itcmetalcon.
            phrase = new Phrase();
            phrase.Add(new Chunk("itc", logo2));
            phrase.Add(new Chunk("metalcon", logo1));
            phrase.Add(new Chunk(".", logo2));
            phrase.Add(new Chunk("\n ", small));
            par = new Paragraph();
            par.Add(phrase);
            par.SpacingAfter = -4f;
            par.Alignment = 2;
            doc.Add(par);

            /*
            phrase = new Phrase(new Chunk(" ", small));
            par = new Paragraph();
            par.Add(phrase);
            doc.Add(par);
            */

            int cellcount = 0;
            //1tabela dane klienta
            table = new PdfPTable(3);
            cell = new PdfPCell(new Phrase(MainClass.Personaldaten[7] + " " + MainClass.Personaldaten[0] + " " + MainClass.Personaldaten[1], standard_bold));
            cell.Border = Rectangle.NO_BORDER;
            cellcount++;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(" "));
            cell.Border = Rectangle.NO_BORDER;
            cell.Rowspan = 8;
            cellcount++;
            table.AddCell(cell);

            //cell = new PdfPCell(new Phrase("Hallensysteme\n Herstellung", small_blue));
            //cell.AddElement(new Phrase("Ferligung nach maB", small_blue));
            //cell.AddElement(new Phrase("Verkauf und Vermietung", small_blue));
            cell = new PdfPCell();
            cell.Border = Rectangle.NO_BORDER;
            cell.UseAscender = true;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            //par = new Paragraph(new Phrase(" \n", small_blue));
            //par.SpacingAfter = -4f;
            //par.Alignment = 2;
            //cell.AddElement(par);
            par = new Paragraph(new Phrase("Hallensysteme\n", small_blue));
            par.SpacingAfter = -4f;
            par.Alignment = 2;
            cell.AddElement(par);
            par = new Paragraph(new Phrase("Herstellung\n", small_blue));
            par.SpacingAfter = -4f;
            par.Alignment = 2;
            cell.AddElement(par);
            par = new Paragraph(new Phrase("Ferligung nach maß\n", small_blue));
            par.SpacingAfter = -4f;
            par.Alignment = 2;
            cell.AddElement(par);
            par = new Paragraph(new Phrase("Verkauf und Vermietung\n ", small_blue));
            par.Alignment = 2;
            cell.AddElement(par);

            cell.Rowspan = 3;
            cellcount++;
            table.AddCell(cell);

            if (MainClass.Personaldaten[6].Length > 0)
            {
                cell = new PdfPCell(new Phrase(MainClass.Personaldaten[6], standard_bold));
                cell.Border = Rectangle.NO_BORDER;
                cellcount++;
                table.AddCell(cell);
            }

            if (MainClass.Personaldaten[2].Length > 0)
            {
                cell = new PdfPCell(new Phrase(MainClass.Personaldaten[2], standard));
                cell.Border = Rectangle.NO_BORDER;
                cellcount++;
                table.AddCell(cell);
            }

            if (MainClass.Personaldaten[4].Length > 0)
            {
                cell = new PdfPCell(new Phrase(MainClass.Personaldaten[4] + " " + MainClass.Personaldaten[3], standard));
            }
            else
            {
                cell = new PdfPCell(new Phrase(MainClass.Personaldaten[3], standard));
            }
            cell.Border = Rectangle.NO_BORDER;
            cellcount++;
            table.AddCell(cell);

            phrase = new Phrase();
            //Console.WriteLine(Personaldaten[6].Length);
            if (MainClass.Personaldaten[5].Length > 6)
            {
                if(cellcount == 6)
                {
                    phrase = new Phrase(" ");
                    cell = new PdfPCell(phrase);
                    cell.Border = Rectangle.NO_BORDER;
                    cellcount++;
                    table.AddCell(cell);
                }
                phrase = new Phrase(MainClass.Personaldaten[5], standard);
            }
            else
            {
                phrase = new Phrase(" ");
            }
            cell = new PdfPCell(phrase);
            cell.Border = Rectangle.NO_BORDER;
            cellcount++;
            table.AddCell(cell);

            /*
            if (MainClass.Personaldaten[6].Length == 0)
            {
                cell = new PdfPCell(new Phrase(" "));
                //cell.Border = Rectangle.NO_BORDER;
                cellcount++;
                table.AddCell(cell);
            }

            if (MainClass.Personaldaten[2].Length == 0)
            {
                cell = new PdfPCell(new Phrase(" "));
                //cell.Border = Rectangle.NO_BORDER;
                cellcount++;
                table.AddCell(cell);
            }
            */

            phrase = new Phrase(new Chunk("itcmetalcon. Pawel Swiderski", standard_bold));
            par = new Paragraph(phrase);
            par.Alignment = 2;
            cell = new PdfPCell(par);
            cell.UseAscender = true;
            cell.HorizontalAlignment = 2;
            cell.Border = Rectangle.NO_BORDER;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(" "));
            cell.Border = Rectangle.NO_BORDER;
            table.AddCell(cell);

            phrase = new Phrase(new Chunk("Normannenstrasse 2", medium));
            par = new Paragraph(phrase);
            par.Alignment = 2;
            cell = new PdfPCell(par);
            cell.UseAscender = true;
            cell.HorizontalAlignment = 2;
            cell.VerticalAlignment = Rectangle.ALIGN_BOTTOM;
            cell.Border = Rectangle.NO_BORDER;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(" "));
            cell.Border = Rectangle.NO_BORDER;
            table.AddCell(cell);

            phrase = new Phrase(new Chunk("71263 Weil der Stadt", medium));
            par = new Paragraph(phrase);
            par.Alignment = 2;
            cell = new PdfPCell(par);
            cell.UseAscender = true;
            cell.HorizontalAlignment = 2;
            cell.Border = Rectangle.NO_BORDER;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Weil der Stadt, den " + MainClass.datapotwierdzenia.ToString("dd.MM.yyyy"), standard));
            cell.Border = Rectangle.NO_BORDER;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(" "));
            cell.Border = Rectangle.NO_BORDER;
            table.AddCell(cell);
            table.AddCell(cell);

            if (MainClass.Personaldaten[6].Length != 0 && MainClass.Personaldaten[2].Length != 0)
            {
                cell = new PdfPCell(new Phrase(" "));
                cell.Border = Rectangle.NO_BORDER;
                cellcount++;
                table.AddCell(cell);
            }

            phrase = new Phrase(new Chunk("Angebot Nr: ", medium_bold));
            phrase.Add(new Chunk(MainClass.Oferta[1] + " von " + MainClass.dataoferty.ToString("dd.MM.yyyy"), medium));
            par = new Paragraph(phrase);
            par.Alignment = 2;
            cell = new PdfPCell(par);
            cell.UseAscender = true;
            cell.HorizontalAlignment = 2;
            cell.Border = Rectangle.NO_BORDER;
            table.AddCell(cell);


            cell = new PdfPCell(new Phrase(" "));
            cell.Border = Rectangle.NO_BORDER;
            table.AddCell(cell);
            //table.AddCell(cell);


            table.TotalWidth = doc.Right - doc.Left;
            //Console.WriteLine(doc.Right - doc.Left); //default 534
            //Console.WriteLine(doc.LeftMargin + " " + doc.RightMargin); //default 36/36
            table.LockedWidth = true;

            table.SetWidths(new float[] { 1.572f, 0.908f, 1.240f });

            doc.Add(table);
            #endregion

            phrase = new Phrase();
            phrase.Add(new Chunk(" \n" + "Auftragsbestätigung Nr " + MainClass.nrzam + "\n \n ", title));
            par = new Paragraph();
            par.Add(phrase);
            //par.SpacingAfter = -4f;
            par.Alignment = 1;
            doc.Add(par);

            #region Bezeichnung
            table = new PdfPTable(9);

            //naglowek
            cell = new PdfPCell(new Phrase("Ifd. Nr", medium_bold));
            cell.Rowspan = 2;
            cell.UseAscender = true;
            cell.HorizontalAlignment = 1;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Bezeichnung", medium_bold));
            cell.Rowspan = 2;
            cell.UseAscender = true;
            cell.HorizontalAlignment = 1;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("M.E.", medium_bold));
            cell.Rowspan = 2;
            cell.UseAscender = true;
            cell.HorizontalAlignment = 1;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Menge", medium_bold));
            cell.Rowspan = 2;
            cell.UseAscender = true;
            cell.HorizontalAlignment = 1;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Netto Einzelpreis (EUR)", medium_bold));
            cell.Rowspan = 2;
            cell.UseAscender = true;
            cell.HorizontalAlignment = 1;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Nettobetrag (EUR)", medium_bold));
            cell.Rowspan = 2;
            cell.UseAscender = true;
            cell.HorizontalAlignment = 1;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("MwSt %", medium_bold));
            cell.Rowspan = 2;
            cell.UseAscender = true;
            cell.HorizontalAlignment = 1;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("MwSt. Betrag (EUR)", medium_bold));
            cell.Rowspan = 2;
            cell.UseAscender = true;
            cell.HorizontalAlignment = 1;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Bruttobetrag (EUR)", medium_bold));
            cell.Rowspan = 2;
            cell.UseAscender = true;
            cell.HorizontalAlignment = 1;
            table.AddCell(cell);

            //2 linia

            cell = new PdfPCell(new Phrase("1", medium_bold));
            cell.UseAscender = true;
            cell.HorizontalAlignment = 0;
            cell.VerticalAlignment = Rectangle.ALIGN_BOTTOM;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(MainClass.Bezeich, medium));
            cell.UseAscender = true;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Stk.", medium));
            cell.UseAscender = true;
            cell.HorizontalAlignment = 1;
            cell.VerticalAlignment = Rectangle.ALIGN_BOTTOM;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("1", medium));
            cell.UseAscender = true;
            cell.HorizontalAlignment = 2;
            cell.VerticalAlignment = Rectangle.ALIGN_BOTTOM;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(MainClass.netto, medium));
            cell.UseAscender = true;
            cell.HorizontalAlignment = 2;
            cell.VerticalAlignment = Rectangle.ALIGN_BOTTOM;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(MainClass.netto, medium));
            cell.UseAscender = true;
            cell.HorizontalAlignment = 2;
            cell.VerticalAlignment = Rectangle.ALIGN_BOTTOM;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(MainClass.vat, medium));
            cell.UseAscender = true;
            cell.HorizontalAlignment = 2;
            cell.VerticalAlignment = Rectangle.ALIGN_BOTTOM;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(MainClass.kwvat, medium));
            cell.UseAscender = true;
            cell.HorizontalAlignment = 2;
            cell.VerticalAlignment = Rectangle.ALIGN_BOTTOM;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(MainClass.brutto, medium));
            cell.UseAscender = true;
            cell.HorizontalAlignment = 2;
            cell.VerticalAlignment = Rectangle.ALIGN_BOTTOM;
            table.AddCell(cell);

            //3 linia

            cell = new PdfPCell(new Phrase("Zusammen", medium_bold));
            cell.UseAscender = true;
            cell.HorizontalAlignment = 2;
            cell.Colspan = 5;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(MainClass.netto, medium));
            cell.UseAscender = true;
            cell.HorizontalAlignment = 2;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(MainClass.vat, medium));
            cell.UseAscender = true;
            cell.HorizontalAlignment = 2;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(MainClass.kwvat, medium));
            cell.UseAscender = true;
            cell.HorizontalAlignment = 2;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(MainClass.brutto, medium));
            cell.UseAscender = true;
            cell.HorizontalAlignment = 2;
            table.AddCell(cell);

            table.TotalWidth = doc.Right - doc.Left;
            table.LockedWidth = true;
            table.SetWidths(new float[] { 3.258f, 39.089f, 4.137f, 5.688f, 12.927f, 9.307f, 5.274f, 10.289f, 10.031f });
            doc.Add(table);
            #endregion

            phrase = new Phrase();
            phrase.Add(new Chunk("Zahlungen:\n ", standard_bold));
            par = new Paragraph();
            par.Add(phrase);
            par.SpacingAfter = -4f;
            doc.Add(par);

            #region Zahlungen
            table = new PdfPTable(8);

            cell = new PdfPCell(new Phrase("Ifd. Nr", medium_bold));
            cell.Rowspan = 2;
            cell.UseAscender = true;
            cell.HorizontalAlignment = 1;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(" ", medium_bold));
            cell.Rowspan = 2;
            cell.UseAscender = true;
            cell.HorizontalAlignment = 1;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Datum", medium_bold));
            cell.Rowspan = 2;
            cell.UseAscender = true;
            cell.HorizontalAlignment = 1;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Bezeichnung", medium_bold));
            cell.Rowspan = 2;
            cell.UseAscender = true;
            cell.HorizontalAlignment = 1;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Nettobetrag (EUR)", medium_bold));
            cell.Rowspan = 2;
            cell.UseAscender = true;
            cell.HorizontalAlignment = 1;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("MwSt %", medium_bold));
            cell.Rowspan = 2;
            cell.UseAscender = true;
            cell.HorizontalAlignment = 1;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("MwSt. Betrag (EUR)", medium_bold));
            cell.Rowspan = 2;
            cell.UseAscender = true;
            cell.HorizontalAlignment = 1;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Bruttobetrag (EUR)", medium_bold));
            cell.Rowspan = 2;
            cell.UseAscender = true;
            cell.HorizontalAlignment = 1;
            table.AddCell(cell);

            //2 linia

            cell = new PdfPCell(new Phrase("1", medium_green));
            cell.UseAscender = true;
            cell.HorizontalAlignment = 1;
            cell.VerticalAlignment = Rectangle.ALIGN_MIDDLE;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(" ", medium_green));
            cell.UseAscender = true;
            cell.HorizontalAlignment = 1;
            cell.VerticalAlignment = Rectangle.ALIGN_MIDDLE;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Auftragserteilung", medium_green));
            cell.UseAscender = true;
            cell.HorizontalAlignment = 1;
            cell.VerticalAlignment = Rectangle.ALIGN_MIDDLE;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("30 % Anzahlung", medium_green));
            cell.UseAscender = true;
            cell.HorizontalAlignment = 1;
            cell.VerticalAlignment = Rectangle.ALIGN_MIDDLE;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(MainClass.netto30, medium_green));
            cell.UseAscender = true;
            cell.HorizontalAlignment = 1;
            cell.VerticalAlignment = Rectangle.ALIGN_MIDDLE;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(MainClass.vat, medium_green));
            cell.UseAscender = true;
            cell.HorizontalAlignment = 1;
            cell.VerticalAlignment = Rectangle.ALIGN_MIDDLE;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(MainClass.vat30, medium_green));
            cell.UseAscender = true;
            cell.HorizontalAlignment = 1;
            cell.VerticalAlignment = Rectangle.ALIGN_MIDDLE;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(MainClass.brutto30, medium_green));
            cell.UseAscender = true;
            cell.HorizontalAlignment = 1;
            cell.VerticalAlignment = Rectangle.ALIGN_MIDDLE;
            table.AddCell(cell);

            //3 linia

            cell = new PdfPCell(new Phrase("2", medium_blue));
            cell.UseAscender = true;
            cell.HorizontalAlignment = 1;
            cell.VerticalAlignment = Rectangle.ALIGN_MIDDLE;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(" ", medium_blue));
            cell.UseAscender = true;
            cell.HorizontalAlignment = 1;
            cell.VerticalAlignment = Rectangle.ALIGN_MIDDLE;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Anlieferung", medium_blue));
            cell.UseAscender = true;
            cell.HorizontalAlignment = 1;
            cell.VerticalAlignment = Rectangle.ALIGN_MIDDLE;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("60 % Anzahlung", medium_blue));
            cell.UseAscender = true;
            cell.HorizontalAlignment = 1;
            cell.VerticalAlignment = Rectangle.ALIGN_MIDDLE;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(MainClass.netto60, medium_blue));
            cell.UseAscender = true;
            cell.HorizontalAlignment = 1;
            cell.VerticalAlignment = Rectangle.ALIGN_MIDDLE;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(MainClass.vat, medium_blue));
            cell.UseAscender = true;
            cell.HorizontalAlignment = 1;
            cell.VerticalAlignment = Rectangle.ALIGN_MIDDLE;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(MainClass.vat60, medium_blue));
            cell.UseAscender = true;
            cell.HorizontalAlignment = 1;
            cell.VerticalAlignment = Rectangle.ALIGN_MIDDLE;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(MainClass.brutto60, medium_blue));
            cell.UseAscender = true;
            cell.HorizontalAlignment = 1;
            cell.VerticalAlignment = Rectangle.ALIGN_MIDDLE;
            table.AddCell(cell);

            //4 linia

            cell = new PdfPCell(new Phrase("3", medium_green));
            cell.UseAscender = true;
            cell.HorizontalAlignment = 1;
            cell.VerticalAlignment = Rectangle.ALIGN_MIDDLE;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(" ", medium_green));
            cell.UseAscender = true;
            cell.HorizontalAlignment = 1;
            cell.VerticalAlignment = Rectangle.ALIGN_MIDDLE;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Abnahme", medium_green));
            cell.UseAscender = true;
            cell.HorizontalAlignment = 1;
            cell.VerticalAlignment = Rectangle.ALIGN_MIDDLE;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("10 % Restzahlung", medium_green));
            cell.UseAscender = true;
            cell.HorizontalAlignment = 1;
            cell.VerticalAlignment = Rectangle.ALIGN_MIDDLE;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(MainClass.netto10, medium_green));
            cell.UseAscender = true;
            cell.HorizontalAlignment = 1;
            cell.VerticalAlignment = Rectangle.ALIGN_MIDDLE;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(MainClass.vat, medium_green));
            cell.UseAscender = true;
            cell.HorizontalAlignment = 1;
            cell.VerticalAlignment = Rectangle.ALIGN_MIDDLE;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(MainClass.vat10, medium_green));
            cell.UseAscender = true;
            cell.HorizontalAlignment = 1;
            cell.VerticalAlignment = Rectangle.ALIGN_MIDDLE;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(MainClass.brutto10, medium_green));
            cell.UseAscender = true;
            cell.HorizontalAlignment = 1;
            cell.VerticalAlignment = Rectangle.ALIGN_MIDDLE;
            table.AddCell(cell);

            //5 linia

            cell = new PdfPCell(new Phrase("Zusammen erhalten", medium_bold));
            cell.Colspan = 3;
            cell.UseAscender = true;
            cell.HorizontalAlignment = 1;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(" ", medium));
            cell.UseAscender = true;
            cell.HorizontalAlignment = 1;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("-", medium));
            cell.UseAscender = true;
            cell.HorizontalAlignment = 1;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(MainClass.vat, medium));
            cell.UseAscender = true;
            cell.HorizontalAlignment = 1;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("-", medium));
            cell.UseAscender = true;
            cell.HorizontalAlignment = 1;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("-", medium));
            cell.UseAscender = true;
            cell.HorizontalAlignment = 1;
            table.AddCell(cell);

            //6 linia

            cell = new PdfPCell(new Phrase("Fälliger Betrag", medium_blue2_bold));
            cell.Colspan = 3;
            cell.UseAscender = true;
            cell.HorizontalAlignment = 1;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(" ", medium));
            cell.UseAscender = true;
            cell.HorizontalAlignment = 1;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("-", medium));
            cell.UseAscender = true;
            cell.HorizontalAlignment = 1;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(MainClass.vat, medium_blue2));
            cell.UseAscender = true;
            cell.HorizontalAlignment = 1;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("-", medium));
            cell.UseAscender = true;
            cell.HorizontalAlignment = 1;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("-", medium));
            cell.UseAscender = true;
            cell.HorizontalAlignment = 1;
            table.AddCell(cell);


            table.TotalWidth = (doc.Right - doc.Left) * 0.75233f;
            table.LockedWidth = true;
            table.HorizontalAlignment = 0;
            table.SetWidths(new float[] { 4.399f, 0.893f, 18.351f, 21.993f, 14.914f, 8.384f, 16.151f, 14.845f });
            doc.Add(table);
            #endregion

            phrase = new Phrase();
            phrase.Add(new Chunk(" \n" + "Liefertermin / Montagebeginn", standard_bold));
            phrase.Add(new Chunk(": " + MainClass.liefer, standard));
            phrase.Add(new Chunk(" nach Auftragserteilung / Anzahlungseingang.", standard));
            par = new Paragraph();
            par.Add(phrase);
            //par.SpacingAfter = -4f;
            doc.Add(par);

            par = new Paragraph(new Phrase(" ", standard));
            par.SpacingAfter = 0f;
            par.Alignment = 0;
            doc.Add(par);

            par = new Paragraph(new Phrase(" ", standard));
            par.SpacingAfter = 0f;
            par.Alignment = 0;
            doc.Add(par);

            par = new Paragraph(new Phrase(" ", standard));
            par.SpacingAfter = 0f;
            par.Alignment = 0;
            doc.Add(par);

            phrase = new Phrase(new Chunk("Mit freundlichen Grüßen / With best regards / ", standard));
            phrase.Add(new Chunk("致以亲切问候", chinese));
            par = new Paragraph(phrase);
            par.SpacingAfter = -4f;
            par.SetLeading(10f, 0f);
            par.Alignment = 0;
            doc.Add(par);

            par = new Paragraph(new Phrase(" ", standard));
            par.SpacingAfter = 0f;
            par.Alignment = 0;
            doc.Add(par);

            phrase = new Phrase(new Chunk("Maja Zerko", standard));
            par = new Paragraph(phrase);
            par.SpacingAfter = -4f;
            par.SetLeading(10f, 0f);
            par.Alignment = 0;
            doc.Add(par);

            doc.Close();

            writer.Close();

            fs.Close();
        }
    }
}
