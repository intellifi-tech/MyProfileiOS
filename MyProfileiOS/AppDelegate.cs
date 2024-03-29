﻿using Foundation;
using Google.Maps;
using System;
using UIKit;

namespace MyProfileiOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the
    // User Interface of the application, as well as listening (and optionally responding) to application events from iOS.
    [Register("AppDelegate")]
    public class AppDelegate : UIApplicationDelegate
    {
        // class-level declarations
        const string MapsApiKey = "AIzaSyD6F8c4Mf4a4lMLj_XLYvLKnBd_SP4_jVE";
        public override UIWindow Window
        {
            get;
            set;
        }

        #region Custom Method
        public UIStoryboard MainStoryboard
        {
            get { return UIStoryboard.FromName("Main", NSBundle.MainBundle); }
        }
        
        public UIViewController GetViewController(UIStoryboard storyboard, string viewControllerName)
        {
            return storyboard.InstantiateViewController(viewControllerName);
        }

        public void SetRootViewController(UIViewController rootViewController, bool animate)
        {
            if (animate)
            {
                var transitionType = UIViewAnimationOptions.TransitionFlipFromRight;

                Window.RootViewController = rootViewController;
                UIView.Transition(Window, 0.5, transitionType,
                                  () => Window.RootViewController = rootViewController,
                                  null);
            }
            else
            {
                Window.RootViewController = rootViewController;
            }
        }
        public static UIWindow window = new UIWindow(UIScreen.MainScreen.Bounds);
        public static UINavigationController navController;
        public void SetLoginNavigationController()
        {
            var AppIntroController = GetViewController(MainStoryboard, "LoginNavigationViewController") as LoginNavigationViewController;
            SetRootViewController(AppIntroController, true);
        }

        public void SetAnaMainController()
        {
            var AppIntroController = GetViewController(MainStoryboard, "AnaMainTabBarViewController") as AnaMainTabBarViewController;
            SetRootViewController(AppIntroController, true);
        }
        public static nfloat GetStatusBarHeight()
        {
            var statusBarSize = UIApplication.SharedApplication.StatusBarFrame.Size;
            return (nfloat)Math.Min(statusBarSize.Height, statusBarSize.Width);
        }
        #endregion



        public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
        {
            // Override point for customization after application launch.
            // If not required for your application you can safely delete this method
            MapServices.ProvideAPIKey(MapsApiKey);
            return true;
        }

        public override void OnResignActivation(UIApplication application)
        {
            // Invoked when the application is about to move from active to inactive state.
            // This can occur for certain types of temporary interruptions (such as an incoming phone call or SMS message)
            // or when the user quits the application and it begins the transition to the background state.
            // Games should use this method to pause the game.
        }

        public override void DidEnterBackground(UIApplication application)
        {
            // Use this method to release shared resources, save user data, invalidate timers and store the application state.
            // If your application supports background exection this method is called instead of WillTerminate when the user quits.
        }

        public override void WillEnterForeground(UIApplication application)
        {
            // Called as part of the transiton from background to active state.
            // Here you can undo many of the changes made on entering the background.
        }

        public override void OnActivated(UIApplication application)
        {
            // Restart any tasks that were paused (or not yet started) while the application was inactive.
            // If the application was previously in the background, optionally refresh the user interface.
        }

        public override void WillTerminate(UIApplication application)
        {
            // Called when the application is about to terminate. Save data, if needed. See also DidEnterBackground.
        }
    }
}

