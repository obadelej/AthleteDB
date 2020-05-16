using AthleteDBUI.Enums;
using AthleteDBUI.Library.DataAccess;
using AthleteDBUI.Library.Models;
using AthleteDBUI.Models;
using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AthleteDBUI.ViewModels
{
    public class AthleteViewModel : Screen
    {
        #region PRIVATE BACKING FIELDS

        private string _firstName;
        private string _lastName;
        private DateTime _birthDate;
        private bool _isMale;
        private string _phone;
        private string _email;
        public int _addressId;
        public int _ParentId;
        public int _schoolId;
        public int _coachId;
        
        private BindingList<AthleteDisplayModel> _athletes;
        private BindingList<AddressDisplayModel> _addresses;
        private BindingList<ParentDisplayModel> _parents;
        private BindingList<SchoolDisplayModel> _schools;
        private BindingList<CoachDisplayModel> _coaches;
        private bool isAdding;
        private bool isDeleting;
        private bool isUpdating;
        private bool isClearing;
        
        private AthleteDisplayModel _selectedAthlete;
        private AddressDisplayModel _selectedAddress;
        private ParentDisplayModel _selectedParent;
        private SchoolDisplayModel _selectedSchool;
        private CoachDisplayModel _selectedCoach;
        private Gender _selectedGender;
        private string msg;

        #endregion
        #region CONSTRUCTORS

        public AthleteViewModel()
        {
            
            Addresses = new BindingList<AddressDisplayModel>(GetAllAddresses());
            Coaches = new BindingList<CoachDisplayModel>(GetAllCoaches());
            Parents = new BindingList<ParentDisplayModel>(GetAllParents());            
            Schools = new BindingList<SchoolDisplayModel>(GetAllSchools());
            Athletes = new BindingList<AthleteDisplayModel>(GetAllAthletes());
        }

        #endregion
        #region PUBLIC PROPERTIES
        

        public bool IsMale
        {
            get { return _isMale; }
            set
            {
                _isMale = value;
                NotifyOfPropertyChange(() => IsMale);                
                NotifyFields();
            }
        }
        
     
        public int AddressId
        {
            get { return _addressId; }
            set
            {
                _addressId = value;
                NotifyOfPropertyChange(() => AddressId);
                UpdateFields();
                NotifyOfPropertyChange(() => CanDelete);
            }
        }

        public Gender SelectedGender
        {
            get { return _selectedGender; }
            set
            {
                _selectedGender = value;
                NotifyOfPropertyChange(() => SelectedGender);
                UpdateFields();
                NotifyOfPropertyChange(() => CanDelete);
            }
        }
        

        public BindingList<AthleteDisplayModel> Athletes
        {
            get { return _athletes; }
            set
            {
                _athletes = value;
                NotifyOfPropertyChange(() => Athletes);
                NotifyOfPropertyChange(() => CanDelete);

            }
        }

        public BindingList<CoachDisplayModel> Coaches
        {
            get { return _coaches; }
            set
            {
                _coaches = value;
                NotifyOfPropertyChange(() => Coaches);
                NotifyOfPropertyChange(() => CanDelete);

            }
        }

        public BindingList<ParentDisplayModel> Parents
        {
            get { return _parents; }
            set
            {
                _parents = value;
                NotifyOfPropertyChange(() => Parents);
                NotifyOfPropertyChange(() => CanDelete);

            }
        }

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

        public BindingList<SchoolDisplayModel> Schools
        {
            get { return _schools; }
            set
            {
                _schools = value;
                NotifyOfPropertyChange(() => Schools);
                NotifyOfPropertyChange(() => CanDelete);

            }
        }

        public AthleteDisplayModel SelectedAthlete
        {
            get { return _selectedAthlete; }
            set
            {                
                _selectedAthlete = value;
                NotifyOfPropertyChange(() => SelectedAthlete);

                if (SelectedAthlete != null)
                {
                    SelectedParent = Parents.Where(x => x.Id == SelectedAthlete.ParentId).FirstOrDefault();
                    SelectedAddress = Addresses.Where(x => x.Id == SelectedAthlete.AddressId).FirstOrDefault();
                    SelectedSchool = Schools.Where(x => x.Id == SelectedAthlete.SchoolId).FirstOrDefault();
                    SelectedCoach = Coaches.Where(x => x.Id == SelectedAthlete.CoachId).FirstOrDefault();
                }
                
                UpdateFields();
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

        public ParentDisplayModel SelectedParent
        {
            get { return _selectedParent; }
            set
            {
                _selectedParent = value;
                NotifyOfPropertyChange(() => SelectedParent);
                UpdateFields();
                NotifyOfPropertyChange(() => CanDelete);

            }
        }
        

        public CoachDisplayModel SelectedCoach
        {
            get { return _selectedCoach; }
            set
            {
                _selectedCoach = value;
                NotifyOfPropertyChange(() => SelectedCoach);
                UpdateFields();
                NotifyOfPropertyChange(() => CanDelete);

            }
        }       

        public SchoolDisplayModel SelectedSchool
        {
            get { return _selectedSchool; }
            set
            {
                _selectedSchool = value;
                NotifyOfPropertyChange(() => SelectedSchool);
                UpdateFields();
                NotifyOfPropertyChange(() => CanDelete);

            }
        }

        public string FirstName
        {
            get { return _firstName; }
            set
            {
                _firstName = value;
                NotifyOfPropertyChange(() => FirstName);
                NotifyFields();
            }
        }

        public string LastName
        {
            get { return _lastName; }
            set
            {
                _lastName = value;
                NotifyOfPropertyChange(() => LastName);
                NotifyFields();
            }
        }

        public DateTime BirthDate
        {
            get { return _birthDate; }
            set
            {
                _birthDate = value;
                NotifyOfPropertyChange(() => BirthDate);
                NotifyFields();
            }
        }

        

        public string Phone
        {
            get { return _phone; }
            set
            {
                _phone = value;
                NotifyOfPropertyChange(() => Phone);
                NotifyFields();
            }
        }

        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
                NotifyOfPropertyChange(() => Email);
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
                if (SelectedAthlete != null && Athletes.Count > 0)
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
                if (FirstName?.Length > 0 || LastName?.Length > 0)
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

            if (FirstName?.Length > 0 && LastName?.Length > 0)
            {
                output = true;
            }

            return output;
        }

        public void Clear()
        {
            isClearing = true;
            FirstName = "";
            LastName = "";
            //BirthDate = DateTime.Now;            
            Phone = "";
            Email = "";
            _isMale = false;
            SelectedAthlete = null;
            SelectedAddress = null;
            SelectedParent = null;
            SelectedSchool = null;
            SelectedCoach = null;
            isClearing = false;
            
        }

        public void Add()
        {
            isAdding = true;

            AthleteDisplayModel e = Athletes.Where(x => x.FirstName == FirstName && x.LastName == LastName).FirstOrDefault();
            if (e == null)
            {
                SqlDataAccess sql = new SqlDataAccess();
                sql.SaveData<dynamic>("dbo.spAthlete_Insert",
                    new
                    {
                        FirstName = _firstName,
                        LastName = _lastName,
                        BirthDate = _birthDate,
                        IsMale = _isMale,
                        Phone = _phone,
                        Email = _email,
                        AddressId = _selectedAddress.Id,
                        ParentId = SelectedParent.Id,
                        SchoolId = SelectedSchool.Id,
                        CoachId = _selectedCoach.Id                   

                    }, "ADBData");

                Athletes = new BindingList<AthleteDisplayModel>(GetAllAthletes());
                NotifyOfPropertyChange(() => Athletes);
                Clear();
            }
            else
            {
                msg = $"Error: An Event named ({SelectedAthlete.FullName}) already exist!!!";
                MessageBox.Show(msg, "Error");
            }

            isAdding = false;
        }

        public void Update()
        {
            AthleteDisplayModel exists = Athletes.Where(x => x.Id == SelectedAthlete.Id).FirstOrDefault();
            if (exists != null)
            {
                if (SelectedAthlete != null && Athletes.Count > 0)
                {
                    isUpdating = true;

                    AthleteModel e = new AthleteModel
                    {
                        Id = SelectedAthlete.Id,
                        FirstName = _firstName,
                        LastName = _lastName,
                        BirthDate = _birthDate,
                        IsMale = _isMale,
                        Phone = _phone,
                        Email = _email,
                        AddressId = SelectedAddress.Id,
                        ParentId = SelectedParent.Id,
                        SchoolId = SelectedSchool.Id,
                        CoachId = SelectedCoach.Id
                    };

                    SqlDataAccess sql = new SqlDataAccess();
                    sql.UpdateData<AthleteModel>("dbo.spAthlete_Update", e, "ADBData");

                    msg = $"Athlete ({SelectedAthlete.FullName}) was successfully updated.";
                    MessageBox.Show(msg, "Athlete Updated");
                    Athletes = new BindingList<AthleteDisplayModel>(GetAllAthletes());
                    Clear();


                    isUpdating = false;
                }

            }
        }

        public void UpdateFields()
        {
            if (SelectedAthlete != null)
            {
                FirstName = SelectedAthlete.FirstName;
                LastName = SelectedAthlete.LastName;
                BirthDate = SelectedAthlete.BirthDate;
                Phone = SelectedAthlete.Phone;
                Email = SelectedAthlete.Email;
                IsMale = SelectedAthlete.IsMale;
            }
        }

        

        public void Delete()
        {
            AthleteDisplayModel e = Athletes.Where(x => x.Id == SelectedAthlete.Id).FirstOrDefault();
            if (e != null)
            {
                SqlDataAccess sql = new SqlDataAccess();
                sql.DeleteData<dynamic>("dbo.spAthlete_Delete", new { Id = SelectedAthlete.Id }, "ADBData");

                Athletes = new BindingList<AthleteDisplayModel>(GetAllAthletes());
                SelectedAthlete = null;
                Clear();
            }
        }

        public void Close()
        {            
            this.Refresh();
            this.TryClose();

        }

        public List<AthleteDisplayModel> GetAllAthletes()
        {
            SqlDataAccess sql = new SqlDataAccess();
            var output = sql.LoadData<AthleteDisplayModel, dynamic>("dbo.spAthlete_GetAll", new { }, "ADBData");
            return output;
        }

        public List<CoachDisplayModel> GetAllCoaches()
        {
            SqlDataAccess sql = new SqlDataAccess();
            var output = sql.LoadData<CoachDisplayModel, dynamic>("dbo.spCoach_GetAll", new { }, "ADBData");
            return output;
        }

        public List<ParentDisplayModel> GetAllParents()
        {
            SqlDataAccess sql = new SqlDataAccess();
            var output = sql.LoadData<ParentDisplayModel, dynamic>("dbo.spParent_GetAll", new { }, "ADBData");
            return output;
        }

        public List<AddressDisplayModel> GetAllAddresses()
        {
            SqlDataAccess sql = new SqlDataAccess();
            var output = sql.LoadData<AddressDisplayModel, dynamic>("dbo.spAddress_GetAll", new { }, "ADBData");
            return output;
        }

        public List<SchoolDisplayModel> GetAllSchools()
        {
            SqlDataAccess sql = new SqlDataAccess();
            var output = sql.LoadData<SchoolDisplayModel, dynamic>("dbo.spSchool_GetAll", new { }, "ADBData");
            return output;
        }
        #endregion
    }
}
