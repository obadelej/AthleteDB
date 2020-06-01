using AthleteDBUI.EventModels;
using AthleteDBUI.Library.DataAccess;
using AthleteDBUI.Library.Models;
using AthleteDBUI.Models;
using Caliburn.Micro;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace AthleteDBUI.ViewModels
{
    public class ResultViewModel : Screen
    {
        #region PRIVATE BACKING FIELDS
        private bool isAdding;
        private bool isUpdating;

        private string _mark;
        private string _wind;
        private DateTime _perfDate;
        private int _resultId;
        private int _athleteId;
        private int _meetId;
        private int _eventId;
        private string _evtName;
        private string msg;

        private BindingList<ResultDisplayModel> _results;
        private BindingList<AthleteDisplayModel> _athletes;
        private BindingList<MeetDisplayModel> _meets;
        private BindingList<EventDisplayModel> _eventList;
        private AthleteDisplayModel _selectedAthlete;
        private MeetDisplayModel _selectedMeet;
        private EventDisplayModel _selectedEvent;
        private ResultDisplayModel _selectedResult;
        //IEventAggregator _events;


        List<ResultModel> resultsToSave = new List<ResultModel>();
        List<ResultImportModel> importedResults = new List<ResultImportModel>();
        int id = 0, athleteId = 0, eventId = 0, meetId = 0, meetDay = 0;
        float mark=0.00f;
        
        DateTime perfDate, startDate = DateTime.Now, endDate = DateTime.Now;
        string eventCode = "", meetName = "", firstName = "", lastName = "", wind = "";

        ResultImportModel result;
        ResultModel resultToSave;

        
        #endregion

        #region CONSTRUCTORS

        public ResultViewModel()
        {
            Results = new BindingList<ResultDisplayModel>(GetAllResults());
            Athletes = new BindingList<AthleteDisplayModel>(GetAllAthletes());
            Meets = new BindingList<MeetDisplayModel>(GetAllMeets());
            EventList = new BindingList<EventDisplayModel>(GetAllEvents());

            //_events = events;

           
        }


        #endregion

        #region PUBLIC PROPERTIES

        public string EvtName
        {
            get { return _evtName; }
            set
            {
                _evtName = value;
                NotifyOfPropertyChange(() => EvtName);
            }
        }

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
                if(value != null)
                {
                    MarkDisplay = ConvertFloatToMark(value.Mark, GetResultEventName());
                    NotifyOfPropertyChange(() => MarkDisplay);
                }
                
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

        public string MarkDisplay
        {
            get { return _mark; }
            set
            {
                _mark = value;
                NotifyOfPropertyChange(() => MarkDisplay);
                NotifyFields();
            }
        }

        public string Wind
        {
            get { return _wind; }
            set
            {
                if(string.IsNullOrWhiteSpace(value))
                {
                    _wind = "NWI";
                }
                else
                {
                    _wind = value;                    
                }

                NotifyOfPropertyChange(() => Wind);
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

        public void Import()
        {
            resultsToSave = new List<ResultModel>();
            id = 0; athleteId = 0; eventId = 0; meetId = 0; meetDay = 0;
            importedResults = new List<ResultImportModel>();

            mark = 0.00f;

            perfDate = DateTime.Now; startDate = DateTime.Now; endDate = DateTime.Now;
            eventCode = ""; meetName = ""; firstName = ""; lastName = ""; wind = "";


            result = new ResultImportModel();
            resultToSave = new ResultModel();

            string filePath = @"C:\Exported_MM_Results_CSV";
            OpenFileDialog openFD = new OpenFileDialog();
            openFD.InitialDirectory = filePath;
            if (openFD.ShowDialog() == true)
            {

                string file = openFD.FileName;

                List<string> lines = File.ReadAllLines(file).ToList();

                foreach (var line in lines)
                {
                    string[] rec = line.Split(';');

                    if (rec[0] == "H")
                    {
                        meetName = rec[1];
                        startDate = DateTime.Parse(rec[2]);
                        endDate = DateTime.Parse(rec[3]);

                        var meet = Meets.Where(x => x.MeetName == meetName).FirstOrDefault();
                        if (meet == null)
                        {
                            MeetModel m = new MeetModel()
                            {
                                MeetName = meetName,
                                StartDate = startDate,
                                EndDate = endDate,                                
                                Location = ""
                            };

                            SqlDataAccess sql = new SqlDataAccess();
                            sql.SaveData<dynamic>("dbo.spMeet_Insert", new
                            {
                                MeetName = m.MeetName,
                                StartDate = m.StartDate,
                                EndDate = m.EndDate,
                                Location = ""
                            }, "ADBData");
                        }

                        Meets = new BindingList<MeetDisplayModel>(GetAllMeets());
                    }
                    else if (rec[0] == "E" && rec[1] == "T")
                    {

                        if (rec[10] == "DNS" || rec[10] == "DQ" || rec[10] == "FS" || rec[10] == "NT" || rec[10] == "SCR" || rec[10] == "DNF")
                        {

                        }
                        else
                        {
                            if (rec[4] == "100" || rec[4] == "200" || rec[4] == "100H" || rec[4] == "110H")
                            {
                                string mk = rec[10];
                                string[] ts = mk.Split(':');

                                if (ts.GetLength(0) == 1)
                                {
                                    mark = float.Parse(rec[10]);
                                }
                                else
                                {
                                    var secs = float.Parse(ts[0]) * 60;
                                    secs += float.Parse(ts[1]);
                                }
                            }                            
                            else if (rec[4] == "400" || rec[4] == "400H")
                            {
                                string temp = rec[10];

                                string[] ts = temp.Split(':');

                                if (ts.GetLength(0) > 1)
                                {
                                    var secs = float.Parse(ts[0]) * 60;
                                    secs += float.Parse(ts[1]);

                                    mark = secs;
                                }
                                else
                                {
                                    mark = float.Parse(rec[10]);
                                }
                            }
                            else if (rec[4] == "800" || rec[4] == "1500")
                            {
                                string temp = rec[10];
                                string[] ts = temp.Split(':');
                                var secs = float.Parse(ts[0]) * 60;
                                secs += float.Parse(ts[1]);

                                mark = secs;
                            }

                            firstName = rec[23];
                            lastName = rec[22];
                            eventCode = rec[4] + "M";
                            //mark = float.Parse(rec[10]);
                            wind = rec[12];
                            meetDay = 1;
                            perfDate = startDate;

                            result = new ResultImportModel()
                            {
                                FirstName = firstName,
                                LastName = lastName,
                                EventCode = eventCode,
                                MeetName = meetName,
                                Mark = mark,
                                Wind = wind,
                                MeetDay = 1,
                                PerfDate = startDate,
                                StartDate = startDate,
                                EndDate = endDate
                            };

                            importedResults.Add(result);
                        }
                    }
                    else if (rec[0] == "R")
                    {

                    }
                }
            }

            foreach (var athlete in Athletes)
            {
                var aId = athlete.Id;
                firstName = athlete.FirstName;
                lastName = athlete.LastName;
                meetId = Meets.Where(x => x.MeetName == meetName).FirstOrDefault().Id;

                var aResults = importedResults.Where(x => x.FirstName == firstName && x.LastName == lastName).ToList();

                if (aResults.Count > 0)
                {
                    foreach (var rs in aResults)
                    {
                        eventId = GetEventId(rs.EventCode);
                        meetId = GetMeetId(rs.MeetName);
                        athleteId = GetAthleteId(rs.FirstName, rs.LastName);
                        mark = rs.Mark;
                        wind = rs.Wind;
                        meetDay = rs.MeetDay;
                        perfDate = startDate;

                        ResultModel rts = new ResultModel()
                        {
                            AthleteId = athleteId,
                            EventId = eventId,
                            MeetId = meetId,
                            Mark = mark,
                            Wind = wind,                            
                            PerfDate = perfDate
                        };

                        SqlDataAccess sql = new SqlDataAccess();
                        sql.SaveData<dynamic>("dbo.spResult_Insert", new
                        {
                            rts.AthleteId,
                            rts.EventId,
                            rts.MeetId,
                            rts.Mark,
                            rts.Wind,                            
                            rts.PerfDate
                        }, "ADBData");
                    }
                }
            }

            Meets = new BindingList<MeetDisplayModel>(GetAllMeets());
            Results = new BindingList<ResultDisplayModel>(GetAllResults());
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
                Wind = SelectedResult.Wind;
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
            MarkDisplay = "";
            Wind = "";
            PerfDate = DateTime.Now.Date;

            SelectedResult = null;
        }

        public void Add()
        {
            isAdding = true;

            float mark = ConvertMarkToFloat();

            
                SqlDataAccess sql = new SqlDataAccess();
                sql.SaveData<dynamic>("dbo.spResult_Insert",
                    new
                    {
                        AthleteId = SelectedAthlete.Id,
                        MeetId = SelectedMeet.Id,
                        EventId = SelectedEvent.Id,
                        Mark = mark,
                        Wind = _wind,
                        PerfDate = _perfDate
                    }, "ADBData");

                Results = new BindingList<ResultDisplayModel>(GetAllResults());
                //NotifyOfPropertyChange(() => Parents);
                Clear();

                //_events.PublishOnUIThread(new ResultChangedEvent());
            

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

                    float mark = ConvertMarkToFloat();

                    ResultModel e = new ResultModel
                    {
                        Id = SelectedResult.Id,
                        AthleteId = SelectedResult.AthleteId,
                        MeetId = SelectedResult.MeetId,
                        EventId = SelectedResult.EventId,
                        Mark = mark,
                        PerfDate = _perfDate

                    };

                    SqlDataAccess sql = new SqlDataAccess();
                    sql.UpdateData<ResultModel>("dbo.spResult_Update", e, "ADBData");

                    msg = $"Result {SelectedResult.FullName} was successfully updated.";
                    MessageBox.Show(msg, "Result Updated");
                    Results = new BindingList<ResultDisplayModel>(GetAllResults());
                    Clear();

                    isUpdating = false;
                }

            }

            //_events.PublishOnUIThread(new ResultChangedEvent());
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

                //_events.PublishOnUIThread(new ResultChangedEvent());
            }
        }
        #endregion

        #region PRIVATE METHODS

        private float ConvertMarkToFloat()
        {
            float output = 0.00f;
            string eventName = GetEventName();
            if(eventName == "100M" || eventName == "200M" || eventName == "100MH" || eventName=="110H" || eventName == "400M" || 
                eventName == "400MH" || eventName == "800M" || eventName == "1500M" || eventName == "3000M" || eventName == "5000M"
                || eventName == "Long Jump" || eventName == "High Jump" || eventName == "Triple Jump" || eventName == "Pole Vault"
                || eventName == "Javelin" || eventName=="Shot Put" || eventName == "Discus" || eventName=="Hammer")
            {
                string[] t = Mark.Split(':');

                if(t.Length == 1)
                {
                    output = float.Parse(Mark);
                    return output;

                } 
                else if (t.Length == 2)
                {
                    float mins = float.Parse(t[0]);
                    output = mins * 60;
                    float secs = float.Parse(t[1]);
                    output += secs;
                    return output;
                }
                
            }

            return output;
        }

        private string ConvertFloatToMark(float mrk, string eventName)
        {
            string output = "";
            if (eventName == "100M" || eventName == "200M" || eventName == "100MH" || eventName == "110H" || eventName == "400M" ||
                eventName == "400MH" || eventName == "800M" || eventName == "1500M" || eventName == "3000M" || eventName == "5000M")
            {

                if (mrk > 59)
                {
                    int mins = (int)mrk / 60;
                    float secs = mrk % 60;

                    if (secs < 10)
                    {
                        output = $"{mins}:0{Math.Round(secs, 2)}";
                        return output;
                    }
                    else
                    {
                        output = $"{mins}:{Math.Round(secs, 2)}";
                        return output;
                    }

                }
                else
                {
                    output = mrk.ToString();
                    return output;
                }

            }
            else
            {
                output = mrk.ToString();
                return output;
            }

            //return output;
        }

        private string GetEventName()
        {
            string eventName = EventList.Where(x => x.Id == SelectedEvent.Id).FirstOrDefault().EventName;
            return eventName;
        }

        private string GetResultEventName()
        {
            string eventName = EventList.Where(x => x.Id == SelectedResult.EventId).FirstOrDefault().EventName;
            return eventName;
        }

        private bool IsAllFieldsFilled()
        {
            bool output = false;

            if (SelectedAthlete != null && SelectedMeet != null && SelectedEvent != null && Mark?.Length > 2)
            {
                output = true;
            }

            return output;
        }

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


        private int GetAthleteId(string firstName, string lastName)
        {
            int id = 0;
            id = Athletes.Where(x => x.FirstName == firstName && x.LastName == lastName).FirstOrDefault().Id;
            return id;
        }

        private int GetMeetId(string meetName)
        {
            int id = 0;
            id = Meets.Where(x => x.MeetName == meetName).Any() ? Meets.Where(x => x.MeetName == meetName).FirstOrDefault().Id : 0;
            return id;
        }

        private int GetEventId(string eventName)
        {
            int id = 0;
            id = EventList.Where(x => x.EventName == eventName).FirstOrDefault().Id;
            return id;
        }






        #endregion
    }
}
