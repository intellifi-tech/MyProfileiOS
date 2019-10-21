using Foundation;
using ObjCRuntime;
using System;
using UIKit;
using static MyProfileiOS.TakipEttiklerimEtkinliklerViewController;

namespace MyProfileiOS
{
    public partial class TakipEttiklerimHorizontalKisiView : UIView
    {
        public TakipEttiklerimHorizontalKisiView (IntPtr handle) : base (handle)
        {
        }

        public static TakipEttiklerimHorizontalKisiView Create(TakipEttigimmKisilerDataModel Modell, TakipEttiklerimEtkinliklerViewController KesfetViewController2)
        {
            var arr = NSBundle.MainBundle.LoadNib("TakipEttiklerimHorizontalKisiView", null, null);
            var v = Runtime.GetNSObject<TakipEttiklerimHorizontalKisiView>(arr.ValueAt(0));
            //v.KesfetViewController1 = KesfetViewController2;
            //v.GelenModel = Modell;
            //  v.Duzenle();
            return v;
        }

        public override void LayoutSubviews()
        {
            base.LayoutSubviews();
            //ArkaHazne.Layer.CornerRadius = 10f;
            //ArkaHazne.ClipsToBounds = true;
            //ArkaHazne.Layer.MasksToBounds = false;
            //ArkaHazne.Layer.CornerRadius = 10;
            //ArkaHazne.Layer.ShadowColor = UIColor.DarkGray.CGColor;
            //ArkaHazne.Layer.ShadowOpacity = 1.0f;
            //ArkaHazne.Layer.ShadowRadius = 6.0f;
            //ArkaHazne.Layer.ShadowOffset = new System.Drawing.SizeF(0f, 2f);
            ProfileFoto.ClipsToBounds = true;
            ProfileFoto.Layer.CornerRadius = ProfileFoto.Frame.Height / 2;
        }
    }
}