using System;
namespace Oferta__
{
    public class Product_Tab5
    {
        public string Title { get; set; } = "";
        public string Description { get; set; } = "";

        public Product_Tab5()
        {
        }

        public Product_Tab5(string title, string description)
        {
            this.Title = title;
            this.Description = description;
        }
    }
}