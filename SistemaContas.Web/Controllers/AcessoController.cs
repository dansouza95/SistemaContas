using CadastroContas.Core.Dominio.Dados.Contrato;
using Ninject;
using SistemaContas.Core.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Security;

namespace SistemaContas.Web.Controllers
{
    [Authorize]
    [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
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
                return RedirectToAction("Index", "Relatorios");
            }
            else
            {
                return View();
            }
        }
        [AllowAnonymous, AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Logon(string dados)
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            Cliente cliente = js.Deserialize<Cliente>(dados);

            MD5 md5 = new MD5CryptoServiceProvider();
            Byte[] senhaOriginal = ASCIIEncoding.Default.GetBytes(cliente.Senha);
            Byte[] senhaCodificada = md5.ComputeHash(senhaOriginal);

            string hash = BitConverter.ToString(senhaCodificada).Replace("-", "");
            cliente.Senha = hash;
            int id = repository.LoginCliente(cliente);
            if (id > 0)
            {
                cliente = repository.PegarClientePorId(id);
                AutorizarLogin(cliente.Id, cliente.Permissao);
                Session.Add("UsuarioLogado", cliente);
<<<<<<< HEAD
                return Json(true, JsonRequestBehavior.AllowGet);
=======
                return Json(true,JsonRequestBehavior.AllowGet);
>>>>>>> 5e4e4453edd660e948bbb5fdac76b6e0fb56c9f2
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }

        }


        public void AutorizarLogin(int userId, string roles)
        {
            FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(
            1,
            userId.ToString(),
            DateTime.Now,
            DateTime.Now.AddMinutes(10),
            false,
            roles);
            HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(authTicket));
            cookie.Expires = DateTime.Now.AddMinutes(10);
            Response.Cookies.Add(cookie);
        }


        [AllowAnonymous]
        public ActionResult RegistroUsuario()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Relatorios");
            }
            else
            {
                return View();
            }
        }

        [AllowAnonymous, AcceptVerbs(HttpVerbs.Post)]
        public ActionResult RegistroUsuario(Cliente cliente)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            Byte[] senhaOriginal = ASCIIEncoding.Default.GetBytes(cliente.Senha);
            Byte[] senhaCodificada = md5.ComputeHash(senhaOriginal);

            string hash = BitConverter.ToString(senhaCodificada).Replace("-", "");
            cliente.Senha = hash;

            cliente.Permissao = "Usuario";
            cliente.DataCadastro = DateTime.Now;
            repository.CadastraCliente(cliente);
            return RedirectToAction("Login");
        }

        [AllowAnonymous]
        public ActionResult Validar(string usuario)
        {
<<<<<<< HEAD
            Cliente cliente = new Cliente();
            var js = new JavaScriptSerializer();
            cliente = js.Deserialize<Cliente>(usuario);
            return Json(repository.VerificarCadastro(cliente), JsonRequestBehavior.AllowGet);
=======
            return Json(repository.VerificarCadastro(usuario), JsonRequestBehavior.AllowGet);
>>>>>>> 5e4e4453edd660e948bbb5fdac76b6e0fb56c9f2
        }


        [Authorize(Roles = "Usuario, Administrador")]
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