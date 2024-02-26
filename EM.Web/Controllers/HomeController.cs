using EM.Domain;
using EM.Domain.ProjetoEM.EM.Domain;
using EM.Repository;
using EM.Web.Models;
using Microsoft.AspNetCore.Mvc;
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
        public string Index(string searchString, bool notUsed)
        {
            return "From [HttpPost]Index: filter on " + searchString;
        }

        [HttpGet]
        public IActionResult Editar(string id)
        {
            var aluno = _rep.Obtenha(id);

            return View(aluno);

        }

        [HttpPost]
        public IActionResult Editar(Aluno getAluno)
        {

            var aluno = new Aluno()
            {

                Matricula = getAluno.Matricula,
                Nome = getAluno.Nome?.ToUpper().Trim(),
                Sexo = getAluno.Sexo,
                Nascimento = getAluno.Nascimento,
                CPF = getAluno.CPF,
            };

            if (getAluno.Matricula == aluno.Matricula && !string.IsNullOrWhiteSpace(aluno.Nome))
            {
                try
                {
                    _rep.Atualize(aluno);
                    ViewBag.Mensagem = "Atualizado!";
                    return View();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Cadastrar");
                    ViewBag.Mensagem = "erro";
                }
            }
            else
            {
                ViewBag.Mensagem = "Favor preencha o nome!";
                return View();
            }
            return View();
        }

        public IActionResult Cadastrar()
        {
            Aluno aluno = new Aluno();

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

            return View(aluno);
        }


        [HttpPost]
        public IActionResult Cadastrar(Aluno getAluno)
        {
            int getUltimaMatriculaMaisUm = 0;

            if (!string.IsNullOrEmpty(_rep.ObtenhaTodos().Max(a => a.Matricula.ToString())))
            {
                getUltimaMatriculaMaisUm = _rep.ObtenhaTodos().Max(a => a.Matricula.ToString()) == "" ? 1 : _rep.ObtenhaTodos().Max(a => a.Matricula) + 1;
            }
            else
            {
                getUltimaMatriculaMaisUm = 1;
            }

            var aluno = new Aluno()
            {
                Matricula = (getAluno.Matricula > 0) ? getAluno.Matricula : 1,
                Nome = getAluno.Nome?.ToUpper().Trim(),
                Sexo = getAluno.Sexo,
                Nascimento = getAluno.Nascimento,
                CPF = getAluno.CPF,
            };
            Aluno? verificaSeMatriculaExiste = _rep.Obtenha(getAluno.Matricula.ToString());

            if (verificaSeMatriculaExiste == null && !string.IsNullOrWhiteSpace(aluno.Nome))
            {
                try
                {
                    _rep.Adicione(aluno);
                    ViewBag.Mensagem = "Cadastrado!";
                    return View(); 
                    
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Cadastrar");
                    ViewBag.Mensagem = "erro";
                }
            }
            else
            {
                ViewBag.Mensagem = "erro";
                return View();
            }
            return View();

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