using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using webs2_api.Model;

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
