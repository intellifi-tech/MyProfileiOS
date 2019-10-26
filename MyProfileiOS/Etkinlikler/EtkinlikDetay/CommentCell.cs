using System;

using Foundation;
using UIKit;

namespace MyProfileiOS.Etkinlikler.EtkinlikDetay
{
    public partial class CommentCell : UITableViewCell
    {
        public static readonly NSString Key = new NSString("CommentCell");
        public static readonly UINib Nib;

        static CommentCell()
        {
            Nib = UINib.FromName("CommentCell", NSBundle.MainBundle);
        }

        protected CommentCell(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }
        public static CommentCell Create()
        {
            var OlusanView = (CommentCell)Nib.Instantiate(null, null)[0];

            return OlusanView;
        }

        public override void LayoutSubviews()
        {
            base.LayoutSubviews();
        }
    }
}
