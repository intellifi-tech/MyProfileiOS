using Foundation;
using MyProfileiOS.Etkinlikler.EtkinlikDetay;
using System;
using System.Collections.Generic;
using UIKit;
using static MyProfileiOS.EtkinlikDetayBaseVC;

namespace MyProfileiOS
{
    public partial class EtkinlikKatilimcilariVC : UIViewController
    {
        public List<User> KatilimciLis;
        public EtkinlikKatilimcilariVC (IntPtr handle) : base (handle)
        {
        }
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            GeriButton.TouchUpInside += GeriButton_TouchUpInside;
        }

        private void GeriButton_TouchUpInside(object sender, EventArgs e)
        {
            this.DismissViewController(true, null);
        }

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);
        }
        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            KatilimcilariGetir();
        }

        void KatilimcilariGetir()
        {
            Tabloo.SeparatorStyle = UITableViewCellSeparatorStyle.None;
            Tabloo.Source = new FriendListCustomTableSource(KatilimciLis, this);
            Tabloo.ReloadData();
        }

        public void RowSelectt(User SecilenKullanici)
        {

        }


        class FriendListCustomTableSource : UITableViewSource
        {
            List<User> TableItems;
            EtkinlikKatilimcilariVC AnaMainListCustomView1;
            string MeID;
            public FriendListCustomTableSource(List<User> items, EtkinlikKatilimcilariVC GelenYer)
            {
                TableItems = items;
                AnaMainListCustomView1 = GelenYer;
            }
            public override nint RowsInSection(UITableView tableview, nint section)
            {
                return TableItems.Count;
            }
            public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
            {
                return 84f;
            }
            public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
            {

                var itemss = TableItems[indexPath.Row];

                //var cell = (MesajlarKisiListItem)tableView.DequeueReusableCell("MesajlarKisiListItem", indexPath) as MesajlarKisiListItem;
                var cell = (KatilimciCustomItem)tableView.DequeueReusableCell(KatilimciCustomItem.Key);
                if (cell == null)
                {
                    cell = KatilimciCustomItem.Create();
                }
                cell.UpdateCell(itemss);
                return cell;
            }


            public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
            {
                tableView.DeselectRow(indexPath, true);
                AnaMainListCustomView1.RowSelectt(TableItems[indexPath.Row]);

            }
        }
    }
}