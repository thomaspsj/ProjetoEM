using EM.Domain.Interface;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EM.Domain
{
    namespace ProjetoEM.EM.Domain
    {
        public class Aluno : IEntidade
        {
            [Key]
            [Display(Name = "Matricula")]
            [Column("MATRICULA")]
            public int Matricula { get; set; }

            [Display(Name = "Nome")]
            [Column("NOME")]
            public string? Nome { get; set; }

            [Display(Name = "Sexo")]
            [Column("SEXO")]
            public Sexo Sexo { get; set; }

            [Display(Name = "Nascimento")]
            [Column("NASCIMENTO")]
            [DataType(DataType.Date)]
            public DateTime Nascimento { get; set; }

            [Display(Name = "CPF")]
            [Column("CPF")]
            [MaxLength(14)]
            public string? CPF { get; set; }

            public override bool Equals(object? obj)
            {
                return obj is Aluno aluno &&
                       Matricula == aluno.Matricula &&
                       Nome == aluno.Nome &&
                       CPF == aluno.CPF &&
                       Nascimento == aluno.Nascimento;
            }

        public override int GetHashCode()
        {
            return HashCode.Combine(Matricula);
        }

        public override string ToString() => $"{Matricula} - {Nome}";
        }
    }
}



