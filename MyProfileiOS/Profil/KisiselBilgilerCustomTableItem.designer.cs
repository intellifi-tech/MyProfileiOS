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
    [Register ("KisiselBilgilerCustomTableItem")]
    partial class KisiselBilgilerCustomTableItem
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel BaslikText { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel IcerikText { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView Iconn { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (BaslikText != null) {
                BaslikText.Dispose ();
                BaslikText = null;
            }

            if (IcerikText != null) {
                IcerikText.Dispose ();
                IcerikText = null;
            }

            if (Iconn != null) {
                Iconn.Dispose ();
                Iconn = null;
            }
        }
    }
}