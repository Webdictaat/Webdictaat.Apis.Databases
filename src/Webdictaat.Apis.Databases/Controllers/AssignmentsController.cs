using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using webs2_api.Model;
using Webdictaat.Apis.Databases.Model;
using Webdictaat.Apis.Databases.ViewModel;
using Microsoft.Extensions.Logging;

namespace webs2_api.Controllers
{
   

    [Route("[controller]")]
    public class AssignmentsController : Controller
    {
        private MyDbContext _repo;
        private ISecretService _secretService;
        private ILogger<AssignmentsController> _logger;

        public AssignmentsController(MyDbContext dbContext, ISecretService secretService, ILogger<AssignmentsController> logger)
        {
            _repo = dbContext;
            _secretService = secretService;
            _logger = logger;
        }


        /// <summary>
        /// Returns all the submissions of an assignment
        /// </summary>
        /// <param name="assignmentId"></param>
        /// <returns></returns>
        [HttpGet("{assignmentId}")]
        public AssignmentVM Get(int assignmentId)
        {
            var assignment = _repo.Assignments.Where(s => s.ID == assignmentId).FirstOrDefault();

            if (assignment == null)
                return null;

            var response = new AssignmentVM();
            response.AssignmentId = assignment.ID;
            response.ExpectedOutput = assignment.resultsHTML;
            return response;
        }


        /// <summary>
        /// Returns all the submissions of an assignment
        /// </summary>
        /// <param name="assignmentId"></param>
        /// <returns></returns>
        [HttpGet("log")]
        public string TestLog()
        {
            try
            {
                _logger.LogCritical("Test log", new Exception("Test exception"));
                return "The log has been written";
            }
            catch (Exception e)
            {
                return "de log is kapot! :(";
            }
           
        }

        /// <summary>
        /// Returns all the submissions of an assignment
        /// </summary>
        /// <param name="assignmentId"></param>
        /// <returns></returns>
        [HttpGet("{assignmentId}/submissions/{userId}")]
        public SubmissionVM GetSubmission(int assignmentId, string userId)
        {
            try
            {
                
                var submission = _repo.Submissions.Where(s => s.AssignmentId == assignmentId && s.Email == userId).FirstOrDefault();

                if (submission == null)
                    return null;

                var response = new SubmissionVM();
                response.AssignmentId = submission.AssignmentId;
                response.Email = submission.Email;
                response.Query = submission.Query;
                response.Timestamp = submission.TimeStamp;
                response.Message = submission.Message;
                response.StatusId = submission.StatusId;

                if (submission.StatusId == 1)

                {
                    var assignment = _repo.Assignments.FirstOrDefault(a => a.ID == submission.AssignmentId);
                    response.AssignmentToken = _secretService.GetAssignmentToken(response.Email, assignment.ID);
                }

                return response;
            }
            catch (Exception e)
            {
                _logger.LogCritical("add submission", e);
                throw e;
            }
        
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="assignmentId"></param>
        /// <param name="submissionForm"></param>
        [HttpPost("{assignmentId}/submissions")]
        public SubmissionVM Post(int assignmentId, [FromBody] SubmissionVM form)
        {
            using (var transaction = _repo.Database.BeginTransaction())
            {
                try
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

                    // Commit transaction if all commands succeed, transaction will auto-rollback
                    // when disposed if either commands fails
                    transaction.Commit();
                }
                catch (Exception e)
                {
                    _logger.LogCritical("Submit", e);
                    throw e;
                }
            }


            return GetSubmission(assignmentId, form.Email);
        }


    }
}
