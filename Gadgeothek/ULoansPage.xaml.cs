using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ch.hsr.wpf.gadgeothek.domain;
using ch.hsr.wpf.gadgeothek.service;

namespace Gadgeothek
{
    /// <summary>
    /// Interaktionslogik für ULoansPage.xaml
    /// </summary>
    public partial class ULoansPage : UserControl
    {
        public ObservableCollection<Loan> Loans { get; set; }
        private readonly LibraryAdminService _service = new LibraryAdminService(MainWindow.ServerUrl);
        private Loan _selectedLoan = null;
        public Loan SelectedLoan
        {

            get { return _selectedLoan; }
            set
            {
                if (_selectedLoan != null && _selectedLoan.Equals(value))
                {
                    return;
                }
                _selectedLoan = value;
            }
        }
        public ULoansPage()
        {
            Loans = new ObservableCollection<Loan>();
            DataContext = this;
            InitializeComponent();
            LoadData();

        }
        public void LoadData()
        {
            Loans.Clear();
            _service.GetAllLoans().ForEach(l => Loans.Add(l));
        }
    }
}
