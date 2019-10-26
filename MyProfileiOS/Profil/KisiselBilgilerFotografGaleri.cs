using CoreGraphics;
using FFImageLoading;
using Foundation;
using MyProfileiOS.DataBasee;
using MyProfileiOS.GenericClass;
using MyProfileiOS.WebServiceHelper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using UIKit;

namespace MyProfileiOS
{
    public partial class KisiselBilgilerFotografGaleri : UIViewController
    {
        List<UserGallery> UserGallery1 = new List<UserGallery>();
        public KisiselBilgilerFotografGaleri (IntPtr handle) : base (handle)
        {
        }
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
        }
        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);
            GaleriyiGetir();
        }
        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            CollView.ScrollEnabled = false;
        }
        void GaleriyiGetir()
        {


            #region Genislik Alýr
            //Display display = this.Activity.WindowManager.DefaultDisplay;
            var size = UIScreen.MainScreen.Bounds;
            //display.GetSize(size);
            var width = size.Width;
            var height = size.Height;
            var Genislik = width / 3;
            #endregion

            WebService webService = new WebService();
            var Donus = webService.OkuGetir("user/" + SecilenKullanici.UserID + "/gallery");
            if (Donus != null)
            {
                var Modell = Newtonsoft.Json.JsonConvert.DeserializeObject<GaleriRecyclerViewDataModel>(Donus);
                UserGallery1 = Modell.userGallery;

                UserGallery1.Reverse();

                if (DataBase.USER_INFO_GETIR()[0].id == SecilenKullanici.UserID)
                {
                    UserGallery1.Insert(0, new UserGallery() { AddNewPhoto = true });
                }

                var Layoutt = new UICollectionViewFlowLayout();
                CollView.CollectionViewLayout = Layoutt;
                Layoutt.SectionInset = new UIEdgeInsets(5, 5, 5, 5);
                var ItemW = (UIScreen.MainScreen.Bounds.Width - 5 * 3) / 3;
                Layoutt.ItemSize = new CGSize(ItemW, 156);
                Layoutt.MinimumLineSpacing = 3;
                Layoutt.MinimumInteritemSpacing = 0;

                CollView.RegisterClassForCell(typeof(ShortCell), ShortCell.CellID);
                CollView.Source = new DataSourcee(UserGallery1, this);
                CollView.ReloadData();
                YukseklikCagrisiYap();
            }
            else
            {
                if (DataBase.USER_INFO_GETIR()[0].id == SecilenKullanici.UserID)
                {
                    UserGallery1 = new List<UserGallery>();
                    UserGallery1.Insert(0, new UserGallery() { AddNewPhoto = true });
                }

                var Layoutt = new UICollectionViewFlowLayout();
                CollView.CollectionViewLayout = Layoutt;
                Layoutt.SectionInset = new UIEdgeInsets(5, 5, 5, 5);
                var ItemW = (UIScreen.MainScreen.Bounds.Width - 5 * 3) / 2;
                Layoutt.ItemSize = new CGSize(ItemW, 156);
                Layoutt.MinimumLineSpacing = 5;
                Layoutt.MinimumInteritemSpacing = 0;

                CollView.RegisterClassForCell(typeof(ShortCell), ShortCell.CellID);
                CollView.Source = new DataSourcee(UserGallery1, this);
                CollView.ReloadData();
                YukseklikCagrisiYap();
            }
        }

        void YukseklikCagrisiYap()
        {
            CollView.LayoutIfNeeded();
            SonYuksekligiAyarla.YukseklikUygula(CollView.ContentSize.Height);
        }

        UIImagePickerController picker;
        public void ItemClick(UserGallery TiklananFotograf)
        {
            if (TiklananFotograf.AddNewPhoto)
            {
                picker = new UIImagePickerController();
                picker.SourceType = UIImagePickerControllerSourceType.PhotoLibrary;
                picker.MediaTypes = UIImagePickerController.AvailableMediaTypes(UIImagePickerControllerSourceType.PhotoLibrary);
                picker.FinishedPickingMedia += Picker_FinishedPickingMedia;
                picker.Canceled += Picker_Canceled;
                this.PresentModalViewController(picker, true);
            }
            else
            {

            }
        }

        private void Picker_Canceled(object sender, EventArgs e)
        {
            picker.DismissModalViewController(true);
        }

        private void Picker_FinishedPickingMedia(object sender, UIImagePickerMediaPickedEventArgs e)
        {
            bool isImage = false;
            switch (e.Info[UIImagePickerController.MediaType].ToString())
            {
                case "public.image":
                    isImage = true;
                    break;
                case "public.video":
                    break;
            }
            NSUrl referenceURL = e.Info[new NSString("UIImagePickerControllerReferenceUrl")] as NSUrl;
            if (referenceURL != null) Console.WriteLine("Url:" + referenceURL.ToString());
            if (isImage)
            {
                UIImage originalImage = e.Info[UIImagePickerController.OriginalImage] as UIImage;
                if (originalImage != null)
                {
                    string _base64String;
                    NSData imageData = originalImage.AsJPEG(compressionQuality: 0.1f);
                    _base64String = imageData.GetBase64EncodedString(NSDataBase64EncodingOptions.None);
                    FotoGuncelle(_base64String);
                }
            }
            else
            {
                NSUrl mediaURL = e.Info[UIImagePickerController.MediaURL] as NSUrl;
                if (mediaURL != null)
                {
                    Console.WriteLine(mediaURL.ToString());
                }
            }
            picker.DismissModalViewController(true);
        }

        void FotoGuncelle(string base644)
        {
            WebService webService = new WebService();
            AddnewPhotoClass AddnewPhotoClass1 = new AddnewPhotoClass();
            AddnewPhotoClass1.photo = base644;
            string jsonString = JsonConvert.SerializeObject(AddnewPhotoClass1);
            var Donus = webService.ServisIslem("user/addPhoto", jsonString);
            if (Donus != "Hata")
            {
                var YeniEklenenResim = Newtonsoft.Json.JsonConvert.DeserializeObject<UserGallery>(Donus);
                UserGallery1.Insert(1, YeniEklenenResim);
                CollView.Source = null;
                CollView.ReloadData();
                CollView.Source = new DataSourcee(UserGallery1, this);
                CollView.ReloadData();
                CollView.ReloadData();
                YukseklikCagrisiYap();
                return;
            }
            else
            {
                CustomToast.ShowToast(this,"Bir sorun oluþtu.", ToastType.Error);
                return;
            }
        }

        #region DataModel
        public class UserGallery
        {
            public int id { get; set; }
            public int user_id { get; set; }
            public string photo_name { get; set; }
            public int rating { get; set; }
            public string created_at { get; set; }
            public string updated_at { get; set; }

            //Custom Property
            public bool AddNewPhoto { get; set; }
        }
        public class GaleriRecyclerViewDataModel
        {
            public int status { get; set; }
            public string message { get; set; }
            public List<UserGallery> userGallery { get; set; }
        }

        public class AddnewPhotoClass
        {
            public string photo { get; set; }
        }
        #endregion



        public class DataSourcee : UICollectionViewSource
        {
            List<UserGallery> GelenModel;
            KisiselBilgilerFotografGaleri GelenBase;
            public DataSourcee(List<UserGallery> GelenBaseModel, KisiselBilgilerFotografGaleri GelenBasee)
            {
                GelenModel = GelenBaseModel;
                GelenBase = GelenBasee;
            }
            public override nint NumberOfSections(UICollectionView collectionView)
            {
                return 1;
            }

            public override nint GetItemsCount(UICollectionView collectionView, nint section)
            {
                return GelenModel.Count;
            }

            public override bool ShouldHighlightItem(UICollectionView collectionView, NSIndexPath indexPath)
            {
                return true;
            }

            public override void ItemHighlighted(UICollectionView collectionView, NSIndexPath indexPath)
            {
                var cell = (ShortCell)collectionView.CellForItem(indexPath);
                cell.Alpha = 0.5f;
            }

            public override void ItemUnhighlighted(UICollectionView collectionView, NSIndexPath indexPath)
            {
                var cell = (ShortCell)collectionView.CellForItem(indexPath);
                cell.Alpha = 1f;

            }
            public override UICollectionViewCell GetCell(UICollectionView collectionView, NSIndexPath indexPath)
            {
                var Celll = (ShortCell)collectionView.DequeueReusableCell(ShortCell.CellID, indexPath);

                if (!Celll.Olustumu)
                {
                    Celll.UpdateCell(GelenModel[indexPath.Row]);
                }
                return Celll;
            }

            public override void ItemSelected(UICollectionView collectionView, NSIndexPath indexPath)
            {
                GelenBase.ItemClick(GelenModel[indexPath.Row]);
                
            }
        }


        public class ShortCell : UICollectionViewCell
        {
            public UIView ImageBG;
            public UIImageView ImageVieww;
            public UILabel MekanName;
            public static NSString CellID = new NSString("KisaVideoCustomCell");
            public bool Olustumu = false;

            // UseFulAppLogos ShortVideoCustomListItem1;

            public override void PrepareForReuse()
            {
                //this.ContentView.Subviews.ToList().ForEach(item => {
                //    if (item.GetType() == typeof(UIImageView))
                //    {
                //        item.RemoveFromSuperview();
                //    }
                //});
            }

            [Export("initWithFrame:")]
            public ShortCell(CGRect frame) : base(frame)
            {
                this.BackgroundColor = UIColor.Clear;
            }

            public void UpdateCell(UserGallery GelenModel)
            {
                //this.ContentView.Subviews.ToList().ForEach(item => {
                //    if (item.GetType() == typeof(UIImageView))
                //    {
                //        item.RemoveFromSuperview();
                //    }
                //});

                if (GelenModel.AddNewPhoto)
                {
                    ImageVieww = new UIImageView();
                    ImageVieww.Frame = this.ContentView.Bounds;
                    ImageVieww.ContentMode = UIViewContentMode.ScaleAspectFit;
                    ImageVieww.ClipsToBounds = true;
                    ImageVieww.Image = UIImage.FromBundle("Images/add.png");
                    ImageVieww.RemoveFromSuperview();
                    this.ContentView.Add(ImageVieww);
                    ImageVieww.ContentMode = UIViewContentMode.ScaleAspectFill;
                }
                else
                {
                    if (ImageVieww == null)
                    {
                        ImageVieww = new UIImageView();
                        ImageVieww.Frame = this.ContentView.Bounds;
                        ImageVieww.ContentMode = UIViewContentMode.ScaleAspectFill;
                        ImageVieww.ClipsToBounds = true;
                        ImageService.Instance.LoadUrl(GelenModel.photo_name).Into(ImageVieww);
                        ImageVieww.RemoveFromSuperview();
                        this.ContentView.Add(ImageVieww);
                    }
                }
                Olustumu = true;
            }
        }
    }
}