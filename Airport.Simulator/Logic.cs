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
        Timer departTimer;

        public bool autoLandState = false;
        bool autoDepartState = false;
        ConsoleColor primaryColor = ConsoleColor.White;
        ConsoleColor secondaryColor = ConsoleColor.Cyan;
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
            landTimer.Interval = 1000;
            landTimer.Enabled = true;

            departTimer = new Timer();
            departTimer.Interval = 1000;
            departTimer.Enabled = true;
        }

        private void TimerDeport_Elapsed(object sender, ElapsedEventArgs e)
        {
            departTimer.Interval = random.Next(4, 11) * 1000;
            connection.Current.InvokeAsync("DepartPlane", Planes[random.Next(0, Planes.Count)]);
        }

        private void TimerLand_Elapsed(object sender, ElapsedEventArgs e)
        {
            landTimer.Interval = random.Next(4, 11) * 1000;
            connection.Current.InvokeAsync("LandPlane", $"Plane{random.Next(100)}");
        }

        public void WriteCommandsOnConsole()
        {
            Console.Clear();

            
            if(connection.Current.State == HubConnectionState.Connected) Console.ForegroundColor = ConsoleColor.Green;
            else Console.ForegroundColor = ConsoleColor.Red;

            Console.WriteLine($"\nConnection: {connection.Current.State}\n");

            Console.ForegroundColor = statesColor;
            Console.WriteLine($"Is Auto Land:           -   {autoLandState}");
            Console.WriteLine($"Is Auto Deport:         -   {autoDepartState}");
            Console.WriteLine($"Planes In The Garage:   -   {Planes.Count} planes\n");

            Console.ForegroundColor = primaryColor;
            Console.WriteLine("-------------------------------------------------------------------------");
            Console.ForegroundColor = secondaryColor;
            Console.WriteLine($"'{Commands.connect}'    -   {Commands.connect.GetEnumDescription()}.");
            Console.WriteLine($"'{Commands.disconnect}' -   {Commands.disconnect.GetEnumDescription()}.");
            Console.WriteLine($"'{Commands.autoland}'   -   {Commands.autoland.GetEnumDescription()}.");
            Console.WriteLine($"'{Commands.autodepart}' -   {Commands.autodepart.GetEnumDescription()}.");
            Console.WriteLine($"'{Commands.land}'       -   {Commands.land.GetEnumDescription()}.");
            Console.WriteLine($"'{Commands.depart}'     -   {Commands.depart.GetEnumDescription()}.");
            Console.WriteLine($"'{Commands.planes}'     -   {Commands.planes.GetEnumDescription()}.");
            Console.WriteLine($"'{Commands.stop}'       -   {Commands.stop.GetEnumDescription()}.");
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

            else if (input == Commands.connect.ToString()) Connect();
            else if (connection.Current.State != HubConnectionState.Connected) Message = "Connect first!";

            else if (input == Commands.disconnect.ToString()) Disconnect();
            else if (input == Commands.autoland.ToString()) AutoLand();
            else if (input == Commands.autodepart.ToString()) AutoDepart();
            else if (input == Commands.land.ToString()) Land();
            else if (input == Commands.depart.ToString()) Depart();
            else if (input == Commands.planes.ToString()) ShowPlanes();

            return true;
        }

        private void Connect()
        {
            if (connection.Current.State != HubConnectionState.Disconnected) return;

            connection.Current.StartAsync().Wait();
            connection.Current.InvokeAsync("GetPlanes");
        }

        private void Disconnect()
        {
            connection.Current.StopAsync().Wait();
            autoLandState = false;
            autoDepartState = false;
            Planes = default;
        }

        public void AutoLand()
        {
            autoLandState = !autoLandState;
            if (autoLandState)
            {
                landTimer.Elapsed += TimerLand_Elapsed;
                landTimer.Start();
            }
            else
            {
                landTimer.Elapsed -= TimerLand_Elapsed;
                landTimer.Stop();
            }
        }

        private void AutoDepart()
        {
            autoDepartState = !autoDepartState;
            if (autoDepartState)
            {
                departTimer.Elapsed += TimerDeport_Elapsed;
                departTimer.Start();
            }
            else
            {
                departTimer.Elapsed -= TimerDeport_Elapsed;
                departTimer.Stop();
            }
        }

        private void Land()
        {
            Console.Write("PlaneName:");
            var input = Console.ReadLine();
            connection.Current.InvokeAsync("LandPlane", input);
            connection.Current.InvokeAsync("GetPlanes");
        }

        private void Depart()
        {
            Console.WriteLine("Spesific palne: ('Enter' for random plane)");
            var input = Console.ReadLine();
            if (input == "") connection.Current.InvokeAsync("DepartPlane", Planes[random.Next(0, Planes.Count)]);
            else connection.Current.InvokeAsync("DepartPlane", new Plane() { PlaneName = input });
            connection.Current.InvokeAsync("GetPlanes");
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