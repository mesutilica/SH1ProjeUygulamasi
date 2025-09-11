using System.ComponentModel.DataAnnotations;

namespace SH1ProjeUygulamasi.Core.Entities
{
    public class Slider : IEntity
    {
        public int Id { get; set; }
        [Display(Name = "Başlık"), StringLength(250)]
        public string? Title { get; set; }
        [Display(Name = "Açıklama"), DataType(DataType.MultilineText), StringLength(500)]
        public string? Description { get; set; }
        [Display(Name = "Resim"), StringLength(100)]
        public string? Image { get; set; }
    }
}
