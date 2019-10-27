using CoreGraphics;
using CoreLocation;
using FFImageLoading;
using Foundation;
using Google.Maps;
using MyProfileiOS.DataBasee;
using MyProfileiOS.WebServiceHelper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UIKit;

namespace MyProfileiOS
{
    public partial class KesfetViewController : UIViewController
    {

        KesfetCustomKisiView[] KisilerDizi = new KesfetCustomKisiView[0];
        nfloat VarsayilaTop1;
        MapView mapView;
        List<NearbyUserCoordinate> MapDataModel1 = new List<NearbyUserCoordinate>();
        public List<TakipEttiklerim_RootObject> TakipEttiklerimListe = new List<TakipEttiklerim_RootObject>();
        string MeId;
        public KesfetViewController(IntPtr handle) : base(handle)
        {
        }

        #region Yeni Hiyarsi
        
        #region Life
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            GizleGosterButton.TouchUpInside += GizleGosterButton_TouchUpInside;
            BenimLokasyonumButton.TouchUpInside += BenimLokasyonumButton_TouchUpInside;
        }
        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            MeId = DataBase.USER_INFO_GETIR()[0].id;
        }
        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);
            KullaniciFoto.Layer.CornerRadius = KullaniciFoto.Frame.Height / 2;
            KullaniciFoto.ClipsToBounds = true;
            SliderVieww.Transform = CGAffineTransform.MakeRotation(3.14159f * -90 / 180f);
            SliderVieww.MinValue = 1;
            SliderVieww.MaxValue = 500;
            SliderVieww.Value = 100;
            var IconImage = UIImage.FromBundle("Images/menu.png");
            var TintImage = IconImage.ImageWithRenderingMode(UIImageRenderingMode.AlwaysTemplate);
            Acik = KisilerScroll.Center;
            Kapali = new CGPoint(KisilerScroll.Center.X, UIScreen.MainScreen.Bounds.Bottom);
            AcikButton = GizleGosterButton.Center;
            KapaliButton = new CGPoint(GizleGosterButton.Center.X, UIScreen.MainScreen.Bounds.Bottom - (this.TabBarController.TabBar.Bounds.Height + 50));
            ButtonTasarimlariniAyarla(GizleGosterButton);
            ButtonTasarimlariniAyarla(BenimLokasyonumButton);
            VarsayilaTop1 = KisilerScroll.Frame.Y;
            SliderVieww.ValueChanged += SliderVieww_ValueChanged;

            GetMap(MapVieww);
            TakipEttiklerimiGetir();
            SetUserPhoto();
        }

        void SetUserPhoto()
        {
            new System.Threading.Thread(new System.Threading.ThreadStart(delegate
            {
                WebService webService = new WebService();
                var Donus = webService.OkuGetir("user/" + MeId + "/show");
                if (Donus != null)
                {
                    InvokeOnMainThread(delegate () {
                        var MemberInfo1 = Newtonsoft.Json.JsonConvert.DeserializeObject<USER_INFO>(Donus);
                        ImageService.Instance.LoadUrl(MemberInfo1.profile_photo).Into(KullaniciFoto);
                    });
                }
            })).Start();
        }
        #endregion

        #region Button Events
        private void BenimLokasyonumButton_TouchUpInside(object sender, EventArgs e)
        {
            try
            {
                var newCamera = CameraPosition.FromCamera(mapView.MyLocation.Coordinate, 18, mapView.Camera.Bearing + 10, mapView.Camera.ViewingAngle + 10);
                mapView.Animate(newCamera);
            }
            catch 
            {
            }

        }
        private void SliderVieww_ValueChanged(object sender, EventArgs e)
        {
            if (circ != null)
            {
                circ.Radius = SliderVieww.Value / 2;
                var NormalDeger = SliderVieww.Value;
                UzaklikLabel.Text = string.Format("{0}m", Convert.ToInt32(SliderVieww.Value));
                new System.Threading.Thread(new System.Threading.ThreadStart(delegate
                {
                    CemberIcindekileriAyikla();
                    return;
                    if ((NormalDeger % 1) == 0)
                    {
                        CemberIcindekileriAyikla();
                    }
                    else
                    {
                        Console.WriteLine("DEÐERR: " + NormalDeger);
                    }
                    
                })).Start();
            }
        }
        #endregion

        #region Uzak DB KordinatDuzenle
        void UzakDBLokasyonGuncelle(CoreLocation.CLLocationCoordinate2D latLng)
        {
            new System.Threading.Thread(new System.Threading.ThreadStart(delegate
            {
                try
                {
                    var _timer = new System.Threading.Timer((o) =>
                    {
                        try
                        {
                            WebService webService = new WebService();
                            KordinatGonder_RootObject KordinatGonder_RootObject1 = new KordinatGonder_RootObject()
                            {
                                latitude = latLng.Latitude.ToString(System.Globalization.CultureInfo.InvariantCulture),
                                longitude = latLng.Longitude.ToString(System.Globalization.CultureInfo.InvariantCulture)
                            };
                            string jsonString = JsonConvert.SerializeObject(KordinatGonder_RootObject1);
                            var Donus = webService.ServisIslem("user/coordinate/setCoordinates", jsonString);
                        }
                        catch
                        {


                        }

                    }, null, 0, 60000);
                }
                catch
                {
                }

            })).Start();
        }
        #endregion

        #region UI Duzenlemeleri
        void ButtonTasarimlariniAyarla(UIButton GelenButton)
        {
            GelenButton.Layer.CornerRadius = GelenButton.Bounds.Height / 2;
            GelenButton.ClipsToBounds = true;
            GelenButton.ContentEdgeInsets = new UIEdgeInsets(10, 10, 10, 10);
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
        CGPoint Acik, Kapali, AcikButton, KapaliButton;
        private void GizleGosterButton_TouchUpInside(object sender, EventArgs e)
        {

            if (!AcKapat) //Kapat
            {
                UIView.Animate(0.3, () =>
                {
                    var framee = KisilerScroll.Frame;
                    framee.Y = framee.Y + framee.Height;
                    KisilerScroll.Frame = framee;
                });
            }
            else//Aç
            {
                UIView.Animate(0.3, () =>
                {
                    var framee = KisilerScroll.Frame;
                    framee.Y = VarsayilaTop1;
                    KisilerScroll.Frame = framee;
                });
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
        #endregion

        #region Takip Ettiklerimi Getir
        public void TakipEttiklerimiGetir()
        {
            new System.Threading.Thread(new System.Threading.ThreadStart(delegate
            {
                try
                {
                    WebService webService = new WebService();
                    var Donus = webService.ServisIslem("user/followings", "");
                    if (Donus != "Hata")
                    {
                        TakipEttiklerimListe = Newtonsoft.Json.JsonConvert.DeserializeObject<List<TakipEttiklerim_RootObject>>(Donus);
                    }
                }
                catch
                {
                }
            })).Start();
        }

        #endregion

        #region Default Map Init
        public void GetMap(UIView _mapView)
        {
            CameraPosition camera = CameraPosition.FromCamera(37.797865, -122.402526, 6);
            mapView = MapView.FromCamera(CGRect.Empty, camera);
            mapView.MyLocationEnabled = true;
            mapView.Frame = _mapView.Bounds;
            _mapView.AddSubview(mapView);
            KullaniciyaCemberCiz();
        }
        #endregion

        #region Kullaniciya Cember Ciz
        Google.Maps.Circle circ;
        async void KullaniciyaCemberCiz()
        {
           await Task.Run(async delegate () {
           Atla:
               CLLocation kordinatdurum = null;
               InvokeOnMainThread(delegate ()
               {
                   kordinatdurum = mapView.MyLocation;
               });
               if (kordinatdurum == null)
               {
                   await Task.Delay(1000);
                   goto Atla;
               }
               else
               {
                   if (circ == null)
                   {
                       InvokeOnMainThread(delegate ()
                       {
                           var newCamera = CameraPosition.FromCamera(mapView.MyLocation.Coordinate, 18, mapView.Camera.Bearing + 10, mapView.Camera.ViewingAngle + 10);
                           mapView.Animate(newCamera);
                           var circleCenter = mapView.MyLocation;
                           circ = new Google.Maps.Circle();
                           circ.FillColor = UIColor.FromRGB(18, 150, 219).ColorWithAlpha(0.5f);
                           circ.StrokeColor = UIColor.FromRGB(18, 150, 219).ColorWithAlpha(0.2f);
                           circ.Radius = 50f;
                           circ.Position = circleCenter.Coordinate;
                           circ.StrokeWidth = 2f;
                           circ.Map = mapView;
                           CircleKullaniciyiTakipEt();
                           FillDataModel(kordinatdurum.Coordinate);
                           LokasyonDegisimKontrol();
                       });
                   }
               }
            });
            
        }
        void CircleKullaniciyiTakipEt()
        {
            new System.Threading.Thread(new System.Threading.ThreadStart(delegate
            {
                var _timer = new System.Threading.Timer((o) =>
                {
                    InvokeOnMainThread(delegate ()
                    {
                        try
                        {
                            circ.Position = mapView.MyLocation.Coordinate;
                        }
                        catch
                        {
                        }
                    });
                }, null, 0, 1000);
            })).Start();
        }
        #endregion

        #region Map Doldur
        void FillDataModel(CoreLocation.CLLocationCoordinate2D latLng)
        {
            new System.Threading.Thread(new System.Threading.ThreadStart(delegate
            {
               

                WebService webService = new WebService();
                YakindakiNearbyUserCoordinate_RootObject KordinatGonder_RootObject1 = new YakindakiNearbyUserCoordinate_RootObject()
                {
                    latitude = latLng.Latitude.ToString(System.Globalization.CultureInfo.InvariantCulture),
                    longitude = latLng.Longitude.ToString(System.Globalization.CultureInfo.InvariantCulture),
                    meterLimit = 500
                };
                string jsonString = JsonConvert.SerializeObject(KordinatGonder_RootObject1);
                var Donus = webService.ServisIslem("user/coordinate/nearbyUsers", jsonString);
                if (Donus != "Hata")
                {
                    var DonusModel = Newtonsoft.Json.JsonConvert.DeserializeObject<Cevredeki_Kiseler_RootObject>(Donus);
                    if (DonusModel.status == 200)
                    {
                        var UserOlanlar = DonusModel.nearbyUserCoordinates.FindAll(item => item.user != null);
                        var BenHaric = UserOlanlar.FindAll(item => item.user.id.ToString() != MeId);
                        if (BenHaric.Count >= 0)
                        {
                           
                            MapDataModel1 = BenHaric;
                            Console.WriteLine(Donus.ToString());
                            InvokeOnMainThread(delegate()
                            {
                                KisilerScroll.Hidden = false;
                            });
                            CemberIcindekileriAyikla();
                        }
                        else
                        {
                            InvokeOnMainThread(delegate ()
                            {
                                KisilerScroll.Hidden = true;
                                OzelDurumMarkerlariMaptenKaldir();
                                ScrollIciniTemizle();
                                MapDataModel1 = new List<NearbyUserCoordinate>();
                            });
                        }
                    }
                    else
                    {
                        OzelDurumMarkerlariMaptenKaldir();
                        ScrollIciniTemizle();
                        MapDataModel1 = new List<NearbyUserCoordinate>();
                    }
                }
                else
                {
                    OzelDurumMarkerlariMaptenKaldir();
                    ScrollIciniTemizle();
                    MapDataModel1 = new List<NearbyUserCoordinate>();
                }
            })).Start();
        }
        void OzelDurumMarkerlariMaptenKaldir()
        {
            InvokeOnMainThread(delegate () {
                MapDataModel1.ForEach(x => x.IlgiliMarker.Map = null);
            });
            
        }
        void CemberIcindekileriAyikla()
        {
            InvokeOnMainThread(delegate ()
            {
                MapDataModel1.ForEach(x =>
                {
                    if (x.userDistance < circ.Radius)
                    {
                        Console.WriteLine("DURUMMM1");
                        if (x.IlgiliMarker == null)
                        {
                            //Yeni Marker Oluþtur
                            x.IlgiliMarker = ResimliMarkerOlustur(new CLLocationCoordinate2D(x.lat, x.lon), x.user.profile_photo);
                            x.IlgiliMarker.Map = mapView;

                        }
                        else
                        {
                            x.IlgiliMarker.Map = mapView;

                        }
                    }
                    else
                    {
                        Console.WriteLine("DURUMMM2");
                        if (x.IlgiliMarker == null)
                        {
                            //Yeni Marker Oluþtur
                            x.IlgiliMarker = ResimliMarkerOlustur(new CLLocationCoordinate2D(x.lat, x.lon), x.user.profile_photo);
                            x.IlgiliMarker.Map = null;
                        }
                        else
                        {
                            x.IlgiliMarker.Map = null;
                        }
                    }


                });
                ListeyiFragmentCagir();
            });
        }

        Marker ResimliMarkerOlustur(CLLocationCoordinate2D MarkerLocation,string UserPPPath)
        {
            Console.WriteLine("MARKERRRR");
            var xamMarker = new Marker()
            {
                Title = "",
                Snippet = "",
                Position = MarkerLocation,
                Map = mapView,
                Icon = GetMarkerImage(UserPPPath)
            };
            mapView.SelectedMarker = xamMarker;

            return xamMarker;
        }
      
        #region Marker Image Proggess
        UIImage GetMarkerImage(string UserImagePath)
        {
            var ImageUIView = new UIView();
            ImageUIView.Frame = new CGRect(0, 0, 50f, 50f);
            var Balonn = new UIImageView();
            Balonn.Frame = ImageUIView.Bounds;
            Balonn.Image = UIImage.FromBundle("Images/vectorpaint_marker.png");
            Balonn.ContentMode = UIViewContentMode.ScaleAspectFit;
            BalonRekAyarla(Balonn);
            ImageUIView.AddSubview(Balonn);
            ImageUIView.BackgroundColor = UIColor.Clear;
            Balonn.BackgroundColor = UIColor.Clear;
            ImageUIView.Layer.BackgroundColor = UIColor.Clear.CGColor;
            Balonn.Layer.BackgroundColor = UIColor.Clear.CGColor;
            ImageUIView.AddSubview(DownloadUserImage(UserImagePath));


            UIGraphics.BeginImageContextWithOptions(ImageUIView.Bounds.Size, false, 0);
            ImageUIView.Layer.RenderInContext(UIGraphics.GetCurrentContext());
            var img = UIGraphics.GetImageFromCurrentImageContext();
            return img;
        }
        void BalonRekAyarla(UIImageView Iconn)
        {
            var IconImage = Iconn.Image.ImageWithAlignmentRectInsets(new UIEdgeInsets(-5, -5, -5, -5));
            var TintImage = IconImage.ImageWithRenderingMode(UIImageRenderingMode.AlwaysTemplate);
            Iconn.Image = TintImage;
            Iconn.TintColor = UIColor.White;
        }
        UIImageView DownloadUserImage(string path)
        {
            var Iamgee = new UIImageView();
            Iamgee.Frame = new CGRect(6.5f, 2f, 37f, 37f);
            Iamgee.ContentMode = UIViewContentMode.ScaleAspectFill;
            Iamgee.Layer.CornerRadius = 35 / 2;
            Iamgee.ClipsToBounds = true;

            try
            {
                using (var url = new NSUrl(path))
                using (var data = NSData.FromUrl(url))
                {
                    if (data != null)
                    {
                        Iamgee.Image = UIImage.LoadFromData(data);
                    }
                    else
                    {
                        Iamgee.Image = UIImage.FromBundle("Images/male_avatar.png");
                    }
                    return Iamgee;
                }
            }
            catch
            {
                Iamgee.Image = UIImage.FromBundle("Images/male_avatar.png");
                return Iamgee;
            }
        }
        #endregion

        #endregion

        #region Alttaki Liste Islemleri
        CGPoint LastScrollPoint;
        List<NearbyUserCoordinate> LastAyiklanmis = new List<NearbyUserCoordinate>();
        void ListeyiFragmentCagir()
        {
            var MapteGosterilenleriAyikla = MapDataModel1.FindAll(item => item.IlgiliMarker.Map != null);
            LastAyiklanmis = MapteGosterilenleriAyikla;
            ScrollIciniTemizle();
            InvokeOnMainThread(delegate () {
                LastScrollPoint = KisilerScroll.ContentOffset;
            });
            KisilerDizi = new KesfetCustomKisiView[MapteGosterilenleriAyikla.Count];
            for (int i = 0; i < MapteGosterilenleriAyikla.Count; i++)
            {
                var NoktaItem = KesfetCustomKisiView.Create(MapteGosterilenleriAyikla[i], TakipEttiklerimListe, this);
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
            if (MapteGosterilenleriAyikla.Count > 0)
            {
                KisilerScroll.ContentSize = new CGSize(KisilerDizi[KisilerDizi.Length - 1].Frame.Right, 180f);
                KisilerScroll.ShowsHorizontalScrollIndicator = false;
                KisilerScroll.ShowsVerticalScrollIndicator = false;
                KisilerScroll.ContentOffset = LastScrollPoint;
                GetRigToLeftScrollView();
            }
        }
        void ScrollIciniTemizle()
        {
            InvokeOnMainThread(delegate () { 
                 KisilerDizi.ToList().ForEach(x => x.RemoveFromSuperview());
            });
        }
        void GetRigToLeftScrollView()
        {
            return;
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
        #endregion

        #region Lokasyon Degisim Konum Kontrol
        CoreLocation.CLLocationCoordinate2D? LastUserLocaiton;
        void LokasyonDegisimKontrol()
        {
            new System.Threading.Thread(new System.Threading.ThreadStart(delegate
            {
                var _timer = new System.Threading.Timer((o) =>
                {
                    try
                    {
                        InvokeOnMainThread(delegate () {
                            if (mapView.MyLocation != null)
                            {
                                if (LastUserLocaiton == null)
                                {
                                    LastUserLocaiton = mapView.MyLocation.Coordinate;
                                    UserLastCurrentLocation.LastLoc = LastUserLocaiton;
                                }
                                else
                                {
                                    var distance = mapView.MyLocation.DistanceFrom(new CLLocation(((CLLocationCoordinate2D)LastUserLocaiton).Latitude, ((CLLocationCoordinate2D)LastUserLocaiton).Longitude));
                                    if (distance > 10)//10 metreden fazla uzaklaþtýysa
                                    {
                                        try
                                        {
                                            var newCamera = CameraPosition.FromCamera(mapView.MyLocation.Coordinate, mapView.Camera.Zoom, mapView.Camera.Bearing , mapView.Camera.ViewingAngle);
                                            mapView.Animate(newCamera);
                                        }catch{}
                                      
                                        LastUserLocaiton = mapView.MyLocation.Coordinate;
                                        UserLastCurrentLocation.LastLoc = LastUserLocaiton;
                                        FillDataModel(mapView.MyLocation.Coordinate);
                                        return;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Konum Deðiþtiii Mesafee : " + distance.ToString());
                                        return;
                                    }
                                }
                               
                            }
                        });
                    }
                    catch
                    {
                    }

                }, null, 0, 5000);

            })).Start();
        }
        #endregion

        #endregion

        #region DataModels
        public class KordinatGonder_RootObject
        {
            public string latitude { get; set; }
            public string longitude { get; set; }
        }
        public class YakindakiNearbyUserCoordinate_RootObject
        {
            public string latitude { get; set; }
            public string longitude { get; set; }
            public int meterLimit { get; set; }
        }
        public class TakipEttiklerim_RootObject
        {
            public int from_user_id { get; set; }
            public int to_user_id { get; set; }
        }
        public class User
        {
            public int id { get; set; }
            public int type { get; set; }
            public string profile_photo { get; set; }
            public string cover_photo { get; set; }
            public string title { get; set; }
            public string name { get; set; }
            public string surname { get; set; }
            public string career_history { get; set; }
            public string short_biography { get; set; }
            public string credentials { get; set; }
            public string date_of_birth { get; set; }
            public string company_id { get; set; }
            public string sector_id { get; set; }
            public string email { get; set; }
            public string email_verified_at { get; set; }
            public int status { get; set; }
            public string package { get; set; }
            public string created_at { get; set; }
            public string updated_at { get; set; }
        }
        public class NearbyUserCoordinate
        {
            public User user { get; set; }
            public double lat { get; set; }
            public double lon { get; set; }
            public int userDistance { get; set; }

            //Custom Property
            public bool Secim { get; set; }
            public bool IsShow { get; set; }
            public Marker IlgiliMarker { get; set; }

        }
        public class Cevredeki_Kiseler_RootObject
        {
            public int status { get; set; }
            public string message { get; set; }
            public List<NearbyUserCoordinate> nearbyUserCoordinates { get; set; }
        }
        public class OlusturulanMarkerlar
        {
            public Marker marker { get; set; }
            public int markerID { get; set; }
        }
        #endregion

        #region Custom Class
        public class IdComparer : IEqualityComparer<NearbyUserCoordinate>
        {
            public int GetHashCode(NearbyUserCoordinate co)
            {
                if (co == null)
                {
                    return 0;
                }
                return co.user.id.GetHashCode();
            }

            public bool Equals(NearbyUserCoordinate x1, NearbyUserCoordinate x2)
            {
                if (object.ReferenceEquals(x1, x2))
                {
                    return true;
                }
                if (object.ReferenceEquals(x1, null) ||
                    object.ReferenceEquals(x2, null))
                {
                    return false;
                }
                return x1.user.id == x2.user.id;
            }
        }
        #endregion
    }
    public static class UserLastCurrentLocation
    {
        public static CoreLocation.CLLocationCoordinate2D? LastLoc { get; set; }
    }
}
