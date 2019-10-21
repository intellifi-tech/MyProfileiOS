using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoreGraphics;
using Foundation;
using UIKit;

namespace MyProfileiOS.GenericClass
{
    public static class CustomToast
    {
        public static void ShowToast(UIViewController GelenBase, string message, ToastType toastType = ToastType.Clean)
        {
            var Genislik = UIScreen.MainScreen.Bounds.Width;
            var ToastView = new UIView();
            ToastView.Frame = new CoreGraphics.CGRect(0, -100, Genislik - 0, 50f);
            ToastView.BackgroundColor = GetToastColor(toastType);
            //ToastView.Layer.CornerRadius = 10f;
         
            //ToastView.Layer.ShadowOpacity = 0.8f;
            //ToastView.Layer.ShadowOffset = new CGSize(0, 0);
            //ToastView.Layer.ShadowColor = UIColor.Black.CGColor;


            var Logo = new UIImageView();
            Logo.Frame = new CoreGraphics.CGRect(5, 0, 50, 50f);
            Logo.ContentMode = UIViewContentMode.ScaleAspectFit;
            Logo.Image = UIImage.FromBundle("Images/ic_launcher.png");

            ToastView.AddSubview(Logo);

            var Labell = new UILabel();
            Labell.Frame = new CoreGraphics.CGRect(Logo.Frame.Right + 10, 0, ToastView.Frame.Width - (Logo.Frame.Right + 20), 50f);
            Labell.Text = message;
            Labell.Lines = 0;
            Labell.AdjustsFontSizeToFitWidth = true;
            Labell.MinimumFontSize = 6f;
            Labell.LineBreakMode = UILineBreakMode.WordWrap;
            Labell.TextColor = UIColor.White;
            ToastView.AddSubview(Labell);

            if (toastType ==ToastType.Clean)
            {
                Labell.TextColor = UIColor.Black;
            }


            UIWindow currentWindow = UIApplication.SharedApplication.KeyWindow;
            currentWindow.AddSubview(ToastView);

            StartAnimation(ToastView);
        }

        static void StartAnimation(UIView Toastt)
        {
            UIView.Animate(0.1, () => {
                var framee = Toastt.Frame;
                framee.Y = 0;
                Toastt.Frame = framee;
            });
            HideToast(Toastt);
        }

        static async void HideToast(UIView GelenView)
        {
           await Task.Run(async delegate () {await Task.Delay(1000); });
           UIView.Animate(0.9, () => {
               GelenView.Alpha = 0f;
            });
        }



        static UIColor GetToastColor(ToastType Typee)
        {
            switch (Typee)
            {
                case ToastType.Success:
                    return UIColor.FromRGB(51, 204, 51);
                case ToastType.Warnig:
                    return UIColor.FromRGB(255, 153, 51);
                case ToastType.Error:
                    return UIColor.FromRGB(255, 0, 0);
                case ToastType.Clean:
                    return UIColor.White;
                default:
                    return UIColor.White; 
            }
        }
    }
    public enum ToastType
    {
        Success,
        Warnig,
        Error,
        Clean
    }
}