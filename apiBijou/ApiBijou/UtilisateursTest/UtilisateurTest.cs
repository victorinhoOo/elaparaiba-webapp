using ApiBijou.Model.Utilisateurs;

namespace UtilisateursTest
{
    public class UtilisateurTest
    {
        //Test de la fonction hashPassword de l'utilisateur Manager
        [Fact]
        public void hashPasswordTest()
        {
            UtilisateursManager utilisateursManager = new UtilisateursManager();
            string pwd = "chat256";
            Assert.Equal(utilisateursManager.HashPassword(pwd), "0da651f3a757364a4a6ce8730990afa46fe8d62e95f26172c47ca2fde814c6f7");
        }

        //Test de la fonction CheckLoginPwd du fake DAO
        [Fact]
        public void CheckLoginPwdTest()
        {
            UtilisateursManager utilisateursManager = new UtilisateursManager();
            string pwd = "chat256";
            Assert.True(utilisateursManager.CheckLoginPwd("root", pwd));
            pwd = "false";
            Assert.False(utilisateursManager.CheckLoginPwd("root", pwd));
        }

        //Test les fonctions IsAdmin et ConnectAsAdmin
        [Fact]
        public void addAdmin()
        {
            UtilisateursManager utilisateursManager = new UtilisateursManager();
            //Bon mdp
            Assert.True(utilisateursManager.ConnectAsAdmin("123456", "root", "chat256"));
            //Muavais mdp
            Assert.False(utilisateursManager.ConnectAsAdmin("123458", "root", "chat257"));
            //On vérifie que l'admin avec le bon mdp a bien été ajouté 
            Assert.True(utilisateursManager.IsAdmin("123456"));
            //On vérifie que l'utilisateur qui s'est connécté avec un faux mdp n'a pas été ajouté
            Assert.False(utilisateursManager.IsAdmin("1234567"));

        }
    }
}