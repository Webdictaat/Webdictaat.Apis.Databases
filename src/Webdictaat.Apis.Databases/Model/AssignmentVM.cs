using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace webs2_api.Model
{

    public class AssignmentFormVM
    {
        public int AssignmentId { get; set; }

        public string Email { get; set; }

        public string Query { get; set; }
    }

    public enum AssignmentStatus
    {
        NOTPROCESSED = 0,
        APPROVED = 1,
        REJECTED = 2,
        INCORRECT = 3,
        SUCCESS = 4
    }

    public class AssignmentVM : AssignmentFormVM
    {
        public AssignmentStatus Status { get; set; }

        public string VAlidationMessage { get; set; }
    }
    
}
