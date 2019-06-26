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
    public class Raport
    {
        public Raport()
        {
        }

        public static string zakresdat = "";

        public static int checkStatus = 0;

        public static string[] CheckDates()
        {
            string[] daty = new string[0];

            string[] pliki = Directory.GetFiles(Assembly.GetEntryAssembly().Location.Replace("Oferta+.app/Contents/MonoBundle/Oferta+.exe", "Projects/"));

            if (pliki.Length > 0)
            {
                if(checkStatus == 0) //miesiace
                {
                    int count = 0;
                    do
                    {
                        if (daty.Contains(File.GetLastWriteTime(pliki[count]).Date.ToString("MM/yyyy")) == false)
                        {
                            Array.Resize(ref daty, daty.Length + 1);
                            daty[daty.Length - 1] = File.GetLastWriteTime(pliki[count]).Date.ToString("MM/yyyy");
                        }
                        count++;
                    } while (count < pliki.Length);
                }
                else if(checkStatus == 1) //tygodnie
                {
                    int count = 0;
                    do
                    {
                        if(daty.Length > 0)
                        {
                            int numer = (int)File.GetLastWriteTime(pliki[count]).Date.DayOfWeek;
                            if (numer == 0)
                            {
                                numer = 7;
                            }
                            string firstday = File.GetLastWriteTime(pliki[count]).AddDays((Convert.ToDouble(numer) * -1) + 1).ToString("dd");
                            string endday = File.GetLastWriteTime(pliki[count]).AddDays(7 - Convert.ToDouble(numer)).ToString("dd");

                            string readyelement = firstday + "-" + endday + "/" + File.GetLastWriteTime(pliki[count]).Date.ToString("MM/yyyy");

                            if(daty.Contains(readyelement) == false)
                            {
                                Array.Resize(ref daty, daty.Length + 1);
                                daty[daty.Length - 1] = readyelement;
                            }
                        }
                        else
                        {
                            Array.Resize(ref daty, daty.Length + 1);
                            int numer = (int)File.GetLastWriteTime(pliki[count]).Date.DayOfWeek;
                            if(numer == 0)
                            {
                                numer = 7;
                            }
                            string firstday = File.GetLastWriteTime(pliki[count]).AddDays((Convert.ToDouble(numer) * -1) + 1).ToString("dd");
                            string endday = File.GetLastWriteTime(pliki[count]).AddDays(7 - Convert.ToDouble(numer)).ToString("dd");

                            daty[daty.Length - 1] = firstday + "-" + endday + "/" + File.GetLastWriteTime(pliki[count]).Date.ToString("MM/yyyy");
                        }
                        count++;
                    } while (count < pliki.Length);
                }
            }

            return daty;
        }

        public static string[] CheckDates2() //po wprowadzeniu dat
        {
            string[] daty = new string[0];

            string[] pliki = Directory.GetFiles(Assembly.GetEntryAssembly().Location.Replace("Oferta+.app/Contents/MonoBundle/Oferta+.exe", "Projects/"));

            if(pliki.Length > 0)
            {
                if (checkStatus == 0) //miesiace
                {
                    int count = 0;
                    do
                    {
                        if(pliki[count].Substring(pliki[count].Length - 9) != ".DS_Store")
                        {
                            string[] dane = File.ReadAllLines(pliki[count]);

                            //w z zwiazku z przeskakujaca linia 50 musi sprawdzic ktora linie ma brac
                            int line = 75;
                            if (dane[50].Length > 0)
                            {
                                if (dane[50].Substring(0, 1) == "-")
                                {
                                    line = 76;
                                }
                            }

                            if (dane[line].Length > 1)
                            {
                                if (daty.Contains(dane[line].Substring(5, 2) + "/" + dane[line].Substring(0, 4)) == false)
                                {
                                    Array.Resize(ref daty, daty.Length + 1);
                                    daty[daty.Length - 1] = dane[line].Substring(5, 2) + "/" + dane[line].Substring(0, 4);
                                }
                            }
                            else //zabezpieczenie obslugujace stare wersje
                            {
                                if (daty.Contains( new FileInfo(pliki[count]).CreationTime.Date.ToString("MM/yyyy")) == false)
                                {
                                    Array.Resize(ref daty, daty.Length + 1);
                                    daty[daty.Length - 1] = new FileInfo(pliki[count]).CreationTime.Date.ToString("MM/yyyy");
                                }
                            }
                        }

                        /*
                        if (daty.Contains(File.GetLastWriteTime(pliki[count]).Date.ToString("MM/yyyy")) == false)
                        {
                            Array.Resize(ref daty, daty.Length + 1);
                            daty[daty.Length - 1] = File.GetLastWriteTime(pliki[count]).Date.ToString("MM/yyyy");
                        }
                        */

                        count++;
                    } while (count < pliki.Length);
                }
                else if(checkStatus == 1) //tygodnie
                {
                    int count = 0;
                    do
                    {
                        if (pliki[count].Substring(pliki[count].Length - 9) != ".DS_Store")
                        {
                            string[] dane = File.ReadAllLines(pliki[count]);

                            //w z zwiazku z przeskakujaca linia 50 musi sprawdzic ktora linie ma brac
                            int line = 75;
                            if (dane[50].Length > 0)
                            {
                                if (dane[50].Substring(0, 1) == "-")
                                {
                                    line = 76;
                                }
                            }

                            if (dane[line].Length > 1)
                            {
                                DateTime data = DateTime.ParseExact(dane[line], "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
                                int numer = (int)data.Date.DayOfWeek;
                                if (numer == 0)
                                {
                                    numer = 7;
                                }
                                string firstday = data.AddDays((Convert.ToDouble(numer) * -1) + 1).ToString("dd");
                                string endday = data.AddDays(7 - Convert.ToDouble(numer)).ToString("dd");

                                string readyelement = firstday + "-" + endday + "/" + data.Date.ToString("MM/yyyy");

                                if (daty.Contains(readyelement) == false)
                                {
                                    Array.Resize(ref daty, daty.Length + 1);
                                    daty[daty.Length - 1] = readyelement;
                                }
                            }
                            else //zabezpieczenie obslugujace stare wersje
                            {
                                int numer = (int)new FileInfo(pliki[count]).CreationTime.Date.DayOfWeek;
                                if (numer == 0)
                                {
                                    numer = 7;
                                }
                                string firstday = new FileInfo(pliki[count]).CreationTime.AddDays((Convert.ToDouble(numer) * -1) + 1).ToString("dd");
                                string endday = new FileInfo(pliki[count]).CreationTime.AddDays(7 - Convert.ToDouble(numer)).ToString("dd");

                                string readyelement = firstday + "-" + endday + "/" + new FileInfo(pliki[count]).CreationTime.Date.ToString("MM/yyyy");

                                if (daty.Contains(readyelement) == false)
                                {
                                    Array.Resize(ref daty, daty.Length + 1);
                                    daty[daty.Length - 1] = readyelement;
                                }
                            }
                        }

                        count++;
                    } while (count < pliki.Length);
                }
            }


            return daty;
        }


        public static string[] GetList()
        {
            if(zakresdat.Length < 12)
            {
                string m = zakresdat.Substring(0, 2);
                string y = zakresdat.Substring(3, 4);

                zakresdat = "01-" + Convert.ToString(DateTime.DaysInMonth(Convert.ToInt32(y), Convert.ToInt32(m))) + "/" + zakresdat;
            }

            string firstday = zakresdat.Substring(0, 2);
            string endday = zakresdat.Substring(3, 2);
            string month = zakresdat.Substring(6, 2);
            string year = zakresdat.Substring(9, 4);

            DateTime firstdata = DateTime.ParseExact(year + "-" + month + "-" + firstday + " 00:00:00:000", "yyyy-MM-dd HH:mm:ss:fff", System.Globalization.CultureInfo.InvariantCulture);
            DateTime enddata = DateTime.ParseExact(year + "-" + month + "-" + endday + " 23:59:59:000", "yyyy-MM-dd HH:mm:ss:fff", System.Globalization.CultureInfo.InvariantCulture);

            string[] pliki = Directory.GetFiles(Assembly.GetEntryAssembly().Location.Replace("Oferta+.app/Contents/MonoBundle/Oferta+.exe", "Projects/"));

            string[] readypliki = new string[0];

            if(pliki.Length > 0)
            {
                int count = 0;
                do
                {
                    if (File.GetLastWriteTime(pliki[count]) >= firstdata && File.GetLastWriteTime(pliki[count]) <= enddata && pliki[count].Substring(pliki[count].Length - 9, 9) != ".DS_Store")
                    {
                        Array.Resize(ref readypliki, readypliki.Length + 1);
                        readypliki[readypliki.Length - 1] = pliki[count];
                    }
                    count++;
                } while (count < pliki.Length);
            }
            //Console.WriteLine(readypliki[0]);
            return readypliki;
        }

        public static string[] GetList2() //po wprowadzeniu dat
        {
            if (zakresdat.Length < 12)
            {
                string m = zakresdat.Substring(0, 2);
                string y = zakresdat.Substring(3, 4);

                zakresdat = "01-" + Convert.ToString(DateTime.DaysInMonth(Convert.ToInt32(y), Convert.ToInt32(m))) + "/" + zakresdat;
            }

            string firstday = zakresdat.Substring(0, 2);
            string endday = zakresdat.Substring(3, 2);
            string month = zakresdat.Substring(6, 2);
            string year = zakresdat.Substring(9, 4);

            DateTime firstdata = DateTime.ParseExact(year + "-" + month + "-" + firstday + " 00:00:00:000", "yyyy-MM-dd HH:mm:ss:fff", System.Globalization.CultureInfo.InvariantCulture);
            DateTime enddata = DateTime.ParseExact(year + "-" + month + "-" + endday + " 23:59:59:000", "yyyy-MM-dd HH:mm:ss:fff", System.Globalization.CultureInfo.InvariantCulture);

            string[] pliki = Directory.GetFiles(Assembly.GetEntryAssembly().Location.Replace("Oferta+.app/Contents/MonoBundle/Oferta+.exe", "Projects/"));

            string[] readypliki = new string[0];

            if (pliki.Length > 0)
            {
                int count = 0;
                do
                {
                    if (pliki[count].Substring(pliki[count].Length - 9) != ".DS_Store")
                    {
                        string[] dane = File.ReadAllLines(pliki[count]);

                        //w z zwiazku z przeskakujaca linia 50 musi sprawdzic ktora linie ma brac
                        int line = 75;
                        if (dane[50].Length > 0)
                        {
                            if (dane[50].Substring(0, 1) == "-")
                            {
                                line = 76;
                            }
                        }

                        if (dane[line].Length > 1)
                        {
                            DateTime data = DateTime.ParseExact(dane[line], "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
                            if(data >= firstdata && data <= enddata)
                            {
                                Array.Resize(ref readypliki, readypliki.Length + 1);
                                readypliki[readypliki.Length - 1] = pliki[count];
                            }
                        }
                        else //na stare wersje
                        {
                            if (new FileInfo(pliki[count]).CreationTime >= firstdata && new FileInfo(pliki[count]).CreationTime <= enddata)
                            {
                                Array.Resize(ref readypliki, readypliki.Length + 1);
                                readypliki[readypliki.Length - 1] = pliki[count];
                            }
                        }
                    }


                    count++;
                } while (count < pliki.Length);
            }
            //Console.WriteLine(readypliki[0]);
            return readypliki;
        }




        public static void Create()
        {

            FileStream fs = new FileStream("dokument.pdf", FileMode.Create, FileAccess.Write, FileShare.None);

            Document doc = new Document(PageSize.A4.Rotate());
            //doc.SetMargins(24.65f, 23.8f, doc.TopMargin, doc.BottomMargin);

            string path = Environment.CurrentDirectory;

            string[] pliki = GetList2();

            string nazwa1 = "RAPORT " + zakresdat;
            PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location.Replace("Oferta+.app/Contents/MonoBundle", nazwa1.Replace("/",".") + ".pdf")), FileMode.Create));

            doc.Open();

            PdfPTable table;
            PdfPCell cell;
            Paragraph par;
            Phrase phrase;

            BaseFont arial = BaseFont.CreateFont(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location.Replace("Oferta+.app/Contents/MonoBundle", "Fonts/Arial.ttf")), BaseFont.CP1252, BaseFont.EMBEDDED);
            BaseFont arialbd = BaseFont.CreateFont(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location.Replace("Oferta+.app/Contents/MonoBundle", "Fonts/ARIALBD0.TTF")), BaseFont.CP1252, BaseFont.EMBEDDED);

            Font logo = new Font(arialbd, 20f, Font.NORMAL, BaseColor.BLACK);

            Font standard = new Font(arial, 9f, Font.NORMAL, BaseColor.BLACK);
            Font standard_bold = new Font(arialbd, 9f, Font.NORMAL, BaseColor.BLACK);

            Font small_bold = new Font(arialbd, 7f, Font.NORMAL, BaseColor.DARK_GRAY);

            Font odstep = new Font(arial, 5f, Font.NORMAL, BaseColor.DARK_GRAY);

            phrase = new Phrase(new Chunk("Raport", logo));
            par = new Paragraph(phrase);
            doc.Add(par);

            phrase = new Phrase(new Chunk("Maja Żerko", standard_bold));
            par = new Paragraph(phrase);
            par.Alignment = 2;
            doc.Add(par);

            phrase = new Phrase(new Chunk("z zakresu: " + zakresdat, standard_bold));
            par = new Paragraph(phrase);
            doc.Add(par);

            phrase = new Phrase(new Chunk(" ", standard_bold));
            par = new Paragraph(phrase);
            doc.Add(par);

            phrase = new Phrase(new Chunk(" ", standard_bold));
            par = new Paragraph(phrase);
            doc.Add(par);

            table = new PdfPTable(9);

            //nowe
            cell = new PdfPCell(new Phrase("Lp.", standard_bold));
            cell.HorizontalAlignment = 1;
            cell.UseAscender = true;
            cell.VerticalAlignment = Element.ALIGN_MIDDLE;
            cell.BackgroundColor = BaseColor.LIGHT_GRAY;
            cell.Rowspan = 2;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Numer oferty", standard_bold));
            cell.HorizontalAlignment = 1;
            cell.UseAscender = true;
            cell.VerticalAlignment = Element.ALIGN_MIDDLE;
            cell.BackgroundColor = BaseColor.LIGHT_GRAY;
            cell.Rowspan = 2;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Klient", standard_bold));
            cell.HorizontalAlignment = 1;
            cell.UseAscender = true;
            cell.VerticalAlignment = Element.ALIGN_MIDDLE;
            cell.BackgroundColor = BaseColor.LIGHT_GRAY;
            cell.Rowspan = 2;
            table.AddCell(cell);

            //nowe
            cell = new PdfPCell(new Phrase("E-mail", standard_bold));
            cell.HorizontalAlignment = 1;
            cell.UseAscender = true;
            cell.VerticalAlignment = Element.ALIGN_MIDDLE;
            cell.BackgroundColor = BaseColor.LIGHT_GRAY;
            cell.Rowspan = 2;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Nazwa hali", standard_bold));
            cell.HorizontalAlignment = 1;
            cell.UseAscender = true;
            cell.VerticalAlignment = Element.ALIGN_MIDDLE;
            cell.BackgroundColor = BaseColor.LIGHT_GRAY;
            cell.Rowspan = 2;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Cena hali", standard_bold));
            cell.HorizontalAlignment = 1;
            cell.UseAscender = true;
            cell.VerticalAlignment = Element.ALIGN_MIDDLE;
            cell.BackgroundColor = BaseColor.LIGHT_GRAY;
            cell.Rowspan = 2;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Cena montażu", standard_bold));
            cell.HorizontalAlignment = 1;
            cell.UseAscender = true;
            cell.VerticalAlignment = Element.ALIGN_MIDDLE;
            cell.BackgroundColor = BaseColor.LIGHT_GRAY;
            cell.Rowspan = 2;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Kontakty z klientem", standard_bold));
            cell.HorizontalAlignment = 1;
            cell.UseAscender = true;
            cell.VerticalAlignment = Element.ALIGN_MIDDLE;
            cell.BackgroundColor = BaseColor.LIGHT_GRAY;
            cell.Rowspan = 2;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Komentarz", standard_bold));
            cell.HorizontalAlignment = 1;
            cell.UseAscender = true;
            cell.VerticalAlignment = Element.ALIGN_MIDDLE;
            cell.BackgroundColor = BaseColor.LIGHT_GRAY;
            cell.Rowspan = 2;
            table.AddCell(cell);

            int count = 0;
            do
            {
                cell = new PdfPCell(new Phrase(Convert.ToString(count + 1), standard));
                cell.HorizontalAlignment = 1;
                cell.UseAscender = true;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                table.AddCell(cell);

                string[] dane = File.ReadAllLines(pliki[count]);
                Console.WriteLine(dane[0]);
                cell = new PdfPCell(new Phrase(dane[0], standard));
                cell.HorizontalAlignment = 1;
                cell.UseAscender = true;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase(" \n" + dane[2] + " " + dane[3] + "\n" + dane[4] + "\n" + dane[6] + ", " + dane[5] + "\n ", standard));
                cell.HorizontalAlignment = 0;
                cell.UseAscender = true;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                table.AddCell(cell);

                //fix przesuniecia linii (robie 2x bo tutaj jest wartosc ponizej 50 a nizej sa znowu wyzej wiec tutaj nie moge zmienic tablicy)
                if (dane[50].Length > 0)
                {
                    if (dane[50].Substring(0, 1) == "-")
                    {
                        cell = new PdfPCell(new Phrase(dane[69], standard));
                        cell.HorizontalAlignment = 1;
                        cell.UseAscender = true;
                        cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                        table.AddCell(cell);
                    }
                    else
                    {
                        cell = new PdfPCell(new Phrase(dane[68], standard));
                        cell.HorizontalAlignment = 1;
                        cell.UseAscender = true;
                        cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                        table.AddCell(cell);
                    }
                }
                else
                {
                    cell = new PdfPCell(new Phrase(dane[68], standard));
                    cell.HorizontalAlignment = 1;
                    cell.UseAscender = true;
                    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    table.AddCell(cell);
                }

                //cell = new PdfPCell(new Phrase(dane[10] + " " + pliki[count].Split("_")[1], standard)); //to u mnie nie dziala bo mam wiecej '_' w sciezce
                cell = new PdfPCell(new Phrase(dane[10] + " " + dane[13] + "x" + dane[16] + "x" + dane[19] + "m", standard)); //teraz pobiera z danych a nie z nazwy XD
                cell.HorizontalAlignment = 0;
                cell.UseAscender = true;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                table.AddCell(cell);





                int[] bazaTabela1_ilosc = new int[0];
                float[] bazaTabela1_cena = new float[0];
                if(dane[38].Length > 2)
                {
                    bazaTabela1_ilosc = Array.ConvertAll(dane[38].Substring(0, dane[38].Length - 2).Split("||"), int.Parse);
                    bazaTabela1_cena = Array.ConvertAll(dane[39].Substring(0, dane[39].Length - 2).Split("||"), float.Parse);
                }

                cell = new PdfPCell(new Phrase(String.Format("{0:0.00}", Convert.ToDouble(AllManager.PoliczSume(bazaTabela1_ilosc, bazaTabela1_cena, 0f).Replace(",", "."))).Replace(".", ",") + " €", standard));
                cell.HorizontalAlignment = 2;
                cell.UseAscender = true;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                table.AddCell(cell);

                //fix przesuniecia linii (przesuwam tablice bo juz nie ma wyzszych wartosci)
                if (dane[50].Length > 0)
                {
                    if (dane[50].Substring(0, 1) == "-")
                    {
                        Array.Reverse(dane);
                        Array.Resize(ref dane, dane.Length - 1);
                        Array.Reverse(dane);
                    }
                }
                
                if (dane[51] == "" || int.TryParse(dane[51], out int n) == false)
                {
                    dane[51] = "0";
                }
                Console.WriteLine(pliki[count]);
                cell = new PdfPCell(new Phrase(String.Format("{0:0.00}", Convert.ToDouble(dane[51].Replace(",", "."))).Replace(".", ",") + " €", standard));
                cell.HorizontalAlignment = 2;
                cell.UseAscender = true;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                table.AddCell(cell);

                string kontakt = "";

                string[] pom = pliki[count].Split("/");

                string sciezka = "";

                int count2 = 1;
                do
                {
                    sciezka = sciezka + "/" + pom[count2];
                    count2++;
                } while (count2 < pom.Length - 1);

                sciezka = sciezka + "/Bazy/" + pom[count2].Replace(".txt", "BT5.txt");


                if (File.Exists(sciezka))
                {
                    kontakt = File.ReadAllText(sciezka).Replace("|",": ");
                }


                cell = new PdfPCell(new Phrase(kontakt, standard));
                cell.HorizontalAlignment = 2;
                cell.UseAscender = true;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase(dane[69].Replace("||", Environment.NewLine), standard));
                cell.HorizontalAlignment = 0;
                cell.UseAscender = true;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                table.AddCell(cell);

                count++;
            } while (count < pliki.Length);

            //Console.WriteLine(doc.Right + " " + doc.Left);

            table.TotalWidth = (doc.Right - doc.Left);
            table.LockedWidth = true;



            table.SetWidths(new float[] { 0.5f, 2f, 2f, 2f, 3f, 1.5f, 1.5f, 2f, 3f });







            doc.Add(table);



            doc.Close();

            writer.Close();

            fs.Close();


            //dodanie numeracji do pdf
            byte[] bytes = File.ReadAllBytes(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location.Replace("Oferta+.app/Contents/MonoBundle", nazwa1.Replace("/", ".") + ".pdf")));
            Font blackFont = FontFactory.GetFont("Arial", 12, Font.NORMAL, BaseColor.BLACK);
            using (MemoryStream stream = new MemoryStream())
            {
                PdfReader reader = new PdfReader(bytes);
                using (PdfStamper stamper = new PdfStamper(reader, stream))
                {
                    int pages = reader.NumberOfPages;
                    for (int i = 1; i <= pages; i++)
                    {
                        if(i == 1)
                        {
                            ColumnText.ShowTextAligned(stamper.GetUnderContent(i), Element.ALIGN_RIGHT, new Phrase("Strona: " + i.ToString() + " / " + pages.ToString(), blackFont), 800f, 15f, 0);
                        }
                        else
                        {
                            ColumnText.ShowTextAligned(stamper.GetUnderContent(i), Element.ALIGN_RIGHT, new Phrase(nazwa1 + " Strona: " + i.ToString() + " / " + pages.ToString(), blackFont), 800f, 15f, 0);
                        }

                    }
                }
                bytes = stream.ToArray();
            }
            File.WriteAllBytes(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location.Replace("Oferta+.app/Contents/MonoBundle", nazwa1.Replace("/", ".") + ".pdf")), bytes);
        }


    }
}
