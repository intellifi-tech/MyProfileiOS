using CoreLocation;
using Foundation;
using MyProfileiOS.Etkinlikler.EtkinlikDetay.EtkinlikOlustur.MevcutEtkinigeKatil;
using MyProfileiOS.GenericClass;
using MyProfileiOS.WebServiceHelper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using UIKit;

namespace MyProfileiOS
{
    public partial class MevcutEtkinlikBaseVC : UIViewController
    {
        List<NearbyEvent> EtkinlikListtt;
        public MevcutEtkinlikBaseVC (IntPtr handle) : base (handle)
        {
        }
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            GeriButton.TouchUpInside += GeriButton_TouchUpInside;
        }

        private void GeriButton_TouchUpInside(object sender, EventArgs e)
        {
            this.DismissViewController(true, null);
        }

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);
        }
        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            GetEventList();
        }
        void GetEventList()
        {
            if (UserLastCurrentLocation.LastLoc != null)
            {
                WebService webService = new WebService();
                YakinimdakilerSearchDTO yakinimdakilerSearchDTO = new YakinimdakilerSearchDTO()
                {
                    latitude = ((CoreLocation.CLLocationCoordinate2D)UserLastCurrentLocation.LastLoc).Latitude.ToString(System.Globalization.CultureInfo.InvariantCulture),
                    longitude = ((CoreLocation.CLLocationCoordinate2D)UserLastCurrentLocation.LastLoc).Longitude.ToString(System.Globalization.CultureInfo.InvariantCulture),
                    meterLimit = 100
                };
                var jsonstring = JsonConvert.SerializeObject(yakinimdakilerSearchDTO);
                var Donus = webService.ServisIslem("event/nearbyEvents", jsonstring);
                if (Donus != "Hata")
                {
                    var durumm = Donus.ToString();
                    var GelenEtkinler = Newtonsoft.Json.JsonConvert.DeserializeObject<RootObject_Events>(Donus);
                    if (GelenEtkinler.status == 200)
                    {
                        EtkinlikListtt = GelenEtkinler.nearbyEvent;
                        Tabloo.Source = new FriendListCustomTableSource(EtkinlikListtt, this);
                        Tabloo.ReloadData();
                    }
                }
                else
                {
                    CustomToast.ShowToast(this, "Çevrenizde hiç etkinlik bulunamadý.", ToastType.Clean);
                    return;
                }
            }
            else
            {
                CustomToast.ShowToast(this, "Konumunuza ulaþýlamýyor.", ToastType.Error);
                GetUserLoc();
            }
        }
        void GetUserLoc()
        {
            CLLocationManager locationManager = new CLLocationManager();
            locationManager.StartUpdatingLocation();
            locationManager.StartUpdatingHeading();

            locationManager.LocationsUpdated += delegate (object sender, CLLocationsUpdatedEventArgs e)
            {
                foreach (CLLocation loc in e.Locations)
                {
                    Console.WriteLine(loc.Coordinate.Latitude);
                    UserLastCurrentLocation.LastLoc = loc.Coordinate;
                    GetEventList();
                }
            };
        }

        #region DataModels

        public class YakinimdakilerSearchDTO
        {
            public string latitude { get; set; }
            public string longitude { get; set; }
            public int meterLimit { get; set; }
        }



        public class Event
        {
            public int id { get; set; }
            public string title { get; set; }
            public double latitude { get; set; }
            public double longitude { get; set; }
            public string created_at { get; set; }
            public string updated_at { get; set; }
        }

        public class NearbyEvent
        {
            [JsonProperty("event")]
            public Event eventt { get; set; }
            public double lat { get; set; }
            public double lon { get; set; }
            public int eventDistance { get; set; }
        }

        public class RootObject_Events
        {
            public int status { get; set; }
            public string message { get; set; }
            public List<NearbyEvent> nearbyEvent { get; set; }
        }

        #endregion

        class FriendListCustomTableSource : UITableViewSource
        {
            List<NearbyEvent> TableItems;
            MevcutEtkinlikBaseVC AnaMainListCustomView1;
            string MeID;
            public FriendListCustomTableSource(List<NearbyEvent> items, MevcutEtkinlikBaseVC GelenYer)
            {
                TableItems = items;
                AnaMainListCustomView1 = GelenYer;
            }
            public override nint RowsInSection(UITableView tableview, nint section)
            {
                return TableItems.Count;
            }
            public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
            {
                return 84f;
            }
            public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
            {

                var itemss = TableItems[indexPath.Row];

                //var cell = (MesajlarKisiListItem)tableView.DequeueReusableCell("MesajlarKisiListItem", indexPath) as MesajlarKisiListItem;
                var cell = (BulunanEtkinliklerCustomCell)tableView.DequeueReusableCell(BulunanEtkinliklerCustomCell.Key);
                if (cell == null)
                {
                    cell = BulunanEtkinliklerCustomCell.Create();
                }
                cell.UpdateCell(itemss);
                cell.Accessory = UITableViewCellAccessory.DisclosureIndicator;
                return cell;
            }


            public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
            {
                tableView.DeselectRow(indexPath, true);
                //AnaMainListCustomView1.RowSelectt(TableItems[indexPath.Row]);
            }
        }

    }
}