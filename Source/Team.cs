using System;
using System.Collections.Generic;

namespace Codenation.Challenge
{
    class Team
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public DateTime CreateDate { get; set; }
        public string MainShirtColor { get; set; }
        public string SecondaryShirtColor { get; set; }
        public long Captain { get; set; }
        public List<long> Players { get; set; }

        public Team(long id, string name, DateTime createDate, string mainShirtColor, string secondaryShirtColor)
        {
            Id = id;
            Name = name;
            CreateDate = createDate;
            MainShirtColor = mainShirtColor;
            SecondaryShirtColor = secondaryShirtColor;
            Players = new List<long>();
        }
    }
}
