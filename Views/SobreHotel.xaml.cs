namespace Hotel.Views;

public partial class SobreHotel : ContentPage
{
    public SobreHotel()
    {
        InitializeComponent();
    }

    private async void OnVoltar_Clicked(object sender, EventArgs e)
    {
        // Remove a página atual da pilha e volta para a anterior
        await Navigation.PopAsync();
    }
}