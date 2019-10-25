using Foundation;
using Pager;
using System;
using UIKit;

namespace MyProfileiOS
{
    public partial class ProfilViewControllerSubController : UIViewController
    {
        public ProfilViewControllerSubController (IntPtr handle) : base (handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            IconlariAyarla(KapakEkleIcon);
            IconlariAyarla(ProfilDuzenleIcon);
            HazneleriAyarla(KapakEkleHazne);
            HazneleriAyarla(ProfilDuzenleHazne);
            ProfilFotoDegistirButtonTasarim();
        }
        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);
            var colors = new[]
           {
                UIColor.FromRGB(48, 79, 254),
                UIColor.FromRGB(48, 79, 254),
            };
            var MainStoryBoard = UIStoryboard.FromName("Main", NSBundle.MainBundle);
            var KisiselBilgilerTabController1 = MainStoryBoard.InstantiateViewController("KisiselBilgilerTabController") as KisiselBilgilerTabController;
            var KisiselBilgilerTabController2 = MainStoryBoard.InstantiateViewController("KisiselBilgilerEtkinlikler") as KisiselBilgilerEtkinlikler;
            var KisiselBilgilerTabController3 = MainStoryBoard.InstantiateViewController("KisiselBilgilerFotografGaleri") as KisiselBilgilerFotografGaleri;
            
            
            //KisiselBilgilerFotografGaleri
            KisiselBilgilerTabController1.Title = "HAKKINDA";
            KisiselBilgilerTabController2.Title = "ETKÝNLÝKLER";
            KisiselBilgilerTabController3.Title = "FOTOÐRAFLAR";

            UIViewController[] pages = new UIViewController[]
            {
                KisiselBilgilerTabController1,
                KisiselBilgilerTabController2,
                KisiselBilgilerTabController3,

            };

            var pager = new PagerViewController(new PagerStyle(PagerStyle.Stretched) { SelectedStripColors = colors }, pages);

            var viewController = pager;
            viewController.View.Frame = ContentViewww.Bounds;
            viewController.WillMoveToParentViewController(this);
            ContentViewww.AddSubview(viewController.View);
            this.AddChildViewController(viewController);
            viewController.DidMoveToParentViewController(this);
        }
        void IconlariAyarla(UIImageView Iconn)
        {
            var IconImage = Iconn.Image;
            var TintImage = IconImage.ImageWithRenderingMode(UIImageRenderingMode.AlwaysTemplate);
            Iconn.Image = TintImage;
            Iconn.TintColor = UIColor.White;
        }
        void HazneleriAyarla(UIView GelenView)
        {
            GelenView.BackgroundColor = UIColor.Black.ColorWithAlpha(0.5f);
            GelenView.Layer.CornerRadius = 5f;
            GelenView.ClipsToBounds = true;
        }
        void ProfilFotoDegistirButtonTasarim()
        {
            ProfilFotoDegistirButton.ContentEdgeInsets = new UIEdgeInsets(7, 7, 7, 7);
            ProfilFotoDegistirButton.BackgroundColor = UIColor.FromRGB(48, 79, 254);
            ProfilFotoDegistirButton.Layer.CornerRadius = ProfilFotoDegistirButton.Frame.Height / 2;
            ProfilFotoDegistirButton.Layer.BorderColor = UIColor.White.CGColor;
            ProfilFotoDegistirButton.Layer.BorderWidth = 2f;
            ProfilFotoDegistirButton.ClipsToBounds = true;
            var ButtonImage = ProfilFotoDegistirButton.ImageView.Image;
            var ButtonImageTintImage = ButtonImage.ImageWithRenderingMode(UIImageRenderingMode.AlwaysTemplate);
            ProfilFotoDegistirButton.SetImage(ButtonImageTintImage, UIControlState.Normal);
            ProfilFotoDegistirButton.TintColor = UIColor.White;
            ProfilFotoDegistirButton.Layer.MasksToBounds = false;
            ProfilFotoDegistirButton.Layer.ShadowColor = UIColor.DarkGray.CGColor;
            ProfilFotoDegistirButton.Layer.ShadowOpacity = 1.0f;
            ProfilFotoDegistirButton.Layer.ShadowRadius = ProfilFotoDegistirButton.Bounds.Height / 2;
            ProfilFotoDegistirButton.Layer.ShadowOffset = new System.Drawing.SizeF(0f, 2f);
        }
    }
}