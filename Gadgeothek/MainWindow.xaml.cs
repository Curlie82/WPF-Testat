
using System.Collections.ObjectModel;
using System.Configuration;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using ch.hsr.wpf.gadgeothek.domain;
using ch.hsr.wpf.gadgeothek.service;

namespace Gadgeothek
{

    public partial class MainWindow : Window
    {

        //private string serverName = ConfigurationManager.AppSettings["server"];
      //  private readonly LibraryAdminService _service = new LibraryAdminService("http://mge7.dev.ifs.hsr.ch");


        public MainWindow()
        {
            InitializeComponent();

            Page page = new GadgetsPage();
            GadgetFrame.Navigate(page);
        }

        public MainWindow(TabItem tabItem)
        {
            InitializeComponent();

            if (tabItem.Name == "GadgetItem")
            {
                //IsSelected has to be set manually, else the switching of the tabs won't work properly
                GadgetItem.IsSelected = true;
                LoanItem.IsSelected = false;

            }
            else if (tabItem.Name == "LoanItem")
            {
                GadgetItem.IsSelected = false;
                LoanItem.IsSelected = true;
            }
        }
    }
}

