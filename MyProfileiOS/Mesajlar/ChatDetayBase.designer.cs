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
    [Register ("ChatDetayBase")]
    partial class ChatDetayBase
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.NSLayoutConstraint BottomKisitlama { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton GeriButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton GonderButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField MesajInput { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITableView MesajTablo { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIView MesajYazHazne { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView UserImage { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel UserName { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (BottomKisitlama != null) {
                BottomKisitlama.Dispose ();
                BottomKisitlama = null;
            }

            if (GeriButton != null) {
                GeriButton.Dispose ();
                GeriButton = null;
            }

            if (GonderButton != null) {
                GonderButton.Dispose ();
                GonderButton = null;
            }

            if (MesajInput != null) {
                MesajInput.Dispose ();
                MesajInput = null;
            }

            if (MesajTablo != null) {
                MesajTablo.Dispose ();
                MesajTablo = null;
            }

            if (MesajYazHazne != null) {
                MesajYazHazne.Dispose ();
                MesajYazHazne = null;
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