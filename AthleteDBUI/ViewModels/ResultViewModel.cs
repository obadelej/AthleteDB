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
    public class ResultViewModel : Screen
    {
        #region PRIVATE BACKING FIELDS
        private bool isAdding;
        private bool isUpdating;

        private string _mark;
        private DateTime _perfDate;
        private int _resultId;
        private int _athleteId;
        private int _meetId;
        private int _eventId;
        private string msg;

        private BindingList<ResultDisplayModel> _results;
        private BindingList<AthleteDisplayModel> _athletes;
        private BindingList<MeetDisplayModel> _meets;
        private BindingList<EventDisplayModel> _eventList;
        private AthleteDisplayModel _selectedAthlete;
        private MeetDisplayModel _selectedMeet;
        private EventDisplayModel _selectedEvent;
        private ResultDisplayModel _selectedResult;
        #endregion

        #region CONSTRUCTORS

        public ResultViewModel()
        {
            Results = new BindingList<ResultDisplayModel>(GetAllResults());
            Athletes = new BindingList<AthleteDisplayModel>(GetAllAthletes());
            Meets = new BindingList<MeetDisplayModel>(GetAllMeets());
            EventList = new BindingList<EventDisplayModel>(GetAllEvents());
        }


        #endregion

        #region PUBLIC PROPERTIES

        public AthleteDisplayModel SelectedAthlete
        {
            get { return _selectedAthlete; }
            set
            {
                _selectedAthlete = value;
                NotifyOfPropertyChange(() => SelectedAthlete);
            }
        }

        public MeetDisplayModel SelectedMeet
        {
            get { return _selectedMeet; }
            set
            {
                _selectedMeet = value;
                NotifyOfPropertyChange(() => SelectedMeet);
            }
        }

        public EventDisplayModel SelectedEvent
        {
            get { return _selectedEvent; }
            set
            {
                _selectedEvent = value;
                NotifyOfPropertyChange(() => SelectedEvent);
            }
        }

        public ResultDisplayModel SelectedResult
        {
            get { return _selectedResult; }
            set
            {
                _selectedResult = value;
                NotifyOfPropertyChange(() => SelectedResult);
                UpdateFields();
                NotifyOfPropertyChange(() => CanDelete);
            }
        }

        public BindingList<ResultDisplayModel> Results
        {
            get { return _results; }
            set
            {
                _results = value;
                NotifyOfPropertyChange(() => Results);
            }
        }

        public BindingList<AthleteDisplayModel> Athletes
        {
            get { return _athletes; }
            set
            {
                _athletes = value;
                NotifyOfPropertyChange(() => Athletes);
            }
        }

        public BindingList<MeetDisplayModel> Meets
        {
            get { return _meets; }
            set
            {
                _meets = value;
                NotifyOfPropertyChange(() => Meets);
            }
        }

        public BindingList<EventDisplayModel> EventList
        {
            get { return _eventList; }
            set
            {
                _eventList = value;
                NotifyOfPropertyChange(() => EventList);
            }
        }

        public string Mark
        {
            get { return _mark; }
            set
            {
                _mark = value;
                NotifyOfPropertyChange(() => Mark);
                NotifyFields();
            }
        }

        public bool CanClear
        {
            get
            {
                bool output = false;
                if(Mark?.Length > 0)
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
                if (SelectedResult != null && Results.Count > 0)
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

        public DateTime PerfDate
        {
            get { return _perfDate; }
            set
            {
                _perfDate = value;
                NotifyOfPropertyChange(() => PerfDate);
            }
        }



        #endregion

        #region PUBLIC METHODS

        private bool IsAllFieldsFilled()
        {
            bool output = false;

            if (SelectedAthlete != null && SelectedMeet != null && SelectedEvent != null && Mark?.Length > 2)
            {
                output = true;
            }

            return output;
        }

        public void UpdateFields()
        {
            if (SelectedResult != null)
            {
                //SelectedMeet.Id = SelectedResult.MeetId;
                //SelectedAthlete.Id = SelectedResult.AthleteId;
                //SelectedEvent.Id = SelectedResult.EventId;
                //Mark = SelectedResult.Mark.ToString();
                //PerfDate = SelectedResult.PerfDate;
                SelectedMeet = Meets.Where(x=>x.Id == SelectedResult.MeetId).FirstOrDefault();
                SelectedAthlete = Athletes.Where(x=>x.Id == SelectedResult.AthleteId).FirstOrDefault();
                SelectedEvent = EventList.Where(x=>x.Id == SelectedResult.EventId).FirstOrDefault();
                Mark = SelectedResult.Mark.ToString();
                PerfDate = SelectedResult.PerfDate;
            }
        }

        public void Close()
        {
            this.TryClose();
        }

        public void Clear()
        {
            SelectedAthlete = null;
            SelectedMeet = null;
            SelectedEvent = null;
            Mark = "";
            PerfDate = DateTime.Now.Date;
            SelectedResult = null;
        }

        public void Add()
        {
            isAdding = true;

            //ResultDisplayModel e = Results.Where(x => x.EventName == EventList.Where(x=>x.EventName ) && x.LastName == LastName).FirstOrDefault();
            //if (e == null)
            //{
                SqlDataAccess sql = new SqlDataAccess();
                sql.SaveData<dynamic>("dbo.spResult_Insert",
                    new
                    {
                        AthleteId = SelectedAthlete.Id,
                        MeetId = SelectedMeet.Id,
                        EventId = SelectedEvent.Id,
                        Mark = _mark,
                        PerfDate = _perfDate
                    }, "ADBData");

                Results = new BindingList<ResultDisplayModel>(GetAllResults());
                //NotifyOfPropertyChange(() => Parents);
                Clear();

                //_events.PublishOnUIThread(new ParentChangedEvent());
            //}
            //else
            //{
            //    msg = $"Error: An Event named ({SelectedParent.FullName}) already exist!!!";
            //    MessageBox.Show(msg, "Error");
            //}

            isAdding = false;
        }

        public void Update()
        {
            ResultDisplayModel exists = Results.Where(x => x.Id == SelectedResult.Id).FirstOrDefault();
            if (exists != null)
            {
                if (SelectedResult != null && Results.Count > 0)
                {
                    isUpdating = true;

                    ResultModel e = new ResultModel
                    {
                        Id = SelectedResult.Id,
                        AthleteId = SelectedResult.AthleteId,
                        MeetId = SelectedResult.MeetId,
                        EventId = SelectedResult.EventId,
                        Mark = int.Parse(_mark),
                        PerfDate = _perfDate

                    };

                    SqlDataAccess sql = new SqlDataAccess();
                    sql.UpdateData<ResultModel>("dbo.spResult_Update", e, "ADBData");

                    msg = $"Result ({SelectedResult.FullName}) was successfully updated.";
                    MessageBox.Show(msg, "Result Updated");
                    Results = new BindingList<ResultDisplayModel>(GetAllResults());
                    Clear();

                    isUpdating = false;
                }

            }

            //_events.PublishOnUIThread(new ParentChangedEvent());
        }

        public void Delete()
        {
            ResultDisplayModel e = Results.Where(x => x.Id == SelectedResult.Id).FirstOrDefault();
            if (e != null)
            {
                SqlDataAccess sql = new SqlDataAccess();
                sql.DeleteData<dynamic>("dbo.spResult_Delete", new { Id = SelectedResult.Id }, "ADBData");

                Results = new BindingList<ResultDisplayModel>(GetAllResults());
                SelectedResult = null;
                Clear();

                //_events.PublishOnUIThread(new ParentChangedEvent());
            }
        }
        #endregion

        #region PRIVATE METHODS

        private void NotifyFields()
        {
            NotifyOfPropertyChange(() => CanAdd);
            NotifyOfPropertyChange(() => CanUpdate);
            NotifyOfPropertyChange(() => CanClear);
        }

        private List<ResultDisplayModel> GetAllResults()
        {
            SqlDataAccess sql = new SqlDataAccess();
            var output = sql.LoadData<ResultDisplayModel, dynamic>("dbo.spResult_GetAll", new { }, "ADBData");
            return output;
        }

        private List<AthleteDisplayModel> GetAllAthletes()
        {
            SqlDataAccess sql = new SqlDataAccess();
            var output = sql.LoadData<AthleteDisplayModel, dynamic>("dbo.spAthlete_GetAll", new { }, "ADBData");
            return output;
        }

        private List<MeetDisplayModel> GetAllMeets()
        {
            SqlDataAccess sql = new SqlDataAccess();
            var output = sql.LoadData<MeetDisplayModel, dynamic>("dbo.spMeet_GetAll", new { }, "ADBData");
            return output;
        }

        private List<EventDisplayModel> GetAllEvents()
        {
            SqlDataAccess sql = new SqlDataAccess();
            var output = sql.LoadData<EventDisplayModel, dynamic>("dbo.spEvent_GetAll", new { }, "ADBData");
            return output;
        }
        #endregion
    }
}
