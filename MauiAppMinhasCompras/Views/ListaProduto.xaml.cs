using MauiAppMinhasCompras.Models;
using System.Collections.ObjectModel;

namespace MauiAppMinhasCompras.Views;

public partial class ListaProduto : ContentPage
{
	ObservableCollection<Produto> lista = new ObservableCollection<Produto>();
	// traz maior automa��o, as altera��es refletem na listview e vice versa
	public ListaProduto()
	{
		InitializeComponent();

		lst_produtos.ItemsSource = lista;
	}

    protected async override void OnAppearing() //sempre � chamado quando uma tela aparece, mecanismo paralelo ao 
											// construtor, n�o interfere no mesmo, evita de ficar preso 
											// na leitura dos dados do sqlite
    {
		//lista = await App.Db.GetAll(); - forma que n�o seria poss�vel - erro na convers�o de dados
		List<Produto> tmp = await App.Db.GetAll();

		tmp.ForEach(i => lista.Add(i)); //necess�rio, pois a chamada direta geraria erro - lista gen�rica para 
										//algo complexo
    }
    //evento iniciado no bot�o, sua a��o l�gica
    private void ToolbarItem_Clicked(object sender, EventArgs e)
    {
		try /* tenta executar a mudan�a de p�gina
		     * caso falhe � gerada uma exce��o com a 
		     mensagem de erro*/
		{
			Navigation.PushAsync(new Views.NovoProduto()); // funcionando conduz � p�gina
		}
		catch (Exception ex)
		{
			DisplayAlert("Falhou!!", ex.Message, "OK"); // mensagem em caso de falha
		}

    }

    private async void txt_search_TextChanged(object sender, TextChangedEventArgs e) //evento gerado ao digitar o texto na barra de busca
    {
		string q = e.NewTextValue;

		lista.Clear(); //limpa a lista, evitando o ac�mulo dos dados

        List<Produto> tmp = await App.Db.Search(q);

        tmp.ForEach(i => lista.Add(i));
    }

    private void ToolbarItem_Clicked_1(object sender, EventArgs e)//evento soma
    {
		double soma = lista.Sum(i => i.Total);//m�todo para somar exige uma fun��o ou express�o lambda
									//express�o lambda (i => ......)
		string msg = $"O total � {soma:C}";

		DisplayAlert("Total dos Produtos", msg, "OK");
    }

    private void MenuItem_Clicked(object sender, EventArgs e)
    {

    }
}