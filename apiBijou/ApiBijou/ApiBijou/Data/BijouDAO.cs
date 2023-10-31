using MySql.Data.MySqlClient;
using API_SAE.Model;



namespace API_SAE.Data
{
    /// <summary>
    /// DAO pour accèder à la base de données MySQL
    /// </summary>
    public class BijouDAO : IBijouDAO
    {
        /// <summary>
        /// String de connexion au serveur
        /// </summary>
        private string connectionString = "Server=localhost;Port=3306;Database=elaparaiba;Uid=root;Pwd=rootroot;";


        public Bijou getById(int id) // Renommez la méthode pour respecter la convention C# (PascalCase)
        {
            MySqlConnection conn = OpenConnection();
            string sqlQuery = "SELECT * FROM bijoux WHERE idBijou = @id";
            MySqlCommand command = new MySqlCommand(sqlQuery, conn);
            command.Parameters.AddWithValue("@id", id);
            MySqlDataReader reader = command.ExecuteReader();
            Bijou bijou = new Bijou();

            if (reader.Read())
            {
                bijou.Id = Convert.ToInt32(reader["idBijou"]);
                bijou.Name = reader["nomBijou"].ToString();
                bijou.Description = reader["descriptionBijou"].ToString();
                bijou.Quantity = Convert.ToInt32(reader["stockBijou"]);
                bijou.Price = Convert.ToInt32(reader["prixBijou"]);
            }

            CloseConnection(conn);
            return bijou;
        }

        public IEnumerable<Bijou> GetAllBijoux()
        {
            MySqlConnection conn = OpenConnection();
            string sqlQuery = "SELECT * FROM bijoux";
            MySqlCommand command = new MySqlCommand(sqlQuery, conn);
            MySqlDataReader reader = command.ExecuteReader();
            List<Bijou> bijoux = new List<Bijou>();
            while (reader.Read())
            {
                Bijou bijou = new Bijou();

                bijou.Id = Convert.ToInt32(reader["idBijou"]);
                bijou.Name = reader["nomBijou"].ToString();
                bijou.Description = reader["descriptionBijou"].ToString();
                bijou.Quantity = Convert.ToInt32(reader["stockBijou"]);
                bijou.Price = Convert.ToInt32(reader["prixBijou"]);

                bijoux.Add(bijou);
            }
            CloseConnection(conn);
            return bijoux;
        }

        public bool AddBijou(Bijou? bijou)
        {
            throw new NotImplementedException();
        }

        public bool DeleteBijouById(int id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Ouvre une connexion à la BDD mySQL
        /// </summary>
        /// <returns></returns>
        public MySqlConnection OpenConnection()
        {
            MySqlConnection connection = new MySqlConnection(connectionString);
            try
            {
                connection.Open();
                return connection;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        /// <summary>
        /// Ferme une connexion à la bdd MySql
        /// </summary>
        /// <param name="connection"></param>
        public void CloseConnection(MySqlConnection connection)
        {
            if (connection != null)
            {
                try
                {
                    if (connection.State != System.Data.ConnectionState.Closed)
                    {
                        connection.Close();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}
