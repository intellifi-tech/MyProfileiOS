using CoreGraphics;
using Foundation;
using ObjCRuntime;
using System;
using UIKit;

namespace MyProfileiOS
{
    public partial class ProfilHeaderView2 : UIView
    {
        public ProfilHeaderView2 (IntPtr handle) : base (handle)
        {
        }

        public static ProfilHeaderView2 Create()
        {
            var arr = NSBundle.MainBundle.LoadNib("ProfilHeaderView2", null, null);
            var v = Runtime.GetNSObject<ProfilHeaderView2>(arr.ValueAt(0));
            return v;
        }

        public override void LayoutSubviews()
        {
            base.LayoutSubviews();
            IconlariAyarla(KapakEkleIcon);
            IconlariAyarla(ProfilDuzenleIcon);
            HazneleriAyarla(KapakEkleHazne);
            HazneleriAyarla(ProfilDuzenleHazne);
            ProfilFotoDegistirButtonTasarim();
            FillDataModel();
        }
        void IconlariAyarla(UIImageView Iconn)
        {
            var IconImage = Iconn.Image;
            var TintImage = IconImage.ImageWithRenderingMode(UIImageRenderingMode.AlwaysTemplate);
            Iconn.Image = TintImage;
            Iconn.TintColor = UIColor.White;
        }
        void HazneleriAyarla(UIView GelenView)
        {
            GelenView.BackgroundColor = UIColor.Black.ColorWithAlpha(0.5f);
            GelenView.Layer.CornerRadius = 5f;
            GelenView.ClipsToBounds = true;
        }
        void ProfilFotoDegistirButtonTasarim()
        {
            ProfilFotoDegistirButton.ContentEdgeInsets = new UIEdgeInsets(7, 7, 7, 7);
            ProfilFotoDegistirButton.BackgroundColor = UIColor.FromRGB(48, 79, 254);
            ProfilFotoDegistirButton.Layer.CornerRadius = ProfilFotoDegistirButton.Frame.Height / 2;
            ProfilFotoDegistirButton.Layer.BorderColor = UIColor.White.CGColor;
            ProfilFotoDegistirButton.Layer.BorderWidth = 2f;
            ProfilFotoDegistirButton.ClipsToBounds = true;
            var ButtonImage = ProfilFotoDegistirButton.ImageView.Image;
            var ButtonImageTintImage = ButtonImage.ImageWithRenderingMode(UIImageRenderingMode.AlwaysTemplate);
            ProfilFotoDegistirButton.SetImage(ButtonImageTintImage, UIControlState.Normal);
            ProfilFotoDegistirButton.TintColor = UIColor.White;
            ProfilFotoDegistirButton.Layer.MasksToBounds = false;
            ProfilFotoDegistirButton.Layer.ShadowColor = UIColor.DarkGray.CGColor;
            ProfilFotoDegistirButton.Layer.ShadowOpacity = 1.0f;
            ProfilFotoDegistirButton.Layer.ShadowRadius = ProfilFotoDegistirButton.Bounds.Height / 2;
            ProfilFotoDegistirButton.Layer.ShadowOffset = new System.Drawing.SizeF(0f, 2f);
        }

        void FillDataModel()
        {
            return;
            string[] tableItems = new string[1000];
            for (int i = 0; i < 1000; i++)
            {
                tableItems[i] = "tableItems";

            }
            //DenemeTable.Source = new TableSource(tableItems);
            
        }
        public float GetiHeighttt()
        {
            return 0;
            //string[] tableItems = new string[1000];
            //for (int i = 0; i < 1000; i++)
            //{
            //    tableItems[i] = "tableItems";

            //}
            //DenemeTable.Source = new TableSource(tableItems);
            //DenemeTable.LayoutIfNeeded();
            //CGSize tableViewSize = DenemeTable.ContentSize;
            //DenemeTable.ScrollEnabled = false;
            //var frameee = DenemeTable.Frame;
            //frameee.Height = tableViewSize.Height;
            //DenemeTable.Frame = frameee;

            //var ContentFramee = this.Frame;
            //ContentFramee.Height += tableViewSize.Height;
            //this.Frame = ContentFramee;
            //return (float)this.Frame.Height;
        }
        public class TableSource : UITableViewSource
        {

            string[] TableItems;
            string CellIdentifier = "TableCell";

            public TableSource(string[] items)
            {
                TableItems = items;
            }

            public override nint RowsInSection(UITableView tableview, nint section)
            {
                return TableItems.Length;
            }

            public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
            {
                UITableViewCell cell = tableView.DequeueReusableCell(CellIdentifier);
                string item = TableItems[indexPath.Row];

                //---- if there are no cells to reuse, create a new one
                if (cell == null)
                { cell = new UITableViewCell(UITableViewCellStyle.Default, CellIdentifier); }

                cell.TextLabel.Text = item + " " + indexPath.Row.ToString();

                return cell;
            }
        }
    }
}