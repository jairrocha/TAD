using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TAD_Treinamento_a_distancia.Models;

namespace TAD_Treinamento_a_distancia.Controllers
{
    public class ProvaController : Controller
    {
        //
        // GET: /Prova/

        public ActionResult Questoes()
        {
            var prova = new Prova();
            prova.BuscaProva("Instalando o Windowns");

            return View(prova);
        }

    }
}
