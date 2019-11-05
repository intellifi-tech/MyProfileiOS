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
    [Register ("GizlilikAyarlaVC")]
    partial class GizlilikAyarlaVC
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIView DisHanzee { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton GeriButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UISwitch Harita_Toogle { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton KaydetButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UISwitch Mesaj_Toggle { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UISwitch Takip_Toggle { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (DisHanzee != null) {
                DisHanzee.Dispose ();
                DisHanzee = null;
            }

            if (GeriButton != null) {
                GeriButton.Dispose ();
                GeriButton = null;
            }

            if (Harita_Toogle != null) {
                Harita_Toogle.Dispose ();
                Harita_Toogle = null;
            }

            if (KaydetButton != null) {
                KaydetButton.Dispose ();
                KaydetButton = null;
            }

            if (Mesaj_Toggle != null) {
                Mesaj_Toggle.Dispose ();
                Mesaj_Toggle = null;
            }

            if (Takip_Toggle != null) {
                Takip_Toggle.Dispose ();
                Takip_Toggle = null;
            }
        }
    }
}