using CadastroContas.Core.Dominio.Dados.Contrato;
using Ninject;
using SistemaContas.Core.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace SistemaContas.Web.Controllers
{
    public class AcessoController : Controller
    {
        // GET: Acesso
        [Inject]
        public IClienteRepository repository { get; set; }

        // GET: Acesso
        [AllowAnonymous]
        public ActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Contas");
            }
            else
            {
                return View();
            }
        }
        [AllowAnonymous, AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Login(Cliente cliente)
        {
            int id = repository.LoginCliente(cliente);
            if (id > 0)
            {
                cliente = repository.PegarClientePorId(id);
                AutorizarLogin(cliente.Id, cliente.Permissao);
                Session.Add("UsuarioLogado", cliente);
                return RedirectToAction("Index", "Contas");
            }
            else
            {
                return RedirectToAction("Login");
            }

        }
        public void AutorizarLogin(int userId, string roles)
        {
            FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(
            1,
            userId.ToString(),
            DateTime.Now,
            DateTime.Now.AddMinutes(5),
            false,
            roles);
            HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(authTicket));
            cookie.Expires.AddMinutes(5);
            Response.Cookies.Add(cookie);
        }


        [AllowAnonymous]
        public ActionResult RegistroUsuario()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Contas");
            }
            else
            {
                return View();
            }
        }

        [AllowAnonymous, AcceptVerbs(HttpVerbs.Post)]
        public ActionResult RegistroUsuario(Cliente cliente)
        {
            cliente.Permissao = "Usuario";
            cliente.DataCadastro = DateTime.Now;
            repository.CadastraCliente(cliente);
            return RedirectToAction("Login");
        }


        [Authorize(Roles="Usuario, Administrador")]
        public ActionResult Logout()
        {
            foreach (var cookie in Response.Cookies.AllKeys)
            {
                Response.Cookies.Remove(cookie);
            }
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.Now.AddSeconds(-1));
            Response.Cache.SetNoStore();

            Session.Remove("UsuarioLogado");
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }

        
    }
}