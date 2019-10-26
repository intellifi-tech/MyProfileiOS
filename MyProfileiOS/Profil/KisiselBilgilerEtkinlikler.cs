using Foundation;
using MyProfileiOS.DataBasee;
using MyProfileiOS.WebServiceHelper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using UIKit;
using static MyProfileiOS.TakipEttiklerimEtkinliklerViewController;

namespace MyProfileiOS
{
    public partial class KisiselBilgilerEtkinlikler : UIViewController
    {
        List<UserAttendedEvent> FavorilerRecyclerViewDataModel1;
        
        public KisiselBilgilerEtkinlikler (IntPtr handle) : base (handle)
        {
        }
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
        }
        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);
        }
        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            EtkinliklerTablo.SeparatorStyle = UITableViewCellSeparatorStyle.None;
            EtkinliklerTablo.ScrollEnabled = false;
            FillDataModel();
        }

        void FillDataModel()
        {
            new System.Threading.Thread(new System.Threading.ThreadStart(delegate
            {
                FavorilerRecyclerViewDataModel1 = new List<UserAttendedEvent>();
                WebService webService = new WebService();
                var Donus = webService.OkuGetir("user/" + SecilenKullanici.UserID+ "/events");
                if (Donus != null)
                {
                    InvokeOnMainThread(delegate ()
                    {
                        var Modell = Newtonsoft.Json.JsonConvert.DeserializeObject<ProfilEtkinliklerRecyclerViewDataModel>(Donus.ToString());
                        FavorilerRecyclerViewDataModel1 = Modell.userAttendedEvents;
                        FavorilerRecyclerViewDataModel1.Reverse();
                        SetUserInformationToList();
                        EtkinliklerTablo.RegisterNibForCellReuse(UINib.FromName("EtkinlikCustomCardView", NSBundle.MainBundle), "EtkinlikCustomCardView");
                        EtkinliklerTablo.Source = new EtkinliklerListCustomTableSource(FavorilerRecyclerViewDataModel1, this);
                        EtkinliklerTablo.Delegate = new EtkinliklerTableDelegate();
                        EtkinliklerTablo.SeparatorStyle = UITableViewCellSeparatorStyle.None;
                        EtkinliklerTablo.RowHeight = UITableView.AutomaticDimension;
                        EtkinliklerTablo.EstimatedRowHeight = 40f;
                        EtkinliklerTablo.ReloadData();
                        YukseklikCagrisiYap();
                    });
                }
            })).Start();
        }
        void YukseklikCagrisiYap()
        {
            EtkinliklerTablo.LayoutIfNeeded();
            SonYuksekligiAyarla.YukseklikUygula(EtkinliklerTablo.ContentSize.Height);
        }
        void SetUserInformationToList()
        {
            WebService webService = new WebService();
            var Donus = webService.OkuGetir("user/" + SecilenKullanici.UserID + "/show");
            if (Donus != null)
            {
               var Userr = Newtonsoft.Json.JsonConvert.DeserializeObject<Following>(Donus.ToString());
                FavorilerRecyclerViewDataModel1.ForEach(x => x.UserInfo = Userr);
            }
        }

        public class Event
        {
            public int id { get; set; }
            public string title { get; set; }
            public double latitude { get; set; }
            public double longitude { get; set; }
            public string created_at { get; set; }
            public string updated_at { get; set; }
        }

        public class ProfilEtkinliklerRecyclerViewDataModel
        {
            public int status { get; set; }
            public string message { get; set; }
            public List<UserAttendedEvent> userAttendedEvents { get; set; }
        }


        class EtkinliklerListCustomTableSource : UITableViewSource
        {
            List<UserAttendedEvent> TableItems;
            KisiselBilgilerEtkinlikler AnaMainListCustomView1;
            public EtkinliklerListCustomTableSource(List<UserAttendedEvent> items, KisiselBilgilerEtkinlikler GelenYer)
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
                cell.UpdateCell(itemss);
                return cell;
            }


            public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
            {
                tableView.DeselectRow(indexPath, true);
                tableView.DeselectRow(indexPath, true);
                //tableView.BeginUpdates();
                //tableView.ReloadRows(new NSIndexPath[] { indexPath }, UITableViewRowAnimation.Fade);
                //tableView.EndUpdates();

                //tableView.ScrollToRow(indexPath, UITableViewScrollPosition.Top, true);
                //var secimm = TableItems[indexPath.Row];

                //AnaMainListCustomView1.RowClickk(secimm);

            }
        }

        class EtkinliklerTableDelegate : UITableViewDelegate
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