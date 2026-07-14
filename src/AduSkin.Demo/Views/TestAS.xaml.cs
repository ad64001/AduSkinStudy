using AduSkin.Demo.ViewModel;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace AduSkin.Demo.Views
{
   /// <summary>
   /// TestAS.xaml 的交互逻辑
   /// </summary>
   public partial class TestAS : UserControl
   {
      // 用 WPF 的 Point，不再依赖 OpenCvSharp.Point
      private Point _startPoint;
      private bool _isSelecting;


      private TestASViewModel? Vm =>
          DataContext as TestASViewModel;
      public TestAS()
      {
         InitializeComponent();
      }

      private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
      {
         var vm = Vm;
         if (vm == null)
            return;
         if (vm.DisplayImageSource == null)
            return;
         _startPoint =
             e.GetPosition(MainImage);
         _isSelecting = true;
         vm.IsSelecting = true;
         vm.RectX = _startPoint.X;
         vm.RectY = _startPoint.Y;
         vm.RectWidth = 0;
         vm.RectHeight = 0;
         MainImage.CaptureMouse();

      }

      private void Image_MouseMove(object sender,MouseEventArgs e)
      {
         if (!_isSelecting)
            return;
         var vm = Vm;
         if (vm == null)
            return;

         Point p = e.GetPosition(MainImage);
         vm.RectX = Math.Min(_startPoint.X, p.X);
         vm.RectY = Math.Min(_startPoint.Y, p.Y);
         vm.RectWidth = Math.Abs(p.X - _startPoint.X);
         vm.RectHeight = Math.Abs(p.Y - _startPoint.Y);
      }

      private void Image_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
      {

         if (!_isSelecting)
            return;
         _isSelecting = false;
         MainImage.ReleaseMouseCapture();
         var vm = Vm;
         if (vm == null)
            return;
         vm.IsSelecting = false;
         vm.ImageControlWidth = MainImage.ActualWidth;
         vm.ImageControlHeight = MainImage.ActualHeight;
         vm.StatusMessage = "ROI选择完成，请点击提取轮廓";


      }
   }
}



