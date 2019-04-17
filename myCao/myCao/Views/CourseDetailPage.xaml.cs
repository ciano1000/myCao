using myCao.Ads;
using myCao.Models;
using myCao.RESTHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace myCao.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CourseDetailPage : ContentPage
    {
        private Course _course;
        private WebSearchService _service;
        private bool showInter;
        public CourseDetailPage(Course course)
        {
            InitializeComponent();           
            _service = new WebSearchService();
            _course = course;
            showInter = true;
            DependencyService.Get<IAdInterstitial>().ShowAd();
            if (Application.Current.Properties.ContainsKey("AdsDisabled"))
            {
                if ((bool)Application.Current.Properties["AdsDisabled"] == true)
                {
                    ads.IsVisible = false;
                }
            }

        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            BindingContext = _course;
            
        }

        private async void Online_Clicked(object sender, EventArgs e)
        {
            Random rand = new Random();
            bool showAd =Convert.ToBoolean(rand.Next(2));

            if (Application.Current.Properties.ContainsKey("AdsDisabled"))
            {
                if ((bool)Application.Current.Properties["AdsDisabled"] == true)
                {
                    showInter = false;
                }
            }

            if (showAd && showInter)
            {
                DependencyService.Get<IAdInterstitial>().Give();
                await Task.Delay(3000);
            }
            
            try
             {
                 var website = await _service.CourseLink(_course);

                 Device.OpenUri(new Uri(website.link));
             }
             catch (HttpRequestException)
             {
                 await DisplayAlert("Network Error", "Unable to connect to Google", "Ok");
                 
                 return;
             }   
            

        }
    }
}