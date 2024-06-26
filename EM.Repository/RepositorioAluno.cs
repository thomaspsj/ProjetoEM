﻿using FirebirdSql.Data.FirebirdClient;
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
                string mSQL = "INSERT INTO ALUNO (MATRICULA, NOME, SEXO, CPF, NASCIMENTO, CEP, LOGRADOURO, BAIRRO, CIDADE, ESTADO) VALUES (@MATRICULA, @NOME, @SEXO, @CPF, @NASCIMENTO, @CEP, @LOGRADOURO, @BAIRRO, @CIDADE, @ESTADO)";
                FbCommand cmd = new(mSQL, conexaoFireBird);

                cmd.Parameters.Add("@MATRICULA", SqlDbType.Int);
                cmd.Parameters.Add("@NOME", SqlDbType.VarChar);
                cmd.Parameters.Add("@SEXO", SqlDbType.VarChar);
                cmd.Parameters.Add("@CPF", SqlDbType.VarChar);
                cmd.Parameters.Add("@NASCIMENTO", SqlDbType.DateTime);
                cmd.Parameters.Add("@CEP", SqlDbType.DateTime);
                cmd.Parameters.Add("@LOGRADOURO", SqlDbType.VarChar);
                cmd.Parameters.Add("@BAIRRO", SqlDbType.VarChar);
                cmd.Parameters.Add("@CIDADE", SqlDbType.VarChar);
                cmd.Parameters.Add("@ESTADO", SqlDbType.Char);

                cmd.Parameters["@MATRICULA"].Value = aluno.Matricula;
                cmd.Parameters["@NOME"].Value = aluno.Nome;
                cmd.Parameters["@SEXO"].Value = aluno.Sexo;
                cmd.Parameters["@CPF"].Value = aluno.CPF;
                cmd.Parameters["@NASCIMENTO"].Value = aluno.Nascimento;
                cmd.Parameters["@CEP"].Value = aluno.CEP;
                cmd.Parameters["@LOGRADOURO"].Value = aluno.Logradouro;
                cmd.Parameters["@BAIRRO"].Value = aluno.Bairro;
                cmd.Parameters["@CIDADE"].Value = aluno.Cidade;
                cmd.Parameters["@ESTADO"].Value = aluno.Estado;


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
                string mSQL = "UPDATE ALUNO SET NOME = @NOME, SEXO = @SEXO, CPF = @CPF, NASCIMENTO = @NASCIMENTO, CEP = @CEP, LOGRADOURO = @LOGRADOURO, BAIRRO = @BAIRRO, CIDADE = @CIDADE, ESTADO = @ESTADO WHERE MATRICULA = @MATRICULA";

                FbCommand cmd = new(mSQL, conexaoFireBird);

                cmd.Parameters.Add("@MATRICULA", SqlDbType.Int);
                cmd.Parameters.Add("@NOME", SqlDbType.VarChar);
                cmd.Parameters.Add("@SEXO", SqlDbType.VarChar);
                cmd.Parameters.Add("@CPF", SqlDbType.VarChar);
                cmd.Parameters.Add("@NASCIMENTO", SqlDbType.DateTime);
                cmd.Parameters.Add("@CEP", SqlDbType.VarChar);
                cmd.Parameters.Add("@LOGRADOURO", SqlDbType.VarChar);
                cmd.Parameters.Add("@BAIRRO", SqlDbType.VarChar);
                cmd.Parameters.Add("@CIDADE", SqlDbType.VarChar);
                cmd.Parameters.Add("@ESTADO", SqlDbType.Char);

                cmd.Parameters["@MATRICULA"].Value = aluno.Matricula;
                cmd.Parameters["@NOME"].Value = aluno.Nome;
                cmd.Parameters["@SEXO"].Value = aluno.Sexo;
                cmd.Parameters["@CPF"].Value = aluno.CPF;
                cmd.Parameters["@NASCIMENTO"].Value = aluno.Nascimento;
                cmd.Parameters["@CEP"].Value = aluno.CEP;
                cmd.Parameters["@LOGRADOURO"].Value = aluno.Logradouro;
                cmd.Parameters["@BAIRRO"].Value = aluno.Bairro;
                cmd.Parameters["@CIDADE"].Value = aluno.Cidade;
                cmd.Parameters["@ESTADO"].Value = aluno.Estado;


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

        public override Aluno? Obtenha(string obj)
        {
            Aluno alunoObtido = new();
            var mat = obj;

            using FbConnection conexaoFireBird = Banco.ObtenhaConexao();

            string sql = "SELECT MATRICULA, NOME, SEXO,  CPF, NASCIMENTO, CEP, LOGRADOURO, BAIRRO, CIDADE, ESTADO FROM ALUNO WHERE MATRICULA = " + mat;
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
                        CEP = item.Field<string>("CEP"),
                        Logradouro = item.Field<string>("LOGRADOURO"),
                        Bairro = item.Field<string>("BAIRRO"),
                        Cidade = item.Field<string>("CIDADE"),
                        Estado = item.Field<string>("ESTADO"),




                    };

                    return alunoObtido = aluno;
                }
            }

            return null;
        }

        public override Aluno? ObtenhaMatricula(int obj)
        {
            Aluno alunoObtido = new();
            var mat = obj;

            using FbConnection conexaoFireBird = Banco.ObtenhaConexao();

            string sql = "SELECT MATRICULA FROM ALUNO WHERE MATRICULA = " + mat;
            DataTable dt = Banco.Consulta(sql);

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow item in dt.Rows)
                {
                    Aluno aluno = new()
                    {
                        Matricula = item.Field<Int32>("MATRICULA"),
                    };

                    return alunoObtido = aluno;
                }
            }

            return null;
        }


    }
}

