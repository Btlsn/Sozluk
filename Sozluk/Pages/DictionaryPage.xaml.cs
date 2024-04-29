using Sozluk.Database;
using Sozluk.Models;

namespace Sozluk.Pages;

public partial class DictionaryPage : ContentPage
{
	private readonly Database.LocalDatabaseService _localDatabaseService = new Database.LocalDatabaseService();
	
    public DictionaryPage()
    {
        InitializeComponent();
		
    }

	protected override async void OnAppearing()
	{
		base.OnAppearing();
		var words = await _localDatabaseService.GetDictionary();
		WordsListView.ItemsSource = words;
	}

    private async void AddWordBtnClicked(object sender, EventArgs e)
	{
        await App.Current.MainPage.Navigation.PushModalAsync(new WordAddingPage());
    }

	private async void WordsListView_ItemTapped(object sender, ItemTappedEventArgs e)
	{
        var item = (Dictionary)e.Item;
        var action = await DisplayActionSheet("Delete", "Cancel", null, "Delete");

        switch (action)
        {
            case "Delete":
                await _localDatabaseService.Delete(item);
                WordsListView.ItemsSource = await _localDatabaseService.GetDictionary();
                break;
        }
    }
}