

using System.Threading.Tasks;

namespace SH1ProjeUygulamasi.WebAPI.Tools
{
    public class FileHelper
    {
        public static async Task<string> FileLoaderAsync(IFormFile formFile, string filePath = "")
        {
            string dosyaAdi = "";

            dosyaAdi = formFile.FileName;
            string klasor = Directory.GetCurrentDirectory() + "/wwwroot/Images/" + filePath;
            using var stream = new FileStream(klasor + formFile.FileName, FileMode.Create);
            await formFile.CopyToAsync(stream);

            return dosyaAdi;
        }
        public static bool FileRemover(string fileName, string klasorYolu = "/wwwroot/Images/")
        {
            string klasor = Directory.GetCurrentDirectory() + klasorYolu + fileName;
            if (File.Exists(klasor)) // eğer sunucuda dosya varsa
            {
                File.Delete(klasor); // dosyayı sil
                return true; // silme başarılı
            }
            return false;
        }
    }
}
