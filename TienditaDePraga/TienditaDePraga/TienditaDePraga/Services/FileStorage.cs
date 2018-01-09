using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PCLStorage;

namespace TienditaDePraga
{
    public class FileStorage : IFileStorage
    {
        private IFolder folder;

        private IFile file;

        public String FilePath { get { return file.Path;  } }

        public FileStorage(string name)
        {
            var result = CreateReport(name).Result;
        }

        public async Task<bool> CreateReport(string name)
        {
            try
            {
                IFolder rootFolder = FileSystem.Current.LocalStorage;
                folder = await rootFolder.CreateFolderAsync("Reports",
                    CreationCollisionOption.OpenIfExists).ConfigureAwait(false);
                file = await folder.CreateFileAsync(name,
                    CreationCollisionOption.ReplaceExisting);
                //await file.WriteAllTextAsync("42");
            } catch (Exception ex)
            {
                throw new Exception("Error with file creation: " + ex.Message);
            }

            return await Task.FromResult(true);
        }

        public async Task<bool> AddStringToReport(string content)
        {
            await file.WriteAllTextAsync(content);

            return await Task.FromResult(true);
        }
        
    }
}
