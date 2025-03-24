using MauiAppMinhasCompras.Models;
using System.Collections.ObjectModel;

namespace MauiAppMinhasCompras.Views;

public partial class ListaProduto : ContentPage
{
	ObservableCollection<Produto> lista = new ObservableCollection<Produto>();
	// traz maior automação, as alterações refletem na listview e vice versa
	public ListaProduto()
	{
		InitializeComponent();

		lst_produtos.ItemsSource = lista;
	}

    protected async override void OnAppearing() //sempre é chamado quando uma tela aparece, mecanismo paralelo ao 
											// construtor, não interfere no mesmo, evita de ficar preso 
											// na leitura dos dados do sqlite
    {
		//lista = await App.Db.GetAll(); - forma que não seria possível - erro na conversão de dados
		List<Produto> tmp = await App.Db.GetAll();

		tmp.ForEach(i => lista.Add(i)); //necessário, pois a chamada direta geraria erro - lista genérica para 
										//algo complexo
    }
    //evento iniciado no botão, sua ação lógica
    private void ToolbarItem_Clicked(object sender, EventArgs e)
    {
		try /* tenta executar a mudança de página
		     * caso falhe é gerada uma exceção com a 
		     mensagem de erro*/
		{
			Navigation.PushAsync(new Views.NovoProduto()); // funcionando conduz à página
		}
		catch (Exception ex)
		{
			DisplayAlert("Falhou!!", ex.Message, "OK"); // mensagem em caso de falha
		}

    }

    private async void txt_search_TextChanged(object sender, TextChangedEventArgs e) //evento gerado ao digitar o texto na barra de busca
    {
		try
		{
			string q = e.NewTextValue;

			lista.Clear(); //limpa a lista, evitando o acúmulo dos dados

			List<Produto> tmp = await App.Db.Search(q);

			tmp.ForEach(i => lista.Add(i));
		}
		catch(Exception ex)
		{
            await DisplayAlert("ops", ex.Message, "Ok");
        }
    }

    private void ToolbarItem_Clicked_1(object sender, EventArgs e)//evento soma
    {
		try
		{
			double soma = lista.Sum(i => i.Total);//método para somar exige uma função ou expressão lambda
												  //expressão lambda (i => ......)
			string msg = $"O total é {soma:C}";

			DisplayAlert("Total dos Produtos", msg, "OK");
		}
		catch(Exception ex)
		{
            DisplayAlert("ops", ex.Message, "Ok");
        }
    }

    private async void MenuItem_Clicked(object sender, EventArgs e)
    {
		try 
		{
			MenuItem selecionado = sender as MenuItem;

			Produto p = selecionado.BindingContext as Produto;

			bool confirmacao = await DisplayAlert("Deseja remover o produto?","Remover Produto?","Sim", "Não");

			if(confirmacao)
			{
				await App.Db.Delete(p.Id);
				lista.Remove(p);

			}


		}
		catch (Exception ex)
		{
			await DisplayAlert("ops", ex.Message, "Ok");
		}

    }

    private void lst_produtos_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
		try
		{
			Produto p = e.SelectedItem as Produto;

			Navigation.PushAsync(new Views.EditarProduto
			{
				BindingContext = p,
			});
		}
		catch (Exception ex)
		{
			DisplayAlert("Ops", ex.Message, "OK");
		}
    }
}