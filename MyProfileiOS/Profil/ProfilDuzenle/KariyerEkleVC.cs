using DT.iOS.DatePickerDialog;
using Foundation;
using MyProfileiOS.GenericClass;
using MyProfileiOS.WebServiceHelper;
using Newtonsoft.Json;
using System;
using UIKit;

namespace MyProfileiOS
{
    public partial class KariyerEkleVC : UIViewController
    {
        public ProfilBilgiGuncelleSubVC ProfilBilgiGuncelleSubVC1;
        public KariyerEkleVC (IntPtr handle) : base (handle)
        {
        }
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            GirisTarihiButton.TouchUpInside += GirisTarihiButton_TouchUpInside;
            CikisYapButton.TouchUpInside += CikisYapButton_TouchUpInside;
            GeriButton.TouchUpInside += GeriButton_TouchUpInside;
            KaydetButton.TouchUpInside += KaydetButton_TouchUpInside;
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

        private void KaydetButton_TouchUpInside(object sender, EventArgs e)
        {
            if (Bosvarmi())
            {
                WebService webservices = new WebService();
                DeneyimEkle_RootObject DeneyimEkle_RootObject1 = new DeneyimEkle_RootObject()
                {
                    company_title = SirketText.Text,
                    description = "",
                    end_time = Convert.ToDateTime(GirisTarihiButton.Title(UIControlState.Normal)).ToString("yyyy-MM-dd"),
                    start_time = Convert.ToDateTime(CikisYapButton.Title(UIControlState.Normal)).ToString("yyyy-MM-dd"),
                    title = UnvanText.Text
                };

                string jsonString = JsonConvert.SerializeObject(DeneyimEkle_RootObject1);
                var Donus = webservices.ServisIslem("user/storeExperiences", jsonString);
                if (Donus != "Hata")
                {
                    ProfilBilgiGuncelleSubVC1.KariyerGecmisiniYansit();
                    this.DismissModalViewController(true);
                }
            }
        }
        bool Bosvarmi()
        {
            if (string.IsNullOrEmpty(SirketText.Text.Trim()))
            {
                CustomToast.ShowToast(this, "Lütfen þirket adýný belirtin");
                return false;
            }
            else if (string.IsNullOrEmpty(UnvanText.Text.Trim()))
            {
                CustomToast.ShowToast(this, "Lütfen ünvanýnýzý belirtin.");
                return false;
            }
            else if (GirisTarihiButton.Title(UIControlState.Normal) == "Giriþ Tarihi" || CikisYapButton.Title(UIControlState.Normal) == "Çýkýþ Tarihi")
            {
                CustomToast.ShowToast(this, "Lütfen giriþ ve çýkýþ tarihlerini belirtin.");
                return false;
            }
            else
            {
                return true;
            }
        }

        private void GeriButton_TouchUpInside(object sender, EventArgs e)
        {
            this.DismissModalViewController(true);
        }

        private void CikisYapButton_TouchUpInside(object sender, EventArgs e)
        {
            var startingTime = DateTime.Now;
            var dialog = new DatePickerDialog();
            dialog.Show("Tarih Belirtin.", "Tamam", "Ýptal", UIDatePickerMode.Date, (dt) =>
            {
                CikisYapButton.SetTitle(dt.ToShortDateString(), UIControlState.Normal);
            }, startingTime);
        }

        private void GirisTarihiButton_TouchUpInside(object sender, EventArgs e)
        {
            var startingTime = DateTime.Now;
            var dialog = new DatePickerDialog();
            dialog.Show("Tarih Belirtin.", "Tamam", "Ýptal", UIDatePickerMode.Date, (dt) =>
            {
                GirisTarihiButton.SetTitle(dt.ToShortDateString(), UIControlState.Normal);
            }, startingTime);
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            HazneAyarla(SirketAdiHazne);
            HazneAyarla(UnvanHazne);
            HazneAyarla(GirisTarihiHazne);
            HazneAyarla(CikisTarihiHazne);
            HazneAyarla(DisHazne);

            KaydetButton.Layer.CornerRadius = 5f;
            KaydetButton.ClipsToBounds = true;
            
        }
        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);
        }

        #region UI
        void HazneAyarla(UIView GelenView)
        {
            GelenView.Layer.CornerRadius = 5f;
            GelenView.ClipsToBounds = true;
        }
        #endregion

        public class DeneyimEkle_RootObject
        {
            public string title { get; set; }
            public string company_title { get; set; }
            public string start_time { get; set; }
            public string end_time { get; set; }
            public string description { get; set; }
        }
    }
}