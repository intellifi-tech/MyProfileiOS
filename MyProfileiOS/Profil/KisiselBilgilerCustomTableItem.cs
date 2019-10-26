using System;
using Foundation;
using UIKit;
using static MyProfileiOS.KisiselBilgilerTabController;

namespace MyProfileiOS
{
    public partial class KisiselBilgilerCustomTableItem : UITableViewCell
    {
        public static readonly NSString Key = new NSString("KisiselBilgilerCustomTableItem");
        public static readonly UINib Nib;

        static KisiselBilgilerCustomTableItem()
        {
            Nib = UINib.FromName("KisiselBilgilerCustomTableItem", NSBundle.MainBundle);
        }

        protected KisiselBilgilerCustomTableItem(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        public void UpdateCell(KisiselBilgilerDataModel GelenModel)
        {
            BaslikText.Text = GelenModel.Title;
            IcerikText.Text = GelenModel.Aciklama;



            //Tint
            var IconImage = UIImage.FromBundle(GelenModel.IconPath).ImageWithAlignmentRectInsets(new UIEdgeInsets(-5, -5, -5, -5));
            var TintImage = IconImage.ImageWithRenderingMode(UIImageRenderingMode.AlwaysTemplate);
            Iconn.Image = TintImage;
            Iconn.TintColor = UIColor.FromRGB(97, 97, 97);
        }
    }
}
