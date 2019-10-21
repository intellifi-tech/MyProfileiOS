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
    [Register ("EtkinliklerViewController")]
    partial class EtkinliklerViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView KullaniciFoto { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIView PgerHazneee { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton YeniEtkinlikOlusturButton { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (KullaniciFoto != null) {
                KullaniciFoto.Dispose ();
                KullaniciFoto = null;
            }

            if (PgerHazneee != null) {
                PgerHazneee.Dispose ();
                PgerHazneee = null;
            }

            if (YeniEtkinlikOlusturButton != null) {
                YeniEtkinlikOlusturButton.Dispose ();
                YeniEtkinlikOlusturButton = null;
            }
        }
    }
}