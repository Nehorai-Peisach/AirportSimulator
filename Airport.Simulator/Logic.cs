using Airport.Models;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Timers;

namespace Airport.Simulator
{
    class Logic
    {
        Connection connection;
        Random random;
        Timer landTimer;
        Timer deportTimer;

        bool autoLandState = false;
        bool autoDeportState = false;
        ConsoleColor primaryColor = ConsoleColor.White;
        ConsoleColor secondaryColor = ConsoleColor.Cyan;
        ConsoleColor connectionColor = ConsoleColor.Red;
        ConsoleColor statesColor = ConsoleColor.Yellow;

        public string Message { get; set; }
        public List<Plane> Planes { get; set; }

        public Logic()
        {
            connection = new Connection(this);
            random = new Random();
            Message = "You don't have new messages.";
            Planes = new List<Plane>();

            landTimer = new Timer();
            landTimer.Elapsed += TimerLand_Elapsed;
            landTimer.Interval = 1000;
            landTimer.Enabled = true;

            deportTimer = new Timer();
            deportTimer.Elapsed += TimerDeport_Elapsed;
            deportTimer.Interval = 1000;
            deportTimer.Enabled = true;
        }

        private void TimerDeport_Elapsed(object sender, ElapsedEventArgs e)
        {
            deportTimer.Interval = random.Next(1, 11) * 1000;
            connection.Current.InvokeCoreAsync("DeportPlane", null);
        }

        private void TimerLand_Elapsed(object sender, ElapsedEventArgs e)
        {
            landTimer.Interval = random.Next(1, 11) * 1000;
            connection.Current.InvokeCoreAsync("LandPlane", null);
        }

        public void WriteCommandsOnConsole()
        {
            Console.Clear();

            Console.ForegroundColor = connectionColor;
            Console.WriteLine($"\nConnection: {connection.Current.State}\n");

            Console.ForegroundColor = statesColor;
            Console.WriteLine($"Auto Land:          {autoLandState}");
            Console.WriteLine($"Auto Deport:        {autoDeportState}");
            Console.WriteLine($"Planes In Airport:  {Planes.Count} planes\n");

            Console.ForegroundColor = primaryColor;
            Console.WriteLine("-------------------------------------------------------------------------");
            Console.ForegroundColor = secondaryColor;
            Console.WriteLine($"'{Commands.connect}'    - {Commands.connect.GetEnumDescription()}.");
            Console.WriteLine($"'{Commands.disconnect}' - {Commands.disconnect.GetEnumDescription()}.");
            Console.WriteLine($"'{Commands.autoland}'   - {Commands.autoland.GetEnumDescription()}.");
            Console.WriteLine($"'{Commands.autodeport}' - {Commands.autodeport.GetEnumDescription()}.");
            Console.WriteLine($"'{Commands.land}'       - {Commands.land.GetEnumDescription()}.");
            Console.WriteLine($"'{Commands.deport}'     - {Commands.deport.GetEnumDescription()}.");
            Console.WriteLine($"'{Commands.planes}'     - {Commands.planes.GetEnumDescription()}.");
            Console.WriteLine($"'{Commands.stop}'       - {Commands.stop.GetEnumDescription()}.");
            Console.ForegroundColor = primaryColor;
            Console.WriteLine("-------------------------------------------------------------------------");
            Console.WriteLine(Message);
            Console.WriteLine("-------------------------------------------------------------------------");
            Console.WriteLine("Enter your command:\n");
        }

        internal void WriteBye()
        {
            Console.ForegroundColor = secondaryColor;
            Console.WriteLine("So long, see you around, but never a final Goodbye!");
        }

        public bool CheckInput(string input)
        {
            if (input == Commands.stop.ToString()) return false;

            if (input == Commands.connect.ToString()) Connect();
            if (input == Commands.disconnect.ToString()) Disconnect();
            if (input == Commands.autoland.ToString()) AutoLand();
            if (input == Commands.autodeport.ToString()) AutoDeport();
            if (input == Commands.land.ToString()) Land();
            if (input == Commands.deport.ToString()) Deport();
            if (input == Commands.planes.ToString()) ShowPlanes();

            return true;
        }

        private void Connect()
        {
            connection.Current.StartAsync().Wait();

            connectionColor = ConsoleColor.Green;
        }

        private void Disconnect()
        {
            connection.Current.StopAsync().Wait();
            autoLandState = false;
            autoDeportState = false;
            Planes = default;

            connectionColor = ConsoleColor.Red;
        }

        private void AutoLand()
        {
            autoLandState = !autoLandState;
            //if(autoLandState)
        }

        private void AutoDeport()
        {
            autoDeportState = !autoDeportState;
        }

        private void Land()
        {
            Console.WriteLine("PlaneName:");
            var input = Console.ReadLine();
            connection.Current.InvokeCoreAsync("LandPlane", new[] { input });
        }

        private void Deport()
        {
            connection.Current.InvokeCoreAsync("DeportPlane", null);
        }

        private void ShowPlanes()
        {
            foreach (var plane in Planes)
            {
                Console.WriteLine($"{plane.PlaneName} : {plane.PlaneId}");
            }
            Console.Read();
        }
    }
}