using System.ComponentModel.DataAnnotations;

namespace SH1ProjeUygulamasi.Core.Entities
{
    public class User : IEntity
    {
        public int Id { get; set; }
        [Display(Name = "Kullanıcı Adı"), StringLength(50)]
        public string? UserName { get; set; }
        [Display(Name = "Şifre"), StringLength(50), Required(ErrorMessage = "{0} Boş Geçilemez!")]
        public string Password { get; set; }
        [StringLength(50), EmailAddress, Required(ErrorMessage = "{0} Boş Geçilemez!")]
        public string Email { get; set; }
        [Display(Name = "Adı"), StringLength(50)]
        public string? Name { get; set; } // ? işareti bu alanın nullable yani boş geçilebilir olmasını sağlar
        [Display(Name = "Soyadı"), StringLength(50)]
        public string? Surname { get; set; }
        [Display(Name = "Aktif?")]
        public bool IsActive { get; set; }
        [Display(Name = "Admin?")]
        public bool IsAdmin { get; set; }
        [Display(Name = "Eklenme Tarihi"), ScaffoldColumn(false)]
        public DateTime CreateDate { get; set; }
        public Guid? UserGuid { get; set; }
        // Jwt için propertyler
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpireDate { get; set; }
    }
}
