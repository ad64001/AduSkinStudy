using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace AduSkin.Demo.Data.Utils
{
   public static class BitmapSourceConverter
   {
      public static BitmapImage ToBitmapSource(Mat mat)
      {
         byte[] bytes =
             mat.ToBytes(".png");
         BitmapImage bmp = new();
         using MemoryStream ms =
             new(bytes);
         bmp.BeginInit();
         bmp.StreamSource = ms;
         bmp.CacheOption = BitmapCacheOption.OnLoad;
         bmp.EndInit();
         bmp.Freeze();
         return bmp;

      }
   }
}
