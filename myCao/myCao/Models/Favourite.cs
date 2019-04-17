using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace myCao.Models
{
    [Table("Favourites")]
    public class Favourite
    {
        [PrimaryKey]
        public int CourseID { get; set; }
        public string CourseName { get; set; }
    }
}
