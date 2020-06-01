using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AthleteDBUI.EventModels;
using AthleteDBUI.WinForms;
using Caliburn.Micro;

namespace AthleteDBUI.ViewModels
{
    public class ShellViewModel : Conductor<object>, 
        IHandle<ParentChangedEvent>, 
        IHandle<AddressChangedEvent>, 
        IHandle<CoachChangedEvent>,
        IHandle<SchoolChangedEvent>,
        IHandle<MeetChangedEvent>,
        IHandle<EventChangedEvent>,
        IHandle<AthleteChangedEvent>
        //IHandle<ResultChangedEvent>
    {

        #region PRIVATE BACKING FIELDS
        IEventAggregator _events;
        private ParentViewModel _parentVM;
        private EventViewModel _eventVM;
        private CoachViewModel _coachVM;
        private MeetViewModel _meetVM;
        private SchoolViewModel _schoolVM;
        private AddressViewModel _addressVM;
        private AthleteViewModel _athleteVM;
        private ResultViewModel _resultVM;
        private ProfileViewModel _profileVM;
        private AboutViewModel _aboutVM;
        //private ProfileReportViewModel _profileReportVM;

        #endregion

        #region CONSTRUCTORS

        public ShellViewModel(
            IEventAggregator events,
            ResultViewModel resultVM,
            AthleteViewModel athleteVM,
            AddressViewModel addressVM,
            SchoolViewModel schoolVM,
            MeetViewModel meetVM,
            ParentViewModel parentVM,
            CoachViewModel coachVM,
            ProfileViewModel profileVM,
            EventViewModel eventVM,
            AboutViewModel aboutVM
            )        
        {
            _events = events;           

            _parentVM = parentVM;            
            _eventVM = eventVM;
            _coachVM = coachVM;
            _meetVM = meetVM;
            _schoolVM = schoolVM;
            _addressVM = addressVM;
            _athleteVM = athleteVM;
            _resultVM = resultVM;
            _profileVM = profileVM;
            _aboutVM = aboutVM;           

            _events.Subscribe(this);
        }

        #endregion

        #region PUBLIC METHODS

        public void ExitApplication()
        {
            this.TryClose();
        }        

        public void EventView()
        {
            ActivateItem(_eventVM);
        }

        public void ParentView()
        {
            ActivateItem(_parentVM);
        }

        public void CoachView()
        {
            ActivateItem(_coachVM);
        }

        public void MeetView()
        {

            ActivateItem(_meetVM);
        }

        public void SchoolView()
        {
            ActivateItem(_schoolVM);
        }

        public void AddressView()
        {
            ActivateItem(_addressVM);
        }

        public void AthleteView()
        {            
            ActivateItem(_athleteVM);
        }       

        public void ResultView()
        {
            ActivateItem(_resultVM);
        }

        public void ProfileView()
        {
            ActivateItem(_profileVM);
        }

        public void PrintProfileView()
        {
            ProfileForm frm = new ProfileForm();
            frm.ShowDialog();
            //ActivateItem(_profileReportVM);
        }

        public void AboutView()
        {
            ActivateItem(_aboutVM);
        }

        #endregion


        #region EVENT HANDLERS

        public void Handle(ParentChangedEvent message)
        {            
            _athleteVM = new AthleteViewModel(_events);
        }

        public void Handle(AddressChangedEvent message)
        {
            _athleteVM = new AthleteViewModel(_events);
        }

        public void Handle(CoachChangedEvent message)
        {
            _athleteVM = new AthleteViewModel(_events);
        }
            

        public void Handle(SchoolChangedEvent message)
        {
            _athleteVM = new AthleteViewModel(_events);            
        }

        public void Handle(MeetChangedEvent message)
        {
            _resultVM = new ResultViewModel();
        }

        public void Handle(EventChangedEvent message)
        {
            _resultVM = new ResultViewModel();
        }

        public void Handle(AthleteChangedEvent message)
        {
            _resultVM = new ResultViewModel();
        }

        //public void Handle(ResultChangedEvent message)
        //{
        //    _resultVM = new ResultViewModel();
        //}

        #endregion
    }
}
