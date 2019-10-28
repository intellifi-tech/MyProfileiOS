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
    [Register ("ProfilBilgiGuncelleSubVC")]
    partial class ProfilBilgiGuncelleSubVC
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField AdText { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton DeneyimEkleBut { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton DogumTarihiButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextView HakkindaText { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton SektorButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField SirketText { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField SoyadText { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField UnvanText { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (AdText != null) {
                AdText.Dispose ();
                AdText = null;
            }

            if (DeneyimEkleBut != null) {
                DeneyimEkleBut.Dispose ();
                DeneyimEkleBut = null;
            }

            if (DogumTarihiButton != null) {
                DogumTarihiButton.Dispose ();
                DogumTarihiButton = null;
            }

            if (HakkindaText != null) {
                HakkindaText.Dispose ();
                HakkindaText = null;
            }

            if (SektorButton != null) {
                SektorButton.Dispose ();
                SektorButton = null;
            }

            if (SirketText != null) {
                SirketText.Dispose ();
                SirketText = null;
            }

            if (SoyadText != null) {
                SoyadText.Dispose ();
                SoyadText = null;
            }

            if (UnvanText != null) {
                UnvanText.Dispose ();
                UnvanText = null;
            }
        }
    }
}