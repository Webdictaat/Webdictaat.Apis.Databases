
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Webdictaat.Apis.Databases.Model
{
    public class Assignment
    {
        [Key]
        public int ID { get; set; }

        public string ResultsHTML{ get; set; }

        public string Description {get; set;}

        [Column("meta_tag")]
        public string MetaTag {get; set;}


        public virtual ICollection<Submission> Submissions { get; set; }

    }
}
