using System.Data;
using FirebirdSql.Data.FirebirdClient;

namespace EM.Repository;

public class Banco
{
    public static FbConnection ObtenhaConexao()
    {
        string conexao = @"User=SYSDBA;Password=masterkey;Database=192.168.3.24/3054:C:\temp\MeuProjetoEM\EM.Repository\BancoDeDados\PROJETOEM.FB4;DataSource=localhost;Dialect=3;Charset=NONE;Pooling=true;user=sysdba;password=masterkey;dialect=3;";
        return new FbConnection(conexao);
    }

    public static DataTable Consulta(string sql)
    {
        DataTable dt = new();
        try
        {
            using FbConnection conexaoFireBird = ObtenhaConexao();
            {
                conexaoFireBird.Open();
                FbDataAdapter? da = new(sql, conexaoFireBird);
                da.Fill(dt);
                conexaoFireBird.Close();
                return dt;
            }
        }
        catch (Exception)
        {
            throw;
        }
    }
}