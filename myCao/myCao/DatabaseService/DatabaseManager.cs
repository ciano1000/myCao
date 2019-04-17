using myCao.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace myCao.DatabaseService
{
    public class DatabaseManager
    {
        SQLiteAsyncConnection dbConnection;
        
        public  DatabaseManager()
        {
            if (!DesignMode.IsDesignModeEnabled)
            {
                dbConnection = DependencyService.Get<IDBInterface>().CreateConnection("myCao");
            }
            
            
        }

        public async Task UpdateAsync(object obj)
        {
            await dbConnection.UpdateAsync(obj);
        }

        public async Task<bool> AddFavAsync(Course course)
        {
            Favourite favourite = new Favourite();
            favourite.CourseID = course.CourseID;
            favourite.CourseName = course.CourseTitle;
            var favs = await dbConnection.ExecuteScalarAsync<int>("Select count(*) from [Favourites] where CourseID = " +favourite.CourseID);

            if(favs == 0)
            {
                await dbConnection.InsertAsync(favourite);
                return true;
            }
            else
            {
                return false;
            }
            
        }

        public async void RemoveFavAsync(Favourite fav)
        {

            await dbConnection.DeleteAsync(fav);
        }

        public async void RemoveFavAsync(Course course)
        {
            Favourite fav = new Favourite();
            fav.CourseID = course.CourseID;
            fav.CourseName = course.CourseTitle;
            await dbConnection.DeleteAsync(fav);
        }
        public async Task<List<Course>> GetFavAsync()
        {
            var favourites = await dbConnection.QueryAsync<Favourite>("Select * From [Favourites]");
            List<Course> courses = new List<Course>();
            foreach(Favourite fav in favourites)
            {
                string query = String.Format("Select * From [CAOCourses] where CourseID = {0}",fav.CourseID);
                var course = await dbConnection.FindWithQueryAsync<Course>(query);
                courses.Add(course);
            }
            return courses;
        }

        public async Task<List<Subject>> GetSubjectsAsync()
        {
            return await dbConnection.QueryAsync<Subject>("Select * From [LC_Subjects] order by Name asc");
        }

        public List<string> RemoveStopWords(string searchString)
        {
            var searches = searchString.Split(' ');
            List<string> output = new List<string>();
            foreach(string search in searches)
            {
                if((search != "and") &&(!string.IsNullOrWhiteSpace(search)))
                {
                    output.Add(search);
                }
            }
            return output;
        }

        //TODO write the proper queries for all scenarios
        public async Task<List<Course>> GetRecommendedCoursesAsync(Search search)
        {
            string baseQuery = "select * from [CAOCourses] ";
            string whereQuery = "";
            string orderQuery = "";
            string query = "";
            //if no fav subjects are specified
            if (search.Subject1 == null && search.Subject2 == null && search.Subject3 == null)
            {
                //if search text is entered
                if (!string.IsNullOrEmpty(search.TextSearch))
                {
                    whereQuery = String.Format("where(CourseTitle like '%{0}%')", search.TextSearch);
                    if (search.MinPoints > 0 || search.MaxPoints != 0) //if the user has specified a points range
                    {

                        whereQuery += String.Format(" and (Points between {0} and {1})", search.MinPoints, search.MaxPoints);
                        orderQuery += "order by Points desc";
                        query = baseQuery + whereQuery + orderQuery;

                        return await dbConnection.QueryAsync<Course>(query);
                    }
                    else //if the user has not entered any other data, query just the search text
                    {
                        orderQuery += "order by Points desc";
                        query = baseQuery + whereQuery + orderQuery;
                        return await dbConnection.QueryAsync<Course>(query);
                    }

                }
                else //if no search text
                {
                    if (search.MinPoints > 0 || search.MaxPoints != 0) //if points range specified
                    {
                        whereQuery += String.Format("where(Points between {0} and {1})", search.MinPoints, search.MaxPoints);
                        orderQuery += "order by Points desc";

                        query += baseQuery + whereQuery + orderQuery;
                        return await dbConnection.QueryAsync<Course>(query);
                    }
                    else//if no points range specified, this scenario should not happen, as we should not let the user submit an empty search, protect against this in the code behind of the search page
                    {
                        return await dbConnection.QueryAsync<Course>(baseQuery);
                    }
                }

            }
            else// if fav subjects supplied
            {
                whereQuery = "where (";
                for (int i = 0; i < search.Subject1.Categories.Count; i++)
                {
                    if (i == 0)
                    {
                        whereQuery += String.Format("CourseArea like '%{0}%'", search.Subject1.Categories[i].Area);
                    }
                    else if ((i > 0))
                    {
                        whereQuery += String.Format(" or CourseArea like '%{0}%'", search.Subject1.Categories[i].Area);
                    }

                }
                for (int i = 0; i < search.Subject2.Categories.Count; i++)
                {
                    if (i == 0)
                    {
                        whereQuery += String.Format(" or CourseArea like '%{0}%'", search.Subject2.Categories[i].Area);
                    }
                    else if ((i > 0))
                    {
                        whereQuery += String.Format(" or CourseArea like '%{0}%'", search.Subject2.Categories[i].Area);
                    }

                }
                for (int i = 0; i < search.Subject3.Categories.Count; i++)
                {
                    if (i == 0)
                    {
                      
                        if (search.Subject3.Categories.Count == 1)
                        {
                            whereQuery += String.Format(" or CourseArea like '%{0}%')", search.Subject3.Categories[i].Area);
                        }
                        else
                        {
                            whereQuery += String.Format(" or CourseArea like '%{0}%'", search.Subject3.Categories[i].Area);
                        }
                    }
                    if ((i > 0) && (i != (search.Subject3.Categories.Count - 1)))
                    {
                        whereQuery += String.Format(" or CourseArea like '%{0}%'", search.Subject3.Categories[i].Area);
                    }
                    if (i == (search.Subject3.Categories.Count - 1))
                    {
                        whereQuery+= String.Format(" or CourseArea like '%{0}%')", search.Subject3.Categories[i].Area);
                    }
                }
                //if search text is entered
                if (!string.IsNullOrEmpty(search.TextSearch))
                {
                    whereQuery += String.Format(" and (CourseTitle like '%{0}%')", search.TextSearch);
                    if (search.MinPoints > 0 || search.MaxPoints != 0) //if the user has specified a points range,first find courses relating to fav subjects, then filter by search text, then order/filter by points
                    {

                        whereQuery += String.Format(" and (Points between {0} and {1})", search.MinPoints, search.MaxPoints);
                        orderQuery += "order by Points desc";
                        query = baseQuery + whereQuery + orderQuery;
                        return await dbConnection.QueryAsync<Course>(query);
                    }
                    else //if the user has not entered any other data, order by just the search text after filtering by fav subjects
                    {
                        orderQuery += "order by Points desc";
                        query = baseQuery + whereQuery + orderQuery;
                        return await dbConnection.QueryAsync<Course>(query);
                    }

                }
                else //if no search text
                {
                    if (search.MinPoints > 0 || search.MaxPoints != 0) //if points range specified, order subject query by points and filter out subjects outside range
                    {
                        whereQuery += String.Format(" and (Points between {0} and {1})", search.MinPoints, search.MaxPoints);
                        orderQuery += "order by Points desc";
                        query = baseQuery + whereQuery + orderQuery;
                        return await dbConnection.QueryAsync<Course>(query);

                    }
                    else//if no points range specified, return all courses filtered by subjects
                    {
                        orderQuery += SubjectSimilarityQuery(search.Subject1.Name,search.Subject2.Name,search.Subject3.Name);
                        query = baseQuery + whereQuery + orderQuery;
                        return await dbConnection.QueryAsync<Course>(query);
                    }
                }
            }



        }

        private string SubjectSimilarityQuery(string subject1, string subject2, string subject3)
        {
            string orderyby = "order by case ";
            string whenStatements = String.Format("when CourseTitle LIKE '%{0}%' then 1 when CourseTitle LIKE '%{1}%' then 2 when CourseTitle LIKE '%{2}%' then 3 else 4 end",subject1,subject2,subject3);
            return orderyby + whenStatements;
        }
    }
}
