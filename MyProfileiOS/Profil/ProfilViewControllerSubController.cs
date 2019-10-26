using FFImageLoading;
using Foundation;
using MyProfileiOS.DataBasee;
using MyProfileiOS.GenericClass;
using MyProfileiOS.WebServiceHelper;
using Newtonsoft.Json;
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

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            ProfilPhoto.Layer.CornerRadius = ProfilPhoto.Frame.Height / 2;
            ProfilPhoto.Layer.BorderColor = UIColor.White.CGColor;
            ProfilPhoto.Layer.BorderWidth = 3f;
            ProfilPhoto.ClipsToBounds = true;
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
            SonYuksekligiAyarla.HeaderYukseklik = (float)ContentViewww.Frame.Top;
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
            GetUserInfo();
            KapakDuzenleButton.TouchUpInside += KapakDuzenleButton_TouchUpInside;
            ProfilFotoDegistirButton.TouchUpInside += ProfilDuzenleButton_TouchUpInside;
        }

        private void ProfilDuzenleButton_TouchUpInside(object sender, EventArgs e)
        {
            picker2 = new UIImagePickerController();
            picker2.SourceType = UIImagePickerControllerSourceType.PhotoLibrary;
            picker2.MediaTypes = UIImagePickerController.AvailableMediaTypes(UIImagePickerControllerSourceType.PhotoLibrary);
            picker2.FinishedPickingMedia += Picker_FinishedPickingMedia2;
            picker2.Canceled += Picker_Canceled2;
            this.PresentModalViewController(picker2, true);
        }
        UIImagePickerController picker, picker2;
        private void KapakDuzenleButton_TouchUpInside(object sender, EventArgs e)
        {
            picker = new UIImagePickerController();
            picker.SourceType = UIImagePickerControllerSourceType.PhotoLibrary;
            picker.MediaTypes = UIImagePickerController.AvailableMediaTypes(UIImagePickerControllerSourceType.PhotoLibrary);
            picker.FinishedPickingMedia += Picker_FinishedPickingMedia;
            picker.Canceled += Picker_Canceled;
            this.PresentModalViewController(picker, true);
        }

        #region Kapak
        private void Picker_Canceled(object sender, EventArgs e)
        {
            picker.DismissModalViewController(true);
        }

        private void Picker_FinishedPickingMedia(object sender, UIImagePickerMediaPickedEventArgs e)
        {
            bool isImage = false;
            switch (e.Info[UIImagePickerController.MediaType].ToString())
            {
                case "public.image":
                    isImage = true;
                    break;
                case "public.video":
                    break;
            }
            NSUrl referenceURL = e.Info[new NSString("UIImagePickerControllerReferenceUrl")] as NSUrl;
            if (referenceURL != null) Console.WriteLine("Url:" + referenceURL.ToString());
            if (isImage)
            {
                UIImage originalImage = e.Info[UIImagePickerController.OriginalImage] as UIImage;
                if (originalImage != null)
                {
                    string _base64String;
                    NSData imageData = originalImage.AsJPEG(compressionQuality: 0.1f);
                    _base64String = imageData.GetBase64EncodedString(NSDataBase64EncodingOptions.None);
                    FotoGuncelle(_base64String);
                }
            }
            else
            {
                NSUrl mediaURL = e.Info[UIImagePickerController.MediaURL] as NSUrl;
                if (mediaURL != null)
                {
                    Console.WriteLine(mediaURL.ToString());
                }
            }
            picker.DismissModalViewController(true);
        }

        void FotoGuncelle(string base644)
        {
            WebService webService = new WebService();
            var Kullanici = DataBase.USER_INFO_GETIR()[0];
            Kullanici.cover_photo = base644;
            string jsonString = JsonConvert.SerializeObject(Kullanici);
            var Donus = webService.ServisIslem("user/update", jsonString, Method: "PUT");
            if (Donus != "Hata")
            {
                var GuncelKullanici = Newtonsoft.Json.JsonConvert.DeserializeObject<FotografGuncelleModel>(Donus);
                USER_INFO GuncellenecekDTO = new USER_INFO()
                {
                    cover_photo = GuncelKullanici.user.cover_photo,
                    api_token = Kullanici.api_token,
                    career_history = GuncelKullanici.user.career_history,
                    company_id = GuncelKullanici.user.company_id,
                    credentials = GuncelKullanici.user.credentials,
                    date_of_birth = GuncelKullanici.user.date_of_birth,
                    email = GuncelKullanici.user.email,
                    id = GuncelKullanici.user.id,
                    localid = Kullanici.localid,
                    name = GuncelKullanici.user.name,
                    profile_photo = GuncelKullanici.user.profile_photo,
                    sector_id = GuncelKullanici.user.sector_id,
                    short_biography = GuncelKullanici.user.short_biography,
                    status = GuncelKullanici.user.status,
                    surname = GuncelKullanici.user.surname,
                    title = GuncelKullanici.user.title,
                };

                DataBase.USER_INFO_Guncelle(GuncellenecekDTO);
                ImageService.Instance.LoadUrl(GuncellenecekDTO.cover_photo).Into(KapakPhoto);
                CustomToast.ShowToast(this,"Kapak Fotoðrafýnýz Güncellendi.", ToastType.Success);
                //KullaniciBilgileriniYansit(false);
                return;
            }
            else
            {
                CustomToast.ShowToast(this, "Bir sorun oluþtu.", ToastType.Success);
                return;
            }
        }
        #endregion

        #region Profil
        private void Picker_Canceled2(object sender, EventArgs e)
        {
            picker2.DismissModalViewController(true);
        }

        private void Picker_FinishedPickingMedia2(object sender, UIImagePickerMediaPickedEventArgs e)
        {
            bool isImage = false;
            switch (e.Info[UIImagePickerController.MediaType].ToString())
            {
                case "public.image":
                    isImage = true;
                    break;
                case "public.video":
                    break;
            }
            NSUrl referenceURL = e.Info[new NSString("UIImagePickerControllerReferenceUrl")] as NSUrl;
            if (referenceURL != null) Console.WriteLine("Url:" + referenceURL.ToString());
            if (isImage)
            {
                UIImage originalImage = e.Info[UIImagePickerController.OriginalImage] as UIImage;
                if (originalImage != null)
                {
                    string _base64String;
                    NSData imageData = originalImage.AsJPEG(compressionQuality: 0.1f);
                    _base64String = imageData.GetBase64EncodedString(NSDataBase64EncodingOptions.None);
                    FotoGuncelle2(_base64String);
                }
            }
            else
            {
                NSUrl mediaURL = e.Info[UIImagePickerController.MediaURL] as NSUrl;
                if (mediaURL != null)
                {
                    Console.WriteLine(mediaURL.ToString());
                }
            }
            picker2.DismissModalViewController(true);
        }

        void FotoGuncelle2(string base644)
        {
            WebService webService = new WebService();
            var Kullanici = DataBase.USER_INFO_GETIR()[0];
            Kullanici.profile_photo = base644;
            string jsonString = JsonConvert.SerializeObject(Kullanici);
            var Donus = webService.ServisIslem("user/update", jsonString, Method: "PUT");
            if (Donus != "Hata")
            {
                var GuncelKullanici = Newtonsoft.Json.JsonConvert.DeserializeObject<FotografGuncelleModel>(Donus);
                USER_INFO GuncellenecekDTO = new USER_INFO()
                {
                    cover_photo = GuncelKullanici.user.cover_photo,
                    api_token = Kullanici.api_token,
                    career_history = GuncelKullanici.user.career_history,
                    company_id = GuncelKullanici.user.company_id,
                    credentials = GuncelKullanici.user.credentials,
                    date_of_birth = GuncelKullanici.user.date_of_birth,
                    email = GuncelKullanici.user.email,
                    id = GuncelKullanici.user.id,
                    localid = Kullanici.localid,
                    name = GuncelKullanici.user.name,
                    profile_photo = GuncelKullanici.user.profile_photo,
                    sector_id = GuncelKullanici.user.sector_id,
                    short_biography = GuncelKullanici.user.short_biography,
                    status = GuncelKullanici.user.status,
                    surname = GuncelKullanici.user.surname,
                    title = GuncelKullanici.user.title,

                };

                DataBase.USER_INFO_Guncelle(GuncellenecekDTO);
                ImageService.Instance.LoadUrl(GuncellenecekDTO.profile_photo).Into(ProfilPhoto);

                CustomToast.ShowToast(this, "Profil Fotoðrafýnýz Güncellendi.", ToastType.Success);
                //KullaniciBilgileriniYansit(false);
                return;
            }
            else
            {
                CustomToast.ShowToast(this, "Bir sorun oluþtu.", ToastType.Success);
                return;
            }
        }
        #endregion

        void GetUserInfo()
        {
            new System.Threading.Thread(new System.Threading.ThreadStart(delegate
            {
                WebService webService = new WebService();
                var Donus = webService.OkuGetir("user/" + SecilenKullanici.UserID + "/show");
                if (Donus != null)
                {
                    InvokeOnMainThread(delegate () {
                        var MemberInfo1 = Newtonsoft.Json.JsonConvert.DeserializeObject<USER_INFO>(Donus);
                        ImageService.Instance.LoadUrl(MemberInfo1.profile_photo).Into(ProfilPhoto);
                        ImageService.Instance.LoadUrl(MemberInfo1.cover_photo).Into(KapakPhoto);
                        UserNameLabel.Text = MemberInfo1.name + " " + MemberInfo1.surname;
                        UserTitleLabel.Text = MemberInfo1.title;


                    });
                }
            })).Start();
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

        #region DataModels
        public class User
        {
            public string id { get; set; }
            public int type { get; set; }
            public string profile_photo { get; set; }
            public string cover_photo { get; set; }
            public string title { get; set; }
            public string name { get; set; }
            public string surname { get; set; }
            public string career_history { get; set; }
            public string short_biography { get; set; }
            public string credentials { get; set; }
            public string date_of_birth { get; set; }
            public string company_id { get; set; }
            public string company_title { get; set; }
            public string sector_id { get; set; }
            public string email { get; set; }
            public string email_verified_at { get; set; }
            public string status { get; set; }
            public string package { get; set; }
            public string created_at { get; set; }
            public string updated_at { get; set; }
        }
        public class FotografGuncelleModel
        {
            public int status { get; set; }
            public string message { get; set; }
            public User user { get; set; }
        }
        #endregion
    }
}