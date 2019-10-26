using CoreGraphics;
using FFImageLoading;
using Foundation;
using MyProfileiOS.DataBasee;
using MyProfileiOS.WebServiceHelper;
using System;
using UIKit;

namespace MyProfileiOS
{
    public partial class ProfilViewKontroller : UIViewController
    {
        public USER_INFO GelenUser;
        public ProfilViewKontroller (IntPtr handle) : base (handle)
        {
        }
        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);
            KullaniciPhoto.Layer.CornerRadius = KullaniciPhoto.Frame.Height / 2;
            KullaniciPhoto.ClipsToBounds = true;
            ButtonTasarimlariniAyarla(TakipButton);

            GetProfilHeader();
        }
        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            SonYuksekligiAyarla.profilViewKontroller = this;
            GelenUser = DataBase.USER_INFO_GETIR()[0];
            SecilenKullanici.UserID = GelenUser.id;
            SetUserPhoto();
        }

        void SetUserPhoto()
        {
            new System.Threading.Thread(new System.Threading.ThreadStart(delegate
            {
                WebService webService = new WebService();
                var Donus = webService.OkuGetir("user/" + SecilenKullanici.UserID + "/show");
                if (Donus != null)
                {
                    InvokeOnMainThread(delegate () {
                        var MemberInfo1 = Newtonsoft.Json.JsonConvert.DeserializeObject<USER_INFO>(Donus);
                        ImageService.Instance.LoadUrl(MemberInfo1.profile_photo).Into(KullaniciPhoto);
                    });
                }
            })).Start();
        }
        ProfilViewControllerSubController viewController;

        void GetProfilHeader()
        {
            var MainStoryBoard = UIStoryboard.FromName("Main", NSBundle.MainBundle);
            var  ProfilViewControllerSubController1 = MainStoryBoard.InstantiateViewController("ProfilViewControllerSubController") as ProfilViewControllerSubController;

            viewController = ProfilViewControllerSubController1;

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
       
        public void SonGenisligeBakk(nfloat yukseklikk)
        {
            viewController.View.Frame = new CGRect(0, 0, UIScreen.MainScreen.Bounds.Width, yukseklikk);
            ProfilScroll.ContentSize = new CGSize(UIScreen.MainScreen.Bounds.Width, yukseklikk);
        }
    }

    public static class SonYuksekligiAyarla
    {
        public static ProfilViewKontroller profilViewKontroller { get; set; }
        public static float HeaderYukseklik { get; set; }
        public static void YukseklikUygula(nfloat Yukseklikk)
        {
            profilViewKontroller.SonGenisligeBakk(Yukseklikk+ HeaderYukseklik);
        }
    }

    public static class SecilenKullanici
    {
        public static string UserID { get; set; }
    }
}