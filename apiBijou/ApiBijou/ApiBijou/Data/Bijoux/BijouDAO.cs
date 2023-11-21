using MySql.Data.MySqlClient;
using ApiBijou.Model.Bijoux;

namespace ApiBijou.Data.Bijoux
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


        public Bijou? getById(int id)
        {
            MySqlConnection conn = OpenConnection(); // Ouvre une connexion à la base de données
            string sqlQuery = "SELECT * FROM bijoux WHERE idBijou = @id";
            MySqlCommand command = new MySqlCommand(sqlQuery, conn);
            command.Parameters.AddWithValue("@id", id); // Définit le paramètre d'ID
            MySqlDataReader reader = command.ExecuteReader(); // Exécute la requête SQL
            Bijou bijou = new Bijou(); // Crée un objet Bijou pour stocker les résultats

            if (reader.Read())
            {
                // Si une ligne est lue, remplit l'objet Bijou avec les données
                bijou.Id = Convert.ToInt32(reader["idBijou"]);
                bijou.Name = reader["nomBijou"].ToString();
                bijou.Description = reader["descriptionBijou"].ToString();
                bijou.Quantity = Convert.ToInt32(reader["stockBijou"]);
                bijou.Price = Convert.ToInt32(reader["prixBijou"]);
            }

            CloseConnection(conn); // Ferme la connexion à la base de données
            return bijou; // Retourne le bijou trouvé ou un bijou vide
        }

        public IEnumerable<Bijou> GetAllBijoux()
        {
            MySqlConnection conn = OpenConnection(); // Ouvre une connexion à la base de données
            string sqlQuery = "SELECT * FROM bijoux";
            MySqlCommand command = new MySqlCommand(sqlQuery, conn);
            MySqlDataReader reader = command.ExecuteReader(); // Exécute la requête SQL
            List<Bijou> bijoux = new List<Bijou>(); // Crée une liste pour stocker les bijoux

            while (reader.Read())
            {
                // Pour chaque ligne lue, crée un nouvel objet Bijou et le remplit avec les données
                Bijou bijou = new Bijou();
                bijou.Id = Convert.ToInt32(reader["idBijou"]);
                bijou.Name = reader["nomBijou"].ToString();
                bijou.Description = reader["descriptionBijou"].ToString();
                bijou.Quantity = Convert.ToInt32(reader["stockBijou"]);
                bijou.Price = Convert.ToInt32(reader["prixBijou"]);

                bijoux.Add(bijou); // Ajoute le bijou à la liste
            }

            CloseConnection(conn); // Ferme la connexion à la base de données
            return bijoux; // Retourne la liste de tous les bijoux
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

        public bool ModifierBijou(int idBijou, Bijou bijou)
        {
            throw new NotImplementedException();
        }
    }
}
