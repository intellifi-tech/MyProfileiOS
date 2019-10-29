using CoreGraphics;
using Foundation;
using MyProfileiOS.DataBasee;
using MyProfileiOS.WebServiceHelper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UIKit;

namespace MyProfileiOS
{
    public partial class TakipEttiklerimEtkinliklerViewController : UIViewController
    {
        List<Following> TakipEttiklerimDataModel1 = new List<Following>();
        TakipEttiklerimHorizontalKisiView[] TakipEttiklerimHorizontalKisiView1 = new TakipEttiklerimHorizontalKisiView[0];
        List<UserAttendedEvent> EtkinliklerDataModel1 = new List<UserAttendedEvent>();
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
            BekletveTakipleriGeitr();
            GetEvents();
        }

        void BekletveTakipleriGeitr()
        {
            new System.Threading.Thread(new System.Threading.ThreadStart(async delegate
            {
                await Task.Run(() =>
                {
                    //await Task.Delay(500);
                    InvokeOnMainThread(() =>
                    {
                        try
                        {
                            GetFollowedUsers();
                        }
                        catch
                        {
                        }

                    });
                });
            })).Start();
        }

        void GetEvents()
        {
            WebService webService = new WebService();
            var Donus = webService.ServisIslem("user/followings", "");
            if (Donus != "Hata")
            {
                try
                {
                    var DonusModel = Newtonsoft.Json.JsonConvert.DeserializeObject<List<TakipEttiklerimDataModel>>(Donus);
                    TakipEttiklerimDataModel1 = DonusModel[0].followings;
                    if (TakipEttiklerimDataModel1.Count > 0)
                    {
                        for (int i = 0; i < TakipEttiklerimDataModel1.Count; i++)
                        {
                            FillDataModel(TakipEttiklerimDataModel1[i]);
                        }

                        if (EtkinliklerDataModel1.Count > 0)
                        {
                            //EtkinliklerDataModel1.ForEach(item => item.event_description = " auýsdas dkasbdkasvd kas dkjavsdk avskdj vaskdv aksdvaskvd aksvd kahsgvd hkagsdhkgas vdkasvdkjasvd kasvd asvd kasvd kavd avdkgasvd kavd kgasvdk vaskdv askjdv aksjvd aksvd kahgsvd ahgsvdahgsvdas ghc");
                            //EtkinlikTableView.RegisterClassForCellReuse(typeof(EtkinlikCustomCardView), "EtkinlikCustomCardView");
                            EtkinlikTableView.RegisterNibForCellReuse(UINib.FromName("EtkinlikCustomCardView", NSBundle.MainBundle), "EtkinlikCustomCardView");
                            EtkinlikTableView.Source = new EtkinliklerListCustomTableSource(EtkinliklerDataModel1, this);
                            //EtkinlikTableView.Delegate = new EtkinliklerTableDelegate();
                            EtkinlikTableView.SeparatorStyle = UITableViewCellSeparatorStyle.None;
                            EtkinlikTableView.RowHeight = UITableView.AutomaticDimension;
                            EtkinlikTableView.EstimatedRowHeight = 40f;
                        }

                    }
                }
                catch
                {
                }
            }
        }

        void FillDataModel(Following UserInfoo)
        {
            EtkinliklerDataModel1 = new List<UserAttendedEvent>();

            WebService webService = new WebService();
            var Donus = webService.OkuGetir("user/" + UserInfoo.id.ToString() + "/events");
            if (Donus != null)
            {
                var DonusModel = Newtonsoft.Json.JsonConvert.DeserializeObject<TakipcilerRecyclerViewDataModel>(Donus);
                if (DonusModel.status == 200)
                {
                    var Modell = DonusModel.userAttendedEvents;
                    if (Modell.Count > 0)
                    {
                        Modell.ForEach(x => x.UserInfo = UserInfoo);
                        EtkinliklerDataModel1.AddRange(Modell);
                    }
                }
            }
        }

        void GetFollowedUsers()
        {
            TakipEttiklerimDataModel1 = new List<Following>();
            WebService webService = new WebService();
            var Donus = webService.ServisIslem("user/followings", "");
            if (Donus != "Hata")
            {
                try
                {
                    var DonusModel = Newtonsoft.Json.JsonConvert.DeserializeObject<List<TakipEttiklerimDataModel>>(Donus);
                    TakipEttiklerimDataModel1 = DonusModel[0].followings;
                    if (TakipEttiklerimDataModel1.Count > 0)
                    {
                        TakipEttiklerimHorizontalKisiView1 = new TakipEttiklerimHorizontalKisiView[TakipEttiklerimDataModel1.Count];
                        for (int i = 0; i < TakipEttiklerimDataModel1.Count; i++)
                        {
                            var NoktaItem = TakipEttiklerimHorizontalKisiView.Create(TakipEttiklerimDataModel1[i], this);
                            if (i == 0)
                            {
                                NoktaItem.Frame = new CGRect(0, 0, 96f, 125f);
                            }
                            else
                            {
                                NoktaItem.Frame = new CGRect(96f * i, 0, 96f, 125f);
                            }
                            var Buttonn = new UIButton();
                            Buttonn.SetTitle(TakipEttiklerimDataModel1[i].id.ToString(), UIControlState.Normal);
                            Buttonn.Frame = NoktaItem.Bounds;
                            Buttonn.SetTitleColor(UIColor.Clear, UIControlState.Normal);
                            Buttonn.TouchUpInside += Buttonn_TouchUpInside;
                            NoktaItem.AddSubview(Buttonn);
                            HorizontalTakipKisiScroll.AddSubview(NoktaItem);
                            TakipEttiklerimHorizontalKisiView1[i] = NoktaItem;
                        }
                        HorizontalTakipKisiScroll.ContentSize = new CGSize(TakipEttiklerimHorizontalKisiView1[TakipEttiklerimHorizontalKisiView1.Length - 1].Frame.Right, 125f);
                        HorizontalTakipKisiScroll.ShowsHorizontalScrollIndicator = false;
                        HorizontalTakipKisiScroll.ShowsVerticalScrollIndicator = false;
                        GetRigToLeftScrollView();
                    }
                    else
                    {
                        //GelenBase.TakipEdilenKimseYok();
                    }
                }
                catch
                {
                }

            }
            else
            {
                //GelenBase.TakipEdilenKimseYok();
            }
          
           
        }

        private void Buttonn_TouchUpInside(object sender, EventArgs e)
        {
            var UserId = ((UIButton)sender).Title(UIControlState.Normal);
            var LokasyonKisilerStory = UIStoryboard.FromName("Main", NSBundle.MainBundle);
            EtkinlikKisiBazliListeVC controller = LokasyonKisilerStory.InstantiateViewController("EtkinlikKisiBazliListeVC") as EtkinlikKisiBazliListeVC;
            controller.ModalPresentationStyle = UIModalPresentationStyle.FullScreen;
            controller.UserID = UserId;
            var KisininEventleri = EtkinliklerDataModel1.FindAll(item => item.user_id.ToString() == UserId);
            if (KisininEventleri.Count > 0)
            {
                controller.GelenListe = KisininEventleri;
                this.PresentViewController(controller, true, null);
            }
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

        public void RowClick(UserAttendedEvent TiklananModel)
        {
            var LokasyonKisilerStory = UIStoryboard.FromName("Main", NSBundle.MainBundle);
            EtkinlikDetayBaseVC controller = LokasyonKisilerStory.InstantiateViewController("EtkinlikDetayBaseVC") as EtkinlikDetayBaseVC;
            controller.ModalPresentationStyle = UIModalPresentationStyle.FullScreen;
            controller.GelenModel1 = TiklananModel;
            this.PresentViewController(controller, true, null);
        }

        public class Following
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
            public int status { get; set; }
            public string package { get; set; }
            public string created_at { get; set; }
            public string updated_at { get; set; }
        }

        public class TakipEttiklerimDataModel
        {
            public int id { get; set; }
            public int from_user_id { get; set; }
            public int to_user_id { get; set; }
            public string created_at { get; set; }
            public string updated_at { get; set; }
            public List<Following> followings { get; set; }
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
            [JsonProperty("event")]
            public Event Event { get; set; }
            public Following UserInfo { get; set; }
            //Custom Propert 
            public Following user { get; set; }//For Global List
        }

        public class TakipcilerRecyclerViewDataModel
        {
            public int status { get; set; }
            public string message { get; set; }
            public List<UserAttendedEvent> userAttendedEvents { get; set; }
        }



        class EtkinliklerListCustomTableSource : UITableViewSource
        {
            List<UserAttendedEvent> TableItems;
            TakipEttiklerimEtkinliklerViewController AnaMainListCustomView1;
            public EtkinliklerListCustomTableSource(List<UserAttendedEvent> items, TakipEttiklerimEtkinliklerViewController GelenYer)
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
                tableView.DeselectRow(indexPath, true);
                AnaMainListCustomView1.RowClick(TableItems[indexPath.Row]);
            }

            //public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
            //{


            //    tableView.DeselectRow(indexPath, true);
            //    AnaMainListCustomView1.RowClick(TableItems[indexPath.Row]);

            //}
        }

        public class EtkinliklerTableDelegate : UITableViewDelegate
        {
            TakipEttiklerimEtkinliklerViewController AnaMainListCustomView1;
            public EtkinliklerTableDelegate()
            {
            }
            public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
            {
                return UITableView.AutomaticDimension;
            }
            public override void RowSelected(UITableView tableView, Foundation.NSIndexPath indexPath)
            {
                tableView.DeselectRow(indexPath, true);
                //AnaMainListCustomView1.RowClick(TableItems[indexPath.Row]);
                Console.WriteLine("Row selected: {0}", indexPath.Row);
            }
        }
    }
}
