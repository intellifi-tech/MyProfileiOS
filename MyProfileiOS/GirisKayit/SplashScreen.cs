using Foundation;
using MyProfileiOS.DataBasee;
using System;
using System.Threading.Tasks;
using UIKit;

namespace MyProfileiOS
{
    public partial class SplashScreen : UIViewController
    {
        public SplashScreen (IntPtr handle) : base (handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            BekletLoginAc();
        }

        void BekletLoginAc()
        {
            new DataBase();
            new System.Threading.Thread(new System.Threading.ThreadStart(async delegate
            {
               await Task.Run(async () => {
                    await Task.Delay(600);
                });
                InvokeOnMainThread(delegate
                {
                    var Kullanici = DataBase.USER_INFO_GETIR();

                    if (Kullanici.Count > 0)
                    {
                        var appDelegate = UIApplication.SharedApplication.Delegate as AppDelegate;
                        appDelegate.SetAnaMainController();
                    }
                    else
                    {
                        var appDelegate = UIApplication.SharedApplication.Delegate as AppDelegate;
                        appDelegate.SetLoginNavigationController();
                    }
                });
            })).Start();
        }
    }
}