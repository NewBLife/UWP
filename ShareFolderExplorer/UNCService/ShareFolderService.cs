using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Windows.Storage;

namespace UNCService
{
    public class ShareFolderService
    {
        /// <summary>
        /// get items from unc path
        /// *you must add the 
        /// </summary>
        /// <param name="uncPath"></param>
        /// <returns></returns>
        public async Task<IReadOnlyList<IStorageItem>> GetShareFolderItemsAsync(string uncPath)
        {
            var folder = await getFolderFromUncPath(uncPath);

            return await folder.GetItemsAsync();
        }

        /// <summary>
        /// create item to share folder
        /// </summary>
        /// <param name="uncPath"></param>
        /// <param name="name"></param>
        /// <param name="isFile"></param>
        /// <returns></returns>
        public async Task<IStorageItem> ShareFolderCreateItemAsync(string uncPath, string name, bool isFile = true)
        {
            var shareFolder = await getFolderFromUncPath(uncPath);
            if (isFile)
            {
                return await shareFolder.CreateFileAsync(name, CreationCollisionOption.ReplaceExisting);
            }
            else
            {
                return await shareFolder.CreateFolderAsync(name, CreationCollisionOption.OpenIfExists);
            }
        }

        private async Task<StorageFolder> getFolderFromUncPath(string uncPath)
        {
            return await StorageFolder.GetFolderFromPathAsync(uncPath);
        }
    }
}
