using FFImageLoading;
using Foundation;
using MyProfileiOS.DataBasee;
using MyProfileiOS.WebServiceHelper;
using System;
using System.Collections.Generic;
using UIKit;
using static MyProfileiOS.TakipEttiklerimEtkinliklerViewController;

namespace MyProfileiOS
{
    public partial class EtkinlikKisiBazliListeVC : UIViewController
    {
        public string UserID;
        public List<UserAttendedEvent> GelenListe;
        public EtkinlikKisiBazliListeVC(IntPtr handle) : base(handle)
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
            EtkinlikTablo.SeparatorStyle = UITableViewCellSeparatorStyle.None;
            GetUserInfo();
            FillTable();
        }

        void FillTable()
        {
            EtkinlikTablo.RegisterNibForCellReuse(UINib.FromName("EtkinlikCustomCardView", NSBundle.MainBundle), "EtkinlikCustomCardView");
            EtkinlikTablo.Source = new EtkinliklerListCustomTableSource(GelenListe, this);
            EtkinlikTablo.Delegate = new EtkinliklerTableDelegate();
            EtkinlikTablo.SeparatorStyle = UITableViewCellSeparatorStyle.None;
            EtkinlikTablo.RowHeight = UITableView.AutomaticDimension;
            EtkinlikTablo.EstimatedRowHeight = 40f;
        }

        USER_INFO HostModelll;
        void GetUserInfo()
        {
            new System.Threading.Thread(new System.Threading.ThreadStart(delegate
            {
                WebService webService = new WebService();
                var Donus = webService.OkuGetir("user/" + UserID + "/show");
                if (Donus != null)
                {
                    InvokeOnMainThread(delegate ()
                    {
                        HostModelll = Newtonsoft.Json.JsonConvert.DeserializeObject<USER_INFO>(Donus.ToString());
                        UserNameLabel.Text = HostModelll.name + " " + HostModelll.surname;
                        UserPhoto.Layer.CornerRadius = UserPhoto.Frame.Height / 2;
                        UserPhoto.ClipsToBounds = true;
                        ImageService.Instance.LoadUrl(HostModelll.profile_photo).Into(UserPhoto);
                    });
                }

            })).Start();
            
        }

        class EtkinliklerListCustomTableSource : UITableViewSource
        {
            List<UserAttendedEvent> TableItems;
            EtkinlikKisiBazliListeVC AnaMainListCustomView1;
            public EtkinliklerListCustomTableSource(List<UserAttendedEvent> items, EtkinlikKisiBazliListeVC GelenYer)
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
                //var cell = (EtkinlikCustomCardView)tableView.DequeueReusableCell("EtkinlikCustomCardView", indexPath) as EtkinlikCustomCardView;
                var cell = (EtkinlikCustomCardView)tableView.DequeueReusableCell(EtkinlikCustomCardView.Key);
                if (cell == null)
                {
                    cell = EtkinlikCustomCardView.Create();
                }
                cell.UpdateCell(itemss, AnaMainListCustomView1);
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