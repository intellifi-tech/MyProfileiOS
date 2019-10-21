using CoreGraphics;
using Foundation;
using System;
using System.Collections.Generic;
using UIKit;

namespace MyProfileiOS
{
    public partial class TakipEttiklerimEtkinliklerViewController : UIViewController
    {
        List<TakipEttigimmKisilerDataModel> KisilerDataModel1 = new List<TakipEttigimmKisilerDataModel>();
        TakipEttiklerimHorizontalKisiView[] TakipEttiklerimHorizontalKisiView1 = new TakipEttiklerimHorizontalKisiView[0];
        List<EtkinliklerDataModel> EtkinliklerDataModel1 = new List<EtkinliklerDataModel>();
        public TakipEttiklerimEtkinliklerViewController (IntPtr handle) : base (handle)
        {
        }
        public TakipEttiklerimEtkinliklerViewController(int index)
        {

        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            this.View.BackgroundColor = UIColor.White;
        }

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);
            GetFollowedUsers();
            GetEvents();
        }

        void GetEvents()
        {
            for (int i = 0; i < 50; i++)
            {
                EtkinliklerDataModel1.Add(new EtkinliklerDataModel());
            }
            //EtkinlikTableView.RegisterClassForCellReuse(typeof(EtkinlikCustomCardView), "EtkinlikCustomCardView");
            EtkinlikTableView.RegisterNibForCellReuse(UINib.FromName("EtkinlikCustomCardView", NSBundle.MainBundle), "EtkinlikCustomCardView");
            EtkinlikTableView.Source = new EtkinliklerListCustomTableSource(EtkinliklerDataModel1, this);
            EtkinlikTableView.Delegate = new EtkinliklerTableDelegate();
            EtkinlikTableView.SeparatorStyle = UITableViewCellSeparatorStyle.None;
            EtkinlikTableView.RowHeight = UITableView.AutomaticDimension;
            EtkinlikTableView.EstimatedRowHeight = 40f;
           
        }

        void GetFollowedUsers()
        {
            for (int i = 0; i < 20; i++)
            {
                KisilerDataModel1.Add(new TakipEttigimmKisilerDataModel());
            }

            TakipEttiklerimHorizontalKisiView1 = new TakipEttiklerimHorizontalKisiView[KisilerDataModel1.Count];
            for (int i = 0; i < KisilerDataModel1.Count; i++)
            {
                var NoktaItem = TakipEttiklerimHorizontalKisiView.Create(KisilerDataModel1[i], this);
                if (i == 0)
                {
                    NoktaItem.Frame = new CGRect(0, 0, 96f, 125f);
                }
                else
                {
                    NoktaItem.Frame = new CGRect(96f * i, 0, 96f, 125f);
                }

                HorizontalTakipKisiScroll.AddSubview(NoktaItem);
                TakipEttiklerimHorizontalKisiView1[i] = NoktaItem;
            }
            HorizontalTakipKisiScroll.ContentSize = new CGSize(TakipEttiklerimHorizontalKisiView1[TakipEttiklerimHorizontalKisiView1.Length - 1].Frame.Right, 125f);
            HorizontalTakipKisiScroll.ShowsHorizontalScrollIndicator = false;
            HorizontalTakipKisiScroll.ShowsVerticalScrollIndicator = false;
            GetRigToLeftScrollView();
        }
        void GetRigToLeftScrollView()
        {
            new System.Threading.Thread(new System.Threading.ThreadStart(delegate
            {
                InvokeOnMainThread(delegate
                {
                    CGPoint pt;
                    pt = HorizontalTakipKisiScroll.Center;
                    HorizontalTakipKisiScroll.Center = new CGPoint(UIScreen.MainScreen.Bounds.Right + HorizontalTakipKisiScroll.Frame.Width / 2, HorizontalTakipKisiScroll.Center.Y);
                    UIView.Animate(1, 0, UIViewAnimationOptions.CurveEaseInOut,
                        () =>
                        {
                            HorizontalTakipKisiScroll.Center = pt;
                            //   new CGPoint(UIScreen.MainScreen.Bounds.Right - Listeislemleributon.Frame.Width / 2, Listeislemleributon.Center.Y);
                        },

                        () =>
                        {
                            HorizontalTakipKisiScroll.Center = pt;
                        }
                    );
                });
            })).Start();
        }
        public class TakipEttigimmKisilerDataModel
        {
            public int ID { get; set; }
        }
        class EtkinliklerListCustomTableSource : UITableViewSource
        {
            List<EtkinliklerDataModel> TableItems;
            TakipEttiklerimEtkinliklerViewController AnaMainListCustomView1;
            public EtkinliklerListCustomTableSource(List<EtkinliklerDataModel> items, TakipEttiklerimEtkinliklerViewController GelenYer)
            {
                TableItems = items;
                AnaMainListCustomView1 = GelenYer;
            }
            public override nint NumberOfSections(UITableView tableView)
            {
                return 1;
            }
            public override nint RowsInSection(UITableView tableview, nint section)
            {
                return TableItems.Count;
            }
            public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
            {

                var itemss = TableItems[indexPath.Row];
                //var cell = tableView.DequeueReusableCell (CellID, indexPath) as GrowRowTableCell;
                var cell = (EtkinlikCustomCardView)tableView.DequeueReusableCell("EtkinlikCustomCardView", indexPath) as EtkinlikCustomCardView;
                //if (cell == null)
                //{
                //    cell = EtkinlikCustomCardView.Create();
                //}
                
                //cell.SelectionStyle = UITableViewCellSelectionStyle.None;
                return cell;
            }

            //public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
            //{
            //    var itemss = TableItems[indexPath.Row];
            //    return 238f;
            //}
            public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
            {
                //if (Secimler[indexPath.Row])
                //{

                //    Secimler[indexPath.Row] = false;
                //}
                //else
                //{

                //    Secimler[indexPath.Row] = true;
                //}

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

        public class EtkinliklerDataModel
        {
            public int ID { get; set; }
        }

        public class EtkinliklerTableDelegate : UITableViewDelegate
        {
            public EtkinliklerTableDelegate()
            {
            }
            public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
            {
                return UITableView.AutomaticDimension;
            }
            public override void RowSelected(UITableView tableView, Foundation.NSIndexPath indexPath)
            {
                // Output selected row
                Console.WriteLine("Row selected: {0}", indexPath.Row);
            }
        }
    }
}
