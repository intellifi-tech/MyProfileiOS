using Foundation;
using System;
using System.Collections.Generic;
using UIKit;

namespace MyProfileiOS
{
    public partial class MesajlarViewController : UIViewController
    {
        List<MesajArkadaslarDataModel> EtkinliklerDataModel1 = new List<MesajArkadaslarDataModel>();
        public MesajlarViewController (IntPtr handle) : base (handle)
        {
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            MesajFriendsTableView.SeparatorStyle = UITableViewCellSeparatorStyle.None;
        }
        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);
            ProfilFoto.Layer.CornerRadius = ProfilFoto.Frame.Height / 2;
            ProfilFoto.ClipsToBounds = true;

            GetMessageFriend();
        }

        void GetMessageFriend()
        {
            for (int i = 0; i < 50; i++)
            {
                EtkinliklerDataModel1.Add(new MesajArkadaslarDataModel());
            }
            //EtkinlikTableView.RegisterClassForCellReuse(typeof(EtkinlikCustomCardView), "EtkinlikCustomCardView");
            MesajFriendsTableView.RegisterNibForCellReuse(UINib.FromName("MesajlarKisiListItem", NSBundle.MainBundle), "MesajlarKisiListItem");
            MesajFriendsTableView.Source = new MesajlarFriendListCustomTableSource(EtkinliklerDataModel1, this);
            MesajFriendsTableView.SeparatorStyle = UITableViewCellSeparatorStyle.None;
            MesajFriendsTableView.RowHeight = 84f;
            MesajFriendsTableView.ReloadData();
        }

        class MesajlarFriendListCustomTableSource : UITableViewSource
        {
            List<MesajArkadaslarDataModel> TableItems;
            MesajlarViewController AnaMainListCustomView1;
            public MesajlarFriendListCustomTableSource(List<MesajArkadaslarDataModel> items, MesajlarViewController GelenYer)
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

                var cell = (MesajlarKisiListItem)tableView.DequeueReusableCell("MesajlarKisiListItem", indexPath) as MesajlarKisiListItem;

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

        public class MesajArkadaslarDataModel
        {
            public int ID { get; set; }
        }
    }
}