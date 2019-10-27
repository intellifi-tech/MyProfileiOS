using CoreAnimation;
using DT.iOS.DatePickerDialog;
using Foundation;
using System;
using UIKit;

namespace MyProfileiOS
{
    public partial class YeniEtkinlikOlusturBaseVC : UIViewController
    {
        public YeniEtkinlikOlusturBaseVC (IntPtr handle) : base (handle)
        {
        }
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            GeriButton.TouchUpInside += GeriButton_TouchUpInside;
            EtkinlikFotoEkleButton.TouchUpInside += EtkinlikFotoEkleButton_TouchUpInside;
            GirisSaatiButton.TouchUpInside += GirisSaatiButton_TouchUpInside;
        }

        private void GirisSaatiButton_TouchUpInside(object sender, EventArgs e)
        {
            var startingTime = DateTime.Today;
            var dialog = new DatePickerDialog();
            dialog.BackgroundColor = UIColor.FromRGBA(0, 0, 0, 0);

            dialog.Show("Dogum Tarihi Seçin", "Tamam", "Vazgeç", UIDatePickerMode.Date, (dt) =>
            {
                EtkinlikAciklamaText.Text = dt.ToShortDateString();
            }, startingTime);

            //var startingTime = DateTime.Now;
            //var dialog = new DatePickerDialog();
            //dialog.Show("Etkinlik Giriþ Saati", "Tamam", "Ýptal", UIDatePickerMode.Time, (dt) =>
            //{
            //    GirisSaatiButton.SetTitle(dt.ToShortTimeString(), UIControlState.Normal);
            //    //TimePickLabel.Text = dt.ToString();
            //}, startingTime);
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
                    string _base64String;
                    NSData imageData = originalImage.AsJPEG(compressionQuality: 0.1f);
                    _base64String = imageData.GetBase64EncodedString(NSDataBase64EncodingOptions.None);
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

            var gradientLayer = new CAGradientLayer();
            gradientLayer.Colors = new[] { UIColor.FromRGB(28, 36, 121).CGColor, UIColor.FromRGB(28, 36, 121).CGColor };
            gradientLayer.Locations = new NSNumber[] { 0, 1 };
            gradientLayer.StartPoint = new CoreGraphics.CGPoint(0, 1);
            gradientLayer.EndPoint = new CoreGraphics.CGPoint(1, 0);
            gradientLayer.Frame = toolbar.Bounds;
            toolbar.Layer.MasksToBounds = true;
            toolbar.Layer.InsertSublayer(gradientLayer, 0);

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
    }
}