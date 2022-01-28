using Microsoft.AspNetCore.Mvc;
using SubmissionsMongodb.Models;
using SubmissionsMongodb.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SubmissionsMongodb.Controllers
{ 
    public class SubmissionController : Controller
    {
        private readonly SubmissionService _subSvc;

        public SubmissionController(SubmissionService submissionService)
        {
            _subSvc = submissionService;
        }

        public ActionResult<IList<Submissions>> Index() =>
            View(_subSvc.Read());

        [HttpGet]
        public ActionResult Create() => View();

        [HttpPost]
        public ActionResult<Submissions> Create(Submissions submission)
        {
            submission.Created = submission.LastUpdated = DateTime.Now;
            if (ModelState.IsValid)
            {
                _subSvc.Create(submission);
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult<Submissions> Edit(string id) =>
            View(_subSvc.Find(id));

        [HttpPost]
        public ActionResult Edit(Submissions submission)
        {
            submission.LastUpdated = DateTime.Now;
            submission.Created = submission.Created.ToLocalTime();
            if (ModelState.IsValid)
            {
                _subSvc.Update(submission);
                return RedirectToAction("Index");
            }
            return View(submission);
        }

        [HttpGet]
        public ActionResult Delete(string id)
        {
            _subSvc.Delete(id);
            return RedirectToAction("Index");
        }


    }
}
