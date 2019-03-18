using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SO.Server.Data.Entities
{
    public class Event
    {
        public Event()
        {
            Matches = new HashSet<Match>();
        }

        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public bool IsLive { get; set; }

        public ICollection<Match> Matches { get; set; }
    }
}