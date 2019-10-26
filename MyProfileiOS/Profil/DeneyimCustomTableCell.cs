using System;

using Foundation;
using UIKit;
using static MyProfileiOS.KisiselBilgilerTabController;

namespace MyProfileiOS.Profil
{
    public partial class DeneyimCustomTableCell : UITableViewCell
    {
        public static readonly NSString Key = new NSString("DeneyimCustomTableCell");
        public static readonly UINib Nib;

        static DeneyimCustomTableCell()
        {
            Nib = UINib.FromName("DeneyimCustomTableCell", NSBundle.MainBundle);
        }

        protected DeneyimCustomTableCell(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        public static DeneyimCustomTableCell Create()
        {
            var OlusanView = (DeneyimCustomTableCell)Nib.Instantiate(null, null)[0];

            return OlusanView;

        }
        public void UpdateCell(UserExperience GelenModel)
        {
            SirketAdi.Text = GelenModel.company.name;
            Tarih.Text = Convert.ToDateTime(GelenModel.start_time).Year.ToString() +
                                                                                         " - " +
                         Convert.ToDateTime(GelenModel.end_time).Year.ToString();
            Tarih.Layer.CornerRadius = Tarih.Frame.Height / 2;
            Tarih.ClipsToBounds = true;
            TitleLabel.Text = GelenModel.title;



            //Tint
            var IconImage = Iconn.Image.ImageWithAlignmentRectInsets(new UIEdgeInsets(-5, -5, -5, -5));
            var TintImage = IconImage.ImageWithRenderingMode(UIImageRenderingMode.AlwaysTemplate);
            Iconn.Image = TintImage;
            Iconn.TintColor = UIColor.FromRGB(97, 97, 97);
        }
    }
}
