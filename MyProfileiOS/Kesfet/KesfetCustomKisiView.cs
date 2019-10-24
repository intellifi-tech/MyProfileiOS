using FFImageLoading;
using Foundation;
using MyProfileiOS.WebServiceHelper;
using ObjCRuntime;
using System;
using System.Collections.Generic;
using UIKit;
using static MyProfileiOS.KesfetViewController;

namespace MyProfileiOS
{
    public partial class KesfetCustomKisiView : UIView
    {
        public List<TakipEttiklerim_RootObject> TakipEttiklerim;
        KesfetViewController KesfetViewController1;
        NearbyUserCoordinate GelenModel;
        public KesfetCustomKisiView (IntPtr handle) : base (handle)
        {
        }
        public static KesfetCustomKisiView Create(NearbyUserCoordinate MapDataModel11, List<TakipEttiklerim_RootObject> Modell, KesfetViewController KesfetViewController2)
        {
            var arr = NSBundle.MainBundle.LoadNib("KesfetCustomKisiView", null, null);
            var v = Runtime.GetNSObject<KesfetCustomKisiView>(arr.ValueAt(0));
            v.KesfetViewController1 = KesfetViewController2;
            v.TakipEttiklerim = Modell;
            v.GelenModel = MapDataModel11;
            //  v.Duzenle();
            return v;
        }

        public override void LayoutSubviews()
        {
            base.LayoutSubviews();
            ArkaHazne.Layer.CornerRadius = 10f;
            ArkaHazne.ClipsToBounds = true;
            ArkaHazne.Layer.MasksToBounds = false;
            ArkaHazne.Layer.CornerRadius = 10;
            ArkaHazne.Layer.ShadowColor = UIColor.DarkGray.CGColor;
            ArkaHazne.Layer.ShadowOpacity = 1.0f;
            ArkaHazne.Layer.ShadowRadius = 6.0f;
            ArkaHazne.Layer.ShadowOffset = new System.Drawing.SizeF(0f, 2f);
            ProfileFoto.ClipsToBounds = true;
            ProfileFoto.Layer.CornerRadius = ProfileFoto.Frame.Height / 2;
            GetUserImage(GelenModel.user.profile_photo, GelenModel.user.cover_photo, ProfileFoto, CoverPhoto);

            AdSoyadLabel.Text = GelenModel.user.name + " " + GelenModel.user.surname;
            TitleLabel.Text = GelenModel.user.title;

            var TakipEttiklerimArasindaVarmi = TakipEttiklerim.FindAll(item2 => item2.to_user_id == GelenModel.user.id);
            if (TakipEttiklerimArasindaVarmi.Count > 0)
            {
                TakipEtButton.SetTitle("Takip",UIControlState.Normal);
            }
            else
            {
                TakipEtButton.SetTitle("Takip Et", UIControlState.Normal);
            }
        }

        void GetUserImage(string pppath, string cppath,UIImageView PPIMW,UIImageView CPIMW)
        {
            new System.Threading.Thread(new System.Threading.ThreadStart(delegate
            {
                InvokeOnMainThread(delegate () {

                    ImageService.Instance.LoadUrl(cppath).Into(CPIMW);

                    ImageService.Instance.LoadUrl(pppath).Into(PPIMW);
                });
            })).Start();
        }
    }
}