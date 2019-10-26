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
    [Register ("YorumCustomCell")]
    partial class YorumCustomCell
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIView ArkaHazne { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel UserName { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView UserPhoto { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel UserYorum { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (ArkaHazne != null) {
                ArkaHazne.Dispose ();
                ArkaHazne = null;
            }

            if (UserName != null) {
                UserName.Dispose ();
                UserName = null;
            }

            if (UserPhoto != null) {
                UserPhoto.Dispose ();
                UserPhoto = null;
            }

            if (UserYorum != null) {
                UserYorum.Dispose ();
                UserYorum = null;
            }
        }
    }
}