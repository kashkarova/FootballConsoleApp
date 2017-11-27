using System;
using System.IO;
using System.Xml.Serialization;

namespace FootballConsoleApp
{
    public class Game
    {
        public delegate void GoalHandler(Footballer footballer);
        public delegate bool BreakingHandler(Footballer footballer);

        public event GoalHandler OnGoal;
        public event BreakingHandler OnBreakRules;

        public Team FirstTeam { get; set; }

        public Team SecondTeam { get; set; }

        public Refferee Refferee { get; set; }

        public Ball Ball { get; set; }

        public double Time { get; set; } //count of seconds

        public int[] Count { get; set; } //Item1 - first team, Item2 - second team

        public Game()
        {
            FirstTeam = LoadTeamFromFile("first_team.xml");
            SecondTeam = LoadTeamFromFile("second_team.xml");

            Refferee = new Refferee();

            Ball = new Ball();

            Time = Constants.TimeForGame;

            Count = new int[2];
        }

        public void MakeGoal(Footballer footballer)
        {
            if (FirstTeam.Title.Equals(footballer.TeamTitle) && Ball.BallOnGoalForSecondTeam())
            {
                Count[0]++;
                Ball.Attacker = SecondTeam.Players.Find(f => f.Role == FootballerRole.Goolkeeper);
                OnGoal?.Invoke(footballer);
            }

            if (SecondTeam.Title.Equals(footballer.TeamTitle) && Ball.BallOnGoalForFirstTeam())
            {
                Count[1]++;
                Ball.Attacker = FirstTeam.Players.Find(f => f.Role == FootballerRole.Goolkeeper);
                OnGoal?.Invoke(footballer);
            }
            Ball.Fly();
        }

        public void BreakingRules(Footballer footballer)
        {
            if (footballer.Role == FootballerRole.Goolkeeper)
                return;

            var firstGuardZone = Constants.Default1GoolkeeperPosition;
            var secondGuardZone = Constants.Default2GoolkeeperPosition;

            if (!(firstGuardZone.Distance(footballer.Position) <= 2 || secondGuardZone.Distance(footballer.Position) <= 2))
                return;

            OnBreakRules(footballer);
            DisqualifyFootballer(footballer);
        }

        public void GetFastResult()
        {
            if (Refferee.CheatLevel == 1)
            {
                FirstTeam.TeamSkill += FirstTeam.TeamSkill * 0.05;
                Console.WriteLine("First team cheats!");
            }

            if (Refferee.CheatLevel == 2)
            {
                SecondTeam.TeamSkill += SecondTeam.TeamSkill * 0.05;
                Console.WriteLine("Second team cheats!");
            }

            if (FirstTeam.TeamSkill > SecondTeam.TeamSkill * 1.1)
                Console.WriteLine("First team can win!");
            else if (SecondTeam.TeamSkill > FirstTeam.TeamSkill * 1.1)
                Console.WriteLine("Second team can win");
            else
                Console.WriteLine("Chances are the same!");
        }

        private void DisqualifyFootballer(Footballer footballer)
        {
            if (FirstTeam.Title.Equals(footballer.TeamTitle))
                FirstTeam.Players.Remove(footballer);
            else
                SecondTeam.Players.Remove(footballer);

            Ball.Position = Constants.DefaultBallPosition;
        }

        private Team LoadTeamFromFile(string fileName)
        {
            Team team;

            using (var fs = new FileStream(fileName, FileMode.OpenOrCreate))
            {
                var formatter = new XmlSerializer(typeof(Team));
                team = (Team)formatter.Deserialize(fs);
            }
            return team;
        }
    }
}