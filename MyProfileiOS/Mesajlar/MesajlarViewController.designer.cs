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
    [Register ("MesajlarViewController")]
    partial class MesajlarViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITableView MesajFriendsTableView { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView ProfilFoto { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (MesajFriendsTableView != null) {
                MesajFriendsTableView.Dispose ();
                MesajFriendsTableView = null;
            }

            if (ProfilFoto != null) {
                ProfilFoto.Dispose ();
                ProfilFoto = null;
            }
        }
    }
}