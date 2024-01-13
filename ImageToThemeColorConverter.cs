using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Windows.Graphics.Imaging;
using Windows.Storage;
using Windows.UI;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace MyStore
{

    // 图片到主题背景色Brush的转换器
    public class ImageToThemeColorConverter
    {

        // 计算ImageSource的主题颜色
        public static async Task<SolidColorBrush> GetImageThemeColorBrushAsync(ImageSource image)
        {
            Uri uri = (image as BitmapImage).UriSource;
            StorageFile file = await StorageFile.GetFileFromApplicationUriAsync(uri);
            Stream imageStream = await file.OpenStreamForReadAsync();
            BitmapDecoder imageDecoder = await BitmapDecoder.CreateAsync(imageStream.AsRandomAccessStream());

            // 读入数据时, 要求最终转换成BGRA格式, 方便后面计算
            PixelDataProvider pixelData = await imageDecoder.GetPixelDataAsync(
                BitmapPixelFormat.Bgra8,
                BitmapAlphaMode.Straight,
                new BitmapTransform(),
                ExifOrientationMode.RespectExifOrientation,
                ColorManagementMode.DoNotColorManage
            );

            uint pixelWidth = imageDecoder.PixelWidth;
            uint pixelHeight = imageDecoder.PixelHeight;
            BitmapPixelFormat pixelFormat = imageDecoder.BitmapPixelFormat;

            Debug.WriteLine($"GetImageThemeColorAsync bitmapImage.UriSource:{uri}, bitmapImage pixelWidth:{pixelWidth}, pixelHeight:{pixelHeight}, pixelFormat:{pixelFormat}");

            byte[] bytes = pixelData.DetachPixelData();

            // 计算平均颜色
            ulong totalRed = 0;
            ulong totalGreen = 0;
            ulong totalBlue = 0;

            for (int i = 0; i < bytes.Length; i += 4)
            {
                byte alpha = bytes[i + 3];

                // 过于透明的像素点去掉
                if (alpha < 128) {
                    continue;
                }
                totalBlue += bytes[i];
                totalGreen += bytes[i + 1];
                totalRed += bytes[i + 2];
            }

            ulong pixelCount = (ulong) (bytes.Length / 4);

            // 变暗因子, 主题色有个偏黑色的蒙版
            float factor = 0.85f;

            byte averageRed = (byte)(totalRed * factor / pixelCount);
            byte averageGreen = (byte)(totalGreen * factor / pixelCount);
            byte averageBlue = (byte)(totalBlue * factor / pixelCount);


            return new SolidColorBrush(Color.FromArgb(255, averageRed, averageGreen, averageBlue));
        }
    }
}
