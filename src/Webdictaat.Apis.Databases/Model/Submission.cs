﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace webs2_api.Model
{

    public class Submission
    {
        public int AssignmentId { get; set; }

        public string Email { get; set; }

        public string Query { get; set; }

        public int StatusId { get; set; }

        public string Message { get; set; }

        public DateTime TimeStamp { get; set; }

    }

    
}
