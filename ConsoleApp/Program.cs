using System;
using System.Collections.Generic;
using System.Linq;
using ConsoleApp.Data;
using ConsoleApp.Models;
using ConsoleApp.Controllers;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var Db = new DataContext();
            var pessoaModel = new PessoaModel();
            var pessoaController = new PessoaController(Db);
            DbExecuteResultModel dbExecuteResultModel;
            IEnumerable<PessoaModel> procuraPessoa;
            int menuOpt, Id, tentativas;
            do{
                menuOpt = Menu();
                switch (menuOpt)
                {
                    case 1:
                        var Pessoas = pessoaController.GetAllPessoas(Db);

                        Console.WriteLine("++++++++++++++++++++++ CADASTRO DE PESSOAS ++++++++++++++++++++\n");

                        bool FlagNext = false;
                        foreach (var Item in Pessoas)
                        {
                            if (FlagNext)
                            {
                                Console.WriteLine("\n======================================================\n");
                            }

                            Console.WriteLine("ID: {0}", Item.Id.ToString());
                            Console.WriteLine("Nome: {0}", Item.Nome);
                            Console.WriteLine("Email: {0}", Item.Email);
                            Console.WriteLine("Idade: {0}", Item.Idade.ToString());
                            Console.WriteLine("Data Cadastro: {0}", Item.Criado.ToString());

                            FlagNext = true;
                        }
                        Console.ReadLine();
                    break;
                    case 2:
                        do
                        {
                            Console.Clear();
                            Console.WriteLine("Informe o Id da pessoa para consultar.");
                            int.TryParse(Console.ReadLine(), out Id);
                            if (Id <= 0)
                            {
                                Console.Clear();
                                Console.WriteLine("Id inválido, digite um Id maior que 0.");
                                Console.ReadLine();
                            }
                        } while (Id <= 0);

                        var Pessoa = pessoaController.GetPessoaById(Db, Id);

                        if (Pessoa.Any())
                        {
                            Console.WriteLine("ID: {0}", Pessoa.FirstOrDefault().Id.ToString());
                            Console.WriteLine("Nome: {0}", Pessoa.FirstOrDefault().Nome);
                            Console.WriteLine("Email: {0}", Pessoa.FirstOrDefault().Email);
                            Console.WriteLine("Idade: {0}", Pessoa.FirstOrDefault().Idade.ToString());
                            Console.WriteLine("Data Cadastro: {0}", Pessoa.FirstOrDefault().Criado.ToString());
                        }
                        else
                        {
                            Console.WriteLine("Cadastro não encontrado.");
                        }
                        Console.ReadLine();
                    break;
                    case 3:
                        do{
                            Console.WriteLine("Informe o Nome da pessoa.");
                            pessoaModel.Nome = Console.ReadLine();
                        } while (pessoaModel.Nome == "");
                        do
                        {
                            Console.WriteLine("Informe o Email da pessoa.");
                            pessoaModel.Email = Console.ReadLine();
                        } while (pessoaModel.Email == "" || !IsValidEmail(pessoaModel.Email));
                        do
                        {
                            Console.WriteLine("Informe a Idade da pessoa.");
                            int idade;
                            int.TryParse(Console.ReadLine(), out idade);
                            pessoaModel.Idade = idade;
                        } while (pessoaModel.Idade <= 0);

                        dbExecuteResultModel = pessoaController.InsertPessoa(Db, pessoaModel);

                        Console.WriteLine(dbExecuteResultModel.Message);

                        Console.ReadLine();
                    break;
                    case 4:
                        tentativas = 0;
                        Id = 0;
                        do
                        {
                            if(tentativas > 3)
                            {
                                break;
                            }
                            Console.WriteLine("Informe o Id da pessoa.");
                            int.TryParse(Console.ReadLine(), out Id);
                            procuraPessoa = pessoaController.GetPessoaById(Db, Id);
                            if (procuraPessoa.Count() != 1)
                            {
                                Console.WriteLine("Id inválido.");
                                Console.ReadLine();
                                Console.Clear();
                            }
                            tentativas++;
                        } while (Id <= 0 || procuraPessoa.Count() != 1);
                        pessoaModel.Id = Id;
                        
                        do
                        {
                            Console.WriteLine("Informe o Nome da pessoa.");
                            pessoaModel.Nome = Console.ReadLine();
                        } while (pessoaModel.Nome == "");
                        do
                        {
                            Console.WriteLine("Informe o Email da pessoa.");
                            pessoaModel.Email = Console.ReadLine();
                        } while (pessoaModel.Email == "" || !IsValidEmail(pessoaModel.Email));
                        do
                        {
                            Console.WriteLine("Informe a Idade da pessoa.");
                            int idade;
                            int.TryParse(Console.ReadLine(), out idade);
                            pessoaModel.Idade = idade;
                        } while (pessoaModel.Idade <= 0);

                        dbExecuteResultModel = pessoaController.UpdatePessoa(Db, pessoaModel);

                        Console.WriteLine(dbExecuteResultModel.Message);

                        Console.ReadLine();
                    break;
                    case 5:
                        tentativas = 0;
                        Id = 0;
                        do
                        {
                            if (tentativas > 3)
                            {
                                break;
                            }
                            Console.WriteLine("Informe o Id da pessoa.");
                            int.TryParse(Console.ReadLine(), out Id);
                            procuraPessoa = pessoaController.GetPessoaById(Db, Id);
                            if (procuraPessoa.Count() != 1)
                            {
                                Console.WriteLine("Id inválido.");
                                Console.ReadLine();
                                Console.Clear();
                            }
                            tentativas++;
                        } while (Id <= 0 || procuraPessoa.Count() != 1);
                        pessoaModel.Id = Id;

                        dbExecuteResultModel = pessoaController.DeletePessoa(Db, Id);

                        Console.WriteLine(dbExecuteResultModel.Message);

                        Console.ReadLine();
                    break;
                }

                
            } while (menuOpt >= 1 && menuOpt <= 5);
        }

        public static int Menu()
        {
            int menuOpt;

            var menu = new Dictionary<int, string>();

            menu.Add(1,"Consultar todos os cadastros de pessoas.");
            menu.Add(2, "Consultar pessoas por Id.");
            menu.Add(3,"Cadastrar pessoa.");
            menu.Add(4,"Atualizar pessoa.");
            menu.Add(5,"Excluir pessoa.");
            menu.Add(6, "Sair.");

            do
            {
                Console.Clear();
                foreach(var item in menu)
                {
                    Console.WriteLine("{0} - {1}", item.Key, item.Value);
                }
                Console.WriteLine("========================================");
                Console.WriteLine("Informe o numero de uma das opções do menu.");
                int.TryParse(Console.ReadLine(), out menuOpt);
                if (!menu.ContainsKey(menuOpt))
                {
                    Console.Clear();
                    Console.WriteLine("Opção inválida, escolha uma opção numérica contida no menu.");
                    Console.ReadLine();
                }
            } while (!menu.ContainsKey(menuOpt));

            Console.Clear();

            return menuOpt;
        }

        public static bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        
    }
}
