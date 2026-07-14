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



      //======================
      //加载图片
      //======================

      [RelayCommand]
      private void LoadImage()
      {

         OpenFileDialog dialog = new();

         dialog.Filter =
         "图片|*.jpg;*.png;*.bmp";
         if (dialog.ShowDialog() != true)
            return;
         _source = Cv2.ImRead(dialog.FileName);
         DisplayImageSource = BitmapSourceConverter.ToBitmapSource(_source);
         StatusMessage = "图片加载完成";

      }


      //======================
      //提取轮廓
      //======================


      [RelayCommand]
      private void ExtractContours()
      {

         if (_source == null)
            return;
         Mat roi = new Mat(_source,new OpenCvSharp.Rect((int)RectX,(int)RectY,(int)RectWidth,(int)RectHeight));

         Mat gray = new();

         Cv2.CvtColor(roi,gray,ColorConversionCodes.BGR2GRAY);
         Mat edge = new();
         Cv2.Canny(gray,edge,80,150);
         Mat result = roi.Clone();

         Cv2.FindContours(edge,out Point[][] contours,out _,RetrievalModes.External,ContourApproximationModes.ApproxSimple);
         Cv2.DrawContours(result,contours,-1,Scalar.Red,2);

         DisplayImageSource = BitmapSourceConverter.ToBitmapSource(result);

         StatusMessage = $"发现轮廓数量:{contours.Length}";

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
