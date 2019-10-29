// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace MyProfileiOS
{
    [Register ("GaleriBuyutVC")]
    partial class GaleriBuyutVC
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton BegenButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton BegenButton2 { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel BegenenKisiSayisiLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton GeriButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton ProfilFotoYapButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIScrollView scrollView { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton SilButton { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (BegenButton != null) {
                BegenButton.Dispose ();
                BegenButton = null;
            }

            if (BegenButton2 != null) {
                BegenButton2.Dispose ();
                BegenButton2 = null;
            }

            if (BegenenKisiSayisiLabel != null) {
                BegenenKisiSayisiLabel.Dispose ();
                BegenenKisiSayisiLabel = null;
            }

            if (GeriButton != null) {
                GeriButton.Dispose ();
                GeriButton = null;
            }

            if (ProfilFotoYapButton != null) {
                ProfilFotoYapButton.Dispose ();
                ProfilFotoYapButton = null;
            }

            if (scrollView != null) {
                scrollView.Dispose ();
                scrollView = null;
            }

            if (SilButton != null) {
                SilButton.Dispose ();
                SilButton = null;
            }
        }
    }
}