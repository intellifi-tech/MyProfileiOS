using System;

using Foundation;
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

            ArkaHazne.Layer.CornerRadius = 5f;
            ArkaHazne.ClipsToBounds = true;
            UserPhoto.Layer.CornerRadius = UserPhoto.Frame.Height / 2;
            UserPhoto.ClipsToBounds = true;
            this.ContentView.BackgroundColor = UIColor.Black;
        }
        public void UpdateCell(Comment GelenModel1)
        {
            
        }
    }
}
