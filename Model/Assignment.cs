
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Webdictaat.Apis.Databases.Model
{
    public class Assignment
    {
        [Key]
        public int ID { get; set; }

        public string resultsHTML{ get; set; }

        public int OriginalAssignmentId { get; set; }

        public virtual ICollection<Submission> Submissions { get; set; }

    }
}
