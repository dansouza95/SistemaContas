using CadastroContas.Core.Dominio.Dados.Contrato;
using Ninject;
using Rotativa;
using Rotativa.Options;
using SistemaContas.Core.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;


namespace SistemaContas.Web.Controllers
{
    [Authorize]
    [OutputCache(NoStore = true, Duration = 60, VaryByParam = "*")]
    public class ContasController : Controller
    {
        [Inject]
        public IContaRepository repositoryConta { get; set; }
        [Inject]
        public IClienteRepository repositoryCliente { get; set; }


        [Authorize(Roles="Usuario, Administrador"), ]
        public ActionResult Index()
        {

            var Creditos = repositoryConta.Creditos(Convert.ToInt32(User.Identity.Name));
            var Debitos = repositoryConta.Debitos(Convert.ToInt32(User.Identity.Name));
            double[] dados = new double[2] {Creditos, Debitos };
            ViewBag.Dados = new JavaScriptSerializer().Serialize(dados);
            return View();
        }

        [Authorize(Roles="Usuario, Administrador")]
        public ActionResult CadastrarConta()
        {
            return View();
        }
        [Authorize(Roles="Usuario, Administrador"), AcceptVerbs(HttpVerbs.Post)]
        public ActionResult CadastrarConta(Conta conta)
        {
            conta.Cliente = repositoryCliente.PegarClientePorId(Convert.ToInt32(User.Identity.Name));
            conta.StatusConta = "Em aberto";
            repositoryConta.CadastraConta(conta);
            return RedirectToAction("SemTratativa", "Pendencias");
        }

        [Authorize(Roles="Usuario, Administrador")]
        public ActionResult EditarConta(int id=0)
        {
            if (id == 0)
            {
                return RedirectToAction("Index");
            }
            else
            {
                var conta = repositoryConta.PegarContaPorId(id);
                return View(conta);
            }
        }

        [Authorize(Roles = "Usuario, Administrador"), AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditarConta(Conta conta)
        {
            conta.Cliente = repositoryCliente.PegarClientePorId(Convert.ToInt32(User.Identity.Name));
            conta.StatusConta = "Em aberto";
            repositoryConta.EditarConta(conta);
            return RedirectToAction("SemTratativa", "Pendencias");
        }

        [Authorize(Roles = "Usuario, Administrador")]
        public ActionResult ExcluirConta(int id = 0)
        {
            if (id == 0)
            {
                return RedirectToAction("Index");
            }
            else
            {
                var conta = repositoryConta.PegarContaPorId(id);
                return View(conta);
            }
        }

        [Authorize(Roles = "Usuario, Administrador"), AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ExcluirConta(Conta conta, int id)
        {
            conta = repositoryConta.PegarContaPorId(id);
            repositoryConta.DeletarConta(conta);
            return RedirectToAction("Index");
        }

    }
}