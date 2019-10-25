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
    [Register ("KisiselBilgilerFotografGaleri")]
    partial class KisiselBilgilerFotografGaleri
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UICollectionView CollView { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (CollView != null) {
                CollView.Dispose ();
                CollView = null;
            }
        }
    }
}