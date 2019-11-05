using FFImageLoading;
using Foundation;
using MyProfileiOS.GenericClass;
using MyProfileiOS.WebServiceHelper;
using Newtonsoft.Json;
using ObjCRuntime;
using System;
using System.Collections.Generic;
using UIKit;
using static MyProfileiOS.KesfetViewController;

namespace MyProfileiOS
{
    public partial class KesfetCustomKisiView : UIView
    {
        public List<TakipEttiklerim_RootObject> TakipEttiklerim;
        KesfetViewController KesfetViewController1;
        NearbyUserCoordinate GelenModel;
        public KesfetCustomKisiView (IntPtr handle) : base (handle)
        {
        }
        public static KesfetCustomKisiView Create(NearbyUserCoordinate MapDataModel11, List<TakipEttiklerim_RootObject> Modell, KesfetViewController KesfetViewController2)
        {
            var arr = NSBundle.MainBundle.LoadNib("KesfetCustomKisiView", null, null);
            var v = Runtime.GetNSObject<KesfetCustomKisiView>(arr.ValueAt(0));
            v.KesfetViewController1 = KesfetViewController2;
            v.TakipEttiklerim = Modell;
            v.GelenModel = MapDataModel11;
            //  v.Duzenle();
            return v;
        }

        public override void LayoutSubviews()
        {
            base.LayoutSubviews();
            ArkaHazne.Layer.CornerRadius = 10f;
            ArkaHazne.ClipsToBounds = true;
            ArkaHazne.Layer.MasksToBounds = false;
            ArkaHazne.Layer.CornerRadius = 10;
            ArkaHazne.Layer.ShadowColor = UIColor.DarkGray.CGColor;
            ArkaHazne.Layer.ShadowOpacity = 1.0f;
            ArkaHazne.Layer.ShadowRadius = 6.0f;
            ArkaHazne.Layer.ShadowOffset = new System.Drawing.SizeF(0f, 2f);
            ProfileFoto.ClipsToBounds = true;
            ProfileFoto.Layer.CornerRadius = ProfileFoto.Frame.Height / 2;
            GetUserImage(GelenModel.user.profile_photo, GelenModel.user.cover_photo, ProfileFoto, CoverPhoto);

            AdSoyadLabel.Text = GelenModel.user.name + " " + GelenModel.user.surname;
            TitleLabel.Text = GelenModel.user.title;

            var TakipEttiklerimArasindaVarmi = TakipEttiklerim.FindAll(item2 => item2.to_user_id == GelenModel.user.id);
            if (TakipEttiklerimArasindaVarmi.Count > 0)
            {
                TakipEtButton.SetTitle("Takip",UIControlState.Normal);
            }
            else
            {
                //Takip isteði almak istiyorum
                if (GelenModel.user.userPrivacy.no_follow_up_request == false)
                {
                    TakipEtButton.SetTitle("Takip Et", UIControlState.Normal);
                }
                //Takip isteði almak istemiyorum
                else
                {
                    TakipEtButton.Hidden = true;
                    TakipEtButton.Enabled = false;
                }
                
            }
            TakipEtButton.TouchUpInside += TakipEtButton_TouchUpInside;
        }

        private void TakipEtButton_TouchUpInside(object sender, EventArgs e)
        {
            var UserIdd = GelenModel.user.id;
            var TakipListesi = KesfetViewController1.TakipEttiklerimListe;
            var TakipEttiklerimArasindaVarmi = TakipListesi.FindAll(item => item.to_user_id == UserIdd);
            if (TakipEttiklerimArasindaVarmi.Count <= 0)
            {
                UIAlertView alert = new UIAlertView();
                alert.Title = "MyProfile";
                alert.AddButton("Evet");
                alert.AddButton("Hayýr");
                alert.Message = GelenModel.user.name + " kullanýcýsýný takip etmek istediðinizden emin misiniz?";
                alert.AlertViewStyle = UIAlertViewStyle.Default;
                alert.Clicked += (object s, UIButtonEventArgs ev) =>
                {
                    if (ev.ButtonIndex == 0)
                    {
                        TakipEt(UserIdd);
                        KesfetViewController1.TakipEttiklerimiGetir();
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
                alert.Message = GelenModel.user.name + " kullanýcýnýn takibini býrakmak istediðnizden emin misiniz?";
                alert.AlertViewStyle = UIAlertViewStyle.Default;
                alert.Clicked += (object s, UIButtonEventArgs ev) =>
                {
                    if (ev.ButtonIndex == 0)
                    {
                        TakibiBirak(UserIdd);
                        KesfetViewController1.TakipEttiklerimiGetir();
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
                CustomToast.ShowToast(KesfetViewController1, "Takip edildi!", ToastType.Success);
                TakipEtButton.SetTitle("Takip", UIControlState.Normal);
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
                CustomToast.ShowToast(KesfetViewController1, "Takip durduruldu.", ToastType.Warnig);
                TakipEtButton.SetTitle("Takip Et", UIControlState.Normal);
                return;
            }
        }

        void GetUserImage(string pppath, string cppath,UIImageView PPIMW,UIImageView CPIMW)
        {
            new System.Threading.Thread(new System.Threading.ThreadStart(delegate
            {
                InvokeOnMainThread(delegate () {

                    if (!String.IsNullOrEmpty(cppath))
                    {
                        ImageService.Instance.LoadUrl(cppath).Into(CPIMW);
                        CPIMW.ContentMode = UIViewContentMode.ScaleAspectFill;
                        CPIMW.ClipsToBounds = true;
                    }

                    ImageService.Instance.LoadUrl(pppath).Into(PPIMW);
                });
            })).Start();
        }

        public class TakipClass
        {
            public int to_user_id { get; set; }
        }
    }
}