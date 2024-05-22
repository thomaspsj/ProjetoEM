using EM.Domain;
using EM.Domain.ProjetoEM.EM.Domain;
using EM.Repository;
using EM.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Diagnostics;

namespace EM.Web.Controllers
{
    public class HomeController(ILogger<HomeController> logger, RepositorioAbstrato<Aluno> rep) : Controller
    {
        private readonly ILogger<HomeController> _logger = logger;
        public readonly RepositorioAbstrato<Aluno> _rep = rep;

        public IActionResult Index(string searchString, string pesquisePor)
        {

            if (pesquisePor == "matricula")
            {
                var alunosPorMatricula = from a in _rep.ObtenhaTodos()
                                         select a;

                if (!String.IsNullOrEmpty(searchString))
                {
                    alunosPorMatricula = alunosPorMatricula.Where(a => a.Matricula.ToString().Contains(searchString));
                }

                return View(alunosPorMatricula.ToList().OrderBy(a => a.Matricula));
            }
            else
            {

                if (_rep.ObtenhaTodos == null)
                {
                    return Problem();
                }

                var alunosPorNome = from a in _rep.ObtenhaTodos()
                                    select a;

                if (!String.IsNullOrEmpty(searchString))
                {
                    alunosPorNome = alunosPorNome.Where(a => a.Nome!.Contains(searchString.ToUpper()));
                }


                return View(alunosPorNome.ToList().OrderBy(a => a.Nome));
            }
        }

        [HttpPost]
        public string Index(string searchString)
        {
            return "From [HttpPost]Index: filter on " + searchString;
        }

       

        public IActionResult Cadastro(string? id)
        {
            Aluno aluno;
            if (id != null)
            {
                aluno = _rep.Obtenha(id);
                return View(aluno);

            }
            else
            {
                aluno = new();
                int getUltimaMatriculaMaisUm = 0;

                if (!string.IsNullOrEmpty(_rep.ObtenhaTodos().Max(a => a.Matricula.ToString())))
                {
                    getUltimaMatriculaMaisUm = _rep.ObtenhaTodos().Max(a => a.Matricula.ToString()) == "" ? 1 : _rep.ObtenhaTodos().Max(a => a.Matricula) + 1;
                }
                else
                {
                    getUltimaMatriculaMaisUm = 1;
                }

                aluno.Sexo = Sexo.Masculino;
                aluno.Matricula = getUltimaMatriculaMaisUm;
            }
            return View(aluno);
        }


        [HttpPost]
        public IActionResult Cadastro(Aluno aluno)
        {
            // Obtém um aluno existente pela matrícula
            Aluno alunoExistente = _rep.Obtenha(aluno.Matricula.ToString());

            // Cria ou atualiza os dados do aluno
            Aluno novoAluno = new Aluno
            {
                Matricula = aluno.Matricula,
                Nome = aluno.Nome?.ToUpper().Trim(),
                Sexo = aluno.Sexo,
                CPF = aluno.CPF,
                Nascimento = aluno.Nascimento,
                CEP = aluno.CEP,
                Logradouro = aluno.Logradouro,
                Bairro = aluno.Bairro,
                Cidade = aluno.Cidade,
                Estado = aluno.Estado,
            };

            if (string.IsNullOrWhiteSpace(novoAluno.Nome))
            {
                ViewBag.Mensagem = "Favor preencha o nome!";
                return View(aluno);
            }

            if (alunoExistente == null)
            {
                _rep.Adicione(novoAluno);
                ViewBag.Mensagem = "Cadastrado!";
                ViewBag.Sucesso = true;
            }
            else
            {
                _rep.Atualize(novoAluno);
                ViewBag.Mensagem = "Atualizado!";
                ViewBag.Sucesso = true;
            }
            return View();
            /*return RedirectToAction("Index", "Home");*/
        }



        public IActionResult Deletar(string id)
        {
            if (!int.TryParse(id, out int matricula))
            {
                return BadRequest("ID inválido.");
            }

            var aluno = _rep.ObtenhaTodos().FirstOrDefault(a => a.Matricula == matricula);

            if (aluno == null)
            {
                return NotFound();
            }


            if (aluno == null || aluno.Matricula < 1)
            {
                return NotFound();
            }

            try
            {
                _rep.Remova(aluno);
                ViewBag.Mensagem = "Deletado!";

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Cadastrar");
                ViewBag.Mensagem = "Falha";
            }
            return RedirectToAction("Index", "Home");

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}