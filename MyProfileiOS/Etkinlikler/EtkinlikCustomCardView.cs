using System;
using System.Collections.Generic;
using CoreAnimation;
using CoreGraphics;
using FFImageLoading;
using Foundation;
using MyProfileiOS.WebServiceHelper;
using UIKit;
using static MyProfileiOS.TakipEttiklerimEtkinliklerViewController;

namespace MyProfileiOS
{
    public partial class EtkinlikCustomCardView : UITableViewCell
    {
        public static readonly NSString Key = new NSString("EtkinlikCustomCardView");
        public static readonly UINib Nib;
        
        static EtkinlikCustomCardView()
        {
            Nib = UINib.FromName("EtkinlikCustomCardView", NSBundle.MainBundle);
        }

        protected EtkinlikCustomCardView(IntPtr handle) : base(handle)
        {
        }

        public static EtkinlikCustomCardView Create()
        {
            var OlusanView = (EtkinlikCustomCardView)Nib.Instantiate(null, null)[0];

            return OlusanView;

        }

        public override void LayoutSubviews()
        {
            base.LayoutSubviews();
           
            EtkinlikFoto.ContentMode = UIViewContentMode.ScaleAspectFill;
            EtkinlikFoto.ClipsToBounds = true;
            IconTint(KatilimciIcon);
            IconTint(GirisSaatiIcon);
            IconTint(CikisSaatiIconn);
            UserPhoto.Layer.CornerRadius = UserPhoto.Frame.Height / 2;
            UserPhoto.ClipsToBounds = true;
        }

        public void UpdateCell(UserAttendedEvent GelenModel1)
        {
            UserNameText.Text = "";
            UserTitleText.Text = "";
            GirisSaatiText.Text = "";
            CikisSaatiText.Text = "";
            KatilimciText.Text = "";


            if (GelenModel1.UserInfo == null)
            {
                GelenModel1.UserInfo = GelenModel1.user;
            }
            
            var aaa = GelenModel1.UserInfo;
            if (!String.IsNullOrEmpty(GelenModel1.UserInfo.profile_photo))
                ImageService.Instance.LoadUrl(GelenModel1.UserInfo.profile_photo).Into(UserPhoto);

            if (!String.IsNullOrEmpty(GelenModel1.event_image))
                ImageService.Instance.LoadUrl(GelenModel1.event_image).Into(EtkinlikFoto);

            UserNameText.Text = GelenModel1.UserInfo.name + " " + GelenModel1.UserInfo.surname;
            UserTitleText.Text = GelenModel1.UserInfo.title;
        
            if (GelenModel1.date_of_participation != "")
            {
                GirisSaatiText.Text = Convert.ToDateTime(GelenModel1.date_of_participation).ToShortTimeString();
            }

            if (GelenModel1.end_date != "")
            {
                CikisSaatiText.Text = Convert.ToDateTime(GelenModel1.end_date).ToShortTimeString();
            }

            if (DateTime.Now > Convert.ToDateTime(GelenModel1.end_date))
            {
                //viewholder.GirisSaati.SetTextColor(Color.Red);
                //viewholder.CikisSaati.SetTextColor(Color.Red);
                var attrString1 = new NSAttributedString(GirisSaatiText.Text, new UIStringAttributes { StrikethroughStyle = NSUnderlineStyle.Single });
                var attrString2 = new NSAttributedString(CikisSaatiText.Text, new UIStringAttributes { StrikethroughStyle = NSUnderlineStyle.Single });

                GirisSaatiText.AttributedText = attrString1;
                CikisSaatiText.AttributedText = attrString2;


            }
            else
            {
                //viewholder.GirisSaati.SetTextColor(Color.White);
                //viewholder.CikisSaati.SetTextColor(Color.White);
            }
            KatilimciSayisiniGetir(GelenModel1.event_id.ToString(),KatilimciText);
            AciklamaLabel.Text = GelenModel1.event_description;

        }
        void IconTint(UIImageView GelenImageView)
        {
            var IconImage = GelenImageView.Image.ImageWithAlignmentRectInsets(new UIEdgeInsets(-2, -2, -2, -2));
            var TintImage = IconImage.ImageWithRenderingMode(UIImageRenderingMode.AlwaysTemplate);
            GelenImageView.Image = TintImage;
            GelenImageView.TintColor = UIColor.White;
        }

        void KatilimciSayisiniGetir(string EventID, UILabel KatilimciSayiText)
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
                        var Countt = Newtonsoft.Json.JsonConvert.DeserializeObject<RootObject>(aaaa).user_attended_event.Count;
                        InvokeOnMainThread(delegate ()
                        {
                            KatilimciSayiText.Text = Countt.ToString();
                        });
                    }
                    catch 
                    {
                        InvokeOnMainThread(delegate ()
                        {
                            KatilimciSayiText.Text = "-";
                        }); 
                    }
                   

                }



            })).Start();
        }


        //Detay Sayfası İçin
       public int LabelYusekliginiGetir()
        {
            AciklamaLabel.LayoutIfNeeded();
            var ilk = AciklamaLabel.Frame.Height;
            this.LayoutIfNeeded();
            var aaa = AciklamaLabel.SizeThatFits(AciklamaLabel.Frame.Size).Height;
            var fark = aaa - ilk;
            var newfmm = this.ContentView.Frame;
            newfmm.Height += fark;
            this.ContentView.Frame = newfmm;
            return (int)this.ContentView.Frame.Height;
        }

        public class RootObject
        {
            public List<object> user_attended_event { get; set; }
        }
    }
}
