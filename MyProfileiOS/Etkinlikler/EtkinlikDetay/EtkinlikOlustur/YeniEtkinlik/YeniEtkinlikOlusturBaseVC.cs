using CoreAnimation;
using DT.iOS.DatePickerDialog;
using Foundation;
using MyProfileiOS.GenericClass;
using MyProfileiOS.WebServiceHelper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using UIKit;

namespace MyProfileiOS
{
    public partial class YeniEtkinlikOlusturBaseVC : UIViewController
    {
        string base64String="";
        public YeniEtkinlikOlusturBaseVC (IntPtr handle) : base (handle)
        {
        }
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            GeriButton.TouchUpInside += GeriButton_TouchUpInside;
            EtkinlikFotoEkleButton.TouchUpInside += EtkinlikFotoEkleButton_TouchUpInside;
            GirisSaatiButton.TouchUpInside += GirisSaatiButton_TouchUpInside;
            CikisSaatiButton.TouchUpInside += CikisSaatiButton_TouchUpInside;
            OlusturButton.TouchUpInside += OlusturButton_TouchUpInside;
            EtkinlikSecildimiKontrolEt();
        }

        private void OlusturButton_TouchUpInside(object sender, EventArgs e)
        {
            if (BosVarmi())
            {
                WebService webService = new WebService();
                EtkinlikOlusturDataModel EtkinlikOlusturDataModel1;
                if (SecilenEtkinlik.EtkinlikID != null)
                {
                    EtkinlikOlusturDataModel1 = new EtkinlikOlusturDataModel()
                    {
                        event_id = Convert.ToInt32(SecilenEtkinlik.EtkinlikID),
                        title = EtkinlikAdiText.Text,
                        event_image = base64String,
                        event_description = EtkinlikAciklamaText.Text,
                        date_of_participation = Convert.ToDateTime(GirisSaatiButton.Title(UIControlState.Normal)).ToString("yyyy-MM-dd") + " " + GirisSaatiButton.Title(UIControlState.Normal),
                        end_date = Convert.ToDateTime(CikisSaatiButton.Title(UIControlState.Normal)).ToString("yyyy-MM-dd") + " " + CikisSaatiButton.Title(UIControlState.Normal),
                        latitude = SecilenEventDetay.latitude.ToString(System.Globalization.CultureInfo.InvariantCulture),
                        longitude = SecilenEventDetay.longitude.ToString(System.Globalization.CultureInfo.InvariantCulture),
                    };
                }
                else
                {
                    try
                    {
                        EtkinlikOlusturDataModel1 = new EtkinlikOlusturDataModel()
                        {
                            title = EtkinlikAdiText.Text,
                            event_image = base64String,
                            event_description = EtkinlikAciklamaText.Text,
                            date_of_participation = Convert.ToDateTime(GirisSaatiButton.Title(UIControlState.Normal)).ToString("yyyy-MM-dd") + " " + GirisSaatiButton.Title(UIControlState.Normal),
                            end_date = Convert.ToDateTime(CikisSaatiButton.Title(UIControlState.Normal)).ToString("yyyy-MM-dd") + " " + CikisSaatiButton.Title(UIControlState.Normal),
                            latitude = ((CoreLocation.CLLocationCoordinate2D)UserLastCurrentLocation.LastLoc).Latitude.ToString(System.Globalization.CultureInfo.InvariantCulture),
                            longitude = ((CoreLocation.CLLocationCoordinate2D)UserLastCurrentLocation.LastLoc).Longitude.ToString(System.Globalization.CultureInfo.InvariantCulture),
                        };
                    }
                    catch 
                    {
                        CustomToast.ShowToast(this, "Konumunuza ulaþýlamýyor.");
                        return;
                    }
                }

                var jsonstring = JsonConvert.SerializeObject(EtkinlikOlusturDataModel1);
                var Donus = webService.ServisIslem("user/userAttendedEvent", jsonstring);
                if (Donus != "Hata")
                {
                    CustomToast.ShowToast(this, "Etkinlik katýlýmý oluþturuldu.");
                    this.DismissViewController(true, null);
                    return;
                }
                else
                {
                    CustomToast.ShowToast(this, "Etkinlik katýlýmý baþarýsýz.");
                    return;
                }
            }
        }

        bool BosVarmi()
        {
            if (String.IsNullOrEmpty(EtkinlikAdiText.Text.Trim()))
            {
                CustomToast.ShowToast(this, "Lütfen Bir Etkinliðin Adýný Belirtin.");
                return false;
            }
            else if (base64String == "")
            {
                CustomToast.ShowToast(this,"Lütfen Bir Etkinlik Fotoðrafý Seçin.");
                return false;
            }
            else if (String.IsNullOrEmpty(EtkinlikAciklamaText.Text.Trim()))
            {
                CustomToast.ShowToast(this, "Lütfen Etkinlik Detayýný Belirtin.");
                return false;
            }
            else if (String.IsNullOrEmpty(GirisSaatiButton.Title(UIControlState.Normal).Trim()))
            {
                CustomToast.ShowToast(this, "Lütfen etkinliðe katýldýðýnýz zamaný belirtin.");
                return false;
            }
            else if (String.IsNullOrEmpty(CikisSaatiButton.Title(UIControlState.Normal).Trim()))
            {
                CustomToast.ShowToast(this, "Lütfen etkinlikten çýkýþ yapacaðýnýz saati belirtin.");
                return false;
            }
            else
            {
                return true;
            }
        }
        void EtkinlikSecildimiKontrolEt()
        {
            if (SecilenEtkinlik.EtkinlikID != null)
            {
                GetEventInfo(SecilenEtkinlik.EtkinlikID.ToString());
            }
        }
        RootObject SecilenEventDetay;
        void GetEventInfo(string EventID)
        {
            new System.Threading.Thread(new System.Threading.ThreadStart(delegate
            {
                WebService webService = new WebService();
                var Donuss = webService.OkuGetir("event/" + EventID + "/show");
                if (Donuss != null)
                {
                    try
                    {
                        var aaaa = Donuss.ToString();
                        SecilenEventDetay = Newtonsoft.Json.JsonConvert.DeserializeObject<RootObject>(aaaa);
                        InvokeOnMainThread(delegate ()
                        {
                            EtkinlikAdiText.Text = SecilenEventDetay.title;
                            EtkinlikAdiText.Enabled = false;
                            EtkinlikAciklamaText.Text = SecilenEventDetay.user_attended_event[0].event_description;
                        });
                    }
                    catch
                    {
                        InvokeOnMainThread(delegate ()
                        {
                            EtkinlikAdiText.Text = "";
                            EtkinlikAciklamaText.Text = "";
                            OlusturButton.Enabled = false;
                        });
                    }
                }
            })).Start();
        }
        private void CikisSaatiButton_TouchUpInside(object sender, EventArgs e)
        {
            var startingTime = DateTime.Now;
            var dialog = new DatePickerDialog();
            dialog.Show("Etkinlik Çýkýþ Saati", "Tamam", "Ýptal", UIDatePickerMode.Time, (dt) =>
            {
                CikisSaatiButton.SetTitle(dt.ToString("HH:mm"), UIControlState.Normal);
            }, startingTime);
        }
        private void GirisSaatiButton_TouchUpInside(object sender, EventArgs e)
        {
            var startingTime = DateTime.Now;
            var dialog = new DatePickerDialog();
            dialog.Show("Etkinlik Giriþ Saati", "Tamam", "Ýptal", UIDatePickerMode.Time, (dt) =>
            {
                GirisSaatiButton.SetTitle(dt.ToString("HH:mm"), UIControlState.Normal);
            }, startingTime);
        }
        UIImagePickerController picker2;
        private void EtkinlikFotoEkleButton_TouchUpInside(object sender, EventArgs e)
        {
            picker2 = new UIImagePickerController();
            picker2.SourceType = UIImagePickerControllerSourceType.PhotoLibrary;
            picker2.MediaTypes = UIImagePickerController.AvailableMediaTypes(UIImagePickerControllerSourceType.PhotoLibrary);
            picker2.FinishedPickingMedia += Picker_FinishedPickingMedia2;
            picker2.Canceled += Picker_Canceled2;
            this.PresentModalViewController(picker2, true);
        }
        private void Picker_Canceled2(object sender, EventArgs e)
        {
            picker2.DismissModalViewController(true);
        }
        private void Picker_FinishedPickingMedia2(object sender, UIImagePickerMediaPickedEventArgs e)
        {
            bool isImage = false;
            switch (e.Info[UIImagePickerController.MediaType].ToString())
            {
                case "public.image":
                    isImage = true;
                    break;
                case "public.video":
                    break;
            }
            NSUrl referenceURL = e.Info[new NSString("UIImagePickerControllerReferenceUrl")] as NSUrl;
            if (referenceURL != null) Console.WriteLine("Url:" + referenceURL.ToString());
            if (isImage)
            {
                UIImage originalImage = e.Info[UIImagePickerController.OriginalImage] as UIImage;
                if (originalImage != null)
                {
                  
                    NSData imageData = originalImage.AsJPEG(compressionQuality: 0.1f);
                    base64String = imageData.GetBase64EncodedString(NSDataBase64EncodingOptions.None);
                    EtkinlikFotoo.Image = UIImage.LoadFromData(imageData);
                    EtkinlikFotoo.ContentMode = UIViewContentMode.ScaleAspectFill;
                }
            }
            else
            {
                NSUrl mediaURL = e.Info[UIImagePickerController.MediaURL] as NSUrl;
                if (mediaURL != null)
                {
                    Console.WriteLine(mediaURL.ToString());
                }
            }
            picker2.DismissModalViewController(true);
        }
        private void GeriButton_TouchUpInside(object sender, EventArgs e)
        {
            this.DismissViewController(true, null);
        }
        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);
            AddDoneButton();
        }
        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            EtkinlikAdiText.AttributedPlaceholder = new NSAttributedString("Etkinlik Adý", null, UIColor.Black);
            /*
             let paddingView: UIView = UIView(frame: CGRect(x: 0, y: 0, width: 5, height: 20))
                textField.leftView = paddingView
                textField.leftViewMode = .always
             */
            var paddingview = new UIView();
            paddingview.Frame = new CoreGraphics.CGRect(0, 0, 7, 32f);
            EtkinlikAdiText.LeftView = paddingview;
            EtkinlikAdiText.LeftViewMode = UITextFieldViewMode.Always;
            UIlariDuzenle();
            EtkinlikAdiText.ShouldReturn += (textField) =>
            {
                textField.ResignFirstResponder();
                return true;
            };
        }
        void AddDoneButton()
        {
            #region DONE BUtton
            UIToolbar toolbar = new UIToolbar();
            toolbar.BarStyle = UIBarStyle.Default;
            toolbar.Translucent = true;
            toolbar.SizeToFit();

            //var gradientLayer = new CAGradientLayer();
            //gradientLayer.Colors = new[] { UIColor.FromRGB(28, 36, 121).CGColor, UIColor.FromRGB(28, 36, 121).CGColor };
            //gradientLayer.Locations = new NSNumber[] { 0, 1 };
            //gradientLayer.StartPoint = new CoreGraphics.CGPoint(0, 1);
            //gradientLayer.EndPoint = new CoreGraphics.CGPoint(1, 0);
            //gradientLayer.Frame = toolbar.Bounds;
            //toolbar.Layer.MasksToBounds = true;
            //toolbar.Layer.InsertSublayer(gradientLayer, 0);
            toolbar.BackgroundColor = UIColor.FromRGB(28, 36, 121);
            toolbar.BarTintColor = UIColor.FromRGB(28, 36, 121);

            UIBarButtonItem doneButton = new UIBarButtonItem("Tamam", UIBarButtonItemStyle.Done,
                                                             (s, e) =>
                                                             {
                                                                 EtkinlikAciklamaText.ResignFirstResponder();
                                                             });

            doneButton.TintColor = UIColor.White;
            toolbar.SetItems(new[] { new UIBarButtonItem(UIBarButtonSystemItem.FlexibleSpace), doneButton }, true);
            EtkinlikAciklamaText.InputAccessoryView = toolbar;
            #endregion
        }

        #region UI Duzenle
        void UIlariDuzenle()
        {
            EtkinlikAciklamaText.Layer.CornerRadius = 10f;
            EtkinlikAciklamaText.ClipsToBounds = true;

            EtkinlikAdiText.Layer.CornerRadius = 10f;
            EtkinlikAdiText.ClipsToBounds = true;

            GirisSaatiHazne.Layer.CornerRadius = 10f;
            GirisSaatiHazne.ClipsToBounds = true;
            CikisSaatiHazne.Layer.CornerRadius = 10f;
            CikisSaatiHazne.ClipsToBounds = true;

            FotografEkleHazne.Layer.CornerRadius = 5;
            FotografEkleHazne.Layer.BorderWidth = 1f;
            FotografEkleHazne.Layer.BorderColor = UIColor.FromRGB(28, 36, 121).CGColor;

            OlusturButton.Layer.CornerRadius = 10f;
            OlusturButton.ClipsToBounds = true;
        }
        #endregion

        #region DTOS
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
        }
        public class RootObject
        {
            public int id { get; set; }
            public string title { get; set; }
            public double latitude { get; set; }
            public double longitude { get; set; }
            public string created_at { get; set; }
            public string updated_at { get; set; }
            public List<UserAttendedEvent> user_attended_event { get; set; }
        }
        public class EtkinlikOlusturDataModel
        {
            public int event_id { get; set; }
            public string title { get; set; }
            public string event_description { get; set; }
            public string event_image { get; set; }
            public string date_of_participation { get; set; }
            public string end_date { get; set; }
            public string longitude { get; set; }
            public string latitude { get; set; }
        }
        #endregion
    }

    public static class SecilenEtkinlik
    {
        public static int? EtkinlikID { get; set; }
    }

}