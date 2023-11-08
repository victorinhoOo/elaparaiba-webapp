using API_SAE.Model;
using ApiBijou.Data.Bijoux;
using ApiBijou.Model;
using ApiBijou.Model.Panier;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

public class PanierTests
{
    /// <summary>
    /// Créé un panier, ajoute des bijoux et vérifie qu'ils ont le bon id dans le panier
    /// </summary>
    [Fact]
    public void AjouterPanierTest()
    {
        Panier panier = new Panier();
        panier.AddBijoux(BijouFakeDAO.Instance.getById(1));
        panier.AddBijoux(BijouFakeDAO.Instance.getById(5));
        panier.AddBijoux(BijouFakeDAO.Instance.getById(6));
        List<PanierItem> listBijou = panier.GetBijoux();
        Assert.Equal(BijouFakeDAO.Instance.getById(1), listBijou[0].Bijou);
        Assert.Equal(BijouFakeDAO.Instance.getById(5), listBijou[1].Bijou);

    }

    /// <summary>
    /// Créé un panier, ajoute des bijoux et vérifie qu'ils ont bien été ajoutés au panier
    /// </summary>
    [Fact]
    public void ContientBijouTest()
    {
        Panier panier = new Panier();
        panier.AddBijoux(BijouFakeDAO.Instance.getById(1));
        panier.AddBijoux(BijouFakeDAO.Instance.getById(5));
        panier.AddBijoux(BijouFakeDAO.Instance.getById(6));
        Assert.Equal(panier.ContientBijou(BijouFakeDAO.Instance.getById(1)), 0);
        Assert.Equal(panier.ContientBijou(BijouFakeDAO.Instance.getById(5)), 1);
        Assert.Equal(panier.ContientBijou(BijouFakeDAO.Instance.getById(6)), 2);
    }


    /// <summary>
    /// Créé un panier ajoute des bijoux et vérifie que la quantité de bijoux dans le panier correspond
    /// </summary>
    [Fact]
    public void quantiteBijouTest()
    {
        Panier panier = new Panier();
        panier.AddBijoux(BijouFakeDAO.Instance.getById(1));
        panier.AddBijoux(BijouFakeDAO.Instance.getById(1));
        List<PanierItem> listBijou = panier.GetBijoux();
        Assert.Equal(listBijou[0].Quantite, 2);
    }
}
