using MauiAppMinhasCompras.Models;
using SQLite;

// classe que acessa o SQLite
// faz criação, leitura e atualização de dados
// readonly evita que seja redefinido acidentalmente

namespace MauiAppMinhasCompras.Helpers
{
    public class SQLiteDatabaseHelper
    {
        readonly SQLiteAsyncConnection _conn;

        // cria uma tabela de modo assincrono - se existir não faz nada

        public SQLiteDatabaseHelper(string path) 
        {

            _conn = new SQLiteAsyncConnection(path);
            _conn.CreateTableAsync<Produto>().Wait(); 
        }
        // insere um registro do tipo produto no bd 
        // Task - retorna um número inteiro q representa a linha afetada
        public Task<int> Insert(Produto p) 
        {
            return _conn.InsertAsync(p);
        }
        //Atualiza um registro existente na tabela Produto com base no id fornecido
        public Task<List<Produto>> Update(Produto p) 
        {
            string sql = "UPDATE Produto SET Descricao=?, Quantidade=?, Preco=? WHERE Id=?";

            return _conn.QueryAsync<Produto>(
                sql, p.Descricao, p.Quantidade, p.Preco, p.Id);
        }

        //método delete  remove um registro de acordo com o Id fornecido
        public Task<int> Delete(int id) 
        {
            return _conn.Table<Produto>().DeleteAsync(i => i.Id == id);
        }
        
        //método GetAll consulta a tabela e retorna todos os dados em forma de lista
        public Task<List<Produto>> GetAll() 
        {
            return _conn.Table<Produto>().ToListAsync();
        }

        //método Search realiza uma busca na tabela
        public Task<List<Produto>> Search(string q) 
        {
            string sql = "SELECT * FROM Produto WHERE Descricao  LIKE'%" + q + "%'";

            return _conn.QueryAsync<Produto>(sql);
        }
        

    }
}
