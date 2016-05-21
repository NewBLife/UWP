using FileOperationDemo.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Template10.Mvvm;
using Template10.Services.NavigationService;
using Template10.Services.SerializationService;
using Windows.Media.Capture;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Core;
using Windows.UI.Xaml.Navigation;

namespace FileOperationDemo.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        public MainPageViewModel()
        {
        }

        private ObservableCollection<FileInfo> _FileList = new ObservableCollection<FileInfo>();

        public ObservableCollection<FileInfo> FileList
        {
            get
            {
                return _FileList;
            }
            set
            {
                Set(ref _FileList, value);
            }
        }

        /// <summary>
        /// 添加文件
        /// </summary>
        public DelegateCommand AddFile => new DelegateCommand(
                    async () =>
                    {
                        // 选择多个文件
                        FileOpenPicker openPicker = new FileOpenPicker();
                        openPicker.ViewMode = PickerViewMode.Thumbnail;
                        openPicker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
                        openPicker.FileTypeFilter.Clear();
                        openPicker.FileTypeFilter.Add("*");
                        var files = await openPicker.PickMultipleFilesAsync();
                        files.ToList().ForEach(
                            file => FileList?.Add(new FileInfo(file))
                            );
                    }
                    );

        /// <summary>
        /// 拍照
        /// </summary>
        public DelegateCommand TakePhoto => new DelegateCommand(
                    async () =>
                    {
                        // 拍照
                        CameraCaptureUI captureUI = new CameraCaptureUI();
                        captureUI.PhotoSettings.Format = CameraCaptureUIPhotoFormat.Png;
                        captureUI.PhotoSettings.AllowCropping = false;
                        var photo = await captureUI.CaptureFileAsync(CameraCaptureUIMode.Photo);
                        if (photo != null)
                        {
                            FileList?.Add(new FileInfo(photo, true));
                        }
                    }
                    );

        /// <summary>
        /// 查看文件
        /// </summary>
        public DelegateCommand<FileInfo> ShowFile => new DelegateCommand<FileInfo>(
            async fileinfo =>
            {
                if (fileinfo != null)
                {
                    // 获取可访问文件信息
                    var file = await fileinfo.GetShowFileInfo();
                    if (file != null)
                    {
                        // 默认应用打开文件
                        await Windows.System.Launcher.LaunchFileAsync(file);
                    }
                }
            }
            );
        /// <summary>
        /// 删除文件
        /// </summary>
        public DelegateCommand<FileInfo> DeleteFile => new DelegateCommand<FileInfo>(
            fileinfo =>
            {
                if (fileinfo != null && FileList.Contains(fileinfo))
                {
                    // 删除访问权限列表中的令牌
                    fileinfo.ClearAccessToken();
                    FileList.Remove(fileinfo);
                }
            }
            );

        public override async Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> suspensionState)
        {
            try
            {
                if (suspensionState.Any())
                {
                    FileList = SerializationService.Json.Deserialize<ObservableCollection<FileInfo>>(suspensionState[nameof(FileList)]?.ToString());
                }
                else
                {
                    // 清理临时数据
                    await ApplicationData.Current.ClearAsync(ApplicationDataLocality.Temporary);
                }

                // 注册系统返回按钮事件
                SystemNavigationManager.GetForCurrentView().BackRequested += OnBackRequested;
                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Debug.WriteLine(ex.Source);
                Debug.WriteLine(ex.StackTrace);
            }
        }

        public override async Task OnNavigatedFromAsync(IDictionary<string, object> suspensionState, bool suspending)
        {
            try
            {
                if (suspending)
                {
                    suspensionState[nameof(FileList)] = SerializationService.Json.Serialize(FileList);
                }
                // 清理临时数据
                await ApplicationData.Current.ClearAsync(ApplicationDataLocality.Temporary);
                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Debug.WriteLine(ex.Source);
                Debug.WriteLine(ex.StackTrace);
            }
        }

        public override async Task OnNavigatingFromAsync(NavigatingEventArgs args)
        {
            try
            {
                args.Cancel = false;
                // 注销系统返回按钮事件
                SystemNavigationManager.GetForCurrentView().BackRequested -= OnBackRequested;
                // 清理临时数据
                await ApplicationData.Current.ClearAsync(ApplicationDataLocality.Temporary);
            }
            catch
            {
                // 临时文件删除异常时不做任何处理
            }
            await Task.CompletedTask;
        }

        /// <summary>
        /// 返回按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnBackRequested(object sender, BackRequestedEventArgs e)
        {
            e.Handled = true;
            NavigationService.GoBack();
        }

        public void GotoSettings() =>
            NavigationService.Navigate(typeof(Views.SettingsPage), 0);

        public void GotoAbout() =>
            NavigationService.Navigate(typeof(Views.SettingsPage), 1);

    }
}

