using System.ComponentModel.DataAnnotations;

namespace SH1ProjeUygulamasi.Core.Entities
{
    public class ProductImage : IEntity
    {
        public int Id { get; set; }
        [Display(Name = "Ürün")]
        public int ProductId { get; set; }
        [Display(Name = "Ürün Resmi"), StringLength(100)]
        public string? Name { get; set; }
        [Display(Name = "Ürün")]
        public Product? Product { get; set; }
    }
}
