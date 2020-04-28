using AthleteDBUI.Library.DataAccess;
using AthleteDBUI.Library.Models;
using AthleteDBUI.Models;
using Caliburn.Micro;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;

namespace AthleteDBUI.ViewModels
{
    public class AddressViewModel : Screen
    {
        #region PRIVATE BACKING FIELDS

        private string _street1;
        private string _street2;
        private string _town;
        private string _parish;
        private string _country;
        private bool isAdding;
        private bool isDeleting;
        private bool isUpdating;
        private BindingList<AddressDisplayModel> _addresses;
        private AddressDisplayModel _selectedAddress;
        private string msg;

        #endregion
        #region CONSTRUCTORS

        public AddressViewModel()
        {
            Addresses = new BindingList<AddressDisplayModel>(GetAllAddresses());

        }

        #endregion
        #region PUBLIC PROPERTIES

        public BindingList<AddressDisplayModel> Addresses
        {
            get { return _addresses; }
            set
            {
                _addresses = value;
                NotifyOfPropertyChange(() => Addresses);
                NotifyOfPropertyChange(() => CanDelete);

            }
        }

        public AddressDisplayModel SelectedAddress
        {
            get { return _selectedAddress; }
            set
            {
                _selectedAddress = value;
                NotifyOfPropertyChange(() => SelectedAddress);
                UpdateFields();
                NotifyOfPropertyChange(() => CanDelete);

            }
        }

        public string Street1
        {
            get { return _street1; }
            set
            {
                _street1 = value;
                NotifyOfPropertyChange(() => Street1);
                NotifyFields();
            }
        }

        public string Street2
        {
            get { return _street2; }
            set
            {
                _street2 = value;
                NotifyOfPropertyChange(() => Street2);
                NotifyFields();
            }
        }

        public string Town
        {
            get { return _town; }
            set
            {
                _town = value;
                NotifyOfPropertyChange(() => Town);
                NotifyFields();
            }
        }

        public string Parish
        {
            get { return _parish; }
            set
            {
                _parish = value;
                NotifyOfPropertyChange(() => Parish);
                NotifyFields();
            }
        }

        public string Country
        {
            get { return _country; }
            set
            {
                _country = value;
                NotifyOfPropertyChange(() => Country);
                NotifyFields();
            }
        }

        public bool CanAdd
        {
            get
            {
                bool output = false;
                if (IsAllFieldsFilled())
                {
                    output = true;
                }

                return output;
            }
        }

        public bool CanDelete
        {
            get
            {
                bool output = false;
                if (SelectedAddress != null && Addresses.Count > 0)
                {
                    output = true;
                }

                return output;
            }
        }

        public bool CanUpdate
        {
            get
            {
                bool output = false;
                if (IsAllFieldsFilled())
                {
                    output = true;
                }

                return output;
            }
        }

        public bool CanClear
        {
            get
            {
                bool output = false;
                if (Street1?.Length > 0 || Street2?.Length > 0)
                {
                    output = true;
                }

                return output;
            }
        }
        #endregion
        #region PUBLIC METHODS
        #endregion
        #region PRIVATE METHODS

        private void NotifyFields()
        {
            NotifyOfPropertyChange(() => CanAdd);
            NotifyOfPropertyChange(() => CanUpdate);
            NotifyOfPropertyChange(() => CanClear);
        }

        private bool IsAllFieldsFilled()
        {
            bool output = false;

            if (Street1?.Length > 0 && Street2?.Length > 0)
            {
                output = true;
            }

            return output;
        }

        public void Clear()
        {
            Street1 = "";
            Street2 = "";
            Town = "";
            Parish = "";
        }

        public void Add()
        {
            isAdding = true;

            AddressDisplayModel e = Addresses.Where(x => x.Street1 == Street1 && x.Street2 == Street2).FirstOrDefault();
            if (e == null)
            {
                SqlDataAccess sql = new SqlDataAccess();
                sql.SaveData<dynamic>("dbo.spAddress_Insert",
                    new
                    {
                        Street1 = _street1,
                        Street2 = _street2,
                        Town = _town,
                        Parish = _parish,
                        Country = _country
                    }, "ADBData");

                Addresses = new BindingList<AddressDisplayModel>(GetAllAddresses());
                NotifyOfPropertyChange(() => Addresses);
                Clear();
            }
            else
            {
                msg = $"Error: An Address named ({SelectedAddress.FullAddress}) already exist!!!";
                MessageBox.Show(msg, "Error");
            }

            isAdding = false;
        }

        public void Update()
        {
            AddressDisplayModel exists = Addresses.Where(x => x.Id == SelectedAddress.Id).FirstOrDefault();
            if (exists != null)
            {
                if (SelectedAddress != null && Addresses.Count > 0)
                {
                    isUpdating = true;

                    AddressModel e = new AddressModel
                    {
                        Id = SelectedAddress.Id,
                        Street1 = _street1,
                        Street2 = _street2,
                        Town = _town,
                        Parish = _parish
                    };

                    SqlDataAccess sql = new SqlDataAccess();
                    sql.UpdateData<AddressModel>("dbo.spAddress_Update", e, "ADBData");

                    msg = $"Address ({SelectedAddress.FullAddress}) was successfully updated.";
                    MessageBox.Show(msg, "Address Updated");
                    Addresses = new BindingList<AddressDisplayModel>(GetAllAddresses());
                    Clear();


                    isUpdating = false;
                }

            }
        }

        public void UpdateFields()
        {
            if (SelectedAddress != null)
            {
                Street1 = SelectedAddress.Street1;
                Street2 = SelectedAddress.Street2;
                Town = SelectedAddress.Town;
                Parish = SelectedAddress.Parish;
            }
        }

        public void Delete()
        {
            AddressDisplayModel e = Addresses.Where(x => x.Id == SelectedAddress.Id).FirstOrDefault();
            if (e != null)
            {
                SqlDataAccess sql = new SqlDataAccess();
                sql.DeleteData<dynamic>("dbo.spAddress_Delete", new { Id = SelectedAddress.Id }, "ADBData");

                Addresses = new BindingList<AddressDisplayModel>(GetAllAddresses());
                SelectedAddress = null;
                Clear();
            }
        }

        public void Close()
        {
            this.Refresh();
            this.TryClose();

        }

        public List<AddressDisplayModel> GetAllAddresses()
        {
            SqlDataAccess sql = new SqlDataAccess();
            var output = sql.LoadData<AddressDisplayModel, dynamic>("dbo.spAddress_GetAll", new { }, "ADBData");
            return output;
        }
        #endregion
    }
}
