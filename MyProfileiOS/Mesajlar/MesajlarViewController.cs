using FFImageLoading;
using Foundation;
using MyProfileiOS.DataBasee;
using MyProfileiOS.WebServiceHelper;
using System;
using System.Collections.Generic;
using UIKit;
using static MyProfileiOS.ChatDetayBase;

namespace MyProfileiOS
{
    public partial class MesajlarViewController : UIViewController
    {
        #region Tanimlamalar
        List<Message> mFriends;
        Mesaj_Listesi_RootObject Mesaj_Listesi_RootObject1;
        #endregion
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
            SetUserPhoto();
        }
        void SetUserPhoto()
        {
            new System.Threading.Thread(new System.Threading.ThreadStart(delegate
            {
                var MeId = DataBase.USER_INFO_GETIR()[0].id;
                WebService webService = new WebService();
                var Donus = webService.OkuGetir("user/" + MeId + "/show");
                if (Donus != null)
                {
                    InvokeOnMainThread(delegate () {
                        var MemberInfo1 = Newtonsoft.Json.JsonConvert.DeserializeObject<USER_INFO>(Donus);
                        ImageService.Instance.LoadUrl(MemberInfo1.profile_photo).Into(ProfilFoto);
                    });
                }
            })).Start();
        }

        void GetMessageFriend()
        {
            mFriends = new List<Message>();
            WebService webService = new WebService();
            var Donus = webService.OkuGetir("message/getMessages");
            if (Donus != null)
            {
                var A = Donus.ToString();
                if (Donus != null)
                {
                    var ddddd = Donus.ToString();
                    Mesaj_Listesi_RootObject1 = Newtonsoft.Json.JsonConvert.DeserializeObject<Mesaj_Listesi_RootObject>(Donus.ToString());
                    mFriends = Mesaj_Listesi_RootObject1.messages;
                }
                if (mFriends.Count > 0)
                {
                    //EtkinlikTableView.RegisterClassForCellReuse(typeof(EtkinlikCustomCardView), "EtkinlikCustomCardView");
                    mFriends.Reverse();
                    MesajFriendsTableView.RegisterNibForCellReuse(UINib.FromName("MesajlarKisiListItem", NSBundle.MainBundle), "MesajlarKisiListItem");
                    MesajFriendsTableView.Source = new MesajlarFriendListCustomTableSource(mFriends, this);
                    MesajFriendsTableView.SeparatorStyle = UITableViewCellSeparatorStyle.None;
                    MesajFriendsTableView.RowHeight = 84f;
                    MesajFriendsTableView.ReloadData();
                }
            }
         
        }

        public void RowSelectt(Message Secim)
        {
            var LokasyonKisilerStory = UIStoryboard.FromName("ChatDetayVC", NSBundle.MainBundle);
            ChatDetayBase controller = LokasyonKisilerStory.InstantiateViewController("ChatDetayBase") as ChatDetayBase;
            controller.ModalPresentationStyle = UIModalPresentationStyle.FullScreen;

            var MeId = DataBase.USER_INFO_GETIR()[0].id;
            if (Secim.from_user_id != MeId)
            {
                ChatUserId.UserID = Secim.from_user_id;
            }
            else
            {
                ChatUserId.UserID = Secim.to_user_id;
            }


            this.PresentViewController(controller, true, null);
        }

        class MesajlarFriendListCustomTableSource : UITableViewSource
        {
            List<Message> TableItems;
            MesajlarViewController AnaMainListCustomView1;
            string MeID;
            public MesajlarFriendListCustomTableSource(List<Message> items, MesajlarViewController GelenYer)
            {
                TableItems = items;
                AnaMainListCustomView1 = GelenYer;
                MeID = DataBase.USER_INFO_GETIR()[0].id;
            }
            public override nint RowsInSection(UITableView tableview, nint section)
            {
                return TableItems.Count;
            }
            public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
            {

                var itemss = TableItems[indexPath.Row];

                //var cell = (MesajlarKisiListItem)tableView.DequeueReusableCell("MesajlarKisiListItem", indexPath) as MesajlarKisiListItem;
                var cell = (MesajlarKisiListItem)tableView.DequeueReusableCell(MesajlarKisiListItem.Key);
                if (cell == null)
                {
                    cell = MesajlarKisiListItem.Create();
                }
                cell.UpdateCell(itemss, MeID);
                return cell;
            }


            public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
            {
                tableView.DeselectRow(indexPath, true);
                tableView.DeselectRow(indexPath, true);
                AnaMainListCustomView1.RowSelectt(TableItems[indexPath.Row]);
                //tableView.BeginUpdates();
                //tableView.ReloadRows(new NSIndexPath[] { indexPath }, UITableViewRowAnimation.Fade);
                //tableView.EndUpdates();

                //tableView.ScrollToRow(indexPath, UITableViewScrollPosition.Top, true);
                //var secimm = TableItems[indexPath.Row];
               
                //AnaMainListCustomView1.RowClickk(secimm);

            }
        }

        public class FromUser
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
            public string email_verified_at { get; set; }
            public int status { get; set; }
            public string package { get; set; }
            public string created_at { get; set; }
            public string updated_at { get; set; }
        }

        public class ToUser
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
            public string email_verified_at { get; set; }
            public int status { get; set; }
            public string package { get; set; }
            public string created_at { get; set; }
            public string updated_at { get; set; }
        }

        public class Message
        {
            public int id { get; set; }
            public int type { get; set; }
            public string parent_id { get; set; }
            public string from_user_id { get; set; }
            public string to_user_id { get; set; }
            public string message { get; set; }
            public int status { get; set; }
            public int end_message { get; set; }
            public string created_at { get; set; }
            public string updated_at { get; set; }
            public FromUser from_user { get; set; }
            public ToUser to_user { get; set; }
        }

        public class Mesaj_Listesi_RootObject
        {
            public int status { get; set; }
            public string message { get; set; }
            public List<Message> messages { get; set; }
        }
    }
}