using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
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

        // 列表中某个AppIcon加载完成后的回调通知
        async void AppIcon_ImageLoaded(object sender, RoutedEventArgs e)
        {
            
            if (sender is Image image) {
                Debug.WriteLine($"AppIcon_ImageLoaded image:{image}");
                var parent = VisualTreeHelper.GetParent(image) as Border;
                if (parent != null) {
                    Debug.WriteLine($"AppIcon_ImageLoaded parent border:{parent}");
                    // 按照计算出的AppIcon主题色, 设置给上半部分的整体背景
                    parent.Background = await ImageToThemeColorConverter.GetImageThemeColorBrushAsync(image.Source);
                }
            }
        }


        private static T FindVisualParent<T>(DependencyObject obj) where T : DependencyObject
        {
            while (obj != null)
            {
                if (obj is T parent)
                {
                    return parent;
                }
                obj = VisualTreeHelper.GetParent(obj);
            }
            return null;
        }

        // AppList的ItemPanel 大小改变时的回调方法
        void AppItemsPanel_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Debug.WriteLine("AppItemsPanel_SizeChanged");
        }

        // AppList的ItemView 鼠标指针移入后的位置变更与背景色变更
        void AppItemBorder_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            Debug.WriteLine("AppItemBorder_PointerEntered");
            var border = sender as Border;
            if (border != null) {
                TranslateTransform translateTransform = new TranslateTransform();
                translateTransform.Y = -2; // 在 Y 轴向上平移 3 个单位
                border.RenderTransform = translateTransform;
                border.Background = new SolidColorBrush(Color.FromArgb(0xBB, 0x60, 0x60, 0x60));
            }
        }

        // AppList的ItemView 鼠标指针移出后的位置恢复与背景色恢复
        void AppItemBorder_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            Debug.WriteLine("AppItemBorder_PointerExited");
            var border = sender as Border;
            if (border != null)
            {
                TranslateTransform translateTransform = new TranslateTransform();
                // 恢复正常, 背景色也恢复正常 BB303030
                border.RenderTransform = translateTransform;
                border.Background = new SolidColorBrush(Color.FromArgb(0xBB, 0x30, 0x30, 0x30));
            }
        }

        // AppList的ItemView 鼠标指针按下后的位置恢复与背景色加深
        void AppItemBorder_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            Debug.WriteLine($"AppItemBorder_PointerPressed {e.OriginalSource}");
            var border = sender as Border;
            if (border != null)
            {
                TranslateTransform translateTransform = new TranslateTransform();
                // 按下状态 位置恢复 背景色加深0xCE202020
                border.RenderTransform = translateTransform;
                border.Background = new SolidColorBrush(Color.FromArgb(0xCE, 0x20, 0x20, 0x20));
            }
            var listViewItem = FindVisualParent<ListViewItem>(e.OriginalSource as FrameworkElement);
            if (listViewItem != null)
            {
                var clickedData = AppListView.ItemFromContainer(listViewItem) as AppItemEntity;
                if (clickedData != null) {
                    // 此处处理用户所点击的appItem数据
                    Debug.WriteLine($"AppItemBorder_PointerPressed clicked appItem: {clickedData.Name}");
                }
            }
        }

        // AppList的ItemView 鼠标指针释放后 背景色恢复
        void AppItemBorder_PointerReleased(object sender, PointerRoutedEventArgs e)
        {
            Debug.WriteLine("AppItemBorder_PointerReleased");
            var border = sender as Border;
            if (border != null)
            {
                // 背景色也恢复正常 BB303030
                border.Background = new SolidColorBrush(Color.FromArgb(0xBB, 0x30, 0x30, 0x30));
            }
        }

        // AppList的ItemView 鼠标指针取消后的位置恢复与背景色恢复
        void AppItemBorder_PointerCanceled(object sender, PointerRoutedEventArgs e)
        {
            Debug.WriteLine("AppItemBorder_PointerCanceled");
            var border = sender as Border;
            if (border != null)
            {
                TranslateTransform translateTransform = new TranslateTransform();
                // 恢复正常, 背景色也恢复正常 BB303030
                border.RenderTransform = translateTransform;
                border.Background = new SolidColorBrush(Color.FromArgb(0xBB, 0x30, 0x30, 0x30));
            }
        }

        void Button_SeeAll_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            var button = sender as Button;
            if (button != null)
            {
                Debug.WriteLine($"Button_SeeAll_PointerEntered button:{button}");
                // 鼠标指针移入时的背景色深一点
                button.Background = new SolidColorBrush(Color.FromArgb(0x40, 0xFF, 0xFF, 0xFF));
            }
            Debug.WriteLine("Button_SeeAll_PointerEntered");
        }

        void Button_SeeAll_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            var button = sender as Button;
            if (button != null)
            {
                Debug.WriteLine($"Button_SeeAll_PointerExited button:{button}");
                // 鼠标指针移出时的背景色恢复原样
                button.Background = new SolidColorBrush(Color.FromArgb(0x20, 0xFF, 0xFF, 0xFF));
            }
            Debug.WriteLine("Button_SeeAll_PointerExited");
        }
    }
}
