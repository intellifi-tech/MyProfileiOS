using Foundation;
using MyProfileiOS.Etkinlikler.EtkinlikDetay;
using MyProfileiOS.WebServiceHelper;
using System;
using System.Collections.Generic;
using UIKit;

namespace MyProfileiOS
{
    public partial class EtkinlikDetayBaseVC : UIViewController
    {
        public TakipEttiklerimEtkinliklerViewController.UserAttendedEvent GelenModel1;
        EtkinlikDetayi_RootObject EtkinlikDetayi_RootObject1;
        UILabel YorumlarTitleLabel;
        UITableView YorumlarTablosu;
        public EtkinlikDetayBaseVC (IntPtr handle) : base (handle)
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
            EtkinlikDetayViewGetir();
        }
        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
        }

        void EtkinlikDetayViewGetir()
        {
            var DetayCard = EtkinlikCustomCardView.Create();
            DetayCard.Frame = new CoreGraphics.CGRect(0, 0, UIScreen.MainScreen.Bounds.Width, 260f);
            DetayCard.UpdateCell(GelenModel1);
            var yukseklik = DetayCard.LabelYusekliginiGetir();
            DetayCard.Frame = new CoreGraphics.CGRect(0, 0, UIScreen.MainScreen.Bounds.Width, yukseklik);
            ScrollVieww.AddSubview(DetayCard);
            YorumlarTitleLabel = new UILabel();
            YorumlarTitleLabel.Frame = new CoreGraphics.CGRect(10, DetayCard.Frame.Bottom + 20f,UIScreen.MainScreen.Bounds.Width,30f);
            YorumlarTitleLabel.TextColor = UIColor.Black;
            ScrollVieww.AddSubview(YorumlarTitleLabel);
            YorumlariGetir();
        }
        void YorumlariGetir()
        {
            WebService webService = new WebService();
            var Donus = webService.OkuGetir("event/" + GelenModel1.event_id + "/show");
            if (Donus != null)
            {
                 EtkinlikDetayi_RootObject1 = Newtonsoft.Json.JsonConvert.DeserializeObject<EtkinlikDetayi_RootObject>(Donus);
                if (EtkinlikDetayi_RootObject1.user_attended_event[0].comments != null)
                {
                    if (EtkinlikDetayi_RootObject1.user_attended_event[0].comments.Count > 0)
                    {
                        YorumlarTitleLabel.Text = "Yorumlar (" + EtkinlikDetayi_RootObject1.user_attended_event[0].comments.Count.ToString() + ")";
                        YorumlariYansit();
                    }
                    else
                    {
                        YorumlarTitleLabel.Text = "Ýlk yorumu sen yap.";
                    }
                }
            }
        }

        void YorumlariYansit()
        {
            YorumlarTablosu = new UITableView();
            YorumlarTablosu.RegisterNibForCellReuse(UINib.FromName("CommentCell", NSBundle.MainBundle), "CommentCell");
            YorumlarTablosu.SeparatorStyle = UITableViewCellSeparatorStyle.None;
            YorumlarTablosu.RowHeight = UITableView.AutomaticDimension;
            YorumlarTablosu.EstimatedRowHeight = 40f;
            YorumlarTablosu.Source = new EtkinliklerListCustomTableSource(EtkinlikDetayi_RootObject1.user_attended_event[0].comments, this);
            YorumlarTablosu.ReloadData();
           
            YorumlarTablosu.LayoutIfNeeded();
            var Sizee = YorumlarTablosu.ContentSize.Height;
            var fmrr = new CoreGraphics.CGRect(0, YorumlarTitleLabel.Frame.Bottom + 10f, UIScreen.MainScreen.Bounds.Width, Sizee);
            YorumlarTablosu.Frame = fmrr;
            ScrollVieww.AddSubview(YorumlarTablosu);
            ScrollVieww.ContentSize = new CoreGraphics.CGSize(UIScreen.MainScreen.Bounds.Width, YorumlarTablosu.Frame.Bottom);
            YorumlarTablosu.BackgroundColor = UIColor.Red;
        }
        public void RowClick(Comment TiklananYorum)
        {

        }

        #region DataModels
        public class Comment
        {
            public int id { get; set; }
            public int user_id { get; set; }
            public int attended_id { get; set; }
            public string comment { get; set; }
            public string created_at { get; set; }
            public string updated_at { get; set; }
        }
        public class User
        {
            public int id { get; set; }
            public int type { get; set; }
            public string profile_photo { get; set; }
            public string cover_photo { get; set; }
            public string title { get; set; }
            public string name { get; set; }
            public string surname { get; set; }
            public string career_history { get; set; }
            public string short_biography { get; set; }
            public string credentials { get; set; }
            public string date_of_birth { get; set; }
            public string company_id { get; set; }
            public string sector_id { get; set; }
            public string email { get; set; }
            public string status { get; set; }
            public string package { get; set; }
            public string created_at { get; set; }
            public string updated_at { get; set; }
        }
        public class UserAttendedEvent
        {
            public int id { get; set; }
            public int event_id { get; set; }
            public int user_id { get; set; }
            public string event_description { get; set; }
            public string event_image { get; set; }
            public string date_of_participation { get; set; }
            public string end_date { get; set; }
            public int rating { get; set; }
            public string created_at { get; set; }
            public string updated_at { get; set; }
            public User user { get; set; }
            public List<Comment> comments { get; set; }
        }
        public class EtkinlikDetayi_RootObject
        {
            public int id { get; set; }
            public string title { get; set; }
            public double latitude { get; set; }
            public double longitude { get; set; }
            public string created_at { get; set; }
            public string updated_at { get; set; }
            public List<UserAttendedEvent> user_attended_event { get; set; }
        }
        #endregion


        class EtkinliklerListCustomTableSource : UITableViewSource
        {
            List<Comment> TableItems;
            EtkinlikDetayBaseVC AnaMainListCustomView1;
            public EtkinliklerListCustomTableSource(List<Comment> items, EtkinlikDetayBaseVC GelenYer)
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
                var cell = (CommentCell)tableView.DequeueReusableCell(CommentCell.Key);
                if (cell == null)
                {
                    cell = CommentCell.Create();
                }
                //cell.UpdateCell(itemss);
                return cell;
            }

            public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
            {
                tableView.DeselectRow(indexPath, true);
                AnaMainListCustomView1.RowClick(TableItems[indexPath.Row]);
            }
        }
    }
}