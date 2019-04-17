using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace myCao.Models
{
    [Table("LC_Subjects")]
    public  class Subject
    {
        [Column("SubjectID")]
        public int SubjectID { get; set; }
        [Column("Name")]
        public string Name { get; set; }
        [Column("SubjectCategories")]
        public string RawCategories { get; set; }
        [Ignore]
        public bool Level { get; set; }
        [Ignore]
        public List<Category> Categories { get { return JsonConvert.DeserializeObject<List<Category>>(RawCategories); } set { Categories = value; } }
    }
}
