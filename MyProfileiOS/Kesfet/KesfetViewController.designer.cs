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
    [Register ("KesfetViewController")]
    partial class KesfetViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton BenimLokasyonumButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton GizleGosterButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIScrollView KisilerScroll { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView KullaniciFoto { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        MapKit.MKMapView MapVieww { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UISlider SliderVieww { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel UzaklikLabel { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (BenimLokasyonumButton != null) {
                BenimLokasyonumButton.Dispose ();
                BenimLokasyonumButton = null;
            }

            if (GizleGosterButton != null) {
                GizleGosterButton.Dispose ();
                GizleGosterButton = null;
            }

            if (KisilerScroll != null) {
                KisilerScroll.Dispose ();
                KisilerScroll = null;
            }

            if (KullaniciFoto != null) {
                KullaniciFoto.Dispose ();
                KullaniciFoto = null;
            }

            if (MapVieww != null) {
                MapVieww.Dispose ();
                MapVieww = null;
            }

            if (SliderVieww != null) {
                SliderVieww.Dispose ();
                SliderVieww = null;
            }

            if (UzaklikLabel != null) {
                UzaklikLabel.Dispose ();
                UzaklikLabel = null;
            }
        }
    }
}