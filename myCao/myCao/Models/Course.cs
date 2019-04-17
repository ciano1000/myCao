using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace myCao.Models
{
    [Table("CAOCourses")]
    public class Course:INotifyPropertyChanged
    {
        private void OnPropertyChanged(string propertyName)
        {
            if(PropertyChanged !=null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private int _favourite;
        [PrimaryKey]
        public int CourseID { get; set; }
        public string CourseTitle { get; set; }
        public string CourseCode { get; set; }
        public int CourseLevel { get; set; }
        public int Points { get; set; }
        public string CourseArea { get; set; }
        public int Favourite { get { return _favourite; }
            set
            {
                if (_favourite == value)
                { return; }
                else
                {
                    _favourite = value;
                    OnPropertyChanged(nameof(Favourite));
                    OnPropertyChanged(nameof(Fav));
                }
            }
        }
        [Ignore]
        public bool Fav { get { return Convert.ToBoolean(_favourite); }}
        [Ignore]
        public string Icon
        {
            get
            {
                switch(CourseArea)
                {
                    case "(041) Business and administration": return "business.png";
                    case "(022) Humanities (except languages)": return "humanity.png";
                    case "(021) Arts":return "art.png";
                    case "(011) Education": return "education.png";
                    case "(061) Information and Communication Technologies (ICTs)": return "it.png";
                    case "(071) Engineering and engineering trades":return "engineering.png";
                    case "(073) Architecture and construction": return "architecture.png";
                    case "(091) Health": return "health.png";
                    case "(051) Biological and related sciences": return "biology.png";
                    case "(101) Personal services": return "service.png";
                    case "(084) Veterinary": return "veterinary.png";
                    case "(053) Physical sciences": return "science.png";
                    case "(092) Welfare": return "welfare.png";
                    case "(042) Law": return "law.png";
                    case "(031) Social and behavioural sciences": return "social.png";
                    case "(081) Agriculture": return "agriculture.png";
                    case "(023) Languages": return "language.png";
                    case "(054) Mathematics and statistics": return "math.png";
                    case "(104) Transport services": return "transport.png";
                    case "(032) Journalism and information": return "journalism.png";
                    case "(040) Business, administration and law not further defined or elsewhere classified": return "business.png";
                    case "(088) Interdisciplinary programmes and qualifications involving agriculture, forestry and veterinary": return "agriculture.png";
                    case "(058) Interdisciplinary programmes and qualifications involving natural sciences, Mathematics and statistics": return "biology.png";
                    case "(082) Forestry": return "agriculture.png";
                    case "(028) Interdisciplinary programmes and qualifications involving arts and humanities": return "humanity.png";
                    default: return "art.png";

                }
            }

            set
            {
                Icon = value;
            }
        }

        
    }
}
