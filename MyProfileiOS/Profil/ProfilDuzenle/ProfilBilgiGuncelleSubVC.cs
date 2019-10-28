using CoreGraphics;
using DT.iOS.DatePickerDialog;
using Foundation;
using MyProfileiOS.DataBasee;
using MyProfileiOS.Profil;
using MyProfileiOS.WebServiceHelper;
using System;
using System.Collections.Generic;
using UIKit;
using static MyProfileiOS.KisiselBilgilerTabController;

namespace MyProfileiOS
{
    public partial class ProfilBilgiGuncelleSubVC : UIViewController
    {
        USER_INFO ME;
        public ProfilBilgiGuncelle GelenBase;
        List<UserExperience> UserExperience1 = new List<UserExperience>();
        SektorModel SecilenSektor = null;
        public ProfilBilgiGuncelleSubVC (IntPtr handle) : base (handle)
        {
        }
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            DogumTarihiButton.TouchUpInside += DogumTarihiButton_TouchUpInside;
            SektorButton.TouchUpInside += SektorButton_TouchUpInside;
            DeneyimEkleBut.TouchUpInside += DeneyimEkleButton_TouchUpInside;
            AdText.ShouldReturn += (textField) =>
            {
                textField.ResignFirstResponder();
                return true;
            };
            SoyadText.ShouldReturn += (textField) =>
            {
                textField.ResignFirstResponder();
                return true;
            };
            SirketText.ShouldReturn += (textField) =>
            {
                textField.ResignFirstResponder();
                return true;
            };
            UnvanText.ShouldReturn += (textField) =>
            {
                textField.ResignFirstResponder();
                return true;
            };
        }

        private void DeneyimEkleButton_TouchUpInside(object sender, EventArgs e)
        {
            var UIStoryboard1 = UIStoryboard.FromName("Main", NSBundle.MainBundle);
            KariyerEkleVC controller = UIStoryboard1.InstantiateViewController("KariyerEkleVC") as KariyerEkleVC;
            controller.ModalPresentationStyle = UIModalPresentationStyle.BlurOverFullScreen;
            controller.ProfilBilgiGuncelleSubVC1 = this;
            this.PresentViewController(controller, true, null);
        }

        private void SektorButton_TouchUpInside(object sender, EventArgs e)
        {
            var UIStoryboard1 = UIStoryboard.FromName("Main", NSBundle.MainBundle);
            SektorSecVC controller = UIStoryboard1.InstantiateViewController("SektorSecVC") as SektorSecVC;
            controller.ModalPresentationStyle = UIModalPresentationStyle.FullScreen;
            controller.ProfilBilgiGuncelleSubVC1 = this;
            this.PresentViewController(controller, true, null);
        }

        private void DogumTarihiButton_TouchUpInside(object sender, EventArgs e)
        {
            var startingTime = DateTime.Now;
            var dialog = new DatePickerDialog();
            dialog.Show("Etkinlik Giriþ Saati", "Tamam", "Ýptal", UIDatePickerMode.Date, (dt) =>
            {
                DogumTarihiButton.SetTitle(dt.ToShortDateString(), UIControlState.Normal);
            }, startingTime);
        }

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);
        }
        bool Actinmi = false;
        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            if (!Actinmi)
            {
                ME = DataBase.USER_INFO_GETIR()[0];
                //DeneyimEkleBut.ContentEdgeInsets = new UIEdgeInsets(0, 8, 0, 8);
                KullaniciBilgileriniYansit();
                AddDoneButton();
                Actinmi = true;
            }
        }
        void KullaniciBilgileriniYansit()
        {
            var Kullanici = DataBase.USER_INFO_GETIR();
            if (Kullanici.Count > 0)
            {
                AdText.Text = Kullanici[0].name;
                SoyadText.Text = Kullanici[0].surname;
                HakkindaText.Text = Kullanici[0].short_biography;

                if (!String.IsNullOrEmpty(Kullanici[0].date_of_birth))
                {
                    try
                    {
                        DogumTarihiButton.SetTitle(Convert.ToDateTime(Kullanici[0].date_of_birth).ToShortDateString(),UIControlState.Normal);
                    }
                    catch { }
                }


                if (!string.IsNullOrEmpty(Kullanici[0].profile_photo))
                {
                    //ImageService.Instance.LoadUrl(Kullanici[0].profile_photo)
                    //                                .Transform(new CircleTransformation(15, "#FFFFFF"))
                    //                                .Into(ProfilFoto);
                }

                if (!String.IsNullOrEmpty(Kullanici[0].sector_id))
                {
                    GetSektor(Kullanici[0].sector_id);
                }
                if (!String.IsNullOrEmpty(Kullanici[0].company_id))
                {
                    GetCompanyID(Kullanici[0].company_id);
                }

                if (!String.IsNullOrEmpty(Kullanici[0].title))
                {
                    UnvanText.Text = Kullanici[0].title;
                }

                KariyerGecmisiniYansit();
            }
        }
        void GetCompanyID(string ID)
        {
            new System.Threading.Thread(new System.Threading.ThreadStart(delegate
            {
                try
                {
                    WebService webService = new WebService();
                    var Donus = webService.OkuGetir("company/" + ID.ToString() + "/show");
                    if (Donus != null)
                    {
                        var Modell = Newtonsoft.Json.JsonConvert.DeserializeObject<SirketModel>(Donus.ToString());
                        InvokeOnMainThread(() =>
                        {
                            SirketText.Text = Modell.name;
                        });
                    }

                }
                catch
                {


                }

            })).Start();

        }
        void GetSektor(string ID)
        {
            new System.Threading.Thread(new System.Threading.ThreadStart(delegate
            {
                try
                {
                    WebService webService = new WebService();
                    var Donus = webService.OkuGetir("sector/" + ID.ToString() + "/show");
                    if (Donus != null)
                    {
                        var Modell = Newtonsoft.Json.JsonConvert.DeserializeObject<SektorModel>(Donus.ToString());
                        InvokeOnMainThread(() =>
                        {
                            SecilenSektor = Modell;
                            SektorButton.SetTitle(Modell.name,UIControlState.Normal);
                            //SektorlerSpin.Tag = Modell.id;
                        });
                    }

                }
                catch
                {


                }

            })).Start();
        }
        void AddDoneButton()
        {
            #region DONE BUtton
            UIToolbar toolbar = new UIToolbar();
            toolbar.BarStyle = UIBarStyle.Default;
            toolbar.Translucent = true;
            toolbar.SizeToFit();

            //var gradientLayer = new CAGradientLayer();
            //gradientLayer.Colors = new[] { UIColor.FromRGB(28, 36, 121).CGColor, UIColor.FromRGB(28, 36, 121).CGColor };
            //gradientLayer.Locations = new NSNumber[] { 0, 1 };
            //gradientLayer.StartPoint = new CoreGraphics.CGPoint(0, 1);
            //gradientLayer.EndPoint = new CoreGraphics.CGPoint(1, 0);
            //gradientLayer.Frame = toolbar.Bounds;
            //toolbar.Layer.MasksToBounds = true;
            //toolbar.Layer.InsertSublayer(gradientLayer, 0);
            toolbar.BackgroundColor = UIColor.FromRGB(28, 36, 121);
            toolbar.BarTintColor = UIColor.FromRGB(28, 36, 121);

            UIBarButtonItem doneButton = new UIBarButtonItem("Tamam", UIBarButtonItemStyle.Done,
                                                             (s, e) =>
                                                             {
                                                                 HakkindaText.ResignFirstResponder();
                                                             });

            doneButton.TintColor = UIColor.White;
            toolbar.SetItems(new[] { new UIBarButtonItem(UIBarButtonSystemItem.FlexibleSpace), doneButton }, true);
            HakkindaText.InputAccessoryView = toolbar;
            #endregion
        }
        public void SecilenSektorUygula(SektorModel GelenSector)
        {
            SecilenSektor = GelenSector;
            SektorButton.SetTitle(GelenSector.name, UIControlState.Normal);
        }
        List<DeneyimCustomTableCell> DeneyimViews = new List<DeneyimCustomTableCell>();
        public void KariyerGecmisiniYansit()
        {
            DeneyimViews.ForEach(item => item.RemoveFromSuperview());
            DeneyimViews = new List<DeneyimCustomTableCell>();
            WebService webService = new WebService();
            var Donus = webService.OkuGetir("user/" + SecilenKullanici.UserID + "/userExperiences");
            if (Donus != null)
            {
                var Modell = Newtonsoft.Json.JsonConvert.DeserializeObject<KariyerGecmisi_RootObject>(Donus);
                if (Modell.status == 200)
                {
                    UserExperience1 = Modell.userExperiences;
                    UserExperience1.Reverse();
                    var TestFooterView = new UIView();
                    TestFooterView.BackgroundColor = UIColor.Clear;
                    for (int i = 0; i < UserExperience1.Count; i++)
                    {
                        var DeneyimCell = DeneyimCustomTableCell.Create();
                        DeneyimCell.UpdateCell(UserExperience1[i]);
                        if (i == 0)
                        {
                            DeneyimCell.Frame = new CGRect(27, 40, UIScreen.MainScreen.Bounds.Width, 94f);
                        }
                        else
                        {
                            DeneyimCell.Frame = new CGRect(27, DeneyimViews[i - 1].Frame.Bottom, UIScreen.MainScreen.Bounds.Width, 94f);
                        }

                        DeneyimViews.Add(DeneyimCell);
                        TestFooterView.AddSubview(DeneyimCell);
                    }
                    TestFooterView.Frame = new CGRect(0, 710f, UIScreen.MainScreen.Bounds.Width, DeneyimViews[DeneyimViews.Count - 1].Frame.Bottom);
                    this.View.AddSubview(TestFooterView);
                    var frmm = this.View.Frame;
                    frmm.Height += DeneyimViews[DeneyimViews.Count - 1].Frame.Bottom;
                    this.View.Frame = frmm;
                    GelenBase.ScrollTopp();
                }
            }
        }
        public int GetMaxHeigt()
        {
           return (int)this.View.Frame.Height;
        }
        public class SirketModel
        {
            public int id { get; set; }
            public string name { get; set; }
        }
        public class SektorModel
        {
            public int id { get; set; }
            public string name { get; set; }
        }
    }
}