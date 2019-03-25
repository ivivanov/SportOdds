using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SO.Data.Entities
{
    [Table("Events")]
    public class Event : IEntity
    {
        public Event()
        {
            Matches = new HashSet<Match>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [ForeignKey("Sport")]
        public int SportId { get; set; }

        public Sport Sport { get; set; }

        public string Name { get; set; }

        public bool IsLive { get; set; }

        public virtual ICollection<Match> Matches { get; set; }
    }
}