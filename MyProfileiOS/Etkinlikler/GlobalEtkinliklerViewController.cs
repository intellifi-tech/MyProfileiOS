using Foundation;
using MyProfileiOS.WebServiceHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using UIKit;
using static MyProfileiOS.TakipEttiklerimEtkinliklerViewController;

namespace MyProfileiOS
{
    public partial class GlobalEtkinliklerViewController : UIViewController
    {
        List<GlobalRecyclerViewDataModel> globalRecyclerViewDataModels = new List<GlobalRecyclerViewDataModel>();
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
            globalRecyclerViewDataModels = new List<GlobalRecyclerViewDataModel>();
            WebService webService = new WebService();
            var Donus = webService.OkuGetir("event/index");
            if (Donus != null)
            {
                var a = Donus.ToString();
                var DonusModel = Newtonsoft.Json.JsonConvert.DeserializeObject<GlobalRecyclerViewDataModel_For_JSON>(Donus.ToString());
                if (DonusModel.status == 200)
                {
                    var FavorilerRecyclerViewDataModel1 = DonusModel.events;
                    if (FavorilerRecyclerViewDataModel1.Count > 0)
                    {

                        for (int i = 0; i < DonusModel.events.Count; i++)
                        {
                            for (int i2 = 0; i2 < DonusModel.events[i].user_attended_event.Count; i2++)
                            {
                                globalRecyclerViewDataModels.Add(new GlobalRecyclerViewDataModel()
                                {
                                    Events = DonusModel.events[i],
                                    user_attended_event = DonusModel.events[i].user_attended_event[i2]
                                });
                            }
                        }

                        var aa = globalRecyclerViewDataModels;
                        globalRecyclerViewDataModels = globalRecyclerViewDataModels.OrderBy(x => x.user_attended_event.created_at).ToList();
                        globalRecyclerViewDataModels.Reverse();

                        GlobalTableView.RegisterNibForCellReuse(UINib.FromName("EtkinlikCustomCardView", NSBundle.MainBundle), "EtkinlikCustomCardView");
                        GlobalTableView.Source = new EtkinliklerListCustomTableSource(globalRecyclerViewDataModels, this);
                        GlobalTableView.Delegate = new EtkinliklerTableDelegate();
                        GlobalTableView.SeparatorStyle = UITableViewCellSeparatorStyle.None;
                        GlobalTableView.RowHeight = UITableView.AutomaticDimension;
                        GlobalTableView.EstimatedRowHeight = 40f;
                    }
                }
            }
        }


        #region DataModels

        public class Event
        {
            public int id { get; set; }
            public string title { get; set; }
            public double latitude { get; set; }
            public double longitude { get; set; }
            public string created_at { get; set; }
            public string updated_at { get; set; }
            public List<UserAttendedEvent> user_attended_event { get; set; }
        }

        public class GlobalRecyclerViewDataModel_For_JSON
        {
            public int status { get; set; }
            public string message { get; set; }
            public List<Event> events { get; set; }
        }


        //CUSTOM MODEL
        public class GlobalRecyclerViewDataModel
        {
            public UserAttendedEvent user_attended_event { get; set; }
            public Event Events { get; set; }
        }
        #endregion


        class EtkinliklerListCustomTableSource : UITableViewSource
        {
            List<GlobalRecyclerViewDataModel> TableItems;
            GlobalEtkinliklerViewController AnaMainListCustomView1;
            public EtkinliklerListCustomTableSource(List<GlobalRecyclerViewDataModel> items, GlobalEtkinliklerViewController GelenYer)
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
                
                //var cell = (EtkinlikCustomCardView)tableView.DequeueReusableCell("EtkinlikCustomCardView", indexPath) as EtkinlikCustomCardView;
                var cell = (EtkinlikCustomCardView)tableView.DequeueReusableCell(EtkinlikCustomCardView.Key);
                if (cell == null)
                {
                    cell = EtkinlikCustomCardView.Create();
                }
                cell.UpdateCell(itemss.user_attended_event, AnaMainListCustomView1);
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