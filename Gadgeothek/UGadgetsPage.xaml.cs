using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using ch.hsr.wpf.gadgeothek.domain;
using ch.hsr.wpf.gadgeothek.service;

namespace Gadgeothek
{
    public partial class UGadgetsPage : UserControl
    {
        private readonly LibraryAdminService _service = new LibraryAdminService("http://mge7.dev.ifs.hsr.ch");
        private Gadget _selectedGadget = null;

        public Gadget SelectedGadget
        {

            get { return _selectedGadget; }
            set
            {
                if (_selectedGadget != null && _selectedGadget.Equals(value))
                {
                    return;
                }
                _selectedGadget = value;
            }
        }

        public ObservableCollection<Gadget> Gadgets { get; set; }


        public UGadgetsPage()
        {
            Gadgets = new ObservableCollection<Gadget>();
            DataContext = this;
            InitializeComponent();
            LoadData();
        }

        

        public void LoadData()
        {
            Gadgets.Clear();
            _service.GetAllGadgets().ForEach(g => Gadgets.Add(g));
        }


        private void ButtonDelete_OnClick(object sender, RoutedEventArgs e)
        {
            if (SelectedGadget != null)
            {
                string message = $"Are you sure that you want to delete the {Environment.NewLine}{SelectedGadget.Name} with ID {SelectedGadget.InventoryNumber}";
                string title = "Are you sure?";
                if (MessageBox.Show(message, title, MessageBoxButton.YesNo, MessageBoxImage.Question) ==
                    MessageBoxResult.Yes)
                {
                    _service.DeleteGadget(SelectedGadget);
                    LoadData();
                }
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            var gadgetToEdit = (Gadget)DgGadgets.SelectedItem;
            var dialog = new GadgetDialog(gadgetToEdit);
            dialog.ActionText.Text = "Gadget ändern";
            if (dialog.ShowDialog() == false)
            {
                return;
            }
            _service.UpdateGadget(gadgetToEdit);
            LoadData();
            DgGadgets.SelectedItem = gadgetToEdit;
        }

        private void addNewGadget_Click(object sender, RoutedEventArgs e)
        {
            var newGadget = new Gadget("");
            var dialog = new GadgetDialog(newGadget);
            dialog.ActionText.Text = "Gadget erstellen";
            if (dialog.ShowDialog() == false)
            {
                return;
            }
            _service.AddGadget(newGadget);
            LoadData();
            DgGadgets.SelectedItem = newGadget;
        }
    }
}
