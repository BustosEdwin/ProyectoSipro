namespace Sipro.Controllers
{

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    public class KpiController : Controller
    {
        // GET: Kpi
        [HttpGet]
        [Authorize]
        public ActionResult InicioKpi()
        {
            return View();
        }
    }
}