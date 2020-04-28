using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;

namespace AthleteDBUI.ViewModels
{
    public class ShellViewModel : Conductor<object>
    {
        //private AthletesViewModel _athletesVM;
        //private TestViewModel _testVM;
        // private TestResultsViewModel _testResultsVM;
        // private PredictionsViewModel _predictionsVM;
        private ParentViewModel _parentVM;
        private EventViewModel _eventVM;
        private CoachViewModel _coachVM;
        private MeetViewModel _meetVM;
        private SchoolViewModel _schoolVM;
        private AddressViewModel _addressVM;
        //private MeetViewModel _meetVM;
        //private MeetResultsViewModel _meetResultsVM;
        //private TestPBViewModel _testPBVM;
        //private ChartViewModel _chartVM;
        //private AboutViewModel _aboutVM;

        public ShellViewModel(
            //AboutViewModel aboutVM,
            //TestPBViewModel testPBVM,
            //MeetViewModel meetVM,
            //ChartViewModel chartVM,
            //AthletesViewModel athletesVM,

            //TestViewModel testVM,
            //MeetResultsViewModel meetResultsVM,
            //TestResultsViewModel testResultsVM,
            //PredictionsViewModel predictionsVM
            AddressViewModel addressVM,
            SchoolViewModel schoolVM,
            MeetViewModel meetVM,
            ParentViewModel parentVM,
            CoachViewModel coachVM,
            EventViewModel eventVM)
        
        {
            //_athletesVM = athletesVM;
            _parentVM = parentVM;
            _eventVM = eventVM;
            _coachVM = coachVM;
            _meetVM = meetVM;
            _schoolVM = schoolVM;
            _addressVM = addressVM;
            //_testVM = testVM;
            //_meetResultsVM = meetResultsVM;
            //_testResultsVM = testResultsVM;
            //_predictionsVM = predictionsVM;
            //_meetVM = meetVM;
            //_chartVM = chartVM;
            //_testPBVM = testPBVM;
            //_aboutVM = aboutVM;
            //ActivateItem(_athletesVM);
        }

        public void ExitApplication()
        {
            this.TryClose();
        }

        public void AthletesView()
        {
            //ActivateItem(_athletesVM);
        }

        public void TestsView()
        {
            //ActivateItem(_testVM);
        }

        public void TestResultsView()
        {
            //ActivateItem(_testResultsVM);
        }

        public void PredictionsView()
        {
            //ActivateItem(_predictionsVM);
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

        public void MeetResultsView()
        {
            //ActivateItem(_meetResultsVM);
        }

        public void TestPBView()
        {
            //ActivateItem(_testPBVM);
        }

        public void ChartView()
        {
            //ActivateItem(_chartVM);
        }

        public void AboutView()
        {
            //ActivateItem(_aboutVM);
        }
    }
}
