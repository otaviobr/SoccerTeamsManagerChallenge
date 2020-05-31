using Codenation.Challenge;
using System;

namespace main
{
    class Program
    {
        static void Main(string[] args)
        {
            var test = new SoccerTeamsManagerTest();
            var manager = new SoccerTeamsManager();

            manager.AddTeam(1, "Time 1", DateTime.Now, "cor 1", "cor 2");
            //manager.AddPlayer(1, 1, "Jogador 1", DateTime.Today, 0, 0);
            //17,22,50,33,50
            //output id = 2

            manager.AddPlayer(1, 1, "Jogador 1", new DateTime(2003, 1, 1), 7, 1509.10m);
            manager.AddPlayer(2, 1, "Jogador 1", new DateTime(1998, 1, 1), 24, 200.20m);
            manager.AddPlayer(3, 1, "Jogador 1", new DateTime(1970, 1, 1), 33, 3300m);
            manager.AddPlayer(4, 1, "Jogador 1", new DateTime(1987, 1, 1), 2, 450020.11m);
            manager.AddPlayer(5, 1, "Jogador 1", new DateTime(1970, 1, 1), 70, 50.0m);


            var teste = manager.GetOlderTeamPlayer(1);

            //Assert.Equal(1, manager.GetTeamCaptain(1));
            //Assert.Throws<PlayerNotFoundException>(() =>
            //    manager.SetCaptain(2));
        }
    }
}
