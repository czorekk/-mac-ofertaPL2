using System;
using AppKit;
using System.IO;
using System.Text;
using System.Reflection;
using iTextSharp.text;
using iTextSharp.text.pdf;
namespace Oferta__
{
    public class Bestellungsformullar
    {
        public Bestellungsformullar()
        {
        }

        public static void CreatePDF()
        {
            FileStream fs = new FileStream("dokument.pdf", FileMode.Create, FileAccess.Write, FileShare.None);

            Document doc = new Document(PageSize.A4);
            doc.SetMargins(24.65f, 23.8f, doc.TopMargin, doc.BottomMargin);

            string path = Environment.CurrentDirectory;

            string nazwa1 = "Bestellungsformular" + MainClass.Oferta[1].Replace("/", "") + "_" + MainClass.Halledaten[1] + "x" + MainClass.Halledaten[2] + "m_" + MainClass.Halledaten[0] + "_" + MainClass.Personaldaten[7] + " " + MainClass.Personaldaten[0] + " " + MainClass.Personaldaten[1];
            PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location.Replace("Oferta+.app/Contents/MonoBundle", nazwa1 + ".pdf")), FileMode.Create));

            doc.Open();


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
            cell = new PdfPCell(new Phrase(MainClass.Personaldaten[7] + " " + MainClass.Personaldaten[0] + " " + MainClass.Personaldaten[1], standard_bold));
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

            if (MainClass.Personaldaten[6].Length > 0)
            {
                cell = new PdfPCell(new Phrase(MainClass.Personaldaten[6], standard_bold));
                cell.Border = Rectangle.NO_BORDER;
                table.AddCell(cell);
            }

            if (MainClass.Personaldaten[2].Length > 0)
            {
                cell = new PdfPCell(new Phrase(MainClass.Personaldaten[2], standard));
                cell.Border = Rectangle.NO_BORDER;
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
            table.AddCell(cell);

            phrase = new Phrase();
            //Console.WriteLine(Personaldaten[6].Length);
            if (MainClass.Personaldaten[5].Length > 6)
            {
                phrase = new Phrase(MainClass.Personaldaten[5], standard);
            }
            else
            {
                phrase = new Phrase(" ");
            }
            cell = new PdfPCell(phrase);
            cell.Border = Rectangle.NO_BORDER;
            table.AddCell(cell);

            if (MainClass.Personaldaten[6].Length == 0)
            {
                cell = new PdfPCell(new Phrase(" "));
                cell.Border = Rectangle.NO_BORDER;
                table.AddCell(cell);
            }

            if (MainClass.Personaldaten[2].Length == 0)
            {
                cell = new PdfPCell(new Phrase(" "));
                cell.Border = Rectangle.NO_BORDER;
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
            table.AddCell(cell);

            table.TotalWidth = doc.Right - doc.Left;
            //Console.WriteLine(doc.Right - doc.Left); //default 534
            //Console.WriteLine(doc.LeftMargin + " " + doc.RightMargin); //default 36/36
            table.LockedWidth = true;

            table.SetWidths(new float[] { 1.572f, 0.908f, 1.240f });

            doc.Add(table);








            par = new Paragraph(new Phrase(" ", standard));
            par.SpacingAfter = 0f;
            par.Alignment = 0;
            doc.Add(par);

            phrase = new Phrase(new Chunk("Lieferadresse / Aufbauort: ", standard_bold));
            phrase.Add(new Chunk("..............................................................", standard));
            par = new Paragraph(phrase);
            par.SpacingAfter = -4f;
            par.SetLeading(10f, 0f);
            par.Alignment = 0;
            doc.Add(par);

            par = new Paragraph(new Phrase(" ", standard));
            par.SpacingAfter = 0f;
            par.Alignment = 0;
            doc.Add(par);

            phrase = new Phrase(new Chunk("Hallengröße (B x L x H): ", standard_bold));
            phrase.Add(new Chunk("..................................................................", standard));
            par = new Paragraph(phrase);
            par.SpacingAfter = -4f;
            par.SetLeading(10f, 0f);
            par.Alignment = 0;
            doc.Add(par);

            par = new Paragraph(new Phrase(" ", standard));
            par.SpacingAfter = 0f;
            par.Alignment = 0;
            doc.Add(par);

            phrase = new Phrase(new Chunk("Schneelast: ", standard_bold));
            string windzone = MainClass.Halledaten[10];
            if(MainClass.Halledaten2[8].Length > 0)
            {
                windzone = MainClass.Halledaten2[8] + " " + windzone;
            }
            string schneelast = MainClass.Halledaten[9];
            if(MainClass.Halledaten2[7].Length > 0)
            {
                schneelast = MainClass.Halledaten2[7] + " " + schneelast;
            }
            phrase.Add(new Chunk(MainClass.Halledaten2[7] + schneelast + " kN/m², Windzone " + windzone + " kN/m²", standard));
            par = new Paragraph(phrase);
            par.SpacingAfter = -4f;
            par.SetLeading(10f, 0f);
            par.Alignment = 0;
            doc.Add(par);

            par = new Paragraph(new Phrase(" ", standard));
            par.SpacingAfter = 0f;
            par.Alignment = 0;
            doc.Add(par);

            phrase = new Phrase(new Chunk("Boden: ", standard_bold));
            phrase.Add(new Chunk("..............................................................................................", standard));
            par = new Paragraph(phrase);
            par.SpacingAfter = -4f;
            par.SetLeading(10f, 0f);
            par.Alignment = 0;
            doc.Add(par);

            par = new Paragraph(new Phrase(" ", standard));
            par.SpacingAfter = 0f;
            par.Alignment = 0;
            doc.Add(par);

















            string[] typ_pom = MainClass.Halledaten[0].Split(" ");
            string[] typ = new string[2];
            if (typ_pom[1].Substring(0, 2) == "TT" || typ_pom[1].Substring(0, 2) == "PT" || typ_pom[1].Substring(0, 2) == "PP")
            {
                typ[1] = typ_pom[1];
            }
            else if (typ_pom[1].Substring(0, 3) == "ISO" || typ_pom[1].Substring(0, 3) == "DPS")
            {
                typ[1] = typ_pom[1];
            }
            else if (typ_pom.Length > 2)
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
            if (typ[1].Substring(0, 2) == "TT")
            {
                //opis1 = "Konstruktion aus Aluminiumkastenprofil mit Zugbändern, mit First- und Eckverbindungen aus feuerverzinktem Stahl. Die feuerverzinkten Fußplatten sind entsprechend den statischen Erfordernissen mittels Erdnägeln (1,00 m lang) auf nicht bindigen, dicht gelagerten Boden evtl. Schwerlastdübeln auf Fundamenten verankert.";
                opis1 = MainClass.TechnischeDaten;
                opis2 = "Dacheindeckung und Giebeldreieck: PVC- beschichtete Gewebe hoch-glanz-lackiert, Gewicht " + MainClass.Technische[4] + " g/m², schwerentflammbar gemäß DIN4102/B1. Es ist in den 4-Kedernut Alu-Profil eingezogen. Das PVC- Material in Standardfarbe Weiß - es kann nach Absprache und evtl. gegen Aufpreis in anderen Farben geliefert werden.";
                opis3 = "Zur Isolierung und Kondenswasservermeidung kann im Dachbereich (von Traufe bis Traufe) gegen Aufpreis eine lichtdurchlässige Innenplane montiert werden.";
            }
            else if (typ[1].Substring(0, 2) == "PT")
            {
                //opis1 = "Konstruktion aus Aluminiumkastenprofil mit Zugbändern, mit First- und Eckverbindungen aus feuerverzinktem Stahl. Die feuerverzinkten Fußplatten sind entsprechend den statischen Erfordernissen mittels Erdnägeln (1,00 m lang) auf nicht bindigen, dicht gelagerten Boden (DIN-EN 13782) evtl. Schwerlastdübeln auf Fundamenten (DIN-EN 1991) verankert.";
                opis1 = MainClass.TechnischeDaten;
                opis2 = "Dacheindeckung und Giebeldreieck: PVC- beschichtete Gewebe hoch-glanz-lackiert, Gewicht " + MainClass.Technische[4] + " g/m², schwerentflammbar gemäß DIN4102/B1. Es ist in den 4-Kedernut Alu-Profil eingezogen. Das PVC- Material in Standardfarbe Weiß - es kann nach Absprache und evtl. gegen Aufpreis in anderen Farben geliefert werden.";
                opis3 = "Zur Isolierung und Kondenswasservermeidung kann im Dachbereich (von Traufe bis Traufe) gegen Aufpreis eine lichtdurchlässige Innenplane montiert werden.";
            }
            else if (typ[1].Substring(0, 2) == "PP")
            {
                opis1 = MainClass.TechnischeDaten;
                opis2 = "Dacheindeckung und Giebeldreieck: PVC- beschichtete Gewebe hoch-glanz-lackiert, Gewicht " + MainClass.Technische[4] + " g/m², schwerentflammbar gemäß DIN4102/B1. Es ist in den 4-Kedernut Alu-Profil eingezogen. Das PVC- Material in Standardfarbe Weiß - es kann nach Absprache und evtl. gegen Aufpreis in anderen Farben geliefert werden.";
                opis3 = "Zur Isolierung und Kondenswasservermeidung kann im Dachbereich (von Traufe bis Traufe) gegen Aufpreis eine lichtdurchlässige Innenplane montiert werden.";
            }
            else if (typ[1].Substring(0, 3) == "ISO")
            {
                //opis1 = "Konstruktion aus Doppel-T-Träger IPE240 (Riegel) und HEA 180 (Ständer), ohne Zugbändern (Flacheisen). Baustahl 235, feuerverzinkt nach DIN-EN ISO 1461. Die Fußplattern sind entsprechend den statischen Erfordernissen mittles Schwerlastdübeln auf Fundementen verankert.";
                opis1 = MainClass.TechnischeDaten;
                opis2 = "Sandwichpaneelen, Stärke 40mm, chwerentflammbar nach DIN 4102 Baustoffklasse 1, U-Wert";
                opis3 = "";
            }
            else if (typ[1].Substring(0, 3) == "DPS")
            {
                //opis1 = "Konstruktion aus Aluminiumkastenprofil mit Zugbändern, mit First- und Eckverbindungen aus feuerverzinktem Stahl. Die feuerverzinkten Fußplatten sind entsprechend den statischen Erfordernissen mittels Erdnägeln (1,00 m lang) auf nicht bindigen, dicht gelagerten Boden (DIN-EN 13782) evtl. Schwerlastdübeln auf Fundamenten (DIN-EN 1991) verankert.";
                opis1 = MainClass.TechnischeDaten;
            }

            phrase = new Phrase(new Chunk("Konstruktion: ", standard_bold));
            phrase.Add(new Chunk(MainClass.TechnischeDaten, standard));
            par = new Paragraph(phrase);
            par.SpacingAfter = -4f;
            par.SetLeading(10f, 0f);
            par.Alignment = 0;
            doc.Add(par);
            par = new Paragraph(new Phrase(" ", standard));
            par.SpacingAfter = 0f;
            par.Alignment = 0;
            doc.Add(par);

           
            if (typ[1].Substring(0, 2) == "PT")
            {
                phrase = new Phrase(new Chunk("Dacheindeckung und Giebeldreieck: ", standard_bold));
                phrase.Add(new Chunk("PVC- beschichtete Gewebe hoch-glanz-lackiert, Gewicht " + MainClass.Technische[4] + " g/m², schwerentflammbar gemäß DIN4102/B1. Es ist in den 4-Kedernut Alu-Profil eingezogen. Das PVC- Material in Standardfarbe Weiß - es kann nach Absprache und evtl. gegen Aufpreis in anderen Farben geliefert werden.", standard));
                phrase.Add(new Chunk("\nFarbton: ............................................................................................", standard));
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
                phrase = new Phrase(new Chunk("Dacheindeckung: ", standard_bold));
                phrase.Add(new Chunk("bestehend aus verzinkten und kunststoffbeschichteten Stahl-Trapezblechen T35 / T18, Stärke " + MainClass.Technische[0] + " mm.", standard));
                phrase.Add(new Chunk("\nFarbton: ............................................................................................", standard));
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
                phrase.Add(new Chunk("PVC- beschichtete Gewebe hoch-glanz-lackiert, Gewicht " + MainClass.Technische[4] + " g/m2, schwerentflammbar gemäß DIN4102/B1. Es ist in den 4-Kedernut Alu-Profil eingezogen. Das PVC- Material in Standardfarbe Weiß oder Grün - es kann nach Absprache in anderen Farben geliefert werden.", standard));
                phrase.Add(new Chunk("\nFarbton: ............................................................................................", standard));
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
                phrase.Add(new Chunk("doppelschalige PVC- beschichtete Gewebe hoch-glanz-lackiert, Gewicht " + MainClass.Technische[4] + " g/m², schwerentflammbar gemäß DIN4102/B1, K-Wert " + MainClass.Technische[5] + " W/m²K, Druckregler mit Kompressor (1 Stk.).", standard));
                phrase.Add(new Chunk("\nFarbton: ............................................................................................", standard));
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
            else if (MainClass.Select3 == 1)
            {
                phrase = new Phrase(new Chunk("Dacheindeckung: ", standard_bold));
                phrase.Add(new Chunk("Sandwichpaneelen (profiliert), ", standard));
                phrase.Add(new Chunk("Stärke " + MainClass.Technische[0] + " mm, ", standard_bold));
                phrase.Add(new Chunk("schwerentflammbar nach DIN 4102 Baustoffklasse 1, ", standard));
                phrase.Add(new Chunk("U-Wert (W/m²K) = " + MainClass.Technische[1] + ". ", standard_bold));
                phrase.Add(new Chunk("Die Panellen bestehen aus einem Kern aus Hartschaum zwischen verzinkten und kunststoffbeschichtetem Stahlblech- Deckschalen.", standard));
                phrase.Add(new Chunk("\nFarbton: ............................................................................................", standard));
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
                phrase = new Phrase(new Chunk("Wandverkleidung ", standard_bold));
                phrase.Add(new Chunk("bestehend aus waagerecht (evtl. senkrecht) verlegten, verzinkten und kunststoffbeschichteten Stahl-Trapezblechen T35 / T18, Stärke " + MainClass.Technische[2] + " mm.", standard));
                phrase.Add(new Chunk("\nFarbton: ............................................................................................", standard));
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
                phrase.Add(new Chunk("bestehend aus waagerecht (evtl. senkrecht) verlegten, verzinkten und kunststoffbeschichteten Stahl-Trapezblechen T35 / T18, Stärke " + MainClass.Technische[2] + " mm.", standard));
                phrase.Add(new Chunk("\nFarbton: ............................................................................................", standard));
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
                phrase.Add(new Chunk("bestehend aus waagerecht verlegten, verzinkten und kunststoffbeschichteten Stahl-Trapezblechen T35 / T18, Stärke " + MainClass.Technische[2] + " mm.", standard));
                phrase.Add(new Chunk("\nFarbton: ............................................................................................", standard));
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
                phrase = new Phrase(new Chunk("Wandverkleidung: ", standard_bold));
                phrase.Add(new Chunk("Sandwichpaneelen (profiliert), ", standard));
                phrase.Add(new Chunk("Stärke " + MainClass.Technische[2] + " mm, ", standard_bold));
                phrase.Add(new Chunk("schwerentflammbar nach DIN 4102 Baustoffklasse 1, ", standard));
                phrase.Add(new Chunk("U-Wert (W/m²K) = " + MainClass.Technische[3] + ". ", standard_bold));
                phrase.Add(new Chunk("Die Panellen bestehen aus einem Kern aus Hartschaum zwischen verzinkten und kunststoffbeschichtetem Stahlblech- Deckschalen. Die Verkleidung schließt mit einem Abschluss aus Kantblechen + Dichtband am Boden ab.", standard));
                phrase.Add(new Chunk("\nFarbton: ............................................................................................", standard));
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
            else if (MainClass.Select3 == 1)
            {
                phrase = new Phrase(new Chunk("Wandverkleidung: ", standard_bold));
                phrase.Add(new Chunk("Sandwichpaneelen (profiliert), ", standard));
                phrase.Add(new Chunk("Stärke " + MainClass.Technische[2] + " mm, ", standard_bold));
                phrase.Add(new Chunk("schwerentflammbar nach DIN 4102 Baustoffklasse 1, ", standard));
                phrase.Add(new Chunk("U-Wert (W/m²K) = " + MainClass.Technische[3] + ". ", standard_bold));
                phrase.Add(new Chunk("Die Panellen bestehen aus einem Kern aus Hartschaum zwischen verzinkten und kunststoffbeschichtetem Stahlblech- Deckschalen. Die Verkleidung schließt mit einem Abschluss aus Kantblechen + Dichtband am Boden ab.", standard));
                phrase.Add(new Chunk("\nFarbton: ............................................................................................", standard));
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

            phrase = new Phrase(new Chunk("Tore und Türen:\n", standard_bold));
            phrase.Add(new Chunk(MainClass.ToreUndTuren, standard));
            par = new Paragraph(phrase);
            par.SpacingAfter = -4f;
            par.SetLeading(10f, 0f);
            par.Alignment = 0;
            doc.Add(par);

            par = new Paragraph(new Phrase(" ", standard));
            par.SpacingAfter = 0f;
            par.Alignment = 0;
            doc.Add(par);

            phrase = new Phrase(new Chunk("Zusätliche Ausstattung:", standard_bold));

            string ausstattung = "";
            string[] elm = AllManager.CreateReadyElement(MainClass.bazaTabela2, MainClass.bazaTabela2_x, MainClass.bazaTabela2_y);
            for (int i = 0; i < MainClass.bazaTabela2.Length; i++)
            {
                ausstattung = ausstattung + "\n-" + Convert.ToString(MainClass.bazaTabela2_ilosc[i]) + " " + MainClass.bazaTabela2_jedn[i] + " " + elm[i];
                if(i < MainClass.bazaTabela2.Length - 1)
                {
                    ausstattung = ausstattung + ",";
                }
            }
            phrase.Add(new Chunk(ausstattung, standard));
            par = new Paragraph(phrase);
            par.SpacingAfter = -4f;
            par.SetLeading(10f, 0f);
            par.Alignment = 0;
            doc.Add(par);

            par = new Paragraph(new Phrase(" ", standard));
            par.SpacingAfter = 0f;
            par.Alignment = 0;
            doc.Add(par);

            string leichtbauhalle = "";
            if(MainClass.Halledaten[0].Split(" ")[MainClass.Halledaten[0].Split(" ").Length - 1].Length > 2) {
                leichtbauhalle = MainClass.Halledaten[0].Substring(0, MainClass.Halledaten[0].Length - 4); 
            }
            else
            {
                leichtbauhalle = MainClass.Halledaten[0].Substring(0, MainClass.Halledaten[0].Length - 3);
            }
            phrase = new Phrase(new Chunk("Ich bestelle die " + leichtbauhalle + " mit Lieferung und mit / ohne Montage.", standard_bold));
            par = new Paragraph(phrase);
            par.SpacingAfter = -4f;
            par.SetLeading(10f, 0f);
            par.Alignment = 0;
            doc.Add(par);

            par = new Paragraph(new Phrase(" ", standard));
            par.SpacingAfter = 0f;
            par.Alignment = 0;
            doc.Add(par);

            phrase = new Phrase(new Chunk("Gesamtpreis: ", standard_bold));

            string gesamtpreis = String.Format("{0:0.00}", Convert.ToDouble(MainClass.Gesamtpreis)).Replace(".", ",");
            phrase.Add(new Chunk(gesamtpreis + " € inkl. Liefer- und Montagekosten, zzgl. 19% MwSt.", standard));
            par = new Paragraph(phrase);
            par.SpacingAfter = -4f;
            par.SetLeading(10f, 0f);
            par.Alignment = 0;
            doc.Add(par);

            par = new Paragraph(new Phrase(" ", standard));
            par.SpacingAfter = 0f;
            par.Alignment = 0;
            doc.Add(par);

            phrase = new Phrase(new Chunk("Zahlung bei Kauf:\n", standard_bold));
            phrase.Add(new Chunk("Mit Montage: 30% vom Auftragswert bei Bestellung, 60% bei Anlieferung, vor Abladung. Restzahlung - 10% bei Abnahme / Fertigstellung (siehe AGB).\nOhne Montage: 40% vom Auftragswert bei Bestellung, Restzahlung 60% bei Anlieferung, vor Abladung.", standard));
            par = new Paragraph(phrase);
            par.SpacingAfter = -4f;
            par.SetLeading(10f, 0f);
            par.Alignment = 0;
            doc.Add(par);

            par = new Paragraph(new Phrase(" ", standard));
            par.SpacingAfter = 0f;
            par.Alignment = 0;
            doc.Add(par);

            phrase = new Phrase(new Chunk("Ich habe die AGB von itcmetalcon. gelesen und akzeptiere diese.", standard_bold));
            par = new Paragraph(phrase);
            par.SpacingAfter = -4f;
            par.SetLeading(10f, 0f);
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

            phrase = new Phrase(new Chunk("Datum, Unterschrift", standard));
            par = new Paragraph(phrase);
            par.SpacingAfter = -4f;
            par.SetLeading(10f, 0f);
            par.Alignment = 0;
            doc.Add(par);



            //stopka
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







            doc.Close();

            writer.Close();

            fs.Close();


        }
    }
}
