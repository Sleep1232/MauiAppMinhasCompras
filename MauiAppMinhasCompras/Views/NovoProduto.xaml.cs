using MauiAppMinhasCompras.Models;
using System.Threading.Tasks;

namespace MauiAppMinhasCompras.Views;

public partial class NovoProduto : ContentPage
{
	public NovoProduto()
	{
		InitializeComponent();
	}

    private async void ToolbarItem_Clicked(object sender, EventArgs e)
    {
		try
		{
			Produto p = new Produto //preenche a model produto com os dados digitados pelo usuario
			{
				Descricao = txt_descricao.Text,
				Quantidade = Convert.ToDouble(txt_quantidade.Text), //converte o dado de texto para número
				Preco = Convert.ToDouble(txt_peco.Text)
			};

			await App.Db.Insert(p);
			await DisplayAlert("Sucesso", "Registro Inserido", "OK");
		}
		catch(Exception ex)
		{
			await DisplayAlert("Erro!!!", ex.Message, "OK");
		}

    }
}