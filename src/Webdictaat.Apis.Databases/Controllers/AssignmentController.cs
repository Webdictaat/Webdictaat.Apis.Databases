using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using webs2_api.Model;

namespace webs2_api.Controllers
{
   

    [Route("api/[controller]")]
    public class AssignmentController : Controller
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="assignmentId"></param>
        /// <param name="email"></param>
        /// <param name="query"></param>
        [HttpPost]
        public AssignmentVM Post([FromBody] AssignmentFormVM form)
        {
            return new AssignmentVM()
            {
                AssignmentId = form.AssignmentId,
                Query = form.Query,
                Email = form.Email,
                Status = AssignmentStatus.APPROVED
                
            };
        }


    }
}
