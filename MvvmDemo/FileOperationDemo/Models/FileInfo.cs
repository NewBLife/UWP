// ***********************************************************************
// FileName:FileInfo
// Description:
// Project:
// Author:NewBLife
// Created:2016/5/7 16:47:46
// Copyright (c) 2016 NewBLife,All rights reserved.
// ***********************************************************************
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using Template10.Mvvm;
using Windows.Storage;
using Windows.Storage.AccessCache;
using Windows.Storage.FileProperties;
using Windows.UI.Xaml.Media.Imaging;

namespace FileOperationDemo.Models
{
    /// <summary>
    /// 文件数据
    /// </summary>
    public class FileInfo : BindableBase
    {
        #region Field
        private StorageFile _File;
        private BitmapImage _FileThumbnail;
        private string _FileMruToken = string.Empty;
        private string _FilePath = string.Empty;
        #endregion

        #region Property
        /// <summary>
        /// 文件信息
        /// </summary>
        [JsonIgnore]
        public StorageFile File
        {
            get
            {
                return _File;
            }

            set
            {
                Set(ref _File, value);
            }
        }
        /// <summary>
        /// 文件缩略图
        /// </summary>
        [JsonIgnore]
        public BitmapImage FileThumbnail
        {
            get
            {
                return _FileThumbnail;
            }

            set
            {
                Set(ref _FileThumbnail, value);
            }
        }
        /// <summary>
        /// 文件访问令牌
        /// 挂起复原时使用
        /// </summary>
        [JsonProperty]
        public string FileMruToken
        {
            get
            {
                return _FileMruToken;
            }

            set
            {
                _FileMruToken = value;
            }
        }
        /// <summary>
        /// 文件路径
        /// 应用内部文件挂起复原时使用
        /// </summary>
        [JsonProperty]
        public string FilePath
        {
            get
            {
                return _FilePath;
            }

            set
            {
                _FilePath = value;
            }
        }
        #endregion

        #region Method
        /// <summary>
        /// 初始化数据
        /// </summary>
        /// <param name="file">选择文件或者照片</param>
        /// <param name="isFromCamera">是否来自照相机</param>
        /// <returns></returns>
        private async Task InitData(StorageFile file, bool isFromCamera = false)
        {
            if (isFromCamera)
            {
                // 来自照相机时重命名文件
                await file.RenameAsync(DateTime.Now.ToString("yyyyMMdd_HH_mm_ss_fff") + file.FileType, NameCollisionOption.ReplaceExisting);
            }
            File = file;
            // 获取文件缩略图
            var thumbnail = await _File.GetThumbnailAsync(ThumbnailMode.SingleItem, 190, ThumbnailOptions.ResizeThumbnail);
            if (thumbnail != null)
            {
                var bitmapImage = new BitmapImage();
                await bitmapImage.SetSourceAsync(thumbnail);
                FileThumbnail = bitmapImage;
            }
            //文件路径设置
            _FilePath = file.Path;
        }
        /// <summary>
        /// 删除文件访问令牌
        /// 由于最大只能有2000个令牌
        /// </summary>
        public void ClearAccessToken()
        {
            if (!string.IsNullOrEmpty(_FileMruToken)
                && StorageApplicationPermissions.FutureAccessList.ContainsItem(_FileMruToken)
                )
            {
                StorageApplicationPermissions.FutureAccessList.Remove(_FileMruToken);
            }
        }
        /// <summary>
        /// 取得显示文件信息
        /// </summary>
        /// <returns></returns>
        public async Task<StorageFile> GetShowFileInfo()
        {
            if (_FilePath.StartsWith(ApplicationData.Current.TemporaryFolder.Path))
            {
                // 应用内部文件
                return File;
            }
            else if (!string.IsNullOrEmpty(_FileMruToken))
            {
                // 应用外部文件
                return await StorageApplicationPermissions.FutureAccessList.GetFileAsync(_FileMruToken);
            }
            return null;
        }
        #endregion

        #region Constructor
        /// <summary>
        /// 反序列化构造函数
        /// </summary>
        /// <param name="fileMruToken">文件访问令牌</param>
        [JsonConstructor]
        public FileInfo(string fileMruToken, string filePath)
        {
            if (filePath.StartsWith(ApplicationData.Current.TemporaryFolder.Path))
            {
                // 应用内文件时
                // 通过文件路径取得文件信息
                var file = StorageFile.GetFileFromPathAsync(filePath).GetResults();
                if (file != null)
                {
                    Task.FromResult(InitData(file));
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(fileMruToken))
                {
                    // 应用外部文件时
                    // 通过文件访问令牌取得文件信息
                    var file = StorageApplicationPermissions.FutureAccessList.GetFileAsync(fileMruToken).GetResults();
                    if (file != null)
                    {
                        Task.FromResult(InitData(file));
                    }
                }
            }
        }

        /// <summary>
        /// 初期化
        /// </summary>
        /// <param name="file">选择文件</param>
        /// <param name="isFromCamera">是否来自照相机</param>
        public FileInfo(StorageFile file, bool isFromCamera = false)
        {
            #region 获取 MRU 中文件的令牌
            // 详细： https://msdn.microsoft.com/zh-cn/library/windows/apps/hh972603
            // 应用外部文件访问时使用
            _FileMruToken = StorageApplicationPermissions.FutureAccessList.Add(file);
            #endregion
            // 加载数据
            Task.FromResult(InitData(file, isFromCamera));
        }

        #endregion
    }
}
