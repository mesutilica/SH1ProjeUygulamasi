

namespace SH1ProjeUygulamasi.WebUI.Tools
{
    public class FileHelper
    {
        public static string FileLoader(IFormFile formFile)
        {
            string dosyaAdi = "";

            dosyaAdi = formFile.FileName;
            string klasor = Directory.GetCurrentDirectory() + "/wwwroot/Images/";
            using var stream = new FileStream(klasor + formFile.FileName, FileMode.Create);
            formFile.CopyTo(stream);

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
