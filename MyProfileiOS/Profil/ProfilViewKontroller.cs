using CoreGraphics;
using Foundation;
using System;
using UIKit;

namespace MyProfileiOS
{
    public partial class ProfilViewKontroller : UIViewController
    {
        public ProfilViewKontroller (IntPtr handle) : base (handle)
        {
        }
        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);
            ProfilFotoHeader.Layer.CornerRadius = ProfilFotoHeader.Frame.Height / 2;
            ProfilFotoHeader.ClipsToBounds = true;
            ButtonTasarimlariniAyarla(TakipButton);

            GetProfilHeader();
        }
        
        void GetProfilHeader()
        {
            var MainStoryBoard = UIStoryboard.FromName("Main", NSBundle.MainBundle);
            var ProfilViewControllerSubController1 = MainStoryBoard.InstantiateViewController("ProfilViewControllerSubController") as ProfilViewControllerSubController;

            var viewController = ProfilViewControllerSubController1;

            viewController.View.Frame = new CGRect(0, 0, UIScreen.MainScreen.Bounds.Width, 1000f);
            viewController.WillMoveToParentViewController(this);
            ProfilScroll.AddSubview(viewController.View);
            this.AddChildViewController(viewController);
            viewController.DidMoveToParentViewController(this);
            ProfilScroll.ContentSize = new CGSize(UIScreen.MainScreen.Bounds.Width, 1000f);
            ProfilScroll.ShowsHorizontalScrollIndicator = false;
            ProfilScroll.ShowsVerticalScrollIndicator = false;
            

        }

        void ButtonTasarimlariniAyarla(UIButton GelenButton)
        {
            GelenButton.Layer.CornerRadius = GelenButton.Bounds.Height / 2;
            GelenButton.ClipsToBounds = true;
           // GelenButton.ContentEdgeInsets = new UIEdgeInsets(2, 2, 2, 2);
            var ButtonImage = GelenButton.ImageView.Image;
            var ButtonImageTintImage = ButtonImage.ImageWithRenderingMode(UIImageRenderingMode.AlwaysTemplate);
            GelenButton.SetImage(ButtonImageTintImage, UIControlState.Normal);
            GelenButton.TintColor = UIColor.FromRGB(48, 79, 254);
            GelenButton.Layer.MasksToBounds = false;
            //GelenButton.Layer.ShadowColor = UIColor.DarkGray.CGColor;
            //GelenButton.Layer.ShadowOpacity = 1.0f;
            //GelenButton.Layer.ShadowRadius = GelenButton.Bounds.Height / 2;
            //GelenButton.Layer.ShadowOffset = new System.Drawing.SizeF(0f, 2f);
        }
    }
}