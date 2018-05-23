using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using InspetorJacquesClouseau1.Models;

namespace TesteMVC2.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Suspeitoslist = GetSuspeitos(null);
            ViewBag.Locaislist = GetLocais(null);
            ViewBag.Armaslist = GetArmas(null);

            SortearCrime();

            return View();
        }

        [HttpPost]
        public JsonResult EnviarTeoria(Teoria TeoriaData)
        {
            bool SuspeitoCorreto = false;
            bool LocalCorreto = false;
            bool ArmaCorreta = false;

            if (int.Parse(TeoriaData.Suspeito) == (int)Session["Suspeito"])
            {
                SuspeitoCorreto = true;
            }

            if (int.Parse(TeoriaData.Local) == (int)Session["Local"])
            {
                LocalCorreto = true;
            }

            if (int.Parse(TeoriaData.Arma) == (int)Session["Arma"])
            {
                ArmaCorreta = true;
            }

            if (SuspeitoCorreto && LocalCorreto && ArmaCorreta)
            {
                TeoriaData.Retorno = 0;
            }
            else
            {
                if (!SuspeitoCorreto && LocalCorreto && ArmaCorreta)
                {
                    TeoriaData.Retorno = 1;
                }
                else if (SuspeitoCorreto && !LocalCorreto && ArmaCorreta)
                {
                    TeoriaData.Retorno = 2;
                }
                else if (SuspeitoCorreto && LocalCorreto && !ArmaCorreta)
                {
                    TeoriaData.Retorno = 3;
                }
                else if (!SuspeitoCorreto && !LocalCorreto && !ArmaCorreta)
                {
                    Random rnd = new Random();
                    TeoriaData.Retorno = rnd.Next(1, 4);
                }
                else if (!SuspeitoCorreto && !LocalCorreto && ArmaCorreta)
                {
                    Random rnd = new Random();
                    TeoriaData.Retorno = rnd.Next(1, 3);
                }
                else if (SuspeitoCorreto && !LocalCorreto && !ArmaCorreta)
                {
                    Random rnd = new Random();
                    TeoriaData.Retorno = rnd.Next(2, 4);
                }
                else if (!SuspeitoCorreto && LocalCorreto && !ArmaCorreta)
                {
                    Random rnd = new Random();
                    for (int i = 1; i <= 200; i++)
                    {
                        TeoriaData.Retorno = rnd.Next(1, 4);
                        if (TeoriaData.Retorno != 2) { break; }
                    }
                }
            }

            return Json(TeoriaData, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public void NovoCrime()
        {
            SortearCrime();
        }

        public void SortearCrime()
        {
            Random rnd = new Random();
            Session["Suspeito"] = rnd.Next(1, 7);
            Session["Local"] = rnd.Next(1, 11);
            Session["Arma"] = rnd.Next(1, 7);
        }

        private MultiSelectList GetSuspeitos(string[] selectedValues)
        {
            List<Suspeito> Suspeitos = new List<Suspeito>()
            {
                new Suspeito() { ID = 1, Descricao= "Esqueleto" },
                new Suspeito() { ID = 2, Descricao= "Khan" },
                new Suspeito() { ID = 3, Descricao= "Darth vader" },
                new Suspeito() { ID = 4, Descricao= "Sideshow Bob" },
                new Suspeito() { ID = 5, Descricao= "Coringa" },
                new Suspeito() { ID = 6, Descricao= "Duende Verde" }
            };

            return new MultiSelectList(Suspeitos, "ID", "Descricao", selectedValues);
        }
        private MultiSelectList GetLocais(string[] selectedValues)
        {
            List<Local> Locais = new List<Local>()
            {
                new Local() { ID = 1, Descricao= "Etérnia" },
                new Local() { ID = 2, Descricao= "Vulcano" },
                new Local() { ID = 3, Descricao= "Tatooine" },
                new Local() { ID = 4, Descricao= "Springfield" },
                new Local() { ID = 5, Descricao= "Gotham" },
                new Local() { ID = 6, Descricao= "Nova York" },
                new Local() { ID = 7, Descricao= "Sibéria" },
                new Local() { ID = 8, Descricao= "Machu Picchu" },
                new Local() { ID = 9, Descricao= "Show do Katinguele" },
                new Local() { ID = 10, Descricao= "São Paulo" }
            };

            return new MultiSelectList(Locais, "ID", "Descricao", selectedValues);
        }
        private MultiSelectList GetArmas(string[] selectedValues)
        {
            List<Arma> Armas = new List<Arma>()
            {
                new Arma() { ID = 1, Descricao= "Cajado Devastador" },
                new Arma() { ID = 2, Descricao= "Phaser" },
                new Arma() { ID = 3, Descricao= "Peixeira" },
                new Arma() { ID = 4, Descricao= "Trezoitão" },
                new Arma() { ID = 5, Descricao= "Sabre de Luz" },
                new Arma() { ID = 6, Descricao= "Bomba" }
            };

            return new MultiSelectList(Armas, "ID", "Descricao", selectedValues);
        }

    }
}