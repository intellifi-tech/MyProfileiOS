using System;
using CoreAnimation;
using Foundation;
using UIKit;

namespace MyProfileiOS
{
    public partial class EtkinlikCustomCardView : UITableViewCell
    {
        public static readonly NSString Key = new NSString("EtkinlikCustomCardView");
        public static readonly UINib Nib;

        static EtkinlikCustomCardView()
        {
            Nib = UINib.FromName("EtkinlikCustomCardView", NSBundle.MainBundle);
        }

        protected EtkinlikCustomCardView(IntPtr handle) : base(handle)
        {
        }

        public static EtkinlikCustomCardView Create()
        {
            var OlusanView = (EtkinlikCustomCardView)Nib.Instantiate(null, null)[0];
            //var Color1 = UIColor.Clear.CGColor;
            //var Color2 = UIColor.Black.CGColor;
            //var gradientLayer = new CAGradientLayer();
            //gradientLayer.Colors = new CoreGraphics.CGColor[] { Color1, Color2 };
            //gradientLayer.Locations = new NSNumber[] { 0.0, 1.0 };
            //gradientLayer.Frame = OlusanView.AltBarHazne.Frame;
            //OlusanView.AltBarHazne.Layer.InsertSublayer(gradientLayer, 0);
            return OlusanView;

        }

        public override void LayoutSubviews()
        {
            base.LayoutSubviews();
           
            EtkinlikFoto.ContentMode = UIViewContentMode.ScaleAspectFill;
            EtkinlikFoto.ClipsToBounds = true;
            IconTint(KatilimciIcon);
            IconTint(GirisSaatiIcon);
            IconTint(CikisSaatiIconn);
            AciklamaLabel.Text = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation";

        }
        void IconTint(UIImageView GelenImageView)
        {
            var IconImage = GelenImageView.Image.ImageWithAlignmentRectInsets(new UIEdgeInsets(-2, -2, -2, -2));
            var TintImage = IconImage.ImageWithRenderingMode(UIImageRenderingMode.AlwaysTemplate);
            GelenImageView.Image = TintImage;
            GelenImageView.TintColor = UIColor.White;
        }
    }
}
