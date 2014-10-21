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



<<<<<<< HEAD
        [Authorize(Roles = "Usuario, Administrador")]
=======
        [Authorize(Roles="Usuario, Administrador")]
>>>>>>> 5e4e4453edd660e948bbb5fdac76b6e0fb56c9f2
        public ActionResult CadastrarConta()
        {
            return View();
        }
<<<<<<< HEAD
        [Authorize(Roles = "Usuario, Administrador"), AcceptVerbs(HttpVerbs.Post)]
=======
        [Authorize(Roles="Usuario, Administrador"), AcceptVerbs(HttpVerbs.Post)]
>>>>>>> 5e4e4453edd660e948bbb5fdac76b6e0fb56c9f2
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

<<<<<<< HEAD
        [Authorize(Roles = "Usuario, Administrador")]
        public ActionResult EditarConta(int id = 0)
        {
            if (id == 0)
            {
                return RedirectToAction("Index", "Relatorios");
=======
        [Authorize(Roles="Usuario, Administrador")]
        public ActionResult EditarConta(int id=0)
        {
            if (id == 0)
            {
                return RedirectToAction("Index","Relatorios");
>>>>>>> 5e4e4453edd660e948bbb5fdac76b6e0fb56c9f2
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
<<<<<<< HEAD

=======
            
>>>>>>> 5e4e4453edd660e948bbb5fdac76b6e0fb56c9f2
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
<<<<<<< HEAD

=======
            
>>>>>>> 5e4e4453edd660e948bbb5fdac76b6e0fb56c9f2
            repositoryConta.DeletarConta(conta);
            return RedirectToAction("Index", "Relatorios");
        }

    }
}