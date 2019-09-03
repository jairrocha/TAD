using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TAD_Treinamento_a_distancia.Controllers
{
    public class SobreController : Controller
    {
        //
        // GET: /Sobre/
        [Authorize]
        public ActionResult Sobre()
        {
            return View();
        }

    }
}
