﻿// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;

namespace MyProfileiOS
{
    [Register ("EtkinlikCustomCardView")]
    partial class EtkinlikCustomCardView
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel AciklamaLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView CikisSaatiIconn { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel CikisSaatiText { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView EtkinlikFoto { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView GirisSaatiIcon { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView KatilimciIcon { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel KatilimciText { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (AciklamaLabel != null) {
                AciklamaLabel.Dispose ();
                AciklamaLabel = null;
            }

            if (CikisSaatiIconn != null) {
                CikisSaatiIconn.Dispose ();
                CikisSaatiIconn = null;
            }

            if (CikisSaatiText != null) {
                CikisSaatiText.Dispose ();
                CikisSaatiText = null;
            }

            if (EtkinlikFoto != null) {
                EtkinlikFoto.Dispose ();
                EtkinlikFoto = null;
            }

            if (GirisSaatiIcon != null) {
                GirisSaatiIcon.Dispose ();
                GirisSaatiIcon = null;
            }

            if (KatilimciIcon != null) {
                KatilimciIcon.Dispose ();
                KatilimciIcon = null;
            }

            if (KatilimciText != null) {
                KatilimciText.Dispose ();
                KatilimciText = null;
            }
        }
    }
}