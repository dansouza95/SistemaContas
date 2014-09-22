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

namespace SistemaContas.Web.Controllers
{
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
    public class PagamentosController : Controller
    {
        [Inject]
        public IMovimentacaoRepository repositoryPagamento { get; set; }

        [Inject]
        public IContaRepository repositoryConta { get; set; }

        [Inject]
        public IClienteRepository repositoryCliente { get; set; }

        [Inject]
        public ITransacaoRepository repositoryTransacao { get; set; }

        

        [Authorize(Roles = "Usuario, Administrador")]
        public ActionResult CadastrarPagamento(int id)
        {
            var conta = repositoryConta.PegarContaPorId(id);
            if (conta.StatusConta == "Em andamento" || conta.StatusConta == "Finalizada")
            {
                return RedirectToAction("Index", "Contas");
            }
            else
            {
                List<string> lista = new List<string>();
                for (int i = 1; i < 6; i++)
                {
                    lista.Add(i.ToString());
                }
                SelectList numeroParcelas = new SelectList(lista);
                ViewBag.ListaParcelas = numeroParcelas;
                Movimentacao pagamento = new Movimentacao();
                pagamento.Conta = repositoryConta.PegarContaPorId(id);
                return View();
            }
        }

        [Authorize(Roles = "Usuario, Administrador"), AcceptVerbs(HttpVerbs.Post)]

        public ActionResult CadastrarPagamento(Movimentacao pagamento, int id = 0)
        {
            var conta = repositoryConta.PegarContaPorId(id);
            pagamento.Conta = conta;
            pagamento.Cliente = (Cliente)Session["UsuarioLogado"];
            pagamento.Valor = pagamento.Conta.ValorConta;
            pagamento.ValorParcela = pagamento.Valor / pagamento.NumeroParcelas;
            pagamento.ParcelasRestantes = pagamento.NumeroParcelas;
            pagamento.Tipo = pagamento.Conta.TipoOperacao;
            pagamento.Status = "Em aberto";
            pagamento.ValorRestante = pagamento.Valor;
            pagamento.UltimaAtualizacao = DateTime.Now;
            conta.StatusConta = "Em andamento";
            repositoryConta.AtualizarConta(conta);
            repositoryPagamento.SalvarMovimentacao(pagamento);
            return RedirectToAction("EmAndamento", "Pendencias");
        }

        [Authorize(Roles = "Usuario, Administrador")]
        public ActionResult Parcela(int id = 0)
        {
            
                var conta = repositoryConta.PegarContaPorId(id);
                if (id != 0 && conta != null)
                {
                    var pagamento = repositoryPagamento.PegarMovimentacao(id);
                    pagamento.Conta = repositoryConta.PegarContaPorId(id);
                    return View(pagamento);
                }
                else
                {
                    return RedirectToAction("Index", "Contas");
                }
        }

        [Authorize(Roles="Usuario, Administrador"), AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Parcela(Movimentacao pagamento, int id=0)
        {
            var conta = repositoryConta.PegarContaPorId(id);
            if (id != 0 && conta != null)
            {
                pagamento = repositoryPagamento.PegarMovimentacao(id);

                pagamento.Conta = conta;
                pagamento.Cliente = (Cliente)Session["UsuarioLogado"];
                pagamento.ValorRestante = pagamento.ValorRestante - pagamento.ValorParcela;
                pagamento.ParcelasRestantes -= 1;
                pagamento.Status = "Em andamento";
                if (pagamento.ParcelasRestantes == 0)
                {
                    pagamento.Status = "Finalizada";
                    conta.StatusConta = "Finalizada";
                    conta.DataFinalizacao = DateTime.Now;
                    repositoryConta.AtualizarConta(conta);
                }
                pagamento.UltimaAtualizacao = DateTime.Now;

                Transacao t = new Transacao();
                t.Cliente = pagamento.Cliente;
                t.Conta = pagamento.Conta;
                t.DataTransacao = pagamento.UltimaAtualizacao;
                t.ValorTransacao = pagamento.ValorParcela;
                t.Movimentacao = pagamento;
                t.TipoTransacao = pagamento.Tipo;

                repositoryTransacao.SalvarTransacao(t);
                repositoryPagamento.SalvarOuAtualizarMovimentacao(pagamento);

                return RedirectToAction("EmAndamento", "Pendencias");

            }
            else
            {
                return RedirectToAction("Index", "Contas");
            }
        }

        [Authorize(Roles = "Usuario, Administrador")]
        public ActionResult QuitarPagamento(int id = 0)
        {

            var conta = repositoryConta.PegarContaPorId(id);
            if (id != 0 && conta != null)
            {
                var pagamento = repositoryPagamento.PegarMovimentacao(id);
                pagamento.Conta = repositoryConta.PegarContaPorId(id);
                return View(pagamento);
            }
            else
            {
                return RedirectToAction("Index", "Contas");
            }
        }

        [Authorize(Roles = "Usuario, Administrador"), AcceptVerbs(HttpVerbs.Post)]
        public ActionResult QuitarPagamento(Movimentacao pagamento, int id = 0)
        {
            var conta = repositoryConta.PegarContaPorId(id);
            if (id != 0 && conta != null)
            {
                pagamento = repositoryPagamento.PegarMovimentacao(id);

                pagamento.Conta = conta;
                pagamento.Cliente = (Cliente)Session["UsuarioLogado"];
                var valor = pagamento.ValorRestante;
                pagamento.ValorRestante = 0;
                pagamento.ParcelasRestantes = 0;
                pagamento.Status = "Em andamento";
                if (pagamento.ParcelasRestantes == 0)
                {
                    pagamento.Status = "Finalizada";
                    conta.StatusConta = "Finalizada";
                    conta.DataFinalizacao = DateTime.Now;
                    repositoryConta.AtualizarConta(conta);
                }
                pagamento.UltimaAtualizacao = DateTime.Now;

                Transacao t = new Transacao();
                t.Cliente = pagamento.Cliente;
                t.Conta = pagamento.Conta;
                t.DataTransacao = pagamento.UltimaAtualizacao;
                t.ValorTransacao = valor;
                t.Movimentacao = pagamento;
                t.TipoTransacao = pagamento.Tipo;

                repositoryTransacao.SalvarTransacao(t);
                repositoryPagamento.SalvarOuAtualizarMovimentacao(pagamento);

                return RedirectToAction("EmAndamento", "Pendencias");

            }
            else
            {
                return RedirectToAction("Index", "Contas");
            }
        }
        
    }
}