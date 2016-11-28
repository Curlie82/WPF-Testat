using ch.hsr.wpf.gadgeothek.domain;
using System.Windows;

namespace Gadgeothek
{

    public partial class GadgetDialog : Window
    {
        private readonly Gadget _gadget;
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

        private void submit_onClick(object sender, RoutedEventArgs e)
        {
            _gadget.Name = Gadget.Name;
            _gadget.Manufacturer = Gadget.Manufacturer;
            _gadget.Price = Gadget.Price;
            _gadget.Condition = Gadget.Condition;
            DialogResult = true;
        }

        private void Cancel_onClick(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
