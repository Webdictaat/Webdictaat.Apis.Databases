using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Webdictaat.Apis.Databases.ViewModel
{
    public class AssignmentVM
    {
        public int AssignmentId { get; set; }

        public string ExpectedOutput { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
    }
}