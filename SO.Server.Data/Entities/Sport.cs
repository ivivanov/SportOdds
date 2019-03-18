using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SO.Server.Data.Entities
{
    public class Sport
    {
        public Sport()
        {
            Events = new HashSet<Event>();
        }

        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<Event> Events { get; set; }

    }
}
