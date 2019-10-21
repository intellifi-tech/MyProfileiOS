using Foundation;
using MyProfileiOS.DataBasee;
using MyProfileiOS.GenericClass;
using MyProfileiOS.WebServiceHelper;
using Newtonsoft.Json;
using System;
using UIKit;

namespace MyProfileiOS
{
    public partial class LoginViewController : UIViewController
    {
        public LoginViewController (IntPtr handle) : base (handle)
        {
        }
        USER_INFO MemberInfo1 = new USER_INFO();
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            GirisButton.TouchUpInside += GirisButton_TouchUpInside;
        }

        private void GirisButton_TouchUpInside(object sender, EventArgs e)
        {
            
            if (Bosmu())
            {
                WebService webService = new WebService();
                UserLogin userLogin = new UserLogin()
                {
                    email = EpostaText.Text.Trim(),
                    password = SifreText.Text
                };
                string jsonString = JsonConvert.SerializeObject(userLogin);
                var Donus = webService.ServisIslem("user/login", jsonString);
                if (Donus == "Hata")
                {
                    CustomToast.ShowToast(this, "Giriþ Yapýlamadý!", ToastType.Error);
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
            if (EpostaText.Text.Trim() == "")
            {
                CustomToast.ShowToast(this, "Lütfen email ve þifrenizi belirtin!", ToastType.Error);
                return false;
            }
            else if (SifreText.Text.Trim() == "")
            {
                CustomToast.ShowToast(this, "Lütfen email ve þifrenizi belirtin!", ToastType.Error);
                return false;
            }
            else if (SifreText.Text.Length < 6)
            {
                CustomToast.ShowToast(this, "Þifre 6 karakterden küçük olamaz!", ToastType.Error);
                return false;
            }
            else
            {
                return true;
            }
        }

      

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            IconlariAyarla(EpostaIcon);
            IconlariAyarla(SifreIcon);
            HazneleriAyarla(EpostaHazne);
            HazneleriAyarla(SifreHazne);
            GirisButton.Layer.CornerRadius = 15f;
            GirisButton.ClipsToBounds = true;
            SifreText.SecureTextEntry = true;
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

        #region DtaaModels
        public class UserLogin
        {
            public string email { get; set; }
            public string password { get; set; }
        }
        #endregion
    }
}