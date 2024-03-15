using FirebirdSql.Data.FirebirdClient;
using EM.Domain.ProjetoEM.EM.Domain;
using System.Data;
using EM.Domain;

namespace EM.Repository
{
    public class AlunoRepository : RepositorioAbstrato<Aluno>
    {


        public override IEnumerable<Aluno> ObtenhaTodos()
        {

            List<Aluno> alunos = [];

            string sql = "SELECT MATRICULA, NOME, SEXO, CPF, NASCIMENTO FROM ALUNO";
            DataTable dt = Banco.Consulta(sql);

            foreach (DataRow item in dt.Rows)
            {
                Aluno aluno = new()
                {
                    Matricula = item.Field<Int32>("MATRICULA"),
                    Nome = item.Field<string>("NOME"),
                    Sexo = item.Field<Sexo>("SEXO"),
                    CPF = item.Field<string>("CPF"),
                    Nascimento = item.Field<DateTime>("NASCIMENTO"),
                };

                alunos.Add(aluno);
            }

            return alunos;
        }

        public override void Adicione(Aluno aluno)
        {
            using FbConnection conexaoFireBird = Banco.ObtenhaConexao();
            try
            {
                conexaoFireBird.Open();
                string mSQL = "INSERT INTO ALUNO (MATRICULA, NOME, SEXO, CPF, NASCIMENTO) VALUES (@MATRICULA, @NOME, @SEXO, @CPF, @NASCIMENTO)";
                FbCommand cmd = new(mSQL, conexaoFireBird);

                cmd.Parameters.Add("@MATRICULA", SqlDbType.Int);
                cmd.Parameters.Add("@NOME", SqlDbType.VarChar);
                cmd.Parameters.Add("@SEXO", SqlDbType.VarChar);
                cmd.Parameters.Add("@CPF", SqlDbType.VarChar);
                cmd.Parameters.Add("@NASCIMENTO", SqlDbType.DateTime);

                cmd.Parameters["@MATRICULA"].Value = aluno.Matricula;
                cmd.Parameters["@NOME"].Value = aluno.Nome;
                cmd.Parameters["@SEXO"].Value = aluno.Sexo;
                cmd.Parameters["@CPF"].Value = aluno.CPF;
                cmd.Parameters["@NASCIMENTO"].Value = aluno.Nascimento;


                cmd.ExecuteNonQuery();
            }
            catch (FbException)
            {
                throw;
            }
            finally
            {
                conexaoFireBird.Close();
            }
        }

        public override void Atualize(Aluno aluno)
        {
            using FbConnection conexaoFireBird = Banco.ObtenhaConexao();

            try
            {
                conexaoFireBird.Open();
                string mSQL = "UPDATE ALUNO SET NOME = @NOME, SEXO = @SEXO, CPF = @CPF, NASCIMENTO = @NASCIMENTO WHERE MATRICULA = @MATRICULA";

                FbCommand cmd = new(mSQL, conexaoFireBird);

                cmd.Parameters.Add("@MATRICULA", SqlDbType.Int);
                cmd.Parameters.Add("@NOME", SqlDbType.VarChar);
                cmd.Parameters.Add("@SEXO", SqlDbType.VarChar);
                cmd.Parameters.Add("@CPF", SqlDbType.VarChar);
                cmd.Parameters.Add("@NASCIMENTO", SqlDbType.DateTime);

                cmd.Parameters["@MATRICULA"].Value = aluno.Matricula;
                cmd.Parameters["@NOME"].Value = aluno.Nome;
                cmd.Parameters["@SEXO"].Value = aluno.Sexo;
                cmd.Parameters["@CPF"].Value = aluno.CPF;
                cmd.Parameters["@NASCIMENTO"].Value = aluno.Nascimento;


                cmd.ExecuteNonQuery();
            }
            catch (FbException)
            {
                throw;
            }
            finally
            {
                conexaoFireBird.Close();
            }
        }

        public override void Remova(Aluno aluno)
        {
            try
            {
                var sql = $"DELETE FROM ALUNO WHERE MATRICULA = '{aluno.Matricula}'";
                Banco.Consulta(sql);
            }
            catch (FbException)
            {
                throw;
            }

        }

        public override Aluno? Obtenha(string id)
        {
            Aluno alunoObtido = new();
            var mat = id;

            using FbConnection conexaoFireBird = Banco.ObtenhaConexao();

            string sql = "SELECT MATRICULA, NOME, SEXO, CPF, NASCIMENTO FROM ALUNO WHERE MATRICULA = " + mat;
            DataTable dt = Banco.Consulta(sql);

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow item in dt.Rows)
                {
                    Aluno aluno = new()
                    {
                        Matricula = item.Field<Int32>("MATRICULA"),
                        Nome = item.Field<string>("NOME"),
                        Sexo = item.Field<Sexo>("SEXO"),
                        CPF = item.Field<string>("CPF"),
                        Nascimento = item.Field<DateTime>("NASCIMENTO"),
                    };

                    return alunoObtido = aluno;
                }
            }

            return null;
        }
    }
}

