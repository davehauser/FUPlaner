using System.Linq;
using AutoMapper;
using FUPlaner.Data;
using FUPlaner.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FUPlaner.Controllers
{
    public class PlansController : Controller
    {
        private readonly IData _data;
        private readonly IMapper _mapper;
        
        public PlansController(IData data, IMapper mapper)
        {
            _data = data;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var plans = _data.Plans.FindAll();
            var viewModel = plans.Select(x => PlanDisplay.From(x, _data)).ToList();
            return View(viewModel);
        }

        [HttpGet]
        [Route("{controller}/{id}")]
        public IActionResult Show(int id) {
            var plan = _data.Plans.FindById(id);
            if(plan == null)
            {
                return RedirectToAction("Index");
            }

            var viewModel = PlanDisplay.From(plan, _data);
            return View(viewModel);
        }
    }
}