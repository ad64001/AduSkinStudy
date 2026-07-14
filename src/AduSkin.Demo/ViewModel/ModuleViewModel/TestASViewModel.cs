using AduSkin.Demo.Data.Enum;
using AduSkin.Demo.Data.Utils;
using AduSkin.Demo.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace AduSkin.Demo.ViewModel
{
   public partial class TestASViewModel : ObservableObject
   {
      private Mat? _source;
      [ObservableProperty]
      private ImageSource? displayImageSource;
      [ObservableProperty]
      private bool isSelecting;
      [ObservableProperty]
      private double rectX;
      [ObservableProperty]
      private double rectY;
      [ObservableProperty]
      private double rectWidth;
      [ObservableProperty]
      private double rectHeight;
      [ObservableProperty]
      private double imageControlWidth;
      [ObservableProperty]
      private double imageControlHeight;
      [ObservableProperty]
      private string _StatusMessage = string.Empty;


      //======================
      //加载图片
      //======================

      [RelayCommand]
      private void LoadImage()
      {

         OpenFileDialog dialog = new();

         dialog.Filter =
         "图片|*.jpg;*.png;*.bmp";
         if (dialog.ShowDialog() != DialogResult.OK)
            return;
         _source = Cv2.ImRead(dialog.FileName);
         DisplayImageSource = BitmapSourceConverter.ToBitmapSource(_source);
         _StatusMessage = "图片加载完成";

      }


      //======================
      //提取轮廓
      //======================


      [RelayCommand]
      private void ExtractContours()
      {

         if (_source == null)
            return;

         // 1. 获取图像的实际边界
         int cols = _source.Cols;
         int rows = _source.Rows;

         // 2. 计算原始坐标和宽高
         int x = (int)RectX;
         int y = (int)RectY;
         int w = (int)RectWidth;
         int h = (int)RectHeight;

         // 3. 强制修正坐标，确保起点不小于 (0,0)
         x = Math.Max(0, x);
         y = Math.Max(0, y);

         // 4. 强制修正宽高，确保终点不超过图像边界 (cols, rows)
         // 如果 x 已经超出边界，w 会被修正为 0 或负数，后续需处理
         if (x + w > cols)
            w = cols - x;

         if (y + h > rows)
            h = rows - y;

         // 5. 最终有效性检查：防止宽或高为负数或0（例如选区完全在图像外）
         if (w <= 0 || h <= 0)
         {
            // 选区无效，直接返回，避免创建 Mat 报错
            // 这里可以根据需求选择清空结果或提示用户
            return;
         }

         // 6. 使用修正后的安全坐标创建 ROI
         OpenCvSharp.Rect safeRect = new OpenCvSharp.Rect(x, y, w, h);
         Mat roi = new Mat(_source, safeRect);

         Mat gray = new();

         Cv2.CvtColor(roi,gray,ColorConversionCodes.BGR2GRAY);
         Mat edge = new();
         Cv2.Canny(gray,edge,80,150);
         Mat result = roi.Clone();

         Cv2.FindContours(edge,out Point[][] contours,out _,RetrievalModes.External,ContourApproximationModes.ApproxSimple);
         Cv2.DrawContours(result,contours,-1,Scalar.Red,2);

         DisplayImageSource = BitmapSourceConverter.ToBitmapSource(result);

         _StatusMessage = $"发现轮廓数量:{contours.Length}";

      }


      [RelayCommand]
      private void ClearContours()
      {
         if (_source != null)
            DisplayImageSource =
            BitmapSourceConverter.ToBitmapSource(_source);
      }

     


   }
}
