using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Webdictaat.Apis.Databases.ViewModel
{
    public class SubmissionVM
    {
        public int AssignmentId { get; set; }

        public string Email { get; set; }

        public string Query { get; set; }

        public int StatusId { get; set; }

        public string Message { get; set; }

        public string AssignmentToken { get; set; }
        public DateTime Timestamp { get; internal set; }
    }
}