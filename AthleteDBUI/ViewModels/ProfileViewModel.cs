using AthleteDBUI.Models;
using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AthleteDBUI.ViewModels
{
    public class ProfileViewModel : Screen
    {
        #region PRIVATE BACKING FIELDS
        private BindingList<AthleteDisplayModel> _athletes;
        private AthleteDisplayModel _selectedAthlete;

        #endregion

        #region CONSTRUCTORS
        public ProfileViewModel()
        {
            Athletes = new BindingList<AthleteDisplayModel>(GetAllAthletes());
        }

        
        #endregion

        #region PUBLIC PROPERTIES

        public BindingList<AthleteDisplayModel> Athletes
        {
            get { return _athletes; }
            set
            {
                _athletes = value;
                NotifyOfPropertyChange(() => Athletes);
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
        #endregion

        #region PUBLIC METHODS

        #endregion

        #region PRIVATE METHODS

        private IList<AthleteDisplayModel> GetAllAthletes()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
