using System.IO;

using TienditaDePraga;
using Xamarin.Forms;

[assembly: Dependency(typeof(FileHelper))]
namespace TienditaDePraga
{
    public class FileHelper : IFileHelper
    {
        public string GetLocalFilePath(string filename)
        {
            string path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            return Path.Combine(path, filename);
        }
    }
}