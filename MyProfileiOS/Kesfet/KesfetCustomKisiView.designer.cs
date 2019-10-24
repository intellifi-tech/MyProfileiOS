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
    [Register ("KesfetCustomKisiView")]
    partial class KesfetCustomKisiView
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel AdSoyadLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIView ArkaHazne { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView CoverPhoto { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView ProfileFoto { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton TakipEtButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel TitleLabel { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (AdSoyadLabel != null) {
                AdSoyadLabel.Dispose ();
                AdSoyadLabel = null;
            }

            if (ArkaHazne != null) {
                ArkaHazne.Dispose ();
                ArkaHazne = null;
            }

            if (CoverPhoto != null) {
                CoverPhoto.Dispose ();
                CoverPhoto = null;
            }

            if (ProfileFoto != null) {
                ProfileFoto.Dispose ();
                ProfileFoto = null;
            }

            if (TakipEtButton != null) {
                TakipEtButton.Dispose ();
                TakipEtButton = null;
            }

            if (TitleLabel != null) {
                TitleLabel.Dispose ();
                TitleLabel = null;
            }
        }
    }
}