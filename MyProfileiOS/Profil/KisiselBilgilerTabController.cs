using CoreGraphics;
using Foundation;
using MyProfileiOS.DataBasee;
using MyProfileiOS.Profil;
using MyProfileiOS.WebServiceHelper;
using ObjCRuntime;
using System;
using System.Collections.Generic;
using UIKit;

namespace MyProfileiOS
{
    public partial class KisiselBilgilerTabController : UIViewController
    {
        List<KisiselBilgilerDataModel> KisiselBilgilerDataModel1 = new List<KisiselBilgilerDataModel>();
        List<UserExperience> UserExperience1 = new List<UserExperience>();
        public KisiselBilgilerTabController (IntPtr handle) : base (handle)
        {
        }
        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            KisiselBilgilerTableVieww.SeparatorStyle = UITableViewCellSeparatorStyle.None;
            KisiselBilgilerTableVieww.ScrollEnabled = false;
        }
        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);
            GetHeight();
        }

        void KisiselBilgileriGetir()
        {
            new System.Threading.Thread(new System.Threading.ThreadStart(delegate
            {
                var UserInfoo = KullaniciBilgileriniGetir(SecilenKullanici.UserID);
                if (UserInfoo != null)
                {
                    KisiselBilgilerDataModel1.Add(new KisiselBilgilerDataModel()
                    {
                        Title = "Hakkýnda",
                        Aciklama = UserInfoo.short_biography,
                        IconPath = "Images/user.png"
                    });
                    if (!String.IsNullOrEmpty(UserInfoo.date_of_birth))
                    {
                        try
                        {
                            KisiselBilgilerDataModel1.Add(new KisiselBilgilerDataModel()
                            {
                                Title = "Doðum Tarihi",
                                Aciklama = Convert.ToDateTime(UserInfoo.date_of_birth).ToShortDateString(),
                                IconPath = "Images/calender.png"
                            });
                        }
                        catch { }
                    }
                    GetCompanyID(UserInfoo.company_id);
                    GetSektor(UserInfoo.sector_id);
                    InvokeOnMainThread(delegate ()
                    {
                        KisiselBilgilerTableVieww.RegisterNibForCellReuse(UINib.FromName("KisiselBilgilerCustomTableItem", NSBundle.MainBundle), "KisiselBilgilerCustomTableItem");
                        KisiselBilgilerTableVieww.Source = new KisiselBilgilerTableSource(KisiselBilgilerDataModel1, this);
                        KisiselBilgilerTableVieww.SeparatorStyle = UITableViewCellSeparatorStyle.None;
                        KisiselBilgilerTableVieww.RowHeight = 84f;
                        KariyerGecmisiniYansit();
                        KisiselBilgilerTableVieww.ReloadData();
                        YukseklikCagrisiYap();

                    });
                }
            })).Start();
        }

        void YukseklikCagrisiYap()
        {
            KisiselBilgilerTableVieww.LayoutIfNeeded();
            SonYuksekligiAyarla.YukseklikUygula(KisiselBilgilerTableVieww.ContentSize.Height);
        }
        USER_INFO KullaniciBilgileriniGetir(string UserID)
        {
            WebService webService = new WebService();
            var Donus = webService.OkuGetir("user/" + UserID + "/show");
            if (Donus != null)
            {
                return Newtonsoft.Json.JsonConvert.DeserializeObject<USER_INFO>(Donus);
            }
            else
            {
                return null;
            }
        }
        void GetCompanyID(string ID)
        {
          
           try
           {
               WebService webService = new WebService();
               var Donus = webService.OkuGetir("company/" + ID.ToString() + "/show");
               if (Donus != null)
               {
                   var Modell = Newtonsoft.Json.JsonConvert.DeserializeObject<SirketModel>(Donus.ToString());
                    KisiselBilgilerDataModel1.Add(new KisiselBilgilerDataModel()
                    {
                        Title = "Þirket",
                        Aciklama = Modell.name,
                        IconPath = "Images/home.png"
                    });
                }

           }
           catch
           {
           }
        }
        void GetSektor(string ID)
        {
           try
           {
               WebService webService = new WebService();
               var Donus = webService.OkuGetir("sector/" + ID.ToString() + "/show");
                if (Donus != null)
                {
                    var Modell = Newtonsoft.Json.JsonConvert.DeserializeObject<SektorModel>(Donus.ToString());
                    KisiselBilgilerDataModel1.Add(new KisiselBilgilerDataModel()
                    {
                        Title = "Þirket",
                        Aciklama = Modell.name,
                        IconPath = "Images/award.png"
                    });
                }
          
           }
           catch
           {
           }
        }

        List<DeneyimCustomTableCell> DeneyimViews = new List<DeneyimCustomTableCell>();
        void KariyerGecmisiniYansit()
        {
            WebService webService = new WebService();
            var Donus = webService.OkuGetir("user/" + SecilenKullanici.UserID + "/userExperiences");
            if (Donus != null)
            {
                var Modell = Newtonsoft.Json.JsonConvert.DeserializeObject<KariyerGecmisi_RootObject>(Donus);
                if (Modell.status == 200)
                {
                    UserExperience1 = Modell.userExperiences;
                    UserExperience1.Reverse();
                    var TestFooterView = new UIView();
                    TestFooterView.BackgroundColor = UIColor.Clear;
                   
                    var TitleLabell = new UILabel();
                    TitleLabell.Text = "Deneyimler";
                    TitleLabell.TextColor = UIColor.Black;
                    TitleLabell.Frame = new CGRect(12f, 20f, UIScreen.MainScreen.Bounds.Width, 25);
                    TestFooterView.AddSubview(TitleLabell);
                    for (int i = 0; i < UserExperience1.Count; i++)
                    {
                        var DeneyimCell = DeneyimCustomTableCell.Create();
                        DeneyimCell.UpdateCell(UserExperience1[i]);
                        if (i == 0)
                        {
                            DeneyimCell.Frame = new CGRect(0, 40, UIScreen.MainScreen.Bounds.Width, 94f);
                        }
                        else
                        {
                            DeneyimCell.Frame = new CGRect(0, DeneyimViews[i-1].Frame.Bottom, UIScreen.MainScreen.Bounds.Width, 94f);
                        }

                        DeneyimViews.Add(DeneyimCell);
                        TestFooterView.AddSubview(DeneyimCell);
                    }
                    TestFooterView.Frame = new CGRect(0, 0, UIScreen.MainScreen.Bounds.Width, DeneyimViews[DeneyimViews.Count-1].Frame.Bottom);
                    KisiselBilgilerTableVieww.TableFooterView = TestFooterView;
                }

            }
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

            var ContentFramee = this.View.Frame;
            ContentFramee.Height += tableViewSize.Height;
            this.View.Frame = ContentFramee;
            return (float)this.View.Frame.Height;
        }
        public class SirketModel
        {
            public int id { get; set; }
            public string name { get; set; }
        }
        public class SektorModel
        {
            public int id { get; set; }
            public string name { get; set; }
        }

        public class Company
        {
            public int id { get; set; }
            public string name { get; set; }
            public string created_at { get; set; }
            public string updated_at { get; set; }
        }



        public class UserExperience
        {
            public int id { get; set; }
            public int user_id { get; set; }
            public string title { get; set; }
            public int company_id { get; set; }
            public string start_time { get; set; }
            public string end_time { get; set; }
            public string description { get; set; }
            public string created_at { get; set; }
            public string updated_at { get; set; }
            public Company company { get; set; }
        }

        public class KariyerGecmisi_RootObject
        {
            public int status { get; set; }
            public string message { get; set; }
            public List<UserExperience> userExperiences { get; set; }
        }


        public class KisiselBilgilerDataModel
        {
            public string Title { get; set; }
            public string Aciklama { get; set; }
            public string IconPath { get; set; }
        }

        class KisiselBilgilerTableSource : UITableViewSource
        {
            List<KisiselBilgilerDataModel> TableItems;
            KisiselBilgilerTabController AnaMainListCustomView1;
            public KisiselBilgilerTableSource(List<KisiselBilgilerDataModel> items, KisiselBilgilerTabController GelenYer)
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
                cell.UpdateCell(itemss);
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