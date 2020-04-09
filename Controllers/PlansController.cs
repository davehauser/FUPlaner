using System;
using System.Linq;
using AutoMapper;
using FUPlaner.Data;
using FUPlaner.Entities;
using FUPlaner.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FUPlaner.Controllers {
    public class PlansController : Controller {
        private readonly IData _data;
        private readonly IMapper _mapper;

        public PlansController (IData data, IMapper mapper) {
            _data = data;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Index () {
            var plans = _data.Plans.FindAll ();
            var viewModel = plans.Select (x => PlanDisplay.From (x, _data)).ToList ();
            return View (viewModel);
        }

        [HttpGet]
        [Route ("{controller}/{id}")]
        public IActionResult Show (int id) {
            var plan = _data.Plans.FindById (id);
            if (plan == null) {
                return RedirectToAction ("Index");
            }

            var viewModel = PlanDisplay.From (plan, _data);
            return View (viewModel);
        }

        [HttpGet]
        public IActionResult Create () {
            var viewModel = PlanFormDisplay.From (new Plan {
                Days = Enumerable.Range (0, 5).Select (x => new Plan.Day ()).ToList ()
            }, _data, _mapper);
            return View (viewModel);
        }

        [HttpPost]
        public IActionResult Create (PlanFormInput form) {
            var plan = form.ToEntity ();
            _data.Plans.Save (plan);
            TempData["messageType"] = "success";
            TempData["messageText"] = "Der Plan wurde erfolgreich erstellt.";
            return RedirectToAction ("Show", new { id = plan.Id});
        }

        [HttpGet]
        public IActionResult Edit (int id) {
            var plan = _data.Plans.FindById (id);
            if (plan == null) {
                return RedirectToAction ("Index");
            }

            var viewModel = PlanFormDisplay.From (plan, _data, _mapper);
            return View (viewModel);
        }

        [HttpPost]
        public IActionResult Edit (PlanFormInput form) {
            var plan = _data.Plans.FindById (form.Id);
            plan = form.UpdateEntity (plan);
            _data.Plans.Save (plan);
            TempData["messageType"] = "success";
            TempData["messageText"] = "Der Plan wurde erfolgreich geändert.";
            return RedirectToAction ("Show", new { id = plan.Id});
        }

        [HttpGet]
        public IActionResult CheckForOverlappingPlans (int level, string start, string end) {
            DateTime.TryParse (start, out var startDate);
            DateTime.TryParse (end, out var endDate);
            var plans = _data.Plans.FindAll ()
                .Where (x => x.Level == level)
                .Where (x => x.Start <= startDate && startDate <= x.End)
                .Where (x => x.Start <= endDate && endDate <= x.End)
                .ToList ();
            var hasPlans = plans.Any ();
            return Json (new { hasOverlappingPlans = hasPlans, plans });
        }
    }
}