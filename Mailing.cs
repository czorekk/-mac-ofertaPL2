using System;
using AppKit;
using Foundation;

namespace Oferta__
{
    public class Mailing
    {
        public Mailing()
        {
        }

        public static void CreateMail1(NSSegmentedControl Plec
                                        , NSTextField Name
                                        , NSSegmentedControl Switch3
                                        , NSTextField Mail_mm
                                        , NSTextField Leichbauhalle
                                        , NSTextField Breite
                                        , NSTextField Lange2
                                        , NSTextField Traufhohe
                                        , NSTextField Mail_miasto
                                        , NSTextField Schneelast
                                        , NSTextField Schneelast2
                                        , NSTextField Windlast
                                        , NSTextField Windlast2
                                        , NSTextField NHN
                                        , NSTextField Mail_cena) //ten zwykly
        {
            //wstep
            string str = "Sehr geehrter ";
            if (Plec.SelectedSegment == 0)
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
            if (Switch3.SelectedSegment == 0)
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
            str += "Die angebotene Halle ist mit " + schneelast + "kN/m2 (" + Schneelast.StringValue.Replace(",", "").Replace("0", "") + " kg/m2) Schneelast berechnet, was die Zone " + Schneelast2.StringValue;
            str += " für " + Mail_miasto.StringValue + " entspricht (bitte siehe Anhang). Die Windzone " + Windlast2.StringValue + "mit Basisgeschwindigkeitsdruck " + windzone + " kN/m2, ";
            if(Schneelast2.StringValue.Length > 0)
            {
                if (Schneelast2.StringValue.Substring(Schneelast2.StringValue.Length - 1, 1) == "*")
                {
                    str += "der Sicherheitsfaktor 2,3 für Norddeutsches Tiefland, und die Höhe von " + NHN.StringValue + " m ü. NHN, wurden bei der Berechnung der Konstruktion berücksichtigt. Die Halle ist für die dauerhafte Aufstellung in der Norddeutschen Tiefebene geeignet.";
                }
                else
                {
                    str += "und die Höhe von " + NHN.StringValue + " m ü. NHN, wurden bei der Berechnung der Konstruktion berücksichtigt. Die Halle ist für die dauerhafte Aufstellung geeignet.";
                }
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

            CopyToClipboard(str);

            /*
            NSPasteboard clipboard = NSPasteboard.GeneralPasteboard;
            clipboard.ClearContents();
            clipboard.WriteObjects(new NSString[] { (NSString)str });
            */           
        }

        public static void CreateMail2(NSSegmentedControl Plec
                                        , NSTextField Name
                                        , NSTextField Leichbauhalle
                                        , string data) //'po ofercie'
        {
            //wstep
            string str = "Sehr geehrter ";
            if (Plec.SelectedSegment == 0)
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
            str += "wir haben Ihnen am " + data + " ein Angebot nr  für " + Leichbauhalle.StringValue + " gesendet. Ich hoffe, dass unser Angebot Ihren Vorstellungen entspricht.";

            //drugi akapit
            str += "\n \n";
            str += "Ich habe probiert, Sie telefonisch zu erreichen - leider ohne erfolg.";

            //trzeci akapit
            str += "\n \n";
            str += "Für eine Rückmeldung wäre ich sehr dankbar.";

            //czwarty akapit
            str += "\n \n \n \n";
            str += "Bei Fragen stehe ich Ihnen gerne zur Verfügung";

            CopyToClipboard(str);
        }

        public static void CopyToClipboard(string text)
        {
            NSPasteboard clipboard = NSPasteboard.GeneralPasteboard;
            clipboard.ClearContents();
            clipboard.WriteObjects(new NSString[] { (NSString)text });
        }
    }
}
