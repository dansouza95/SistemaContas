using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SistemaContas.Teste;
using SistemaContas.Tests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SistemaContas.Initializer
{
    public static class Browser
    {
        public static IWebDriver driver;

        private static IISConfig iis;

        private static void iniciarIIS()
        {
            if (iis == null)
            {
                iis = new IISConfig("SistemaContas.Web");
                iis.StartIIS();
            }

        }

        public static void MudarPagina(string pagina)
        {
            iniciarIIS();
            if (driver == null)
            {
                //driver = Configuration.Driver;
                driver = new ChromeDriver();
                driver.Navigate().GoToUrl(pagina);
                driver.Manage().Window.Maximize();
                driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(10));
            }
            else
                driver.Navigate().GoToUrl(pagina);
        }

        public static void ClicoNoLink(string link)
        {

            driver.FindElement(By.Id(link)).Click();
        }

        internal static void ClicoNoBotao(string botao)
        {

            driver.FindElement(By.Id(botao)).Click();
        }

        internal static void PreencherCampo(string campo, string valor)
        {
            driver.FindElement(By.Id(campo)).Clear();
            driver.FindElement(By.Id(campo)).SendKeys(valor);
        }

        internal static void PaginaInicial(string elemento)
        {
            Assert.IsTrue(driver.FindElement(By.TagName(elemento)).TagName.ToLower().Contains(elemento));
        }

        internal static void PreencherOpcao(string campo, string valor)
        {
            driver.FindElement(By.Id(campo)).SendKeys(valor);
        }

        internal static void DevoVerAMensagem(string mensagem, string elemento)
        {
            Assert.IsTrue(driver.FindElement(By.Id(elemento)).Text.Equals(mensagem));
        }
    }
}
