using CadastroContas.Core.Dominio.Dados.Contrato;
using Ninject;
using SistemaContas.Core.Dominio.Dados.Contrato;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace SistemaContas.Web.Controllers
{
    [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
    public class PendenciasController : Controller
    {
        [Inject]
        public IMovimentacaoRepository repositoryMovimentacao { get; set; }
        [Inject]
        public IContaRepository repositoryConta { get; set; }
        [Inject]
        public IClienteRepository repositoryCliente { get; set; }


        [Authorize(Roles = "Usuario, Administrador")]
        public ActionResult SemTratativa()
        {
            ViewBag.Lista = repositoryConta.ContasEmAberto(Convert.ToInt32(User.Identity.Name)).OrderBy(x => x.DataVencimento).Where(x => x.StatusConta == "Em aberto").ToList();
            return View();
        }

        [Authorize(Roles = "Usuario, Administrador")]
        public ActionResult EmAndamento()
        {
            var lista = repositoryMovimentacao.PegarMovimentacoesNaoFinalizadas();
            foreach (var item in lista)
            {
                item.Conta = repositoryConta.PegarContaPorId(item.Conta.Id);
                item.Cliente = repositoryCliente.PegarClientePorId(Convert.ToInt32(User.Identity.Name));
                item.Conta.Cliente = item.Cliente;
            }
            JavaScriptSerializer js = new JavaScriptSerializer();
            string lista2 = js.Serialize(lista);
            ViewBag.Lista = lista;
            
            ViewBag.Lista2 = lista2;
            return View();
        }
    }
}