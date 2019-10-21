using Foundation;
using System;
using System.Collections.Generic;
using UIKit;

namespace MyProfileiOS
{
    public partial class GlobalEtkinliklerViewController : UIViewController
    {
        List<EtkinliklerDataModel> EtkinliklerDataModel1 = new List<EtkinliklerDataModel>();
        public GlobalEtkinliklerViewController (IntPtr handle) : base (handle)
        {
        }
        public GlobalEtkinliklerViewController(int index)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            this.View.BackgroundColor = UIColor.Red;

        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            GlobalTableView.SeparatorStyle = UITableViewCellSeparatorStyle.None;

        }
        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);
            GetEvents();
        }

        void GetEvents()
        {
            for (int i = 0; i < 50; i++)
            {
                EtkinliklerDataModel1.Add(new EtkinliklerDataModel());
            }
            //EtkinlikTableView.RegisterClassForCellReuse(typeof(EtkinlikCustomCardView), "EtkinlikCustomCardView");
            GlobalTableView.RegisterNibForCellReuse(UINib.FromName("EtkinlikCustomCardView", NSBundle.MainBundle), "EtkinlikCustomCardView");
            GlobalTableView.Source = new EtkinliklerListCustomTableSource(EtkinliklerDataModel1, this);
            GlobalTableView.Delegate = new EtkinliklerTableDelegate();
            GlobalTableView.SeparatorStyle = UITableViewCellSeparatorStyle.None;
            GlobalTableView.RowHeight = UITableView.AutomaticDimension;
            GlobalTableView.EstimatedRowHeight = 40f;

        }


        class EtkinliklerListCustomTableSource : UITableViewSource
        {
            List<EtkinliklerDataModel> TableItems;
            GlobalEtkinliklerViewController AnaMainListCustomView1;
            public EtkinliklerListCustomTableSource(List<EtkinliklerDataModel> items, GlobalEtkinliklerViewController GelenYer)
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
                
                var cell = (EtkinlikCustomCardView)tableView.DequeueReusableCell("EtkinlikCustomCardView", indexPath) as EtkinlikCustomCardView;
            
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