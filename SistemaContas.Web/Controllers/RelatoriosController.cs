using CadastroContas.Core.Dominio.Dados.Contrato;
using Ninject;
using SistemaContas.Core.Dominio.Dados.Contrato;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaContas.Web.Controllers
{
    [OutputCache(NoStore = true, Duration = 60, VaryByParam = "*")]
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
        public ActionResult ContasFinalizadas()
        {
            int id = Convert.ToInt32(User.Identity.Name);
            var lista = repositoryConta.ContasFinalizadas(id);
            ViewBag.Contas = lista;
            ViewBag.Debitos = lista.Where(x => x.TipoOperacao == "Débito").Sum(x => x.ValorConta);
            ViewBag.Creditos = lista.Where(x => x.TipoOperacao == "Crédito").Sum(x => x.ValorConta);
            ViewBag.Total = repositoryConta.PegarExtrato(id);
            return View();
        }
        [Authorize(Roles = "Usuario, Administrador")]
        public ActionResult ContasFinalizadasPDF()
        {
            var cliente = repositoryCliente.PegarClientePorId(Convert.ToInt32(User.Identity.Name));
            int id = Convert.ToInt32(User.Identity.Name);
            var lista = repositoryConta.ContasFinalizadas(id);
            ViewBag.Contas = lista;
            ViewBag.Debitos = lista.Where(x => x.TipoOperacao == "Débito").Sum(x => x.ValorConta);
            ViewBag.Creditos = lista.Where(x => x.TipoOperacao == "Crédito").Sum(x => x.ValorConta);
            ViewBag.Total = repositoryConta.PegarExtrato(id);
            ViewBag.Pdf = true;

            return GeraPDF.ConvertePdf("ContasFinalizadas", cliente);
        }

        [Authorize(Roles = "Usuario, Administrador")]
        public ActionResult Extrato()
        {
            int id = Convert.ToInt32(User.Identity.Name);
            var lista = repositoryTransacao.PegarTransacoes(id);
            foreach (var item in lista)
            {
                item.Conta = repositoryConta.PegarContaPorId(item.Conta.Id);
                item.Movimentacao = repositoryMovimentacao.PegarMovimentacao(item.Conta.Id);
            }
            ViewBag.Historico = lista;
            ViewBag.OperacoesCredito = lista.Where(x => x.TipoTransacao=="Crédito").Sum(x => x.ValorTransacao);
            ViewBag.OperacoesDebito = lista.Where(x => x.TipoTransacao == "Débito").Sum(x => x.ValorTransacao);
            ViewBag.Total = repositoryTransacao.PegarTotalTransacoes(id);
            return View();
                
        }

        [Authorize(Roles = "Usuario, Administrador")]
        public ActionResult ExtratoPDF()
        {
            int id = Convert.ToInt32(User.Identity.Name);
            var cliente = repositoryCliente.PegarClientePorId(id);
            var lista = repositoryTransacao.PegarTransacoes(id);
            foreach (var item in lista)
            {
                item.Conta = repositoryConta.PegarContaPorId(item.Conta.Id);
                item.Movimentacao = repositoryMovimentacao.PegarMovimentacao(item.Conta.Id);
            }


            ViewBag.Historico = lista;
            ViewBag.OperacoesCredito = lista.Where(x => x.TipoTransacao == "Crédito").Sum(x => x.ValorTransacao);
            ViewBag.OperacoesDebito = lista.Where(x => x.TipoTransacao == "Débito").Sum(x => x.ValorTransacao);
            ViewBag.Total = repositoryConta.PegarExtrato(id);
            ViewBag.Pdf = true;
            return GeraPDF.ConvertePdf("Extrato", cliente);
        }
    }
}