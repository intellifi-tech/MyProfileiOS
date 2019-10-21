using System;

using Foundation;
using UIKit;

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
    }
}
