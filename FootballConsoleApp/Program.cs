using System;

namespace FootballConsoleApp
{
    internal class Program
    {
        public static void Main()
        {
            var processor=new GameProcessor();

            processor.Game.OnGoal += Logger.AddToGoalList;
            processor.Game.OnBreakRules += Logger.AddToBreakList;

            Console.WriteLine("---Welcome to football game!---\n");
            Console.WriteLine("Do you want to get fast result? y/n");

            var answer = Console.ReadLine();

            if (answer != null && answer.Equals("y"))
            {
                processor.Game.GetFastResult();
            }
            else
            {
                Console.Clear();
                processor.StartGame();
                Logger.ShowReportForTheGame(processor.Game);
            }

            Console.ReadKey();
        }
    }
}