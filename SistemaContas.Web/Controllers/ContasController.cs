using CadastroContas.Core.Dominio.Dados.Contrato;
using Ninject;
using Rotativa;
using Rotativa.Options;
using SistemaContas.Core.Dominio.Dados.Contrato;
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
    [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
    public class ContasController : Controller
    {
        [Inject]
        public IContaRepository repositoryConta { get; set; }
        [Inject]
        public IClienteRepository repositoryCliente { get; set; }
        [Inject]
        public ITransacaoRepository repositoryTransacao { get; set; }
        [Inject]
        public IMovimentacaoRepository repositoryMovimentacao { get; set; }




        [Authorize(Roles="Usuario, Administrador")]
        public ActionResult CadastrarConta()
        {
            return View();
        }
        [Authorize(Roles="Usuario, Administrador"), AcceptVerbs(HttpVerbs.Post)]
        public ActionResult CadastrarConta(Conta conta)
        {
            if (Session["UsuarioLogado"] != null)
            {
                conta.Cliente = (Cliente)Session["UsuarioLogado"];
            }
            else
            {
                Session.Add("UsuarioLogado", repositoryCliente.PegarClientePorId(Convert.ToInt32(User.Identity.Name)));
                conta.Cliente = (Cliente)Session["UsuarioLogado"];
            }
            conta.UltimaAtualizacao = DateTime.Now;
            conta.StatusConta = "Em aberto";
            repositoryConta.CadastraConta(conta);
            return RedirectToAction("SemTratativa", "Pendencias");
        }
        
        [Authorize(Roles="Usuario, Administrador")]
        public ActionResult EditarConta(int id=0)
        {
            if (id == 0)
            {
                return RedirectToAction("Index","Relatorios");
            }
            else
            {
                var conta = repositoryConta.PegarContaPorId(id);
                conta.Valor = conta.ValorConta.ToString("0.00");
                return View(conta);
            }
        }

        [Authorize(Roles = "Usuario, Administrador"), AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditarConta(Conta conta)
        {
            conta.ValorConta = Convert.ToDouble(conta.Valor);
            if (Session["UsuarioLogado"] != null)
            {
                conta.Cliente = (Cliente)Session["UsuarioLogado"];
            }
            else
            {
                Session.Add("UsuarioLogado", repositoryCliente.PegarClientePorId(Convert.ToInt32(User.Identity.Name)));
                conta.Cliente = (Cliente)Session["UsuarioLogado"];
            }

            conta.StatusConta = "Em aberto";
            conta.UltimaAtualizacao = DateTime.Now;
            repositoryConta.EditarConta(conta);
            return RedirectToAction("SemTratativa", "Pendencias");
        }

        [Authorize(Roles = "Usuario, Administrador")]
        public ActionResult ExcluirConta(int id = 0)
        {
            if (id == 0)
            {
                return RedirectToAction("Index", "Relatorios");
            }
            else
            {
                var conta = repositoryConta.PegarContaPorId(id);
                if (conta != null)
                {
                    return View(conta);
                }
                else
                {
                    return RedirectToAction("Index", "Relatorios");
                }
            }
        }

        [Authorize(Roles = "Usuario, Administrador"), AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ExcluirConta(Conta conta, int id)
        {
            conta = repositoryConta.PegarContaPorId(id);
            var listaTransacoes = repositoryTransacao.PegarTransacoesPorConta(conta.Id);
            var movimentacao = repositoryMovimentacao.PegarMovimentacao(conta.Id);
            if (listaTransacoes.Count > 0) { repositoryTransacao.ExcluirTransacoes(listaTransacoes); }
            if (movimentacao != null) { repositoryMovimentacao.ExcluirMovimentacao(movimentacao); }
            repositoryConta.DeletarConta(conta);
            return RedirectToAction("Index", "Relatorios");
        }

    }
}