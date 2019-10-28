using Foundation;
using MyProfileiOS.WebServiceHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using UIKit;

namespace MyProfileiOS
{
    public partial class SektorSecVC : UIViewController
    {
        List<Sector> mData = new List<Sector>();
        List<Sector> mData_Kopya = new List<Sector>();
        public ProfilBilgiGuncelleSubVC ProfilBilgiGuncelleSubVC1;

        public SektorSecVC (IntPtr handle) : base (handle)
        {
        }
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            GeriButton.TouchUpInside += GeriButton_TouchUpInside;
            AraSearchBar.TextChanged += AraSearchBar_TextChanged;
            AraSearchBar.CancelButtonClicked += AraSearchBar_CancelButtonClicked;
            AraSearchBar.SearchButtonClicked += AraSearchBar_SearchButtonClicked;
        }

        private void AraSearchBar_SearchButtonClicked(object sender, EventArgs e)
        {
            AraSearchBar.ResignFirstResponder();
        }

        private void AraSearchBar_CancelButtonClicked(object sender, EventArgs e)
        {
            AraSearchBar.ResignFirstResponder();
        }

        private void AraSearchBar_TextChanged(object sender, UISearchBarTextChangedEventArgs e)
        {
            BUL(AraSearchBar.Text);
        }

        private void GeriButton_TouchUpInside(object sender, EventArgs e)
        {
            this.DismissViewController(true, null);
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            Tablo.SeparatorStyle = UITableViewCellSeparatorStyle.None;
            GetAllSektor();
        }
        void BUL(string ifade)
        {
            new System.Threading.Thread(new System.Threading.ThreadStart(delegate
            {
                try
                {
                    mData = (from friend in mData_Kopya
                             where friend.name.Contains(ifade, StringComparison.OrdinalIgnoreCase)
                             select friend).ToList<Sector>();

                    InvokeOnMainThread(() =>
                    {
                        Tablo.Source = new TableSource(mData, this);
                        Tablo.ReloadData();
                    });
                }
                catch
                {
                }

            })).Start();
        }
        void GetAllSektor()
        {
            new System.Threading.Thread(new System.Threading.ThreadStart(delegate
            {
                try
                {
                    WebService webService = new WebService();
                    var Donus = webService.OkuGetir("sector/index");
                    if (Donus != null)
                    {
                        var aaa = Donus.ToString();
                        mData = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Sector>>(Donus.ToString());
                        mData_Kopya = mData;
                        InvokeOnMainThread(() =>
                        {
                            Tablo.SeparatorStyle = UITableViewCellSeparatorStyle.None;
                            Tablo.Source = new TableSource(mData, this);
                            Tablo.ReloadData();
                        });
                    }
                }
                catch
                {
                }

            })).Start();
        }
        public void RowClick(Sector SecilenSector)
        {
            ProfilBilgiGuncelleSubVC1.SecilenSektorUygula(new ProfilBilgiGuncelleSubVC.SektorModel() { name = SecilenSector.name, id = SecilenSector.id });
            this.DismissViewController(true, null);
        }
        public class Sector
        {
            public int id { get; set; }
            public string name { get; set; }
        }

        public class TableSource : UITableViewSource
        {

            List<Sector> TableItems;
            string CellIdentifier = "TableCell";
            SektorSecVC BaseVC;

            public TableSource(List<Sector> items, SektorSecVC BaseVC1)
            {
                TableItems = items;
                BaseVC = BaseVC1;
            }

            public override nint RowsInSection(UITableView tableview, nint section)
            {
                return TableItems.Count;
            }

            public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
            {
                UITableViewCell cell = tableView.DequeueReusableCell(CellIdentifier);
                var item = TableItems[indexPath.Row];

                //---- if there are no cells to reuse, create a new one
                if (cell == null)
                { cell = new UITableViewCell(UITableViewCellStyle.Default, CellIdentifier); }

                cell.TextLabel.Text = item.name;

                return cell;
            }
            public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
            {
                tableView.DeselectRow(indexPath, true);
                BaseVC.RowClick(TableItems[indexPath.Row]);
            }
        }
    }
}