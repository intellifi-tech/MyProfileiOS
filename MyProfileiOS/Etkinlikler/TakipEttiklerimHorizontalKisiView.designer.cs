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
    [Register ("TakipEttiklerimHorizontalKisiView")]
    partial class TakipEttiklerimHorizontalKisiView
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView ProfileFoto { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel UserNametxt { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (ProfileFoto != null) {
                ProfileFoto.Dispose ();
                ProfileFoto = null;
            }

            if (UserNametxt != null) {
                UserNametxt.Dispose ();
                UserNametxt = null;
            }
        }
    }
}