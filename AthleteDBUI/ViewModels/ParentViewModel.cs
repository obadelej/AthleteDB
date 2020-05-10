using AthleteDBUI.EventModels;
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
    public class ParentViewModel : Screen
    {
        #region PRIVATE BACKING FIELDS

        private string _firstName;
        private string _lastName;
        private string _phone;
        private string _email;
        private bool isAdding;
        private bool isDeleting;
        private bool isUpdating;
        private BindingList<ParentDisplayModel> _parents;
        private ParentDisplayModel _selectedParent;
        private string msg;
        IEventAggregator _events;

        #endregion
        #region CONSTRUCTORS

        public ParentViewModel(IEventAggregator events)
        {
            Parents = new BindingList<ParentDisplayModel>(GetAllParents());
            _events = events;

        }

        #endregion
        #region PUBLIC PROPERTIES

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
                if (SelectedParent != null && Parents.Count > 0)
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

            ParentDisplayModel e = Parents.Where(x => x.FirstName == FirstName && x.LastName == LastName).FirstOrDefault();
            if (e == null)
            {
                SqlDataAccess sql = new SqlDataAccess();
                sql.SaveData<dynamic>("dbo.spParent_Insert",
                    new
                    {
                        FirstName = _firstName,
                        LastName = _lastName,
                        Phone = _phone,
                        Email = _email
                    }, "ADBData");

                _parents = new BindingList<ParentDisplayModel>(GetAllParents());
                NotifyOfPropertyChange(() => Parents);
                Clear();

                _events.PublishOnUIThread(new ParentChangedEvent());
            }
            else
            {
                msg = $"Error: An Event named ({SelectedParent.FullName}) already exist!!!";
                MessageBox.Show(msg, "Error");
            }            

            isAdding = false;
        }

        public void Update()
        {
            ParentDisplayModel exists = Parents.Where(x => x.Id == SelectedParent.Id).FirstOrDefault();
            if (exists != null)
            {
                if (SelectedParent != null && Parents.Count > 0)
                {
                    isUpdating = true;

                    ParentModel e = new ParentModel
                    {
                        Id = SelectedParent.Id,
                        FirstName = _firstName,
                        LastName = _lastName,
                        Phone = _phone,
                        Email = _email
                    };

                    SqlDataAccess sql = new SqlDataAccess();
                    sql.UpdateData<ParentModel>("dbo.spParent_Update", e, "ADBData");

                    msg = $"Parent ({SelectedParent.FullName}) was successfully updated.";
                    MessageBox.Show(msg, "Parent Updated");
                    Parents = new BindingList<ParentDisplayModel>(GetAllParents());
                    Clear();
                    
                    isUpdating = false;
                }

            }

            _events.PublishOnUIThread(new ParentChangedEvent());
        }

        public void UpdateFields()
        {
            if (SelectedParent != null)
            {
                FirstName = SelectedParent.FirstName;
                LastName = SelectedParent.LastName;
                Phone = SelectedParent.Phone;
                Email = SelectedParent.Email;
            }
        }

        public void Delete()
        {
            ParentDisplayModel e = Parents.Where(x => x.Id == SelectedParent.Id).FirstOrDefault();
            if (e != null)
            {
                SqlDataAccess sql = new SqlDataAccess();
                sql.DeleteData<dynamic>("dbo.spParent_Delete", new { Id = SelectedParent.Id }, "ADBData");

                Parents = new BindingList<ParentDisplayModel>(GetAllParents());
                SelectedParent = null;
                Clear();
                _events.PublishOnUIThread(new ParentChangedEvent());
            }
        }

        public void Close()
        {
            this.Refresh();
            this.TryClose();

        }

        public List<ParentDisplayModel> GetAllParents()
        {
            SqlDataAccess sql = new SqlDataAccess();
            var output = sql.LoadData<ParentDisplayModel, dynamic>("dbo.spParent_GetAll", new { }, "ADBData");
            return output;
        }
        #endregion
    }
}
