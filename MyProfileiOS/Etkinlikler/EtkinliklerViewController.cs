using Foundation;
using Pager;
using System;
using UIKit;

namespace MyProfileiOS
{
    public partial class EtkinliklerViewController : UIViewController
    {
        public EtkinliklerViewController (IntPtr handle) : base (handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            var colors = new[]
            {
                UIColor.FromRGB(48, 79, 254),
                UIColor.FromRGB(48, 79, 254),
            };
            var MainStoryBoard = UIStoryboard.FromName("Main", NSBundle.MainBundle);
            var TakipEttiklerimEtkinliklerViewController1 = MainStoryBoard.InstantiateViewController("TakipEttiklerimEtkinliklerViewController") as TakipEttiklerimEtkinliklerViewController;
            var GlobalEtkinliklerViewController1 = MainStoryBoard.InstantiateViewController("GlobalEtkinliklerViewController") as GlobalEtkinliklerViewController;

            TakipEttiklerimEtkinliklerViewController1.Title = "TAKÝP ETTÝKLERÝM";
            GlobalEtkinliklerViewController1.Title = "GLOBAL";

            UIViewController[] pages = new UIViewController[]
            {
                TakipEttiklerimEtkinliklerViewController1,
                GlobalEtkinliklerViewController1
            };

            var pager = new PagerViewController(new PagerStyle(PagerStyle.Stretched) { SelectedStripColors = colors }, pages);
            
            var viewController = pager;
            viewController.View.Frame = PgerHazneee.Bounds;
            viewController.WillMoveToParentViewController(this);
            PgerHazneee.AddSubview(viewController.View);
            this.AddChildViewController(viewController);
            viewController.DidMoveToParentViewController(this);
        }
        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);
            KullaniciFoto.Layer.CornerRadius = KullaniciFoto.Frame.Height / 2;
            KullaniciFoto.ClipsToBounds = true;

            YeniEtkinlikOlusturButton.Layer.CornerRadius = YeniEtkinlikOlusturButton.Frame.Height / 2;
            YeniEtkinlikOlusturButton.ClipsToBounds = true;
            ButtonTasarimlariniAyarla(YeniEtkinlikOlusturButton);
        }

        void ButtonTasarimlariniAyarla(UIButton GelenButton)
        {
            GelenButton.Layer.CornerRadius = GelenButton.Bounds.Height / 2;
            GelenButton.ClipsToBounds = true;
            GelenButton.ContentEdgeInsets = new UIEdgeInsets(10, 10, 10, 10);
            var ButtonImage = GelenButton.ImageView.Image;
            var ButtonImageTintImage = ButtonImage.ImageWithRenderingMode(UIImageRenderingMode.AlwaysTemplate);
            GelenButton.SetImage(ButtonImageTintImage, UIControlState.Normal);
            GelenButton.TintColor = UIColor.FromRGB(48, 79, 254);
            GelenButton.Layer.MasksToBounds = false;
            GelenButton.Layer.ShadowColor = UIColor.DarkGray.CGColor;
            GelenButton.Layer.ShadowOpacity = 1.0f;
            GelenButton.Layer.ShadowRadius = GelenButton.Bounds.Height / 2;
            GelenButton.Layer.ShadowOffset = new System.Drawing.SizeF(0f, 2f);
        }
    }
}