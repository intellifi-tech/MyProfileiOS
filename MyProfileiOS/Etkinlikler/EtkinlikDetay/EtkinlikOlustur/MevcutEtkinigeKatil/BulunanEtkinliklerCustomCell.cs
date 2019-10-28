using System;
using System.Collections.Generic;
using FFImageLoading;
using Foundation;
using MyProfileiOS.WebServiceHelper;
using UIKit;
using static MyProfileiOS.MevcutEtkinlikBaseVC;

namespace MyProfileiOS.Etkinlikler.EtkinlikDetay.EtkinlikOlustur.MevcutEtkinigeKatil
{
    public partial class BulunanEtkinliklerCustomCell : UITableViewCell
    {
        public static readonly NSString Key = new NSString("BulunanEtkinliklerCustomCell");
        public static readonly UINib Nib;

        static BulunanEtkinliklerCustomCell()
        {
            Nib = UINib.FromName("BulunanEtkinliklerCustomCell", NSBundle.MainBundle);
        }

        protected BulunanEtkinliklerCustomCell(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }
        public static BulunanEtkinliklerCustomCell Create()
        {
            var OlusanView = (BulunanEtkinliklerCustomCell)Nib.Instantiate(null, null)[0];

            return OlusanView;
        }

        public void UpdateCell(NearbyEvent GelenEvent)
        {
            EtkinlikPhoto.Layer.CornerRadius = EtkinlikPhoto.Frame.Height / 2;
            EtkinlikPhoto.ClipsToBounds = true;
            EtkinlikAdi.Text = "";
            KatilimciSayisi.Text = "";
            GetEventInfo(GelenEvent.eventt.id.ToString(), EtkinlikAdi, KatilimciSayisi,EtkinlikPhoto);
            
        }

        void GetEventInfo(string EventID,UILabel EtkinlikAdii, UILabel EtkinlikKatilimci,UIImageView EtkinlikImg)
        {

            new System.Threading.Thread(new System.Threading.ThreadStart(delegate
            {
                WebService webService = new WebService();
                var Donuss = webService.OkuGetir("event/" + EventID + "/show");
                if (Donuss != null)
                {
                    try
                    {
                        var aaaa = Donuss.ToString();
                        var Detayy = Newtonsoft.Json.JsonConvert.DeserializeObject<RootObject>(aaaa);
                        InvokeOnMainThread(delegate ()
                        {
                            EtkinlikAdii.Text = Detayy.title;
                            ImageService.Instance.LoadUrl(Detayy.user_attended_event[0].event_image).Into(EtkinlikImg);
                            EtkinlikKatilimci.Text = Detayy.user_attended_event.Count + " Kişi bu etkinliğe katıldı.";
                        });
                    }
                    catch
                    {
                        InvokeOnMainThread(delegate ()
                        {
                            EtkinlikAdii.Text = "-";
                            KatilimciSayisi.Text = "-";
                        });
                    }
                }
            })).Start();
        }



        
        public class UserAttendedEvent
        {
            public int id { get; set; }
            public int event_id { get; set; }
            public int user_id { get; set; }
            public string event_description { get; set; }
            public string event_image { get; set; }
            public string date_of_participation { get; set; }
            public string end_date { get; set; }
            public int rating { get; set; }
            public string created_at { get; set; }
            public string updated_at { get; set; }
        }

        public class RootObject
        {
            public int id { get; set; }
            public string title { get; set; }
            public double latitude { get; set; }
            public double longitude { get; set; }
            public string created_at { get; set; }
            public string updated_at { get; set; }
            public List<UserAttendedEvent> user_attended_event { get; set; }
        }


    }
}
