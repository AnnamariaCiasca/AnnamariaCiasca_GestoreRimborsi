using GestoreRimborsi.Core.Entities;
using GestoreRimborsi.Core.RepositoryInterface;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestoreRimborsi.AdoRepository
{
    public class RepositoryMonitoraggioSpesa : IRepositoryMonitoraggioSpese
    {
        const string connectionString = @"Data Source = (localdb)\MSSQLLocalDB;" +
                                         "Initial Catalog = AcademyI.EsercitazioneFinale;" +
                                         "Integrated Security = true";


        public List<MonitoraggioSpesa> GetItemsWithOutApproval()
        {
            try
            {

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand();
                    command.Connection = connection;
                    command.CommandType = System.Data.CommandType.Text;
                    command.CommandText = "SELECT * FROM dbo.Spese WHERE Approvata IS NULL";


                    SqlDataReader reader = command.ExecuteReader();

                    List<MonitoraggioSpesa> monitoraggi = new List<MonitoraggioSpesa>();

                    while (reader.Read())
                    {

                        MonitoraggioSpesa monitoraggio = new MonitoraggioSpesa();
                        monitoraggio.Id = (int)reader["Id"];
                        monitoraggio.Data = Convert.ToDateTime(reader["Data"]);
                        monitoraggio.Spesa = (double)reader["Spesa"];
                        monitoraggio.Categoria = (CategoriaEnum)Convert.ToInt32(reader["Categoria"]);
                        monitoraggio.Descrizione = (string)reader["Descrizione"];
                      

                        monitoraggi.Add(monitoraggio);

                    }
                    return monitoraggi;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void Update(MonitoraggioSpesa item)
        {
            try {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand();
                    command.Connection = connection;
                    command.CommandType = System.Data.CommandType.Text;
                    command.CommandText = "update dbo.Spese set Approvata = @approvata, Rimborso = @rimborso, Approvatore = @approvatore where Id= @id";
                    command.Parameters.AddWithValue("@approvata", item.Approvata);
                    command.Parameters.AddWithValue("@rimborso", item.Rimborso);
                    if (item.Approvatore != null)
                    {
                        command.Parameters.AddWithValue("@approvatore", item.Approvatore);
                    }
                    else
                    {
                        command.Parameters.AddWithValue("@approvatore", DBNull.Value);
                    }
                    command.Parameters.AddWithValue("@id", item.Id);
                    command.ExecuteNonQuery();
                }

            }
            catch(Exception ex)
            {
                throw ex;
            }

            }


        public List<MonitoraggioSpesa> GetByName(string nome)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand();
                    command.Connection = connection;
                    command.CommandType = System.Data.CommandType.Text;
                    command.CommandText = "" +
                        "SELECT S.Data, S.Categoria, S.Spesa, S.Approvata, S.Rimborso " +
                        " FROM dbo.Spese as S " +
                        " JOIN dbo.Dipendenti as D ON S.Dipendente = D.Id" +
                        " JOIN dbo.Categorie as C ON S.Categoria = C.Id" +
                        " WHERE D.Nome = @nome";
                    command.Parameters.AddWithValue("@Nome", nome);

                    SqlDataReader reader = command.ExecuteReader();

                    List<MonitoraggioSpesa> monitoraggi = new List<MonitoraggioSpesa>();

                    while (reader.Read())
                    {

                        MonitoraggioSpesa monitoraggio = new MonitoraggioSpesa();
                        monitoraggio.Data = Convert.ToDateTime(reader["Data"]);
                        monitoraggio.Spesa = (double)reader["Spesa"];
                        monitoraggio.Categoria = (CategoriaEnum)(reader["Categoria"]);
                        monitoraggio.Approvata = (bool)reader["Approvata"];
                        monitoraggio.Rimborso = (double)reader["Rimborso"];



                        monitoraggi.Add(monitoraggio);

                    }
                    return monitoraggi;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        }
    }

