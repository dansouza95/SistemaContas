using CadastroContas.Core.Dominio.Dados.Contrato;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaContas.Web.Controllers
{
    [OutputCache(NoStore = true, Duration = 60, VaryByParam = "*")]
    public class PendenciasController : Controller
    {
        [Inject]
        public IContaRepository repositoryConta { get; set; }


        [Authorize(Roles = "Usuario, Administrador")]
        public ActionResult SemTratativa()
        {
            var lista = repositoryConta.ContasEmAberto(Convert.ToInt32(User.Identity.Name)).Where(x => x.StatusConta == "Em aberto");
            return View(lista);
        }

        [Authorize(Roles = "Usuario, Administrador")]
        public ActionResult EmAndamento()
        {
            ViewBag.Lista = repositoryConta.ContasEmAberto(Convert.ToInt32(User.Identity.Name)).Where(x => x.StatusConta == "Em andamento");
            return View();
        }
    }
}