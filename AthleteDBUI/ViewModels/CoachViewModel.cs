using AthleteDBUI.Library.DataAccess;
using AthleteDBUI.Library.Models;
using AthleteDBUI.Models;
using Caliburn.Micro;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using AthleteDBUI.EventModels;

namespace AthleteDBUI.ViewModels
{
    public class CoachViewModel : Screen
    {
        #region PRIVATE BACKING FIELDS

        private string _firstName;
        private string _lastName;
        private string _phone;
        private string _email;
        private bool isAdding;
        private bool isDeleting;
        private bool isUpdating;
        private BindingList<CoachDisplayModel> _coaches;
        private CoachDisplayModel _selectedCoach;
        private string msg;
        IEventAggregator _events;

        #endregion
        #region CONSTRUCTORS

        public CoachViewModel(IEventAggregator events)
        {
            Coaches = new BindingList<CoachDisplayModel>(GetAllCoaches());
            _events = events;
        }

        #endregion
        #region PUBLIC PROPERTIES

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
                if (SelectedCoach != null && Coaches.Count > 0)
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
            FirstName = "";
            LastName = "";
            Phone = "";
            Email = "";
        }

        public void Add()
        {
            isAdding = true;

            CoachDisplayModel e = Coaches.Where(x => x.FirstName == FirstName && x.LastName == LastName).FirstOrDefault();
            if (e == null)
            {
                SqlDataAccess sql = new SqlDataAccess();
                sql.SaveData<dynamic>("dbo.spCoach_Insert",
                    new
                    {
                        FirstName = _firstName,
                        LastName = _lastName,
                        Phone = _phone,
                        Email = _email
                    }, "ADBData");

                Coaches = new BindingList<CoachDisplayModel>(GetAllCoaches());
                NotifyOfPropertyChange(() => Coaches);
                Clear();

                _events.PublishOnUIThread(new CoachChangedEvent());
            }
            else
            {
                msg = $"Error: An Coach named ({SelectedCoach.FullName}) already exist!!!";
                MessageBox.Show(msg, "Error");
            }

            isAdding = false;
        }

        public void Update()
        {
            CoachDisplayModel exists = Coaches.Where(x => x.Id == SelectedCoach.Id).FirstOrDefault();
            if (exists != null)
            {
                if (SelectedCoach != null && Coaches.Count > 0)
                {
                    isUpdating = true;

                    CoachModel e = new CoachModel
                    {
                        Id = SelectedCoach.Id,
                        FirstName = _firstName,
                        LastName = _lastName,
                        Phone = _phone,
                        Email = _email
                    };

                    SqlDataAccess sql = new SqlDataAccess();
                    sql.UpdateData<CoachModel>("dbo.spCoach_Update", e, "ADBData");

                    msg = $"Coach ({SelectedCoach.FullName}) was successfully updated.";
                    MessageBox.Show(msg, "Coach Updated");
                    Coaches = new BindingList<CoachDisplayModel>(GetAllCoaches());
                    Clear();


                    isUpdating = false;

                    _events.PublishOnUIThread(new CoachChangedEvent());
                }

            }
        }

        public void UpdateFields()
        {
            if (SelectedCoach != null)
            {
                FirstName = SelectedCoach.FirstName;
                LastName = SelectedCoach.LastName;
                Phone = SelectedCoach.Phone;
                Email = SelectedCoach.Email;
            }
        }

        public void Delete()
        {
            CoachDisplayModel e = Coaches.Where(x => x.Id == SelectedCoach.Id).FirstOrDefault();
            if (e != null)
            {
                SqlDataAccess sql = new SqlDataAccess();
                sql.DeleteData<dynamic>("dbo.spCoach_Delete", new { Id = SelectedCoach.Id }, "ADBData");

                Coaches = new BindingList<CoachDisplayModel>(GetAllCoaches());
                SelectedCoach = null;
                Clear();

                _events.PublishOnUIThread(new CoachChangedEvent());
            }
        }

        public void Close()
        {
            this.Refresh();
            this.TryClose();

        }

        public List<CoachDisplayModel> GetAllCoaches()
        {
            SqlDataAccess sql = new SqlDataAccess();
            var output = sql.LoadData<CoachDisplayModel, dynamic>("dbo.spCoach_GetAll", new { }, "ADBData");
            return output;
        }
        #endregion
    }
}
