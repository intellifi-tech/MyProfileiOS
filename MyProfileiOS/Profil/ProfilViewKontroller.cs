using CoreGraphics;
using FFImageLoading;
using Foundation;
using MyProfileiOS.DataBasee;
using MyProfileiOS.GenericClass;
using MyProfileiOS.WebServiceHelper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using UIKit;
using static MyProfileiOS.ChatDetayBase;

namespace MyProfileiOS
{
    public partial class ProfilViewKontroller : UIViewController
    {
        public USER_INFO GelenUser;
        List<TakipEttiklerim_RootObject> TakipEttiklerimDataModel1 = new List<TakipEttiklerim_RootObject>();
        public ProfilViewKontroller(IntPtr handle) : base(handle)
        {
        }
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            TakipButton.TouchUpInside += TakipButton_TouchUpInside;
            MessageButton.TouchUpInside += MessageButton_TouchUpInside;
        }

        private void MessageButton_TouchUpInside(object sender, EventArgs e)
        {
            var LokasyonKisilerStory = UIStoryboard.FromName("ChatDetayVC", NSBundle.MainBundle);
            ChatDetayBase controller = LokasyonKisilerStory.InstantiateViewController("ChatDetayBase") as ChatDetayBase;
            controller.ModalPresentationStyle = UIModalPresentationStyle.FullScreen;
            ChatUserId.UserID = SecilenKullanici.UserID;
            this.PresentViewController(controller, true, null);
        }

        private void TakipButton_TouchUpInside(object sender, EventArgs e)
        {
            if (!TakipDurumm)
            {
                UIAlertView alert = new UIAlertView();
                alert.Title = "MyProfile";
                alert.AddButton("Evet");
                alert.AddButton("Hayýr");
                alert.Message = "Takip etmek istediðinize emin misiniz?";
                alert.AlertViewStyle = UIAlertViewStyle.Default;
                alert.Clicked += (object s, UIButtonEventArgs ev) =>
                {
                    if (ev.ButtonIndex == 0)
                    {

                        TakipEt(Convert.ToInt32(SecilenKullanici.UserID));
                        alert.Dispose();
                    }
                    else
                    {
                        alert.Dispose();
                    }
                };
                alert.Show();
            }
            else
            {
                UIAlertView alert = new UIAlertView();
                alert.Title = "MyProfile";
                alert.AddButton("Evet");
                alert.AddButton("Hayýr");
                alert.Message = "Takibi býrakmak istediðinizden emin misiniz?";
                alert.AlertViewStyle = UIAlertViewStyle.Default;
                alert.Clicked += (object s, UIButtonEventArgs ev) =>
                {
                    if (ev.ButtonIndex == 0)
                    {

                        TakibiBirak(Convert.ToInt32(SecilenKullanici.UserID));
                        alert.Dispose();
                    }
                    else
                    {
                        alert.Dispose();
                    }
                };
                alert.Show();
            }
            
        }

        #region Takip
        void TakipEt(int UserId)
        {
            TakipClass TakipClass1 = new TakipClass()
            {
                to_user_id = UserId
            };
            var jsonstring = JsonConvert.SerializeObject(TakipClass1);
            WebService webService = new WebService();
            var Donus = webService.ServisIslem("user/follow", jsonstring);
            if (Donus != "Hata")
            {
                CustomToast.ShowToast(this, "Takip edildi.", ToastType.Clean);
                TakipButton.SetImage(UIImage.FromBundle("Images/user-error.png"), UIControlState.Normal);
                ButtonTasarimlariniAyarla(TakipButton);
                TakipDurumm = true;
                return;
            }

        }
        void TakibiBirak(int UserId)
        {
            TakipClass TakipClass1 = new TakipClass()
            {
                to_user_id = UserId
            };
            var jsonstring = JsonConvert.SerializeObject(TakipClass1);
            WebService webService = new WebService();
            var Donus = webService.ServisIslem("user/stopFollowing", jsonstring);
            if (Donus != "Hata")
            {
                CustomToast.ShowToast(this, "Takip durduruldu.", ToastType.Clean);
                TakipButton.SetImage(UIImage.FromBundle("Images/user-add.png"), UIControlState.Normal);
                ButtonTasarimlariniAyarla(TakipButton);
                TakipDurumm = false;
                return;
            }
        }
        public class TakipClass
        {
            public int to_user_id { get; set; }
        }

        bool TakipDurumm = false;
        void TakipDurum()
        {
            WebService webService = new WebService();
            var Donus = webService.ServisIslem("user/followings", "");
            if (Donus != "Hata")
            {
                TakipEttiklerimDataModel1 = Newtonsoft.Json.JsonConvert.DeserializeObject<List<TakipEttiklerim_RootObject>>(Donus);
                var Durum = DataBase.USER_INFO_GETIR()[0];
                var TakipEdiyormuyum = TakipEttiklerimDataModel1.FindAll(item => item.to_user_id.ToString() == SecilenKullanici.UserID);

                if (TakipEdiyormuyum.Count > 0) //Takip Ediyorum
                {
                    TakipButton.SetImage(UIImage.FromBundle("Images/user-error.png"), UIControlState.Normal);
                    ButtonTasarimlariniAyarla(TakipButton);
                    TakipDurumm = true;
                }
                else
                {
                    TakipButton.SetImage(UIImage.FromBundle("Images/user-add.png"), UIControlState.Normal);
                    ButtonTasarimlariniAyarla(TakipButton);
                    TakipDurumm = false;
                }
            }
        }
        #endregion

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);
            KullaniciPhoto.Layer.CornerRadius = KullaniciPhoto.Frame.Height / 2;
            KullaniciPhoto.ClipsToBounds = true;
            ButtonTasarimlariniAyarla(TakipButton);
            ButtonTasarimlariniAyarla(MessageButton);
            GeriButton.TouchUpInside += GeriButton_TouchUpInside;
            GetProfilHeader();
        }

        private void GeriButton_TouchUpInside(object sender, EventArgs e)
        {
            this.DismissViewController(true, null);
        }
        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            SonYuksekligiAyarla.profilViewKontroller = this;
            if (SecilenKullanici.UserID == null)
            {
                GelenUser = DataBase.USER_INFO_GETIR()[0];
                SecilenKullanici.UserID = GelenUser.id;
                TakipButton.Hidden = true;
                MessageButton.Hidden = true;
                GeriButton.Hidden = true;
               
            }
            else
            {
                TakipDurum();
                KullaniciPhoto.Hidden = true;
            }
            
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
                    InvokeOnMainThread(delegate ()
                    {
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
            var ProfilViewControllerSubController1 = MainStoryBoard.InstantiateViewController("ProfilViewControllerSubController") as ProfilViewControllerSubController;

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

        bool takiptencik = true;
        public void TakipEtButtonGuncelle()
        {
            
            takiptencik = true;
        }
        public class TakipEttiklerim_RootObject
        {
            public int from_user_id { get; set; }
            public int to_user_id { get; set; }
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