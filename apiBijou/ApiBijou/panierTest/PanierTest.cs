using API_SAE.Data;
using API_SAE.Model;
using ApiBijou.Model;
using ApiBijou.Model.Panier;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

public class PanierTests
{
    [Fact]
    public void AjouterPanierTest()
    {
        PanierBijoux panier = new PanierBijoux();
        panier.AddBijoux(BijouFakeDAO.Instance.getById(1));
        panier.AddBijoux(BijouFakeDAO.Instance.getById(5));
        panier.AddBijoux(BijouFakeDAO.Instance.getById(6));
        List<PanierItem> listBijou = panier.GetBijoux();
        Assert.Equal(BijouFakeDAO.Instance.getById(1), listBijou[0].Bijou);
        Assert.Equal(BijouFakeDAO.Instance.getById(5), listBijou[1].Bijou);

    }

    [Fact]
    public void ContientBijouTest()
    {
        PanierBijoux panier = new PanierBijoux();
        panier.AddBijoux(BijouFakeDAO.Instance.getById(1));
        panier.AddBijoux(BijouFakeDAO.Instance.getById(5));
        panier.AddBijoux(BijouFakeDAO.Instance.getById(6));
        Assert.Equal(panier.ContientBijou(BijouFakeDAO.Instance.getById(1)), 0);
        Assert.Equal(panier.ContientBijou(BijouFakeDAO.Instance.getById(5)), 1);
        Assert.Equal(panier.ContientBijou(BijouFakeDAO.Instance.getById(6)), 2);
    }

    [Fact]
    public void quantiteBijouTest()
    {
        PanierBijoux panier = new PanierBijoux();
        panier.AddBijoux(BijouFakeDAO.Instance.getById(1));
        panier.AddBijoux(BijouFakeDAO.Instance.getById(1));
        List<PanierItem> listBijou = panier.GetBijoux();
        Assert.Equal(listBijou[0].Quantite, 2);
    }
}
