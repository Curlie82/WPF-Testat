using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows;
using System.Windows.Controls;
using ch.hsr.wpf.gadgeothek.domain;
using ch.hsr.wpf.gadgeothek.service;
using Condition = ch.hsr.wpf.gadgeothek.domain.Condition;

namespace Gadgeothek
{
    /// <summary>
    /// Interaktionslogik für UGadgetsPage.xaml
    /// </summary>
    public partial class UGadgetsPage : UserControl
    {
        private readonly LibraryAdminService _service = new LibraryAdminService("http://mge6.dev.ifs.hsr.ch");
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
            Gadgets.CollectionChanged += GadgetsCollectionChanged;
            
            
        }

        private void GadgetsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            
        }

        public void LoadData()
        {
            Gadgets.Clear();
            _service.GetAllGadgets().ForEach(g => Gadgets.Add(g));
        }



        private void addNewGadget_onClick(object sender, RoutedEventArgs e)
        {
            Gadgets.Add(new Gadget ("TestName")
            {
                Manufacturer = "TestManufacturer",
                Price = 12.00
            });

            DgGadgets.SelectedIndex = Gadgets.Count - 1;
            DgGadgets.CurrentColumn = DgGadgets.Columns[0];
            _service.AddGadget(SelectedGadget);

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
    }

}
