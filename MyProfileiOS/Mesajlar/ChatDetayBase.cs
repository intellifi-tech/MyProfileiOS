using CoreGraphics;
using FFImageLoading;
using Foundation;
using MyProfileiOS.DataBasee;
using MyProfileiOS.GenericClass;
using MyProfileiOS.Mesajlar.Cells;
using MyProfileiOS.WebServiceHelper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using UIKit;

namespace MyProfileiOS
{
    public partial class ChatDetayBase : UIViewController
    {
        nfloat normalKisit, MesajBGViewwFrameBottom, AralikDurumu;
        List<Message> mItems;
        USER_INFO Mee; 
        public ChatDetayBase (IntPtr handle) : base (handle)
        {
        }
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            NSNotificationCenter.DefaultCenter.AddObserver(UIKeyboard.WillShowNotification, KeyboardWillShow);
            NSNotificationCenter.DefaultCenter.AddObserver(UIKeyboard.DidShowNotification, KeyboardDidShow);
            NSNotificationCenter.DefaultCenter.AddObserver(UIKeyboard.WillHideNotification, KeyboardWillHide);
            MesajInput.ShouldReturn += (textField) =>
            {
                textField.ResignFirstResponder();
                return true;
            };
            normalKisit = this.BottomKisitlama.Constant;
            GonderButton.TouchUpInside += GonderButton_TouchUpInside;
            GeriButton.TouchUpInside += GeriButton_TouchUpInside;
            PorfileGitButton.TouchUpInside += PorfileGitButton_TouchUpInside;
        }

        private void PorfileGitButton_TouchUpInside(object sender, EventArgs e)
        {
            if (SecilenKullanici.UserID == ChatUserId.UserID)
            {
                this.DismissViewController(true, null);
            }
            else
            {
                SecilenKullanici.UserID = ChatUserId.UserID;
                var LokasyonKisilerStory = UIStoryboard.FromName("Main", NSBundle.MainBundle);
                ProfilViewKontroller controller = LokasyonKisilerStory.InstantiateViewController("ProfilViewKontroller") as ProfilViewKontroller;
                controller.ModalPresentationStyle = UIModalPresentationStyle.FullScreen;
                this.PresentViewController(controller, true, null);
            }
        }

        private void GeriButton_TouchUpInside(object sender, EventArgs e)
        {
            this.DismissViewController(true,null);
        }
        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            GetUserInfo();
            MesajTablo.BackgroundColor = UIColor.Clear;
            MesajInput.Layer.CornerRadius = MesajInput.Frame.Height / 2;
            MesajInput.Layer.BorderWidth = 1f;
            MesajInput.Layer.BorderColor = UIColor.FromRGB(238, 238, 238).CGColor;
            MesajInput.ClipsToBounds = true;
            MesajTablo.SeparatorStyle = UITableViewCellSeparatorStyle.None;
        }
        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);
            Mee = DataBase.USER_INFO_GETIR()[0];
            MesajlariCek();
            MesajlariOkunduYap();
            MessageListenerr();
        }

        #region Klavye Ayarlari
        bool IlkAcilis = false;
        nfloat keyboardHeight = 0;
        nfloat iphpnexIlkKisit = 0;
        private void KeyboardWillShow(NSNotification notification)
        {
            #region Keyboardd
            //if (keyboardHeight <= 0)
            //{
            //    keyboardHeight = ((NSValue)notification.UserInfo.ValueForKey(UIKeyboard.FrameBeginUserInfoKey)).RectangleFValue.Height;
            //}
            //var maxYuseklik = UIScreen.MainScreen.Bounds.Height;
            keyboardHeight = ((NSValue)notification.UserInfo.ValueForKey(UIKeyboard.FrameEndUserInfoKey)).RectangleFValue.Height;
            UIView.Animate(0.1, () => {
                if (!IlkAcilis)
                {
                    //var AralikDurumu = this.View.Frame.Height - MesajBGViewwFrameBottom;
                    if (AralikDurumu > 1)//For Iphone X ++++
                    {
                        iphpnexIlkKisit = keyboardHeight - (this.View.Frame.Height - MesajBGViewwFrameBottom);
                        this.BottomKisitlama.Constant = iphpnexIlkKisit;
                        this.View.LayoutIfNeeded();
                    }
                    else
                    {
                        this.BottomKisitlama.Constant = keyboardHeight;
                        this.View.LayoutIfNeeded();
                    }
                    IlkAcilis = true;
                }
                else
                {

                    if (AralikDurumu > 1)//For Iphone X ++++
                    {
                        this.BottomKisitlama.Constant = iphpnexIlkKisit;
                        this.View.LayoutIfNeeded();
                    }
                    else
                    {
                        this.BottomKisitlama.Constant = keyboardHeight /*+ (normalKisit * 1) + MesajBGVieww.Frame.Height*/;
                        this.View.LayoutIfNeeded();
                    }

                }

                //var framee = this.MesajYazBGView.Frame;
                //MesajYazBGView.Frame = new CoreGraphics.CGRect(framee.X, framee.Y - keyboardHeight, framee.Width, framee.Height);
            });
            #endregion
        }
        private void KeyboardDidShow(NSNotification notification)
        {
            if (mItems.Count > 0)
            {
                var bottomIndexPath = NSIndexPath.FromRowSection(MesajTablo.NumberOfRowsInSection(0) - 1, 0);
                try
                {
                    MesajTablo.ScrollToRow(bottomIndexPath, UITableViewScrollPosition.Bottom, true);
                }
                catch
                {
                }

            }
        }
        private void KeyboardWillHide(NSNotification notification)
        {
            UIView.Animate(0.1, () => {
                this.BottomKisitlama.Constant = normalKisit;
                this.View.LayoutIfNeeded();
            });
        }
        #endregion

        #region Get User Info
        User_RootObject HostModelll;
        void GetUserInfo()
        {
            WebService webService = new WebService();
            var Donus = webService.OkuGetir("user/" + ChatUserId.UserID + "/show");
            if (Donus != null)
            {
                HostModelll = Newtonsoft.Json.JsonConvert.DeserializeObject<User_RootObject>(Donus.ToString());
                UserName.Text = HostModelll.name + " " + HostModelll.surname;
                UserImage.Layer.CornerRadius = UserImage.Frame.Height / 2;
                UserImage.ClipsToBounds = true;
                UserImage.ContentMode = UIViewContentMode.ScaleAspectFill;
                ImageService.Instance.LoadUrl(HostModelll.profile_photo).Into(UserImage);
                //HostImagePath = HostModelll.profile_photo;
                //UserImagePath = DataBase.USER_INFO_GETIR()[0].profile_photo;

            }
        }

        #endregion

        #region Mesaj Listener
        bool MesajlariCek()
        {
            WebService webService = new WebService();
            var Donus = webService.OkuGetir("message/" + ChatUserId.UserID + "/userIndexMessages");
            if (Donus != null)
            {
                var Modell = Newtonsoft.Json.JsonConvert.DeserializeObject<ChatItems>(Donus.ToString());
                if (mItems!=null)
                {
                    if (mItems.Count == Modell.messages.Count)//DegisimOlmamis
                    {
                        mItems = Modell.messages;
                        return false;
                    }
                    else
                    {
                        mItems = Modell.messages;
                        MesajTablo.AllowsSelection = false;
                        MesajTablo.SeparatorStyle = UITableViewCellSeparatorStyle.None;
                        MesajTablo.RegisterClassForCellReuse(typeof(IncomingCell), IncomingCell.CellId);
                        MesajTablo.RegisterClassForCellReuse(typeof(OutgoingCell), OutgoingCell.CellId);
                        //View.AddSubview(tableView);
                        MesajTablo.Source = new ChatCustomTableCellSoruce(mItems, this, Mee);
                        MesajTablo.ReloadData();
                        if (mItems.Count > 0)
                        {
                            var bottomIndexPath = NSIndexPath.FromRowSection(MesajTablo.NumberOfRowsInSection(0) - 1, 0);
                            MesajTablo.ScrollToRow(bottomIndexPath, UITableViewScrollPosition.Bottom, true);
                        }
                        return true;
                    }
                }
                else
                {
                    mItems = Modell.messages;
                    MesajTablo.AllowsSelection = false;
                    MesajTablo.SeparatorStyle = UITableViewCellSeparatorStyle.None;
                    MesajTablo.RegisterClassForCellReuse(typeof(IncomingCell), IncomingCell.CellId);
                    MesajTablo.RegisterClassForCellReuse(typeof(OutgoingCell), OutgoingCell.CellId);
                    //View.AddSubview(tableView);
                    MesajTablo.Source = new ChatCustomTableCellSoruce(mItems, this, Mee);
                    MesajTablo.ReloadData();
                    if (mItems.Count > 0)
                    {
                        var bottomIndexPath = NSIndexPath.FromRowSection(MesajTablo.NumberOfRowsInSection(0) - 1, 0);
                        MesajTablo.ScrollToRow(bottomIndexPath, UITableViewScrollPosition.Bottom, true);
                    }
                    return true;
                }
               
            }
            else
            {
                return false;
            }
        }

        void MesajlariOkunduYap()
        {
            WebService webService = new WebService();
            var Donus = webService.OkuGetir("message/" + ChatUserId.UserID + "/readMessage");
        }

        System.Threading.Timer _timer;
        void MessageListenerr()
        {
            new System.Threading.Thread(new System.Threading.ThreadStart(delegate
            {

                _timer = new System.Threading.Timer((o) =>
                {
                    try
                    {
                        MesajlariCek();
                        //InvokeOnMainThread(delegate ()
                        //{
                        //    if (Durum) //Ýçerik  Deðiþmiþse Uygula
                        //    {
                        //        MesajTablo.AllowsSelection = false;
                        //        MesajTablo.SeparatorStyle = UITableViewCellSeparatorStyle.None;
                        //        MesajTablo.RegisterClassForCellReuse(typeof(IncomingCell), IncomingCell.CellId);
                        //        MesajTablo.RegisterClassForCellReuse(typeof(OutgoingCell), OutgoingCell.CellId);
                        //        //View.AddSubview(tableView);
                        //        MesajTablo.Source = new ChatCustomTableCellSoruce(mItems, this, Mee);
                        //        MesajTablo.ReloadData();
                        //        if (mItems.Count > 0)
                        //        {
                        //            var bottomIndexPath = NSIndexPath.FromRowSection(MesajTablo.NumberOfRowsInSection(0) - 1, 0);
                        //            MesajTablo.ScrollToRow(bottomIndexPath, UITableViewScrollPosition.Bottom, true);
                        //        }
                        //    }
                        //});
                    }
                    catch
                    {
                    }

                }, null, 0, 3000);
            })).Start();
        }
        #endregion

        #region Mesaj Gonder
        private void GonderButton_TouchUpInside(object sender, EventArgs e)
        {
            if (MesajInput.Text.Trim() != "")
            {
                if (MesajAt())
                {
                    MesajlariCek();
                    if (mItems.Count > 0)
                    {
                        MesajTablo.AllowsSelection = false;
                        MesajTablo.SeparatorStyle = UITableViewCellSeparatorStyle.None;
                        MesajTablo.RegisterClassForCellReuse(typeof(IncomingCell), IncomingCell.CellId);
                        MesajTablo.RegisterClassForCellReuse(typeof(OutgoingCell), OutgoingCell.CellId);
                        //View.AddSubview(tableView);
                        MesajTablo.Source = new ChatCustomTableCellSoruce(mItems, this, Mee);
                        MesajTablo.ReloadData();
                        if (mItems.Count > 0)
                        {
                            var bottomIndexPath = NSIndexPath.FromRowSection(MesajTablo.NumberOfRowsInSection(0) - 1, 0);
                            MesajTablo.ScrollToRow(bottomIndexPath, UITableViewScrollPosition.Bottom, true);
                        }

                        //ChatTableView.Source = new ChatCustomTableCellSoruce(ChatDetayDTO1, this, Mee);
                        MesajTablo.BackgroundColor = UIColor.Clear;
                        MesajInput.Text = "";
                    }
                }
            }
            else
            {
                MesajInput.Text = "";
            }
        }
        bool MesajAt()
        {
            WebService webService = new WebService();
            Mesaj_Gonder_RootObject Mesaj_Gonder_RootObject1 = new Mesaj_Gonder_RootObject()
            {
                message = MesajInput.Text,
                to_user_id = Convert.ToInt32(ChatUserId.UserID)
            };
            var jsonstring = JsonConvert.SerializeObject(Mesaj_Gonder_RootObject1);
            var Donus = webService.ServisIslem("message/sendMessage", jsonstring);
            if (Donus != "Hata")
            {
                return true;
            }
            else
            {
                CustomToast.ShowToast(this, "Mesaj Gönderilemedi!", ToastType.Error);
                return true;
            }


        }
        #endregion

        #region DtaModels
        public class User_RootObject
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


        public class Mesaj_Gonder_RootObject
        {
            public int to_user_id { get; set; }
            public string message { get; set; }
        }


        public class Message
        {
            public int id { get; set; }
            public int type { get; set; }
            public object parent_id { get; set; }
            public int from_user_id { get; set; }
            public int to_user_id { get; set; }
            public string message { get; set; }
            public int status { get; set; }
            public int end_message { get; set; }
            public object created_at { get; set; }
            public object updated_at { get; set; }
        }

        public class ChatItems
        {
            public int status { get; set; }
            public string message { get; set; }
            public List<Message> messages { get; set; }
        }
        #endregion

        #region Table Source
        #region Chat Table Source
        public class ChatCustomTableCellSoruce : UITableViewSource
        {
            static readonly NSString IncomingCellId = new NSString("Incoming");
            static readonly NSString OutgoingCellId = new NSString("Outgoing");

            IList<Message> messages;
            ChatDetayBase ChatVC1;
            USER_INFO ME;

            readonly BubbleCell[] sizingCells;

            public ChatCustomTableCellSoruce(IList<Message> messages, ChatDetayBase ChatVC2, USER_INFO ME2)
            {
                if (messages == null)
                    throw new ArgumentNullException(nameof(messages));

                this.messages = messages;
                sizingCells = new BubbleCell[2];
                ChatVC1 = ChatVC2;
                ME = ME2;
            }

            public override nint RowsInSection(UITableView tableview, nint section)
            {
                return messages.Count;
            }

            public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
            {
                BubbleCell cell = null;
                Message msg = messages[indexPath.Row];

                cell = (BubbleCell)tableView.DequeueReusableCell(GetReuseId(msg));
                cell.Message = msg;
                cell.BackgroundColor = UIColor.Clear;
                return cell;
            }

            public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
            {
                Message msg = messages[indexPath.Row];
                return CalculateHeightFor(msg, tableView);
            }

            public override nfloat EstimatedHeight(UITableView tableView, NSIndexPath indexPath)
            {
                Message msg = messages[indexPath.Row];
                return CalculateHeightFor(msg, tableView);
            }

            nfloat CalculateHeightFor(Message msg, UITableView tableView)
            {
                int index = -1;
                if (ME.id == msg.from_user_id.ToString()) //GelenMesaj
                {
                    index = 0;
                }
                else
                {
                    index = 1;
                }
                BubbleCell cell = sizingCells[index];
                if (cell == null)
                    cell = sizingCells[index] = (BubbleCell)tableView.DequeueReusableCell(GetReuseId(msg));

                cell.Message = msg;

                cell.SetNeedsLayout();
                cell.LayoutIfNeeded();
                CGSize size = cell.ContentView.SystemLayoutSizeFittingSize(UIView.UILayoutFittingCompressedSize);

                return NMath.Ceiling(size.Height) + 1;

            }

            NSString GetReuseId(Message GelenDTO)
            {
                if (ME.id == GelenDTO.from_user_id.ToString())//Incoming
                {
                    return OutgoingCellId;
                }
                else //OutGoing
                {
                    return IncomingCellId;
                    
                }
            }
        }
        #endregion
        #endregion


        public static class ChatUserId
        {
            public static string UserID { get; set; }
        }
    }
}