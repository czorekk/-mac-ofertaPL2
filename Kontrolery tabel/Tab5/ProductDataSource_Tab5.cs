using System; using AppKit; using CoreGraphics; using Foundation; using System.Collections; using System.Collections.Generic; 
namespace Oferta__
{
    public class ProductTableDataSource_Tab5 : NSTableViewDataSource     {          public List<Product_Tab5> Products = new List<Product_Tab5>();           public ProductTableDataSource_Tab5()         {         }          public override nint GetRowCount(NSTableView tableView)         {             return Products.Count;         }     } }  