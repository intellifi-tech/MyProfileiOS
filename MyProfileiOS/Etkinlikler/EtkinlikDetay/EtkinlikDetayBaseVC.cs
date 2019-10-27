using CoreGraphics;
using Foundation;
using MyProfileiOS.DataBasee;
using MyProfileiOS.Etkinlikler.EtkinlikDetay;
using MyProfileiOS.WebServiceHelper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using UIKit;

namespace MyProfileiOS
{
    public partial class EtkinlikDetayBaseVC : UIViewController
    {
        public TakipEttiklerimEtkinliklerViewController.UserAttendedEvent GelenModel1;
        EtkinlikDetayi_RootObject EtkinlikDetayi_RootObject1;
        UILabel YorumlarTitleLabel;
        nfloat normalKisit, MesajBGViewwFrameBottom, AralikDurumu;
        public EtkinlikDetayBaseVC (IntPtr handle) : base (handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            NSNotificationCenter.DefaultCenter.AddObserver(UIKeyboard.WillShowNotification, KeyboardWillShow);
            NSNotificationCenter.DefaultCenter.AddObserver(UIKeyboard.DidShowNotification, KeyboardDidShow);
            NSNotificationCenter.DefaultCenter.AddObserver(UIKeyboard.WillHideNotification, KeyboardWillHide);
            YorumTextField.ShouldReturn += (textField) =>
            {
                textField.ResignFirstResponder();
                return true;
            };
            normalKisit = this.BottomKisitlama.Constant;
            GeriButton.TouchUpInside += GeriButton_TouchUpInside;
            YorumGonderButton.TouchUpInside += YorumGonderButton_TouchUpInside;
            KatilimcilarButton.TouchUpInside += KatilimcilarButton_TouchUpInside;
        }

        private void KatilimcilarButton_TouchUpInside(object sender, EventArgs e)
        {
            var Katilimcilarrr = EtkinlikDetayi_RootObject1.user_attended_event.Select(item => item.user).ToList();
            var UIStoryboard1 = UIStoryboard.FromName("Main", NSBundle.MainBundle);
            EtkinlikKatilimcilariVC controller = UIStoryboard1.InstantiateViewController("EtkinlikKatilimcilariVC") as EtkinlikKatilimcilariVC;
            controller.ModalPresentationStyle = UIModalPresentationStyle.FullScreen;
            controller.KatilimciLis = Katilimcilarrr;
            this.PresentViewController(controller, true, null);
        }

        private void YorumGonderButton_TouchUpInside(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(YorumTextField.Text))
            {
                WebService webService = new WebService();
                Yorum_Gonder_RootObject Yorum_Gonder_RootObject1 = new Yorum_Gonder_RootObject()
                {
                    attended_id = Convert.ToInt32(EtkinlikDetayi_RootObject1.user_attended_event[0].id),
                    comment = YorumTextField.Text
                };
                string jsonString = JsonConvert.SerializeObject(Yorum_Gonder_RootObject1);
                var Donus = webService.ServisIslem("comment/create", jsonString);
                if (Donus != "Hata")
                {
                    var guncelyorumlar = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Comment>>(Donus);
                    YorumlarTitleLabel.Text = "Yorumlar (" + guncelyorumlar.Count.ToString() + ")";
                    EtkinlikDetayi_RootObject1.user_attended_event[0].comments = guncelyorumlar;
                    YorumTextField.Text = "";
                    var MeIDD = DataBase.USER_INFO_GETIR()[0].id;
                    var BenimSonYorumum = EtkinlikDetayi_RootObject1.user_attended_event[0].comments.FindLast(item => item.user_id.ToString() == MeIDD);
                    if (BenimSonYorumum != null)
                    {
                        YorumEkle(BenimSonYorumum);
                    }
                }
            }
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
            //if (mItems.Count > 0)
            //{
            //    var bottomIndexPath = NSIndexPath.FromRowSection(MesajTablo.NumberOfRowsInSection(0) - 1, 0);
            //    try
            //    {
            //        MesajTablo.ScrollToRow(bottomIndexPath, UITableViewScrollPosition.Bottom, true);
            //    }
            //    catch
            //    {
            //    }

            //}
        }
        private void KeyboardWillHide(NSNotification notification)
        {
            UIView.Animate(0.1, () => {
                this.BottomKisitlama.Constant = normalKisit;
                this.View.LayoutIfNeeded();
            });
        }
        #endregion

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
            YorumGonderButton.ContentEdgeInsets = new UIEdgeInsets(0, 6, 0, 6);
            
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
            YorumlariGetir2();
        }

        void YorumlariGetir2()
        {
            new System.Threading.Thread(new System.Threading.ThreadStart(delegate
            {
                WebService webService = new WebService();
                var Donus = webService.OkuGetir("event/" + GelenModel1.event_id + "/show");
                if (Donus != null)
                {
                    EtkinlikDetayi_RootObject1 = Newtonsoft.Json.JsonConvert.DeserializeObject<EtkinlikDetayi_RootObject>(Donus);
                    if (EtkinlikDetayi_RootObject1.user_attended_event[0].comments != null)
                    {
                        InvokeOnMainThread(delegate ()
                        {
                            if (EtkinlikDetayi_RootObject1.user_attended_event[0].comments.Count > 0)
                            {

                                YorumlarTitleLabel.Text = "Yorumlar (" + EtkinlikDetayi_RootObject1.user_attended_event[0].comments.Count.ToString() + ")";
                                YorumlarScrollDoldur();
                            }
                            else
                            {
                                YorumlarTitleLabel.Text = "Ýlk yorumu sen yap.";
                            }
                        });
                    }
                }
            })).Start();
        }
        List<YorumCustomCell> YorumIcerikleri = new List<YorumCustomCell>();
        void YorumlarScrollDoldur()
        {
            for (int i = 0; i < EtkinlikDetayi_RootObject1.user_attended_event[0].comments.Count; i++)
            {
                var cell = YorumCustomCell.Create();
                cell.UpdateCell(EtkinlikDetayi_RootObject1.user_attended_event[0].comments[i]);
                if (i==0)
                {
                    cell.Frame = new CGRect(0, YorumlarTitleLabel.Frame.Bottom + 10f, UIScreen.MainScreen.Bounds.Width, cell.HucreYuksekliginiGetir());
                }
                else
                {
                    cell.Frame = new CGRect(0, YorumIcerikleri[i-1].Frame.Bottom, UIScreen.MainScreen.Bounds.Width, cell.HucreYuksekliginiGetir());
                }
                YorumIcerikleri.Add(cell);
                ScrollVieww.AddSubview(cell);
            }
            if (EtkinlikDetayi_RootObject1.user_attended_event[0].comments.Count>0)
            {
                ScrollVieww.ContentSize = new CoreGraphics.CGSize(UIScreen.MainScreen.Bounds.Width, YorumIcerikleri[YorumIcerikleri.Count - 1].Frame.Bottom);
            }
        }

        void YorumEkle(Comment EklenenYorum)
        {
            var cell = YorumCustomCell.Create();
            cell.UpdateCell(EklenenYorum);
            if (YorumIcerikleri.Count > 0)
            {
                cell.Frame = new CGRect(0, YorumIcerikleri[YorumIcerikleri.Count - 1].Frame.Bottom, UIScreen.MainScreen.Bounds.Width, cell.HucreYuksekliginiGetir());
            }
            else
            {
                cell.Frame = new CGRect(0, YorumlarTitleLabel.Frame.Bottom + 10f, UIScreen.MainScreen.Bounds.Width, cell.HucreYuksekliginiGetir());
            }
            YorumIcerikleri.Add(cell);
            ScrollVieww.AddSubview(cell);
            ScrollVieww.ContentSize = new CoreGraphics.CGSize(UIScreen.MainScreen.Bounds.Width, YorumIcerikleri[YorumIcerikleri.Count - 1].Frame.Bottom);
            ScrollVieww.LayoutIfNeeded();
            /*
             * let bottomOffset = CGPoint(x: 0, y: scrollView.contentSize.height - scrollView.bounds.size.height)
                scrollView.setContentOffset(bottomOffset, animated: true)
             */
            var BottomOfsett = new CGPoint(0, ScrollVieww.ContentSize.Height - ScrollVieww.Bounds.Size.Height);
            ScrollVieww.SetContentOffset(BottomOfsett, animated: true);
        }

        #region DataModels
        public class Yorum_Gonder_RootObject
        {
            public int attended_id { get; set; }
            public string comment { get; set; }
        }

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
    }
}