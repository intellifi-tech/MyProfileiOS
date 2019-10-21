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
    [Register ("ProfilViewKontroller")]
    partial class ProfilViewKontroller
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView ProfilFotoHeader { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIScrollView ProfilScroll { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton TakipButton { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (ProfilFotoHeader != null) {
                ProfilFotoHeader.Dispose ();
                ProfilFotoHeader = null;
            }

            if (ProfilScroll != null) {
                ProfilScroll.Dispose ();
                ProfilScroll = null;
            }

            if (TakipButton != null) {
                TakipButton.Dispose ();
                TakipButton = null;
            }
        }
    }
}