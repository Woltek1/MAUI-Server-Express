using System.Net.Http;
using System.Net.Http.Json;
using System.Collections.ObjectModel;
using System.Text.Json;

namespace ProjektExpress
{
    public partial class MainPage : ContentPage
    {
        class Matrix
        {
            public double X { get; set; }
            public double Y { get; set; }
        }

        ObservableCollection<Matrix> matrix = new ObservableCollection<Matrix>();

        public MainPage()
        {
            InitializeComponent();
            MatrixXY.ItemsSource = matrix;
        }

        private void DodajWartosc(object sender, EventArgs args)
        {
            double wartoscX = Convert.ToDouble(PodajX.Text);    
            double wartoscY = Convert.ToDouble(PodajY.Text);

            matrix.Add(new Matrix { X = wartoscX, Y = wartoscY });
        }

        private async void WyslijDane(object sender, EventArgs e)
        {
            try
            {
                var httpClient = new HttpClient();
                var json = JsonSerializer.Serialize(matrix);
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                var response = await httpClient.PostAsync("http://localhost:3000/dane", content);
                // ↑ podmień IP na adres serwera Express (może być localhost jeśli testujesz przez emulator)

                if (response.IsSuccessStatusCode)
                {
                    await DisplayAlert("Sukces", "Dane wysłane pomyślnie!", "OK");
                }
                else
                {
                    await DisplayAlert("Błąd", "Nie udało się wysłać danych", "OK");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Błąd", ex.Message, "OK");
            }
        }
    }
}
