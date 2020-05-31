using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Codenation.Challenge.Exceptions;

namespace Codenation.Challenge
{
    public class SoccerTeamsManager : IManageSoccerTeams
    {
        private List<Team> _teams;
        private List<Player> _players;
        public SoccerTeamsManager()
        {
            _players = new List<Player>();
            _teams = new List<Team>();
        }

        public void AddTeam(long id, string name, DateTime createDate, string mainShirtColor, string secondaryShirtColor)
        {
            if (_teams.Any(a => a.Id == id))
                throw new UniqueIdentifierException();

            Team team = new Team(id, name, createDate, mainShirtColor, secondaryShirtColor);

            _teams.Add(team);
        }

        public void AddPlayer(long id, long teamId, string name, DateTime birthDate, int skillLevel, decimal salary)
        {
            if (_players.Any(a => a.Id == id))
                throw new UniqueIdentifierException();

            if (!_teams.Any(a => a.Id == teamId))
                throw new TeamNotFoundException();

            Player player = new Player(id, teamId, name, birthDate, skillLevel, salary);
            Team team = _teams.Where(w => w.Id == player.TeamId).First();

            _players.Add(player);
            team.Players.Add(player.Id);
        }

        public void SetCaptain(long playerId)
        {
            if (!_players.Any(a => a.Id == playerId))
                throw new PlayerNotFoundException();

            Player newCaptain = _players.Where(w => w.Id == playerId).First();
            Team team = _teams.Where(w => w.Id == newCaptain.TeamId).First();

            team.Captain = newCaptain.Id;

        }

        public long GetTeamCaptain(long teamId)
        {
            if (!_teams.Any(a => a.Id == teamId))
                throw new TeamNotFoundException();

            Team team = _teams.Where(w => w.Id == teamId).First();

            if (team.Captain == 0)
                throw new CaptainNotFoundException();

            return team.Captain;
        }

        public string GetPlayerName(long playerId)
        {
            if (_players.Any(a => a.Id == playerId))
                throw new PlayerNotFoundException();

            Player player = _players.Where(w => w.Id == playerId).First();
            return player.Name;
        }

        public string GetTeamName(long teamId)
        {
            if (!_teams.Any(a => a.Id == teamId))
                throw new TeamNotFoundException();

            Team team = _teams.Where(w => w.Id == teamId).First();
            return team.Name;
        }

        public List<long> GetTeamPlayers(long teamId)
        {
            if (!_teams.Any(a => a.Id == teamId))
                throw new TeamNotFoundException();

            Team team = _teams.Where(w => w.Id == teamId).First();
            team.Players.Sort();
            return team.Players;
        }

        public long GetBestTeamPlayer(long teamId)
        {
            if (!_teams.Any(a => a.Id == teamId))
                throw new TeamNotFoundException();

            Team team = _teams.Where(w => w.Id == teamId).First();
            List<Player> players = new List<Player>();
            
            foreach (long playerId in team.Players)
            {
                players.Add(_players.Where(w => w.Id == playerId).First());
            }

            players.Max(m => m.SkillLevel);

            return _players.OrderByDescending(o => o.SkillLevel).First().Id;
        }

        public long GetOlderTeamPlayer(long teamId)
        {
            if (!_teams.Any(a => a.Id == teamId))
                throw new TeamNotFoundException();

            Team team = _teams.Where(w => w.Id == teamId).First();
            List<Player> players = new List<Player>();

            foreach (long playerId in team.Players)
            {
                players.Add(_players.Where(w => w.Id == playerId).First());
            }
            
            players.Sort((x, y) => DateTime.Compare(x.BirthDate, y.BirthDate));
            
            return players.First().Id;
        }

        public List<long> GetTeams()
        {
            return _teams.OrderBy(o => o.Id).Select(s => s.Id).ToList();
        }

        public long GetHigherSalaryPlayer(long teamId)
        {
            if (!_teams.Any(a => a.Id == teamId))
                throw new TeamNotFoundException();

            Team team = _teams.Where(w => w.Id == teamId).First();
            List<Player> players = new List<Player>();

            foreach (long playerId in team.Players)
            {
                players.Add(_players.Where(w => w.Id == playerId).First());
            }

            return players.OrderByDescending(o => o.Salary).First().Id;
        }

        public decimal GetPlayerSalary(long playerId)
        {
            if (!_players.Any(a => a.Id == playerId))
                throw new PlayerNotFoundException();

            return _players.Where(w => w.Id == playerId).First().Salary;

        }

        public List<long> GetTopPlayers(int top)
        {
            return _players.OrderByDescending(o => o.SkillLevel).Take(top).ToList().Select(s => s.Id).ToList();
        }

        public string GetVisitorShirtColor(long teamId, long visitorTeamId)
        {
            if (!_teams.Any(a => a.Id == teamId))
                throw new TeamNotFoundException();
            
            if (!_teams.Any(a => a.Id == visitorTeamId))
                throw new TeamNotFoundException();

            Team team = _teams.Where(w => w.Id == teamId).First();
            Team visitorTeam = _teams.Where(w => w.Id == visitorTeamId).First();

            if (team.MainShirtColor == visitorTeam.MainShirtColor)
                return visitorTeam.SecondaryShirtColor;
            else
                return visitorTeam.MainShirtColor;
        }
    }
}
