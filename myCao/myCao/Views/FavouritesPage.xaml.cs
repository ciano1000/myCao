using myCao.DatabaseService;
using myCao.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace myCao.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class FavouritesPage : ContentPage
	{
        private DatabaseManager _manager;
        private ObservableCollection<Course> _courses;

		public FavouritesPage (DatabaseManager _manager)
		{
			InitializeComponent ();
            this._manager = _manager;
            if (Application.Current.Properties.ContainsKey("AdsDisabled"))
            {
                if ((bool)Application.Current.Properties["AdsDisabled"] == true)
                {
                    ads.IsVisible = false;
                }
            }
        }
        protected override async void OnAppearing()
        {
            var courses = await _manager.GetFavAsync();
            _courses = new ObservableCollection<Course>(courses);
            if(_courses.Count ==0)
            {
                courseListView.IsVisible = false;
                help.IsVisible = true;
            }
            courseListView.ItemsSource = _courses;

            base.OnAppearing();
        }
        private async void Delete_Clicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var course = button.CommandParameter as Course;
            course.Favourite = 0;
            await _manager.UpdateAsync(course);
            Favourite fav = new Favourite();
            fav.CourseID = course.CourseID;
            fav.CourseName = course.CourseTitle;
            _manager.RemoveFavAsync(fav);
            _courses.Remove(course);
        }

        private async void courseListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var course = e.SelectedItem as Course;
            await Navigation.PushAsync(new CourseDetailPage(course));
        }
    }
}