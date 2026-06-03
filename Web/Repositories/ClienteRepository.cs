using MySqlConnector;
using Dapper;
using nIKernel.Models.Cliente;

namespace nIKernel.Repositories
{
    public class ClienteRepository
    {
        private readonly string _connectionString;

        public ClienteRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")
                ?? throw new Exception("Connection string não encontrada");
        }

        public async Task<IEnumerable<ClienteModel>> ListarTodosAsync()
        {
            using var db = new MySqlConnection(_connectionString);
            string sql = @"SELECT * FROM TB_CL_CLIENTES";
            return await db.QueryAsync<ClienteModel>(sql);
        }

        public async Task InserirAsync(ClienteModel cliente)
        {
            using var db = new MySqlConnection(_connectionString);

            cliente.CL_data_inclusao = DateTime.Now;
            if (cliente.CL_status == null || cliente.CL_status == "")
            {
                cliente.CL_status= "A";
            }
             
            string sql = @"INSERT INTO TB_CL_CLIENTES (CL_cpf_cnpj, CL_rg_ie, CL_nome, CL_apelido, CL_status, CL_data_inclusao) VALUES (@CL_cpf_cnpj, @CL_rg_ie, @CL_nome, @CL_apelido, @CL_status, @CL_data_inclusao);
            SELECT LAST_INSERT_ID();";

            cliente.CL_id = await db.ExecuteScalarAsync<int>(sql, cliente);
            
            string sql_end = @"INSERT INTO TB_END_CLIENTES (CL_ID, END_CEP, END_LOG, END_NUM, END_CPL, END_BAI, END_CID, END_EST) VALUES(@CL_id, @END_CEP, @END_LOG, @END_NUM, @END_CPL, @END_BAI, @END_CID, @END_EST);";
            await db.ExecuteAsync(sql_end, cliente);
        }

        public async Task DeletarAsync(int id)
        {
            using var db = new MySqlConnection(_connectionString);
            string sql = "DELETE FROM TB_CL_CLIENTES WHERE CL_id = @Id";
            await db.ExecuteAsync(sql, new { Id = id });
        }

        public async Task<ClienteModel?> BuscarPorIdAsync(int id)
        {
            using var db = new MySqlConnection(_connectionString);
            string sql = @"SELECT c.*, e.END_CEP, e.END_LOG, e.END_NUM, e.END_CPL,
                    e.END_BAI, e.END_CID, e.END_EST
                FROM TB_CL_CLIENTES c
                LEFT JOIN TB_END_CLIENTES e ON e.CL_ID = c.CL_id
                WHERE c.CL_id = @Id";
            return await db.QueryFirstOrDefaultAsync<ClienteModel>(sql, new { Id = id });
        }

        public async Task AtualizarAsync(ClienteModel cliente)
        {
            using var db = new MySqlConnection(_connectionString);

            string sql = @"UPDATE TB_CL_CLIENTES
                SET CL_cpf_cnpj = @CL_cpf_cnpj, 
                    CL_rg_ie = @CL_rg_ie, 
                    CL_nome = @CL_nome, 
                    CL_apelido = @CL_apelido,
                    CL_status = @CL_status
                WHERE CL_id = @CL_id";
            
            await db.ExecuteAsync(sql, cliente);
            
            string sql_end = @"UPDATE TB_END_CLIENTES
                SET END_CEP = @END_CEP,
                    END_LOG = @END_LOG,
                    END_NUM = @END_NUM,
                    END_CPL = @END_CPL,
                    END_BAI = @END_BAI,
                    END_CID = @END_CID,
                    END_EST = @END_EST
                WHERE CL_ID = @CL_id";
             await db.ExecuteAsync(sql_end, cliente);
        }
    }
}