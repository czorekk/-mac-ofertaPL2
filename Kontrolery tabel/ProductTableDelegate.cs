using System; using AppKit; using CoreGraphics; using Foundation; using System.Collections; using System.Collections.Generic; 
namespace Oferta__
{
    public class ProductTableDelegate : NSTableViewDelegate     {          private const string CellIndentifer = "ProdCell";          private ProductTableDataSource DataSource;          public ProductTableDelegate(ProductTableDataSource datasource)         {             this.DataSource = datasource;         }          public override NSView GetViewForItem(NSTableView tableView, NSTableColumn tableColumn, nint row)         {             NSTextField view = (NSTextField)tableView.MakeView(CellIndentifer, this);             if (view == null)             {                 view = new NSTextField();                 view.Identifier = CellIndentifer;                 view.BackgroundColor = NSColor.Clear;                 view.Bordered = false;                 view.Selectable = false;                 view.Editable = false;             }              switch (tableColumn.Title)             {                 case "Element":                     view.StringValue = DataSource.Products[(int)row].Title;                     break;                 case "Ilosc":                     view.Alignment = NSTextAlignment.Right;                     view.StringValue = DataSource.Products[(int)row].Description;                     break;                 case "Cena":                     view.Alignment = NSTextAlignment.Right;                     view.StringValue = DataSource.Products[(int)row].Cena;                     break;             }              return view;           }           public override bool SelectionShouldChange(NSTableView tableView)         {
            //ViewController.pozycja = -1;
            if (tableView.GetIdentifier() == "Tabela1")             {                 MainClass.pozycja1 = -1;             }             else if (tableView.GetIdentifier() == "Tabela2")             {                 MainClass.pozycja2 = -1;             }             else if(tableView.GetIdentifier() == "Tabela3")
            {
                MainClass.pozycja3 = -1;
            }             else if(tableView.GetIdentifier() == "ListaOferty")
            {
                MainClass.pozycja4 = -1;
            }             return true;         }          public override bool ShouldSelectRow(NSTableView tableView, nint row)         {              //Console.WriteLine(tableView.GetIdentifier());             if(tableView.GetIdentifier() == "Tabela1")             {                 MainClass.pozycja1 = Convert.ToInt32(row);             }             else if(tableView.GetIdentifier() == "Tabela2")
            {
                MainClass.pozycja2 = Convert.ToInt32(row);
            }             else if (tableView.GetIdentifier() == "Tabela3")             {                 MainClass.pozycja3 = Convert.ToInt32(row);             }             else if (tableView.GetIdentifier() == "ListaOferty")             {                 MainClass.pozycja4 = Convert.ToInt32(row);             }

            //ViewController.pozycja = Convert.ToInt32(row);
            //Console.WriteLine("zaznaczylem" + Convert.ToInt32(row));
            return true;         }     } }  