using System;

namespace Airport.Simulator
{
    class Program
    {
        static Logic logic = new Logic();
        static void Main(string[] args)
        {
            while (true)
            {
                logic.WriteCommandsOnConsole();
                string input = Console.ReadLine();
                if (!logic.CheckInput(input)) break;
            }
            logic.WriteBye();
        }
    }
}
