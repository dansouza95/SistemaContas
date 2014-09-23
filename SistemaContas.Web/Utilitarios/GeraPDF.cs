using Rotativa;
using Rotativa.Options;
using SistemaContas.Core.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaContas.Web
{
    public class GeraPDF
    {
        public static ViewAsPdf ConvertePdf(string nomeView, Cliente cliente)
        {
            var pdf = new ViewAsPdf
            {
                ViewName = nomeView,
                FileName = nomeView + "_" + cliente.Nome.Replace(" ", "_") + ".pdf",
                PageSize = Size.A4,
                IsGrayScale = true,
                PageMargins = new Margins { Bottom = 5, Left = 5, Right = 5, Top = 0 }
            };
            return pdf;
        }
    }
}