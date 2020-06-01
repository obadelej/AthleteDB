using AthleteDBUI.Converters;
using AthleteDBUI.Library.DataAccess;
using AthleteDBUI.Models;
using AthleteDBUI.WinForms;
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
        private BindingList<AddressDisplayModel> _addresses;
        private BindingList<ParentDisplayModel> _parents;
        private BindingList<SchoolDisplayModel> _schools;
        private BindingList<ResultDisplayModel> _results;
        private string _evtName;
        private AthleteDisplayModel _selectedAthlete;
        private AddressDisplayModel _selectedAddress;
        private ParentDisplayModel _selectedParent;
        private SchoolDisplayModel _selectedSchool;
        private string _convertedMark;
        private BindingList<ResultDisplayModel> _selectedAthletePBs;
       

        #endregion

        #region CONSTRUCTORS
        public ProfileViewModel()
        {            
            Athletes = new BindingList<AthleteDisplayModel>(GetAllAthletes());
            Addresses = new BindingList<AddressDisplayModel>(GetAllAddresses());
            Parents = new BindingList<ParentDisplayModel>(GetAllParents());
            Schools = new BindingList<SchoolDisplayModel>(GetAllSchools());
            Results = new BindingList<ResultDisplayModel>(GetAllResults());
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

        public string ConvertedMark
        {
            get { return _convertedMark; }
            set
            {
                _convertedMark = value;
                NotifyOfPropertyChange(() => ConvertedMark);
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

        public BindingList<AddressDisplayModel> Addresses
        {
            get { return _addresses; }
            set
            {
                _addresses = value;
                NotifyOfPropertyChange(() => Addresses);
            }
        }

        public BindingList<ParentDisplayModel> Parents
        {
            get { return _parents; }
            set
            {
                _parents = value;
                NotifyOfPropertyChange(() => Parents);
            }
        }

        public BindingList<SchoolDisplayModel> Schools
        {
            get { return _schools; }
            set
            {
                _schools = value;
                NotifyOfPropertyChange(() => Schools);
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



        public AthleteDisplayModel SelectedAthlete
        {
            get { return _selectedAthlete; }
            set
            {
                _selectedAthlete = value;
                SelectedAddress = Addresses.Where(x => x.Id == SelectedAthlete.AddressId).FirstOrDefault();
                SelectedParent = Parents.Where(x => x.Id == SelectedAthlete.ParentId).FirstOrDefault();
                SelectedSchool = Schools.Where(x => x.Id == SelectedAthlete.SchoolId).FirstOrDefault();
                GetSelectedAthletePBs(SelectedAthlete.Id);
                EvtName = SelectedAthletePBs.Where(x => x.AthleteId == SelectedAthlete.Id).FirstOrDefault().EventName;

                NotifyOfPropertyChange(() => SelectedAthlete);
            }
        }

        public AddressDisplayModel SelectedAddress
        {
            get { return _selectedAddress; }
            set
            {
                _selectedAddress = value;
                NotifyOfPropertyChange(() => SelectedAddress);
            }
        }

        public ParentDisplayModel SelectedParent
        {
            get { return _selectedParent; }
            set
            {
                _selectedParent = value;
                NotifyOfPropertyChange(() => SelectedParent);
            }
        }

        public SchoolDisplayModel SelectedSchool
        {
            get { return _selectedSchool; }
            set
            {
                _selectedSchool = value;
                NotifyOfPropertyChange(() => SelectedSchool);
            }
        }

        public BindingList<ResultDisplayModel> SelectedAthletePBs
        {
            get { return _selectedAthletePBs; }
            set
            {
                _selectedAthletePBs = value;                
                NotifyOfPropertyChange(() => SelectedAthletePBs);
            }
        }
        #endregion

        #region PUBLIC METHODS
        public void PrintReport()
        {
            ProfileForm frm = new ProfileForm();
            frm.ShowDialog();
        }

        #endregion

        #region PRIVATE METHODS

        private void ChangePBs()
        {
            foreach(var item in SelectedAthletePBs)
            {
                if(item.Mark > 59)
                {
                    ConvertedMark = TimeConverter.ConvertFloatToMark(item.Mark, item.EventName);
                }
            }
        }

        private IList<AthleteDisplayModel> GetAllAthletes()
        {
            SqlDataAccess sql = new SqlDataAccess();
            var output = sql.LoadData<AthleteDisplayModel, dynamic>("dbo.spAthlete_GetAll", new { }, "ADBData");
            return output; 
        }

        private IList<AddressDisplayModel> GetAllAddresses()
        {
            SqlDataAccess sql = new SqlDataAccess();
            var output = sql.LoadData<AddressDisplayModel, dynamic>("dbo.spAddress_GetAll", new { }, "ADBData");
            return output;
        }

        private IList<ParentDisplayModel> GetAllParents()
        {
            SqlDataAccess sql = new SqlDataAccess();
            var output = sql.LoadData<ParentDisplayModel, dynamic>("dbo.spParent_GetAll", new { }, "ADBData");
            return output;
        }

        private IList<SchoolDisplayModel> GetAllSchools()
        {
            SqlDataAccess sql = new SqlDataAccess();
            var output = sql.LoadData<SchoolDisplayModel, dynamic>("dbo.spSchool_GetAll", new { }, "ADBData");
            return output;
        }

        private IList<ResultDisplayModel> GetAllResults()
        {
            SqlDataAccess sql = new SqlDataAccess();
            var output = sql.LoadData<ResultDisplayModel, dynamic>("dbo.spResult_GetAll", new { }, "ADBData");
            return output;
        }

        private void GetSelectedAthletePBs(int selectedAthleteId)
        {
            //var mResults = GetAllResults();
            var select = Results.Where(x => x.AthleteId == selectedAthleteId).OrderByDescending(x => x.Mark).GroupBy(x => x.EventName);
            var newList = new List<ResultDisplayModel>();

            foreach (var item in select)
            {                
                newList.Add(item.Last());
            }       

            
            SelectedAthletePBs = new BindingList<ResultDisplayModel>(newList.OrderBy(x=>x.EventName).ToList());
            
            NotifyOfPropertyChange(() => SelectedAthletePBs);           
        }


        
        #endregion
    }
}
