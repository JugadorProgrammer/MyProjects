using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ModelViewController.DataBase
{
    public class Product
    {
        [BindNever]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ModelName { get; set; }
        public decimal Price { get; set; }
        public string Characteristics { get; set; }
        [BindNever]
        public string? Image { get; set; }
    }
}
