using CoreGraphics;
using Foundation;
using System;
using System.Collections.Generic;
using UIKit;

namespace MyProfileiOS
{
    public partial class KesfetViewController : UIViewController
    {
        List<KisilerDataModel> KisilerDataModel1 = new List<KisilerDataModel>();
        KesfetCustomKisiView[] KisilerDizi = new KesfetCustomKisiView[0];
        public KesfetViewController (IntPtr handle) : base (handle)
        {
        }
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            GizleGosterButton.TouchUpInside += GizleGosterButton_TouchUpInside;
        }


        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);
            KullaniciFoto.Layer.CornerRadius = KullaniciFoto.Frame.Height / 2;
            KullaniciFoto.ClipsToBounds = true;
            SliderVieww.Transform = CGAffineTransform.MakeRotation(3.14159f * 90 / 180f);
            var IconImage = UIImage.FromBundle("Images/menu.png");
            var TintImage = IconImage.ImageWithRenderingMode(UIImageRenderingMode.AlwaysTemplate);
            FillDataModel();
            Acik = KisilerScroll.Center;
            Kapali = new CGPoint(KisilerScroll.Center.X, UIScreen.MainScreen.Bounds.Bottom);
            AcikButton = GizleGosterButton.Center;
            KapaliButton = new CGPoint(GizleGosterButton.Center.X, UIScreen.MainScreen.Bounds.Bottom - (this.TabBarController.TabBar.Bounds.Height + 50));
            ButtonTasarimlariniAyarla(GizleGosterButton);
            ButtonTasarimlariniAyarla(BenimLokasyonumButton);
        }

        void ButtonTasarimlariniAyarla(UIButton GelenButton)
        {
            GelenButton.Layer.CornerRadius = GelenButton.Bounds.Height / 2;
            GelenButton.ClipsToBounds = true;
            GelenButton.ContentEdgeInsets = new UIEdgeInsets(10,10,10,10);
            var ButtonImage = GelenButton.ImageView.Image;
            var ButtonImageTintImage = ButtonImage.ImageWithRenderingMode(UIImageRenderingMode.AlwaysTemplate);
            GelenButton.SetImage(ButtonImageTintImage, UIControlState.Normal);
            GelenButton.TintColor = UIColor.FromRGB(48, 79, 254);
            GelenButton.Layer.MasksToBounds = false;
            GelenButton.Layer.ShadowColor = UIColor.DarkGray.CGColor;
            GelenButton.Layer.ShadowOpacity = 1.0f;
            GelenButton.Layer.ShadowRadius = GelenButton.Bounds.Height / 2;
            GelenButton.Layer.ShadowOffset = new System.Drawing.SizeF(0f, 2f);
        }
        bool AcKapat = false;
        CGPoint Acik, Kapali, AcikButton,KapaliButton;
        private void GizleGosterButton_TouchUpInside(object sender, EventArgs e)
        {
            if (!AcKapat) //Kapat
            {
                new System.Threading.Thread(new System.Threading.ThreadStart(delegate
                {
                    InvokeOnMainThread(delegate
                    {
                        UIView.Animate(0.3, 0, UIViewAnimationOptions.CurveEaseInOut,
                            () =>
                            {
                                KisilerScroll.Center = Kapali;
                                //   new CGPoint(UIScreen.MainScreen.Bounds.Right - Listeislemleributon.Frame.Width / 2, Listeislemleributon.Center.Y);
                            },
                            () =>
                            {
                                KisilerScroll.Center = Kapali;
                            }
                        );
                    });
                })).Start();


               //SlideVerticaly(KisilerScroll, true, false, 1, null);
                
            }
            else//Aç
            {


                new System.Threading.Thread(new System.Threading.ThreadStart(delegate
                {
                    InvokeOnMainThread(delegate
                    {
                        UIView.Animate(0.3, 0, UIViewAnimationOptions.CurveEaseInOut,
                            () =>
                            {
                                KisilerScroll.Center = Acik;
                                //   new CGPoint(UIScreen.MainScreen.Bounds.Right - Listeislemleributon.Frame.Width / 2, Listeislemleributon.Center.Y);
                            },

                            () =>
                            {
                                KisilerScroll.Center = Acik;
                            }
                        );
                    });
                })).Start();



                // SlideVerticaly(KisilerScroll, false, true, 1, null);
            }

            GosterGizleButtonAnimation();
            AcKapat = !AcKapat;
        }
        void GosterGizleButtonAnimation()
        {
            if (!AcKapat) //Kapat
            {
                new System.Threading.Thread(new System.Threading.ThreadStart(delegate
                {
                    InvokeOnMainThread(delegate
                    {
                        UIView.Animate(0.3, 0, UIViewAnimationOptions.CurveEaseInOut,
                            () =>
                            {
                                GizleGosterButton.Center = KapaliButton;
                                //   new CGPoint(UIScreen.MainScreen.Bounds.Right - Listeislemleributon.Frame.Width / 2, Listeislemleributon.Center.Y);
                            },
                            () =>
                            {
                                GizleGosterButton.Center = KapaliButton;
                            }
                        );
                    });
                })).Start();
            }
            else//Aç
            {
                new System.Threading.Thread(new System.Threading.ThreadStart(delegate
                {
                    InvokeOnMainThread(delegate
                    {
                        UIView.Animate(0.3, 0, UIViewAnimationOptions.CurveEaseInOut,
                            () =>
                            {
                                GizleGosterButton.Center = AcikButton;
                                //   new CGPoint(UIScreen.MainScreen.Bounds.Right - Listeislemleributon.Frame.Width / 2, Listeislemleributon.Center.Y);
                            },
                            () =>
                            {
                                GizleGosterButton.Center = AcikButton;
                            }
                        );
                    });
                })).Start();



                // SlideVerticaly(KisilerScroll, false, true, 1, null);
            }
        }
        void FillDataModel()
        {
            for (int i = 0; i < 20; i++)
            {
                KisilerDataModel1.Add(new KisilerDataModel());
            }

            KisilerDizi = new KesfetCustomKisiView[KisilerDataModel1.Count];
            for (int i = 0; i < KisilerDataModel1.Count; i++)
            {
                var NoktaItem = KesfetCustomKisiView.Create(KisilerDataModel1[i], this);
                if (i == 0)
                {
                    NoktaItem.Frame = new CGRect(0, 0, 140f, 180f);
                }
                else
                {
                    NoktaItem.Frame = new CGRect(145f * i, 0, 140f, 180f);
                }

                KisilerScroll.AddSubview(NoktaItem);
                KisilerDizi[i] = NoktaItem;
            }
            KisilerScroll.ContentSize = new CGSize(KisilerDizi[KisilerDizi.Length - 1].Frame.Right, 180f);
            KisilerScroll.ShowsHorizontalScrollIndicator = false;
            KisilerScroll.ShowsVerticalScrollIndicator = false;
            GetRigToLeftScrollView();
        }
        void GetRigToLeftScrollView()
        {
            new System.Threading.Thread(new System.Threading.ThreadStart(delegate
            {
                InvokeOnMainThread(delegate
                {
                    CGPoint pt;
                    pt = KisilerScroll.Center;
                    KisilerScroll.Center = new CGPoint(UIScreen.MainScreen.Bounds.Right + KisilerScroll.Frame.Width / 2, KisilerScroll.Center.Y);
                    UIView.Animate(1, 0, UIViewAnimationOptions.CurveEaseInOut,
                        () =>
                        {
                            KisilerScroll.Center = pt;
                            //   new CGPoint(UIScreen.MainScreen.Bounds.Right - Listeislemleributon.Frame.Width / 2, Listeislemleributon.Center.Y);
                        },

                        () =>
                        {
                            KisilerScroll.Center = pt;
                        }
                    );
                });
            })).Start();
        }
        public class KisilerDataModel
        {
            public int ID { get; set; }
        }
    }
}
