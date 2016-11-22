
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using ch.hsr.wpf.gadgeothek.domain;
using ch.hsr.wpf.gadgeothek.service;


namespace Gadgeothek
{
    /// <summary>
    /// Interaktionslogik für GadgetsPage.xaml
    /// </summary>
    public partial class GadgetsPage : Page
    {
        private readonly LibraryAdminService _service = new LibraryAdminService("http://mge3.dev.ifs.hsr.ch");
        bool isUpdateMode = false;
        public Gadget _selectedGadget = null;

        public Gadget SelectedGadget { 

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


        public GadgetsPage()
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


        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            isUpdateMode = true;
       }

        private void DgGadgets_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedGadget = DgGadgets.SelectedItem as Gadget;
        }

       /* private void DgGadgets_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            MessageBox.Show("Update gemacht");
        }*/

        /*private void DgGadgets_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            
            if (isUpdateMode) //The Row is  edited

            {
                TextBox t = e.EditingElement as TextBox;  // Assumes columns are all TextBoxes
                DataGridColumn dgc = e.Column;
            }

        }*/

        private void addGadget_Click(object sender, RoutedEventArgs e)
        {
            
        }


        private void ButtonDelete_OnClick(object sender, RoutedEventArgs e)
        {
            string message = $"Are you sure that you want to delete the {Environment.NewLine}{SelectedGadget.Name} with ID {SelectedGadget.InventoryNumber}";
            string title = "Are you sure?";
            
            if (SelectedGadget != null &&
                MessageBox.Show(message,title,MessageBoxButton.YesNo, MessageBoxImage.Question) ==
                MessageBoxResult.Yes)
            {
                _service.DeleteGadget(SelectedGadget);
                LoadData();
            }


        }
    }

}
