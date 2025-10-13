using System.ComponentModel.DataAnnotations;

namespace SH1ProjeUygulamasi.Core.Models
{
    public class UserLoginModel
    {
        [StringLength(50), EmailAddress, Required(ErrorMessage = "{0} Boş Geçilemez!")]
        public string Email { get; set; }
        [Display(Name = "Şifre"), StringLength(50), Required(ErrorMessage = "{0} Boş Geçilemez!")]
        public string Password { get; set; }
    }
}
