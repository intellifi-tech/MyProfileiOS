using CoreGraphics;
using Foundation;
using ObjCRuntime;
using System;
using System.Collections.Generic;
using UIKit;

namespace MyProfileiOS
{
    public partial class KisiselBilgilerCustomView : UIView
    {
        List<KisiselBilgilerDataModel> KisiselBilgilerDataModel1 = new List<KisiselBilgilerDataModel>();

        public KisiselBilgilerCustomView (IntPtr handle) : base (handle)
        {
        }
        public static KisiselBilgilerCustomView Create(KisiselBilgilerCustomView Modell)
        {
            var arr = NSBundle.MainBundle.LoadNib("KisiselBilgilerCustomView", null, null);
            var v = Runtime.GetNSObject<KisiselBilgilerCustomView>(arr.ValueAt(0));
            return v;
        }
        public override void LayoutSubviews()
        {
            base.LayoutSubviews();
        }

        void KisiselBilgileriGetir()
        {
            KisiselBilgilerDataModel1.Add(new KisiselBilgilerDataModel() {
                Title = "Hakkýnda",
                Aciklama = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat."
            });

            KisiselBilgilerDataModel1.Add(new KisiselBilgilerDataModel()
            {
                Title = "Doðum Tarihi",
                Aciklama = "12.12.1994"
            });
            KisiselBilgilerDataModel1.Add(new KisiselBilgilerDataModel()
            {
                Title = "Þirket",
                Aciklama = "IntelliFi Yazýlým Danýþmanlýk ve Reklam Hizmetleri"
            });
            KisiselBilgilerDataModel1.Add(new KisiselBilgilerDataModel()
            {
                Title = "Sektör",
                Aciklama = "Yazýlým Sektörü"
            });


            //EtkinlikTableView.RegisterClassForCellReuse(typeof(EtkinlikCustomCardView), "EtkinlikCustomCardView");
            KisiselBilgilerTableVieww.RegisterNibForCellReuse(UINib.FromName("KisiselBilgilerCustomTableItem", NSBundle.MainBundle), "KisiselBilgilerCustomTableItem");
            KisiselBilgilerTableVieww.Source = new KisiselBilgilerTableSource(KisiselBilgilerDataModel1, this);
            KisiselBilgilerTableVieww.SeparatorStyle = UITableViewCellSeparatorStyle.None;
            KisiselBilgilerTableVieww.RowHeight = 84f;
            KisiselBilgilerTableVieww.ReloadData();
        }

        float GetHeight()
        {
            KisiselBilgileriGetir();
            KisiselBilgilerTableVieww.LayoutIfNeeded();
            CGSize tableViewSize = KisiselBilgilerTableVieww.ContentSize;
            KisiselBilgilerTableVieww.ScrollEnabled = false;
            var frameee = KisiselBilgilerTableVieww.Frame;
            frameee.Height = tableViewSize.Height;
            KisiselBilgilerTableVieww.Frame = frameee;

            var ContentFramee = this.Frame;
            ContentFramee.Height += tableViewSize.Height;
            this.Frame = ContentFramee;
            return (float)this.Frame.Height;
        }

        public class KisiselBilgilerDataModel
        {
            public string Title { get; set; }
            public string Aciklama { get; set; }
        }

        class KisiselBilgilerTableSource : UITableViewSource
        {
            List<KisiselBilgilerDataModel> TableItems;
            KisiselBilgilerCustomView AnaMainListCustomView1;
            public KisiselBilgilerTableSource(List<KisiselBilgilerDataModel> items, KisiselBilgilerCustomView GelenYer)
            {
                TableItems = items;
                AnaMainListCustomView1 = GelenYer;
            }
            public override nint RowsInSection(UITableView tableview, nint section)
            {
                return TableItems.Count;
            }
            public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
            {

                var itemss = TableItems[indexPath.Row];

                var cell = (KisiselBilgilerCustomTableItem)tableView.DequeueReusableCell("KisiselBilgilerCustomTableItem", indexPath) as KisiselBilgilerCustomTableItem;

                return cell;
            }


            public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
            {
                tableView.DeselectRow(indexPath, true);
                //tableView.BeginUpdates();
                //tableView.ReloadRows(new NSIndexPath[] { indexPath }, UITableViewRowAnimation.Fade);
                //tableView.EndUpdates();

                //tableView.ScrollToRow(indexPath, UITableViewScrollPosition.Top, true);
                //var secimm = TableItems[indexPath.Row];
                //tableView.DeselectRow(indexPath, true);
                //AnaMainListCustomView1.RowClickk(secimm);

            }
        }

    }
}