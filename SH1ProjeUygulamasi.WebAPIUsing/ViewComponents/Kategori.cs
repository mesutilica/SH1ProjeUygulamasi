using Microsoft.AspNetCore.Mvc;
using SH1ProjeUygulamasi.Core.Entities;

namespace SH1ProjeUygulamasi.WebAPIUsing.ViewComponents
{
    public class Kategori : ViewComponent
    {
        static string _apiAdres = "http://localhost:5018/Api/Categories";
        HttpClient _httpClient = new();

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = await _httpClient.GetFromJsonAsync<List<Category>>(_apiAdres);
            return View(model);
        }
    }
}
