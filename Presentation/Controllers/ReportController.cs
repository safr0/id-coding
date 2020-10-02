using Logic.Services.Report;
using System;
using System.Data;
using System.Linq;
using System.Web.Mvc;

namespace Presentation.Controllers
{
	public class ReportController : Controller
	{		
		private readonly IReportService service= new ReportService();

		// GET: Disadvs
		public ActionResult Index()
		{
			ViewData["States"] = service.States.Select(p => new SelectListItem()
			{
				Value = p.StateId.ToString(),
				Text =  $"{p.StateName}-{p.Median}",
				Selected = (p.StateName.Equals($"Victoria-{p.Median}", StringComparison.CurrentCultureIgnoreCase))
			})
			.ToList();
			return View(service.GetDisadges(null,null));
		}

		public ActionResult ReportGrid(string stateId, string showAll)
		{
			int id = stateId != null ? int.Parse(stateId) : -1;
			int allScore = showAll != null ? int.Parse(showAll) : -1;
			return PartialView(service.GetDisadges(id, allScore));
		}
	}
}
