using System;
using System.Collections.Generic;
using System.Data;
using Dapper;
using ConsoleApp.Data;
using ConsoleApp.Models;

namespace ConsoleApp.Controllers
{
    public class PessoaController
    {
        public DataContext Db;

        public PessoaController(DataContext db)
        {
            Db = db;
        }

        public IEnumerable<PessoaModel> GetAllPessoas(DataContext Db)
        {
            return Db.Connection
                    .Query<PessoaModel>(
                        "SELECT * FROM dbo.Pessoas",
                        commandType: CommandType.Text
                    );
        }

        public IEnumerable<PessoaModel> GetPessoaById(DataContext Db, int Id)
        {
            return Db.Connection
                    .Query<PessoaModel>(
                        "SELECT * FROM dbo.Pessoas WHERE ID = '" + Id + "'",
                        commandType: CommandType.Text
                    );
        }

        public DbExecuteResultModel InsertPessoa(DataContext Db, PessoaModel pessoa)
        {
            try
            {
                Db.Connection
                    .Execute(
                        "INSERT INTO dbo.Pessoas (" +
                            "Nome," +
                            "Email," +
                            "Idade," +
                            "Criado" +
                        ")VALUES(" +
                            "'" + pessoa.Nome + "'," +
                            "'" + pessoa.Email + "'," +
                            "'" + pessoa.Idade.ToString() + "'," +
                            "GETDATE()" +
                        ")",
                        commandType: CommandType.Text
                    );

                return new DbExecuteResultModel(true, "Cadastro realizado com sucesso.");
            }
            catch (Exception e)
            {
                return new DbExecuteResultModel(false, "Erro ao tentar cadastrar a pessoa: " + e.Message);
            }
        }

        public DbExecuteResultModel UpdatePessoa(DataContext Db, PessoaModel pessoa)
        {
            try
            {
                Db.Connection
                    .Execute(
                        "UPDATE dbo.Pessoas " +
                            "SET " +
                            "Nome = '" + pessoa.Nome + "'," +
                            "Email = '" + pessoa.Email + "'," +
                            "Idade = '" + pessoa.Idade.ToString() + "'" +
                        "WHERE " +
                            "Id = '" + pessoa.Id.ToString() + "'",
                        commandType: CommandType.Text
                    );

                return new DbExecuteResultModel(true, "Cadastro atualizado com sucesso.");
            }
            catch (Exception e)
            {
                return new DbExecuteResultModel(false, "Erro ao tentar atualizar a pessoa: " + e.Message);
            }
        }

         public DbExecuteResultModel DeletePessoa(DataContext Db, int Id)
        {
            try
            {
                Db.Connection
                    .Execute(
                        "DELETE dbo.Pessoas " +
                        "WHERE " +
                            "Id = '" + Id.ToString() + "'",
                        commandType: CommandType.Text
                    );

                return new DbExecuteResultModel(true, "Cadastro excluido com sucesso.");
            }
            catch (Exception e)
            {
                return new DbExecuteResultModel(false, "Erro ao tentar excluir a pessoa: " + e.Message);
            }
        }
    }
}
