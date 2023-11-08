using ApiBijou.Model.Services;
using MySql.Data.MySqlClient;

namespace ApiBijou.Data
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
            string token = Token.GenerateToken();
            using (var conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                var sql = "INSERT INTO panier_tokens (token, date) VALUES (@token, NOW());";
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@token", token);
                    cmd.ExecuteNonQuery();
                    // Récupère l'ID du nouveau token inséré
                    var id = cmd.LastInsertedId;
                    return token;
                }
            }
        }

        /// <summary>
        /// Récupère l'ID du panier en utilisant le token
        /// </summary>
        /// <param name="token">token du panier</param>
        /// <returns>id du panier</returns>
        public int GetPanierId(string token)
        {
            using (var conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                var sql = "SELECT id FROM panier_tokens WHERE token = @token;";
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@token", token);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return reader.GetInt32("id");
                        }
                        else
                        {
                            return -1; // Le token n'existe pas
                        }
                    }
                }
            }
        }
    }
}
