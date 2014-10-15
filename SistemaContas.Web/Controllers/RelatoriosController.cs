using CadastroContas.Core.Dominio.Dados.Contrato;
using Ninject;
using SistemaContas.Core.Dominio.Dados.Contrato;
using SistemaContas.Core.Dominio.Entidades;
using SistemaContas.Web.Utilitarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaContas.Web.Controllers
{
    [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
    public class RelatoriosController : Controller
    {
        [Inject]
        public IClienteRepository repositoryCliente { get; set; }

        [Inject]
        public IContaRepository repositoryConta { get; set; }
        [Inject]
        public ITransacaoRepository repositoryTransacao { get; set; }
        [Inject]
        public IMovimentacaoRepository repositoryMovimentacao { get; set; }


        [Authorize(Roles = "Usuario, Administrador")]
        public ActionResult Index()
        {
            int id = Convert.ToInt32(User.Identity.Name);
            ViewBag.Creditos = repositoryTransacao.PegarTransacoes(id).Where(x => x.TipoTransacao == "Crédito").Sum(x => x.ValorTransacao).ToString("0.00").Replace(",", ".");
            ViewBag.Debitos = repositoryTransacao.PegarTransacoes(id).Where(x => x.TipoTransacao == "Débito").Sum(x => x.ValorTransacao).ToString("0.00").Replace(",", ".");

            return View();
        }

        [Authorize(Roles = "Usuario, Administrador")]
        public ActionResult ContasFinalizadas()
        {
            int id = Convert.ToInt32(User.Identity.Name);
            var lista = repositoryConta.ContasFinalizadas(id);
            ViewBag.Contas = lista;
            return View();
        }

        [Authorize(Roles = "Usuario, Administrador")]
        public ActionResult Extrato(int id = 0)
        {
            if (id > 0 && id <= 4)
            {
                switch (id)
                {
                    case 1: getExtrato(7);
                        break;
                    case 2: getExtrato(15);
                        break;
                    case 3: getExtrato(30);
                        break;

                    default: int idCliente = Convert.ToInt32(User.Identity.Name);
                             var lista = repositoryTransacao.PegarTransacoes(idCliente);
                             foreach (var item in lista)
                             {
                                 item.Conta = repositoryConta.PegarContaPorId(item.Conta.Id);
                                 item.Movimentacao = repositoryMovimentacao.PegarMovimentacao(item.Conta.Id);
                             }
                             
                             ViewBag.OperacoesCredito = lista.Where(x => x.TipoTransacao == "Crédito").Sum(x => x.ValorTransacao);
                             ViewBag.OperacoesDebito = lista.Where(x => x.TipoTransacao == "Débito").Sum(x => x.ValorTransacao);

                             ViewBag.Historico = lista;
                             ViewBag.Total = ViewBag.OperacoesCredito - ViewBag.OperacoesDebito;
                        break;
                }
                return View();
            }
            else
            {
                return RedirectToAction("Index");
            }

        }

        public void getExtrato(int numDias)
        {
            var data = DateTime.Now.AddDays(numDias *-1);
            var lista = repositoryTransacao.PegarTransacoes(Convert.ToInt32(User.Identity.Name)).Where(x => x.DataTransacao >= data);

            foreach (var item in lista)
            {
                item.Conta = repositoryConta.PegarContaPorId(item.Conta.Id);
                item.Movimentacao = repositoryMovimentacao.PegarMovimentacao(item.Conta.Id);
            }
            
            ViewBag.OperacoesCredito = lista.Where(x => x.TipoTransacao == "Crédito").Sum(x => x.ValorTransacao);
            ViewBag.OperacoesDebito = lista.Where(x => x.TipoTransacao == "Débito").Sum(x => x.ValorTransacao);
            
            ViewBag.Historico = lista;
            ViewBag.Total = ViewBag.OperacoesCredito - ViewBag.OperacoesDebito;
            ViewBag.Informacao = "(Últimos "+ numDias.ToString() +" dias)";
        }


        [Authorize(Roles = "Usuario, Administrador"), AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Extrato(Transacao tran, int id)
        {
            if (id > 0 && id <= 4)
            {
                Cliente cliente = new Cliente();
                if (Session["UsuarioLogado"] != null)
                {
                    cliente = (Cliente)Session["UsuarioLogado"];
                }
                else
                {
                    Session.Add("UsuarioLogado", repositoryCliente.PegarClientePorId(Convert.ToInt32(User.Identity.Name)));
                    cliente = (Cliente)Session["UsuarioLogado"];
                }
                switch (id)
                {
                    case 1: getExtrato(7);
                        break;
                    case 2: getExtrato(15);
                        break;
                    case 3: getExtrato(30);
                        break;

                    default: int idCliente = Convert.ToInt32(User.Identity.Name);
                        var lista = repositoryTransacao.PegarTransacoes(idCliente);
                        foreach (var item in lista)
                        {
                            item.Conta = repositoryConta.PegarContaPorId(item.Conta.Id);
                            item.Movimentacao = repositoryMovimentacao.PegarMovimentacao(item.Conta.Id);
                        }
                        ViewBag.Historico = lista;
                        ViewBag.OperacoesCredito = lista.Where(x => x.TipoTransacao == "Crédito").Sum(x => x.ValorTransacao);
                        ViewBag.OperacoesDebito = lista.Where(x => x.TipoTransacao == "Débito").Sum(x => x.ValorTransacao);
                        ViewBag.Total = lista.Sum(x => x.ValorTransacao);
                        ViewBag.Informacao = "(Completo)";
                        break;
                }
                ViewBag.Pdf = true;
                return GeraPDF.ConvertePdf("Extrato", cliente, ViewBag.Informacao);
            }
            else
            {
                return RedirectToAction("Index");
            }

        }
    }
}