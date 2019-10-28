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
    [Register ("ProfilBilgiGuncelle")]
    partial class ProfilBilgiGuncelle
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton GeriButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton KaydetButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIScrollView ScrollVieww { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (GeriButton != null) {
                GeriButton.Dispose ();
                GeriButton = null;
            }

            if (KaydetButton != null) {
                KaydetButton.Dispose ();
                KaydetButton = null;
            }

            if (ScrollVieww != null) {
                ScrollVieww.Dispose ();
                ScrollVieww = null;
            }
        }
    }
}