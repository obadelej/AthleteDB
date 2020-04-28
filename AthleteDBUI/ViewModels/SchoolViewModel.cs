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
    public class SchoolViewModel : Screen
    {
        #region PRIVATE BACKING FIELDS

        private string _schoolName;
        private string _schoolCode;
        private string _phone;
        private string _location;
        private bool isAdding;
        private bool isDeleting;
        private bool isUpdating;
        private BindingList<SchoolDisplayModel> _schools;
        private SchoolDisplayModel _selectedSchool;
        private string msg;

        #endregion
        #region CONSTRUCTORS

        public SchoolViewModel()
        {
            Schools = new BindingList<SchoolDisplayModel>(GetAllSchools());

        }

        #endregion
        #region PUBLIC PROPERTIES

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

        public string SchoolName
        {
            get { return _schoolName; }
            set
            {
                _schoolName = value;
                NotifyOfPropertyChange(() => SchoolName);
                NotifyFields();
            }
        }

        public string SchoolCode
        {
            get { return _schoolCode; }
            set
            {
                _schoolCode = value;
                NotifyOfPropertyChange(() => SchoolCode);
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

        public string Location
        {
            get { return _location; }
            set
            {
                _location = value;
                NotifyOfPropertyChange(() => Location);
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
                if (SelectedSchool != null && Schools.Count > 0)
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
                if (SchoolName?.Length > 0 || SchoolCode?.Length > 0)
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

            if (SchoolName?.Length > 0 && SchoolCode?.Length > 0)
            {
                output = true;
            }

            return output;
        }

        public void Clear()
        {
            SchoolName = "";
            SchoolCode = "";
            Phone = "";
            Location = "";
        }

        public void Add()
        {
            isAdding = true;

            SchoolDisplayModel e = Schools.Where(x => x.SchoolName == SchoolName && x.SchoolCode == SchoolCode).FirstOrDefault();
            if (e == null)
            {
                SqlDataAccess sql = new SqlDataAccess();
                sql.SaveData<dynamic>("dbo.spSchool_Insert",
                    new
                    {
                        SchoolName = _schoolName,
                        SchoolCode = _schoolCode,
                        Phone = _phone,
                        Location = _location
                    }, "ADBData");

                Schools = new BindingList<SchoolDisplayModel>(GetAllSchools());
                NotifyOfPropertyChange(() => Schools);
                Clear();
            }
            else
            {
                msg = $"Error: An School named ({SelectedSchool.SchoolName}) already exist!!!";
                MessageBox.Show(msg, "Error");
            }

            isAdding = false;
        }

        public void Update()
        {
            SchoolDisplayModel exists = Schools.Where(x => x.Id == SelectedSchool.Id).FirstOrDefault();
            if (exists != null)
            {
                if (SelectedSchool != null && Schools.Count > 0)
                {
                    isUpdating = true;

                    SchoolModel e = new SchoolModel
                    {
                        Id = SelectedSchool.Id,
                        SchoolName = _schoolName,
                        SchoolCode = _schoolCode,
                        Phone = _phone,
                        Location = _location
                    };

                    SqlDataAccess sql = new SqlDataAccess();
                    sql.UpdateData<SchoolModel>("dbo.spSchool_Update", e, "ADBData");

                    msg = $"School ({SelectedSchool.SchoolName}) was successfully updated.";
                    MessageBox.Show(msg, "School Updated");
                    Schools = new BindingList<SchoolDisplayModel>(GetAllSchools());
                    Clear();


                    isUpdating = false;
                }

            }
        }

        public void UpdateFields()
        {
            if (SelectedSchool != null)
            {
                SchoolName = SelectedSchool.SchoolName;
                SchoolCode = SelectedSchool.SchoolCode;
                Phone = SelectedSchool.Phone;
                Location = SelectedSchool.Location;
            }
        }

        public void Delete()
        {
            SchoolDisplayModel e = Schools.Where(x => x.Id == SelectedSchool.Id).FirstOrDefault();
            if (e != null)
            {
                SqlDataAccess sql = new SqlDataAccess();
                sql.DeleteData<dynamic>("dbo.spSchool_Delete", new { Id = SelectedSchool.Id }, "ADBData");

                Schools = new BindingList<SchoolDisplayModel>(GetAllSchools());
                SelectedSchool = null;
                Clear();
            }
        }

        public void Close()
        {
            this.Refresh();
            this.TryClose();

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
