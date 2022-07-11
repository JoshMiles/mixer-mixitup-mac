namespace MixItUp.Mac;
using Microsoft.Maui;
public partial class LoginWindow : ContentPage
{
	public LoginWindow()
	{
		InitializeComponent();
	}

	private void StreamerLoginButton_Clicked(object sender, EventArgs e)
	{
		DisplayAlert("Alert", "Streamer login button clicked", "ok");
	}

    private void NewStreamerLoginButton_Clicked(object sender, EventArgs e)
    {
        DisplayAlert("Alert", "New Streamer login button clicked", "ok");
    }
}

