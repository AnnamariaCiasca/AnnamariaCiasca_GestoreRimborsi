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
    public class RepositoryDipendente : IRepositoryDipendenti
    {
        const string connectionString = @"Data Source = (localdb)\MSSQLLocalDB;" +
                                     "Initial Catalog = AcademyI.EsercitazioneFinale;" +
                                     "Integrated Security = true";

        public List<Dipendente> FetchDipendenti()
        {
            try
            {

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand();
                    command.Connection = connection;
                    command.CommandType = System.Data.CommandType.Text;
                    command.CommandText = "SELECT * FROM dbo.Dipendenti";


                    SqlDataReader reader = command.ExecuteReader();

                    List<Dipendente> dipendenti = new List<Dipendente>();

                    while (reader.Read())
                    {

                        Dipendente dipendente = new Dipendente();
                        dipendente.IdDipendente = (int)reader["Id"];
                        dipendente.Nome = (string)reader["Nome"];


                        dipendenti.Add(dipendente);

                    }
                    return dipendenti;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Update(Dipendente item)
        {
            throw new NotImplementedException();
        }

    }


}
