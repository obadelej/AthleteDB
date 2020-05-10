using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AthleteDBUI.EventModels;
using Caliburn.Micro;

namespace AthleteDBUI.ViewModels
{
    public class ShellViewModel : Conductor<object>, 
        IHandle<ParentChangedEvent>, 
        IHandle<AddressChangedEvent>, 
        IHandle<CoachChangedEvent>,
        IHandle<SchoolChangedEvent>
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
            EventViewModel eventVM)
        
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

        public void AboutView()
        {
            //ActivateItem(_aboutVM);
        }

        #endregion


        #region EVENT HANDLERS

        public void Handle(ParentChangedEvent message)
        {
            _athleteVM = new AthleteViewModel();
        }

        public void Handle(AddressChangedEvent message)
        {
            _athleteVM = new AthleteViewModel();
        }

        public void Handle(CoachChangedEvent message)
        {
            _athleteVM = new AthleteViewModel();
        }
            

        public void Handle(SchoolChangedEvent message)
        {
            _athleteVM = new AthleteViewModel();
        }

        #endregion
    }
}
