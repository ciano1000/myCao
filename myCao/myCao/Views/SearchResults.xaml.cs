using myCao.DatabaseService;
using myCao.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace myCao.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchResults : ContentPage
    {
        private DatabaseManager _manager;
        private Search search;
        public SearchResults(Search search, DatabaseManager _manager)
        {
            InitializeComponent();
            this._manager = _manager;
            this.search = search;
        }
        protected override async void OnAppearing()
        {
            var courses = await _manager.GetRecommendedCoursesAsync(search);
            var subjects = await _manager.GetSubjectsAsync();
            courseListView.ItemsSource = courses;
            base.OnAppearing();
            
        }

        private async void Like_Clicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var course = button.CommandParameter as Course;
            if(course.Favourite == 0)
            {
                course.Favourite = 1;
                await _manager.UpdateAsync(course);
                bool added = await _manager.AddFavAsync(course);
            }
            else
            {
                course.Favourite = 0;
                await _manager.UpdateAsync(course);
                 _manager.RemoveFavAsync(course);
            }

        }

        private async void courseListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var course = e.SelectedItem as Course;
            await Navigation.PushAsync(new CourseDetailPage(course));
        }
    }
}