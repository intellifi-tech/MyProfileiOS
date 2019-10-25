// WARNING
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
    [Register ("MesajlarKisiListItem")]
    partial class MesajlarKisiListItem
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel Badgee { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel LastMessage { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel Timee { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView UserImage { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel UserName { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (Badgee != null) {
                Badgee.Dispose ();
                Badgee = null;
            }

            if (LastMessage != null) {
                LastMessage.Dispose ();
                LastMessage = null;
            }

            if (Timee != null) {
                Timee.Dispose ();
                Timee = null;
            }

            if (UserImage != null) {
                UserImage.Dispose ();
                UserImage = null;
            }

            if (UserName != null) {
                UserName.Dispose ();
                UserName = null;
            }
        }
    }
}