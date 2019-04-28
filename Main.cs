using AppKit;
using System;
using System.IO;
using System.Text;
using System.Reflection;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace Oferta__
{
    static class MainClass
    {
        static void Main(string[] args)
        {
            NSApplication.Init();
            NSApplication.Main(args);
        }

        public static string[] mainbaza = new string[0];
        public static string[] mainbaza2 = new string[0];

        public static string[] mainbaza_cena = new string[0];

        public static string[] bazaComboBox1 = new string[3];
        public static string[] bazaComboBox2 = new string[3];

        public static string[] bazaTabela1 = new string[0];
        public static int[] bazaTabela1_ilosc = new int[0];
        public static float[] bazaTabela1_cena = new float[0];
        public static string[] bazaTabela1_x = new string[0];
        public static string[] bazaTabela1_y = new string[0];
        public static string[] bazaTabela1_jedn = new string[0];
        public static int pozycja1;

        public static string[] bazaTabela2 = new string[0];
        public static int[] bazaTabela2_ilosc = new int[0];
        public static float[] bazaTabela2_cena = new float[0];
        public static string[] bazaTabela2_x = new string[0];
        public static string[] bazaTabela2_y = new string[0];
        public static string[] bazaTabela2_jedn = new string[0];
        public static int pozycja2;

        public static string[] bazaTabela3 = new string[0];
        public static int pozycja3;

        public static string[] bazaTabela4 = new string[0];

        public static string[] bazaTabela5 = new string[0];
        public static string[] bazaTabela5_data = new string[0];
        public static int pozycja5;

        public static string[] Personaldaten = new string[9];
        public static string[] Halledaten = new string[12];
        public static string[] Halledaten2 = new string[9];
        public static string[] HalledatenCa = new string[7];
        public static string[] Oferta = new string[3];

        public static string CenaMontaz = "";

        public static string TechnischeDaten = "";
        public static string Unterlagen = "";
        public static string[] Technische = new string[6];

        public static string ToreUndTuren = "";
        public static string Montage = "";
        public static string VonIhnen = "";
        
        public static string AufWunsch = "";

        public static string Select2 = "";

        public static int pozycja4;

        public static int Select3;

        public static string Stallhalle = "";
        public static string HauptGewicht = "";

        public static string[] Nawiasy = new string[4];

        public static string Bezeich = "";
        public static string netto = "";
        public static string vat = "";
        public static string kwvat = "";
        public static string brutto = "";
        public static string netto30 = "";
        public static string netto60 = "";
        public static string netto10 = "";
        public static string brutto30 = "";
        public static string brutto60 = "";
        public static string brutto10 = "";
        public static string vat30 = "";
        public static string vat60 = "";
        public static string vat10 = "";

        public static string nrzam = "";
        public static string liefer = "";

        public static DateTime dataoferty;
        public static DateTime datapotwierdzenia;


        public static void CreatePDF()
        {
            FileStream fs = new FileStream("dokument.pdf", FileMode.Create, FileAccess.Write, FileShare.None);

            Document doc = new Document(PageSize.A4);
            doc.SetMargins(24.65f, 23.8f, doc.TopMargin, doc.BottomMargin);

            string path = Environment.CurrentDirectory;

            string nazwa1 = Oferta[1].Replace("/", "") + "_" + Halledaten[1] + "x" + Halledaten[2] + "m_" + Halledaten[0] + "_" + Personaldaten[7] + " " + Personaldaten[0] + " " + Personaldaten[1];
            PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location.Replace("Oferta+.app/Contents/MonoBundle", nazwa1 + ".pdf")), FileMode.Create));

            doc.Open();



            string[] Tabela = AllManager.CreateReadyElement(bazaTabela1, bazaTabela1_x, bazaTabela1_y);
            string[] Tabela2 = AllManager.CreateReadyElement(bazaTabela2, bazaTabela2_x, bazaTabela2_y);

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

            Font standard = new Font(arial, 9f, Font.NORMAL, BaseColor.BLACK);
            Font standard_bold = new Font(arialbd, 9f, Font.NORMAL, BaseColor.BLACK);
            Font standard_blue = new Font(arialbd, 9f, Font.NORMAL, new BaseColor(0, 69, 134));
            Font standard_lightblue = new Font(arial, 9f, Font.NORMAL, new BaseColor(0, 69, 134));

            Font chinese = new Font(simsun, 9f, Font.NORMAL, BaseColor.BLACK);

            Font small = new Font(verdana, 7f, Font.NORMAL, BaseColor.BLACK);

            Font medium = new Font(arial, 8f, Font.NORMAL, BaseColor.BLACK);
            Font medium_bold = new Font(arialbd, 8f, Font.NORMAL, BaseColor.BLACK);

            Font small_blue = new Font(arial, 7f, Font.NORMAL, new BaseColor(0, 69, 134));

            //itcmetalcon.
            phrase = new Phrase();
            phrase.Add(new Chunk("itc", logo2));
            phrase.Add(new Chunk("metalcon", logo1));
            phrase.Add(new Chunk(".", logo2));
            par = new Paragraph();
            par.Add(phrase);
            par.Alignment = 2;
            doc.Add(par);

            //1tabela dane klienta
            table = new PdfPTable(3);
            cell = new PdfPCell(new Phrase(Personaldaten[7] + " " + Personaldaten[0] + " " + Personaldaten[1], standard_bold));
            cell.Border = Rectangle.NO_BORDER;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(" "));
            cell.Border = Rectangle.NO_BORDER;
            cell.Rowspan = 6;
            table.AddCell(cell);

            //cell = new PdfPCell(new Phrase("Hallensysteme\n Herstellung", small_blue));
            //cell.AddElement(new Phrase("Ferligung nach maB", small_blue));
            //cell.AddElement(new Phrase("Verkauf und Vermietung", small_blue));
            cell = new PdfPCell();
            cell.Border = Rectangle.NO_BORDER;
            cell.UseAscender = true;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            par = new Paragraph(new Phrase(" \n", small_blue));
            par.SpacingAfter = -4f;
            par.Alignment = 2;
            cell.AddElement(par);
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

            cell.Rowspan = 4;
            table.AddCell(cell);

            if(Personaldaten[6].Length > 0)
            {
                cell = new PdfPCell(new Phrase(Personaldaten[6], standard_bold));
                cell.Border = Rectangle.NO_BORDER;
                table.AddCell(cell);
            }

            if(Personaldaten[2].Length > 0)
            {
                cell = new PdfPCell(new Phrase(Personaldaten[2], standard));
                cell.Border = Rectangle.NO_BORDER;
                table.AddCell(cell);
            }

            if(Personaldaten[4].Length > 0)
            {
                cell = new PdfPCell(new Phrase(Personaldaten[4] + " " + Personaldaten[3], standard));
            }
            else
            {
                cell = new PdfPCell(new Phrase(Personaldaten[3], standard));
            }
            cell.Border = Rectangle.NO_BORDER;
            table.AddCell(cell);

            phrase = new Phrase();
            //Console.WriteLine(Personaldaten[6].Length);
            if(Personaldaten[5].Length > 6)
            {
                phrase = new Phrase(Personaldaten[5], standard);
            }
            else
            {
                phrase = new Phrase(" ");
            }
            cell = new PdfPCell(phrase);
            cell.Border = Rectangle.NO_BORDER;
            table.AddCell(cell);

            if(Personaldaten[6].Length == 0)
            {
                cell = new PdfPCell(new Phrase(" "));
                cell.Border = Rectangle.NO_BORDER;
                table.AddCell(cell);
            }

            if (Personaldaten[2].Length == 0)
            {
                cell = new PdfPCell(new Phrase(" "));
                cell.Border = Rectangle.NO_BORDER;
                table.AddCell(cell);
            }




            phrase = new Phrase(new Chunk("Angebot Nr: ",medium_bold));
            phrase.Add(new Chunk(Oferta[1] + " von " + dataoferty.ToString("dd.MM.yyyy"), medium));
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
            table.AddCell(cell);

            table.TotalWidth = doc.Right - doc.Left;
            //Console.WriteLine(doc.Right - doc.Left); //default 534
            //Console.WriteLine(doc.LeftMargin + " " + doc.RightMargin); //default 36/36
            table.LockedWidth = true;

            table.SetWidths(new float[] { 1.572f, 0.908f, 1.240f });

            doc.Add(table);






            par = new Paragraph(new Phrase("Sehr geehrter " + Personaldaten[7] + " " + Personaldaten[1] + ",", standard));
            par.SpacingAfter = -2f;
            doc.Add(par);
            par = new Paragraph(new Phrase("wir bedanken uns für Ihre Anfrage und bieten Ihnen freibleibend eine " + Halledaten[0] + " wie folgt an:\n ", standard));
            doc.Add(par);



            table = new PdfPTable(5);

            if(Nawiasy[0] == "On")
            {
                cell = new PdfPCell(new Phrase("Breite (Außenkannte Fassade):", standard));
            }
            else
            {
                cell = new PdfPCell(new Phrase("Breite:", standard));
            }
            cell.Border = Rectangle.NO_BORDER;
            table.AddCell(cell);

            string breite = String.Format("{0:0.00}", Convert.ToDouble(Halledaten[1].Replace(",", "."))).Replace(".", ",") + " m";
            if(breite == "0,00 m")
            {
                breite = "";
            }
            if (MainClass.HalledatenCa[1] == "On")
            {
                breite = "ca. " + breite;
            }
            if(MainClass.Halledaten2[1].Length > 0)
            {
                breite = breite + "(" + Halledaten2[1] + ")";
            }
            par = new Paragraph(breite, standard);
            par.Alignment = 2;
            cell = new PdfPCell();
            cell.Border = Rectangle.NO_BORDER;
            cell.UseAscender = true;
            cell.VerticalAlignment = Element.ALIGN_MIDDLE;
            cell.AddElement(par);
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(" "));
            cell.Border = Rectangle.NO_BORDER;
            table.AddCell(cell);

            if(Nawiasy[1] == "On")
            {
                cell = new PdfPCell(new Phrase("Länge (Außenkannte Fassade):", standard));
            }
            else
            {
                cell = new PdfPCell(new Phrase("Länge:", standard));
            }
            cell.Border = Rectangle.NO_BORDER;
            table.AddCell(cell);

            string lange = String.Format("{0:0.00}", Convert.ToDouble(Halledaten[2].Replace(",", "."))).Replace(".", ",") + " m";
            if(lange == "0,00 m")
            {
                lange = "";
            }
            if (MainClass.HalledatenCa[2] == "On")
            {
                lange = "ca. " + lange;
            }
            if (MainClass.Halledaten2[2].Length > 0)
            {
                lange = lange + "(" + Halledaten2[2] + ")";
            }
            par = new Paragraph(lange, standard);
            par.Alignment = 2;
            cell = new PdfPCell();
            cell.Border = Rectangle.NO_BORDER;
            cell.UseAscender = true;
            cell.VerticalAlignment = Element.ALIGN_MIDDLE;
            cell.AddElement(par);
            table.AddCell(cell);

            if(Nawiasy[2] == "On")
            {
                cell = new PdfPCell(new Phrase("Traufhöhe (Schnittpunkt AK Wand - OK Dach):", standard));
            }
            else
            {
                cell = new PdfPCell(new Phrase("Traufhöhe:", standard));
            }
            cell.Border = Rectangle.NO_BORDER;
            table.AddCell(cell);

            string traufhohe = String.Format("{0:0.00}", Convert.ToDouble(Halledaten[3].Replace(",", "."))).Replace(".", ",") + " m";
            if(traufhohe == "0,00 m")
            {
                traufhohe = "";
            }
            if (MainClass.HalledatenCa[3] == "On")
            {
                traufhohe = "ca. " + traufhohe;
            }
            if (MainClass.Halledaten2[3].Length > 0)
            {
                traufhohe = traufhohe + "(" + Halledaten2[3] + ")";
            }
            par = new Paragraph(traufhohe, standard);
            par.Alignment = 2;
            cell = new PdfPCell();
            cell.Border = Rectangle.NO_BORDER;
            cell.UseAscender = true;
            cell.VerticalAlignment = Element.ALIGN_MIDDLE;
            cell.AddElement(par);
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(" "));
            cell.Border = Rectangle.NO_BORDER;
            table.AddCell(cell);

            if(Nawiasy[3] == "On")
            {
                cell = new PdfPCell(new Phrase("Firsthöhe (OK Dach):", standard));
            }
            else
            {
                cell = new PdfPCell(new Phrase("Firsthöhe:", standard));
            }
            cell.Border = Rectangle.NO_BORDER;
            table.AddCell(cell);

            string firsthohe = String.Format("{0:0.00}", Convert.ToDouble(Halledaten[4].Replace(",", "."))).Replace(".", ",") + " m";
            if(firsthohe == "0,00 m")
            {
                firsthohe = "";
            }
            if (MainClass.HalledatenCa[4] == "On")
            {
                firsthohe = "ca. " + firsthohe;
            }
            if (MainClass.Halledaten2[4].Length > 0)
            {
                firsthohe = firsthohe + "(" + Halledaten2[4] + ")";
            }
            par = new Paragraph(firsthohe, standard);
            par.Alignment = 2;
            cell = new PdfPCell();
            cell.Border = Rectangle.NO_BORDER;
            cell.UseAscender = true;
            cell.VerticalAlignment = Element.ALIGN_MIDDLE;
            cell.AddElement(par);
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Binderabstand:", standard));
            cell.Border = Rectangle.NO_BORDER;
            table.AddCell(cell);

            string binderabstand = String.Format("{0:0.00}", Convert.ToDouble(Halledaten[5].Replace(",", "."))).Replace(".", ",") + " m";
            if(binderabstand == "0,00 m")
            {
                binderabstand = "";
            }
            if (MainClass.HalledatenCa[5] == "On")
            {
                binderabstand = "ca. " + binderabstand;
            }
            if (MainClass.Halledaten2[5].Length > 0)
            {
                binderabstand = binderabstand + "(" + Halledaten2[5] + ")";
            }
            par = new Paragraph(binderabstand, standard);
            par.Alignment = 2;
            cell = new PdfPCell();
            cell.Border = Rectangle.NO_BORDER;
            cell.UseAscender = true;
            cell.VerticalAlignment = Element.ALIGN_MIDDLE;
            cell.AddElement(par);
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(" "));
            cell.Border = Rectangle.NO_BORDER;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Zugbandhöhe:", standard));
            cell.Border = Rectangle.NO_BORDER;
            table.AddCell(cell);

            string zugbandhohe = String.Format("{0:0.00}", Convert.ToDouble(Halledaten[6].Replace(",", "."))).Replace(".", ",") + " m";
            if (MainClass.HalledatenCa[6] == "On")
            {
                zugbandhohe = "ca. " + zugbandhohe;
            }
            if (MainClass.Halledaten2[6].Length > 0)
            {
                zugbandhohe = zugbandhohe + "(" + Halledaten2[6] + ")";
            }
            if(Halledaten[6] == "0")
            {
                par = new Paragraph("-", standard);
            }
            else
            {
                par = new Paragraph(zugbandhohe, standard);
            }
            par.Alignment = 2;
            cell = new PdfPCell();
            cell.Border = Rectangle.NO_BORDER;
            cell.UseAscender = true;
            cell.VerticalAlignment = Element.ALIGN_MIDDLE;
            cell.AddElement(par);
            table.AddCell(cell);

            if(Stallhalle == "Off")
            {
                if (Halledaten[11] == "0" && Halledaten[7] != "0 x 0")
                {
                    cell = new PdfPCell(new Phrase("Hauptprofil:", standard));
                }
                else if (Halledaten[11] != "0" && Halledaten[7] == "0 x 0")
                {
                    cell = new PdfPCell(new Phrase("Gewicht:", standard));
                }
                else if (Halledaten[11] != "0" && Halledaten[7] != "0 x 0")
                {
                    cell = new PdfPCell(new Phrase("Hauptprofil/Gewicht:", standard));
                }
                else if (Halledaten[11] == "0" && Halledaten[7] == "0 x 0")
                {
                    cell = new PdfPCell(new Phrase("Hauptprofil:", standard));
                }
                cell.Border = Rectangle.NO_BORDER;
                table.AddCell(cell);
            }
            else
            {
                cell = new PdfPCell(new Phrase("Hauptprofil / Gewicht (ca.):", standard));
                cell.Border = Rectangle.NO_BORDER;
                table.AddCell(cell);
            }

            if(Stallhalle == "Off")
            {
                if (Halledaten[11] == "0" && Halledaten[7] != "0 x 0")
                {
                    par = new Paragraph(Halledaten[7] + " mm", standard);
                }
                else if (Halledaten[11] != "0" && Halledaten[7] == "0 x 0")
                {
                    par = new Paragraph(String.Format("{0:0.00}", Convert.ToDouble(Halledaten[11].Replace(",", "."))).Replace(".", ",") + " To.", standard);
                }
                else if (Halledaten[11] != "0" && Halledaten[7] != "0 x 0")
                {
                    par = new Paragraph(Halledaten[7] + " mm, " + String.Format("{0:0.00}", Convert.ToDouble(Halledaten[11].Replace(",", "."))).Replace(".", ",") + " To.", standard);
                }
                else if (Halledaten[11] == "0" && Halledaten[7] == "0 x 0")
                {
                    par = new Paragraph("-", standard);
                }
            }
            else
            {
                par = new Paragraph(HauptGewicht, standard);
            }


            par.Alignment = 2;
            cell = new PdfPCell();
            cell.Border = Rectangle.NO_BORDER;
            cell.UseAscender = true;
            cell.VerticalAlignment = Element.ALIGN_MIDDLE;
            cell.AddElement(par);
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(" "));
            cell.Border = Rectangle.NO_BORDER;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Dach:", standard));
            cell.Border = Rectangle.NO_BORDER;
            table.AddCell(cell);

            if(Halledaten[8] == "0")
            {
                par = new Paragraph("-", standard);
            }
            else
            {
                par = new Paragraph(Halledaten[8] + "°", standard);
            }
            par.Alignment = 2;
            cell = new PdfPCell();
            cell.Border = Rectangle.NO_BORDER;
            cell.UseAscender = true;
            cell.VerticalAlignment = Element.ALIGN_MIDDLE;
            cell.AddElement(par);
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Schneelastzone " + Halledaten2[7], standard));
            cell.Border = Rectangle.NO_BORDER;
            table.AddCell(cell);

            par = new Paragraph(String.Format("{0:0.00}", Convert.ToDouble(Halledaten[9].Replace(",", "."))).Replace(".", ",") + " kN/m", standard);
            par.Add(new Chunk("2", FontFactory.GetFont(FontFactory.HELVETICA, 6f, Font.NORMAL)).SetTextRise(3));
            par.Alignment = 2;
            cell = new PdfPCell();
            cell.Border = Rectangle.NO_BORDER;
            cell.UseAscender = true;
            cell.VerticalAlignment = Element.ALIGN_MIDDLE;
            cell.AddElement(par);
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(" "));
            cell.Border = Rectangle.NO_BORDER;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Windzone " + Halledaten2[8], standard));
            cell.Border = Rectangle.NO_BORDER;
            table.AddCell(cell);

            par = new Paragraph(String.Format("{0:0.00}", Convert.ToDouble(Halledaten[10].Replace(",", "."))).Replace(".", ",") + " kN/m", standard);
            par.Add(new Chunk("2", FontFactory.GetFont(FontFactory.HELVETICA, 6f, Font.NORMAL)).SetTextRise(3));
            par.Alignment = 2;
            cell = new PdfPCell();
            cell.Border = Rectangle.NO_BORDER;
            cell.UseAscender = true;
            cell.VerticalAlignment = Element.ALIGN_MIDDLE;
            cell.AddElement(par);
            table.AddCell(cell);

            table.TotalWidth = doc.Right - doc.Left;
            table.LockedWidth = true;
            table.SetWidths(new float[] { 1.376f, 0.604f, 0.272f, 0.911f, 0.605f });

            doc.Add(table);




            par = new Paragraph(new Phrase("Kauf der Halle:", standard_bold));
            par.SpacingBefore = 10f;
            par.SpacingAfter = 10f;
            doc.Add(par);





            table = new PdfPTable(3);
            cell = new PdfPCell(new Phrase("Bezeichnung", standard_blue));
            cell.HorizontalAlignment = 0;
            cell.Border = Rectangle.LEFT_BORDER | Rectangle.TOP_BORDER;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Anzahl", standard_blue));
            cell.HorizontalAlignment = 0;
            cell.Border = Rectangle.TOP_BORDER;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Preis", standard_blue));
            cell.HorizontalAlignment = 2;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.TOP_BORDER;
            table.AddCell(cell);

            if (bazaTabela1.Length > 1)
            {
                int count = 0;
                do
                {

                    cell = new PdfPCell(new Phrase(Tabela[count], standard));
                    cell.HorizontalAlignment = 0;
                    cell.Border = Rectangle.LEFT_BORDER;
                    table.AddCell(cell);

                    /*
                    if(Select2 == "0")
                    {
                        cell = new PdfPCell(new Phrase(Convert.ToString(bazaTabela1_ilosc[count]) + " Stk.", standard));
                    }
                    else if(Select2 == "1")
                    {
                        cell = new PdfPCell(new Phrase(Convert.ToString(bazaTabela1_ilosc[count]) + " Lfm.", standard));
                    }
                    else if(Select2 == "2")
                    {
                        cell = new PdfPCell(new Phrase(Convert.ToString(bazaTabela1_ilosc[count]) + " m²", standard));
                    }
                    */
                    cell = new PdfPCell(new Phrase(Convert.ToString(bazaTabela1_ilosc[count]) + " " + bazaTabela1_jedn[count], standard));
                    cell.HorizontalAlignment = 0;
                    cell.Border = Rectangle.NO_BORDER;
                    table.AddCell(cell);

                    string cenaa = String.Format("{0:0.00}", (Convert.ToDouble(bazaTabela1_cena[count]) * Convert.ToDouble(bazaTabela1_ilosc[count]))).Replace(".", ",");
                    if(cenaa == "0,00")
                    {
                        cenaa = "inkl.";
                    }
                    else
                    {
                        cenaa = cenaa + " €";
                    }
                    cell = new PdfPCell(new Phrase(cenaa, standard));
                    cell.HorizontalAlignment = 2;
                    cell.Border = Rectangle.RIGHT_BORDER;
                    table.AddCell(cell);

                    count++;
                } while (count < Tabela.Length - 1);
            }

            if(bazaTabela1.Length > 0)
            {
                cell = new PdfPCell(new Phrase(Tabela[Tabela.Length - 1], standard));
                cell.HorizontalAlignment = 0;
                cell.Border = Rectangle.LEFT_BORDER;
                table.AddCell(cell);

                /*
                if (Select2 == "0")
                {
                    cell = new PdfPCell(new Phrase(Convert.ToString(bazaTabela1_ilosc[Tabela.Length -1]) + " Stk.", standard));
                }
                else if (Select2 == "1")
                {
                    cell = new PdfPCell(new Phrase(Convert.ToString(bazaTabela1_ilosc[Tabela.Length - 1]) + " Lfm.", standard));
                }
                else if (Select2 == "2")
                {
                    cell = new PdfPCell(new Phrase(Convert.ToString(bazaTabela1_ilosc[Tabela.Length - 1]) + " m²", standard));
                }
                */
                cell = new PdfPCell(new Phrase(Convert.ToString(bazaTabela1_ilosc[Tabela.Length - 1]) + " " + bazaTabela1_jedn[Tabela.Length - 1], standard));
                cell.HorizontalAlignment = 0;
                cell.Border = Rectangle.NO_BORDER;
                table.AddCell(cell);

                string cena = String.Format("{0:0.00}", Convert.ToDouble(bazaTabela1_cena[Tabela.Length - 1])).Replace(".", ",");
                if (cena == "0,00")
                {
                    cena = "inkl.";
                }
                else
                {
                    cena = cena + " €";
                }
                cell = new PdfPCell(new Phrase(cena, standard));
                cell.HorizontalAlignment = 2;
                cell.Border = Rectangle.RIGHT_BORDER;
                table.AddCell(cell);
            }
            else
            {
                cell = new PdfPCell(new Phrase("-", standard));
                cell.HorizontalAlignment = 0;
                cell.Border = Rectangle.LEFT_BORDER;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase(" ", standard));
                cell.HorizontalAlignment = 0;
                cell.Border = Rectangle.NO_BORDER;
                table.AddCell(cell);


                cell = new PdfPCell(new Phrase(" ", standard));
                cell.HorizontalAlignment = 2;
                cell.Border = Rectangle.RIGHT_BORDER;
                table.AddCell(cell);
            }


            cell = new PdfPCell(new Phrase(" ", standard));
            cell.HorizontalAlignment = 0;
            cell.Border = Rectangle.LEFT_BORDER;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(" ", standard));
            cell.HorizontalAlignment = 0;
            cell.Border = Rectangle.NO_BORDER;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(" " , standard));
            cell.HorizontalAlignment = 2;
            cell.Border = Rectangle.RIGHT_BORDER;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Gestamtpreis", standard_bold));
            cell.HorizontalAlignment = 0;
            cell.Border = Rectangle.LEFT_BORDER;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(" ", standard));
            cell.HorizontalAlignment = 0;
            cell.Border = Rectangle.NO_BORDER;
            table.AddCell(cell);

            string suma = String.Format("{0:0.00}", Convert.ToDouble(AllManager.PoliczSume(MainClass.bazaTabela1_ilosc, MainClass.bazaTabela1_cena, 0f).Replace(",", "."))).Replace(".", ",");
            phrase = new Phrase();
            phrase.Add(new Chunk(suma, standard_bold));
            phrase.Add(new Chunk(" €", standard));
            cell = new PdfPCell(phrase);
            cell.HorizontalAlignment = 2;
            cell.Border = Rectangle.RIGHT_BORDER;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("zzgl. Montage (ohne Übernachtungskosten)", standard_lightblue));
            cell.HorizontalAlignment = 0;
            cell.Border = Rectangle.LEFT_BORDER | Rectangle.BOTTOM_BORDER;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(" ", standard));
            cell.HorizontalAlignment = 0;
            cell.Border = Rectangle.BOTTOM_BORDER;
            table.AddCell(cell);

            if(CenaMontaz != "0")
            {
                suma = String.Format("{0:0.00}", Convert.ToDouble(CenaMontaz.Replace(",", "."))).Replace(".", ",");
                cell = new PdfPCell(new Phrase(suma + " €", standard_lightblue));
            }
            else
            {
                cell = new PdfPCell(new Phrase("inkl.", standard_lightblue));
            }
            cell.HorizontalAlignment = 2;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER;
            table.AddCell(cell);

            table.TotalWidth = doc.Right - doc.Left;
            table.LockedWidth = true;
            table.SetWidths(new float[] { 3.034f, 0.239f, 0.429f });

            doc.Add(table);








            par = new Paragraph(new Phrase("Zusätzliche Ausstattungsmöglichkeiten gegen Aufpreis (inkl. Zubehör und Montagekosten):", standard_bold));
            par.SpacingBefore = 4f;
            par.SpacingAfter = 11f;
            doc.Add(par);





            table = new PdfPTable(3);
            cell = new PdfPCell(new Phrase("Bezeichnung", standard_blue));
            cell.HorizontalAlignment = 0;
            cell.Border = Rectangle.LEFT_BORDER | Rectangle.TOP_BORDER;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Anzahl", standard_blue));
            cell.HorizontalAlignment = 0;
            cell.Border = Rectangle.TOP_BORDER;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Preis", standard_blue));
            cell.HorizontalAlignment = 2;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.TOP_BORDER;
            table.AddCell(cell);


            if (bazaTabela2.Length > 1)
            {
                int count = 0;
                do
                {
                    cell = new PdfPCell(new Phrase(Tabela2[count], standard));
                    cell.HorizontalAlignment = 0;
                    cell.Border = Rectangle.LEFT_BORDER;
                    table.AddCell(cell);

                    /*
                    if (Select2 == "0")
                    {
                        cell = new PdfPCell(new Phrase(Convert.ToString(bazaTabela2_ilosc[count]) + " Stk.", standard));
                    }
                    else if (Select2 == "1")
                    {
                        cell = new PdfPCell(new Phrase(Convert.ToString(bazaTabela2_ilosc[count]) + " Lfm.", standard));
                    }
                    else if (Select2 == "2")
                    {
                        cell = new PdfPCell(new Phrase(Convert.ToString(bazaTabela2_ilosc[count]) + " m²", standard));
                    }
                    */
                    cell = new PdfPCell(new Phrase(Convert.ToString(bazaTabela2_ilosc[count]) + " " + bazaTabela2_jedn[count], standard));
                    cell.HorizontalAlignment = 0;
                    cell.Border = Rectangle.NO_BORDER;
                    table.AddCell(cell);

                    string cenaa = String.Format("{0:0.00}", Convert.ToDouble(bazaTabela2_cena[count])).Replace(".", ",");
                    cell = new PdfPCell(new Phrase(cenaa + " €", standard));
                    cell.HorizontalAlignment = 2;
                    cell.Border = Rectangle.RIGHT_BORDER;
                    table.AddCell(cell);

                    count++;
                } while (count < Tabela2.Length - 1);
            }

            if(bazaTabela2.Length > 0)
            {
                cell = new PdfPCell(new Phrase(Tabela2[Tabela2.Length - 1], standard));
                cell.HorizontalAlignment = 0;
                cell.Border = Rectangle.LEFT_BORDER | Rectangle.BOTTOM_BORDER;
                table.AddCell(cell);

                /*
                if (Select2 == "0")
                {
                    cell = new PdfPCell(new Phrase(Convert.ToString(bazaTabela2_ilosc[Tabela2.Length - 1]) + " Stk.", standard));
                }
                else if (Select2 == "1")
                {
                    cell = new PdfPCell(new Phrase(Convert.ToString(bazaTabela2_ilosc[Tabela2.Length - 1]) + " Lfm.", standard));
                }
                else if (Select2 == "2")
                {
                    cell = new PdfPCell(new Phrase(Convert.ToString(bazaTabela2_ilosc[Tabela2.Length - 1]) + " m²", standard));
                }
                */
                cell = new PdfPCell(new Phrase(Convert.ToString(bazaTabela2_ilosc[Tabela2.Length - 1]) + " " + bazaTabela2_jedn[Tabela2.Length - 1], standard));
                cell.HorizontalAlignment = 0;
                cell.Border = Rectangle.BOTTOM_BORDER;
                table.AddCell(cell);

                string cena = String.Format("{0:0.00}", Convert.ToDouble(bazaTabela2_cena[Tabela2.Length - 1])).Replace(".", ",");
                cell = new PdfPCell(new Phrase(cena + " €", standard));
                cell.HorizontalAlignment = 2;
                cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER;
                table.AddCell(cell);

                table.TotalWidth = doc.Right - doc.Left;
                table.LockedWidth = true;
                table.SetWidths(new float[] { 3.034f, 0.239f, 0.429f });
            }
            else
            {
                cell = new PdfPCell(new Phrase("-", standard));
                cell.HorizontalAlignment = 0;
                cell.Border = Rectangle.LEFT_BORDER | Rectangle.BOTTOM_BORDER;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase(" ", standard));
                cell.HorizontalAlignment = 0;
                cell.Border = Rectangle.BOTTOM_BORDER;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase(" ", standard));
                cell.HorizontalAlignment = 2;
                cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER;
                table.AddCell(cell);

                table.TotalWidth = doc.Right - doc.Left;
                table.LockedWidth = true;
                table.SetWidths(new float[] { 3.034f, 0.239f, 0.429f });
            }


            doc.Add(table);





            par = new Paragraph(new Phrase("Alle Preise verstehen sich zzgl. 19% MwSt.", standard));
            par.SpacingBefore = 4f;
            par.SpacingAfter = 11f;
            doc.Add(par);

            if(bazaTabela1.Length + bazaTabela2.Length > 18)
            {
                par = new Paragraph(new Phrase("Zahlung bei Kauf:", standard_bold));
                par.SpacingAfter = -2f;
                doc.Add(par);
                par = new Paragraph(new Phrase("Mit Montage: 30% vom Auftragswert bei Bestellung, 60% bei Anlieferung, vor Abladung,", standard));
                par.SpacingAfter = -2f;
                doc.Add(par);
                par = new Paragraph(new Phrase("3) Restzahlung - 10% bei Abnahme / Fertgstellung.", standard));
                par.SpacingAfter = 11f;
                doc.Add(par);
            }
            else
            {
                par = new Paragraph(new Phrase("Zahlung bei Kauf:", standard_bold));
                par.SpacingAfter = -2f;
                doc.Add(par);
                par = new Paragraph(new Phrase("Mit Montage:", standard));
                par.SpacingAfter = -2f;
                doc.Add(par);
                par = new Paragraph(new Phrase("1) 30% vom Auftragswert bei Bestellung,", standard));
                par.SpacingAfter = -2f;
                doc.Add(par);
                par = new Paragraph(new Phrase("2) 60% bei Anlieferung, vor Abladung,", standard));
                par.SpacingAfter = -2f;
                doc.Add(par);
                par = new Paragraph(new Phrase("3) Restzahlung - 10% bei Abnahme / Fertgstellung.", standard));
                par.SpacingAfter = 11f;
                doc.Add(par);
            }





            if(bazaTabela1.Length + bazaTabela2.Length > 15)
            {
                par = new Paragraph(new Phrase("Lieferzeit:", standard_bold));
                par.SpacingAfter = -2f;
                doc.Add(par);
                par = new Paragraph(new Phrase("ca. " + Oferta[0] + " nach Auftragserteilung / Anzahlungseingang Das Angebot ist 14 Tage gültig.", standard));
                par.SpacingAfter = 11f;
                doc.Add(par);
            }
            else
            {
                par = new Paragraph(new Phrase("Lieferzeit:", standard_bold));
                par.SpacingAfter = -2f;
                doc.Add(par);
                par = new Paragraph(new Phrase("ca. " + Oferta[0] + " nach Auftragserteilung / Anzahlungseingang", standard));
                par.SpacingAfter = 11f;
                doc.Add(par);

                par = new Paragraph(new Phrase("Das Angebot ist 14 Tage gültig.", standard));
                doc.Add(par);
            }




            table = new PdfPTable(3);
            cell = new PdfPCell();
            cell.Rowspan = 4;
            cell.UseAscender = true;
            cell.VerticalAlignment = Element.ALIGN_MIDDLE;

            phrase = new Phrase();
            phrase.Add(new Chunk("itc", logo2_small));
            phrase.Add(new Chunk("metalcon", logo1_small));
            phrase.Add(new Chunk(".\n", logo2_small));
            par = new Paragraph(phrase);
            par.SpacingAfter = -4f;
            par.Alignment = 0;
            cell.AddElement(par);

            par = new Paragraph(new Phrase("Normannenstrasse 2", small));
            par.SpacingAfter = -4f;
            par.Alignment = 0;
            cell.AddElement(par);

            par = new Paragraph(new Phrase("71263 Weil der Stadt", small));
            par.SpacingAfter = -4f;
            par.Alignment = 0;
            cell.AddElement(par);

            par = new Paragraph(new Phrase("Tel.: (+49) 162 4230214", small));
            par.SpacingAfter = -4f;
            par.Alignment = 0;
            cell.AddElement(par);

            par = new Paragraph(new Phrase("Fax.: (+49) 7033 467017 \n ", small));
            par.SpacingAfter = -4f;
            par.Alignment = 0;
            cell.AddElement(par);

            Font small_special = new Font(verdana, 4f, Font.NORMAL, BaseColor.BLACK);
            par = new Paragraph(new Phrase(" ", small));
            par.SpacingAfter = -0.5f;
            par.Alignment = 0;
            cell.AddElement(par);

            cell.BackgroundColor = new BaseColor(238, 238, 238);
            cell.Border = Rectangle.NO_BORDER;

            table.AddCell(cell);


            cell = new PdfPCell();
            cell.Rowspan = 4;
            cell.UseAscender = true;
            cell.VerticalAlignment = Element.ALIGN_TOP;

            //par = new Paragraph(new Phrase(" ", small));
            //par.SpacingAfter = -4f;
            //par.Alignment = 0;
            // cell.AddElement(par);

            phrase = new Phrase();
            phrase.Add(new Chunk(" \n", logo2_small));
            par = new Paragraph(phrase);
            par.SpacingAfter = -4f;
            par.Alignment = 0;
            cell.AddElement(par);

            par = new Paragraph(new Phrase("Konto: 4530164301", small));
            par.SpacingAfter = -4f;
            par.Alignment = 0;
            cell.AddElement(par);

            par = new Paragraph(new Phrase("BLZ: 85591000", small));
            par.SpacingAfter = -4f;
            par.Alignment = 0;
            cell.AddElement(par);

            par = new Paragraph(new Phrase("IBAN: DE55855910004530164301", small));
            par.SpacingAfter = -4f;
            par.Alignment = 0;
            cell.AddElement(par);

            par = new Paragraph(new Phrase("BIC: GENODEF1GR1", small));
            par.SpacingAfter = -4f;
            par.Alignment = 0;
            cell.AddElement(par);

            cell.BackgroundColor = new BaseColor(238,238,238);
            cell.Border = Rectangle.NO_BORDER;

            table.AddCell(cell);


            cell = new PdfPCell();
            cell.Rowspan = 5;
            cell.UseAscender = true;
            cell.VerticalAlignment = Element.ALIGN_TOP;

            //par = new Paragraph(new Phrase(" ", small));
            //par.SpacingAfter = -4f;
            //par.Alignment = 0;
           // cell.AddElement(par);

            phrase = new Phrase();
            phrase.Add(new Chunk(" \n", logo2_small));
            par = new Paragraph(phrase);
            par.SpacingAfter = -4f;
            par.Alignment = 0;
            cell.AddElement(par);

            par = new Paragraph(new Phrase("info@itcmetalcon.de", small));
            par.SpacingAfter = -4f;
            par.Alignment = 0;
            cell.AddElement(par);

            par = new Paragraph(new Phrase("www.itcmetalcon.de", small));
            par.SpacingAfter = -4f;
            par.Alignment = 0;
            cell.AddElement(par);

            par = new Paragraph(new Phrase("Geshäftsführer / Inhaber: Pawel Swiderski", small));
            par.SpacingAfter = -4f;
            par.Alignment = 0;
            cell.AddElement(par);

            par = new Paragraph(new Phrase(" ", small));
            par.SpacingAfter = -4f;
            par.Alignment = 0;
            cell.AddElement(par);

            par = new Paragraph(new Phrase("USt. ID - Nummer: DE 275947975", small));
            par.SpacingAfter = -4f;
            par.Alignment = 0;
            cell.AddElement(par);

            par = new Paragraph(new Phrase("Steuernummer: 70418/72302", small));
            par.SpacingAfter = -4f;
            par.Alignment = 0;
            cell.AddElement(par);

            cell.BackgroundColor = new BaseColor(238, 238, 238);
            cell.Border = Rectangle.NO_BORDER;

            table.AddCell(cell);

            table.TotalWidth = doc.Right - doc.Left;
            table.LockedWidth = true;
            PdfContentByte pcb = writer.DirectContent;
            table.WriteSelectedRows(0, -1, doc.Left, doc.Bottom+50, pcb);









            doc.NewPage();

            phrase = new Phrase();
            phrase.Add(new Chunk("itc", logo2));
            phrase.Add(new Chunk("metalcon", logo1));
            phrase.Add(new Chunk(".\n ", logo2));
            par = new Paragraph();
            par.Add(phrase);
            par.Alignment = 2;
            doc.Add(par);


            par = new Paragraph(new Phrase("Technische Daten:", standard_bold));
            par.SpacingAfter = 0f;
            par.Alignment = 0;
            doc.Add(par);

            string[] typ_pom = Halledaten[0].Split(" ");
            string[] typ = new string[2];
            if(typ_pom[1].Substring(0,2) == "TT" || typ_pom[1].Substring(0, 2) == "PT" || typ_pom[1].Substring(0, 2) == "PP")
            {
                typ[1] = typ_pom[1];
            }
            else if(typ_pom[1].Substring(0, 3) == "ISO" || typ_pom[1].Substring(0, 3) == "DPS")
            {
                typ[1] = typ_pom[1];
            }
            else if(typ_pom.Length > 2)
            {
                if (typ_pom[2].Substring(0, 2) == "TT" || typ_pom[2].Substring(0, 2) == "PT" || typ_pom[2].Substring(0, 2) == "PP")
                {
                    typ[1] = typ_pom[2];
                }
                else if (typ_pom[2].Substring(0, 3) == "ISO" || typ_pom[2].Substring(0, 3) == "DPS")
                {
                    typ[1] = typ_pom[2];
                }
            }

            string opis1 = "";
            string opis2 = "";
            string opis3 = "";
            if(typ[1].Substring(0,2) == "TT")
            {
                //opis1 = "Konstruktion aus Aluminiumkastenprofil mit Zugbändern, mit First- und Eckverbindungen aus feuerverzinktem Stahl. Die feuerverzinkten Fußplatten sind entsprechend den statischen Erfordernissen mittels Erdnägeln (1,00 m lang) auf nicht bindigen, dicht gelagerten Boden evtl. Schwerlastdübeln auf Fundamenten verankert.";
                opis1 = TechnischeDaten;
                opis2 = "Dacheindeckung und Giebeldreieck: PVC- beschichtete Gewebe hoch-glanz-lackiert, Gewicht " + Technische[4] + " g/m², schwerentflammbar gemäß DIN4102/B1. Es ist in den 4-Kedernut Alu-Profil eingezogen. Das PVC- Material in Standardfarbe Weiß - es kann nach Absprache und evtl. gegen Aufpreis in anderen Farben geliefert werden.";
                opis3 = "Zur Isolierung und Kondenswasservermeidung kann im Dachbereich (von Traufe bis Traufe) gegen Aufpreis eine lichtdurchlässige Innenplane montiert werden.";
            }
            else if(typ[1].Substring(0, 2) == "PT")
            {
                //opis1 = "Konstruktion aus Aluminiumkastenprofil mit Zugbändern, mit First- und Eckverbindungen aus feuerverzinktem Stahl. Die feuerverzinkten Fußplatten sind entsprechend den statischen Erfordernissen mittels Erdnägeln (1,00 m lang) auf nicht bindigen, dicht gelagerten Boden (DIN-EN 13782) evtl. Schwerlastdübeln auf Fundamenten (DIN-EN 1991) verankert.";
                opis1 = TechnischeDaten;
                opis2 = "Dacheindeckung und Giebeldreieck: PVC- beschichtete Gewebe hoch-glanz-lackiert, Gewicht " + Technische[4] + " g/m², schwerentflammbar gemäß DIN4102/B1. Es ist in den 4-Kedernut Alu-Profil eingezogen. Das PVC- Material in Standardfarbe Weiß - es kann nach Absprache und evtl. gegen Aufpreis in anderen Farben geliefert werden.";
                opis3 = "Zur Isolierung und Kondenswasservermeidung kann im Dachbereich (von Traufe bis Traufe) gegen Aufpreis eine lichtdurchlässige Innenplane montiert werden.";
            }
            else if(typ[1].Substring(0, 2) == "PP")
            {
                opis1 = TechnischeDaten;
                opis2 = "Dacheindeckung und Giebeldreieck: PVC- beschichtete Gewebe hoch-glanz-lackiert, Gewicht " + Technische[4] + " g/m², schwerentflammbar gemäß DIN4102/B1. Es ist in den 4-Kedernut Alu-Profil eingezogen. Das PVC- Material in Standardfarbe Weiß - es kann nach Absprache und evtl. gegen Aufpreis in anderen Farben geliefert werden.";
                opis3 = "Zur Isolierung und Kondenswasservermeidung kann im Dachbereich (von Traufe bis Traufe) gegen Aufpreis eine lichtdurchlässige Innenplane montiert werden.";
            }
            else if (typ[1].Substring(0, 3) == "ISO")
            {
                //opis1 = "Konstruktion aus Doppel-T-Träger IPE240 (Riegel) und HEA 180 (Ständer), ohne Zugbändern (Flacheisen). Baustahl 235, feuerverzinkt nach DIN-EN ISO 1461. Die Fußplattern sind entsprechend den statischen Erfordernissen mittles Schwerlastdübeln auf Fundementen verankert.";
                opis1 = TechnischeDaten;
                opis2 = "Sandwichpaneelen, Stärke 40mm, chwerentflammbar nach DIN 4102 Baustoffklasse 1, U-Wert";
                opis3 = "";
            }
            else if (typ[1].Substring(0, 3) == "DPS")
            {
                //opis1 = "Konstruktion aus Aluminiumkastenprofil mit Zugbändern, mit First- und Eckverbindungen aus feuerverzinktem Stahl. Die feuerverzinkten Fußplatten sind entsprechend den statischen Erfordernissen mittels Erdnägeln (1,00 m lang) auf nicht bindigen, dicht gelagerten Boden (DIN-EN 13782) evtl. Schwerlastdübeln auf Fundamenten (DIN-EN 1991) verankert.";
                opis1 = TechnischeDaten;
            }

            par = new Paragraph(new Phrase(opis1, standard));
            par.SpacingAfter = -4f;
            par.SetLeading(10f, 0f);
            par.Alignment = 0;
            doc.Add(par);
            Console.WriteLine(typ[1]);
            Console.WriteLine("opis|"+ opis1);
            Console.WriteLine("tech|" + TechnischeDaten);
            par = new Paragraph(new Phrase(" ", standard));
            par.SpacingAfter = 0f;
            par.Alignment = 0;
            doc.Add(par);

            /*
            if (typ[1].Substring(0, 3) == "DPS")
            {
                phrase = new Phrase(new Chunk("Dacheindeckung und Giebeldreieck: ", standard_bold));
                phrase.Add(new Chunk("doppelschalige PVC- beschichtete Gewebe hoch-glanz-lackiert, Gewicht " + Technische[4] + " g/m², schwerentflammbar gemäß DIN4102/B1, K-Wert " + Technische[5] + " W/m²K, Druckregler mit Kompressor (1 Stk.).", standard));
                phrase.Add(new Chunk("Farbton Weiß.", standard_bold));
                par = new Paragraph(phrase);
            }
            else if(typ[1].Substring(0, 3) == "ISO")
            {
                phrase = new Phrase(new Chunk("Dacheindeckung: ", standard_bold));
                phrase.Add(new Chunk("Sandwichpaneelen, ",standard));
                phrase.Add(new Chunk(Technische[0] + "mm, ", standard_bold));
                phrase.Add(new Chunk("chwerentflammbar nach DIN 4102 Baustoffklasse 1, ", standard));
                phrase.Add(new Chunk("U-Wert (W/m²K) = " + Technische[1] + ". ", standard_bold));
                phrase.Add(new Chunk("Die Panellen bestehen aus einem Kern aus Hartschaum zwischen verzinkten und kunststoffbeschichtetem Syahlblech- Deckschalen.", standard));
                par = new Paragraph(phrase);
            }
            else if(typ[1].Substring(0, 2) == "PT")
            {
                phrase = new Phrase(new Chunk("Dacheindeckung und Giebeldreieck: ", standard_bold));
                phrase.Add(new Chunk(opis2, standard));
            }
            else
            {
                par = new Paragraph(new Phrase(opis2, standard));
            }
            */
            if(typ[1].Substring(0, 2) == "PT")
            {
                phrase = new Phrase(new Chunk("Dacheindeckung und Giebeldreieck: ", standard_bold));
                phrase.Add(new Chunk("PVC- beschichtete Gewebe hoch-glanz-lackiert, Gewicht " + Technische[4] + " g/m², schwerentflammbar gemäß DIN4102/B1. Es ist in den 4-Kedernut Alu-Profil eingezogen. Das PVC- Material in Standardfarbe Weiß - es kann nach Absprache und evtl. gegen Aufpreis in anderen Farben geliefert werden.", standard));
                par = new Paragraph(phrase);

                par.SpacingAfter = -4f;
                par.SetLeading(10f, 0f);
                par.Alignment = 0;
                doc.Add(par);

                par = new Paragraph(new Phrase(" ", standard));
                par.SpacingAfter = 0f;
                par.Alignment = 0;
                doc.Add(par);
            }
            else if(typ[1].Substring(0, 2) == "TT")
            {
                phrase = new Phrase(new Chunk("Dacheindeckung: ", standard_bold));
                phrase.Add(new Chunk("bestehend aus verzinkten und kunststoffbeschichteten Stahl-Trapezblechen T35 / T18, Stärke " + Technische[0] + " mm.", standard));
                par = new Paragraph(phrase);

                par.SpacingAfter = -4f;
                par.SetLeading(10f, 0f);
                par.Alignment = 0;
                doc.Add(par);

                par = new Paragraph(new Phrase(" ", standard));
                par.SpacingAfter = 0f;
                par.Alignment = 0;
                doc.Add(par);
            }
            else if (typ[1].Substring(0, 2) == "PP")
            {
                phrase = new Phrase(new Chunk("Dacheindeckung und Wandverkleidung: ", standard_bold));
                phrase.Add(new Chunk("PVC- beschichtete Gewebe hoch-glanz-lackiert, Gewicht " + Technische[4] + " g/m2, schwerentflammbar gemäß DIN4102/B1. Es ist in den 4-Kedernut Alu-Profil eingezogen. Das PVC- Material in Standardfarbe Weiß oder Grün - es kann nach Absprache in anderen Farben geliefert werden.", standard));
                par = new Paragraph(phrase);

                par.SpacingAfter = -4f;
                par.SetLeading(10f, 0f);
                par.Alignment = 0;
                doc.Add(par);

                par = new Paragraph(new Phrase(" ", standard));
                par.SpacingAfter = 0f;
                par.Alignment = 0;
                doc.Add(par);
            }
            else if (typ[1].Substring(0, 3) == "DPS")
            {
                phrase = new Phrase(new Chunk("Dacheindeckung und Giebeldreieck: ", standard_bold));
                phrase.Add(new Chunk("doppelschalige PVC- beschichtete Gewebe hoch-glanz-lackiert, Gewicht " + Technische[4] + " g/m², schwerentflammbar gemäß DIN4102/B1, K-Wert " + Technische[5] + " W/m²K, Druckregler mit Kompressor (1 Stk.).", standard));
                phrase.Add(new Chunk(" Farbton Weiß.", standard_bold));
                par = new Paragraph(phrase);

                par.SpacingAfter = -4f;
                par.SetLeading(10f, 0f);
                par.Alignment = 0;
                doc.Add(par);

                par = new Paragraph(new Phrase(" ", standard));
                par.SpacingAfter = 0f;
                par.Alignment = 0;
                doc.Add(par);


            }
            else if(Select3 == 1)
            {
                phrase = new Phrase(new Chunk("Dacheindeckung: ", standard_bold));
                phrase.Add(new Chunk("Sandwichpaneelen (profiliert), ", standard));
                phrase.Add(new Chunk("Stärke " + Technische[0] + " mm, ", standard_bold));
                phrase.Add(new Chunk("schwerentflammbar nach DIN 4102 Baustoffklasse 1, ", standard));
                phrase.Add(new Chunk("U-Wert (W/m²K) = " + Technische[1] + ". ", standard_bold));
                phrase.Add(new Chunk("Die Panellen bestehen aus einem Kern aus Hartschaum zwischen verzinkten und kunststoffbeschichtetem Stahlblech- Deckschalen.", standard));
                par = new Paragraph(phrase);

                par.SpacingAfter = -4f;
                par.SetLeading(10f, 0f);
                par.Alignment = 0;
                doc.Add(par);

                par = new Paragraph(new Phrase(" ", standard));
                par.SpacingAfter = 0f;
                par.Alignment = 0;
                doc.Add(par);
            }


            /*
            if(typ[1].Substring(0, 3) == "DPS")
            {
                phrase = new Phrase(new Chunk("Wandverkleidung: ", standard_bold));
                phrase.Add(new Chunk("Sandwichpaneelen, ", standard));
                phrase.Add(new Chunk("Stärke " + Technische[2] + " mm, ", standard_bold));
                phrase.Add(new Chunk("chwerentflammbar nach DIN 4102 Baustoffklasse 1, ", standard));
                phrase.Add(new Chunk("U-Wert (W/m²K) = " + Technische[3] + ". ", standard_bold));
                phrase.Add(new Chunk("Die Paneelen bestehen aus einem Kern aus Hartschaum zwischen verzinkten und kunststoffbeschichtetem Stahlblech- Deckschalen, mit sichtbarer Befestigung. Die Verkleidung schließt mit einem Abschluss aus Kantblechen am Boden ab.", standard));
                par = new Paragraph(phrase);

                par.SpacingAfter = -4f;
                par.SetLeading(10f, 0f);
                par.Alignment = 0;
                doc.Add(par);

                par = new Paragraph(new Phrase(" ", standard));
                par.SpacingAfter = 0f;
                par.Alignment = 0;
                doc.Add(par);
            }

            else if (typ[1].Substring(0,3) == "ISO")
            {
                phrase = new Phrase(new Chunk("Wandverkleidung: ", standard_bold));
                phrase.Add(new Chunk("Sandwichpaneelen, ", standard));
                phrase.Add(new Chunk("Stärke " + Technische[2] + " mm, ", standard_bold));
                phrase.Add(new Chunk("chwerentflammbar nach DIN 4102 Baustoffklasse 1, ", standard));
                phrase.Add(new Chunk("U-Wert (W/m²K) = " + Technische[3] + ". ", standard_bold));
                phrase.Add(new Chunk("Die Panellen bestehen aus einem Kern aus Hartschaum zwischen verzinkten und kunststoffbeschichtetem Stahlblech- Deckschalen, mit sichtbarer Befestigung. Die Verkleidung schließt mit einem Abschluss aus Kantblechen am Boden ab.", standard));
                par = new Paragraph(phrase);
            }
            else
            {
                par = new Paragraph(new Phrase(opis3, standard_lightblue));
            }
            */



            if (typ[1].Substring(0, 2) == "PT")
            {
                phrase = new Phrase(new Chunk("Wandverkleidung ", standard_bold));
                phrase.Add(new Chunk("bestehend aus waagerecht (evtl. senkrecht) verlegten, verzinkten und kunststoffbeschichteten Stahl-Trapezblechen T35 / T18, Stärke " + Technische[2] + " mm.", standard));
                par = new Paragraph(phrase);

                par.SpacingAfter = -4f;
                par.SetLeading(10f, 0f);
                par.Alignment = 0;
                doc.Add(par);

                par = new Paragraph(new Phrase(" ", standard));
                par.SpacingAfter = 0f;
                par.Alignment = 0;
                doc.Add(par);
            }
            else if (typ[1].Substring(0, 2) == "PP")
            {
                phrase = new Phrase(new Chunk("Wandverkleidung ", standard_bold));
                phrase.Add(new Chunk("bestehend aus waagerecht (evtl. senkrecht) verlegten, verzinkten und kunststoffbeschichteten Stahl-Trapezblechen T35 / T18, Stärke " + Technische[2] + " mm.", standard));
                par = new Paragraph(phrase);

                par.SpacingAfter = -4f;
                par.SetLeading(10f, 0f);
                par.Alignment = 0;
                doc.Add(par);

                par = new Paragraph(new Phrase(" ", standard));
                par.SpacingAfter = 0f;
                par.Alignment = 0;
                doc.Add(par);
            }
            else if (typ[1].Substring(0, 2) == "TT")
            {
                phrase = new Phrase(new Chunk("Wandverkleidung ", standard_bold));
                phrase.Add(new Chunk("bestehend aus waagerecht verlegten, verzinkten und kunststoffbeschichteten Stahl-Trapezblechen T35 / T18, Stärke " + Technische[2] + " mm.", standard));
                par = new Paragraph(phrase);

                par.SpacingAfter = -4f;
                par.SetLeading(10f, 0f);
                par.Alignment = 0;
                doc.Add(par);

                par = new Paragraph(new Phrase(" ", standard));
                par.SpacingAfter = 0f;
                par.Alignment = 0;
                doc.Add(par);
            }
            else if(typ[1].Substring(0, 3) == "DPS")
            {
                phrase = new Phrase(new Chunk("Wandverkleidung: ", standard_bold));
                phrase.Add(new Chunk("Sandwichpaneelen (profiliert), ", standard));
                phrase.Add(new Chunk("Stärke " + Technische[2] + " mm, ", standard_bold));
                phrase.Add(new Chunk("schwerentflammbar nach DIN 4102 Baustoffklasse 1, ", standard));
                phrase.Add(new Chunk("U-Wert (W/m²K) = " + Technische[3] + ". ", standard_bold));
                phrase.Add(new Chunk("Die Panellen bestehen aus einem Kern aus Hartschaum zwischen verzinkten und kunststoffbeschichtetem Stahlblech- Deckschalen. Die Verkleidung schließt mit einem Abschluss aus Kantblechen + Dichtband am Boden ab.", standard));
                par = new Paragraph(phrase);

                par.SpacingAfter = -4f;
                par.SetLeading(10f, 0f);
                par.Alignment = 0;
                doc.Add(par);

                par = new Paragraph(new Phrase(" ", standard));
                par.SpacingAfter = 0f;
                par.Alignment = 0;
                doc.Add(par);
            }
            else if(Select3 == 1)
            {
                phrase = new Phrase(new Chunk("Wandverkleidung: ", standard_bold));
                phrase.Add(new Chunk("Sandwichpaneelen (profiliert), ", standard));
                phrase.Add(new Chunk("Stärke " + Technische[2] + " mm, ", standard_bold));
                phrase.Add(new Chunk("schwerentflammbar nach DIN 4102 Baustoffklasse 1, ", standard));
                phrase.Add(new Chunk("U-Wert (W/m²K) = " + Technische[3] + ". ", standard_bold));
                phrase.Add(new Chunk("Die Panellen bestehen aus einem Kern aus Hartschaum zwischen verzinkten und kunststoffbeschichtetem Stahlblech- Deckschalen. Die Verkleidung schließt mit einem Abschluss aus Kantblechen + Dichtband am Boden ab.", standard));
                par = new Paragraph(phrase);

                par.SpacingAfter = -4f;
                par.SetLeading(10f, 0f);
                par.Alignment = 0;
                doc.Add(par);

                par = new Paragraph(new Phrase(" ", standard));
                par.SpacingAfter = 0f;
                par.Alignment = 0;
                doc.Add(par);
            }

            if (typ[1].Substring(0, 2) == "PT")
            {

                phrase = new Phrase(new Chunk("Zur Isolierung und Kondenswasservermeidung kann im Dachbereich (von Traufe bis Traufe) gegen Aufpreis eine lichtdurchlässige Innenplane montiert werden.", standard_lightblue));
                par = new Paragraph(phrase);
                par.SpacingAfter = -4f;
                par.SetLeading(10f, 0f);
                par.Alignment = 0;
                doc.Add(par);

                par = new Paragraph(new Phrase(" ", standard));
                par.SpacingAfter = 0f;
                par.Alignment = 0;
                doc.Add(par);
            }
            else if (typ[1].Substring(0, 2) == "TT")
            {

                phrase = new Phrase(new Chunk("Zur Kondenswasservermeidung kann im inneren Dachbereich (gegen Aufpreis) das Antikondensvlies aufgetragen werden.", standard_lightblue));
                par = new Paragraph(phrase);
                par.SpacingAfter = -4f;
                par.SetLeading(10f, 0f);
                par.Alignment = 0;
                doc.Add(par);

                par = new Paragraph(new Phrase(" ", standard));
                par.SpacingAfter = 0f;
                par.Alignment = 0;
                doc.Add(par);
            }
            if (typ[1].Substring(0, 2) == "PP")
            {

                phrase = new Phrase(new Chunk("Zur Isolierung und Kondenswasservermeidung kann im Dachbereich (von Traufe bis Traufe) gegen Aufpreis eine lichtdurchlässige Innenplane montiert werden.", standard_lightblue));
                par = new Paragraph(phrase);
                par.SpacingAfter = -4f;
                par.SetLeading(10f, 0f);
                par.Alignment = 0;
                doc.Add(par);

                par = new Paragraph(new Phrase(" ", standard));
                par.SpacingAfter = 0f;
                par.Alignment = 0;
                doc.Add(par);
            }
            /*
            if(typ[1].Substring(0, 3) == "DPS" || typ[1].Substring(0, 3) == "ISO")
            {
                par = new Paragraph(new Phrase(" ", standard));
                par.SpacingAfter = 0f;
                par.Alignment = 0;
                doc.Add(par);

                if (typ[1].Substring(0, 3) == "DPS")
                {
                    phrase = new Phrase(new Chunk("RAL- Farben:", standard_bold));
                    phrase.Add(new Chunk("3000, 3009, 3011, 3016, 5010, 6005, 6011, 6020, 7016, 7024, 7035, 8004, 8012, 8017, 8019, 9002, 9005, 9006, 9007, 9010, 1003", standard));
                    par = new Paragraph(phrase);
                }
                else if(typ[1].Substring(0, 3) == "ISO")
                {
                    phrase = new Phrase(new Chunk("RAL- Farben:", standard_bold));
                    phrase.Add(new Chunk("3000, 3009, 3011, 3016, 5010, 6005, 6011, 6020, 7016, 7024, 7035, 8004, 8012, 8017, 8019, 9002, 9005, 9006, 9007, 9010, 1003", standard));
                    par = new Paragraph(phrase);
                }

                par.SpacingAfter = -4f;
                par.SetLeading(10f, 0f);
                par.Alignment = 0;
                doc.Add(par);
            }
            */


            if (Select3 == 0)
            {
                phrase = new Phrase(new Chunk("RAL- Farben der Trapezblechen (Standard):\n", standard_bold));
                phrase.Add(new Chunk("9002, 9006, 9007, 9010, 3000, 3005, 3009, 3011, 6005, 6011, 6019, 6020, 6029, 7016, 7024, 7035, 1021, 5010, 8004, 8017, 8019, 9005", standard));
                par = new Paragraph(phrase);
            }
            else if(Select3 == 1)
            {
                phrase = new Phrase(new Chunk("RAL- Farben der Sandwichpaneelen:\n", standard_bold));
                phrase.Add(new Chunk("3000, 3009, 3011, 3016, 5010, 6005, 6011, 6020, 7016, 7024, 7035, 8004, 8012, 8017, 8019, 9002, 9005, 9006, 9007, 9010, 1003", standard));
                par = new Paragraph(phrase);
            }
            par.SpacingAfter = -4f;
            par.SetLeading(10f, 0f);
            par.Alignment = 0;
            doc.Add(par);

            par = new Paragraph(new Phrase(" ", standard));
            par.SpacingAfter = 0f;
            par.Alignment = 0;
            doc.Add(par);

            phrase = new Phrase(new Chunk("Tore und Türen:\n", standard_bold));
            phrase.Add(new Chunk(ToreUndTuren, standard));
            par = new Paragraph(phrase);
            par.SpacingAfter = -4f;
            par.SetLeading(10f, 0f);
            par.Alignment = 0;
            doc.Add(par);

            par = new Paragraph(new Phrase(" ", standard));
            par.SpacingAfter = 0f;
            par.Alignment = 0;
            doc.Add(par);

            phrase = new Phrase(new Chunk("Unterlagen:\n", standard_bold));
            //phrase.Add(new Chunk("Eine prüffähige Statik nach Eurocode (EC) DIN-EN 13782 / DIN-EN 1991 und Konstruktionspläne gehören zu unserem Lieferumfang. Sie bekommen die Konstruktionspläne kostenlos vor verbindlicher Bestellung.", standard));
            phrase.Add(new Chunk(Unterlagen, standard));
            par = new Paragraph(phrase);
            par.SpacingAfter = -4f;
            par.SetLeading(10f, 0f);
            par.Alignment = 0;
            doc.Add(par);

            par = new Paragraph(new Phrase(" ", standard));
            par.SpacingAfter = 0f;
            par.Alignment = 0;
            doc.Add(par);

            phrase = new Phrase(new Chunk(AufWunsch, standard));
            par = new Paragraph(phrase);
            par.SpacingAfter = -4f;
            par.SetLeading(10f, 0f);
            par.Alignment = 0;
            doc.Add(par);

            par = new Paragraph(new Phrase(" ", standard));
            par.SpacingAfter = 0f;
            par.Alignment = 0;
            doc.Add(par);

            phrase = new Phrase(new Chunk("Garantie:\n", standard_bold));
            phrase.Add(new Chunk("24 Montage itcmetalcon- Herstellergarantie.", standard));
            par = new Paragraph(phrase);
            par.SpacingAfter = -4f;
            par.SetLeading(10f, 0f);
            par.Alignment = 0;
            doc.Add(par);

            par = new Paragraph(new Phrase(" ", standard));
            par.SpacingAfter = 0f;
            par.Alignment = 0;
            doc.Add(par);

            phrase = new Phrase(new Chunk("Montage:\n", standard_blue));
            //phrase.Add(new Chunk(Montage + "\n ", standard_lightblue)); 
            if(bazaTabela3.Length > 0)
            {
                if (bazaTabela3.Length > 0)
                {
                    phrase.Add(new Chunk(bazaTabela3[0] + "\n", standard_lightblue));
                    if(bazaTabela3.Length > 1)
                    {
                        int i = 1;
                        do
                        {
                            phrase.Add(new Chunk("-" + bazaTabela3[i] + ",\n", standard_lightblue));
                            i++;
                        } while (i < bazaTabela3.Length - 1);
                    }
                }
                phrase.Add(new Chunk("-" + bazaTabela3[bazaTabela3.Length - 1] + ".", standard_lightblue));
            }
            else
            {
                phrase.Add(new Chunk("-", standard_lightblue));
            }
            par = new Paragraph(phrase);
            par.SpacingAfter = -4f;
            par.SetLeading(10f, 0f);
            par.Alignment = 0;
            doc.Add(par);

            par = new Paragraph(new Phrase(" ", standard));
            par.SpacingAfter = 0f;
            par.Alignment = 0;
            doc.Add(par);

            phrase = new Phrase(new Chunk("Der Auftraggeber veranlasst die Hotelbuchung (Monteurzimmer, Ferienwohnung) für unsere Monteure zu seinen Lasten.\n ", standard_lightblue));
            par = new Paragraph(phrase);
            par.SpacingAfter = -4f;
            par.SetLeading(10f, 0f);
            par.Alignment = 0;
            doc.Add(par);

            par = new Paragraph(new Phrase(" ", standard));
            par.SpacingAfter = 0f;
            par.Alignment = 0;
            doc.Add(par);

            phrase = new Phrase(new Chunk("Bei Fragen stehe ich Ihnen gerne zuer Verfügang.\n ", standard));
            par = new Paragraph(phrase);
            par.SpacingAfter = -4f;
            par.SetLeading(10f, 0f);
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




            table = new PdfPTable(3);
            cell = new PdfPCell();
            cell.Rowspan = 4;
            cell.UseAscender = true;
            cell.VerticalAlignment = Element.ALIGN_MIDDLE;

            phrase = new Phrase();
            phrase.Add(new Chunk("itc", logo2_small));
            phrase.Add(new Chunk("metalcon", logo1_small));
            phrase.Add(new Chunk(".\n", logo2_small));
            par = new Paragraph(phrase);
            par.SpacingAfter = -4f;
            par.Alignment = 0;
            cell.AddElement(par);

            par = new Paragraph(new Phrase("Normannenstrasse 2", small));
            par.SpacingAfter = -4f;
            par.Alignment = 0;
            cell.AddElement(par);

            par = new Paragraph(new Phrase("71263 Weil der Stadt", small));
            par.SpacingAfter = -4f;
            par.Alignment = 0;
            cell.AddElement(par);

            par = new Paragraph(new Phrase("Tel.: (+49) 162 4230214", small));
            par.SpacingAfter = -4f;
            par.Alignment = 0;
            cell.AddElement(par);

            par = new Paragraph(new Phrase("Fax.: (+49) 7033 467017 \n ", small));
            par.SpacingAfter = -4f;
            par.Alignment = 0;
            cell.AddElement(par);

            par = new Paragraph(new Phrase(" ", small));
            par.SpacingAfter = -0.5f;
            par.Alignment = 0;
            cell.AddElement(par);

            cell.BackgroundColor = new BaseColor(238, 238, 238);
            cell.Border = Rectangle.NO_BORDER;

            table.AddCell(cell);


            cell = new PdfPCell();
            cell.Rowspan = 4;
            cell.UseAscender = true;
            cell.VerticalAlignment = Element.ALIGN_TOP;

            //par = new Paragraph(new Phrase(" ", small));
            //par.SpacingAfter = -4f;
            //par.Alignment = 0;
            // cell.AddElement(par);

            phrase = new Phrase();
            phrase.Add(new Chunk(" \n", logo2_small));
            par = new Paragraph(phrase);
            par.SpacingAfter = -4f;
            par.Alignment = 0;
            cell.AddElement(par);

            par = new Paragraph(new Phrase("Konto: 4530164301", small));
            par.SpacingAfter = -4f;
            par.Alignment = 0;
            cell.AddElement(par);

            par = new Paragraph(new Phrase("BLZ: 85591000", small));
            par.SpacingAfter = -4f;
            par.Alignment = 0;
            cell.AddElement(par);

            par = new Paragraph(new Phrase("IBAN: DE55855910004530164301", small));
            par.SpacingAfter = -4f;
            par.Alignment = 0;
            cell.AddElement(par);

            par = new Paragraph(new Phrase("BIC: GENODEF1GR1", small));
            par.SpacingAfter = -4f;
            par.Alignment = 0;
            cell.AddElement(par);

            cell.BackgroundColor = new BaseColor(238, 238, 238);
            cell.Border = Rectangle.NO_BORDER;

            table.AddCell(cell);


            cell = new PdfPCell();
            cell.Rowspan = 5;
            cell.UseAscender = true;
            cell.VerticalAlignment = Element.ALIGN_TOP;

            //par = new Paragraph(new Phrase(" ", small));
            //par.SpacingAfter = -4f;
            //par.Alignment = 0;
            // cell.AddElement(par);

            phrase = new Phrase();
            phrase.Add(new Chunk(" \n", logo2_small));
            par = new Paragraph(phrase);
            par.SpacingAfter = -4f;
            par.Alignment = 0;
            cell.AddElement(par);

            par = new Paragraph(new Phrase("info@itcmetalcon.de", small));
            par.SpacingAfter = -4f;
            par.Alignment = 0;
            cell.AddElement(par);

            par = new Paragraph(new Phrase("www.itcmetalcon.de", small));
            par.SpacingAfter = -4f;
            par.Alignment = 0;
            cell.AddElement(par);

            par = new Paragraph(new Phrase("Geshäftsführer / Inhaber: Pawel Swiderski", small));
            par.SpacingAfter = -4f;
            par.Alignment = 0;
            cell.AddElement(par);

            par = new Paragraph(new Phrase(" ", small));
            par.SpacingAfter = -4f;
            par.Alignment = 0;
            cell.AddElement(par);

            par = new Paragraph(new Phrase("USt. ID - Nummer: DE 275947975", small));
            par.SpacingAfter = -4f;
            par.Alignment = 0;
            cell.AddElement(par);

            par = new Paragraph(new Phrase("Steuernummer: 70418/72302", small));
            par.SpacingAfter = -4f;
            par.Alignment = 0;
            cell.AddElement(par);

            cell.BackgroundColor = new BaseColor(238, 238, 238);
            cell.Border = Rectangle.NO_BORDER;

            table.AddCell(cell);

            table.TotalWidth = doc.Right - doc.Left;
            table.LockedWidth = true;
            table.WriteSelectedRows(0, -1, doc.Left, doc.Bottom + 50, pcb);


            doc.Close();

            writer.Close();

            fs.Close();

        }

        public static void CreateAGB()
        {
            FileStream fs = new FileStream("dokumentAGB.pdf", FileMode.Create, FileAccess.Write, FileShare.None);

            Document doc = new Document(PageSize.A4);
            doc.SetMargins(24.65f, 23.8f, doc.TopMargin, doc.BottomMargin);

            string path = Environment.CurrentDirectory;

            string nazwa1 = Oferta[1].Replace("/", "") + "_" + Halledaten[1] + "x" + Halledaten[2] + "m_" + Halledaten[0] + "_" + Personaldaten[7] + " " + Personaldaten[0] + " " + Personaldaten[1];
            PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location.Replace("Oferta+.app/Contents/MonoBundle", "AGB " + nazwa1 + ".pdf")), FileMode.Create));

            doc.Open();



            string[] Tabela = AllManager.CreateReadyElement(bazaTabela1, bazaTabela1_x, bazaTabela1_y);
            string[] Tabela2 = AllManager.CreateReadyElement(bazaTabela2, bazaTabela2_x, bazaTabela2_y);

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

            Font standard = new Font(arial, 9f, Font.NORMAL, BaseColor.BLACK);
            Font standard_10 = new Font(arial, 10f, Font.NORMAL, BaseColor.BLACK);
            Font standard_10_bold = new Font(arialbd, 10f, Font.NORMAL, BaseColor.BLACK);
            Font standard_bold = new Font(arialbd, 9f, Font.NORMAL, BaseColor.BLACK);
            Font standard_blue = new Font(arialbd, 9f, Font.NORMAL, new BaseColor(0, 69, 134));
            Font standard_lightblue = new Font(arial, 9f, Font.NORMAL, new BaseColor(0, 69, 134));

            Font chinese = new Font(simsun, 9f, Font.NORMAL, BaseColor.BLACK);

            Font small = new Font(verdana, 7f, Font.NORMAL, BaseColor.BLACK);

            Font medium = new Font(arial, 8f, Font.NORMAL, BaseColor.BLACK);
            Font medium_bold = new Font(arialbd, 8f, Font.NORMAL, BaseColor.BLACK);

            Font small_blue = new Font(arial, 7f, Font.NORMAL, new BaseColor(0, 69, 134));

            phrase = new Phrase();
            phrase.Add(new Chunk("itc", logo2));
            phrase.Add(new Chunk("metalcon", logo1));
            phrase.Add(new Chunk(".", logo2));
            par = new Paragraph();
            par.Add(phrase);
            par.Alignment = 2;
            doc.Add(par);

            //1tabela dane klienta
            table = new PdfPTable(3);
            cell = new PdfPCell(new Phrase(Personaldaten[7] + " " + Personaldaten[0] + " " + Personaldaten[1], standard_bold));
            cell.Border = Rectangle.NO_BORDER;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(" "));
            cell.Border = Rectangle.NO_BORDER;
            cell.Rowspan = 6;
            table.AddCell(cell);

            //cell = new PdfPCell(new Phrase("Hallensysteme\n Herstellung", small_blue));
            //cell.AddElement(new Phrase("Ferligung nach maB", small_blue));
            //cell.AddElement(new Phrase("Verkauf und Vermietung", small_blue));
            cell = new PdfPCell();
            cell.Border = Rectangle.NO_BORDER;
            cell.UseAscender = true;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            par = new Paragraph(new Phrase(" \n", small_blue));
            par.SpacingAfter = -4f;
            par.Alignment = 2;
            cell.AddElement(par);
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

            cell.Rowspan = 4;
            table.AddCell(cell);

            if (Personaldaten[6].Length > 0)
            {
                cell = new PdfPCell(new Phrase(Personaldaten[6], standard_bold));
                cell.Border = Rectangle.NO_BORDER;
                table.AddCell(cell);
            }

            if (Personaldaten[2].Length > 0)
            {
                cell = new PdfPCell(new Phrase(Personaldaten[2], standard));
                cell.Border = Rectangle.NO_BORDER;
                table.AddCell(cell);
            }

            if (Personaldaten[4].Length > 0)
            {
                cell = new PdfPCell(new Phrase(Personaldaten[4] + " " + Personaldaten[3], standard));
            }
            else
            {
                cell = new PdfPCell(new Phrase(Personaldaten[3], standard));
            }
            cell.Border = Rectangle.NO_BORDER;
            table.AddCell(cell);

            phrase = new Phrase();
            Console.WriteLine(Personaldaten[6].Length);
            if (Personaldaten[5].Length > 6)
            {
                phrase = new Phrase(Personaldaten[5], standard);
            }
            else
            {
                phrase = new Phrase(" ");
            }
            cell = new PdfPCell(phrase);
            cell.Border = Rectangle.NO_BORDER;
            table.AddCell(cell);

            if (Personaldaten[6].Length == 0)
            {
                cell = new PdfPCell(new Phrase(" "));
                cell.Border = Rectangle.NO_BORDER;
                table.AddCell(cell);
            }

            if (Personaldaten[2].Length == 0)
            {
                cell = new PdfPCell(new Phrase(" "));
                cell.Border = Rectangle.NO_BORDER;
                table.AddCell(cell);
            }



            phrase = new Phrase(new Chunk("Angebot Nr: ", medium_bold));
            phrase.Add(new Chunk(Oferta[1] + " von " + MainClass.dataoferty.ToString("dd.MM.yyyy"), medium));
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
            table.AddCell(cell);

            table.TotalWidth = doc.Right - doc.Left;
            //Console.WriteLine(doc.Right - doc.Left);
            table.LockedWidth = true;

            table.SetWidths(new float[] { 1.572f, 0.908f, 1.240f });

            doc.Add(table);



            phrase = new Phrase(new Chunk("MONTAGELEISTUNGEN DURCH AUFTRAGGEBER:", standard_10_bold));
            par = new Paragraph(phrase);
            par.SpacingAfter = -4f;
            par.SetLeading(10f, 0f);
            par.Alignment = 0;
            doc.Add(par);

            par = new Paragraph(new Phrase(" ", standard_10));
            par.SpacingAfter = 0f;
            par.Alignment = 0;
            doc.Add(par);

            phrase = new Phrase(new Chunk("Von Ihnen müssen für den Aufbau gestellt werden (nach Absprache):\n", standard_10));
            //phrase.Add(new Chunk(VonIhnen + "\n ", standard_10));
            //par = new Paragraph(phrase);
            //par.SpacingAfter = -4f;
            //par.SetLeading(10f, 0f);
            //par.Alignment = 0;
            //doc.Add(par);
            if (bazaTabela4.Length > 0)
            {
                if (bazaTabela4.Length > 1)
                {
                    int i = 0;
                    do
                    {
                        phrase.Add(new Chunk(bazaTabela4[i] + "\n", standard_10));
                        i++;
                    } while (i < bazaTabela4.Length - 1);
                }
                phrase.Add(new Chunk(bazaTabela4[bazaTabela4.Length - 1], standard_10));
            }
            else
            {
                phrase.Add(new Chunk("-", standard_lightblue));
            }
            par = new Paragraph(phrase);
            par.SpacingAfter = -4f;
            par.SetLeading(10f, 0f);
            par.Alignment = 0;
            doc.Add(par);

            par = new Paragraph(new Phrase(" ", standard_10));
            par.SpacingAfter = 0f;
            par.Alignment = 0;
            doc.Add(par);

            phrase = new Phrase(new Chunk("Sofern kein betriebsinterner Stapler / Teleskoplader gestellt werden kann, hat der Auftraggeber die entsprechende Maschine / Gerät bereitzustellen und die Kosten dafür zu tragen. Der Auftraggeber veranlasst die Hotelbuchung für unsere Monteure zu seinen Lasten.", standard_10));
            par = new Paragraph(phrase);
            par.SpacingAfter = -4f;
            par.SetLeading(10f, 0f);
            par.Alignment = 0;
            doc.Add(par);

            par = new Paragraph(new Phrase(" ", standard_10));
            par.SpacingAfter = 0f;
            par.Alignment = 0;
            doc.Add(par);

            phrase = new Phrase(new Chunk("Unsere Monteure sollen mind. zwischen 7.00 – 18.00 Uhr arbeiten dürfen. Die Uhrzeiten können nach Absprache geändert werden.", standard_10));
            par = new Paragraph(phrase);
            par.SpacingAfter = -4f;
            par.SetLeading(10f, 0f);
            par.Alignment = 0;
            doc.Add(par);

            par = new Paragraph(new Phrase(" ", standard_10));
            par.SpacingAfter = 0f;
            par.Alignment = 0;
            doc.Add(par);

            phrase = new Phrase(new Chunk("Das für den Aufbau der Halle vorgesehene Gelände muss waagerecht/ ebenerdig sein (Absprache). ", standard_10));
            phrase.Add(new Chunk("Die Gefälle müssen bauseits nivelliert werden. Wir emphehlen die Gründung auf Streifen- evtl. Punktfundament.", standard_10_bold));
            par = new Paragraph(phrase);
            par.SpacingAfter = -4f;
            par.SetLeading(10f, 0f);
            par.Alignment = 0;
            doc.Add(par);

            par = new Paragraph(new Phrase(" ", standard_10));
            par.SpacingAfter = 0f;
            par.Alignment = 0;
            doc.Add(par);

            phrase = new Phrase(new Chunk("Aufgrund der vorgegebenen Konstruktion, muss eine Abdichtung von Spalten unter festen Wandelementen (Trapezbleche/ Isolierelemente) wegen Bodengefälle bauseits geschlossen werden.", standard_10));
            par = new Paragraph(phrase);
            par.SpacingAfter = -4f;
            par.SetLeading(10f, 0f);
            par.Alignment = 0;
            doc.Add(par);

            par = new Paragraph(new Phrase(" ", standard_10));
            par.SpacingAfter = 0f;
            par.Alignment = 0;
            doc.Add(par);

            phrase = new Phrase(new Chunk("Im Bereich der Alustützen und Stahlankerplatten kann bei Leichtbauhallen Kondenswasser und Kapillarwasser aus den Planen-Kedernuten abtropfen. Auch bei Außen-Abdichtungen z. B. mit Bitumenbahnen kann innen im Bereich der Stützenfüße etwas Feuchtigkeit entstehen, ggf. Innenabdichtung umlaufend um inneren Ankerplattenteil.", standard_10));
            par = new Paragraph(phrase);
            par.SpacingAfter = -4f;
            par.SetLeading(10f, 0f);
            par.Alignment = 0;
            doc.Add(par);

            par = new Paragraph(new Phrase(" ", standard_10));
            par.SpacingAfter = 0f;
            par.Alignment = 0;
            doc.Add(par);

            phrase = new Phrase(new Chunk("Der Auftraggeber stellt sicher, dass bauseits ein verantwortlicher Ansprechpartner benannt wird. Dieser muss vor Baubeginn den genauen Standort der Leichtbauhalle unseren Monteuren vor Ort auf der Baustelle zweifelsfrei angeben und sicherstellen, dass im Baustellbereich keine Erdleitungen z.B. Strom, Gas, Wasser etc. beschädigt werden können. Kommt der Auftraggeber dieser Verpflichtung nicht nach, so haftet er für die Schäden, einschließlich eventuelle Folgeschäden. Er stellt den Verkäufer insoweit bereits jetzt von Ansprüchen Dritter frei.", standard_10));
            par = new Paragraph(phrase);
            par.SpacingAfter = -4f;
            par.SetLeading(10f, 0f);
            par.Alignment = 0;
            doc.Add(par);

            par = new Paragraph(new Phrase(" ", standard_10));
            par.SpacingAfter = 0f;
            par.Alignment = 0;
            doc.Add(par);

            phrase = new Phrase(new Chunk("Die Entsorgung von Verpackungsmaterial (z.B. Kunststoff und Holzreste) sowie von Material- bzw. Verschnittresten, die durch die Montage anfallen, erfolgt bauseits durch den Auftraggeber.", standard_10));
            par = new Paragraph(phrase);
            par.SpacingAfter = -4f;
            par.SetLeading(10f, 0f);
            par.Alignment = 0;
            doc.Add(par);

            par = new Paragraph(new Phrase(" ", standard_10));
            par.SpacingAfter = 0f;
            par.Alignment = 0;
            doc.Add(par);

            phrase = new Phrase(new Chunk("Eine Zufahrt für Schwerlast-LKW direkt bis zur Baustelle muss gewährleistet sein. Die Baustelle muss geräumt sein. Außerhalb der Leichtbauhalle muss ein Montagefreiraum für Stapler vorhanden sein von mind. 2,5 m in einem Längswand- und einem Giebelwandbereich. Sollte nicht genügend Freiraum für die Montage vorhanden sein, muss eine telefonische Abstimmung erfolgen.", standard_10));
            par = new Paragraph(phrase);
            par.SpacingAfter = -4f;
            par.SetLeading(10f, 0f);
            par.Alignment = 0;
            doc.Add(par);

            par = new Paragraph(new Phrase(" ", standard_10));
            par.SpacingAfter = 0f;
            par.Alignment = 0;
            doc.Add(par);

            phrase = new Phrase(new Chunk("Die Baustelle ist bauseits von Schnee und Eis zu befreien. Der Montagefreiraum sowie die Hallenfläche müssen komplett freigeräumt und Befahrbar sein.", standard_10));
            par = new Paragraph(phrase);
            par.SpacingAfter = -4f;
            par.SetLeading(10f, 0f);
            par.Alignment = 0;
            doc.Add(par);

            par = new Paragraph(new Phrase(" ", standard_10));
            par.SpacingAfter = 0f;
            par.Alignment = 0;
            doc.Add(par);

            phrase = new Phrase(new Chunk("Beim Einbau eines Rolltores wird ein Probelauf durchgeführt. Der Festanschluß muss bauseits erfolgen. Die Pohlung der Phasen soll richtig angeschlossen werden.", standard_10));
            par = new Paragraph(phrase);
            par.SpacingAfter = -4f;
            par.SetLeading(10f, 0f);
            par.Alignment = 0;
            doc.Add(par);

            par = new Paragraph(new Phrase(" ", standard_10));
            par.SpacingAfter = 0f;
            par.Alignment = 0;
            doc.Add(par);

            phrase = new Phrase(new Chunk("Widrige Witterungsverhältnisse, wie z.B. Wind, Regen, Schnee, Eis und Kälte, gelten als Montageunterbrechung und verlängern ensprechend die Liefer-/ Bauzeit.", standard_10));
            par = new Paragraph(phrase);
            par.SpacingAfter = -4f;
            par.SetLeading(10f, 0f);
            par.Alignment = 0;
            doc.Add(par);





            table = new PdfPTable(3);
            cell = new PdfPCell();
            cell.Rowspan = 4;
            cell.UseAscender = true;
            cell.VerticalAlignment = Element.ALIGN_MIDDLE;

            phrase = new Phrase();
            phrase.Add(new Chunk("itc", logo2_small));
            phrase.Add(new Chunk("metalcon", logo1_small));
            phrase.Add(new Chunk(".\n", logo2_small));
            par = new Paragraph(phrase);
            par.SpacingAfter = -4f;
            par.Alignment = 0;
            cell.AddElement(par);

            par = new Paragraph(new Phrase("Normannenstrasse 2", small));
            par.SpacingAfter = -4f;
            par.Alignment = 0;
            cell.AddElement(par);

            par = new Paragraph(new Phrase("71263 Weil der Stadt", small));
            par.SpacingAfter = -4f;
            par.Alignment = 0;
            cell.AddElement(par);

            par = new Paragraph(new Phrase("Tel.: (+49) 162 4230214", small));
            par.SpacingAfter = -4f;
            par.Alignment = 0;
            cell.AddElement(par);

            par = new Paragraph(new Phrase("Fax.: (+49) 7033 467017 \n ", small));
            par.SpacingAfter = -4f;
            par.Alignment = 0;
            cell.AddElement(par);

            par = new Paragraph(new Phrase(" ", small));
            par.SpacingAfter = -0.5f;
            par.Alignment = 0;
            cell.AddElement(par);

            cell.BackgroundColor = new BaseColor(238, 238, 238);
            cell.Border = Rectangle.NO_BORDER;

            table.AddCell(cell);


            cell = new PdfPCell();
            cell.Rowspan = 4;
            cell.UseAscender = true;
            cell.VerticalAlignment = Element.ALIGN_TOP;

            //par = new Paragraph(new Phrase(" ", small));
            //par.SpacingAfter = -4f;
            //par.Alignment = 0;
            // cell.AddElement(par);

            phrase = new Phrase();
            phrase.Add(new Chunk(" \n", logo2_small));
            par = new Paragraph(phrase);
            par.SpacingAfter = -4f;
            par.Alignment = 0;
            cell.AddElement(par);

            par = new Paragraph(new Phrase("Konto: 4530164301", small));
            par.SpacingAfter = -4f;
            par.Alignment = 0;
            cell.AddElement(par);

            par = new Paragraph(new Phrase("BLZ: 85591000", small));
            par.SpacingAfter = -4f;
            par.Alignment = 0;
            cell.AddElement(par);

            par = new Paragraph(new Phrase("IBAN: DE55855910004530164301", small));
            par.SpacingAfter = -4f;
            par.Alignment = 0;
            cell.AddElement(par);

            par = new Paragraph(new Phrase("BIC: GENODEF1GR1", small));
            par.SpacingAfter = -4f;
            par.Alignment = 0;
            cell.AddElement(par);

            cell.BackgroundColor = new BaseColor(238, 238, 238);
            cell.Border = Rectangle.NO_BORDER;

            table.AddCell(cell);


            cell = new PdfPCell();
            cell.Rowspan = 5;
            cell.UseAscender = true;
            cell.VerticalAlignment = Element.ALIGN_TOP;

            //par = new Paragraph(new Phrase(" ", small));
            //par.SpacingAfter = -4f;
            //par.Alignment = 0;
            // cell.AddElement(par);

            phrase = new Phrase();
            phrase.Add(new Chunk(" \n", logo2_small));
            par = new Paragraph(phrase);
            par.SpacingAfter = -4f;
            par.Alignment = 0;
            cell.AddElement(par);

            par = new Paragraph(new Phrase("info@itcmetalcon.de", small));
            par.SpacingAfter = -4f;
            par.Alignment = 0;
            cell.AddElement(par);

            par = new Paragraph(new Phrase("www.itcmetalcon.de", small));
            par.SpacingAfter = -4f;
            par.Alignment = 0;
            cell.AddElement(par);

            par = new Paragraph(new Phrase("Geshäftsführer / Inhaber: Pawel Swiderski", small));
            par.SpacingAfter = -4f;
            par.Alignment = 0;
            cell.AddElement(par);

            par = new Paragraph(new Phrase(" ", small));
            par.SpacingAfter = -4f;
            par.Alignment = 0;
            cell.AddElement(par);

            par = new Paragraph(new Phrase("USt. ID - Nummer: DE 275947975", small));
            par.SpacingAfter = -4f;
            par.Alignment = 0;
            cell.AddElement(par);

            par = new Paragraph(new Phrase("Steuernummer: 70418/72302", small));
            par.SpacingAfter = -4f;
            par.Alignment = 0;
            cell.AddElement(par);

            cell.BackgroundColor = new BaseColor(238, 238, 238);
            cell.Border = Rectangle.NO_BORDER;

            table.AddCell(cell);

            table.TotalWidth = doc.Right - doc.Left;
            table.LockedWidth = true;
            PdfContentByte pcb = writer.DirectContent;
            table.WriteSelectedRows(0, -1, doc.Left, doc.Bottom + 50, pcb);



            doc.NewPage();

            phrase = new Phrase();
            phrase.Add(new Chunk("itc", logo2));
            phrase.Add(new Chunk("metalcon", logo1));
            phrase.Add(new Chunk(".", logo2));
            par = new Paragraph();
            par.Add(phrase);
            par.Alignment = 2;
            doc.Add(par);

            table = new PdfPTable(1);

            cell = new PdfPCell(new Phrase(" ", standard_10));
            cell.Border = Rectangle.NO_BORDER;
            table.AddCell(cell);
            table.AddCell(cell);

            phrase = new Phrase(new Chunk("Angebot Nr: ", medium_bold));
            phrase.Add(new Chunk(Oferta[1] + " von " + MainClass.dataoferty.ToString("dd.MM.yyyy"), medium));
            par = new Paragraph(phrase);
            par.Alignment = 2;
            cell = new PdfPCell(par);
            cell.UseAscender = true;
            cell.HorizontalAlignment = 2;
            cell.Border = Rectangle.NO_BORDER;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(" ", standard_10));
            cell.Border = Rectangle.NO_BORDER;
            table.AddCell(cell);
            table.AddCell(cell);

            table.TotalWidth = doc.Right - doc.Left;
            table.LockedWidth = true;
            doc.Add(table);

            par = new Paragraph(new Phrase(" ", standard_10));
            par.SpacingAfter = 0f;
            par.Alignment = 0;
            doc.Add(par);

            phrase = new Phrase(new Chunk("Nach Fertigstellung der Halle erfolgt eine Abnahme.\n", standard_10_bold));
            phrase.Add(new Chunk("Der Auftraggeber hat eine unterschriftsberechtigte Person zu benennen, die das Abnahmeprotokoll unterzeichnet. Ist vom Auftraggeber niemand vor Ort, der das Abnahmeprotokoll unterzeichnen kann oder will, gilt die Abnahme der Zelthalle als mängelfrei bestätigt ohne Unterschrift.", standard_10));
            par = new Paragraph(phrase);
            par.SpacingAfter = -4f;
            par.SetLeading(10f, 0f);
            par.Alignment = 0;
            doc.Add(par);

            par = new Paragraph(new Phrase(" ", standard_10));
            par.SpacingAfter = 0f;
            par.Alignment = 0;
            doc.Add(par);

            phrase = new Phrase(new Chunk("Vor Auftragserteilung obliegt es dem Auftraggeber, den Aufbau der Leichtbauhalle/ Zelthalle beim örtlichen Bauamt anzuzeigen. Bei Standzeiten länger als 3 Monate ist vom Auftraggeber rechtzeitig vor Auftragserteilung und Aufbau der Leichtbauhalle beim Bauamt zu klären, ob eine Baugenehmigung einzuholen ist. Die Beantragung des fliegenden Baus bzw. der Baugenehmigung wird vom Bauherrn vorgenommen. Das Risiko einer fehlenden Baugenehmigung trägt der Auftraggeber. Wir empfehlen dringend, vor Auftragserteilung die Freigabe durch den Brandschutzbeauftragten einzuholen.", standard_10));
            par = new Paragraph(phrase);
            par.SpacingAfter = -4f;
            par.SetLeading(10f, 0f);
            par.Alignment = 0;
            doc.Add(par);

            par = new Paragraph(new Phrase(" ", standard_10));
            par.SpacingAfter = 0f;
            par.Alignment = 0;
            doc.Add(par);

            phrase = new Phrase(new Chunk("Abnahme- und Genehmigungsgebühren, die Erfüllung zusätzlicher Auflagen des Bauamtes wie z.B. Betonfundamente, Brandschutzauflagen etc. sowie die Wiederherstellung der Baustelle in der ursprünglichen Zustand sind Sache des Auftraggebers.", standard_10));
            par = new Paragraph(phrase);
            par.SpacingAfter = -4f;
            par.SetLeading(10f, 0f);
            par.Alignment = 0;
            doc.Add(par);

            par = new Paragraph(new Phrase(" ", standard_10));
            par.SpacingAfter = 0f;
            par.Alignment = 0;
            doc.Add(par);

            phrase = new Phrase(new Chunk("Bis zur Auftragserteilung behalten wir uns eine anderweitige Verkauf vor, das Angebot ist gültig solange Vorrat reicht.", standard_10));
            par = new Paragraph(phrase);
            par.SpacingAfter = -4f;
            par.SetLeading(10f, 0f);
            par.Alignment = 0;
            doc.Add(par);

            par = new Paragraph(new Phrase(" ", standard_10));
            par.SpacingAfter = 0f;
            par.Alignment = 0;
            doc.Add(par);

            phrase = new Phrase(new Chunk("Zahlung bei Kauf:\n", standard_10));
            phrase.Add(new Chunk("1) Mit Montage: 30 % vom Auftragswert bei Bestellung. 60 % bei Anlieferung, zahlbar vor Abladung. Die restlichen 10 % nach Abnahme/ Fertigstellung.\n", standard_10));
            phrase.Add(new Chunk("Ohne Montage: 40% vom Auftragswert bei Bestellung, Restzahlung 60% bei Anlieferung, vor Abladung.", standard_10));
            par = new Paragraph(phrase);
            par.SpacingAfter = -4f;
            par.SetLeading(10f, 0f);
            par.Alignment = 0;
            doc.Add(par);

            par = new Paragraph(new Phrase(" ", standard_10));
            par.SpacingAfter = 0f;
            par.Alignment = 0;
            doc.Add(par);

            phrase = new Phrase(new Chunk("2) Sollte die Bonitätsprüfung negativ ausfallen, erfolgt eine 50 % Anzahlung bei Bestellung, 50 % Restzahlung bei Anlieferung - vor Abladung.", standard_10));
            par = new Paragraph(phrase);
            par.SpacingAfter = -4f;
            par.SetLeading(10f, 0f);
            par.Alignment = 0;
            doc.Add(par);

            par = new Paragraph(new Phrase(" ", standard_10));
            par.SpacingAfter = 0f;
            par.Alignment = 0;
            doc.Add(par);

            phrase = new Phrase(new Chunk("Die Lieferung erfolgt unter erweitertem Eigentumsvorbehalt, d.h. bis zur vollständigen Bezahlung verbleibt die Zelthalle in unseren Eigentum.", standard_10));
            par = new Paragraph(phrase);
            par.SpacingAfter = -4f;
            par.SetLeading(10f, 0f);
            par.Alignment = 0;
            doc.Add(par);

            par = new Paragraph(new Phrase(" ", standard_10));
            par.SpacingAfter = 0f;
            par.Alignment = 0;
            doc.Add(par);

            phrase = new Phrase(new Chunk("Das Angebot ist freibleibend. Aufgrund der Metallpreissituation muss mit Preiserhöhungen gerechnet werden.", standard_10));
            par = new Paragraph(phrase);
            par.SpacingAfter = -4f;
            par.SetLeading(10f, 0f);
            par.Alignment = 0;
            doc.Add(par);

            par = new Paragraph(new Phrase(" ", standard_10));
            par.SpacingAfter = 0f;
            par.Alignment = 0;
            doc.Add(par);

            phrase = new Phrase(new Chunk(" \n \n \n ", standard_10));
            par = new Paragraph(phrase);
            par.SpacingAfter = -4f;
            par.SetLeading(10f, 0f);
            par.Alignment = 0;
            doc.Add(par);

            par = new Paragraph(new Phrase(" ", standard_10));
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

            phrase = new Phrase(new Chunk("Ihr ITC- Team", standard));
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

            table = new PdfPTable(3);
            cell = new PdfPCell();
            cell.Rowspan = 4;
            cell.UseAscender = true;
            cell.VerticalAlignment = Element.ALIGN_MIDDLE;

            phrase = new Phrase();
            phrase.Add(new Chunk("itc", logo2_small));
            phrase.Add(new Chunk("metalcon", logo1_small));
            phrase.Add(new Chunk(".\n", logo2_small));
            par = new Paragraph(phrase);
            par.SpacingAfter = -4f;
            par.Alignment = 0;
            cell.AddElement(par);

            par = new Paragraph(new Phrase("Normannenstrasse 2", small));
            par.SpacingAfter = -4f;
            par.Alignment = 0;
            cell.AddElement(par);

            par = new Paragraph(new Phrase("71263 Weil der Stadt", small));
            par.SpacingAfter = -4f;
            par.Alignment = 0;
            cell.AddElement(par);

            par = new Paragraph(new Phrase("Tel.: (+49) 162 4230214", small));
            par.SpacingAfter = -4f;
            par.Alignment = 0;
            cell.AddElement(par);

            par = new Paragraph(new Phrase("Fax.: (+49) 7033 467017 \n ", small));
            par.SpacingAfter = -4f;
            par.Alignment = 0;
            cell.AddElement(par);

            par = new Paragraph(new Phrase(" ", small));
            par.SpacingAfter = -0.5f;
            par.Alignment = 0;
            cell.AddElement(par);

            cell.BackgroundColor = new BaseColor(238, 238, 238);
            cell.Border = Rectangle.NO_BORDER;

            table.AddCell(cell);


            cell = new PdfPCell();
            cell.Rowspan = 4;
            cell.UseAscender = true;
            cell.VerticalAlignment = Element.ALIGN_TOP;

            //par = new Paragraph(new Phrase(" ", small));
            //par.SpacingAfter = -4f;
            //par.Alignment = 0;
            // cell.AddElement(par);

            phrase = new Phrase();
            phrase.Add(new Chunk(" \n", logo2_small));
            par = new Paragraph(phrase);
            par.SpacingAfter = -4f;
            par.Alignment = 0;
            cell.AddElement(par);

            par = new Paragraph(new Phrase("Konto: 4530164301", small));
            par.SpacingAfter = -4f;
            par.Alignment = 0;
            cell.AddElement(par);

            par = new Paragraph(new Phrase("BLZ: 85591000", small));
            par.SpacingAfter = -4f;
            par.Alignment = 0;
            cell.AddElement(par);

            par = new Paragraph(new Phrase("IBAN: DE55855910004530164301", small));
            par.SpacingAfter = -4f;
            par.Alignment = 0;
            cell.AddElement(par);

            par = new Paragraph(new Phrase("BIC: GENODEF1GR1", small));
            par.SpacingAfter = -4f;
            par.Alignment = 0;
            cell.AddElement(par);

            cell.BackgroundColor = new BaseColor(238, 238, 238);
            cell.Border = Rectangle.NO_BORDER;

            table.AddCell(cell);


            cell = new PdfPCell();
            cell.Rowspan = 5;
            cell.UseAscender = true;
            cell.VerticalAlignment = Element.ALIGN_TOP;

            //par = new Paragraph(new Phrase(" ", small));
            //par.SpacingAfter = -4f;
            //par.Alignment = 0;
            // cell.AddElement(par);

            phrase = new Phrase();
            phrase.Add(new Chunk(" \n", logo2_small));
            par = new Paragraph(phrase);
            par.SpacingAfter = -4f;
            par.Alignment = 0;
            cell.AddElement(par);

            par = new Paragraph(new Phrase("info@itcmetalcon.de", small));
            par.SpacingAfter = -4f;
            par.Alignment = 0;
            cell.AddElement(par);

            par = new Paragraph(new Phrase("www.itcmetalcon.de", small));
            par.SpacingAfter = -4f;
            par.Alignment = 0;
            cell.AddElement(par);

            par = new Paragraph(new Phrase("Geshäftsführer / Inhaber: Pawel Swiderski", small));
            par.SpacingAfter = -4f;
            par.Alignment = 0;
            cell.AddElement(par);

            par = new Paragraph(new Phrase(" ", small));
            par.SpacingAfter = -4f;
            par.Alignment = 0;
            cell.AddElement(par);

            par = new Paragraph(new Phrase("USt. ID - Nummer: DE 275947975", small));
            par.SpacingAfter = -4f;
            par.Alignment = 0;
            cell.AddElement(par);

            par = new Paragraph(new Phrase("Steuernummer: 70418/72302", small));
            par.SpacingAfter = -4f;
            par.Alignment = 0;
            cell.AddElement(par);

            cell.BackgroundColor = new BaseColor(238, 238, 238);
            cell.Border = Rectangle.NO_BORDER;

            table.AddCell(cell);

            table.TotalWidth = doc.Right - doc.Left;
            table.LockedWidth = true;
            table.WriteSelectedRows(0, -1, doc.Left, doc.Bottom + 50, pcb);























            doc.Close();

            writer.Close();

            fs.Close();
        }


    }



}
