// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;

namespace MyProfileiOS.Etkinlikler.EtkinlikDetay
{
    [Register ("KatilimciCustomItem")]
    partial class KatilimciCustomItem
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel UserName { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView UserPhoto { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel UserTitle { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (UserName != null) {
                UserName.Dispose ();
                UserName = null;
            }

            if (UserPhoto != null) {
                UserPhoto.Dispose ();
                UserPhoto = null;
            }

            if (UserTitle != null) {
                UserTitle.Dispose ();
                UserTitle = null;
            }
        }
    }
}