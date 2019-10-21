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
    [Register ("LoginViewController")]
    partial class LoginViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIView EpostaHazne { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView EpostaIcon { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField EpostaText { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton GirisButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIView SifreHazne { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView SifreIcon { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField SifreText { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (EpostaHazne != null) {
                EpostaHazne.Dispose ();
                EpostaHazne = null;
            }

            if (EpostaIcon != null) {
                EpostaIcon.Dispose ();
                EpostaIcon = null;
            }

            if (EpostaText != null) {
                EpostaText.Dispose ();
                EpostaText = null;
            }

            if (GirisButton != null) {
                GirisButton.Dispose ();
                GirisButton = null;
            }

            if (SifreHazne != null) {
                SifreHazne.Dispose ();
                SifreHazne = null;
            }

            if (SifreIcon != null) {
                SifreIcon.Dispose ();
                SifreIcon = null;
            }

            if (SifreText != null) {
                SifreText.Dispose ();
                SifreText = null;
            }
        }
    }
}