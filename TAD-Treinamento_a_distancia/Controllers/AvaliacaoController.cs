using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TAD_Treinamento_a_distancia.Models;

namespace TAD_Treinamento_a_distancia.Controllers
{
    public class AvaliacaoController : Controller
    {
        //
        // GET: /Avaliacao/

        CarregarAvaliacao c = new CarregarAvaliacao();
        
        
        


        [Authorize]
        [HttpGet]
        public ActionResult Catalogo()
        {
            CarregarTreinamentos carrega = new CarregarTreinamentos();
            carrega.Busca();
            return View(carrega);
        }
        
        [Authorize]
        [HttpPost]
        public ActionResult Catalogo(FormCollection form)
        {
            Session["Curso"] = form["curso"];
            FormsAuthentication.SetAuthCookie(Session.SessionID, false);
             
            return RedirectToAction("Teste", c);
        }


        [Authorize]
        [HttpGet]
        public ActionResult Teste(CarregarAvaliacao c)
        {
            var curso = (string)Session["Curso"];
            c.BuscaEnunciados(curso);
            return View(c);
        }
        [Authorize]
        [HttpPost]
        public ActionResult Teste(FormCollection form)
        {
            for (int i = 0; i < form.Count; i++)
			{
                c.Gabarito.Add(form[i]);
			}

            for (int i = 0; i < c.Gabarito.Count; i++)
            {
			
                var curso = (string)Session["Curso"];
                c.Corrigir(curso, i + 1); 
			}

            var pessoa = (Pessoa)Session["UsuarioAutenticado"];
            var cursos = (string)Session["Curso"];
            c.GravarNota(pessoa, cursos);

            return Redirect("Historico");
        }


        [Authorize]
        public ActionResult Historico()
        {

            var h = new Historico();
            var pessoa = (Pessoa)Session["UsuarioAutenticado"];
            h.LoginUsuario = pessoa.Usuario.Login;
            h.RealizaBusca();

            return View(h);
        }



    }
}
