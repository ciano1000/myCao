using myCao.DatabaseService;
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
	public partial class SubjectPicker : ContentPage
	{
        public ListView Subjects { get { return subjectsView; } }

        private DatabaseManager _manager;
		public SubjectPicker (DatabaseManager _manager)
		{
			InitializeComponent ();
            this._manager = _manager;
		}
        protected override async void OnAppearing()
        {
            var subjects = await _manager.GetSubjectsAsync();
            subjectsView.ItemsSource = subjects;

            base.OnAppearing();
        }
	}
}