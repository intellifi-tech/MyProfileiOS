using System;
using FFImageLoading;
using Foundation;
using MyProfileiOS.DataBasee;
using MyProfileiOS.WebServiceHelper;
using UIKit;
using static MyProfileiOS.EtkinlikDetayBaseVC;

namespace MyProfileiOS.Etkinlikler.EtkinlikDetay
{
    public partial class YorumCustomCell : UITableViewCell
    {
        public static readonly NSString Key = new NSString("YorumCustomCell");
        public static readonly UINib Nib;

        static YorumCustomCell()
        {
            Nib = UINib.FromName("YorumCustomCell", NSBundle.MainBundle);
        }

        protected YorumCustomCell(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }
        public static YorumCustomCell Create()
        {
            var OlusanView = (YorumCustomCell)Nib.Instantiate(null, null)[0];

            return OlusanView;

        }
        public override void LayoutSubviews()
        {
            base.LayoutSubviews();

            //this.ContentView.Layer.CornerRadius = 5f;
            //this.ContentView.ClipsToBounds = true;
            UserPhoto.Layer.CornerRadius = UserPhoto.Frame.Height / 2;
            UserPhoto.ClipsToBounds = true;
            UserPhoto.ContentMode = UIViewContentMode.ScaleAspectFill;
            //this.ContentView.BackgroundColor = UIColor.FromRGB(238, 238, 238);
            //this.ContentView.Frame = new CoreGraphics.CGRect(6, 6, this.ContentView.Frame.Width - 12, this.ContentView.Frame.Height - 12);
        }
        public void UpdateCell(Comment GelenModel1)
        {
            UserYorum.Text = GelenModel1.comment;
            GetUserInfo(UserPhoto, UserName,GelenModel1.user_id.ToString());
            ProfileGitButton.TouchUpInside += ProfileGitButton_TouchUpInside;
        }

        private void ProfileGitButton_TouchUpInside(object sender, EventArgs e)
        {
            
        }

        void GetUserInfo(UIImageView UserImg,UILabel UserName,string UserID)
        {
            WebService webService = new WebService();
            var Donus = webService.OkuGetir("user/" + UserID + "/show");
            if (Donus != null)
            {
                var Model =  Newtonsoft.Json.JsonConvert.DeserializeObject<USER_INFO>(Donus);
                InvokeOnMainThread(delegate () {
                    ImageService.Instance.LoadUrl(Model.profile_photo).Into(UserImg);
                    UserName.Text = Model.name + " " + Model.surname;
                });
            }
            else
            {
                InvokeOnMainThread(delegate () {
                    UserName.Text = "";
                });
            }
        }

        public int HucreYuksekliginiGetir()
        {
            UserYorum.LayoutIfNeeded();
            var ilk = UserYorum.Frame.Height;
            this.LayoutIfNeeded();
            var aaa = UserYorum.SizeThatFits(UserYorum.Frame.Size).Height;
            var fark = aaa - ilk;
            var newfmm = this.ContentView.Frame;
            newfmm.Height += fark;
            this.ContentView.Frame = newfmm;
            return (int)this.ContentView.Frame.Height;
        }
    }
}
