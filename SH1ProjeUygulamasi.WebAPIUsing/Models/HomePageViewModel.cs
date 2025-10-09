using SH1ProjeUygulamasi.Core.Entities;

namespace SH1ProjeUygulamasi.WebAPIUsing.Models
{
    public class HomePageViewModel
    {
        public IEnumerable<Slider>? Sliders { get; set; } // sayfada 1 slider listesi
        public IEnumerable<Product>? Products { get; set; } // 1 de ürün listesi kullanacağız
    }
}
