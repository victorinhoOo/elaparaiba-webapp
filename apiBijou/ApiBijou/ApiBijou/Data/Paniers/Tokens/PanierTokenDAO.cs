using ApiBijou.Model.Services;
using MySql.Data.MySqlClient;

namespace ApiBijou.Data.Paniers.Tokens
{
    public class PanierTokenDAO : IPanierTokenDAO
    {

        private string connectionString = "Server=localhost;Port=3306;Database=elaparaiba;Uid=root;Pwd=rootroot;";

        /// <summary>
        /// Créé un token dans la base de données
        /// </summary>
        /// <returns>token créé</returns>
        public string CreerPanierToken()
        {
            string result = string.Empty;
            string token = Token.GenerateToken();
            MySqlConnection conn = OpenConnection();

            try
            {
                var sql = "INSERT INTO tokenPanier (token, dateToken) VALUES (@token, NOW());";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@token", token);
                cmd.ExecuteNonQuery();
                result = token;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                result = ex.Message;
            }

            CloseConnection(conn);
            return result;
        }


        /// <summary>
        /// Récupère l'ID du panier en utilisant le token
        /// </summary>
        /// <param name="token">token du panier</param>
        /// <returns>id du panier</returns>
        public int GetPanierId(string token)
        {
            int result = -1;
            MySqlConnection conn = OpenConnection();

            try
            {
                var sql = "SELECT idToken FROM tokenPanier WHERE token = @token;";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@token", token);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        result = reader.GetInt32("idToken");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            CloseConnection(conn);
            return result;
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
