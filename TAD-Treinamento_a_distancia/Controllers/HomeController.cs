using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TAD_Treinamento_a_distancia.Models;

namespace TAD_Treinamento_a_distancia.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Login/
       
        


        [HttpGet]
        public ActionResult Login()
        {
            var p = new Pessoa();
            var u = new Usuario();

            p.Usuario = u;
            return View(p);
        }


        [HttpPost]
        public ActionResult Login(Pessoa p)
        {
                    
            
            if (p.Usuario.Autentica(p.Usuario.Login, p.Usuario.Senha))
            {
                p.Usuario.Login = UsuarioLogado.Login;
                p.Nome = UsuarioLogado.Nome;
                p.Usuario.Email = UsuarioLogado.Email;
                p.Usuario.Senha = "";
                

                Session["UsuarioAutenticado"] = p;
                FormsAuthentication.SetAuthCookie(Session.SessionID, false);
                return View("Index");
            }
            else
            {
                return View();
            }
        }

        [Authorize]
        public ActionResult Index()
        {
                return View();
            
        }

        public ActionResult Sair()
        {
            Session.Abandon();
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");

        }

        public ActionResult RecuperarSenha()
        {

            var p = new Pessoa();
            var u = new Usuario();
            p.Usuario = u;
            
            return View(p);

        }
        [HttpPost]
        public ActionResult RecuperarSenha(Pessoa p)
        {
            

            ViewData["status"] = p.Usuario.RecuperarSenha(p.Usuario.Email);

            return View();
        }

        public ActionResult Cadastro()
        {
            var p = new Pessoa();
            var u = new Usuario();
            p.Usuario = u;

            return View(p);

           ;
        }

        [HttpPost]
        public ActionResult Cadastro(Pessoa p)
        {
            if (ModelState.IsValid)
            {

                ViewBag.Mensagem = p.Usuario.CadastraUsuario(p.Nome, p.Usuario.Email, p.Usuario.Login, p.Usuario.Senha);
                
                return View(p);

            }
            return View(p);
        }

    }
}
