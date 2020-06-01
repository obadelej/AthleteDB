using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
//using System.Web.Configuration;
using AthleteDBUI.Helpers;

namespace AthleteDBUI.ViewModels
{
    public class AboutViewModel : Screen
    {
        #region Private Backing Fields
        private string _productName;
        private string _version;
        private string _copyright;
        private string _company;
        private string _description;

        private AssemblyInfo _info; 
        #endregion

        #region CONSTRUCTORS
        public AboutViewModel()
        {
            _info = new AssemblyInfo();
            ProductName = _info.Product;
            Version = _info.AssemblyVersion;
            Copyright = _info.Copyright;
            Company = _info.Company;
            Description = _info.Description;

        }
        #endregion

        #region PROPERTIES

        public string ProductName
        {
            get { return _productName; }
            set
            {
                _productName = value;
                NotifyOfPropertyChange(() => ProductName);
            }
        }

        public string Version
        {
            get { return _version; }
            set
            {
                _version = value;
                NotifyOfPropertyChange(() => Version);
            }
        }

        public string Copyright
        {
            get { return _copyright; }
            set
            {
                _copyright = value;
                NotifyOfPropertyChange(() => Copyright);
            }
        }

        public string Company
        {
            get { return _company; }
            set
            {
                _company = value;
                NotifyOfPropertyChange(() => Company);
            }
        }

        public string Description
        {
            get { return _description;}
            set
            {
                _description = value;
                NotifyOfPropertyChange(() => Description);
            }
        }
        #endregion
    }
}
