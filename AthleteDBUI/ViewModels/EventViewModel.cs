using AthleteDBUI.EventModels;
using AthleteDBUI.Library.DataAccess;
using AthleteDBUI.Library.Models;
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
    public class EventViewModel : Screen
    {
        #region PRIVATE BACKING FIELDS

        private string _eventName;
        private string _eventCode;
        private bool isAdding;
        private bool isDeleting;
        private bool isUpdating;
        private BindingList<EventModel> _events;
        private EventModel _selectedEvent;
        private string msg;

        IEventAggregator _eventsNotifier;

        #endregion
        #region CONSTRUCTORS

        public EventViewModel(IEventAggregator eventsNotifier)
        {
            _events = new BindingList<EventModel>(GetAllEvents());
            _eventsNotifier = eventsNotifier;
        }

        #endregion
        #region PUBLIC PROPERTIES

        public BindingList<EventModel> Events
        {
            get { return _events; }
            set
            {
                _events = value;
                NotifyOfPropertyChange(() => Events);
                NotifyOfPropertyChange(() => CanDelete);

            }
        }

        public EventModel SelectedEvent
        {
            get { return _selectedEvent; }
            set
            {
                _selectedEvent = value;
                NotifyOfPropertyChange(() => SelectedEvent);
                UpdateFields();
                NotifyOfPropertyChange(() => CanDelete);

            }
        }

        public string EventName
        {
            get { return _eventName; }
            set
            {
                _eventName = value;
                NotifyOfPropertyChange(() => EventName);
                NotifyFields();
            }
        }

        public string EventCode
        {
            get { return _eventCode; }
            set
            {
                _eventCode = value;
                NotifyOfPropertyChange(() => EventCode);
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
                if (SelectedEvent != null && Events.Count > 0)
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
                if (EventName?.Length > 0 || EventCode?.Length > 0)
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

            if (EventName?.Length > 0 && EventCode?.Length > 0)
            {
                output = true;
            }

            return output;
        }

        public void Clear()
        {
            EventName = "";
            EventCode = "";
        }

        public void Add()
        {
            isAdding = true;

            EventModel e = Events.Where(x => x.EventName == EventName).FirstOrDefault();
            if (e == null)
            {
                SqlDataAccess sql = new SqlDataAccess();
                sql.SaveData<dynamic>("dbo.spEvent_Insert",
                    new
                    {
                        EventName = _eventName,
                        EventCode = _eventCode
                    }, "ADBData");

                _events = new BindingList<EventModel>(GetAllEvents());
                NotifyOfPropertyChange(() => Events);
                Clear();

                _eventsNotifier.PublishOnUIThread(new EventChangedEvent());
            }
            else
            {
                msg = $"Error: An Event named ({SelectedEvent.EventName}) already exist!!!";
                MessageBox.Show(msg, "Error");
            }

            isAdding = false;
        }

        public void Update()
        {
            EventModel exists = Events.Where(x => x.Id == SelectedEvent.Id).FirstOrDefault();
            if (exists != null)
            {
                if (SelectedEvent != null && Events.Count > 0)
                {
                    isUpdating = true;

                    EventModel e = new EventModel
                    {
                        Id = SelectedEvent.Id,
                        EventName = _eventName,
                        EventCode = _eventCode
                    };

                    SqlDataAccess sql = new SqlDataAccess();
                    sql.UpdateData<EventModel>("dbo.spEvent_Update", e, "ADBData");

                    msg = $"Event ({SelectedEvent.EventName}) was successfully updated.";
                    MessageBox.Show(msg, "Event Updated");
                    Events = new BindingList<EventModel>(GetAllEvents());
                    Clear();


                    isUpdating = false;
                    _eventsNotifier.PublishOnUIThread(new EventChangedEvent());
                }

            }
        }

        public void UpdateFields()
        {
            if (SelectedEvent != null)
            {
                EventName = SelectedEvent.EventName;
                EventCode = SelectedEvent.EventCode;
            }
        }

        public void Delete()
        {
            EventModel e = Events.Where(x => x.Id == SelectedEvent.Id).FirstOrDefault();
            if (e != null)
            {
                SqlDataAccess sql = new SqlDataAccess();
                sql.DeleteData<dynamic>("dbo.spEvent_Delete", new { Id = SelectedEvent.Id }, "ADBData");

                Events = new BindingList<EventModel>(GetAllEvents());
                SelectedEvent = null;
                Clear();

                _eventsNotifier.PublishOnUIThread(new EventChangedEvent());
            }
        }

        public void Close()
        {
            this.Refresh();
            this.TryClose();

        }

        public List<EventModel> GetAllEvents()
        {
            SqlDataAccess sql = new SqlDataAccess();
            var output = sql.LoadData<EventModel, dynamic>("dbo.spEvent_GetAll", new { }, "ADBData");
            return output;
        }
        #endregion
    }
}
