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
    [Register ("TakipEttiklerimEtkinliklerViewController")]
    partial class TakipEttiklerimEtkinliklerViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITableView EtkinlikTableView { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIScrollView HorizontalTakipKisiScroll { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (EtkinlikTableView != null) {
                EtkinlikTableView.Dispose ();
                EtkinlikTableView = null;
            }

            if (HorizontalTakipKisiScroll != null) {
                HorizontalTakipKisiScroll.Dispose ();
                HorizontalTakipKisiScroll = null;
            }
        }
    }
}