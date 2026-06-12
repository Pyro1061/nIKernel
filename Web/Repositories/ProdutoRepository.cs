using Dapper;
using MySqlConnector;
using Web.Models.Produto;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace Web.Repositories
{
    public class ProdutoRepository
    {
        private readonly string _connectionString;

        public ProdutoRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection") ?? "";
        }

        public async Task<IEnumerable<ProdutoModel>> ListarTodosAsync(bool apenasAtivos = true)
        {
            using var db = new MySqlConnection(_connectionString);
            string sql = @"SELECT p.prd_id, p.prd_cod, p.prd_gtin_ean, p.prd_descricao, p.prd_un_compra, p.prd_un_venda, 
                                  p.prd_preco_compra, p.prd_margem_venda, p.prd_preco_venda, p.prd_ativo, p.prd_data_criacao,
                                  p.prd_ctg_id, c.CTG_DCC as prd_ctg_dcc
                           FROM tb_prd_produtos p
                           LEFT JOIN tb_ctg_categorias c ON p.prd_ctg_id = c.CTG_ID
                           WHERE (@ApenasAtivos = 0 OR p.prd_ativo = 'S')
                           ORDER BY p.prd_descricao";
            return await db.QueryAsync<ProdutoModel>(sql, new { ApenasAtivos = apenasAtivos ? 1 : 0 });
        }

        public async Task<ProdutoModel?> BuscarPorIdAsync(int id)
        {
            using var db = new MySqlConnection(_connectionString);
            string sql = @"SELECT p.prd_id, p.prd_cod, p.prd_gtin_ean, p.prd_descricao, p.prd_un_compra, p.prd_un_venda, 
                                  p.prd_preco_compra, p.prd_margem_venda, p.prd_preco_venda, p.prd_ativo, p.prd_data_criacao,
                                  p.prd_ctg_id, c.CTG_DCC as prd_ctg_dcc
                           FROM tb_prd_produtos p
                           LEFT JOIN tb_ctg_categorias c ON p.prd_ctg_id = c.CTG_ID
                           WHERE p.prd_id = @Id";
            return await db.QueryFirstOrDefaultAsync<ProdutoModel>(sql, new { Id = id });
        }

        public async Task InserirAsync(ProdutoModel produto)
        {
            using var db = new MySqlConnection(_connectionString);
                        string sql = @"INSERT INTO tb_prd_produtos 
                                                        (prd_cod, prd_gtin_ean, prd_descricao, prd_un_compra, prd_un_venda, 
                                                         prd_preco_compra, prd_margem_venda, prd_preco_venda, prd_ativo, prd_data_criacao, prd_ctg_id)
                                                     VALUES
                                                        (@prd_cod, @prd_gtin_ean, @prd_descricao, @prd_un_compra, @prd_un_venda, 
                                                         @prd_preco_compra, @prd_margem_venda, @prd_preco_venda, @prd_ativo, @prd_data_criacao, @prd_ctg_id)";
            await db.ExecuteAsync(sql, produto);
        }

        public async Task AtualizarAsync(ProdutoModel produto)
        {
            using var db = new MySqlConnection(_connectionString);
            string sql = @"UPDATE tb_prd_produtos SET
                                prd_cod = @prd_cod,
                                prd_gtin_ean = @prd_gtin_ean,
                                prd_descricao = @prd_descricao,
                                prd_un_compra = @prd_un_compra,
                                prd_un_venda = @prd_un_venda,
                                prd_preco_compra = @prd_preco_compra,
                                prd_margem_venda = @prd_margem_venda,
                                prd_preco_venda = @prd_preco_venda,
                                prd_ativo = @prd_ativo,
                                prd_data_criacao = @prd_data_criacao,
                                prd_ctg_id = @prd_ctg_id
                           WHERE prd_id = @prd_id";
            await db.ExecuteAsync(sql, produto);
        }

        public async Task DeletarAsync(int id)
        {
            using var db = new MySqlConnection(_connectionString);
            string sql = "UPDATE tb_prd_produtos SET prd_ativo = 'N' WHERE prd_id = @Id";
            await db.ExecuteAsync(sql, new { Id = id });
        }
    }
}




