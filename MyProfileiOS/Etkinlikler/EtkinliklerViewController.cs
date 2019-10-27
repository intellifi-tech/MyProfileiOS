using FFImageLoading;
using Foundation;
using MyProfileiOS.DataBasee;
using MyProfileiOS.WebServiceHelper;
using Pager;
using System;
using UIKit;

namespace MyProfileiOS
{
    public partial class EtkinliklerViewController : UIViewController
    {
        public EtkinliklerViewController (IntPtr handle) : base (handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            var colors = new[]
            {
                UIColor.FromRGB(48, 79, 254),
                UIColor.FromRGB(48, 79, 254),
            };
            var MainStoryBoard = UIStoryboard.FromName("Main", NSBundle.MainBundle);
            var TakipEttiklerimEtkinliklerViewController1 = MainStoryBoard.InstantiateViewController("TakipEttiklerimEtkinliklerViewController") as TakipEttiklerimEtkinliklerViewController;
            var GlobalEtkinliklerViewController1 = MainStoryBoard.InstantiateViewController("GlobalEtkinliklerViewController") as GlobalEtkinliklerViewController;

            TakipEttiklerimEtkinliklerViewController1.Title = "TAKÝP ETTÝKLERÝM";
            GlobalEtkinliklerViewController1.Title = "GLOBAL";

            UIViewController[] pages = new UIViewController[]
            {
                TakipEttiklerimEtkinliklerViewController1,
                GlobalEtkinliklerViewController1
            };

            var pager = new PagerViewController(new PagerStyle(PagerStyle.Stretched) { SelectedStripColors = colors }, pages);
            var viewController = pager;
            viewController.View.Frame = PgerHazneee.Bounds;
            viewController.WillMoveToParentViewController(this);
            PgerHazneee.AddSubview(viewController.View);
            this.AddChildViewController(viewController);
            viewController.DidMoveToParentViewController(this);
        }
        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);
            KullaniciFoto.Layer.CornerRadius = KullaniciFoto.Frame.Height / 2;
            KullaniciFoto.ClipsToBounds = true;
            YeniEtkinlikOlusturButton.Layer.CornerRadius = YeniEtkinlikOlusturButton.Frame.Height / 2;
            YeniEtkinlikOlusturButton.ClipsToBounds = true;
            ButtonTasarimlariniAyarla(YeniEtkinlikOlusturButton);
            YeniEtkinlikOlusturButton.TouchUpInside += YeniEtkinlikOlusturButton_TouchUpInside;
            YeniEtkinlikSubButton.TouchUpInside += YeniEtkinlikSubButton_TouchUpInside;
            EtkinligeKatilSubButton.TouchUpInside += EtkinligeKatilSubButton_TouchUpInside;
        }

        private void EtkinligeKatilSubButton_TouchUpInside(object sender, EventArgs e)
        {
            HideSubButtons();
            HoverView.Hidden = true;
            var UIStoryboard1 = UIStoryboard.FromName("Main", NSBundle.MainBundle);
            MevcutEtkinlikBaseVC controller = UIStoryboard1.InstantiateViewController("MevcutEtkinlikBaseVC") as MevcutEtkinlikBaseVC;
            controller.ModalPresentationStyle = UIModalPresentationStyle.FullScreen;
            this.PresentViewController(controller, true, null);
        }

        private void YeniEtkinlikSubButton_TouchUpInside(object sender, EventArgs e)
        {
            HideSubButtons();
            HoverView.Hidden = true;
            var UIStoryboard1 = UIStoryboard.FromName("Main", NSBundle.MainBundle);
            YeniEtkinlikOlusturBaseVC controller = UIStoryboard1.InstantiateViewController("YeniEtkinlikOlusturBaseVC") as YeniEtkinlikOlusturBaseVC;
            controller.ModalPresentationStyle = UIModalPresentationStyle.FullScreen;
            this.PresentViewController(controller, true, null);
        }

        private void YeniEtkinlikOlusturButton_TouchUpInside(object sender, EventArgs e)
        {
            if (YeniEtkinlikSubButton.Alpha == 0)
            {
                ShowSubButtons();
                HoverView.Hidden = false;
            }
            else
            {
                HideSubButtons();
                HoverView.Hidden = true;
            }
        }
        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            HideSubButtons();
            ArkaHoverOlustur();
            EtkinligeKatilSubButton.ContentEdgeInsets = new UIEdgeInsets(4,4,4,4);
            SetRounded(YeniEtkinlikSubButton);
            SetRounded(YeniEtkinlikSubLabel);
            SetRounded(EtkinligeKatilSubButton);
            SetRounded(EtkinligeKatilSubLabel);
            SetUserPhoto();
        }
        void SetRounded(UIView GelenView)
        {
            GelenView.Layer.CornerRadius = GelenView.Frame.Height / 2;
            GelenView.ClipsToBounds = true;
            GelenView.Layer.ShadowColor = UIColor.DarkGray.CGColor;
            GelenView.Layer.ShadowOpacity = 1.0f;
            GelenView.Layer.ShadowRadius = GelenView.Bounds.Height / 2;
            GelenView.Layer.ShadowOffset = new System.Drawing.SizeF(0f, 2f);
        }
        void ButtonTasarimlariniAyarla(UIButton GelenButton)
        {
            GelenButton.Layer.CornerRadius = GelenButton.Bounds.Height / 2;
            GelenButton.ClipsToBounds = true;
            GelenButton.ContentEdgeInsets = new UIEdgeInsets(10, 10, 10, 10);
            var ButtonImage = GelenButton.ImageView.Image;
            var ButtonImageTintImage = ButtonImage.ImageWithRenderingMode(UIImageRenderingMode.AlwaysTemplate);
            GelenButton.SetImage(ButtonImageTintImage, UIControlState.Normal);
            GelenButton.TintColor = UIColor.FromRGB(48, 79, 254);
            GelenButton.Layer.MasksToBounds = false;
            GelenButton.Layer.ShadowColor = UIColor.DarkGray.CGColor;
            GelenButton.Layer.ShadowOpacity = 1.0f;
            GelenButton.Layer.ShadowRadius = GelenButton.Bounds.Height / 2;
            GelenButton.Layer.ShadowOffset = new System.Drawing.SizeF(0f, 2f);
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
                        ImageService.Instance.LoadUrl(MemberInfo1.profile_photo).Into(KullaniciFoto);
                    });
                }
            })).Start();
        }
        void HideSubButtons()
        {
            UIView.Animate(0.1, 0, UIViewAnimationOptions.CurveLinear, () =>
            {
                EtkinligeKatilSubButton.Alpha = 0f;
                EtkinligeKatilSubLabel.Alpha = 0f;
            }, null);

            UIView.Animate(0.2, 0, UIViewAnimationOptions.CurveLinear, () =>
            {
                YeniEtkinlikSubButton.Alpha = 0f;
                YeniEtkinlikSubLabel.Alpha = 0f;
            }, null);

        }
        void ShowSubButtons()
        {
            UIView.Animate(0.1, 0, UIViewAnimationOptions.CurveLinear,() =>
            {
                YeniEtkinlikSubButton.Alpha = 1f;
                YeniEtkinlikSubLabel.Alpha = 1f;
            }, null);

            UIView.Animate(0.2, 0, UIViewAnimationOptions.CurveLinear, () =>
            {
                EtkinligeKatilSubButton.Alpha = 1f;
                EtkinligeKatilSubLabel.Alpha = 1f;
            }, null);
        }
        UIView HoverView;
        void ArkaHoverOlustur()
        {
            HoverView = new UIView();
            HoverView.Frame = new CoreGraphics.CGRect(0, 0, UIScreen.MainScreen.Bounds.Width, UIScreen.MainScreen.Bounds.Height);
            HoverView.BackgroundColor = UIColor.Black.ColorWithAlpha(0.5f);
            HoverView.Hidden = true;
            this.View.InsertSubview(HoverView,3);
        }
    }
}