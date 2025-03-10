namespace MauiAppMinhasCompras.Views;

public partial class ListaProduto : ContentPage
{
	public ListaProduto()
	{
		InitializeComponent();
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

   
}