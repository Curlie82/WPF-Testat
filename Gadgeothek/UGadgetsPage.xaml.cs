using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using ch.hsr.wpf.gadgeothek.domain;
using ch.hsr.wpf.gadgeothek.service;
using ch.hsr.wpf.gadgeothek.websocket;
using System.Threading;
using System.Threading.Tasks;


namespace Gadgeothek
{
    public partial class UGadgetsPage : UserControl
    {
        

        private WebSocketClient _client = new WebSocketClient(MainWindow.ServerUrl);
        
        private readonly LibraryAdminService _service = new LibraryAdminService(MainWindow.ServerUrl);
        
        
       
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
            MainWindow.webSocketClient.NotificationReceived += (o, e) =>
            {
                if (e.Notification.Target == typeof(Gadget).Name.ToLower())
                {
                    var modifiedGadget = e.Notification.DataAs<Gadget>();
                    Gadget oldGadget = null;
                    foreach( var gadget in Gadgets)
                    {
                        if (gadget.InventoryNumber == modifiedGadget.InventoryNumber)
                        {
                            oldGadget = gadget;
                        }
                    }
                    oldGadget = modifiedGadget;
                }
            };
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
            dialog.Title = "Gadget bearbeiten";
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
            dialog.Title = "Neues Gadget";
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
