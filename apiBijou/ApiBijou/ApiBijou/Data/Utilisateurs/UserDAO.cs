using MySql.Data.MySqlClient;

namespace ApiBijou.Data.Utilisateurs
{
    public class UserDAO : IUserDAO
    {


        /// <summary>
        /// String de connexion au serveur
        /// </summary>
        private string connectionString = "Server=db5014804958.hosting-data.io ;Port=3306;Database=elaparaiba;Uid=dbu5413477 ;Pwd=Iutbourgogne.dvpm21;";

        public bool CheckLoginPwd(string login, string pwd)
        {
            bool res = false;
            using (var connection = OpenConnection())
            {
                var command = connection.CreateCommand();
                command.CommandText = "SELECT COUNT(*) FROM Administrateur WHERE login = @login AND mdpHash = @mdp";
                command.Parameters.AddWithValue("@login", login);
                command.Parameters.AddWithValue("@mdp", pwd);

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        var count = reader.GetInt32(0);
                        res = count > 0;
                    }
                }
            }
            return res;
        }

        public bool GiveAdmin(string tokenPanier)
        {
            using (var connection = OpenConnection())
            {
                var command = connection.CreateCommand();
                command.CommandText = "UPDATE Administrateur SET tokenPanier = @tokenPanier WHERE idAdministrateur = 1";
                command.Parameters.AddWithValue("@tokenPanier", tokenPanier);

                int rowsAffected = command.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }


        public bool IsAdmin(string tokenPanier)
        {
            using (var connection = OpenConnection())
            {
                var command = connection.CreateCommand();
                command.CommandText = "SELECT COUNT(*) FROM Administrateur WHERE tokenPanier = @tokenPanier";
                command.Parameters.AddWithValue("@tokenPanier", tokenPanier);

                var result = Convert.ToInt32(command.ExecuteScalar());
                return result > 0;
            }
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
