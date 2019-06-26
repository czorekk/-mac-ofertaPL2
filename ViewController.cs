using AppKit;
using System;
using System.IO;
using System.IO.Compression;
using System.Text;
using System.Linq;
using System.Reflection;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

using System;
using System.Linq;

using AppKit;
using Foundation;
using CoreGraphics;
using System.Threading.Tasks;

namespace Oferta__
{
    public partial class ViewController : NSViewController
    {

        public string TypHali = "";
        public string Stallhallee = "";

        public ViewController(IntPtr handle) : base(handle)
        {
        }
        
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            //AppSecure.Verify();
            //GenerateAlert("test", "test");
            // Do any additional setup after loading the view.
            //LabelTest1.StringValue = Assembly.GetEntryAssembly().Location;
            ChechFiles(SearchTextField.StringValue);
            Data.DateValue = NSDate.Now;
            DataOferty.DateValue = NSDate.Now;
            DataPotwierdzenia.DateValue = NSDate.Now;
            //Console.WriteLine();
            //DateTime data = DateTime.ParseExact("2019-01-22", "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
            //Console.WriteLine((int)data.DayOfWeek);
            //LabelTest2.StringValue = System.IO.Directory.GetCurrentDirectory();
            //Console.WriteLine(MacUUID());

            //trigger na pisanie w szukaniu pliku
            SearchTextField.Changed += SearchTextField_Changed;
            //AppSecure.Verify();
            
        }

        public override NSObject RepresentedObject
        {
            get
            {
                return base.RepresentedObject;
            }
            set
            {
                base.RepresentedObject = value;
                // Update the view, if already loaded.

            }
        }

        void SearchTextField_Changed(object sender, EventArgs e)
        {
            ChechFiles(SearchTextField.StringValue);
        }


        public static DateTime Czas(DateTime dt)
        {
            return DateTime.SpecifyKind(dt, DateTimeKind.Utc).ToLocalTime();
        }

        partial void PotwierdzenieButton_Click(NSObject sender)
        {
            Save();
            MainClass.netto = String.Format("{0:0.00}", Convert.ToDouble(PriceText2.StringValue.Replace(",", "."))).Replace(".", ",");
            MainClass.netto10 = String.Format("{0:0.00}", (Convert.ToDouble(PriceText2.StringValue.Replace(",", ".")) * 0.10)).Replace(".", ",");
            MainClass.netto30 = String.Format("{0:0.00}", (Convert.ToDouble(PriceText2.StringValue.Replace(",", ".")) * 0.30)).Replace(".", ",");
            MainClass.netto60 = String.Format("{0:0.00}", (Convert.ToDouble(PriceText2.StringValue.Replace(",", ".")) * 0.60)).Replace(".", ",");
            MainClass.vat = String.Format("{0:0.00}", Convert.ToDouble(Vat2.StringValue.Replace(",", "."))).Replace(".", ",");
            double kwvat = Convert.ToDouble(PriceText2.StringValue.Replace(",", ".")) * (Convert.ToDouble(Vat2.StringValue.Replace(",", ".")) / 100);
            MainClass.kwvat = String.Format("{0:0.00}", kwvat).Replace(".", ",");
            MainClass.vat10 = String.Format("{0:0.00}", (kwvat * 0.10)).Replace(".", ",");
            MainClass.vat30 = String.Format("{0:0.00}", (kwvat * 0.30)).Replace(".", ",");
            MainClass.vat60 = String.Format("{0:0.00}", (kwvat * 0.60)).Replace(".", ",");
            MainClass.brutto = String.Format("{0:0.00}", (Convert.ToDouble(PriceText2.StringValue.Replace(",", ".")) + kwvat)).Replace(".", ",");
            MainClass.brutto10 = String.Format("{0:0.00}", ((Convert.ToDouble(PriceText2.StringValue.Replace(",", ".")) + kwvat) * 0.10)).Replace(".", ",");
            MainClass.brutto30 = String.Format("{0:0.00}", ((Convert.ToDouble(PriceText2.StringValue.Replace(",", ".")) + kwvat) * 0.30)).Replace(".", ",");
            MainClass.brutto60 = String.Format("{0:0.00}", ((Convert.ToDouble(PriceText2.StringValue.Replace(",", ".")) + kwvat) * 0.60)).Replace(".", ",");
            MainClass.nrzam = NrPotwierdzenia.StringValue;
            MainClass.liefer = Liefertermin.StringValue;
            MainClass.dataoferty = Czas((DateTime)DataOferty.DateValue);
            MainClass.datapotwierdzenia = Czas((DateTime)DataPotwierdzenia.DateValue);
            MainClass.Bezeich = Bezeichnung.StringValue;

            //Console.WriteLine(DateTime.SpecifyKind((DateTime)DataOferty.DateValue, DateTimeKind.Utc).ToLocalTime());

            PotwierdzenieZamowienia.Create();
            InfoLabel1.StringValue = "Potwierdzenie zostało utworzone.";
        }

        partial void BestellungsformularButton_Click(NSObject sender)
        {
            if(Gesamtpreis.StringValue.Length > 0)
            {
                if(AusstattungDo.IntValue <= MainClass.bazaTabela1.Length)
                {
                    MainClass.dataoferty = Czas((DateTime)DataOferty.DateValue);
                    MainClass.datapotwierdzenia = Czas((DateTime)DataPotwierdzenia.DateValue);
                    SetPersonaldaten();
                    SetHalledaten();
                    MainClass.TechnischeDaten = TechnischeDaten.StringValue;
                    MainClass.AufWunsch = AufWunsch.StringValue;
                    MainClass.Select2 = Convert.ToString(Select2.SelectedSegment);
                    MainClass.Stallhalle = Stallhalle.State.ToString();
                    MainClass.HauptGewicht = HauptProfilGewicht.StringValue;
                    string[] typ = MainClass.Halledaten[0].Split(" ");
                    SetTechDate(typ);
                    MainClass.Unterlagen = Unterlagen.StringValue;

                    MainClass.AusstattungOd = AusstattungOd.IntValue;
                    MainClass.AusstattungDo = AusstattungDo.IntValue;
                    MainClass.Gesamtpreis = Gesamtpreis.DoubleValue;

                    if (Kedernut.SelectedSegment == 0)
                    {
                        MainClass.Kedernut = "4";
                    }
                    else
                    {
                        MainClass.Kedernut = "2";
                    }

                    Bestellungsformullar.CreatePDF();
                    Save();
                    InfoLabel1.StringValue = "PDF został stworzony.";
                }
                else
                {
                    GenerateAlert("Błąd", "Niepoprawny zakres z wyposażenia dla Ausstattung.");
                }
            }
            else
            {
                GenerateAlert("Błąd", "Aby generować Bestellungsformular musisz podać Gesamtpreis.");
            }

        }

        partial void SearchCheckBox_Click(NSObject sender)
        {
            if(Convert.ToString(SearchCheckBox.State) == "On")
            {
                SearchText.Hidden = true;
                SearchTextField.Hidden = true;
                AdvancedSearchBox.Hidden = false;
            }
            else
            {
                SearchText.Hidden = false;
                SearchTextField.Hidden = false;
                AdvancedSearchBox.Hidden = true;
            }
        }

        partial void MontageSwitch_Click(NSObject sender)
        {
            if (Convert.ToString(MontageSwitch.State) == "On")
            {
                MontageBox.Hidden = true;
            }
            else
            {
                MontageBox.Hidden = false;
            }
        }

        partial void UnterlagenSwitch_Click(NSObject sender)
        {
            if(Convert.ToString(UnterlagenSwitch.State) == "On")
            {
                UnterlagenBox.Hidden = true;
            }
            else
            {
                UnterlagenBox.Hidden = false;
            }
        }

        partial void GenerateRaport_Click(NSObject sender)
        {
            Raport.zakresdat = ComboBoxRaport.StringValue;
            Raport.Create();
        }

        partial void RaportType_Click(NSObject sender)
        {
            Raport.checkStatus = (int)RaportType.SelectedSegment;
            AllManager.RefreshComboBox(Raport.CheckDates2(), ComboBoxRaport);
        }

        partial void AddWindowsButton1_Click(NSObject sender)
        {
            //dodaje do 2tabeli wyposazenia kilka elementow (w tym przypadku okna)

            //PVC- Fenster 0,50 x 0,50 mm (B x H) mit Dreh-Kipp-Funktion /1 Stk. /195,00 €
            AllManager.MoveValueFromComboBoxToTable(MainClass.bazaTabela2, 
                                                    MainClass.bazaTabela2_ilosc, 
                                                    MainClass.bazaTabela2_cena, 
                                                    MainClass.bazaTabela2_x, 
                                                    MainClass.bazaTabela2_y, 
                                                    MainClass.bazaTabela2_jedn, 
                                                    MainClass.bazaComboBox1, 
                                                    Tabela2,
                                                    "PVC- Fenster 00x00 mm (B x H) mit Dreh-Kipp-Funktion", 
                                                    1, 
                                                    195f, 
                                                    "0.5", 
                                                    "0.5", 
                                                    "Stk.", 
                                                    2);

            //PVC- Fenster 1,00 x 1,10 mm (B x H) mit Dreh-Kipp-Funktion /1 Stk. /295,00 €
            AllManager.MoveValueFromComboBoxToTable(MainClass.bazaTabela2,
                                                    MainClass.bazaTabela2_ilosc,
                                                    MainClass.bazaTabela2_cena,
                                                    MainClass.bazaTabela2_x,
                                                    MainClass.bazaTabela2_y,
                                                    MainClass.bazaTabela2_jedn,
                                                    MainClass.bazaComboBox1,
                                                    Tabela2,
                                                    "PVC- Fenster 00x00 mm (B x H) mit Dreh-Kipp-Funktion",
                                                    1,
                                                    295f,
                                                    "1",
                                                    "1.1",
                                                    "Stk.",
                                                    2);

            //PVC- Fenster doppelflügelig 1,45 x 1,10 mm (B x H) – ein Flügel mit Dreh-Kipp-Funktion /1 Stk. /365,00 €
            AllManager.MoveValueFromComboBoxToTable(MainClass.bazaTabela2,
                                                    MainClass.bazaTabela2_ilosc,
                                                    MainClass.bazaTabela2_cena,
                                                    MainClass.bazaTabela2_x,
                                                    MainClass.bazaTabela2_y,
                                                    MainClass.bazaTabela2_jedn,
                                                    MainClass.bazaComboBox1,
                                                    Tabela2,
                                                    "PVC- Fenster doppelflügelig 00x00 mm (B x H) – ein Flügel mit Dreh-Kipp-Funktion",
                                                    1,
                                                    365f,
                                                    "1.45",
                                                    "1.1",
                                                    "Stk.",
                                                    2);

            //PVC- Fenster mit Sprosse 2,00 x 1,00 m (B x H) mit Kippfunktion /1 Stk. /345,00 €
            AllManager.MoveValueFromComboBoxToTable(MainClass.bazaTabela2,
                                                    MainClass.bazaTabela2_ilosc,
                                                    MainClass.bazaTabela2_cena,
                                                    MainClass.bazaTabela2_x,
                                                    MainClass.bazaTabela2_y,
                                                    MainClass.bazaTabela2_jedn,
                                                    MainClass.bazaComboBox1,
                                                    Tabela2,
                                                    "PVC- Fenster mit Sprosse 00x00 m (B x H) mit Kippfunktion",
                                                    1,
                                                    345f,
                                                    "2",
                                                    "1",
                                                    "Stk.",
                                                    2);
        }

        public void SetPersonaldaten()
        {
            //Console.WriteLine(Vorname.StringValue);
            MainClass.Personaldaten[0] = Vorname.StringValue;
            MainClass.Personaldaten[1] = Name.StringValue;
            MainClass.Personaldaten[2] = StraBe.StringValue;
            MainClass.Personaldaten[3] = Stadt.StringValue;
            MainClass.Personaldaten[4] = Postlietzahl.StringValue;
            MainClass.Personaldaten[5] = "Tel.: " + Telefonnummer.StringValue;
            MainClass.Personaldaten[6] = Firma.StringValue;
            if(Convert.ToInt32(Plec.SelectedSegment) == 0)
            {
                MainClass.Personaldaten[7] = "Herr";
            }
            else
            {
                MainClass.Personaldaten[7] = "Frau";
            }
            MainClass.Personaldaten[8] = Mail.StringValue;
        }


        public void SetHalledaten()
        {
            MainClass.Halledaten[0] = Leichbauhalle.StringValue;
            MainClass.Halledaten[1] = Breite.StringValue;
            MainClass.Halledaten[2] = Lange2.StringValue;
            MainClass.Halledaten[3] = Traufhohe.StringValue;
            MainClass.Halledaten[4] = Firsthohe.StringValue;
            MainClass.Halledaten[5] = Binderabstand.StringValue;
            MainClass.Halledaten[6] = Zugbandhohe.StringValue;
            if(Hauptprofil3.StringValue == "0")
            {
                MainClass.Halledaten[7] = Hauptprofil1.StringValue + " x " + Hauptprofil2.StringValue;
            }
            else
            {
                MainClass.Halledaten[7] = Hauptprofil1.StringValue + " x " + Hauptprofil2.StringValue + " x " + Hauptprofil3.StringValue;
            }

            MainClass.Halledaten[8] = Dach.StringValue;
            MainClass.Halledaten[9] = Schneelast.StringValue;
            MainClass.Halledaten[10] = Windlast.StringValue;
            MainClass.Halledaten[11] = Gewicht2.StringValue;

            if(Breite2.StringValue.Length > 0)
            {
                MainClass.Halledaten2[1] = Breite2.StringValue;
            }
            else
            {
                MainClass.Halledaten2[1] = "";
            }
            if (Lange3.StringValue.Length > 0)
            {
                MainClass.Halledaten2[2] = Lange3.StringValue;
            }
            else
            {
                MainClass.Halledaten2[2] = "";
            }
            if (Traufhohe2.StringValue.Length > 0)
            {
                MainClass.Halledaten2[3] = Traufhohe2.StringValue;
            }
            else
            {
                MainClass.Halledaten2[3] = "";
            }
            if (Firsthohe2.StringValue.Length > 0)
            {
                MainClass.Halledaten2[4] = Firsthohe2.StringValue;
            }
            else
            {
                MainClass.Halledaten2[4] = "";
            }
            if (Binderabstand2.StringValue.Length > 0)
            {
                MainClass.Halledaten2[5] = Binderabstand2.StringValue;
            }
            else
            {
                MainClass.Halledaten2[5] = "";
            }
            if (Zugbandhohe2.StringValue.Length > 0)
            {
                MainClass.Halledaten2[6] = Zugbandhohe2.StringValue;
            }
            else
            {
                MainClass.Halledaten2[6] = "";
            }
            if (Schneelast2.StringValue.Length > 0)
            {
                MainClass.Halledaten2[7] = Schneelast2.StringValue;
            }
            else
            {
                MainClass.Halledaten2[7] = "";
            }
            if(Windlast2.StringValue.Length > 0)
            {
                MainClass.Halledaten2[8] = Windlast2.StringValue;
            }
            else
            {
                MainClass.Halledaten2[8] = "";
            }

            MainClass.HalledatenCa[1] = BreiteCa.State.ToString();
            MainClass.HalledatenCa[2] = LangeCa.State.ToString();
            MainClass.HalledatenCa[3] = TraufhoheCa.State.ToString();
            MainClass.HalledatenCa[4] = FirsthoheCa.State.ToString();
            MainClass.HalledatenCa[5] = BinderabstandCa.State.ToString();
            MainClass.HalledatenCa[6] = ZugbandhoheCa.State.ToString();

            MainClass.CenaMontaz = CenaMontaz.StringValue;

            MainClass.Oferta[0] = Lieferzeit.StringValue;
            MainClass.Oferta[1] = AngebotNr.StringValue;

            MainClass.ToreUndTuren = ToreUndTuren.StringValue;
            MainClass.Montage = ""; //Montage.StringValue;
            //MainClass.VonIhnen = VonIhnen.StringValue;

            MainClass.Technische[0] = Starke1.StringValue;
            MainClass.Technische[1] = UWert1.StringValue;
            MainClass.Technische[2] = Starke2.StringValue;
            MainClass.Technische[3] = UWert2.StringValue;
            MainClass.Technische[4] = Gewicht.StringValue;
            MainClass.Technische[5] = KWert1.StringValue;

            MainClass.Select3 = Convert.ToInt32(Switch3.SelectedSegment);

            MainClass.Nawiasy[0] = BreiteN.State.ToString();
            MainClass.Nawiasy[1] = LangeN.State.ToString();
            MainClass.Nawiasy[2] = TraufhoheN.State.ToString();
            MainClass.Nawiasy[3] = FirsthoheN.State.ToString();

        }
        //public static int pozycja1;
        partial void SwitchTab1_Click(NSObject sender)
        {
            if (SwitchTab1.SelectedSegment == 0)
            {
                Tab1.Hidden = false;
                Tab2.Hidden = true;
                Tab3.Hidden = true;
                Tab4.Hidden = true;
                Tab5.Hidden = true;
                Tab6.Hidden = true;
                Tab7.Hidden = true;
            }
            else if (SwitchTab1.SelectedSegment == 1)
            {
                Tab1.Hidden = true;
                Tab2.Hidden = false;
                Tab3.Hidden = true;
                Tab4.Hidden = true;
                Tab5.Hidden = true;
                Tab6.Hidden = true;
                Tab7.Hidden = true;
                //Load();
            }
            else if (SwitchTab1.SelectedSegment == 2)
            {
                Tab1.Hidden = true;
                Tab2.Hidden = true;
                Tab3.Hidden = false;
                Tab4.Hidden = true;
                Tab5.Hidden = true;
                Tab6.Hidden = true;
                Tab7.Hidden = true;
            }
            else if (SwitchTab1.SelectedSegment == 3)
            {
                Console.WriteLine(Stallhalle.State.ToString());
                MainClass.Halledaten[0] = Leichbauhalle.StringValue;
                string[] typ = MainClass.Halledaten[0].Split(" ");
                if(typ.Length > 1)
                {
                    SetTechDate(typ);
                }
                else
                {
                    //InfoLabel1.StringValue = "Niepoprawny typ hali.";
                    GenerateAlert("Błąd", "Aby móc wprowadzać zmiany w tej zakładce, musisz ustawić poprawny typ hali (TT, PT, PP, DPS, ISO)");
                }
            }
            else if(SwitchTab1.SelectedSegment == 4)
            {
                Tab1.Hidden = true;
                Tab2.Hidden = true;
                Tab3.Hidden = true;
                Tab4.Hidden = true;
                Tab5.Hidden = false;
                Tab6.Hidden = true;
                Tab7.Hidden = true;
            }
            else if (SwitchTab1.SelectedSegment == 5)
            {
                Tab1.Hidden = true;
                Tab2.Hidden = true;
                Tab3.Hidden = true;
                Tab4.Hidden = true;
                Tab5.Hidden = true;
                Tab6.Hidden = false;
                Tab7.Hidden = true;
                PriceText1.StringValue = String.Format("{0:0.00}", Convert.ToDouble(AllManager.PoliczSume(MainClass.bazaTabela1_ilosc, MainClass.bazaTabela1_cena, Lieferungskosten.FloatValue).Replace(",", "."))).Replace(".", ",");
                if(Bezeichnung.StringValue.Length < 1)
                {
                    SetBezeichnung();
                }
            }
            else if (SwitchTab1.SelectedSegment == 6)
            {
                Tab1.Hidden = true;
                Tab2.Hidden = true;
                Tab3.Hidden = true;
                Tab4.Hidden = true;
                Tab5.Hidden = true;
                Tab6.Hidden = true;
                Tab7.Hidden = false;
            }
        }

        partial void MailCreateButton_Click(NSObject sender)
        {
            /*
            //wstep
            string str = "Sehr geehrter ";
            if(Plec.SelectedSegment == 0)
            {
                str += "Herr ";
            }
            else
            {
                str += "Frau ";
            }
            str += Name.StringValue + ",";

            //pierwszy akapit
            str += "\n \n";
            str += "wir bedanken uns für Ihre Anfrage und bieten Ihnen freibleibend eine " + Leichbauhalle.StringValue + " , vollisoliert, verkleidet mit ";
            if(Switch3.SelectedSegment == 0)
            {
                str += "Trapezblechen ";
            }
            else
            {
                str += "Sandwichpaneelen ";
            }
            str += Mail_mm.StringValue + " mm, mit der Maßen " + Breite.StringValue + " x " + Lange2.StringValue + " x " + Traufhohe.StringValue + " m, inkl. Lieferung nach " + Mail_miasto.StringValue + " und Montage.";

            //drugi akapit
            str += "\n \n";
            string schneelast = String.Format("{0:0.00}", Convert.ToDouble(Schneelast.StringValue.Replace(",", "."))).Replace(".", ",");
            string windzone = String.Format("{0:0.00}", Convert.ToDouble(Windlast.StringValue.Replace(",", "."))).Replace(".", ",");
            str += "Die angebotene Halle ist mit " + schneelast + "kN/m2 (" + Schneelast.StringValue.Replace(",", "").Replace("0","") + " kg/m2) Schneelast berechnet, was die Zone " + Schneelast2.StringValue;
            str += " für " + Mail_miasto.StringValue + " entspricht (bitte siehe Anhang). Die Windzone " + Windlast2.StringValue + "mit Basisgeschwindigkeitsdruck " + windzone + " kN/m2, ";
            if(Schneelast2.StringValue.Substring(Schneelast2.StringValue.Length -1,1) == "*")
            {
                str += "der Sicherheitsfaktor 2,3 für Norddeutsches Tiefland, und die Höhe von " + NHN.StringValue + " m ü. NHN, wurden bei der Berechnung der Konstruktion berücksichtigt. Die Halle ist für die dauerhafte Aufstellung in der Norddeutschen Tiefebene geeignet.";
            }
            else
            {
                str += "und die Höhe von " + NHN.StringValue + " m ü. NHN, wurden bei der Berechnung der Konstruktion berücksichtigt. Die Halle ist für die dauerhafte Aufstellung geeignet.";
            }

            //trzeci akapit
            str += "\n \n";
            str += "Eine prüffähige Statik nach Eurocode (EC) DIN EN 1991 und Konstruktionspläne gehören zu unserem Lieferumfang. Sie bekommen die Konstruktionspläne für Bauantrag kostenlos vor verbindlicher Bestellung.";

            //czwarty akapit
            str += "\n \n";
            str += "Auf Wunsch erhalten Sie gegen Zahlung einer Schutzgebühr in Höhe von " + Mail_cena.StringValue + " € eine Kopie der statischen Berechnung. Diese Gebühr wird nach erfolgter Montage der Leichtbauhalle in der Schlussrechnung vergütet. Die Wartezeit auf die Statik beträgt momentan 2-3 Wochen.";

            //piaty akapit
            str += "\n \n";
            str += "Wir haben Ihnen eine Beispiel- Halle angeboten. Als Hallen- Hersteller sind wir sehr flexibel und können knapp alle Elemente an Ihre Wünsche anpassen. Falls Sie etwas in der Hallen ändern möchten, erstellen wir Ihnen gerne neues Angebot.";

            //szosty akapit
            str += "\n \n \n";
            str += "Besuchen Sie bitte unsere Facebook- Seite, um mehr über unsere Hallen und Neuigkeiten zu erfahren:";

            //siodmy akapit
            str += "\n \n";
            str += "https://www.facebook.com/itcmetalcon.de";

            //osmy akapit
            str += "\n \n \n";
            str += "Wir bieten Ihnen die Bestpreis- und Qualitätsgarantie. Sollten Sie ein besseres Angebot finden, bitte senden Sie es uns.";

            //dziewaty akapit
            str += "\n \n \n";
            str += "Bei Fragen stehe ich Ihnen gerne zur Verfügung";



            NSPasteboard clipboard = NSPasteboard.GeneralPasteboard;
            clipboard.ClearContents();
            clipboard.WriteObjects(new NSString[] { (NSString)str });
            */

            Mailing.CreateMail1(Plec, Name, Switch3, Mail_mm, Leichbauhalle, Breite, Lange2, Traufhohe, Mail_miasto, Schneelast, Schneelast2, Windlast, Windlast2, NHN, Mail_cena);
            InfoLabel1.StringValue = "Mail został skopiowany.";
            Save();

        }

        partial void MailCreateButton2_Click(NSObject sender)
        {
            Mailing.CreateMail2(Plec, Name, Leichbauhalle, Czas((DateTime)DataOferty.DateValue).ToString("dd.MM.yyyy"));
            InfoLabel1.StringValue = "Mail został skopiowany.";
            Save();
        }

        partial void DefaultButton_Click(NSObject sender)
        {
            SetBezeichnung();
            DataPotwierdzenia.DateValue = NSDate.Now;
        }

        public void SetBezeichnung()
        {
            string text = Leichbauhalle.StringValue;
            string breite = String.Format("{0:0.00}", Convert.ToDouble(Breite.StringValue.Replace(",", "."))).Replace(".", ",");
            string lange = String.Format("{0:0.00}", Convert.ToDouble(Lange2.StringValue.Replace(",", "."))).Replace(".", ",");
            string traufhohe = String.Format("{0:0.00}", Convert.ToDouble(Traufhohe.StringValue.Replace(",", "."))).Replace(".", ",");

            text = text + " " + breite + " x " + lange + " x " + traufhohe + " m";

            string schneelast = String.Format("{0:0.00}", Convert.ToDouble(Schneelast.StringValue.Replace(",", "."))).Replace(".", ",");
            string windlast = String.Format("{0:0.00}", Convert.ToDouble(Windlast.StringValue.Replace(",", "."))).Replace(".", ",");

            text = text + " mit Schneelastzone " + Schneelast2.StringValue + " - " + schneelast + " kN/m², Windzone " + Windlast2.StringValue + " - " + windlast + " kN/m²:\n";

            text = text + "-Dacheindeckung ";
            if (Switch3.SelectedSegment == 0)
            {
                text = text + "Trapezblechen T35 / T18 ";
            }
            else
            {
                text = text + "Sandwichpaneelen ";
            }

            text = text + "Stärke " + Starke1.StringValue + " mm,\n";

            text = text + "-Wandverkleidung ";
            if (Switch3.SelectedSegment == 0)
            {
                text = text + "Trapezblechen T35 / T18 ";
            }
            else
            {
                text = text + "Sandwichpaneelen ";
            }

            text = text + "Stärke " + Starke2.StringValue + " mm,\n";

            string[] Tabela = AllManager.CreateReadyElement(MainClass.bazaTabela1, MainClass.bazaTabela1_x, MainClass.bazaTabela1_y);
            int count = 1;
            do
            {
                text = text + "-" + MainClass.bazaTabela1_ilosc[count] + " " + MainClass.bazaTabela1_jedn[count] + " " + Tabela[count] + ",\n";
                count++;
            } while (count < MainClass.bazaTabela1.Length - 1);

            text = text + "-Lieferung und Montage.";

            Bezeichnung.StringValue = text;
            MainClass.Bezeich = text;
        }

        public void SetTechDate(string[] typ)
        {
            if(typ[typ.Length - 1].Length > 0)
            {
                if (typ[typ.Length - 1].Substring(0, 2) == "TT" || typ[typ.Length - 1].Substring(0, 2) == "PT" || typ[typ.Length - 1].Substring(0, 2) == "PP")
                {
                    string typp = "";
                    if (typ[1].Substring(0, 2) == "TT" || typ[1].Substring(0, 2) == "PT" || typ[1].Substring(0, 2) == "PP")
                    {
                        typp = typ[1].Substring(0, 2);
                    }
                    else if (typ[2].Substring(0, 2) == "TT" || typ[2].Substring(0, 2) == "PT" || typ[2].Substring(0, 2) == "PP")
                    {
                        typp = typ[2].Substring(0, 2);
                    }
                    Tab1.Hidden = true;
                    Tab2.Hidden = true;
                    Tab3.Hidden = true;
                    Tab4.Hidden = false;
                    Tab5.Hidden = true;
                    Tab6.Hidden = true;
                    Tab7.Hidden = true;
                    if (typp == "TT" && (typp != TypHali || Stallhalle.State.ToString() != Stallhallee))
                    {
                        Unterlagen.StringValue = "Eine prüffähige Statik nach Eurocode (EC) DIN EN 1991 / DIN EN 13782 und Konstruktionspläne gehören zu unserem Lieferumfang. Sie bekommen die Konstruktionspläne kostenlos vor verbindlicher Bestellung.";
                        if (Stallhalle.State.ToString() == "Off")
                        {
                            TechnischeDaten.StringValue = "Konstruktion aus Aluminiumkastenprofil mit Zugbändern, mit First- und Eckverbindungen aus feuerverzinktem Stahl. Die feuerverzinkten Fußplatten sind entsprechend den statischen Erfordernissen mittels Erdnägeln (1,00 m lang) auf nicht bindigen, dicht gelagerten Boden (DIN-EN 13782) evtl. Schwerlastdübeln auf Fundamenten (DIN-EN 1991) verankert.";
                        }
                        else
                        {
                            TechnischeDaten.StringValue = "Konstruktion aus Doppel-T-Träger HEA 240 ohne Zugbändern, Baustahl S355, feuerverzinkt nach DIN-EN ISO 1461. Die Fußplatten sind entsprechend den statischen Erfordernissen mittels Schwerlastdübeln auf Fundamenten verankert.\n \n" +
                                "Konstruktion aus Doppel-T-Träger mit Zugbändern (Stiele HE220 / Riegel IPE 270). Die Schienen für Brückenkran aus Doppel-T-Träger HEA240. Alle Konstruktionselemente feuerverzinkt nach DIN-EN ISO 1461. Die Fußplatten sind entsprechend den statischen Erfordernissen mittels Schwerlastdübeln auf Fundamenten verankert.";
                        }
                        TypHali = "TT";
                        Stallhallee = Stallhalle.State.ToString();
                    }
                    else if (typp == "PT" && (typp != TypHali || Stallhalle.State.ToString() != Stallhallee))
                    {
                        Unterlagen.StringValue = "Eine prüffähige Statik nach Eurocode (EC) DIN EN 1991 / DIN EN 13782 und Konstruktionspläne gehören zu unserem Lieferumfang. Sie bekommen die Konstruktionspläne kostenlos vor verbindlicher Bestellung."; if (Stallhalle.State.ToString() == "Off")
                        {
                            TechnischeDaten.StringValue = "Konstruktion aus Aluminiumkastenprofil mit Zugbändern, mit First- und Eckverbindungen aus feuerverzinktem Stahl. Die feuerverzinkten Fußplatten sind entsprechend den statischen Erfordernissen mittels Erdnägeln (1,00 m lang) auf nicht bindigen, dicht gelagerten Boden (DIN-EN 13782) evtl. Schwerlastdübeln auf Fundamenten (DIN-EN 1991) verankert.";
                        }
                        else
                        {
                            TechnischeDaten.StringValue = "Konstruktion aus Doppel-T-Träger mit Zugbändern (Stiele HE220 / Riegel IPE 270). Die Schienen für Brückenkran aus Doppel-T-Träger HEA240. Alle Konstruktionselemente feuerverzinkt nach DIN-EN ISO 1461. Die Fußplatten sind entsprechend den statischen Erfordernissen mittels Schwerlastdübeln auf Fundamenten verankert.";
                        }
                        TypHali = "PT";
                        Stallhallee = Stallhalle.State.ToString();
                    }
                    else if (typp == "PP" && (typp != TypHali || Stallhalle.State.ToString() != Stallhallee))
                    {
                        Unterlagen.StringValue = "Eine prüffähige Statik nach Eurocode (EC) DIN EN 1991 / DIN EN 13782 und Konstruktionspläne gehören zu unserem Lieferumfang. Sie bekommen die Konstruktionspläne kostenlos vor verbindlicher Bestellung."; if (Stallhalle.State.ToString() == "Off")
                        {
                            TechnischeDaten.StringValue = "Konstruktion aus Aluminiumkastenprofil mit Zugbändern, mit First- und Eckverbindungen aus feuerverzinktem Stahl. Die feuerverzinkten Fußplatten sind entsprechend den statischen Erfordernissen mittels Erdnägeln (1,00 m lang) auf nicht bindigen, dicht gelagerten Boden (DIN-EN 13782) evtl. Schwerlastdübeln auf Fundamenten (DIN-EN 1991) verankert.";
                        }
                        else
                        {
                            TechnischeDaten.StringValue = "Konstruktion aus Doppel-T-Träger mit Zugbändern (Stiele HE220 / Riegel IPE 270). Die Schienen für Brückenkran aus Doppel-T-Träger HEA240. Alle Konstruktionselemente feuerverzinkt nach DIN-EN ISO 1461. Die Fußplatten sind entsprechend den statischen Erfordernissen mittels Schwerlastdübeln auf Fundamenten verankert.";
                        }
                        TypHali = "PT";
                        Stallhallee = Stallhalle.State.ToString();
                    }
                }
                else if (typ[typ.Length - 1].Substring(0, 3) == "ISO" || typ[typ.Length - 1].Substring(0, 3) == "DPS")
                {
                    string typp = "";
                    if (typ[1].Substring(0, 3) == "ISO" || typ[1].Substring(0, 3) == "DPS")
                    {
                        typp = typ[1].Substring(0, 3);
                    }
                    else if (typ[2].Substring(0, 3) == "ISO" || typ[2].Substring(0, 3) == "DPS")
                    {
                        typp = typ[2].Substring(0, 3);
                    }
                    Tab1.Hidden = true;
                    Tab2.Hidden = true;
                    Tab3.Hidden = true;
                    Tab4.Hidden = false;
                    Tab5.Hidden = true;
                    Tab6.Hidden = true;
                    Tab7.Hidden = true;
                    if (typp == "ISO" && (typp != TypHali || Stallhalle.State.ToString() != Stallhallee))
                    {
                        if (Stallhalle.State.ToString() == "Off")
                        {
                            TechnischeDaten.StringValue = "Konstruktion aus Aluminiumkastenprofil mit Zugbändern, mit First- und Eckverbindungen aus feuerverzinktem Stahl. Die feuerverzinkten Fußplatten sind entsprechend den statischen Erfordernissen mittels Erdnägeln (1,00 m lang) auf nicht bindigen, dicht gelagerten Boden evtl. Schwerlastdübeln auf Fundamenten verankert.";
                        }
                        else
                        {
                            TechnischeDaten.StringValue = "Konstruktion aus Doppel-T-Träger HEA 240 mit Zugband (Flacheisen), Baustahl S355, feuerverzinkt nach DIN-EN ISO 1461. Die Fußplatten sind entsprechend den statischen Erfordernissen mittels Schwerlastdübeln auf Fundementen verankert.";
                        }
                        Unterlagen.StringValue = "Eine prüffähige Statik nach Eurocode (EC) DIN EN 1991 / DIN EN 13782 und Konstruktionspläne gehören zu unserem Lieferumfang. Sie bekommen die Konstruktionspläne kostenlos vor verbindlicher Bestellung."; TypHali = "ISO";
                        Stallhallee = Stallhalle.State.ToString();
                    }
                    else if (typp == "DPS" && (typp != TypHali || Stallhalle.State.ToString() != Stallhallee))
                    {
                        Unterlagen.StringValue = "Eine prüffähige Statik nach Eurocode (EC) DIN EN 1991 / DIN EN 13782 und Konstruktionspläne gehören zu unserem Lieferumfang. Sie bekommen die Konstruktionspläne kostenlos vor verbindlicher Bestellung."; if (Stallhalle.State.ToString() == "Off")
                        {
                            TechnischeDaten.StringValue = "Konstruktion aus Aluminiumkastenprofil mit Zugbändern, mit First- und Eckverbindungen aus feuerverzinktem Stahl. Die feuerverzinkten Fußplatten sind entsprechend den statischen Erfordernissen mittels Erdnägeln (1,00 m lang) auf nicht bindigen, dicht gelagerten Boden (DIN-EN 13782) evtl. Schwerlastdübeln auf Fundamenten (DIN-EN 1991) verankert.";
                        }
                        else
                        {
                            TechnischeDaten.StringValue = "Konstruktion aus Doppel-T-Träger mit Zugbändern (Stiele HE220 / Riegel IPE 270). Die Schienen für Brückenkran aus Doppel-T-Träger HEA240. Alle Konstruktionselemente feuerverzinkt nach DIN-EN ISO 1461. Die Fußplatten sind entsprechend den statischen Erfordernissen mittels Schwerlastdübeln auf Fundamenten verankert.";
                        }
                        TypHali = "DPS";
                        Stallhallee = Stallhalle.State.ToString();
                    }
                }
                else
                {
                    InfoLabel1.StringValue = "Niepoprawny typ hali.";
                }
            }
            else
            {
                InfoLabel1.StringValue = "Niepoprawny typ hali.";
            }

        }

        partial void ReadyButton1_Click(NSObject sender)
        {
            //lieferungskosten jako ostatni element w tabeli jesli uzyto nowej wersji
            if(Lieferungskosten.StringValue.Length > 0)
            {
                //Console.WriteLine(MainClass.bazaTabela1[MainClass.bazaTabela1.Length - 1].Split(" ")[0]);

                if (MainClass.bazaTabela1[MainClass.bazaTabela1.Length - 1].Split(" ")[0] != "Lieferungskosten")
                {
                    Array.Resize(ref MainClass.bazaTabela1, MainClass.bazaTabela1.Length + 1);
                    Array.Resize(ref MainClass.bazaTabela1_ilosc, MainClass.bazaTabela1_ilosc.Length + 1);
                    Array.Resize(ref MainClass.bazaTabela1_cena, MainClass.bazaTabela1_cena.Length + 1);
                    Array.Resize(ref MainClass.bazaTabela1_x, MainClass.bazaTabela1_x.Length + 1);
                    Array.Resize(ref MainClass.bazaTabela1_y, MainClass.bazaTabela1_y.Length + 1);
                    Array.Resize(ref MainClass.bazaTabela1_jedn, MainClass.bazaTabela1_jedn.Length + 1);
                }

                MainClass.bazaTabela1[MainClass.bazaTabela1.Length - 1] = "Lieferungskosten";
                MainClass.bazaTabela1_ilosc[MainClass.bazaTabela1_ilosc.Length - 1] = 1;
                MainClass.bazaTabela1_cena[MainClass.bazaTabela1_cena.Length - 1] = Lieferungskosten.FloatValue;
                MainClass.bazaTabela1_x[MainClass.bazaTabela1_x.Length - 1] = "0";
                MainClass.bazaTabela1_y[MainClass.bazaTabela1_y.Length - 1] = "0";
                MainClass.bazaTabela1_jedn[MainClass.bazaTabela1_jedn.Length - 1] = "Stk.";
                           
            }

            if (Leichbauhalle.StringValue.Length > 0 && Breite.StringValue.Length > 0 && Lange2.StringValue.Length > 0 && Traufhohe.StringValue.Length > 0 && Firsthohe.StringValue.Length > 0 && Binderabstand.StringValue.Length > 0 && Zugbandhohe.StringValue.Length > 0 && Dach.StringValue.Length > 0 && Schneelast.StringValue.Length > 0 && Windlast.StringValue.Length > 0 && CenaMontaz.StringValue.Length > 0 && AngebotNr.StringValue.Length > 0 )
            {
                if(Stallhalle.State.ToString() == "Off" && Gewicht2.StringValue.Length > 0 && Hauptprofil1.StringValue.Length > 0 && Hauptprofil2.StringValue.Length > 0 && Hauptprofil3.StringValue.Length > 0)
                {
                    MainClass.dataoferty = Czas((DateTime)DataOferty.DateValue);
                    MainClass.datapotwierdzenia = Czas((DateTime)DataPotwierdzenia.DateValue);
                    SetPersonaldaten();
                    SetHalledaten();
                    MainClass.TechnischeDaten = TechnischeDaten.StringValue;
                    MainClass.AufWunsch = AufWunsch.StringValue;
                    MainClass.Select2 = Convert.ToString(Select2.SelectedSegment);
                    MainClass.Stallhalle = Stallhalle.State.ToString();
                    MainClass.HauptGewicht = HauptProfilGewicht.StringValue;
                    string[] typ = MainClass.Halledaten[0].Split(" ");
                    SetTechDate(typ);
                    MainClass.Unterlagen = Unterlagen.StringValue;

                    MainClass.MontageState = Convert.ToString(MontageSwitch.State);
                    MainClass.UnterlagenState = Convert.ToString(UnterlagenSwitch.State);

                    if(Kedernut.SelectedSegment == 0)
                    {
                        MainClass.Kedernut = "4";
                    }
                    else
                    {
                        MainClass.Kedernut = "2";
                    }

                    MainClass.CreatePDF();
                    MainClass.CreateAGB();
                    Save();
                    InfoLabel1.StringValue = "PDF został stworzony.";
                }
                else if(Stallhalle.State.ToString() == "On" && HauptProfilGewicht.StringValue.Length > 0)
                {
                    MainClass.dataoferty = Czas((DateTime)DataOferty.DateValue);
                    MainClass.datapotwierdzenia = Czas((DateTime)DataPotwierdzenia.DateValue);
                    SetPersonaldaten();
                    SetHalledaten();
                    MainClass.TechnischeDaten = TechnischeDaten.StringValue;
                    MainClass.AufWunsch = AufWunsch.StringValue;
                    MainClass.Select2 = Convert.ToString(Select2.SelectedSegment);
                    MainClass.Stallhalle = Stallhalle.State.ToString();
                    MainClass.HauptGewicht = HauptProfilGewicht.StringValue;
                    string[] typ = MainClass.Halledaten[0].Split(" ");
                    SetTechDate(typ);
                    MainClass.Unterlagen = Unterlagen.StringValue;

                    MainClass.MontageState = Convert.ToString(MontageSwitch.State);
                    MainClass.UnterlagenState = Convert.ToString(UnterlagenSwitch.State);

                    if (Kedernut.SelectedSegment == 0)
                    {
                        MainClass.Kedernut = "4";
                    }
                    else
                    {
                        MainClass.Kedernut = "2";
                    }

                    MainClass.CreatePDF();
                    MainClass.CreateAGB();
                    Save();
                    InfoLabel1.StringValue = "PDF został stworzony.";
                }
                else
                {
                    //InfoLabel1.StringValue = "Należy uzupełnić pola.";
                    GenerateAlert("Błąd", "Aby móć stworzyć ofertę, proszę uzupełnić brakujące pola oznaczone kolorem.");
                }
            }
            else
            {
                //InfoLabel1.StringValue = "Należy uzupełnić pola.";
                GenerateAlert("Błąd", "Aby móć stworzyć ofertę, proszę uzupełnić brakujące pola oznaczone kolorem.");
            }

            if(Stallhalle.State.ToString() == "Off")
            {
                if (Gewicht2.StringValue.Length == 0)
                {
                    Gewicht2.BackgroundColor = NSColor.SystemPinkColor;
                }
                else
                {
                    Gewicht2.BackgroundColor = NSColor.White;
                }
                if (Hauptprofil1.StringValue.Length == 0)
                {
                    Hauptprofil1.BackgroundColor = NSColor.SystemPinkColor;
                }
                else
                {
                    Hauptprofil1.BackgroundColor = NSColor.White;
                }
                if (Hauptprofil2.StringValue.Length == 0)
                {
                    Hauptprofil2.BackgroundColor = NSColor.SystemPinkColor;
                }
                else
                {
                    Hauptprofil2.BackgroundColor = NSColor.White;
                }
                if (Hauptprofil3.StringValue.Length == 0)
                {
                    Hauptprofil3.BackgroundColor = NSColor.SystemPinkColor;
                }
                else
                {
                    Hauptprofil3.BackgroundColor = NSColor.White;
                }
            }
            else
            {
                if(HauptProfilGewicht.StringValue.Length == 0)
                {
                    HauptProfilGewicht.BackgroundColor = NSColor.SystemPinkColor;
                }
                else
                {
                    HauptProfilGewicht.BackgroundColor = NSColor.White;
                }
            }

            if (Leichbauhalle.StringValue.Length == 0)
            {
                Leichbauhalle.BackgroundColor = NSColor.SystemPinkColor;
            }
            else
            {
                Leichbauhalle.BackgroundColor = NSColor.White;
            }
            if (Breite.StringValue.Length == 0)
            {
                Breite.BackgroundColor = NSColor.SystemPinkColor;
            }
            else
            {
                Breite.BackgroundColor = NSColor.White;
            }
            if (Lange2.StringValue.Length == 0)
            {
                Lange2.BackgroundColor = NSColor.SystemPinkColor;
            }
            else
            {
                Lange2.BackgroundColor = NSColor.White;
            }
            if (Traufhohe.StringValue.Length == 0)
            {
                Traufhohe.BackgroundColor = NSColor.SystemPinkColor;
            }
            else
            {
                Traufhohe.BackgroundColor = NSColor.White;
            }
            if (Firsthohe.StringValue.Length == 0)
            {
                Firsthohe.BackgroundColor = NSColor.SystemPinkColor;
            }
            else
            {
                Firsthohe.BackgroundColor = NSColor.White;
            }
            if (Binderabstand.StringValue.Length == 0)
            {
                Binderabstand.BackgroundColor = NSColor.SystemPinkColor;
            }
            else
            {
                Binderabstand.BackgroundColor = NSColor.White;
            }
            if (Zugbandhohe.StringValue.Length == 0)
            {
                Zugbandhohe.BackgroundColor = NSColor.SystemPinkColor;
            }
            else
            {
                Zugbandhohe.BackgroundColor = NSColor.White;
            }
            if (Dach.StringValue.Length == 0)
            {
                Dach.BackgroundColor = NSColor.SystemPinkColor;
            }
            else
            {
                Dach.BackgroundColor = NSColor.White;
            }
            if (Schneelast.StringValue.Length == 0)
            {
                Schneelast.BackgroundColor = NSColor.SystemPinkColor;
            }
            else
            {
                Schneelast.BackgroundColor = NSColor.White;
            }
            if (Windlast.StringValue.Length == 0)
            {
                Windlast.BackgroundColor = NSColor.SystemPinkColor;
            }
            else
            {
                Windlast.BackgroundColor = NSColor.White;
            }
           
            if (AngebotNr.StringValue.Length == 0)
            {
                AngebotNr.BackgroundColor = NSColor.SystemPinkColor;
            }
            else
            {
                AngebotNr.BackgroundColor = NSColor.White;
            }
            if (CenaMontaz.StringValue.Length == 0)
            {
                CenaMontaz.BackgroundColor = NSColor.SystemPinkColor;
            }
            else
            {
                CenaMontaz.BackgroundColor = NSColor.White;
            }

            AllManager.LieferungskostenFix(Lieferungskosten);

        }

        partial void TypHali_Click(NSObject sender)
        {
            if(Stallhalle.State.ToString() == "On")
            {
                Hauptprofil1.Enabled = false;
                Hauptprofil2.Enabled = false;
                Hauptprofil3.Enabled = false;
                Gewicht2.Enabled = false;

                HauptProfilGewicht.Enabled = true;
            }
            else
            {
                Hauptprofil1.Enabled = true;
                Hauptprofil2.Enabled = true;
                Hauptprofil3.Enabled = true;
                Gewicht2.Enabled = true;

                HauptProfilGewicht.Enabled = false;
            }
        }

        partial void AddButton1_Click(NSObject sender)
        {
            if (Ilosc1.StringValue == "" || Ilosc1.StringValue == "0")
            {
                InfoLabel1.StringValue = "Ilość nie może być 0.";
            }
            else
            {
                if (ComboBox1.StringValue != "")
                {
                    CheckThis();
                    string jedn = "";
                    if (Select2.SelectedSegment == 0)
                    {
                        jedn = "Stk.";
                    }
                    else if (Select2.SelectedSegment == 1)
                    {
                        jedn = "Lfm.";
                    }
                    else if (Select2.SelectedSegment == 2)
                    {
                        jedn = "m²";
                    }
                    AllManager.MoveValueFromComboBoxToTable(MainClass.bazaTabela1, MainClass.bazaTabela1_ilosc, MainClass.bazaTabela1_cena, MainClass.bazaTabela1_x, MainClass.bazaTabela1_y, MainClass.bazaTabela1_jedn, MainClass.bazaComboBox1, Tabela1, ComboBox1, Ilosc1, Cena1, X1, Y1, jedn, 1);
                    Suma1.StringValue = AllManager.PoliczSume(MainClass.bazaTabela1_ilosc, MainClass.bazaTabela1_cena, Lieferungskosten.FloatValue);
                }
                Ilosc1.StringValue = "";
                Cena1.StringValue = "";
                X1.StringValue = "";
                Y1.StringValue = "";
                Select2.SelectedSegment = 0;
            }
        }
        
        partial void AddButton2_Click(NSObject sender)
        {
            if (Ilosc1.StringValue == "" || Ilosc1.StringValue == "0")
            {
                InfoLabel1.StringValue = "Ilość nie może być 0.";
            }
            else
            {
                if (ComboBox1.StringValue != "")
                {
                    CheckThis();
                    string jedn = "";
                    if (Select2.SelectedSegment == 0)
                    {
                        jedn = "Stk.";
                    }
                    else if (Select2.SelectedSegment == 1)
                    {
                        jedn = "Lfm.";
                    }
                    else if (Select2.SelectedSegment == 2)
                    {
                        jedn = "m²";
                    }
                    AllManager.MoveValueFromComboBoxToTable(MainClass.bazaTabela2, MainClass.bazaTabela2_ilosc, MainClass.bazaTabela2_cena, MainClass.bazaTabela2_x, MainClass.bazaTabela2_y, MainClass.bazaTabela2_jedn, MainClass.bazaComboBox1, Tabela2, ComboBox1, Ilosc1, Cena1, X1, Y1, jedn, 2);
                }
                Ilosc1.StringValue = "";
                Cena1.StringValue = "";
                X1.StringValue = "";
                Y1.StringValue = "";
                Select2.SelectedSegment = 0;
            }
        }

        partial void AddButton3_Click(NSObject sender)
        {
            if(ComboBox2.StringValue != "")
            {
                CheckThis2();
                AllManager.MoveValueFromComboBoxToTable2(MainClass.bazaTabela3, MainClass.bazaComboBox2, Tabela3, ComboBox2);
                RefreshMontage();
            }
        }

        partial void AddButton5_Click(NSObject sender)
        {
            if(ComboBox5.StringValue != "")
            {
                Array.Resize(ref MainClass.bazaTabela5, MainClass.bazaTabela5.Length + 1);
                Array.Resize(ref MainClass.bazaTabela5_data, MainClass.bazaTabela5.Length + 1);
                MainClass.bazaTabela5[MainClass.bazaTabela5.Length - 1] = ComboBox5.StringValue;
                MainClass.bazaTabela5_data[MainClass.bazaTabela5.Length - 1] = Czas((DateTime)Data.DateValue).ToString("yyyy-MM-dd");
                AllManager.RefreshTable5(MainClass.bazaTabela5, MainClass.bazaTabela5_data, Tabela5);
            }
            else
            {
                InfoLabel1.StringValue = "Wybierz formę kontaktu.";
            }
        }

        partial void TestButton3(NSObject sender)
        {
            int a = 0;
            do
            {
                Console.WriteLine(MainClass.bazaTabela3[a]);
                a++;
            } while (a < MainClass.bazaTabela3.Length);
            Console.WriteLine("//");
            a = 0;
            do
            {
                Console.WriteLine(MainClass.bazaComboBox2[a]);
                a++;
            } while (a < MainClass.bazaComboBox2.Length);
        }

        partial void DeleteButton1_Click(NSObject sender)
        {
            AllManager.MoveValueFromTableToComboBox(MainClass.bazaTabela1, MainClass.bazaTabela1_ilosc, MainClass.bazaTabela1_cena, MainClass.bazaTabela1_x, MainClass.bazaTabela1_y, MainClass.bazaTabela1_jedn, MainClass.bazaComboBox1, Tabela1, ComboBox1, MainClass.pozycja1, 1);
            DeleteButton1.Enabled = false;
            WypChange1.Enabled = false;
            Suma1.StringValue = AllManager.PoliczSume(MainClass.bazaTabela1_ilosc, MainClass.bazaTabela1_cena, Lieferungskosten.FloatValue);
        }

        partial void DeleteButton2_Click(NSObject sender)
        {
            AllManager.MoveValueFromTableToComboBox(MainClass.bazaTabela2, MainClass.bazaTabela2_ilosc, MainClass.bazaTabela2_cena, MainClass.bazaTabela2_x, MainClass.bazaTabela2_y, MainClass.bazaTabela2_jedn, MainClass.bazaComboBox1, Tabela2, ComboBox1, MainClass.pozycja2, 2);
            DeleteButton2.Enabled = false;
            WypChange2.Enabled = false;
        }

        partial void DeleteButton3_Click(NSObject sender)
        {
            AllManager.MoveValueFromTableToComboBox2(MainClass.bazaTabela3, MainClass.bazaComboBox2, Tabela3, ComboBox2, MainClass.pozycja3);
            DeleteButton3.Enabled = false;
            WypChange3.Enabled = false;
            RefreshMontage();
        }

        partial void DeleteButton5_Click(NSObject sender)
        {
            AllManager.MoveValueFromTableToNothing5(MainClass.bazaTabela5, MainClass.bazaTabela5_data, Tabela5, MainClass.pozycja5);
            DeleteButton5.Enabled = false;
        }


        partial void Tabela1_Click(NSObject sender)
        {
            if (MainClass.pozycja1 != -1)
            {
                DeleteButton1.Enabled = true;
                WypChange1.Enabled = true;
                ComboBox1.StringValue = MainClass.bazaTabela1[MainClass.pozycja1];
                X1.StringValue = MainClass.bazaTabela1_x[MainClass.pozycja1];
                Y1.StringValue = MainClass.bazaTabela1_y[MainClass.pozycja1];
                Ilosc1.IntValue = MainClass.bazaTabela1_ilosc[MainClass.pozycja1];
                Cena1.FloatValue = MainClass.bazaTabela1_cena[MainClass.pozycja1];
                if(MainClass.bazaTabela1_jedn[MainClass.pozycja1] == "Stk.")
                {
                    Select2.SelectedSegment = 0;
                }
                else if(MainClass.bazaTabela1_jedn[MainClass.pozycja1] == "Lfm.")
                {
                    Select2.SelectedSegment = 1;
                }
                else if(MainClass.bazaTabela1_jedn[MainClass.pozycja1] == "m²")
                {
                    Select2.SelectedSegment = 2;
                }

                if(MainClass.pozycja1 > 0)
                {
                    Up1.Enabled = true;
                }
                else
                {
                    Up1.Enabled = false;
                }
                if (MainClass.pozycja1 < MainClass.bazaTabela1.Length - 1)
                {
                    Down1.Enabled = true;
                }
                else
                {
                    Down1.Enabled = false;
                }
            }
            else
            {
                DeleteButton1.Enabled = false;
                WypChange1.Enabled = false;
                Up1.Enabled = false;
                Down1.Enabled = false;
            }
        }

        partial void WypChange1_Click(NSObject sender)
        {
            WypChange1.Enabled = false;
            MainClass.bazaTabela1[MainClass.pozycja1] = ComboBox1.StringValue;
            MainClass.bazaTabela1_x[MainClass.pozycja1] = X1.StringValue;
            MainClass.bazaTabela1_y[MainClass.pozycja1] = Y1.StringValue;
            MainClass.bazaTabela1_ilosc[MainClass.pozycja1] = Ilosc1.IntValue;
            MainClass.bazaTabela1_cena[MainClass.pozycja1] = Cena1.FloatValue;
            if (Select2.SelectedSegment == 0)
            {
                MainClass.bazaTabela1_jedn[MainClass.pozycja1] = "Stk.";
            }
            else if (Select2.SelectedSegment == 1)
            {
                MainClass.bazaTabela1_jedn[MainClass.pozycja1] = "Lfm.";
            }
            else if (Select2.SelectedSegment == 2)
            {
                MainClass.bazaTabela1_jedn[MainClass.pozycja1] = "m²";
            }
            AllManager.RefreshTable(MainClass.bazaTabela1, MainClass.bazaTabela1_ilosc, MainClass.bazaTabela1_cena, MainClass.bazaTabela1_x, MainClass.bazaTabela1_y, MainClass.bazaTabela1_jedn, Tabela1);
            Suma1.StringValue = AllManager.PoliczSume(MainClass.bazaTabela1_ilosc, MainClass.bazaTabela1_cena, Lieferungskosten.FloatValue);
            Ilosc1.StringValue = "";
            Cena1.StringValue = "";
            X1.StringValue = "";
            Y1.StringValue = "";
            Select2.SelectedSegment = 0;
            ComboBox1.StringValue = "";
        }

        partial void Up1_Click(NSObject sender)
        {
            string temp = MainClass.bazaTabela1[MainClass.pozycja1];
            string temp_x = MainClass.bazaTabela1_x[MainClass.pozycja1];
            string temp_y = MainClass.bazaTabela1_y[MainClass.pozycja1];
            int temp_ilosc = MainClass.bazaTabela1_ilosc[MainClass.pozycja1];
            float temp_cena = MainClass.bazaTabela1_cena[MainClass.pozycja1];
            string temp_jedn = MainClass.bazaTabela1_jedn[MainClass.pozycja1];

            MainClass.bazaTabela1[MainClass.pozycja1] = MainClass.bazaTabela1[MainClass.pozycja1 - 1];
            MainClass.bazaTabela1_x[MainClass.pozycja1] = MainClass.bazaTabela1_x[MainClass.pozycja1 - 1];
            MainClass.bazaTabela1_y[MainClass.pozycja1] = MainClass.bazaTabela1_y[MainClass.pozycja1 - 1];
            MainClass.bazaTabela1_ilosc[MainClass.pozycja1] = MainClass.bazaTabela1_ilosc[MainClass.pozycja1 - 1];
            MainClass.bazaTabela1_cena[MainClass.pozycja1] = MainClass.bazaTabela1_cena[MainClass.pozycja1 - 1];
            MainClass.bazaTabela1_jedn[MainClass.pozycja1] = MainClass.bazaTabela1_jedn[MainClass.pozycja1 - 1];

            MainClass.pozycja1 = MainClass.pozycja1 - 1;

            MainClass.bazaTabela1[MainClass.pozycja1] = temp;
            MainClass.bazaTabela1_x[MainClass.pozycja1] = temp_x;
            MainClass.bazaTabela1_y[MainClass.pozycja1] = temp_y;
            MainClass.bazaTabela1_ilosc[MainClass.pozycja1] = temp_ilosc;
            MainClass.bazaTabela1_cena[MainClass.pozycja1] = temp_cena;
            MainClass.bazaTabela1_jedn[MainClass.pozycja1] = temp_jedn;

            AllManager.LieferungskostenFix(Lieferungskosten);
            AllManager.RefreshTable(MainClass.bazaTabela1, MainClass.bazaTabela1_ilosc, MainClass.bazaTabela1_cena, MainClass.bazaTabela1_x, MainClass.bazaTabela1_y, MainClass.bazaTabela1_jedn, Tabela1);

            DeleteButton1.Enabled = false;
            WypChange1.Enabled = false;
            Up1.Enabled = false;
            Down1.Enabled = false;

            Ilosc1.StringValue = "";
            Cena1.StringValue = "";
            X1.StringValue = "";
            Y1.StringValue = "";
            Select2.SelectedSegment = 0;
            ComboBox1.StringValue = "";
        }

        partial void Down1_Click(NSObject sender)
        {
            string temp = MainClass.bazaTabela1[MainClass.pozycja1];
            string temp_x = MainClass.bazaTabela1_x[MainClass.pozycja1];
            string temp_y = MainClass.bazaTabela1_y[MainClass.pozycja1];
            int temp_ilosc = MainClass.bazaTabela1_ilosc[MainClass.pozycja1];
            float temp_cena = MainClass.bazaTabela1_cena[MainClass.pozycja1];
            string temp_jedn = MainClass.bazaTabela1_jedn[MainClass.pozycja1];

            MainClass.bazaTabela1[MainClass.pozycja1] = MainClass.bazaTabela1[MainClass.pozycja1 + 1];
            MainClass.bazaTabela1_x[MainClass.pozycja1] = MainClass.bazaTabela1_x[MainClass.pozycja1 + 1];
            MainClass.bazaTabela1_y[MainClass.pozycja1] = MainClass.bazaTabela1_y[MainClass.pozycja1 + 1];
            MainClass.bazaTabela1_ilosc[MainClass.pozycja1] = MainClass.bazaTabela1_ilosc[MainClass.pozycja1 + 1];
            MainClass.bazaTabela1_cena[MainClass.pozycja1] = MainClass.bazaTabela1_cena[MainClass.pozycja1 + 1];
            MainClass.bazaTabela1_jedn[MainClass.pozycja1] = MainClass.bazaTabela1_jedn[MainClass.pozycja1 + 1];

            MainClass.pozycja1 = MainClass.pozycja1 + 1;

            MainClass.bazaTabela1[MainClass.pozycja1] = temp;
            MainClass.bazaTabela1_x[MainClass.pozycja1] = temp_x;
            MainClass.bazaTabela1_y[MainClass.pozycja1] = temp_y;
            MainClass.bazaTabela1_ilosc[MainClass.pozycja1] = temp_ilosc;
            MainClass.bazaTabela1_cena[MainClass.pozycja1] = temp_cena;
            MainClass.bazaTabela1_jedn[MainClass.pozycja1] = temp_jedn;

            AllManager.LieferungskostenFix(Lieferungskosten);
            AllManager.RefreshTable(MainClass.bazaTabela1, MainClass.bazaTabela1_ilosc, MainClass.bazaTabela1_cena, MainClass.bazaTabela1_x, MainClass.bazaTabela1_y, MainClass.bazaTabela1_jedn, Tabela1);

            DeleteButton1.Enabled = false;
            WypChange1.Enabled = false;
            Up1.Enabled = false;
            Down1.Enabled = false;

            Ilosc1.StringValue = "";
            Cena1.StringValue = "";
            X1.StringValue = "";
            Y1.StringValue = "";
            Select2.SelectedSegment = 0;
            ComboBox1.StringValue = "";
        }

        partial void Tabela2_Click(NSObject sender)
        {
            if (MainClass.pozycja2 != -1)
            {
                DeleteButton2.Enabled = true;
                WypChange2.Enabled = true;
                ComboBox1.StringValue = MainClass.bazaTabela2[MainClass.pozycja2];
                X1.StringValue = MainClass.bazaTabela2_x[MainClass.pozycja2];
                Y1.StringValue = MainClass.bazaTabela2_y[MainClass.pozycja2];
                Ilosc1.IntValue = MainClass.bazaTabela2_ilosc[MainClass.pozycja2];
                Cena1.FloatValue = MainClass.bazaTabela2_cena[MainClass.pozycja2];
                if (MainClass.bazaTabela2_jedn[MainClass.pozycja2] == "Stk.")
                {
                    Select2.SelectedSegment = 0;
                }
                else if (MainClass.bazaTabela2_jedn[MainClass.pozycja2] == "Lfm.")
                {
                    Select2.SelectedSegment = 1;
                }
                else if (MainClass.bazaTabela2_jedn[MainClass.pozycja2] == "m²")
                {
                    Select2.SelectedSegment = 2;
                }

                if (MainClass.pozycja2 > 0)
                {
                    Up2.Enabled = true;
                }
                else
                {
                    Up2.Enabled = false;
                }
                if (MainClass.pozycja2 < MainClass.bazaTabela2.Length - 1)
                {
                    Down2.Enabled = true;
                }
                else
                {
                    Down2.Enabled = false;
                }
            }
            else
            {
                DeleteButton2.Enabled = false;
                WypChange2.Enabled = false;
                Up2.Enabled = false;
                Down2.Enabled = false;
            }
        }

        partial void WypChange2_Click(NSObject sender)
        {
            WypChange2.Enabled = false;
            MainClass.bazaTabela2[MainClass.pozycja2] = ComboBox1.StringValue;
            MainClass.bazaTabela2_x[MainClass.pozycja2] = X1.StringValue;
            MainClass.bazaTabela2_y[MainClass.pozycja2] = Y1.StringValue;
            MainClass.bazaTabela2_ilosc[MainClass.pozycja2] = Ilosc1.IntValue;
            MainClass.bazaTabela2_cena[MainClass.pozycja2] = Cena1.FloatValue;
            if (Select2.SelectedSegment == 0)
            {
                MainClass.bazaTabela2_jedn[MainClass.pozycja2] = "Stk.";
            }
            else if (Select2.SelectedSegment == 1)
            {
                MainClass.bazaTabela2_jedn[MainClass.pozycja2] = "Lfm.";
            }
            else if (Select2.SelectedSegment == 2)
            {
                MainClass.bazaTabela2_jedn[MainClass.pozycja2] = "m²";
            }
            AllManager.RefreshTable(MainClass.bazaTabela2, MainClass.bazaTabela2_ilosc, MainClass.bazaTabela2_cena, MainClass.bazaTabela2_x, MainClass.bazaTabela2_y, MainClass.bazaTabela2_jedn, Tabela2);
            Ilosc1.StringValue = "";
            Cena1.StringValue = "";
            X1.StringValue = "";
            Y1.StringValue = "";
            Select2.SelectedSegment = 0;
            ComboBox1.StringValue = "";
        }

        partial void Up2_Click(NSObject sender)
        {
            string temp = MainClass.bazaTabela2[MainClass.pozycja2];
            string temp_x = MainClass.bazaTabela2_x[MainClass.pozycja2];
            string temp_y = MainClass.bazaTabela2_y[MainClass.pozycja2];
            int temp_ilosc = MainClass.bazaTabela2_ilosc[MainClass.pozycja2];
            float temp_cena = MainClass.bazaTabela2_cena[MainClass.pozycja2];
            string temp_jedn = MainClass.bazaTabela2_jedn[MainClass.pozycja2];

            MainClass.bazaTabela2[MainClass.pozycja2] = MainClass.bazaTabela2[MainClass.pozycja2 - 1];
            MainClass.bazaTabela2_x[MainClass.pozycja2] = MainClass.bazaTabela2_x[MainClass.pozycja2 - 1];
            MainClass.bazaTabela2_y[MainClass.pozycja2] = MainClass.bazaTabela2_y[MainClass.pozycja2 - 1];
            MainClass.bazaTabela2_ilosc[MainClass.pozycja2] = MainClass.bazaTabela2_ilosc[MainClass.pozycja2 - 1];
            MainClass.bazaTabela2_cena[MainClass.pozycja2] = MainClass.bazaTabela2_cena[MainClass.pozycja2 - 1];
            MainClass.bazaTabela2_jedn[MainClass.pozycja2] = MainClass.bazaTabela2_jedn[MainClass.pozycja2 - 1];

            MainClass.pozycja2 = MainClass.pozycja2 - 1;

            MainClass.bazaTabela2[MainClass.pozycja2] = temp;
            MainClass.bazaTabela2_x[MainClass.pozycja2] = temp_x;
            MainClass.bazaTabela2_y[MainClass.pozycja2] = temp_y;
            MainClass.bazaTabela2_ilosc[MainClass.pozycja2] = temp_ilosc;
            MainClass.bazaTabela2_cena[MainClass.pozycja2] = temp_cena;
            MainClass.bazaTabela2_jedn[MainClass.pozycja2] = temp_jedn;

            AllManager.RefreshTable(MainClass.bazaTabela2, MainClass.bazaTabela2_ilosc, MainClass.bazaTabela2_cena, MainClass.bazaTabela2_x, MainClass.bazaTabela2_y, MainClass.bazaTabela2_jedn, Tabela2);

            DeleteButton2.Enabled = false;
            WypChange2.Enabled = false;
            Up2.Enabled = false;
            Down2.Enabled = false;

            Ilosc1.StringValue = "";
            Cena1.StringValue = "";
            X1.StringValue = "";
            Y1.StringValue = "";
            Select2.SelectedSegment = 0;
            ComboBox1.StringValue = "";
        }

        partial void Down2_Click(NSObject sender)
        {
            string temp = MainClass.bazaTabela2[MainClass.pozycja2];
            string temp_x = MainClass.bazaTabela2_x[MainClass.pozycja2];
            string temp_y = MainClass.bazaTabela2_y[MainClass.pozycja2];
            int temp_ilosc = MainClass.bazaTabela2_ilosc[MainClass.pozycja2];
            float temp_cena = MainClass.bazaTabela2_cena[MainClass.pozycja2];
            string temp_jedn = MainClass.bazaTabela2_jedn[MainClass.pozycja2];

            MainClass.bazaTabela2[MainClass.pozycja2] = MainClass.bazaTabela2[MainClass.pozycja2 + 1];
            MainClass.bazaTabela2_x[MainClass.pozycja2] = MainClass.bazaTabela2_x[MainClass.pozycja2 + 1];
            MainClass.bazaTabela2_y[MainClass.pozycja2] = MainClass.bazaTabela2_y[MainClass.pozycja2 + 1];
            MainClass.bazaTabela2_ilosc[MainClass.pozycja2] = MainClass.bazaTabela2_ilosc[MainClass.pozycja2 + 1];
            MainClass.bazaTabela2_cena[MainClass.pozycja2] = MainClass.bazaTabela2_cena[MainClass.pozycja2 + 1];
            MainClass.bazaTabela2_jedn[MainClass.pozycja2] = MainClass.bazaTabela2_jedn[MainClass.pozycja2 + 1];

            MainClass.pozycja2 = MainClass.pozycja2 + 1;

            MainClass.bazaTabela2[MainClass.pozycja2] = temp;
            MainClass.bazaTabela2_x[MainClass.pozycja2] = temp_x;
            MainClass.bazaTabela2_y[MainClass.pozycja2] = temp_y;
            MainClass.bazaTabela2_ilosc[MainClass.pozycja2] = temp_ilosc;
            MainClass.bazaTabela2_cena[MainClass.pozycja2] = temp_cena;
            MainClass.bazaTabela2_jedn[MainClass.pozycja2] = temp_jedn;

            AllManager.RefreshTable(MainClass.bazaTabela2, MainClass.bazaTabela2_ilosc, MainClass.bazaTabela2_cena, MainClass.bazaTabela2_x, MainClass.bazaTabela2_y, MainClass.bazaTabela2_jedn, Tabela2);

            DeleteButton2.Enabled = false;
            WypChange2.Enabled = false;
            Up2.Enabled = false;
            Down2.Enabled = false;

            Ilosc1.StringValue = "";
            Cena1.StringValue = "";
            X1.StringValue = "";
            Y1.StringValue = "";
            Select2.SelectedSegment = 0;
            ComboBox1.StringValue = "";
        }

        partial void Tabela3_Click(NSObject sender)
        {
            if(MainClass.pozycja3 != -1)
            {
                DeleteButton3.Enabled = true;
                WypChange3.Enabled = true;
                ComboBox2.StringValue = MainClass.bazaTabela3[MainClass.pozycja3];

                if (MainClass.pozycja3 > 0)
                {
                    Up3.Enabled = true;
                }
                else
                {
                    Up3.Enabled = false;
                }
                if (MainClass.pozycja3 < MainClass.bazaTabela3.Length - 1)
                {
                    Down3.Enabled = true;
                }
                else
                {
                    Down3.Enabled = false;
                }
            }
            else
            {
                DeleteButton3.Enabled = false;
                WypChange3.Enabled = false;
                Up3.Enabled = false;
                Down3.Enabled = false;
            }
        }

        partial void WypChange3_Click(NSObject sender)
        {
            WypChange3.Enabled = false;
            MainClass.bazaTabela3[MainClass.pozycja3] = ComboBox2.StringValue;
            AllManager.RefreshTable2(MainClass.bazaTabela3, Tabela3);
            ComboBox2.StringValue = "";
            CheckThis2();
            RefreshMontage();
        }

        partial void Up3_Click(NSObject sender)
        {
            string temp = MainClass.bazaTabela3[MainClass.pozycja3];

            MainClass.bazaTabela3[MainClass.pozycja3] = MainClass.bazaTabela3[MainClass.pozycja3 - 1];

            MainClass.pozycja3 = MainClass.pozycja3 - 1;

            MainClass.bazaTabela3[MainClass.pozycja3] = temp;

            AllManager.RefreshTable2(MainClass.bazaTabela3, Tabela3);
            RefreshMontage();

            DeleteButton3.Enabled = false;
            WypChange3.Enabled = false;
            Up3.Enabled = false;
            Down3.Enabled = false;

            ComboBox2.StringValue = "";
        }

        partial void Down3_Click(NSObject sender)
        {
            string temp = MainClass.bazaTabela3[MainClass.pozycja3];

            MainClass.bazaTabela3[MainClass.pozycja3] = MainClass.bazaTabela3[MainClass.pozycja3 + 1];

            MainClass.pozycja3 = MainClass.pozycja3 + 1;

            MainClass.bazaTabela3[MainClass.pozycja3] = temp;

            AllManager.RefreshTable2(MainClass.bazaTabela3, Tabela3);
            RefreshMontage();

            DeleteButton3.Enabled = false;
            WypChange3.Enabled = false;
            Up3.Enabled = false;
            Down3.Enabled = false;

            ComboBox2.StringValue = "";
        }

        partial void Tabela5_Click(NSObject sender)
        {
            if(MainClass.pozycja5 != -1)
            {
                DeleteButton5.Enabled = true;
            }
            else
            {
                DeleteButton5.Enabled = false;
            }
        }


        public void Load()
        {
            MainClass.bazaComboBox2[0] = "cos 00x00 eldo";
            MainClass.bazaComboBox2[1] = "nara xdddd 00x00";
            MainClass.bazaComboBox2[2] = "trzy";
            Array.Sort(MainClass.bazaComboBox2);

            AllManager.RefreshComboBox(MainClass.bazaComboBox2, ComboBox2);


            //File.WriteAllLines(Assembly.GetEntryAssembly().Location.Replace("Oferta+.app/Contents/MonoBundle/Oferta+.exe", "Baza1.txt"), new string[] { "jeden", "dwa" });
        }

        partial void ButtonNew1_Click(NSObject sender)
        {
            SwitchTab1.Hidden = false;
            ReadyButton1.Hidden = false;
            SaveButton1.Hidden = false;
            TabNavi.Hidden = false;
            Tab1.Hidden = false;
            Tab0.Hidden = true;
            HauptProfilGewicht.Enabled = false;
            Unterlagen.StringValue = "Eine prüffähige Statik nach Eurocode (EC) DIN EN 1991 / DIN EN 13782 und Konstruktionspläne gehören zu unserem Lieferumfang. Sie bekommen die Konstruktionspläne kostenlos vor verbindlicher Bestellung.";




            //Load();
            //File.Create(Assembly.GetEntryAssembly().Location.Replace("Oferta+.app/Contents/MonoBundle/Oferta+.exe", "Baza1.txt"));



            if (File.Exists(Assembly.GetEntryAssembly().Location.Replace("Oferta+.app/Contents/MonoBundle/Oferta+.exe", "Baza1.txt")))
            {
                MainClass.mainbaza = File.ReadAllText(Assembly.GetEntryAssembly().Location.Replace("Oferta+.app/Contents/MonoBundle/Oferta+.exe", "Baza1.txt")).Split("\n");
                MainClass.mainbaza = MainClass.mainbaza.Where(x => !string.IsNullOrEmpty(x)).ToArray();

                Array.Resize(ref MainClass.mainbaza_cena, MainClass.mainbaza.Length);
                int count = 0;
                do
                {
                    if (MainClass.mainbaza[count].Split("|").Length > 1)
                    {
                        MainClass.mainbaza_cena[count] = MainClass.mainbaza[count].Split("|")[1];
                        MainClass.mainbaza[count] = MainClass.mainbaza[count].Split("|")[0];
                    }
                    else
                    {
                        MainClass.mainbaza_cena[count] = "";
                    }
                    count++;
                } while (count < MainClass.mainbaza.Length);
            }
            else
            {
                File.Create(Assembly.GetEntryAssembly().Location.Replace("Oferta+.app/Contents/MonoBundle/Oferta+.exe", "Baza2.txt"));
            }

            if (File.Exists(Assembly.GetEntryAssembly().Location.Replace("Oferta+.app/Contents/MonoBundle/Oferta+.exe", "Baza2.txt")))
            {
                MainClass.mainbaza2 = File.ReadAllText(Assembly.GetEntryAssembly().Location.Replace("Oferta+.app/Contents/MonoBundle/Oferta+.exe", "Baza2.txt")).Split("\n");
                MainClass.mainbaza2 = MainClass.mainbaza2.Where(x => !string.IsNullOrEmpty(x)).ToArray();
            }
            else
            {
                File.Create(Assembly.GetEntryAssembly().Location.Replace("Oferta+.app/Contents/MonoBundle/Oferta+.exe", "Baza2.txt"));
            }





            Array.Sort(MainClass.mainbaza);
            Array.Resize(ref MainClass.bazaComboBox1, MainClass.mainbaza.Length);
            Array.Copy(MainClass.mainbaza, MainClass.bazaComboBox1, MainClass.mainbaza.Length);
            AllManager.RefreshComboBox(MainClass.bazaComboBox1, ComboBox1);

            Array.Sort(MainClass.mainbaza2);
            Array.Resize(ref MainClass.bazaComboBox2, MainClass.mainbaza2.Length);
            Array.Copy(MainClass.mainbaza2, MainClass.bazaComboBox2, MainClass.mainbaza2.Length);
            AllManager.RefreshComboBox(MainClass.bazaComboBox2, ComboBox2);


        }

        public void CheckThis()
        {
            List<string> lista = new List<string>(MainClass.mainbaza);
            if (!MainClass.mainbaza.Contains(ComboBox1.StringValue)) //!lista.Contains(ComboBox1.StringValue)
            {
                Array.Resize(ref MainClass.mainbaza, MainClass.mainbaza.Length + 1);
                MainClass.mainbaza[MainClass.mainbaza.Length - 1] = ComboBox1.StringValue;

                Array.Resize(ref MainClass.mainbaza_cena, MainClass.mainbaza.Length + 1);
                MainClass.mainbaza_cena[MainClass.mainbaza.Length - 1] = Cena1.StringValue;
            }
            else if (MainClass.mainbaza_cena[Array.IndexOf(MainClass.mainbaza, ComboBox1.StringValue)] == "")
            {
                MainClass.mainbaza_cena[Array.IndexOf(MainClass.mainbaza, ComboBox1.StringValue)] = Cena1.StringValue;
            }

            string[] baza = new string[MainClass.mainbaza.Length];
            int count = 0;
            do
            {
                baza[count] = MainClass.mainbaza[count] + "|" + MainClass.mainbaza_cena[count];
                count++;
            } while (count < MainClass.mainbaza.Length);

            File.WriteAllLines(Assembly.GetEntryAssembly().Location.Replace("Oferta+.app/Contents/MonoBundle/Oferta+.exe", "Baza1.txt"), baza);
            AllManager.RefreshComboBox(MainClass.mainbaza, ComboBox1);
        }

        public void CheckThis2()
        {
            List<string> lista = new List<string>(MainClass.mainbaza2);
            if (!lista.Contains(ComboBox2.StringValue))
            {
                lista.Add(ComboBox2.StringValue);
                Array.Resize(ref MainClass.mainbaza2, MainClass.mainbaza2.Length + 1);
                MainClass.mainbaza2[MainClass.mainbaza2.Length - 1] = ComboBox2.StringValue;
            }
            File.WriteAllLines(Assembly.GetEntryAssembly().Location.Replace("Oferta+.app/Contents/MonoBundle/Oferta+.exe", "Baza2.txt"), lista.ToArray());
            AllManager.RefreshComboBox(MainClass.mainbaza2, ComboBox2);
        }

        partial void SaveButton1_Click(NSObject sender)
        {
            Save();
            InfoLabel1.StringValue = "Zapisano.";
        }

        public void Save()
        {
            MainClass.TechnischeDaten = TechnischeDaten.StringValue;
            MainClass.AufWunsch = AufWunsch.StringValue;
            MainClass.Select2 = Convert.ToString(Select2.SelectedSegment);
            SetPersonaldaten();
            SetHalledaten();

            string nazwa1 = MainClass.Oferta[1].Replace("/", "") + "_" + MainClass.Halledaten[1] + "x" + MainClass.Halledaten[2] + "m_" + MainClass.Halledaten[0] + "_" + MainClass.Personaldaten[7] + " " + MainClass.Personaldaten[0] + " " + MainClass.Personaldaten[1];
            string[] dane = new string[100];

            //dane personalne
            dane[0] = AngebotNr.StringValue;
            dane[1] = Lieferzeit.StringValue;
            dane[2] = Vorname.StringValue;
            dane[3] = Name.StringValue;
            dane[4] = StraBe.StringValue;
            dane[5] = Stadt.StringValue;
            dane[6] = Postlietzahl.StringValue;
            dane[7] = Telefonnummer.StringValue;
            dane[8] = Firma.StringValue;
            dane[9] = Convert.ToString(Plec.SelectedSegment);

            //dane hali
            dane[10] = Leichbauhalle.StringValue;
            dane[11] = Stallhalle.State.ToString();
            dane[12] = BreiteCa.State.ToString();
            dane[13] = Breite.StringValue;
            dane[14] = Breite2.StringValue;
            dane[15] = LangeCa.State.ToString();
            dane[16] = Lange2.StringValue;
            dane[17] = Lange3.StringValue;
            dane[18] = TraufhoheCa.State.ToString();
            dane[19] = Traufhohe.StringValue;
            dane[20] = Traufhohe2.StringValue;
            dane[21] = FirsthoheCa.State.ToString();
            dane[22] = Firsthohe.StringValue;
            dane[23] = Firsthohe2.StringValue;
            dane[24] = BinderabstandCa.State.ToString();
            dane[25] = Binderabstand.StringValue;
            dane[26] = Binderabstand2.StringValue;
            dane[27] = ZugbandhoheCa.State.ToString();
            dane[28] = Zugbandhohe.StringValue;
            dane[29] = Zugbandhohe2.StringValue;
            dane[30] = Hauptprofil1.StringValue;
            dane[31] = Hauptprofil2.StringValue;
            dane[32] = Hauptprofil3.StringValue;
            dane[33] = Gewicht.StringValue;
            dane[34] = Dach.StringValue;
            dane[35] = Schneelast.StringValue;
            dane[36] = Windlast.StringValue;

            //wyposazenie
            StringBuilder builder = new StringBuilder();

            /*
            foreach(string value in MainClass.bazaTabela1)
            {
                builder.Append(value.Replace(Environment.NewLine, ""));
                builder.Append("||");
            }
            dane[37] = builder.ToString();
            */
            AllManager.LieferungskostenFixBack(Lieferungskosten);

            File.WriteAllLines(Assembly.GetEntryAssembly().Location.Replace("Oferta+.app/Contents/MonoBundle/Oferta+.exe", "Projects/Bazy/" + nazwa1 + "BT1.txt"), MainClass.bazaTabela1);

            builder = new StringBuilder();
            foreach (int value in MainClass.bazaTabela1_ilosc)
            {
                builder.Append(value);
                builder.Append("||");
            }
            dane[38] = builder.ToString();

            builder = new StringBuilder();
            foreach (int value in MainClass.bazaTabela1_cena)
            {
                builder.Append(value);
                builder.Append("||");
            }
            dane[39] = builder.ToString();

            builder = new StringBuilder();
            foreach (string value in MainClass.bazaTabela1_x)
            {
                builder.Append(value.Replace(Environment.NewLine, ""));
                builder.Append("||");
            }
            dane[40] = builder.ToString();

            builder = new StringBuilder();
            foreach (string value in MainClass.bazaTabela1_y)
            {
                builder.Append(value.Replace(Environment.NewLine, ""));
                builder.Append("||");
            }
            dane[41] = builder.ToString();

            builder = new StringBuilder();
            foreach (string value in MainClass.bazaTabela1_jedn)
            {
                builder.Append(value.Replace(Environment.NewLine, ""));
                builder.Append("||");
            }
            dane[42] = builder.ToString();

            AllManager.LieferungskostenFix(Lieferungskosten);

            /*
            builder = new StringBuilder();
            foreach (string value in MainClass.bazaTabela2)
            {
                builder.Append(value.Replace(Environment.NewLine, ""));
                builder.Append("||");
            }
            dane[43] = builder.ToString();
            */
            File.WriteAllLines(Assembly.GetEntryAssembly().Location.Replace("Oferta+.app/Contents/MonoBundle/Oferta+.exe", "Projects/Bazy/" + nazwa1 + "BT2.txt"), MainClass.bazaTabela2);

            builder = new StringBuilder();
            foreach (int value in MainClass.bazaTabela2_ilosc)
            {
                builder.Append(value);
                builder.Append("||");
            }
            dane[44] = builder.ToString();


            builder = new StringBuilder();
            foreach (int value in MainClass.bazaTabela2_cena)
            {
                builder.Append(value);
                builder.Append("||");
            }
            dane[45] = builder.ToString();


            builder = new StringBuilder();
            foreach (string value in MainClass.bazaTabela2_x)
            {
                builder.Append(value.Replace(Environment.NewLine, ""));
                builder.Append("||");
            }
            dane[46] = builder.ToString();


            builder = new StringBuilder();
            foreach (string value in MainClass.bazaTabela2_y)
            {
                builder.Append(value.Replace(Environment.NewLine, ""));
                builder.Append("||");
            }
            dane[47] = builder.ToString();


            builder = new StringBuilder();
            foreach (string value in MainClass.bazaTabela2_jedn)
            {
                builder.Append(value.Replace(Environment.NewLine, ""));
                builder.Append("||");
            }
            dane[48] = builder.ToString();

            dane[49] = ToreUndTuren.StringValue.Replace(Environment.NewLine,"nowa_linia").Replace("\n","nowa_linia");

            /*
            builder = new StringBuilder();
            foreach (string value in MainClass.bazaTabela3)
            {
                builder.Append(value.Replace(Environment.NewLine,""));
                builder.Append("||");
            }
            */
            File.WriteAllLines(Assembly.GetEntryAssembly().Location.Replace("Oferta+.app/Contents/MonoBundle/Oferta+.exe", "Projects/Bazy/" + nazwa1 + "BT3.txt"), MainClass.bazaTabela3);
           // Console.WriteLine(builder.ToString());
            //dane[50] = builder.ToString().Replace(Environment.NewLine,"");

            dane[51] = CenaMontaz.StringValue;

            dane[52] = TechnischeDaten.StringValue.Replace(Environment.NewLine, "nowa_linia");

            dane[53] = AufWunsch.StringValue.Replace(Environment.NewLine, "nowa_linia");

            dane[54] = Starke1.StringValue;
            dane[55] = Starke2.StringValue;
            dane[56] = UWert1.StringValue;
            dane[57] = UWert2.StringValue;
            dane[58] = Gewicht2.StringValue;
            dane[59] = KWert1.StringValue;

            dane[60] = Convert.ToString(Switch3.SelectedSegment);

            dane[61] = HauptProfilGewicht.StringValue;

            dane[62] = BreiteN.State.ToString();
            dane[63] = LangeN.State.ToString();
            dane[64] = TraufhoheN.State.ToString();
            dane[65] = FirsthoheN.State.ToString();

            //akutalizacja- dopisek przy chneelastzone i windzone
            dane[66] = Schneelast2.StringValue;
            dane[67] = Windlast2.StringValue;

            dane[68] = Mail.StringValue;

            dane[69] = Komentarz.StringValue.Replace(Environment.NewLine, "||");

            dane[70] = Unterlagen.StringValue.Replace(Environment.NewLine, "||");

            //aktualizacja generowanie potwierdzenia
            dane[71] = NrPotwierdzenia.StringValue;
            dane[72] = Liefertermin.StringValue;
            dane[73] = PriceText2.StringValue;
            dane[74] = Vat2.StringValue;

            if(Bezeichnung.StringValue.Length > 1)
            {
                dane[77] = Bezeichnung.StringValue.Replace(Environment.NewLine, "nowa_linia");
            }

            //aktualizacja nowe daty
            //dane[75] = Convert.ToString((NSDate)Czas((DateTime)DataOferty.DateValue)).Substring(0, 10); opcja pokazuje zla strefe czasowa
            dane[75] = Czas((DateTime)DataOferty.DateValue).ToString("yyyy-MM-dd");
            dane[76] = Czas((DateTime)DataPotwierdzenia.DateValue).ToString("yyyy-MM-dd");

            //aktualizacja tworzenie maili
            dane[77] = NHN.StringValue;
            dane[78] = Mail_mm.StringValue;
            dane[79] = Mail_cena.StringValue;
            dane[80] = Mail_miasto.StringValue;

            //aktualizacja Bestellungsformular
            dane[81] = Gesamtpreis.StringValue;
            dane[82] = AusstattungOd.StringValue;
            dane[83] = AusstattungDo.StringValue;

            //aktualizacja puste montage i unterlagen
            dane[84] = Convert.ToString(MontageSwitch.State);
            dane[85] = Convert.ToString(MontageSwitch.State);

            //akutalizacja "małe" hale PT
            dane[86] = Convert.ToString(Kedernut.SelectedSegment);

            int count = 0;
            string[] baza5 = new string[0];
            if(MainClass.bazaTabela5.Length > 0)
            {
                Array.Resize(ref baza5, MainClass.bazaTabela5.Length);
                do
                {
                    baza5[count] = MainClass.bazaTabela5[count] + "|" + MainClass.bazaTabela5_data[count];
                    count++;
                } while (count < MainClass.bazaTabela5.Length);
            }
            File.WriteAllLines(Assembly.GetEntryAssembly().Location.Replace("Oferta+.app/Contents/MonoBundle/Oferta+.exe", "Projects/Bazy/" + nazwa1 + "BT5.txt"), baza5);

            MainClass.bazaTabela4 = MontageAGB.StringValue.Split(Environment.NewLine);
            File.WriteAllLines(Assembly.GetEntryAssembly().Location.Replace("Oferta+.app/Contents/MonoBundle/Oferta+.exe", "Projects/Bazy/" + nazwa1 + "BT4.txt"), MainClass.bazaTabela4);

            File.WriteAllLines(Assembly.GetEntryAssembly().Location.Replace("Oferta+.app/Contents/MonoBundle/Oferta+.exe", "Projects/" + nazwa1 + ".txt"), dane);
        }

        public string[] lista;

        public void ChechFiles(string text)
        {
            //lista plikow
            lista = Directory.GetFiles(Assembly.GetEntryAssembly().Location.Replace("Oferta+.app/Contents/MonoBundle/Oferta+.exe", "Projects/"));
            if(lista.Length > 0)
            {
                int count = 0;
                do
                {
                    lista[count] = lista[count].Replace(Assembly.GetEntryAssembly().Location.Replace("Oferta+.app/Contents/MonoBundle/Oferta+.exe", "Projects/"), "");
                    lista[count] = lista[count].Replace(".txt", "");
                    count++;
                } while (count < lista.Length);
            }

            //ewentualne wyszukiwanie plikow
            if(text.Length > 0)
            {
                string[] lista2 = lista;
                lista = new string[0];

                for (int i = 0; i < lista2.Length; i++)
                {
                    for (int j = 0; j < lista2[i].Length - text.Length; j++)
                    {
                        if(lista2[i].Substring(j, text.Length) == text)
                        {
                            Array.Resize(ref lista, lista.Length + 1);
                            lista[lista.Length - 1] = lista2[i];
                            break;
                        }
                    }
                }
            }

            Array.Reverse(lista);

            //wywalenie .DS_Store
            if(lista.Length > 0)
            {
                if (lista[lista.Length - 1] == ".DS_Store")
                {
                    Array.Resize(ref lista, lista.Length - 1);
                }
            }


            //ladowanie do tabeli
            var DataSource = new ProductTableDataSource();
            if (lista.Length == 0)
            {
                DataSource.Products.Add(new Product("", "", ""));
            }
            else
            {
                int count = 0;
                do
                {
                    DataSource.Products.Add(new Product(lista[count], "", ""));
                    count++;
                } while (count < lista.Length);
            }
            ListaOferty.DataSource = DataSource;
            ListaOferty.Delegate = new ProductTableDelegate(DataSource);
        }

        partial void AdvancedSearchButton_Click(NSObject sender)
        {
            CheckFiles2();
        }

        public void CheckFiles2()
        {
            //lista plikow
            lista = Directory.GetFiles(Assembly.GetEntryAssembly().Location.Replace("Oferta+.app/Contents/MonoBundle/Oferta+.exe", "Projects/"));
            if (lista.Length > 0)
            {
                int count = 0;
                do
                {
                    lista[count] = lista[count].Replace(Assembly.GetEntryAssembly().Location.Replace("Oferta+.app/Contents/MonoBundle/Oferta+.exe", "Projects/"), "");
                    lista[count] = lista[count].Replace(".txt", "");
                    count++;
                } while (count < lista.Length);
            }

            //wyszukiwanie plikow
            string[] lista2 = lista;
            lista = new string[0];

            for (int i = 0; i < lista2.Length; i++)
            {
                /*
                for (int j = 0; j < lista2[i].Length - text.Length; j++)
                {
                    if (lista2[i].Substring(j, text.Length) == text)
                    {
                        Array.Resize(ref lista, lista.Length + 1);
                        lista[lista.Length - 1] = lista2[i];
                        break;
                    }
                }
                */
                if(lista2[i] == ".DS_Store")
                {
                    continue;
                }

                string[] dane = File.ReadAllLines(Assembly.GetEntryAssembly().Location.Replace("Oferta+.app/Contents/MonoBundle/Oferta+.exe", "Projects/") + lista2[i] + ".txt");
                bool szerokosc = false;
                if(SearchSzerokosc.StringValue.Length > 0)
                {
                    if (dane[13] == SearchSzerokosc.StringValue)
                    {
                        szerokosc = true;
                    }
                    else
                    {
                        continue;
                    }
                }
                else
                {
                    szerokosc = true;
                }

                bool wysokosc = false;
                if(SearchWysokosc.StringValue.Length > 0)
                {
                    if(dane[19] == SearchWysokosc.StringValue)
                    {
                        wysokosc = true;
                    }
                    else
                    {
                        continue;
                    }
                }
                else
                {
                    wysokosc = true;
                }

                bool dlugosc = false;
                if(SearchDlugosc.StringValue.Length > 0)
                {
                    if(dane[16] == SearchDlugosc.StringValue)
                    {
                        dlugosc = true;
                    }
                    else
                    {
                        continue;
                    }
                }
                else
                {
                    dlugosc = true;
                }

                bool schneelast = false;
                if(SearchSchneelast.StringValue.Length > 0)
                {
                    if(dane[35] == SearchSchneelast.StringValue)
                    {
                        schneelast = true;
                    }
                    else
                    {
                        continue;
                    }
                }
                else
                {
                    schneelast = true;
                }

                bool windlast = false;
                if(SearchWindlast.StringValue.Length > 0)
                {
                    if(dane[36] == SearchWindlast.StringValue)
                    {
                        windlast = true;
                    }
                    else
                    {
                        continue;
                    }
                }
                else
                {
                    windlast = true;
                }
                bool typ = false;
                if(SearchTypHali.StringValue.Length > 0)
                {
                    if(dane[10].Length > 0)
                    {
                        if(dane[10].Split(" ").Length > 1)
                        {
                            if(dane[10].Split(" ").Length > 2)
                            {
                                if(dane[10].Split(" ")[2] == SearchTypHali.StringValue)
                                {
                                    typ = true;
                                }
                                else
                                {
                                    continue;
                                }
                            }
                            else
                            {
                                if(dane[10].Split(" ")[1] == SearchTypHali.StringValue)
                                {
                                    typ = true;
                                }
                                else
                                {
                                    continue;
                                }
                            }
                        }
                        else
                        {
                            continue;
                        }
                    }
                    else
                    {
                        continue;
                    }
                }
                else
                {
                    typ = true;
                }

                if (szerokosc == true && wysokosc == true && dlugosc == true && schneelast == true && windlast == true && typ == true)
                {
                    Array.Resize(ref lista, lista.Length + 1);
                    lista[lista.Length - 1] = lista2[i];
                }




            }

            Array.Reverse(lista);

            //wywalenie .DS_Store
            /*
            if (lista.Length > 0)
            {
                if (lista[lista.Length - 1] == ".DS_Store")
                {
                    Array.Resize(ref lista, lista.Length - 1);
                }
            }
            */


            //ladowanie do tabeli
            var DataSource = new ProductTableDataSource();
            if (lista.Length == 0)
            {
                DataSource.Products.Add(new Product("", "", ""));
            }
            else
            {
                int count = 0;
                do
                {
                    DataSource.Products.Add(new Product(lista[count], "", ""));
                    count++;
                } while (count < lista.Length);
            }
            ListaOferty.DataSource = DataSource;
            ListaOferty.Delegate = new ProductTableDelegate(DataSource);
        }

        partial void ListaOferty_Click(NSObject sender)
        {
            if(MainClass.pozycja4 != -1)
            {
                OpenButton1.Enabled = true;
                DeleteButton0.Enabled = true;
                ArchiveButton1.Enabled = true;
            }
            else
            {
                OpenButton1.Enabled = false;
                DeleteButton0.Enabled = false;
                ArchiveButton1.Enabled = false;
            }
        }

        partial void ArchiveButton1_Click(NSObject sender)
        {
            GenerateArchive("Zrzut wykonany recznie.");
            OpenButton1.Enabled = false;
            DeleteButton0.Enabled = false;
            ArchiveButton1.Enabled = false;
        }

        public void GenerateArchive(string info)
        {
            DirectoryInfo di = Directory.CreateDirectory(Assembly.GetEntryAssembly().Location.Replace("Oferta+.app/Contents/MonoBundle/Oferta+.exe", "temp"));
            File.Copy(Assembly.GetEntryAssembly().Location.Replace("Oferta+.app/Contents/MonoBundle/Oferta+.exe", "Projects/") + lista[MainClass.pozycja4] + ".txt", Assembly.GetEntryAssembly().Location.Replace("Oferta+.app/Contents/MonoBundle/Oferta+.exe", "temp/") + lista[MainClass.pozycja4] + ".txt");
            File.Copy(Assembly.GetEntryAssembly().Location.Replace("Oferta+.app/Contents/MonoBundle/Oferta+.exe", "Projects/Bazy/") + lista[MainClass.pozycja4] + "BT1.txt", Assembly.GetEntryAssembly().Location.Replace("Oferta+.app/Contents/MonoBundle/Oferta+.exe", "temp/") + lista[MainClass.pozycja4] + "BT1.txt");
            File.Copy(Assembly.GetEntryAssembly().Location.Replace("Oferta+.app/Contents/MonoBundle/Oferta+.exe", "Projects/Bazy/") + lista[MainClass.pozycja4] + "BT2.txt", Assembly.GetEntryAssembly().Location.Replace("Oferta+.app/Contents/MonoBundle/Oferta+.exe", "temp/") + lista[MainClass.pozycja4] + "BT2.txt");
            File.Copy(Assembly.GetEntryAssembly().Location.Replace("Oferta+.app/Contents/MonoBundle/Oferta+.exe", "Projects/Bazy/") + lista[MainClass.pozycja4] + "BT3.txt", Assembly.GetEntryAssembly().Location.Replace("Oferta+.app/Contents/MonoBundle/Oferta+.exe", "temp/") + lista[MainClass.pozycja4] + "BT3.txt");
            File.Copy(Assembly.GetEntryAssembly().Location.Replace("Oferta+.app/Contents/MonoBundle/Oferta+.exe", "Projects/Bazy/") + lista[MainClass.pozycja4] + "BT4.txt", Assembly.GetEntryAssembly().Location.Replace("Oferta+.app/Contents/MonoBundle/Oferta+.exe", "temp/") + lista[MainClass.pozycja4] + "BT4.txt");
            if (File.Exists(Assembly.GetEntryAssembly().Location.Replace("Oferta+.app/Contents/MonoBundle/Oferta+.exe", "Projects/Bazy/") + lista[MainClass.pozycja4] + "BT5.txt"))
            {
                File.Copy(Assembly.GetEntryAssembly().Location.Replace("Oferta+.app/Contents/MonoBundle/Oferta+.exe", "Projects/Bazy/") + lista[MainClass.pozycja4] + "BT5.txt", Assembly.GetEntryAssembly().Location.Replace("Oferta+.app/Contents/MonoBundle/Oferta+.exe", "temp/") + lista[MainClass.pozycja4] + "BT5.txt");
            }
            if (!Directory.Exists(Assembly.GetEntryAssembly().Location.Replace("Oferta+.app/Contents/MonoBundle/Oferta+.exe", "Zrzuty")))
            {
                Directory.CreateDirectory(Assembly.GetEntryAssembly().Location.Replace("Oferta+.app/Contents/MonoBundle/Oferta+.exe", "Zrzuty"));
            }
            File.WriteAllText(Assembly.GetEntryAssembly().Location.Replace("Oferta+.app/Contents/MonoBundle/Oferta+.exe", "temp/") + "info.txt", info);
            if(File.Exists(Assembly.GetEntryAssembly().Location.Replace("Oferta+.app/Contents/MonoBundle/Oferta+.exe", "Zrzuty/") + lista[MainClass.pozycja4] + ".zip"))
            {
                File.Delete(Assembly.GetEntryAssembly().Location.Replace("Oferta+.app/Contents/MonoBundle/Oferta+.exe", "Zrzuty/") + lista[MainClass.pozycja4] + ".zip");
            }
            ZipFile.CreateFromDirectory(Assembly.GetEntryAssembly().Location.Replace("Oferta+.app/Contents/MonoBundle/Oferta+.exe", "temp"), Assembly.GetEntryAssembly().Location.Replace("Oferta+.app/Contents/MonoBundle/Oferta+.exe", "Zrzuty/") + lista[MainClass.pozycja4] + ".zip");
            di.Delete(true);
            GenerateAlert("Gotowe", "Zrzut został wykonany prawidłowo.\nlokalizacja: Zrzuty/" + lista[MainClass.pozycja4] + ".zip");
        }

        partial void DeleteButton0_Click(NSObject sender)
        {
            File.Move(Assembly.GetEntryAssembly().Location.Replace("Oferta+.app/Contents/MonoBundle/Oferta+.exe", "Projects/") + lista[MainClass.pozycja4] + ".txt", Assembly.GetEntryAssembly().Location.Replace("Oferta+.app/Contents/MonoBundle/Oferta+.exe", "Projects/Usuniete/") + lista[MainClass.pozycja4] + ".txt");
            ChechFiles(SearchTextField.StringValue);
            OpenButton1.Enabled = false;
            DeleteButton0.Enabled = false;
            ArchiveButton1.Enabled = false;
        }

        partial void OpenButton1_Click(NSObject sender)
        {

            if (File.Exists(Assembly.GetEntryAssembly().Location.Replace("Oferta+.app/Contents/MonoBundle/Oferta+.exe", "Baza1.txt")))
            {
                MainClass.mainbaza = File.ReadAllText(Assembly.GetEntryAssembly().Location.Replace("Oferta+.app/Contents/MonoBundle/Oferta+.exe", "Baza1.txt")).Split("\n");
                MainClass.mainbaza = MainClass.mainbaza.Where(x => !string.IsNullOrEmpty(x)).ToArray();

                Array.Resize(ref MainClass.mainbaza_cena, MainClass.mainbaza.Length);
                int count = 0;
                do
                {
                    if (MainClass.mainbaza[count].Split("|").Length > 1)
                    {
                        MainClass.mainbaza_cena[count] = MainClass.mainbaza[count].Split("|")[1];
                        MainClass.mainbaza[count] = MainClass.mainbaza[count].Split("|")[0];
                    }
                    else
                    {
                        MainClass.mainbaza_cena[count] = "";
                    }
                    count++;
                } while (count < MainClass.mainbaza.Length);
            }
            else
            {
                File.Create(Assembly.GetEntryAssembly().Location.Replace("Oferta+.app/Contents/MonoBundle/Oferta+.exe", "Baza1.txt"));
            }

            if (File.Exists(Assembly.GetEntryAssembly().Location.Replace("Oferta+.app/Contents/MonoBundle/Oferta+.exe", "Baza2.txt")))
            {
                MainClass.mainbaza2 = File.ReadAllText(Assembly.GetEntryAssembly().Location.Replace("Oferta+.app/Contents/MonoBundle/Oferta+.exe", "Baza2.txt")).Split("\n");
                MainClass.mainbaza2 = MainClass.mainbaza2.Where(x => !string.IsNullOrEmpty(x)).ToArray();
            }
            else
            {
                File.Create(Assembly.GetEntryAssembly().Location.Replace("Oferta+.app/Contents/MonoBundle/Oferta+.exe", "Baza2.txt"));
            }





            Array.Sort(MainClass.mainbaza);
            Array.Resize(ref MainClass.bazaComboBox1, MainClass.mainbaza.Length);
            Array.Copy(MainClass.mainbaza, MainClass.bazaComboBox1, MainClass.mainbaza.Length);
            AllManager.RefreshComboBox(MainClass.bazaComboBox1, ComboBox1);

            Array.Sort(MainClass.mainbaza2);
            Array.Resize(ref MainClass.bazaComboBox2, MainClass.mainbaza2.Length);
            Array.Copy(MainClass.mainbaza2, MainClass.bazaComboBox2, MainClass.mainbaza2.Length);
            AllManager.RefreshComboBox(MainClass.bazaComboBox2, ComboBox2);
            LoadProject();
        }

        partial void ComboBox1_Click(NSObject sender)
        {
            //sugestie cenowe (wylaczam bo chuja dawaly)

            /*
            if (ComboBox1.StringValue != "") 
            {
                if(MainClass.mainbaza.Contains(ComboBox1.StringValue))
                {
                    Cena1.StringValue = MainClass.mainbaza_cena[Array.IndexOf(MainClass.mainbaza, ComboBox1.StringValue)];
                }

            }
            */           

        }

        public static void GenerateAlert(string title, string description)
        {
            NSAlert alert = new NSAlert()
            {
                AlertStyle = NSAlertStyle.Informational,
                InformativeText = description,
                MessageText = title,
            };
            alert.RunModal();
        }

        public void LoadProject()
        {
            try {
            
            string[] dane = File.ReadAllLines(Assembly.GetEntryAssembly().Location.Replace("Oferta+.app/Contents/MonoBundle/Oferta+.exe", "Projects/") + lista[MainClass.pozycja4] + ".txt");

            //dane personalne
            AngebotNr.StringValue = dane[0];
            Lieferzeit.StringValue = dane[1];
            Vorname.StringValue = dane[2];
            Name.StringValue = dane[3];
            StraBe.StringValue = dane[4];
            Stadt.StringValue = dane[5];
            Postlietzahl.StringValue = dane[6];
            Telefonnummer.StringValue = dane[7];
            Firma.StringValue = dane[8];
            Plec.SelectedSegment = Convert.ToInt32(dane[9]);

            //dane hali
            Leichbauhalle.StringValue = dane[10];
            if(dane[10].Length > 1)
            {
                string[] typ = Leichbauhalle.StringValue.Split(" ");
                Console.WriteLine(typ[1].Length);
                if (typ.Length > 2)
                {
                    if (typ[2].Length > 2)
                    {
                        TypHali = typ[2].Substring(0, 3);
                    }
                    else
                    {
                        TypHali = typ[2].Substring(0, 2);
                    }
                }
                else if (typ.Length > 1)
                {
                    if (typ[1].Length > 2)
                    {
                        TypHali = typ[1].Substring(0, 3);
                    }
                        else if (typ[1].Length > 1)
                    {
                        TypHali = typ[1].Substring(0, 2);
                    }

                }
            }

            if (dane[11] == "On")
            {
                Stallhalle.State = NSCellStateValue.On;
            }
            else
            {
                Stallhalle.State = NSCellStateValue.Off;
            }
            Stallhallee = dane[11];
            if (dane[12] == "On")
            {
                BreiteCa.State = NSCellStateValue.On;
            }
            else
            {
                BreiteCa.State = NSCellStateValue.Off;
            }
            Breite.StringValue = dane[13];
            Breite2.StringValue = dane[14];
            if (dane[15] == "On")
            {
                LangeCa.State = NSCellStateValue.On;
            }
            else
            {
                LangeCa.State = NSCellStateValue.Off;
            }
            Lange2.StringValue = dane[16];
            Lange3.StringValue = dane[17];
            if (dane[18] == "On")
            {
                TraufhoheCa.State = NSCellStateValue.On;
            }
            else
            {
                TraufhoheCa.State = NSCellStateValue.Off;
            }
            Traufhohe.StringValue = dane[19];
            Traufhohe2.StringValue = dane[20];
            if (dane[21] == "On")
            {
                FirsthoheCa.State = NSCellStateValue.On;
            }
            else
            {
                FirsthoheCa.State = NSCellStateValue.Off;
            }
            Firsthohe.StringValue = dane[22];
            Firsthohe2.StringValue = dane[23];
            if (dane[24] == "On")
            {
                BinderabstandCa.State = NSCellStateValue.On;
            }
            else
            {
                BinderabstandCa.State = NSCellStateValue.Off;
            }
            Binderabstand.StringValue = dane[25];
            Binderabstand2.StringValue = dane[26];
            if (dane[27] == "On")
            {
                ZugbandhoheCa.State = NSCellStateValue.On;
            }
            else
            {
                ZugbandhoheCa.State = NSCellStateValue.Off;
            }
            Zugbandhohe.StringValue = dane[28];
            Zugbandhohe2.StringValue = dane[29];
            Hauptprofil1.StringValue = dane[30];
            Hauptprofil2.StringValue = dane[31];
            Hauptprofil3.StringValue = dane[32];
            Gewicht.StringValue = dane[33];
            Dach.StringValue = dane[34];
            Schneelast.StringValue = dane[35];
            Windlast.StringValue = dane[36];



            //MainClass.bazaTabela1 = dane[37].Substring(0,dane[37].Length-2).Split("||");
            MainClass.bazaTabela1 = File.ReadAllLines(Assembly.GetEntryAssembly().Location.Replace("Oferta+.app/Contents/MonoBundle/Oferta+.exe", "Projects/Bazy/") + lista[MainClass.pozycja4] + "BT1.txt");
            if(MainClass.bazaTabela1.Length > 0)
            {
                MainClass.bazaTabela1_ilosc = Array.ConvertAll(dane[38].Substring(0, dane[38].Length - 2).Split("||"), int.Parse);
                MainClass.bazaTabela1_cena = Array.ConvertAll(dane[39].Substring(0, dane[39].Length - 2).Split("||"), float.Parse);
                MainClass.bazaTabela1_x = dane[40].Substring(0, dane[40].Length - 2).Split("||");
                MainClass.bazaTabela1_y = dane[41].Substring(0, dane[41].Length - 2).Split("||");
                MainClass.bazaTabela1_jedn = dane[42].Substring(0, dane[42].Length - 2).Split("||");

                    //lieferungskosten przeniesiono do allmanagera
                    AllManager.LieferungskostenFix(Lieferungskosten);
                    
            }


            //MainClass.bazaTabela2 = dane[43].Substring(0, dane[43].Length - 2).Split("||");
            MainClass.bazaTabela2 = File.ReadAllLines(Assembly.GetEntryAssembly().Location.Replace("Oferta+.app/Contents/MonoBundle/Oferta+.exe", "Projects/Bazy/") + lista[MainClass.pozycja4] + "BT2.txt");
            if(MainClass.bazaTabela2.Length > 0)
            {
                MainClass.bazaTabela2_ilosc = Array.ConvertAll(dane[44].Substring(0, dane[44].Length - 2).Split("||"), int.Parse);
                MainClass.bazaTabela2_cena = Array.ConvertAll(dane[45].Substring(0, dane[45].Length - 2).Split("||"), float.Parse);
                MainClass.bazaTabela2_x = dane[46].Substring(0, dane[46].Length - 2).Split("||");
                MainClass.bazaTabela2_y = dane[47].Substring(0, dane[47].Length - 2).Split("||");
                MainClass.bazaTabela2_jedn = dane[48].Substring(0, dane[48].Length - 2).Split("||");
            }

            AllManager.RefreshTable(MainClass.bazaTabela1, MainClass.bazaTabela1_ilosc, MainClass.bazaTabela1_cena, MainClass.bazaTabela1_x, MainClass.bazaTabela1_y, MainClass.bazaTabela1_jedn, Tabela1);
            AllManager.RefreshTable(MainClass.bazaTabela2, MainClass.bazaTabela2_ilosc, MainClass.bazaTabela2_cena, MainClass.bazaTabela2_x, MainClass.bazaTabela2_y, MainClass.bazaTabela2_jedn, Tabela2);

            Suma1.StringValue = AllManager.PoliczSume(MainClass.bazaTabela1_ilosc, MainClass.bazaTabela1_cena, Lieferungskosten.FloatValue);

                //fix przesuniecia linii 
                if(dane[50].Length > 0)
                {
                    if (dane[50].Substring(0, 1) == "-")
                    {
                        ToreUndTuren.StringValue = dane[49].Replace("nowa_linia", Environment.NewLine) + Environment.NewLine + dane[50].Replace("nowa_linia", Environment.NewLine);
                        Array.Reverse(dane);
                        Array.Resize(ref dane, dane.Length - 1);
                        Array.Reverse(dane);
                    }
                }
                else
                {
                    ToreUndTuren.StringValue = dane[49].Replace("nowa_linia", Environment.NewLine);
                }




                //MainClass.bazaTabela3 = dane[50].Substring(0, dane[50].Length - 2).Split("||");
                MainClass.bazaTabela3 = File.ReadAllLines(Assembly.GetEntryAssembly().Location.Replace("Oferta+.app/Contents/MonoBundle/Oferta+.exe", "Projects/Bazy/") + lista[MainClass.pozycja4] + "BT3.txt");


            AllManager.RefreshTable2(MainClass.bazaTabela3, Tabela3);

            CenaMontaz.StringValue = dane[51];

            TechnischeDaten.StringValue = dane[52].Replace("nowa_linia", Environment.NewLine);

            AufWunsch.StringValue = dane[53].Replace("nowa_linia", Environment.NewLine);

            Starke1.StringValue = dane[54];
            Starke2.StringValue = dane[55];
            UWert1.StringValue = dane[56];
            UWert2.StringValue = dane[57];
            Gewicht2.StringValue = dane[58];
            KWert1.StringValue = dane[59];
            //Console.WriteLine(dane[61]);
            Switch3.SelectedSegment = Convert.ToInt32(dane[60]);

            HauptProfilGewicht.StringValue = dane[61];

            Console.WriteLine(dane.Length);

            if(dane[62] == "On")
            {
                BreiteN.State = NSCellStateValue.On;
            }
            else
            {
                BreiteN.State = NSCellStateValue.Off;
            }
            if(dane[63] == "On")
            {
                LangeN.State = NSCellStateValue.On;
            }
            else
            {
                LangeN.State = NSCellStateValue.Off;
            }
            if(dane[64] == "On")
            {
                TraufhoheN.State = NSCellStateValue.On;
            }
            else
            {
                TraufhoheN.State = NSCellStateValue.Off;
            }
            if(dane[65] == "On")
            {
                FirsthoheN.State = NSCellStateValue.On;
            }
            else
            {
                FirsthoheN.State = NSCellStateValue.Off;
            }

            if (Stallhalle.State.ToString() == "On")
            {
                Hauptprofil1.Enabled = false;
                Hauptprofil2.Enabled = false;
                Hauptprofil3.Enabled = false;
                Gewicht2.Enabled = false;

                HauptProfilGewicht.Enabled = true;
            }
            else
            {
                Hauptprofil1.Enabled = true;
                Hauptprofil2.Enabled = true;
                Hauptprofil3.Enabled = true;
                Gewicht2.Enabled = true;

                HauptProfilGewicht.Enabled = false;
            }

            //akutalizacja- dopisek przy chneelastzone i windzone
            Schneelast2.StringValue = dane[66];
            Windlast2.StringValue = dane[67];

            Mail.StringValue = dane[68];

            Komentarz.StringValue = dane[69].Replace("||", Environment.NewLine);

            if(dane[70].Length < 1)
            {
                    Unterlagen.StringValue = "Eine prüffähige Statik nach Eurocode (EC) DIN EN 1991 / DIN EN 13782 und Konstruktionspläne gehören zu unserem Lieferumfang. Sie bekommen die Konstruktionspläne kostenlos vor verbindlicher Bestellung.";
                }
                else
            {
                Unterlagen.StringValue = dane[70].Replace("||", Environment.NewLine);
            }

            //aktualizacja generowanie potwierdzenia
            NrPotwierdzenia.StringValue = dane[71];
            Liefertermin.StringValue = dane[72];
            PriceText2.StringValue = dane[73];
            Vat2.StringValue = dane[74];

            if(dane[77].Length > 1)
            {
                Bezeichnung.StringValue = dane[77].Replace("nowa_linia", Environment.NewLine);
            }

            //nowe daty i zabezpieczenie: jesli nie ma daty pokazuje date utworzenia pliku
            if (dane[75].Length > 1) 
            {
                DataOferty.DateValue = (NSDate)DateTime.SpecifyKind(DateTime.Parse(dane[75]), DateTimeKind.Local);
            }
            else
            {
                string data = new FileInfo(Assembly.GetEntryAssembly().Location.Replace("Oferta+.app/Contents/MonoBundle/Oferta+.exe", "Projects/") + lista[MainClass.pozycja4] + ".txt").CreationTime.ToString("yyyy-MM-dd");
                Console.WriteLine(File.GetCreationTime(Assembly.GetEntryAssembly().Location.Replace("Oferta+.app/Contents/MonoBundle/Oferta+.exe", "Projects/") + lista[MainClass.pozycja4] + ".txt"));
                //DataOferty.DateValue = (NSDate)DateTime.SpecifyKind(DateTime.Parse(data), DateTimeKind.Local);
                DataOferty.DateValue = (NSDate)Czas(DateTime.Parse(data));
            }

            if (dane[76].Length > 1)
            {
                DataPotwierdzenia.DateValue = (NSDate)DateTime.SpecifyKind(DateTime.Parse(dane[76]), DateTimeKind.Local);
            }

            NHN.StringValue = dane[77];
            Mail_mm.StringValue = dane[78];
            Mail_cena.StringValue = dane[79];
            Mail_miasto.StringValue = dane[80];

                //aktualizacja Bestellungsformular
                Gesamtpreis.StringValue = dane[81];
                AusstattungOd.StringValue = dane[82];
                AusstattungDo.StringValue = dane[83];

                //aktualizacja puste montage i unterlagen
                if(dane[84].Length > 0)
                {
                    if(dane[84] == "On")
                    {
                        MontageSwitch.State = NSCellStateValue.On;
                        MontageBox.Hidden = true;
                    }
                    else
                    {
                        MontageSwitch.State = NSCellStateValue.Off;
                    }
                }
                if(dane[85].Length > 0)
                {
                    if(dane[85] == "On")
                    {
                        UnterlagenSwitch.State = NSCellStateValue.On;
                        UnterlagenBox.Hidden = true;
                    }
                    else
                    {
                        UnterlagenSwitch.State = NSCellStateValue.Off;
                    }
                }

                //aktualizacja "małe" hale PT
                if(dane[86].Length > 0)
                {
                    Kedernut.SelectedSegment = Convert.ToInt32(dane[86]);
                }


                //zabezpieczenie przed starymi wersjami
                if (File.Exists(Assembly.GetEntryAssembly().Location.Replace("Oferta+.app/Contents/MonoBundle/Oferta+.exe", "Projects/Bazy/") + lista[MainClass.pozycja4] + "BT4.txt"))
            {
                Console.WriteLine("znaleziono");
                MainClass.bazaTabela4 = File.ReadAllLines(Assembly.GetEntryAssembly().Location.Replace("Oferta+.app/Contents/MonoBundle/Oferta+.exe", "Projects/Bazy/") + lista[MainClass.pozycja4] + "BT4.txt");
            }
            else //jesli pliku jeszcze nie ma tabela załaduje się z odświerzenia
            {
                RefreshMontage();
            }
            if (MainClass.bazaTabela4.Length > 0)
            {
                int count = 0;
                do
                {
                    if(MontageAGB.StringValue.Length > 0)
                    {
                        MontageAGB.StringValue = MontageAGB.StringValue + "\n" + MainClass.bazaTabela4[count];
                    }
                    else
                    {
                        MontageAGB.StringValue = MainClass.bazaTabela4[count];
                    }
                    count++;
                } while (count < MainClass.bazaTabela4.Length);
            }

            if (File.Exists(Assembly.GetEntryAssembly().Location.Replace("Oferta+.app/Contents/MonoBundle/Oferta+.exe", "Projects/Bazy/") + lista[MainClass.pozycja4] + "BT5.txt"))
            {
                string[] baza5 = File.ReadAllLines(Assembly.GetEntryAssembly().Location.Replace("Oferta+.app/Contents/MonoBundle/Oferta+.exe", "Projects/Bazy/") + lista[MainClass.pozycja4] + "BT5.txt");
                if(baza5.Length > 0)
                {
                    Array.Resize(ref MainClass.bazaTabela5, baza5.Length);
                    Array.Resize(ref MainClass.bazaTabela5_data, baza5.Length);
                    int count = 0;
                    do
                    {
                        MainClass.bazaTabela5[count] = baza5[count].Split("|")[0];
                        MainClass.bazaTabela5_data[count] = baza5[count].Split("|")[1];
                        count++;
                    } while (count < baza5.Length);
                    AllManager.RefreshTable5(MainClass.bazaTabela5, MainClass.bazaTabela5_data, Tabela5);
                }
            }

                Tab0.Hidden = true;
                Tab1.Hidden = false;
                SwitchTab1.Hidden = false;
                ReadyButton1.Hidden = false;
                SaveButton1.Hidden = false;
                TabNavi.Hidden = false;

            }//try
            catch(Exception e)//lapanie bledow przy wczytywaniu
            {
                //Console.WriteLine(e.Message + new StackTrace(e, true).GetFrame(0).GetFileLineNumber() + e.ToString());
                NSAlert alert = new NSAlert()
                {
                    AlertStyle = NSAlertStyle.Informational,
                    InformativeText = "Podczas otwierania oferty wystąpił błąd, który to uniemożliwia.\nJeśli wykonasz teraz zrzut oferty, w zrzucie znajdzie się informacja na temat błędu, który wystąpił (ułatwi to naprawę błędu).",
                    MessageText = "Błąd",
                };
                alert.AddButton("Ok"); //1000
                alert.AddButton("Wykonaj zrzut"); //1001
                if(alert.RunModal() == 1001) //kliknieto 'wykonaj zrzut'
                {
                    GenerateArchive(e.ToString());
                }
            }

        }

        public void RefreshMontage()
        {
            MontageAGB.StringValue = "-ein Stromanschluss (230V),";
            if(MainClass.bazaTabela3.Length > 1)
            {
                if(MainClass.bazaTabela3.Length > 2)
                {
                    int count = 1;
                    do
                    {
                        MontageAGB.StringValue = MontageAGB.StringValue + "\n" + "-" + MainClass.bazaTabela3[count].Replace(Environment.NewLine,"") + ",";
                        count++;
                    } while (count < MainClass.bazaTabela3.Length - 1);
                }
                MontageAGB.StringValue = MontageAGB.StringValue + "\n" + "-" + MainClass.bazaTabela3[MainClass.bazaTabela3.Length - 1].Replace(Environment.NewLine,"") + ".";
            }

            Array.Resize(ref MainClass.bazaTabela4, MontageAGB.StringValue.Split(Environment.NewLine).Length);
            MainClass.bazaTabela4 = MontageAGB.StringValue.Split(Environment.NewLine);
        }

        partial void ProgramInfoButton_Click(NSObject sender)
        {
            GenerateAlert("Oferta+", "Program Oferta+ wykonany dla Maja Żerko.\nAutor: Bartosz Wieczorek,\nbartek.wieczorek@outlook.com\nWszystkie prawa zastrzeżone.");
        }

    }
}