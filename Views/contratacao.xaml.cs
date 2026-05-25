using Hotel.Models;

namespace Hotel.Views;

public partial class ContratacaoHospedagem : ContentPage
{
    App PropriedadesApp;

    public ContratacaoHospedagem()
    {
        InitializeComponent();

        PropriedadesApp = (App)Application.Current;
        pck_quarto.ItemsSource = PropriedadesApp.lista_quartos;

        // Configurações iniciais das datas
        dtpck_checkin.MinimumDate = DateTime.Today;
        dtpck_checkin.MaximumDate = DateTime.Today.AddMonths(1);

        AtualizarLimitesCheckout(DateTime.Today);
    }

    private void AtualizarLimitesCheckout(DateTime dataCheckin)
    {
        dtpck_checkout.MinimumDate = dataCheckin.AddDays(1);
        dtpck_checkout.MaximumDate = dataCheckin.AddMonths(6);
    }

    private void dtpck_checkin_DateSelected(object sender, DateChangedEventArgs e)
    {
        AtualizarLimitesCheckout(e.NewDate);
        ValidarFormulario();
    }

    private void OnComponentes_Changed(object sender, EventArgs e)
    {
        ValidarFormulario();
    }

    // NOVA FUNCIONALIDADE: Habilita o botão dinamicamente apenas se os dados forem válidos
    private void ValidarFormulario()
    {
        int totalHospedes = Convert.ToInt32(stp_adultos.Value) + Convert.ToInt32(stp_criancas.Value);
        bool quartoSelecionado = pck_quarto.SelectedItem != null;
        bool datasValidas = dtpck_checkout.Date > dtpck_checkin.Date;

        btn_avancar.IsEnabled = totalHospedes > 0 && quartoSelecionado && datasValidas;

        // Altera opacidade para feedback visual de desabilitado
        btn_avancar.Opacity = btn_avancar.IsEnabled ? 1.0 : 0.5;
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        try
        {
            // Validação extra de segurança
            if (Convert.ToInt32(stp_adultos.Value) == 0)
            {
                await DisplayAlert("Atenção", "É necessário pelo menos 1 adulto responsável no quarto.", "Voltar");
                return;
            }

            Hospedagem h = new Hospedagem
            {
                QuartoSelecionado = (Quarto)pck_quarto.SelectedItem,
                QntAdultos = Convert.ToInt32(stp_adultos.Value),
                QntCriancas = Convert.ToInt32(stp_criancas.Value),
                DataCheckIn = dtpck_checkin.Date,
                DataCheckOut = dtpck_checkout.Date,
            };

            await Navigation.PushAsync(new HospedagemContratada()
            {
                BindingContext = h
            });
        }
        catch (Exception ex)
        {
            await DisplayAlert("Erro", ex.Message, "OK");
        }
    }

    private async void BtnSobre_Clicked(object sender, EventArgs e)
    {
        // Navega para a nova página "SobreHotel" de forma assíncrona e fluida
        await Navigation.PushAsync(new SobreHotel());
    }

    private async void BtnSobreApp_Clicked(object sender, EventArgs e)
    {
        // Navega para a nova página "SobreApp" de forma assíncrona e fluida
        await Navigation.PushAsync(new Sobre());
    }



}