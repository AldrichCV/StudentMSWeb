using Microsoft.AspNetCore.Mvc;
using StudentInformationSystemWebApp.Models;

namespace StudentInformationSystemWebApp.Controllers
{
    public class StudentController : Controller
    {
        private static List<Student> studentsList = new List<Student>();
        public IActionResult StudentList()
        {
            return View(studentsList);
        }

        [HttpGet]
        public IActionResult CreateStudent()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateStudent(Student student)
        { 
          
           
            int recordNumber = studentsList.Count + 1;
            student.studentId = GenerateStudentId(recordNumber); 

            studentsList.Add(student);

            return RedirectToAction("StudentList");
        }


        public static string GenerateStudentId(int recordNumber)
        {
            string year = DateTime.Now.Year.ToString(); 
            string paddedRecord = recordNumber.ToString("D4");

            return $"{year}-{paddedRecord}";
        }



        [HttpGet]
        public IActionResult UpdateStudent(string id)
        {
            var student = studentsList.FirstOrDefault(s => s.studentId == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student); 
        }


        [HttpPost]
        public IActionResult UpdateStudent(Student updatedStudent)
        {
            var existing = studentsList.FirstOrDefault(s => s.studentId == updatedStudent.studentId);
            if (existing != null)
            {
              
                existing.firstName = updatedStudent.firstName;
                existing.middleName = updatedStudent.middleName;
                existing.lastName = updatedStudent.lastName;
                existing.nameExtension = updatedStudent.nameExtension;
                existing.gender = updatedStudent.gender;
                existing.dateOfBirth = updatedStudent.dateOfBirth;
                existing.department = updatedStudent.department;
                existing.yearLevel = updatedStudent.yearLevel;
            }

            return RedirectToAction("StudentList");
        }

        [HttpGet]
        public IActionResult DeleteStudent(string id)
        {
            var student = studentsList.FirstOrDefault(s => s.studentId == id);
            if (student != null)
            {
                studentsList.Remove(student);
            }

            return RedirectToAction("StudentList");
        }

    }
}
