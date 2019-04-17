using myCao.Models;
using myCao.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace myCao
{
    public partial class App : Application
    {
        public static int ScreenWidth;
      
        

        public App()
        {
            InitializeComponent();

            var navPage = new NavigationPage(new MainPage());

            MainPage = navPage;
        }

        protected override void OnStart()
        {
            
            if (!Application.Current.Properties.ContainsKey("AdsDisabled"))
            {
                Application.Current.Properties["AdsDisabled"] = false;
            }
            // Handle when your app starts
            if (Application.Current.Properties.ContainsKey("FirstStart"))
            {
                Application.Current.Properties["FirstStart"] = false;
            }
            else
            {
                Application.Current.Properties["FirstStart"] = true;
                Application.Current.Properties["AdsDisabled"] = false; 
            }

       
            Application.Current.SavePropertiesAsync();
            
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
