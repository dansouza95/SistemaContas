using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SistemaContas.Initializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;


namespace SistemaContas.Teste.Steps
{
    [Binding]
    public class Acoes
    {
        public IWebDriver driver;

        [Given(@"que estou na página ""(.*)""")]
        public void DadoQueEstouNaPagina(string pagina)
        {
            Browser.MudarPagina(pagina);
        }

        [Given(@"clico no link ""(.*)""")]
        public void DadoClicoNoLink(string link)
        {
            Browser.ClicoNoLink(link);

        }

        [Given(@"clico na opcao ""(.*)""")]
        [Then(@"clico na opcao ""(.*)""")]
        [When(@"clico na opcao ""(.*)""")]
        public void QuandoClicoNoBotao(string botao)
        {
            Browser.ClicoNoBotao(botao);
        }


        [Then(@"preencho o campo ""(.*)"" com o valor ""(.*)""")]
        [Given(@"preencho o campo ""(.*)"" com o valor ""(.*)""")]
        public void EntaoPreenchoOCampoComOValor(string campo, string valor)
        {
            Browser.PreencherCampo(campo, valor);
        }

        [Then(@"preencho o a opcao ""(.*)"" com o valor ""(.*)""")]
        public void EntaoPreenchoOAOpcaoComOValor(string campo, string valor)
        {
            Browser.PreencherOpcao(campo, valor);
            
        }

        [Then(@"devo ver o elemento ""(.*)""")]
        public void EntaoDevoVerAPaginaInicial(string elemento)
        {
            Browser.PaginaInicial(elemento);
        }
    }
}
