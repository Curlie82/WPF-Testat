
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace Gadgeothek
{

    public partial class MainWindow : Window
    {
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

