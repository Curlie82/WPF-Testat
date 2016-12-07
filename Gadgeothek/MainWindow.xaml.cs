using System;
using System.Windows;
using ch.hsr.wpf.gadgeothek.websocket;
using ch.hsr.wpf.gadgeothek.domain;
using System.Threading.Tasks;

namespace Gadgeothek
{
    public partial class MainWindow : Window
    {
        public static String ServerUrl = "http://mge7.dev.ifs.hsr.ch";
        public static WebSocketClient webSocketClient;
        public MainWindow()
        {

            webSocketClient = new ch.hsr.wpf.gadgeothek.websocket.WebSocketClient(ServerUrl);
            Console.WriteLine("The app is listening for updates through web sockets");
            InitializeComponent();

            webSocketClient.NotificationReceived += (o, e) =>
            {
                Console.WriteLine("WebSocket::Notification: " + e.Notification.Target + " > " + e.Notification.Type);

                if (e.Notification.Target == typeof(Gadget).Name.ToLower())
                {
                    var gadget = e.Notification.DataAs<Gadget>();
                    // now you can use it as usual...
                    Console.WriteLine("Details: " + gadget);
                }
            };

            // spawn a new background thread in which the websocket client listens to notifications from the server
            var bgTask = webSocketClient.ListenAsync();

            // make sure, the foreground thread of the console app does not terminate
            // (would stop the background thread, too)
            // press CTRL + C in the console window to stop execution
      
        }
    }
}

