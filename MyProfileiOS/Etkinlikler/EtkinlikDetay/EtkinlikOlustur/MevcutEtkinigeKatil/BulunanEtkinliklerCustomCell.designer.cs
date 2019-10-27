// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;

namespace MyProfileiOS.Etkinlikler.EtkinlikDetay.EtkinlikOlustur.MevcutEtkinigeKatil
{
    [Register ("BulunanEtkinliklerCustomCell")]
    partial class BulunanEtkinliklerCustomCell
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel EtkinlikAdi { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView EtkinlikPhoto { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel KatilimciSayisi { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (EtkinlikAdi != null) {
                EtkinlikAdi.Dispose ();
                EtkinlikAdi = null;
            }

            if (EtkinlikPhoto != null) {
                EtkinlikPhoto.Dispose ();
                EtkinlikPhoto = null;
            }

            if (KatilimciSayisi != null) {
                KatilimciSayisi.Dispose ();
                KatilimciSayisi = null;
            }
        }
    }
}