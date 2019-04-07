using System; using System.Linq; using AppKit; using CoreGraphics; using Foundation; using System.Collections; using System.Collections.Generic; 
namespace Oferta__
{
    public class AllManager
    {
        public AllManager()
        {
        }

        public static void RefreshTable(string[] lista, int[] bazaTabela_ilosc, float[] bazaTabela_cena, string[] bazaTabela_x, string[] bazaTabela_y, string[] bazaTabela_jedn, NSTableView tabela)         {              string[] lista2 = CreateReadyElement(lista, bazaTabela_x, bazaTabela_y);              var DataSource = new ProductTableDataSource();             if (lista2.Length == 0)
            {
                DataSource.Products.Add(new Product("", "", ""));
            }             else
            {
                int count = 0;
                do
                {
                    DataSource.Products.Add(new Product(lista2[count], Convert.ToString(bazaTabela_ilosc[count]) + " " + bazaTabela_jedn[count], Convert.ToString(bazaTabela_cena[count]).Replace(".",",")));
                    count++;
                } while (count < lista2.Length);
            }             tabela.DataSource = DataSource;             tabela.Delegate = new ProductTableDelegate(DataSource);          }          public static void LieferungskostenFix(NSTextField lieferung)
        {
            //aktualizacja- lieferungskosten jest w osobnym polu a w tabeli byl na koncu wiec wywalam ostatnie miejsce i przenosze
            if (MainClass.bazaTabela1[MainClass.bazaTabela1.Length - 1].Split(" ")[0] == "Lieferungskosten")
            {
                lieferung.FloatValue = MainClass.bazaTabela1_cena[MainClass.bazaTabela1_cena.Length - 1];

                Array.Resize(ref MainClass.bazaTabela1, MainClass.bazaTabela1.Length - 1);
                Array.Resize(ref MainClass.bazaTabela1_ilosc, MainClass.bazaTabela1_ilosc.Length - 1);
                Array.Resize(ref MainClass.bazaTabela1_cena, MainClass.bazaTabela1_cena.Length - 1);
                Array.Resize(ref MainClass.bazaTabela1_x, MainClass.bazaTabela1_x.Length - 1);
                Array.Resize(ref MainClass.bazaTabela1_y, MainClass.bazaTabela1_y.Length - 1);
                Array.Resize(ref MainClass.bazaTabela1_jedn, MainClass.bazaTabela1_jedn.Length - 1);
            }
        }          public static void LieferungskostenFixBack(NSTextField lieferung)
        {             if (MainClass.bazaTabela1[MainClass.bazaTabela1.Length - 1].Split(" ")[0] != "Lieferungskosten")
            {
                Array.Resize(ref MainClass.bazaTabela1, MainClass.bazaTabela1.Length + 1);
                Array.Resize(ref MainClass.bazaTabela1_ilosc, MainClass.bazaTabela1_ilosc.Length + 1);
                Array.Resize(ref MainClass.bazaTabela1_cena, MainClass.bazaTabela1_cena.Length + 1);
                Array.Resize(ref MainClass.bazaTabela1_x, MainClass.bazaTabela1_x.Length + 1);
                Array.Resize(ref MainClass.bazaTabela1_y, MainClass.bazaTabela1_y.Length + 1);
                Array.Resize(ref MainClass.bazaTabela1_jedn, MainClass.bazaTabela1_jedn.Length + 1);

                MainClass.bazaTabela1[MainClass.bazaTabela1.Length - 1] = "Lieferungskosten";
                MainClass.bazaTabela1_ilosc[MainClass.bazaTabela1_ilosc.Length - 1] = 1;
                MainClass.bazaTabela1_cena[MainClass.bazaTabela1_cena.Length - 1] = lieferung.FloatValue;
                MainClass.bazaTabela1_x[MainClass.bazaTabela1_x.Length - 1] = "0";
                MainClass.bazaTabela1_y[MainClass.bazaTabela1_y.Length - 1] = "0";
                MainClass.bazaTabela1_jedn[MainClass.bazaTabela1_jedn.Length - 1] = "Stk.";
            }
        }          public static void RefreshTable2(string[] lista, NSTableView tabela)         {              var DataSource = new ProductTableDataSource();             if (lista.Length == 0)             {                 DataSource.Products.Add(new Product("", "", ""));             }             else             {                 int count = 0;                 do                 {                     DataSource.Products.Add(new Product(lista[count], "", ""));                     count++;                 } while (count < lista.Length);             }             tabela.DataSource = DataSource;             tabela.Delegate = new ProductTableDelegate(DataSource);          }          public static void RefreshTable5(string[] lista, string[] data, NSTableView tabela)         {              var DataSource = new ProductTableDataSource_Tab5();             if (lista.Length == 0)             {                 DataSource.Products.Add(new Product_Tab5("", ""));             }             else             {                 int count = 0;                 do                 {                     DataSource.Products.Add(new Product_Tab5(lista[count], data[count]));                     count++;                 } while (count < lista.Length);             }             tabela.DataSource = DataSource;             tabela.Delegate = new ProductTableDelegate_Tab5(DataSource);          }               public static void RefreshComboBox(string[] lista, NSComboBox comboBox)         {             List<string> eldo = new List<string>(lista);             comboBox.UsesDataSource = true;             comboBox.DataSource = new BloomTypesDataSource(eldo);          }          public static string[] CreateReadyElement(string[] bazaTabela, string[] bazaTabela_x, string[] bazaTabela_y)
        {
            string[] tabela = new string[bazaTabela.Length];             Array.Copy(bazaTabela, tabela, bazaTabela.Length);             if(bazaTabela.Length > 0)
            {
                int count = 0;
                do
                {
                    int count2 = 0;
                    do
                    {
                        if (bazaTabela[count].Length > 5)
                        {
                            if (bazaTabela[count].Substring(count2, 5) == "00x00")
                            {                                 string x = String.Format("{0:0.00}", Convert.ToDouble(bazaTabela_x[count].Replace(",","."))).Replace(".", ",");                                 string y = String.Format("{0:0.00}", Convert.ToDouble(bazaTabela_y[count].Replace(",","."))).Replace(".", ",");
                                tabela[count] = bazaTabela[count].Substring(0, count2) + x + " x " + y + bazaTabela[count].Substring(count2 + 5, bazaTabela[count].Length - (count2 + 5));
                            }
                        }
                        count2++;
                    } while (count2 < bazaTabela[count].Length - 4);
                    count++;
                } while (count < bazaTabela.Length);
            }             return tabela;
        }                      public static void MoveValueFromComboBoxToTable(string[] bazaTabela, int[] bazaTabela_ilosc, float[] bazaTabela_cena, string[] bazaTabela_x, string[] bazaTabela_y, string[] bazaTabela_jedn, string[] bazaComboBox, NSTableView Tabela, NSComboBox ComboBox, NSTextField Ilosc, NSTextField Cena, NSTextField X, NSTextField Y, string JEDN, int numer)
        {
            //dodanie wartosci do tabeli             Array.Resize(ref bazaTabela, bazaTabela.Length + 1);             bazaTabela[bazaTabela.Length - 1] = ComboBox.StringValue;              Array.Resize(ref bazaTabela_ilosc, bazaTabela_ilosc.Length + 1);             bazaTabela_ilosc[bazaTabela_ilosc.Length - 1] = Ilosc.IntValue;              Array.Resize(ref bazaTabela_cena, bazaTabela_cena.Length + 1);             bazaTabela_cena[bazaTabela_cena.Length - 1] = Cena.FloatValue;              Array.Resize(ref bazaTabela_x, bazaTabela_x.Length + 1);             bazaTabela_x[bazaTabela_x.Length - 1] = X.StringValue;              Array.Resize(ref bazaTabela_y, bazaTabela_y.Length + 1);             bazaTabela_y[bazaTabela_y.Length - 1] = Y.StringValue;              Array.Resize(ref bazaTabela_jedn, bazaTabela_jedn.Length + 1);             bazaTabela_jedn[bazaTabela_jedn.Length - 1] = JEDN;              AllManager.RefreshTable(bazaTabela, bazaTabela_ilosc, bazaTabela_cena, bazaTabela_x, bazaTabela_y, bazaTabela_jedn, Tabela);              //wywalenie wartosci z comboboxa              /*             if(bazaComboBox.Length != 0 && bazaComboBox.Contains(ComboBox.StringValue) == true)             {                 if (Convert.ToInt32(ComboBox.SelectedIndex) != bazaComboBox.Length - 1)                 {                     int pos = Convert.ToInt32(ComboBox.SelectedIndex);                     do                     {                         bazaComboBox[pos] = bazaComboBox[pos + 1];                         pos++;                     } while (pos < bazaComboBox.Length - 1);                 }                 Array.Resize(ref bazaComboBox, bazaComboBox.Length - 1);                 AllManager.RefreshComboBox(bazaComboBox, ComboBox);             }             */             ComboBox.StringValue = "";                                     //ustawianie nowych wartosci list             if(numer == 1)
            {
                MainClass.bazaTabela1 = bazaTabela;                 MainClass.bazaTabela1_ilosc = bazaTabela_ilosc;                 MainClass.bazaTabela1_cena = bazaTabela_cena;                 MainClass.bazaTabela1_x = bazaTabela_x;                 MainClass.bazaTabela1_y = bazaTabela_y;                 MainClass.bazaTabela1_jedn = bazaTabela_jedn;                 MainClass.bazaComboBox1 = bazaComboBox;
            }             else if (numer == 2)
            {
                MainClass.bazaTabela2 = bazaTabela;
                MainClass.bazaTabela2_ilosc = bazaTabela_ilosc;
                MainClass.bazaTabela2_cena = bazaTabela_cena;
                MainClass.bazaTabela2_x = bazaTabela_x;
                MainClass.bazaTabela2_y = bazaTabela_y;                 MainClass.bazaTabela2_jedn = bazaTabela_jedn;
                MainClass.bazaComboBox1 = bazaComboBox;
            }
        }          //druga wersja i innymi typami (zamiast obiektow od raz wrzucam wartosci)         public static void MoveValueFromComboBoxToTable(string[] bazaTabela, int[] bazaTabela_ilosc, float[] bazaTabela_cena, string[] bazaTabela_x, string[] bazaTabela_y, string[] bazaTabela_jedn, string[] bazaComboBox, NSTableView Tabela, string ComboBox, int Ilosc, float Cena, string X, string Y, string JEDN, int numer)         {             //dodanie wartosci do tabeli             Array.Resize(ref bazaTabela, bazaTabela.Length + 1);             bazaTabela[bazaTabela.Length - 1] = ComboBox;              Array.Resize(ref bazaTabela_ilosc, bazaTabela_ilosc.Length + 1);             bazaTabela_ilosc[bazaTabela_ilosc.Length - 1] = Ilosc;              Array.Resize(ref bazaTabela_cena, bazaTabela_cena.Length + 1);             bazaTabela_cena[bazaTabela_cena.Length - 1] = Cena;              Array.Resize(ref bazaTabela_x, bazaTabela_x.Length + 1);             bazaTabela_x[bazaTabela_x.Length - 1] = X;              Array.Resize(ref bazaTabela_y, bazaTabela_y.Length + 1);             bazaTabela_y[bazaTabela_y.Length - 1] = Y;              Array.Resize(ref bazaTabela_jedn, bazaTabela_jedn.Length + 1);             bazaTabela_jedn[bazaTabela_jedn.Length - 1] = JEDN;              AllManager.RefreshTable(bazaTabela, bazaTabela_ilosc, bazaTabela_cena, bazaTabela_x, bazaTabela_y, bazaTabela_jedn, Tabela);              //wywalenie wartosci z comboboxa              /*             if(bazaComboBox.Length != 0 && bazaComboBox.Contains(ComboBox.StringValue) == true)             {                 if (Convert.ToInt32(ComboBox.SelectedIndex) != bazaComboBox.Length - 1)                 {                     int pos = Convert.ToInt32(ComboBox.SelectedIndex);                     do                     {                         bazaComboBox[pos] = bazaComboBox[pos + 1];                         pos++;                     } while (pos < bazaComboBox.Length - 1);                 }                 Array.Resize(ref bazaComboBox, bazaComboBox.Length - 1);                 AllManager.RefreshComboBox(bazaComboBox, ComboBox);             }             */                                     //ustawianie nowych wartosci list             if(numer == 1)             {                 MainClass.bazaTabela1 = bazaTabela;                 MainClass.bazaTabela1_ilosc = bazaTabela_ilosc;                 MainClass.bazaTabela1_cena = bazaTabela_cena;                 MainClass.bazaTabela1_x = bazaTabela_x;                 MainClass.bazaTabela1_y = bazaTabela_y;                 MainClass.bazaTabela1_jedn = bazaTabela_jedn;                 MainClass.bazaComboBox1 = bazaComboBox;             }             else if (numer == 2)             {                 MainClass.bazaTabela2 = bazaTabela;                 MainClass.bazaTabela2_ilosc = bazaTabela_ilosc;                 MainClass.bazaTabela2_cena = bazaTabela_cena;                 MainClass.bazaTabela2_x = bazaTabela_x;                 MainClass.bazaTabela2_y = bazaTabela_y;                 MainClass.bazaTabela2_jedn = bazaTabela_jedn;                 MainClass.bazaComboBox1 = bazaComboBox;             }         }          public static void MoveValueFromComboBoxToTable2(string[] bazaTabela, string[] bazaComboBox, NSTableView Tabela, NSComboBox ComboBox)         {             //dodanie wartosci do tabeli             Array.Resize(ref bazaTabela, bazaTabela.Length + 1);             bazaTabela[bazaTabela.Length - 1] = ComboBox.StringValue;              AllManager.RefreshTable2(bazaTabela, Tabela);              //wywalenie wartosci z comboboxa              /*             if(bazaComboBox.Length != 0 && bazaComboBox.Contains(ComboBox.StringValue) == true)             {                 if (Convert.ToInt32(ComboBox.SelectedIndex) != bazaComboBox.Length - 1)                 {                     int pos = Convert.ToInt32(ComboBox.SelectedIndex);                     do                     {                         bazaComboBox[pos] = bazaComboBox[pos + 1];                         pos++;                     } while (pos < bazaComboBox.Length - 1);                 }                 Array.Resize(ref bazaComboBox, bazaComboBox.Length - 1);                 AllManager.RefreshComboBox(bazaComboBox, ComboBox);             }             */             ComboBox.StringValue = "";              //ustawianie nowych wartosci list             MainClass.bazaTabela3 = bazaTabela;             MainClass.bazaComboBox2 = bazaComboBox;         }              public static void MoveValueFromTableToComboBox(string[] bazaTabela, int[] bazaTabela_ilosc, float[] bazaTabela_cena, string[] bazaTabela_x, string[] bazaTabela_y, string[] bazaTabela_jedn, string[] bazaComboBox, NSTableView Tabela, NSComboBox ComboBox, int pozycja, int numer)
        {             //Console.WriteLine(pozycja);
            if(pozycja != -1 && bazaTabela.Length != 0)             {                 //dodanie wartosci do comboboxa                  /*                 Array.Resize(ref bazaComboBox, bazaComboBox.Length + 1);                 bazaComboBox[bazaComboBox.Length - 1] = bazaTabela[pozycja];                 Array.Sort(bazaComboBox);                 AllManager.RefreshComboBox(bazaComboBox, ComboBox);                 */                                 //wywalenie wartosci z listy                 if (pozycja != bazaTabela.Length - 1)                 {                     int pos = pozycja;                     do                     {                         bazaTabela[pos] = bazaTabela[pos + 1];                         bazaTabela_ilosc[pos] = bazaTabela_ilosc[pos + 1];                         bazaTabela_cena[pos] = bazaTabela_cena[pos + 1];                         bazaTabela_x[pos] = bazaTabela_x[pos + 1];                         bazaTabela_y[pos] = bazaTabela_y[pos + 1];                         bazaTabela_jedn[pos] = bazaTabela_jedn[pos + 1];                         pos++;                     } while (pos < bazaTabela.Length - 1);                 }                 Array.Resize(ref bazaTabela, bazaTabela.Length - 1);                 Array.Resize(ref bazaTabela_ilosc, bazaTabela_ilosc.Length - 1);                 Array.Resize(ref bazaTabela_cena, bazaTabela_cena.Length - 1);                 Array.Resize(ref bazaTabela_x, bazaTabela_x.Length - 1);                 Array.Resize(ref bazaTabela_y, bazaTabela_y.Length - 1);                 Array.Resize(ref bazaTabela_jedn, bazaTabela_jedn.Length - 1);                 AllManager.RefreshTable(bazaTabela, bazaTabela_ilosc, bazaTabela_cena, bazaTabela_x, bazaTabela_y, bazaTabela_jedn, Tabela);                  //ustawienie nowych wartosci list                 if(numer == 1)
                {
                    MainClass.bazaTabela1 = bazaTabela;
                    MainClass.bazaTabela1_ilosc = bazaTabela_ilosc;
                    MainClass.bazaTabela1_cena = bazaTabela_cena;                     MainClass.bazaTabela1_x = bazaTabela_x;
                    MainClass.bazaTabela1_y = bazaTabela_y;                     MainClass.bazaTabela1_jedn = bazaTabela_jedn;
                    MainClass.bazaComboBox1 = bazaComboBox;
                }                 else if(numer == 2)
                {
                    MainClass.bazaTabela2 = bazaTabela;                     MainClass.bazaTabela2_ilosc = bazaTabela_ilosc;                     MainClass.bazaTabela2_cena = bazaTabela_cena;                     MainClass.bazaTabela2_x = bazaTabela_x;                     MainClass.bazaTabela2_y = bazaTabela_y;                     MainClass.bazaTabela2_jedn = bazaTabela_jedn;                     MainClass.bazaComboBox1 = bazaComboBox;
                }             }
        }          public static void MoveValueFromTableToComboBox2(string[] bazaTabela, string[] bazaComboBox, NSTableView Tabela, NSComboBox ComboBox, int pozycja)         {             if(pozycja != -1 && bazaTabela.Length != 0)             {                 //dodanie wartosci do comboboxa                  /*                 Array.Resize(ref bazaComboBox, bazaComboBox.Length + 1);                 bazaComboBox[bazaComboBox.Length - 1] = bazaTabela[pozycja];                 Array.Sort(bazaComboBox);                 AllManager.RefreshComboBox(bazaComboBox, ComboBox);                 */                                 //wywalenie wartosci z listy                 if (pozycja != bazaTabela.Length - 1)                 {                     int pos = pozycja;                     do                     {                         bazaTabela[pos] = bazaTabela[pos + 1];                         pos++;                     } while (pos < bazaTabela.Length - 1);                 }                 Array.Resize(ref bazaTabela, bazaTabela.Length - 1);                 AllManager.RefreshTable2(bazaTabela, Tabela);                  //ustawienie nowych wartosci list                 MainClass.bazaTabela3 = bazaTabela;                 MainClass.bazaComboBox2 = bazaComboBox;             }         }          public static void MoveValueFromTableToNothing5(string[] bazaTabela, string[] bazaTabela_data, NSTableView Tabela, int pozycja)         {             if(pozycja != -1 && bazaTabela.Length != 0)             {                 //wywalenie wartosci z listy                 if (pozycja != bazaTabela.Length - 1)                 {                     int pos = pozycja;                     do                     {                         bazaTabela[pos] = bazaTabela[pos + 1];                         bazaTabela_data[pos] = bazaTabela_data[pos + 1];                         pos++;                     } while (pos < bazaTabela.Length - 1);                 }                 Array.Resize(ref bazaTabela, bazaTabela.Length - 1);                 Array.Resize(ref bazaTabela_data, bazaTabela_data.Length - 1);                 AllManager.RefreshTable5(bazaTabela, bazaTabela_data, Tabela);                  //ustawienie nowych wartosci list                 MainClass.bazaTabela5 = bazaTabela;                 MainClass.bazaTabela5_data = bazaTabela_data;             }         }           public static string PoliczSume(int[] ilosc, float[] lista, float lieferungskosten)
        {             float pom = 0;
            if(lista.Length > 0)
            {                 int pos = 0;
                do
                {                     pom = pom + (lista[pos] * ilosc[pos]);
                    pos++;
                } while (pos < lista.Length);
            }             pom += lieferungskosten;             return Convert.ToString(pom).Replace(".",",");
        }
    }
}
