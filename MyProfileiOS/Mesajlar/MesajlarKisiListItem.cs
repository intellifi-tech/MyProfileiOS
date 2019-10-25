using System;
using FFImageLoading;
using Foundation;
using UIKit;
using static MyProfileiOS.MesajlarViewController;

namespace MyProfileiOS
{
    public partial class MesajlarKisiListItem : UITableViewCell
    {
        public static readonly NSString Key = new NSString("MesajlarKisiListItem");
        public static readonly UINib Nib;
        
        static MesajlarKisiListItem()
        {
            Nib = UINib.FromName("MesajlarKisiListItem", NSBundle.MainBundle);
        }

        protected MesajlarKisiListItem(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }
        public static MesajlarKisiListItem Create()
        {
            var OlusanView = (MesajlarKisiListItem)Nib.Instantiate(null, null)[0];

            return OlusanView;

        }

        public void UpdateCell(Message GelenModel,string MEID)
        {
            UserImage.Layer.CornerRadius = UserImage.Frame.Height / 2;
            UserImage.ClipsToBounds = true;
            if (MEID != GelenModel.from_user_id)
            {
                UserName.Text = GelenModel.from_user.name + " " + GelenModel.from_user.surname;
                LastMessage.Text = GelenModel.message;
                Timee.Text = Convert.ToDateTime(GelenModel.created_at).ToShortTimeString();
                ImageService.Instance.LoadUrl(GelenModel.from_user.profile_photo).Into(UserImage);
                if (GelenModel.status == 0)
                {
                    Badgee.Hidden = false;
                }
                else
                {
                    Badgee.Hidden = true;
                }
            }
            else
            {
                UserName.Text = GelenModel.to_user.name + " " + GelenModel.to_user.surname;
                LastMessage.Text = GelenModel.message;
                Timee.Text = Convert.ToDateTime(GelenModel.created_at).ToShortTimeString();
                if (GelenModel.status == 0)
                {
                    Badgee.Hidden = false;
                }
                else
                {
                    Badgee.Hidden = true;
                }

                ImageService.Instance.LoadUrl(GelenModel.to_user.profile_photo).Into(UserImage);
            }

            
        }
    }
}
