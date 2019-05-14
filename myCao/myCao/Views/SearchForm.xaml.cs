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
	public partial class SearchForm : ContentPage
	{
        private Search _search;
        private int subjectCount;
        private Subject subject1;
        private Subject subject2;
        private Subject subject3;
        private bool isMinSet;
        private bool isMaxSet;
        private bool subjectsEntered;
        private bool _displayHelp;
        private bool numNAN;
        private DatabaseManager _manager;

		public SearchForm (DatabaseManager _manager)
		{
			InitializeComponent ();           
            _search = new Search();
            subject1 = new Subject();
            subject2 = new Subject();
            
            subject3 = new Subject();
            isMaxSet = false;
            numNAN = false;
            isMinSet = false;
            subjectsEntered = false;
            subjectCount = 0;
            if ((bool)Application.Current.Properties["FirstStart"])
            {
                help.IsVisible = false;
            }
            this._manager = _manager;

        }

        private  void Subject1_Tapped(object sender, EventArgs e)
        {
            var page = new SubjectPicker(_manager);
            page.Subjects.ItemSelected += (source, args) =>
            {
                subject1 = args.SelectedItem as Subject;
                subject_1.Text = subject1.Name;
                subjectCount++;
                Navigation.PopAsync();
            };
              Navigation.PushAsync(page);
            
        }

        private  void Subject2_Tapped(object sender, EventArgs e)
        {
            var page = new SubjectPicker(_manager);
            page.Subjects.ItemSelected += (source, args) =>
            {
                subject2 = args.SelectedItem as Subject;
                subject_2.Text = subject2.Name;
                subjectCount++;
                Navigation.PopAsync();
            };
             Navigation.PushAsync(page);
           
        }

        private  void Subject3_Tapped(object sender, EventArgs e)
        {
            var page = new SubjectPicker(_manager);
            page.Subjects.ItemSelected += (source, args) =>
            {
                 subject3 = args.SelectedItem as Subject;
                subject_3.Text = subject3.Name;
                subjectCount++;
                Navigation.PopAsync();
            };
             Navigation.PushAsync(page);
            
        }

        private  void Min_Completed()
        {
            if (!String.IsNullOrWhiteSpace(minEntry.Text))
            {
                var value = System.Convert.ToInt32(minEntry.Text);
                if (value > 999 || value < 0)
                {
                    DisplayAlert("Alert", "Enter a number in the range 0-999", "Ok");
                    minEntry.Text = null;
                }
                else if (value > 0)
                {
                    isMinSet = true;
                }
                else
                {
                    isMinSet = false;
                    numNAN = true;
                }
            }
                
        }
        private void Max_Completed()
        {
            if (!String.IsNullOrWhiteSpace(maxEntry.Text))
            {
                var value = System.Convert.ToInt32(maxEntry.Text);
                if (value > 999 || value < 0)
                {
                    DisplayAlert("Alert", "Enter a number in the range 0-999", "Ok");
                    maxEntry.Text = null;
                }
                else if (value > 0)
                {
                    isMaxSet = true;
                }
                else
                {
                    isMaxSet = false;
                    numNAN = true;
                }
            }
            
        }

        private async void Search_Clicked(object sender, EventArgs e)
        {
            if(!String.IsNullOrWhiteSpace(searchText.Text))
            {
                _search.TextSearch = searchText.Text;
            }
              
            if(subject1.Name == null || subject2.Name == null||subject3.Name == null)
            {
                subjectsEntered = false;
            }
            else
            {
                subjectsEntered = true;
            }

            if(subjectCount>=3 ||( subjectCount>=0 &&subjectsEntered))
            {
                _search.Subject1 = subject1;
                _search.Subject2 = subject2;
                _search.Subject3 = subject3;
                subjectsEntered = true;
            }
            else if (subjectCount >=0 && (subject1.Name == null || subject2.Name == null || subject3.Name == null) )
            {
                if(subjectCount>0)
                {
                    await DisplayAlert("Alert", "If selecting favourite subjects, you must select all 3", "Ok");
                }
                subjectsEntered = false;
               
            }
            else if(subjectCount == 0)
            {
                _search.Subject1 = null;
                _search.Subject2 = null;
                _search.Subject3 = null;
                subjectsEntered = false;
            }
            
            if((!String.IsNullOrWhiteSpace(minEntry.Text) && !String.IsNullOrWhiteSpace(maxEntry.Text)))
            {
                Max_Completed();
                Min_Completed();

                if (isMaxSet && isMinSet)
                {
                    _search.MinPoints = System.Convert.ToInt32(minEntry.Text);
                    _search.MaxPoints = System.Convert.ToInt32(maxEntry.Text);
                }
               
            }
            else if(!isMinSet&&isMaxSet)
            {
                await DisplayAlert("Alert", "Must set both a min and a max points", "Ok");
                return;
            }
            else if (isMinSet && !isMaxSet)
            {
                await DisplayAlert("Alert", "Must set both a min and a max points", "Ok");
                return;
            }

            if((!String.IsNullOrWhiteSpace(searchText.Text)) || (isMaxSet&&isMinSet)||subjectsEntered == true)
            {
                
                await Navigation.PushAsync(new SearchResults(_search,_manager));
                _search = new Search();
                subjectsEntered = false;
                subjectCount = 0;
            }
            
        }
        private void Ok_Clicked(object sender, EventArgs e)
        {
            help.IsVisible = false;
            _displayHelp = false;
        }
    }
}