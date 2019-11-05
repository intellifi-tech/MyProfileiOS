using Foundation;
using MyProfileiOS.DataBasee;
using MyProfileiOS.WebServiceHelper;
using Newtonsoft.Json;
using System;
using UIKit;

namespace MyProfileiOS
{
    public partial class GizlilikAyarlaVC : UIViewController
    {
        public GizlilikAyarlaVC (IntPtr handle) : base (handle)
        {
        }
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            GeriButton.TouchUpInside += GeriButton_TouchUpInside;
            KaydetButton.TouchUpInside += KaydetButton_TouchUpInside;
        }

        private void KaydetButton_TouchUpInside(object sender, EventArgs e)
        {
            SetGizlilik();
        }

        private void GeriButton_TouchUpInside(object sender, EventArgs e)
        {
            this.DismissViewController(true, null);
        }

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);
        }
        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            HazneAyarla(DisHanzee);
            KaydetButton.Layer.CornerRadius = 5f;
            KaydetButton.ClipsToBounds = true;
            GetGizlilik();
        }
        void HazneAyarla(UIView GelenView)
        {
            GelenView.Layer.CornerRadius = 5f;
            GelenView.ClipsToBounds = true;
        }
        void SetGizlilik()
        {
            var MEID = DataBase.USER_INFO_GETIR()[0];
            WebService webService = new WebService();
            GIZLILIK set_Ayarlar_DTO = new GIZLILIK()
            {
                no_follow_up_request = Takip_Toggle.On,
                no_message = Mesaj_Toggle.On,
                visibility_on_the_map = Harita_Toogle.On
            };
            string jsonString = JsonConvert.SerializeObject(set_Ayarlar_DTO);
            var Donus = webService.ServisIslem("user/userPrivacySettings", jsonString);
            if (Donus != "Hata")
            {
                var AyarlarDonus = Newtonsoft.Json.JsonConvert.DeserializeObject<Get_Ayarlar_DTO>(Donus);
                this.DismissViewController(true,null);
            }
        }

        void GetGizlilik()
        {
            new System.Threading.Thread(new System.Threading.ThreadStart(delegate
            {
                var MeId = DataBase.USER_INFO_GETIR()[0];
                WebService webService = new WebService();
                var Donus = webService.OkuGetir("user/" + MeId.id.ToString() + "/getUserPrivacySettings");
                if (Donus != null)
                {
                    var aaa = Donus.ToString();
                    var Ayarlarr = Newtonsoft.Json.JsonConvert.DeserializeObject<UserPrivacy>(Donus.ToString());
                    InvokeOnMainThread(delegate ()
                    {
                        Harita_Toogle.On = Ayarlarr.visibility_on_the_map;
                        Mesaj_Toggle.On = Ayarlarr.no_message;
                        Takip_Toggle.On = Ayarlarr.no_follow_up_request;
                    });
                }
            })).Start();
        }

        public class UserPrivacy
        {
            public int id { get; set; }
            public int user_id { get; set; }
            public bool visibility_on_the_map { get; set; }
            public bool no_message { get; set; }
            public bool no_follow_up_request { get; set; }
            public string created_at { get; set; }
            public string updated_at { get; set; }
        }

        public class Get_Ayarlar_DTO
        {
            public int status { get; set; }
            public string message { get; set; }
            public UserPrivacy userPrivacy { get; set; }
        }

        public class GIZLILIK
        {
            public bool visibility_on_the_map { get; set; }
            public bool no_message { get; set; }
            public bool no_follow_up_request { get; set; }
        }
    }
}