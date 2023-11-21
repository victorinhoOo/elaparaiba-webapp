using ApiBijou.Model.Utilisateurs;

namespace UtilisateursTest
{
    public class UtilisateurTest
    {
        [Fact]
        public void hashPasswordTest()
        {
            UtilisateursManager utilisateursManager = new UtilisateursManager();
            string pwd = "chat256";
            Assert.Equal(utilisateursManager.HashPassword(pwd), "0da651f3a757364a4a6ce8730990afa46fe8d62e95f26172c47ca2fde814c6f7");
        }

        [Fact]
        public void CheckLoginPwdTest()
        {
            UtilisateursManager utilisateursManager = new UtilisateursManager();
            string pwd = "chat256";
            Assert.True(utilisateursManager.CheckLoginPwd("root", pwd));
            pwd = "false";
            Assert.False(utilisateursManager.CheckLoginPwd("root", pwd));
        }
    }
}