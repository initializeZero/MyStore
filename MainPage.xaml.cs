using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x804 上介绍了“空白页”项模板

namespace MyStore
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        // 列表数据
        private static ObservableCollection<AppItemEntity> listData = new ObservableCollection<AppItemEntity>();

        public MainPage()
        {
            this.InitializeComponent();
            loadAppListData();
        }

        // 载入数据
        void loadAppListData() {
            AppItemEntity appItemEntity1 = new AppItemEntity();
            appItemEntity1.Name = "TikTok";
            appItemEntity1.Score = "4.1";
            appItemEntity1.Status = AppItemEntity.STATUS.FREE;
            appItemEntity1.IconUrl = "ms-appx:///AppIcons/tiktok.png";


            AppItemEntity appItemEntity2 = new AppItemEntity();
            appItemEntity2.Name = "Adobe Photoshop";
            appItemEntity2.Score = "3.9";
            appItemEntity2.Status = AppItemEntity.STATUS.PAID;
            appItemEntity2.IconUrl = "ms-appx:///AppIcons/adobe_photoshop.png";


            AppItemEntity appItemEntity3 = new AppItemEntity();
            appItemEntity3.Name = "LinkedIn";
            appItemEntity3.Score = "4.2";
            appItemEntity3.Status = AppItemEntity.STATUS.FREE;
            appItemEntity3.IconUrl = "ms-appx:///AppIcons/linkedin.png";


            AppItemEntity appItemEntity4 = new AppItemEntity();
            appItemEntity4.Name = "Adobe Acrobat Reader DC";
            appItemEntity4.Score = "3.5";
            appItemEntity4.Status = AppItemEntity.STATUS.FREE;
            appItemEntity4.IconUrl = "ms-appx:///AppIcons/adobe_acrobat_reader_dc.png";


            AppItemEntity appItemEntity5 = new AppItemEntity();
            appItemEntity5.Name = "Dolby Access";
            appItemEntity5.Score = "4.3";
            appItemEntity5.Status = AppItemEntity.STATUS.FREE;
            appItemEntity5.IconUrl = "ms-appx:///AppIcons/dolby_access.png";


            listData.Add(appItemEntity1);
            listData.Add(appItemEntity2);
            listData.Add(appItemEntity3);
            listData.Add(appItemEntity4);
            listData.Add(appItemEntity5);

            // 数据设置给ListView
            AppListView.ItemsSource = listData;
        }

        // SeeAll 按钮点击回调方法
        void ButtonOnClick_SeeAll(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("ButtonOnClick_SeeAll ");
        }

        // AppList的ItemPanel 大小改变时的回调方法
        void AppItemsPanel_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Debug.WriteLine("AppItemsPanel_SizeChanged ");
        }
    }
}
