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
    public class MeetViewModel : Screen
    {
        #region PRIVATE BACKING FIELDS

        private string _meetName;
        private DateTime _startDate;
        private DateTime _endDate;
        private string _location;
        private bool isAdding;
        private bool isDeleting;
        private bool isUpdating;
        private BindingList<MeetDisplayModel> _meets;
        private MeetDisplayModel _selectedMeet;
        private string msg;

        #endregion
        #region CONSTRUCTORS

        public MeetViewModel()
        {
            Meets = new BindingList<MeetDisplayModel>(GetAllMeets());

        }

        #endregion
        #region PUBLIC PROPERTIES

        public BindingList<MeetDisplayModel> Meets
        {
            get { return _meets; }
            set
            {
                _meets = value;
                NotifyOfPropertyChange(() => Meets);
                NotifyOfPropertyChange(() => CanDelete);
            }
        }

        public MeetDisplayModel SelectedMeet
        {
            get { return _selectedMeet; }
            set
            {
                _selectedMeet = value;
                NotifyOfPropertyChange(() => SelectedMeet);
                UpdateFields();
                NotifyOfPropertyChange(() => CanDelete);

            }
        }

        public string MeetName
        {
            get { return _meetName; }
            set
            {
                _meetName = value;
                NotifyOfPropertyChange(() => MeetName);
                NotifyFields();
            }
        }

        public DateTime StartDate
        {
            get { return _startDate; }
            set
            {
                _startDate = value;
                NotifyOfPropertyChange(() => StartDate);
                NotifyFields();
            }
        }

        public DateTime EndDate
        {
            get { return _endDate; }
            set
            {
                _endDate = value;
                NotifyOfPropertyChange(() => EndDate);
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
                if (SelectedMeet != null && Meets.Count > 0)
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
                if (MeetName?.Length > 0 || Location?.Length > 0)
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

            if (MeetName?.Length > 0 && Location?.Length > 0)
            {
                output = true;
            }

            return output;
        }

        public void Clear()
        {
            MeetName = "";
            Location = "";            
        }

        public void Add()
        {
            isAdding = true;

            MeetDisplayModel e = Meets.Where(x => x.MeetName == MeetName).FirstOrDefault();
            if (e == null)
            {
                SqlDataAccess sql = new SqlDataAccess();
                sql.SaveData<dynamic>("dbo.spMeet_Insert",
                    new
                    {
                        MeetName = _meetName,
                        StartDate = _startDate,
                        EndDate = _endDate,
                        Location = _location
                    }, "ADBData");

                Meets = new BindingList<MeetDisplayModel>(GetAllMeets());
                NotifyOfPropertyChange(() => Meets);
                Clear();
            }
            else
            {
                msg = $"Error: An Meet named ({SelectedMeet.MeetName}) already exist!!!";
                MessageBox.Show(msg, "Error");
            }

            isAdding = false;
        }

        public void Update()
        {
            MeetDisplayModel exists = Meets.Where(x => x.Id == SelectedMeet.Id).FirstOrDefault();
            if (exists != null)
            {
                if (SelectedMeet != null && Meets.Count > 0)
                {
                    isUpdating = true;

                    MeetModel e = new MeetModel
                    {
                        Id = SelectedMeet.Id,
                        MeetName = _meetName,
                        StartDate = _startDate,
                        EndDate = _endDate,
                        Location = _location
                    };

                    SqlDataAccess sql = new SqlDataAccess();
                    sql.UpdateData<MeetModel>("dbo.spMeet_Update", e, "ADBData");

                    msg = $"Meet ({SelectedMeet.MeetName}) was successfully updated.";
                    MessageBox.Show(msg, "Meet Updated");
                    Meets = new BindingList<MeetDisplayModel>(GetAllMeets());
                    Clear();


                    isUpdating = false;
                }

            }
        }

        public void UpdateFields()
        {
            if (SelectedMeet != null)
            {
                MeetName = SelectedMeet.MeetName;
                StartDate = SelectedMeet.StartDate;
                EndDate = SelectedMeet.EndDate;
                Location = SelectedMeet.Location;
            }
        }

        public void Delete()
        {
            MeetDisplayModel e = Meets.Where(x => x.Id == SelectedMeet.Id).FirstOrDefault();
            if (e != null)
            {
                SqlDataAccess sql = new SqlDataAccess();
                sql.DeleteData<dynamic>("dbo.spMeet_Delete", new { Id = SelectedMeet.Id }, "ADBData");

                Meets = new BindingList<MeetDisplayModel>(GetAllMeets());
                SelectedMeet = null;
                Clear();
            }
        }

        public void Close()
        {
            this.Refresh();
            this.TryClose();

        }

        public List<MeetDisplayModel> GetAllMeets()
        {
            SqlDataAccess sql = new SqlDataAccess();
            var output = sql.LoadData<MeetDisplayModel, dynamic>("dbo.spMeet_GetAll", new { }, "ADBData");
            return output;
        }
        #endregion
    }
}
