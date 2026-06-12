using Dapper;
using MySqlConnector;
using Web.Models.Categoria;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Web.Repositories
{
    public class CategoriaRepository
    {
        private readonly string _connectionString;
        public CategoriaRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection") ?? "";
        }

        public async Task<IEnumerable<CtgCategoriaModel>> ListarTodosAsync(bool apenasAtivos = true)
        {
            using var db = new MySqlConnection(_connectionString);
            string sql = @"SELECT CTG_ID as ctg_id, CTG_DCC as ctg_dcc, CTG_STA as ctg_sta
                           FROM tb_ctg_categorias
                           WHERE (@ApenasAtivos = 0 OR CTG_STA = 'S')
                           ORDER BY CTG_DCC";
            return await db.QueryAsync<CtgCategoriaModel>(sql, new { ApenasAtivos = apenasAtivos ? 1 : 0 });
        }

        public async Task<CtgCategoriaModel?> BuscarPorIdAsync(int id)
        {
            using var db = new MySqlConnection(_connectionString);
            string sql = @"SELECT CTG_ID as ctg_id, CTG_DCC as ctg_dcc, CTG_STA as ctg_sta
                           FROM tb_ctg_categorias
                           WHERE CTG_ID = @Id";
            return await db.QueryFirstOrDefaultAsync<CtgCategoriaModel>(sql, new { Id = id });
        }

        public async Task InserirAsync(CtgCategoriaModel categoria)
        {
            using var db = new MySqlConnection(_connectionString);
            string sql = @"INSERT INTO tb_ctg_categorias (CTG_DCC, CTG_STA)
                           VALUES (@ctg_dcc, @ctg_sta)";
            await db.ExecuteAsync(sql, categoria);
        }

        public async Task AtualizarAsync(CtgCategoriaModel categoria)
        {
            using var db = new MySqlConnection(_connectionString);
            string sql = @"UPDATE tb_ctg_categorias SET CTG_DCC = @ctg_dcc, CTG_STA = @ctg_sta WHERE CTG_ID = @ctg_id";
            await db.ExecuteAsync(sql, categoria);
        }

        public async Task DeletarAsync(int id)
        {
            using var db = new MySqlConnection(_connectionString);
            string sql = "UPDATE tb_ctg_categorias SET CTG_STA = 'N' WHERE CTG_ID = @Id";
            await db.ExecuteAsync(sql, new { Id = id });
        }
    }
}
