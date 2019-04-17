using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Flex.Controls;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace myCao.Views
{
    public struct SubjectPoints
    {
        public int _points;
    }

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CalculatorPage : ContentPage
    {
        private bool isLCVP;
        private bool isHigher;
        private bool isOrdinary;
        private bool higherMaths;

        private int _points;
        private int subjectCount;

        private enum HigherGrades {H1 =100,H2 =88,H3 = 77,H4=66,H5=56,H6=46,H7=37,H8=0 };
        private enum OrdinaryGrades { O1 = 56, O2 = 46, O3 = 37, O4 = 28, O5 = 20, O6 = 12, O7 = 0, O8 = 0 };
        private enum LCVPGrades {Distinction = 66,Merit = 46, Pass =  28};

        private List<SubjectPoints> subjectPoints;

        public CalculatorPage()
        {
            InitializeComponent();
            isLCVP = false;
            isOrdinary = false;
            isHigher = true;
            higherMaths = false;
            _points = 0;
            subjectCount= 0;
             subjectPoints= new List<SubjectPoints>();
            if (Application.Current.Properties.ContainsKey("AdsDisabled"))
            {
                if ((bool)Application.Current.Properties["AdsDisabled"] == true)
                {
                    ads.IsVisible = false;
                }
            }
               

        }

        private void Reset_Clicked(object sender, EventArgs e)
        {
            isLCVP = false;
            isOrdinary = false;
            isHigher = true;
            higherMaths = false;
            _points = 0;
            subjectCount = 0;
            subjectPoints = new List<SubjectPoints>();
            picker.IsVisible = false;
            gradePicker.IsVisible = false;
            mathsPicker.IsVisible = true;
            points.Text = "0 points";
        }

        private void Grade_Clicked(object sender, EventArgs e)
        {
            if(subjectCount <=10)
            {
                var button = sender as FlexButton;
                if (button.Equals(One))
                    UpdatePoints(1);
                if (button.Equals(Two))
                    UpdatePoints(2);
                if (button.Equals(Three))
                    UpdatePoints(3);
                if (button.Equals(Four))
                    UpdatePoints(4);
                if (button.Equals(Five))
                    UpdatePoints(5);
                if (button.Equals(Six))
                    UpdatePoints(6);
                if (button.Equals(Seven))
                    UpdatePoints(7);
                if (button.Equals(Eight))
                    UpdatePoints(8);
                if (subjectCount == 0)
                {
                    picker.IsVisible = true;
                }
            }

            if(subjectCount>10)
            {
                DisplayAlert("Max Subjects Entered", "Please reset the calculator to start again", "Ok");
            }

            subjectCount++;
        }

        private void MathsYes_Clicked(object sender, EventArgs e)
        {
            higherMaths = true;
            mathsPicker.IsVisible = false;
            gradePicker.IsVisible = true;
        }
        private void MathsNo_Clicked(object sender, EventArgs e)
        {
            higherMaths = false;
            mathsPicker.IsVisible = false;
            ChangeGradePicker("O");
            gradePicker.IsVisible = true;
        }

        private void Ordinary_Clicked(object sender, EventArgs e)
        {
            isLCVP = false;
            isOrdinary = true;
            isHigher = false;
            ChangeGradePicker("O");
        }
        private void Higher_Clicked(object sender, EventArgs e)
        {
            isLCVP = false;
            isOrdinary = false;
            isHigher = true;
            ChangeGradePicker("H");
        }
        private void LCVP_Clicked(object sender, EventArgs e)
        {
            isLCVP = true;
            isOrdinary = false;
            isHigher = false;
            ChangeGradePicker(null, true);
        }

        private void ChangeGradePicker(string level = null, bool isLCVP = false)
        {
            var children = gradePicker.Children;
            List<FlexButton> buttons = new List<FlexButton>();
            foreach (View view in children)
            {
                buttons.Add(view as FlexButton);
            }
            if (!isLCVP)
            {
                for (int j = (buttons.Count - 1); j > 2; j--)
                {
                    buttons[j].IsVisible = true;
                }

                int i = 0;
                foreach (FlexButton button in buttons)
                {
                    button.Text = level + (i + 1);
                    i++;
                }
            }
            else
            {
                for(int i = (buttons.Count-1);i>2;i--)
                {
                    buttons[i].IsVisible = false;
                }
                buttons[0].Text = "Distinction";
                buttons[1].Text = "Merit";
                buttons[2].Text = "Pass";
            }
            
            
        }

        private void UpdatePoints(int grade)
        {
            SubjectPoints subjectPoint = new SubjectPoints();
            if (subjectCount == 0)
            {
                if(higherMaths)
                {
                    switch (grade)
                    {
                        case 1:
                            subjectPoint._points = 25 + (int)HigherGrades.H1;
                            break;
                        case 2:
                            subjectPoint._points = 25 + (int)HigherGrades.H2;
                            break;
                        case 3:
                            subjectPoint._points = 25 + (int)HigherGrades.H3;
                            break;
                        case 4:
                            subjectPoint._points = 25 + (int)HigherGrades.H4;
                            break;
                        case 5:
                            subjectPoint._points = 25 + (int)HigherGrades.H5;
                            break;
                        case 6:
                            subjectPoint._points = 25 + (int)HigherGrades.H6;
                            break;
                        case 7:
                            subjectPoint._points = (int)HigherGrades.H7;
                            break;
                        case 8:
                            subjectPoint._points = (int)HigherGrades.H8;
                            break;
                    }
                }
                else
                {
                    switch (grade)
                    {
                        case 1:
                            subjectPoint._points = (int)OrdinaryGrades.O1;
                            break;
                        case 2:
                            subjectPoint._points = (int)OrdinaryGrades.O2;
                            break;
                        case 3:
                            subjectPoint._points = (int)OrdinaryGrades.O3;
                            break;
                        case 4:
                            subjectPoint._points = (int)OrdinaryGrades.O4;
                            break;
                        case 5:
                            subjectPoint._points = (int)OrdinaryGrades.O5;
                            break;
                        case 6:
                            subjectPoint._points = (int)OrdinaryGrades.O6;
                            break;
                        case 7:
                            subjectPoint._points = (int)OrdinaryGrades.O7;
                            break;
                        case 8:
                            subjectPoint._points = (int)OrdinaryGrades.O8;
                            break;
                    }
                }
            }
            else
            {
                if(isHigher)
                {
                    switch (grade)
                    {
                        case 1:
                            subjectPoint._points =  (int)HigherGrades.H1;
                            break;
                        case 2:
                            subjectPoint._points = (int)HigherGrades.H2;
                            break;
                        case 3:
                            subjectPoint._points = (int)HigherGrades.H3;
                            break;
                        case 4:
                            subjectPoint._points = (int)HigherGrades.H4;
                            break;
                        case 5:
                            subjectPoint._points = (int)HigherGrades.H5;
                            break;
                        case 6:
                            subjectPoint._points =(int)HigherGrades.H6;
                            break;
                        case 7:
                            subjectPoint._points = (int)HigherGrades.H7;
                            break;
                        case 8:
                            subjectPoint._points = (int)HigherGrades.H8;
                            break;
                    }
                }
                if(isOrdinary)
                {
                    switch (grade)
                    {
                        case 1:
                            subjectPoint._points = (int)OrdinaryGrades.O1;
                            break;
                        case 2:
                            subjectPoint._points = (int)OrdinaryGrades.O2;
                            break;
                        case 3:
                            subjectPoint._points = (int)OrdinaryGrades.O3;
                            break;
                        case 4:
                            subjectPoint._points = (int)OrdinaryGrades.O4;
                            break;
                        case 5:
                            subjectPoint._points = (int)OrdinaryGrades.O5;
                            break;
                        case 6:
                            subjectPoint._points = (int)OrdinaryGrades.O6;
                            break;
                        case 7:
                            subjectPoint._points = (int)OrdinaryGrades.O7;
                            break;
                        case 8:
                            subjectPoint._points = (int)OrdinaryGrades.O8;
                            break;
                    }
                }
                if(isLCVP)
                {
                    switch (grade)
                    {
                        case 1:
                            subjectPoint._points = (int)LCVPGrades.Distinction;
                            break;
                        case 2:
                            subjectPoint._points = (int)LCVPGrades.Merit;
                            break;
                        case 3:
                            subjectPoint._points = (int)LCVPGrades.Pass;
                            break;
        
                    }
                }
            }
            subjectPoints.Add(subjectPoint);
            _points = CalculatePoints();
            points.Text = _points + " points";
           
        }
        
        private int CalculatePoints()
        {
            List<SubjectPoints> orderedPoints =   subjectPoints.OrderByDescending(o=> o._points).ToList();
            int points = 0;
            for(int i =0;i<orderedPoints.Count;i++)
            {
                if(i<6)
                {
                    points += orderedPoints[i]._points;
                }
                
            }
            return points;
        }
    }
}