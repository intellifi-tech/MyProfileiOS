using System;
using FFImageLoading;
using Foundation;
using UIKit;
using static MyProfileiOS.EtkinlikDetayBaseVC;

namespace MyProfileiOS.Etkinlikler.EtkinlikDetay
{
    public partial class KatilimciCustomItem : UITableViewCell
    {
        public static readonly NSString Key = new NSString("KatilimciCustomItem");
        public static readonly UINib Nib;

        static KatilimciCustomItem()
        {
            Nib = UINib.FromName("KatilimciCustomItem", NSBundle.MainBundle);
        }

        protected KatilimciCustomItem(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }
        public static KatilimciCustomItem Create()
        {
            var OlusanView = (KatilimciCustomItem)Nib.Instantiate(null, null)[0];

            return OlusanView;
        }

        public void UpdateCell(User GelenUser)
        {
            UserName.Text = GelenUser.name + " " + GelenUser.surname;
            UserTitle.Text = GelenUser.title;
            ImageService.Instance.LoadUrl(GelenUser.profile_photo).Into(UserPhoto);
        }
    }
}
