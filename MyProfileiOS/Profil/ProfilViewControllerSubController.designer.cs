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
    [Register ("ProfilViewControllerSubController")]
    partial class ProfilViewControllerSubController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIView ContentViewww { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIView KapakEkleHazne { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView KapakEkleIcon { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIView ProfilDuzenleHazne { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView ProfilDuzenleIcon { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton ProfilFotoDegistirButton { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (ContentViewww != null) {
                ContentViewww.Dispose ();
                ContentViewww = null;
            }

            if (KapakEkleHazne != null) {
                KapakEkleHazne.Dispose ();
                KapakEkleHazne = null;
            }

            if (KapakEkleIcon != null) {
                KapakEkleIcon.Dispose ();
                KapakEkleIcon = null;
            }

            if (ProfilDuzenleHazne != null) {
                ProfilDuzenleHazne.Dispose ();
                ProfilDuzenleHazne = null;
            }

            if (ProfilDuzenleIcon != null) {
                ProfilDuzenleIcon.Dispose ();
                ProfilDuzenleIcon = null;
            }

            if (ProfilFotoDegistirButton != null) {
                ProfilFotoDegistirButton.Dispose ();
                ProfilFotoDegistirButton = null;
            }
        }
    }
}