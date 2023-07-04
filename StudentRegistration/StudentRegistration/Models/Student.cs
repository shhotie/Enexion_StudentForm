using Microsoft.Data.SqlClient;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Xml.Linq;

namespace StudentRegistration.Models
{
    public class Student
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Date of Birth is required")]
        public DateTime DOB { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Gender is required")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "Language is required")]
        public string Language { get; set; }


        public static List<Student> GetStudents()
        {
            List<Student> studentList = new List<Student>();
            SqlConnection con = new SqlConnection();
            con.ConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Student;Integrated Security=True";
            try
            {
                con.Open();
                SqlCommand cmdList = new SqlCommand();
                cmdList.Connection = con;
                cmdList.CommandType = CommandType.Text;
                cmdList.CommandText = "select * from Students";
                SqlDataReader dr = cmdList.ExecuteReader();
                while (dr.Read())
                {
                    studentList.Add(new Student
                    {
                        Id = dr.GetInt32("Id"),
                        Name = dr.GetString("Name"),
                        DOB = (DateTime)dr["DOB"],
                        Email = dr["Email"].ToString(),
                        Gender = dr["Gender"].ToString(),
                        Language = dr["Language"].ToString()
                    });
                }
                dr.Close();
                return studentList;
            }
            catch
            {
                throw;
            }
            finally { con.Close(); }
        }

        public static void AddStudent(Student student)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Student;Integrated Security=True";
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "Insert into Students values (@Name, @DOB, @Email, @Gender, @Language)";
                
                cmd.Parameters.AddWithValue("@Name", student.Name);
                cmd.Parameters.AddWithValue("@DOB", student.DOB);
                cmd.Parameters.AddWithValue("@Email", student.Email);
                cmd.Parameters.AddWithValue("@Gender", student.Gender);
                cmd.Parameters.AddWithValue("@Language", student.Language);

                cmd.ExecuteNonQuery();

            }
            catch { throw; }
            finally { con.Close(); }

        }
    }
}
