using ch.hsr.wpf.gadgeothek.domain;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace Gadgeothek
{
    
    
    public partial class GadgetDialog : Window
    {
        private Gadget _gadget;
        public Gadget Gadget { get; set; }

        public GadgetDialog(Gadget gadget)
        {
            Gadget = new Gadget();
            _gadget = gadget;
            Gadget.Name = _gadget.Name;
            Gadget.Manufacturer = _gadget.Manufacturer;
            Gadget.Price = _gadget.Price;
            Gadget.Condition = _gadget.Condition;
            DataContext = Gadget;
            InitializeComponent();
        }

        private void submit_Click(object sender, RoutedEventArgs e)
        {
            _gadget.Name = Gadget.Name;
            _gadget.Manufacturer = Gadget.Manufacturer;
            _gadget.Price = Gadget.Price;
            _gadget.Condition = Gadget.Condition;
            DialogResult = true;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
