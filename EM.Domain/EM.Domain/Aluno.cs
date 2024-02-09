namespace EM.Domain
{
    using EM.Domain.Interface;

    namespace ProjetoEM.EM.Domain
    {
        public class Aluno : IEntidade
        {
            public int Matricula { get; set; }
            public string? Nome { get; set; }
            public Sexo Sexo { get; set; }
            public DateTime Nascimento { get; set; }
            public string? CPF { get; set; }

            public Aluno(int matricula, string? nome, Sexo sexo, DateTime nascimento, string? cPF)
            {
                Matricula = matricula;
                Nome = nome;
                Sexo = sexo;
                Nascimento = nascimento;
                CPF = cPF;
            }

            public override bool Equals(object? obj)
            {
                return base.Equals(obj);
            }

            public override int GetHashCode()
            {
                return base.GetHashCode();
            }

            public override string ToString()
            {
                return base.ToString();
            }
        }
    }
}



