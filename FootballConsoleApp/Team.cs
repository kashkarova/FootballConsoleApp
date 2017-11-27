using System;
using System.Collections.Generic;
using System.Linq;

namespace FootballConsoleApp
{
    [Serializable]
    public class Team
    {
        public string Title { get; set; }

        public Coach Coach { get; set; }

        public double TeamSkill { get; set; }

        public List<Footballer> Players { get; set; }

        public Team()
        {
            Coach = new Coach();
            Players = new List<Footballer>();
        }

        public Team(string title, Coach coach, List<Footballer> players)
        {
            Title = title;
            Coach = coach;
            Players = players;
            TeamSkill = SetTeamSkill();
        }

        public void AddFootballerToTeam(Footballer footballer)
        {
            if (Players.Count < 11)
                Players.Add(footballer);
            else
                throw new CustomException("The team is full");
        }

        public List<Footballer> GetFootballersWithTheirSkills()
        {
            return Players.OrderByDescending(item => item.Skill).ToList();
        }

        private double SetTeamSkill()
        {
            var playersSkill = Players.Sum(player => player.Skill);

            return playersSkill * Coach.LuckyCoefficient;
        }

    }
}