using Foundation;
using System;
using System.Linq;
using UIKit;

namespace MyProfileiOS
{
    public partial class AnaMainTabBarViewController : UITabBarController,IUITabBarControllerDelegate
    {
        public AnaMainTabBarViewController (IntPtr handle) : base (handle)
        {
        }
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            this.Delegate = new MyTabbarControllerDelegate(this);
        }
    }
    public class MyTabbarControllerDelegate : UITabBarControllerDelegate
    {
        UITabBarController TabbarController;
        public MyTabbarControllerDelegate(UITabBarController tabbarController)
        {
            TabbarController = tabbarController;
        }

        public override bool ShouldSelectViewController(UITabBarController tabBarController, UIViewController viewController)
        {
            var controllers = tabBarController.ViewControllers.ToList();
            int index = controllers.IndexOf(viewController);
            // You can change this condition to determine which tab could be selected and which tab should present another 
            //modal view controller
            // Here I disable the second tab's default displaying
            if (index == 3)
            {
                SecilenKullanici.UserID = null;
            //tabBarController.PresentViewController(viewController, true, null);
            //    return false;
            }
            return true;
        }
    }
}