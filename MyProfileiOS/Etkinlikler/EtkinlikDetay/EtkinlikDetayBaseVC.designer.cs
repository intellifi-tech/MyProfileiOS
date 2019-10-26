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
    [Register ("EtkinlikDetayBaseVC")]
    partial class EtkinlikDetayBaseVC
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton EtkinlikOlusturButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton GeriButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton KatilimcilarButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton PaylasButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIScrollView ScrollVieww { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (EtkinlikOlusturButton != null) {
                EtkinlikOlusturButton.Dispose ();
                EtkinlikOlusturButton = null;
            }

            if (GeriButton != null) {
                GeriButton.Dispose ();
                GeriButton = null;
            }

            if (KatilimcilarButton != null) {
                KatilimcilarButton.Dispose ();
                KatilimcilarButton = null;
            }

            if (PaylasButton != null) {
                PaylasButton.Dispose ();
                PaylasButton = null;
            }

            if (ScrollVieww != null) {
                ScrollVieww.Dispose ();
                ScrollVieww = null;
            }
        }
    }
}