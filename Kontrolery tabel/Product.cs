using System;
namespace Oferta__
{
    public class Product     {         public string Title { get; set; } = "";         public string Description { get; set; } = "";
        public string Cena { get; set; } = "";          public Product()         {         }          public Product(string title, string description, string cena)         {             this.Title = title;             this.Description = description;
            this.Cena = cena;         }     } }