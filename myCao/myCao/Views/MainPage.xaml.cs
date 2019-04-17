using myCao.AppPurchases;
using myCao.DatabaseService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace myCao.Views
{
    public partial class MainPage : ContentPage
    {
        private DatabaseManager _manager;
        private PurchaseService _purchase;
        private bool _displayHelp;

     
    
        public MainPage()
        {
            InitializeComponent();
            _manager = new DatabaseManager();
            _purchase = new PurchaseService();
           
            if(Application.Current.Properties.ContainsKey("FirstStart"))
            {
                _displayHelp = (bool)Application.Current.Properties["FirstStart"];
            }
            else
            {
                _displayHelp = true;
            }
            if(_displayHelp == false)
            {
                help.IsVisible = false;
            }
            if (Application.Current.Properties.ContainsKey("AdsDisabled"))
            {
                if ((bool)Application.Current.Properties["AdsDisabled"] == true)
                {
                    ads.IsVisible = false;
                }
            }  
            
            
        }

        private async void Search_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SearchForm(_manager));
        }

        private async void Fav_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new FavouritesPage(_manager));
            
        }

        private async void Calculator_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CalculatorPage());
        }

        private void Ok_Clicked(object sender, EventArgs e)
        {
            help.IsVisible = false;
        }

        private async void AdsRemove_Clicked(object sender, EventArgs e)
        {
           await _purchase.MakePurchase("88691.my_cao");
     
        }
    }
}
