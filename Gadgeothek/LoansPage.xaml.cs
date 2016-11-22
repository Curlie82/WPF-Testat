using System;
using System.Collections.ObjectModel;
using System.Windows.Controls;

using ch.hsr.wpf.gadgeothek.domain;

namespace Gadgeothek
{
    /// <summary>
    /// Interaktionslogik für LoansPage.xaml
    /// </summary>
    public partial class LoansPage : Page
    {
        public ObservableCollection<Gadget> Gadgets { get; set; }

        public LoansPage()
        {
            InitializeComponent();
        }
    }
}
