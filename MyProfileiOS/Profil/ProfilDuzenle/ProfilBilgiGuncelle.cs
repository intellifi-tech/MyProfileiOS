using Foundation;
using System;
using System.Threading.Tasks;
using UIKit;

namespace MyProfileiOS
{
    public partial class ProfilBilgiGuncelle : UIViewController
    {
        ProfilBilgiGuncelleSubVC PublicProfileVC1;
        public ProfilBilgiGuncelle (IntPtr handle) : base (handle)
        {
        }
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            GeriButton.TouchUpInside += GeriButton_TouchUpInside;
            KaydetButton.TouchUpInside += KaydetButton_TouchUpInside;
        }

        private void KaydetButton_TouchUpInside(object sender, EventArgs e)
        {
            
        }

        private void GeriButton_TouchUpInside(object sender, EventArgs e)
        {
            Closee();
        }

        bool Actimi = false;
        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);
            if (!Actimi)
            {
                var MainStoryBoard = UIStoryboard.FromName("Main", NSBundle.MainBundle);
                PublicProfileVC1 = MainStoryBoard.InstantiateViewController("ProfilBilgiGuncelleSubVC") as ProfilBilgiGuncelleSubVC;
                PublicProfileVC1.GelenBase = this;
                var viewController = PublicProfileVC1;
                viewController.View.Frame = new CoreGraphics.CGRect(0, 0, UIScreen.MainScreen.Bounds.Width, 730);
                viewController.WillMoveToParentViewController(this);
                ScrollVieww.AddSubview(viewController.View);
                this.AddChildViewController(viewController);
                viewController.DidMoveToParentViewController(this);
                ScrollVieww.ContentSize = new CoreGraphics.CGSize(UIScreen.MainScreen.Bounds.Width, 730);

                Actimi = true;
            }
        }
        public void Closee()
        {
            this.DismissViewController(true, null);
        }
        public void ScrollTopp()
        {
            Task.Run(async delegate () {
                await Task.Delay(1000);
                InvokeOnMainThread(delegate () {
                    ScrollVieww.SetContentOffset(new CoreGraphics.CGPoint(0, ScrollVieww.ContentInset.Top), true);
                    var newh = PublicProfileVC1.GetMaxHeigt();
                    ScrollVieww.ContentSize = new CoreGraphics.CGSize(UIScreen.MainScreen.Bounds.Width, newh);
                });

            });
        }
      
    }
}