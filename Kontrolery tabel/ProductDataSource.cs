using System; using AppKit; using CoreGraphics; using Foundation; using System.Collections; using System.Collections.Generic; 
namespace Oferta__
{
    public class ProductTableDataSource : NSTableViewDataSource     {          public List<Product> Products = new List<Product>();           public ProductTableDataSource()         {         }          public override nint GetRowCount(NSTableView tableView)         {             return Products.Count;         }     } }  