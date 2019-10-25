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
    [Register ("KisiselBilgilerEtkinlikler")]
    partial class KisiselBilgilerEtkinlikler
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITableView EtkinliklerTablo { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (EtkinliklerTablo != null) {
                EtkinliklerTablo.Dispose ();
                EtkinliklerTablo = null;
            }
        }
    }
}