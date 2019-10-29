using CoreGraphics;
using Foundation;
using MyProfileiOS.DataBasee;
using MyProfileiOS.GenericClass;
using MyProfileiOS.WebServiceHelper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using UIKit;
using static MyProfileiOS.KisiselBilgilerFotografGaleri;

namespace MyProfileiOS
{
    public partial class GaleriBuyutVC : UIViewController
    {
        public UIImage GelenImage;
        public UserGallery GelenGaleriDTO;
        List<BEGENILEN_FOTOLAR> BegenilenFotolar = new List<BEGENILEN_FOTOLAR>();
        bool FotoBegenildimi = false;
        public GaleriBuyutVC (IntPtr handle) : base (handle)
        {
        }
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
        }
        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);
            #region Zoom Islemleri
            var boyut = new System.Drawing.Size((int)GelenImage.Size.Width, (int)GelenImage.Size.Height);
            UIImageView imageView = new UIImageView(new CGRect(0, 0, boyut.Width, boyut.Height));
            imageView.Image = GelenImage;

            imageView.Center = scrollView.Center;

            scrollView.ContentSize = imageView.Frame.Size;
            scrollView.AddSubview(imageView);

            scrollView.MaximumZoomScale = 3f;
            scrollView.MinimumZoomScale = 1f;

            scrollView.ViewForZoomingInScrollView += (UIScrollView sv) => {
                return imageView;
            };
            var doubleTap = new UITapGestureRecognizer(OnDoubleTap)
            {
                NumberOfTapsRequired = 2
            };
            scrollView.AddGestureRecognizer(doubleTap);
            #endregion

            GeriButton.TouchUpInside += GeriButton_TouchUpInside;
            BegenButton.TouchUpInside += BegenButton_TouchUpInside;
            BegenButton2.TouchUpInside += BegenButton2_TouchUpInside;
            ProfilFotoYapButton.TouchUpInside += ProfilFotoYapButton_TouchUpInside;
            SilButton.TouchUpInside += SilButton_TouchUpInside;
        }

        private void SilButton_TouchUpInside(object sender, EventArgs e)
        {
            UIAlertView alert = new UIAlertView();
            alert.Title = "MyProfile";
            alert.AddButton("Evet");
            alert.AddButton("Hayýr");
            alert.Message = "Fotoðrafý silmek istediðinizden emin misniz?";
            alert.AlertViewStyle = UIAlertViewStyle.Default;
            alert.Clicked += (object s, UIButtonEventArgs ev) =>
            {
                if (ev.ButtonIndex == 0)
                {
                    FotografSil();
                    alert.Dispose();
                }
                else
                {
                    alert.Dispose();
                }
            };
            alert.Show();
        }

        private void ProfilFotoYapButton_TouchUpInside(object sender, EventArgs e)
        {
            UIAlertView alert = new UIAlertView();
            alert.Title = "MyProfile";
            alert.AddButton("Evet");
            alert.AddButton("Hayýr");
            alert.Message = "Bu fotoðrafý profil fotoðrafý yapmak istiyor musunuz?";
            alert.AlertViewStyle = UIAlertViewStyle.Default;
            alert.Clicked += (object s, UIButtonEventArgs ev) =>
            {
                if (ev.ButtonIndex == 0)
                {
                    using (var stream = new MemoryStream())
                    {
                        string _base64String;
                        NSData imageData = GelenImage.AsJPEG(compressionQuality: 0.1f);
                        _base64String = imageData.GetBase64EncodedString(NSDataBase64EncodingOptions.None);
                        FotoGuncelle(_base64String);
                    }
                    alert.Dispose();
                }
                else
                {
                    alert.Dispose();
                }
            };
            alert.Show();
        }

        private void BegenButton2_TouchUpInside(object sender, EventArgs e)
        {
            FotografiBegen();
        }

        private void BegenButton_TouchUpInside(object sender, EventArgs e)
        {
            FotografiBegen();
        }

        private void GeriButton_TouchUpInside(object sender, EventArgs e)
        {
            this.DismissViewController(true, null);
        }

        private void OnDoubleTap(UIGestureRecognizer gesture)
        {
            scrollView.ZoomScale = (scrollView.ZoomScale.Equals(1)) ? 2 : 1;
        }
        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            BegeniSayisiniGetir();
            BegenilenFotolar = DataBase.BEGENILEN_FOTOLAR_GETIR();
            if (BegenilenFotolar.Count > 0)
            {
                var Bufoto = BegenilenFotolar.FindAll(item => item.foto_id == GelenGaleriDTO.id);
                if (Bufoto.Count > 0)
                {
                    BegenButton.SetImage(UIImage.FromBundle("Images/like_icon_img.png"),UIControlState.Normal);
                   // Begenbutton.ClearColorFilter();
                    FotoBegenildimi = true;
                }
            }
        }

        void BegeniSayisiniGetir()
        {
            if (GelenGaleriDTO.rating == 0)
            {
                BegenenKisiSayisiLabel.Text = "Ýlk beðenen sen ol!";
            }
            else
            {
                BegenenKisiSayisiLabel.Text = GelenGaleriDTO.rating.ToString() + " kiþi beðendi";
            }
        }

        void FotografiBegen()
        {
            var Bufoto = BegenilenFotolar.FindAll(item => item.foto_id == GelenGaleriDTO.id);
            if (Bufoto.Count <= 0)
            {
                WebService webService = new WebService();
                var Donus = webService.OkuGetir("photo/" + GelenGaleriDTO.id.ToString() + "/likePhoto");
                if (Donus != null)
                {
                    BegenButton.SetImage(UIImage.FromBundle("Images/like_icon_img.png"), UIControlState.Normal);
                    //Begenbutton.SetColorFilter(null);
                    FotoBegenildimi = true;
                    DataBase.BEGENILEN_FOTOLAR_EKLE(new BEGENILEN_FOTOLAR() { foto_id = GelenGaleriDTO.id });
                    GelenGaleriDTO.rating++;
                    BegeniSayisiniGetir();
                    BegenilenFotolar = DataBase.BEGENILEN_FOTOLAR_GETIR();
                }
            }
        }

        void FotoGuncelle(string base644)
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
                CustomToast.ShowToast(this, "Profil Fotoðrafýnýz Güncellendi.");
                //AlertHelper.AlertGoster("Profil Fotoðrafýnýz Güncellendi.", this);

                return;
            }
            else
            {
                CustomToast.ShowToast(this, "Bir sorun oluþtu.");
                //AlertHelper.AlertGoster("Bir sorun oluþtu.", this);
                return;
            }
        }

        void FotografSil()
        {
            WebService webService = new WebService();
            var Donus = webService.OkuGetir("photo/" + GelenGaleriDTO.id.ToString() + "/deletePhoto");
            if (Donus != null)
            {
                CustomToast.ShowToast(this, "Fotoðraf silindi");
                //AlertHelper.AlertGoster("Fotoðraf silindi", this);
                this.DismissViewController(true,null);
            }
        }


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

    }
}
