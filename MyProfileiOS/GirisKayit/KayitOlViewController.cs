using Foundation;
using MyProfileiOS.DataBasee;
using MyProfileiOS.GenericClass;
using MyProfileiOS.WebServiceHelper;
using Newtonsoft.Json;
using System;
using UIKit;

namespace MyProfileiOS
{
    public partial class KayitOlViewController : UIViewController
    {
        USER_INFO MemberInfo1 = new USER_INFO();
        public KayitOlViewController (IntPtr handle) : base (handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            IconlariAyarla(AdIcon);
            IconlariAyarla(SoyadIcon);
            IconlariAyarla(EmailIcon);
            IconlariAyarla(SifreIcon);
            HazneleriAyarla(AdHazne);
            HazneleriAyarla(SoyadHazne);
            HazneleriAyarla(EmailHazne);
            HazneleriAyarla(SifreHazne);
            SifreText.SecureTextEntry = true;
            KayitButton.Layer.CornerRadius = 15f;
            KayitButton.ClipsToBounds = true;
            KayitButton.TouchUpInside += KayitButton_TouchUpInside;
        }

        private void KayitButton_TouchUpInside(object sender, EventArgs e)
        {
            if (Bosmu())
            {
                WebService webService = new WebService();
                KayitRootObject userLogin = new KayitRootObject()
                {
                    email = EmailText.Text.Trim(),
                    password = SifreText.Text,
                    name = AdText.Text,
                    surname = SoyadText.Text
                };
                string jsonString = JsonConvert.SerializeObject(userLogin);
                var Donus = webService.ServisIslem("user/register", jsonString);
                if (Donus == "Hata")
                {
                    CustomToast.ShowToast(this, "Bir Sorun Olu�tu! L�tfen tekrar deneyin..", ToastType.Error);
                    return;
                }
                else
                {
                    MemberInfo1 = Newtonsoft.Json.JsonConvert.DeserializeObject<USER_INFO>(Donus);
                    DataBase.USER_INFO_EKLE(MemberInfo1);
                    var appDelegate = UIApplication.SharedApplication.Delegate as AppDelegate;
                    appDelegate.SetAnaMainController();
                }
            }
        }
        bool Bosmu()
        {
            if (EmailText.Text.Trim() == "")
            {
                CustomToast.ShowToast(this, "L�tfen email ve �ifrenizi belirtin!", ToastType.Error);
                return false;
            }
            else if (SifreText.Text.Trim() == "")
            {
                CustomToast.ShowToast(this, "L�tfen email ve �ifrenizi belirtin!", ToastType.Error);
                return false;
            }
            else if (SifreText.Text.Length < 6)
            {
                CustomToast.ShowToast(this, "�ifre 6 kararkterden k���k olamaz!", ToastType.Error);
                return false;
            }
            else if (AdText.Text.Trim() == "")
            {
                CustomToast.ShowToast(this, "L�tfen ad�n�z� belirtin!", ToastType.Error);
                return false;
            }
            else if (SoyadText.Text.Trim() == "")
            {
                CustomToast.ShowToast(this, "L�tfen soyad�n�z� belirtin!", ToastType.Error);
                return false;
            }
            else
            {
                return true;
            }
        }
        void IconlariAyarla(UIImageView Iconn)
        {
            //Tint
            var IconImage = Iconn.Image.ImageWithAlignmentRectInsets(new UIEdgeInsets(-5, -5, -5, -5));
            var TintImage = IconImage.ImageWithRenderingMode(UIImageRenderingMode.AlwaysTemplate);
            Iconn.Image = TintImage;
            Iconn.TintColor = UIColor.FromRGB(97, 97, 97);

            //Padding
            //Iconn.LayoutMargins =new UIEdgeInsets(10, 10, 10, 10);

        }

        void HazneleriAyarla(UIView Hazne)
        {
            Hazne.Layer.CornerRadius = 10f;
            Hazne.ClipsToBounds = true;
        }
        public class KayitRootObject
        {
            public string name { get; set; }
            public string surname { get; set; }
            public string email { get; set; }
            public string password { get; set; }
        }
    }
}