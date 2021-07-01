using System;


namespace  Webdictaat.Apis.Databases.Model
{

    public class Submission
    {
        public int AssignmentId { get; set; }

        public string UserName { get; set; }

        public string Query { get; set; }

        public int StatusId { get; set; }

        public string Message { get; set; }

        public DateTime TimeStamp { get; set; }

    }

    
}
