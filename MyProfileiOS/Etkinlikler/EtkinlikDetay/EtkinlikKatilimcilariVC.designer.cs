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
    [Register ("EtkinlikKatilimcilariVC")]
    partial class EtkinlikKatilimcilariVC
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton GeriButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITableView Tabloo { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (GeriButton != null) {
                GeriButton.Dispose ();
                GeriButton = null;
            }

            if (Tabloo != null) {
                Tabloo.Dispose ();
                Tabloo = null;
            }
        }
    }
}