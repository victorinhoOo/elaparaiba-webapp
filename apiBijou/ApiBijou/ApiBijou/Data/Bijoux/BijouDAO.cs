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
                bijou.DossierPhoto = reader["dossierPhoto"].ToString();
                bijou.Datepublication = reader["dateSortie"].ToString();
                bijou.NbPhotos = Convert.ToInt32(reader["nbPhoto"]);
                bijou.Type = reader["typeBijou"].ToString();
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
                bijou.DossierPhoto = reader["dossierPhoto"].ToString();
                bijou.Datepublication = reader["dateSortie"].ToString();
                bijou.NbPhotos = Convert.ToInt32(reader["nbPhoto"]);
                bijou.Type = reader["typeBijou"].ToString();

                bijoux.Add(bijou); // Ajoute le bijou à la liste
            }

            CloseConnection(conn); // Ferme la connexion à la base de données
            return bijoux; // Retourne la liste de tous les bijoux
        }


        public bool AddBijou(Bijou bijou)
        {
            MySqlConnection conn = OpenConnection();
            bool res;
            try
            {
                string insertSql = "INSERT INTO bijoux (nomBijou, descriptionBijou, stockBijou, prixBijou, dossierPhoto, dateSortie, nbPhoto, typeBijou) VALUES (@nom, @description, @stock, @prix, @dossierPhoto, @dateSortie, @nbPhoto, @type)";
                MySqlCommand cmd = new MySqlCommand(insertSql, conn);
                cmd.Parameters.AddWithValue("@nom", bijou.Name);
                cmd.Parameters.AddWithValue("@description", bijou.Description);
                cmd.Parameters.AddWithValue("@stock", bijou.Quantity);
                cmd.Parameters.AddWithValue("@prix", bijou.Price);
                cmd.Parameters.AddWithValue("@dossierPhoto", bijou.DossierPhoto);
                cmd.Parameters.AddWithValue("@dateSortie", bijou.Datepublication);
                cmd.Parameters.AddWithValue("@nbPhoto", bijou.NbPhotos);
                cmd.Parameters.AddWithValue("@type", bijou.Type);

                cmd.ExecuteNonQuery(); // Exécute la requête SQL
                res = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                res = false; 
            }

            CloseConnection(conn); // Ferme la connexion à la base de données
            return res;
        }


        public bool DeleteBijouById(int id)
        {
            MySqlConnection conn = OpenConnection();
            bool res;
            try
            {
                string deleteSql = "DELETE FROM bijoux WHERE idBijou = @id";
                MySqlCommand cmd = new MySqlCommand(deleteSql, conn);
                cmd.Parameters.AddWithValue("@id", id);

                cmd.ExecuteNonQuery(); // Exécute la requête SQL
                res = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                res = false;
            }

            CloseConnection(conn); // Ferme la connexion à la base de données
            return res;
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

        public bool DecreaseStock(int id, int quantity)
        {
            MySqlConnection conn = OpenConnection(); // Ouvre une connexion à la base de données

            bool result = false;

            try
            {
                // Vérifie d'abord si le stock est suffisant
                string checkStockSql = "SELECT stockBijou FROM bijoux WHERE idBijou = @id AND stockBijou >= @quantity";
                MySqlCommand checkStockCmd = new MySqlCommand(checkStockSql, conn);
                checkStockCmd.Parameters.AddWithValue("@id", id);
                checkStockCmd.Parameters.AddWithValue("@quantity", quantity);

                object resultstock = checkStockCmd.ExecuteScalar();
                if (resultstock != null) // Diminue le stock si le stock est suffisant
                {
                    
                    string updateStockSql = "UPDATE bijoux SET stockBijou = stockBijou - @quantity WHERE idBijou = @id";
                    MySqlCommand updateStockCmd = new MySqlCommand(updateStockSql, conn);
                    updateStockCmd.Parameters.AddWithValue("@id", id);
                    updateStockCmd.Parameters.AddWithValue("@quantity", quantity);

                    updateStockCmd.ExecuteNonQuery(); // Exécute la requête SQL
                    result = true; 
                }

            }
            catch (Exception ex)
            {
                result= false;
                Console.WriteLine(ex.Message);
            }
            CloseConnection(conn); // Ferme la connexion à la base de données
            return result;
        }


        public bool ModifierBijou(int idBijou, Bijou bijou)
        {
            MySqlConnection conn = OpenConnection(); // Ouvre une connexion à la base de données

            bool result = false;

            try
            {
                string updateSql = "UPDATE bijoux SET nomBijou = @nom, descriptionBijou = @description, stockBijou = @stock, prixBijou = @prix, dossierPhoto = @dossierPhoto, dateSortie = @dateSortie, nbPhoto = @nbPhoto, typeBijou = @type WHERE idBijou = @id";
                MySqlCommand cmd = new MySqlCommand(updateSql, conn);
                cmd.Parameters.AddWithValue("@id", idBijou);
                cmd.Parameters.AddWithValue("@nom", bijou.Name);
                cmd.Parameters.AddWithValue("@description", bijou.Description);
                cmd.Parameters.AddWithValue("@stock", bijou.Quantity);
                cmd.Parameters.AddWithValue("@prix", bijou.Price);
                cmd.Parameters.AddWithValue("@dossierPhoto", bijou.DossierPhoto);
                cmd.Parameters.AddWithValue("@dateSortie", DateTime.Parse(bijou.Datepublication).ToString("yyyy-MM-dd HH:mm:ss")); // Formatage de la date
                cmd.Parameters.AddWithValue("@nbPhoto", bijou.NbPhotos);
                cmd.Parameters.AddWithValue("@type", bijou.Type);

                cmd.ExecuteNonQuery(); // Exécute la requête SQL

                result = true; // Retourne vrai si au moins une ligne a été mise à jour
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                result = false;
            }

            CloseConnection(conn); // Ferme la connexion à la base de données
            return result;
        }


    }
}
