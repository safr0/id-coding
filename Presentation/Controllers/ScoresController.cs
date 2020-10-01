using Data.ViewModel;
using Logic.Services.Report;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Presentation.Controllers
{
	public class ScoresController : Controller
	{
		private ReportService service = new ReportService();

		// GET: Scores
		public ActionResult Index()
		{
			ViewData["States"] = service.States.Select(p => new SelectListItem()
			{
				Value = p.StateId.ToString(),
				Text = p.StateName,
				Selected = (p.StateName.Equals("Victoria", StringComparison.CurrentCultureIgnoreCase))
			})
			.ToList();

			return View(service.GetAllData(null));
		}
		public ActionResult DataGrid(string stateId)
		{
			int id = stateId != null ? int.Parse(stateId) : -1;
			return PartialView(service.GetAllData(id));
		}
	}
}
