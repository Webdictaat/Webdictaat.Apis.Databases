using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using webs2_api.Model;
using Webdictaat.Apis.Databases.Model;
using Webdictaat.Apis.Databases.ViewModel;

namespace webs2_api.Controllers
{
   

    [Route("[controller]")]
    public class AssignmentsController : Controller
    {
        private MyDbContext _repo;
        private ISecretService _secretService;

        public AssignmentsController(MyDbContext dbContext, ISecretService secretService)
        {
            _repo = dbContext;
            _secretService = secretService;
        }

        /// <summary>
        /// Returns all the submissions of an assignment
        /// </summary>
        /// <param name="assignmentId"></param>
        /// <returns></returns>
        [HttpGet("{assignmentId}/submissions/{userId}")]
        public SubmissionVM Get(int assignmentId, string userId)
        {

            var submission = _repo.Submissions.Where(s => s.AssignmentId == assignmentId && s.Email == userId).FirstOrDefault();

            if (submission == null)
                return null;

            var response = new SubmissionVM();
            response.AssignmentId = submission.AssignmentId;
            response.Email = submission.Email;
            response.Query = submission.Query;
            response.Message = submission.Message;
            response.StatusId = submission.StatusId;
            
            if (submission.StatusId == 1)

            {
                var assignment = _repo.Assignments.FirstOrDefault(a => a.ID == submission.AssignmentId);
                response.AssignmentToken = _secretService.GetAssignmentToken(response.Email, assignment.OriginalAssignmentId);
            }

            return response;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="assignmentId"></param>
        /// <param name="submissionForm"></param>
        [HttpPost("{assignmentId}/submissions")]
        public SubmissionVM Post(int assignmentId, [FromBody] SubmissionVM form)
        {
            var submission = _repo.Submissions.Where(s => s.AssignmentId == assignmentId && s.Email == form.Email).FirstOrDefault();

            if (submission == null)
            {
                submission = new Submission()
                {
                    Email = form.Email,
                    Query = form.Query,
                    TimeStamp = DateTime.Now,
                    AssignmentId = assignmentId
                };

                _repo.Submissions.Add(submission);
            }
            else
            {
                submission.StatusId = 0;
                submission.Query = form.Query;
                submission.TimeStamp = DateTime.Now;
            }

            _repo.SaveChanges();
            return Get(assignmentId, form.Email);
        }


    }
}
