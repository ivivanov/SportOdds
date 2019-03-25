using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SO.Data.Entities
{
    [Table("Sports")]
    public class Sport : IEntity
    {
        public Sport()
        {
            Events = new HashSet<Event>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<Event> Events { get; set; }
    }
}
