using System.ComponentModel;

namespace EmployeeData.Models
{
    public class EmployeeViewModel
    {
        public int Id { get; set; }
        [DisplayName("First Name")]
        public string FirstName { get; set; }
        [DisplayName("Last Name")]
        public string LastName { get; set; }
        [DisplayName("Date Of Birth")]
        public DateTime DateofBirth { get; set; }
        [DisplayName("Email")]
        public string Email { get; set; }
        [DisplayName("Salary")]
        public double Salary { get; set; }
    }
}
