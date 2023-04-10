using littleviewservice.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using static littleviewservice.Controllers.AccountController;

namespace littleviewservice.Controllers
{
    [Route("LittleService/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly LittleContext _dbContext;

        public StudentController(LittleContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Student>>> GetStudent()
        {
            if (_dbContext.vw_students == null)
            {
                return NotFound();
            }
            return await _dbContext.vw_students.ToListAsync();
        }

        [HttpGet("Attendance")]
        public async Task<ActionResult<IEnumerable<StudentAttendance>>> GetAttendance()
        {
            if (_dbContext.view_attendance == null)
            {
                return NotFound();
            }
            return await _dbContext.view_attendance.ToListAsync();
        }

        [HttpGet("AttendanceDetail")]
        public async Task<ActionResult<IEnumerable<Attendance>>> GetAttendanceDetail()
        {
            if (_dbContext.tbl_attendance == null)
            {
                return NotFound();
            }
            return await _dbContext.tbl_attendance.ToListAsync();
        }

        [HttpGet("Attendance/GetDailyAttendance/{student_id}")]
        public async Task<ActionResult<List<Attendance>>> GetDailyAttendance(int student_id)
        {
            var attendanceList = await _dbContext.tbl_attendance
            .Where(n => n.Student_id == student_id)
            .OrderByDescending(n => n.Date)  // Send_date sütununa göre tersine sırala
            .ToListAsync();

            if (attendanceList == null || !attendanceList.Any())
            {
                return NotFound();
            }

            return attendanceList;

        }

        [HttpGet("Attendance/GetAttendanceReport/{student_id}")]
        public async Task<ActionResult<List<AttendanceReport>>> GetAttendanceReport(int student_id)
        {
            var attendanceList = await _dbContext.view_attendance_report
            .Where(n => n.Student_id == student_id)
            .OrderByDescending(n => n.Year)  // Date sütununa göre tersine sırala
            .ThenByDescending(n => n.Month) // Month sütununa göre tersine sırala
            .ToListAsync();

            if (attendanceList == null || !attendanceList.Any())
            {
                return NotFound();
            }

            return attendanceList;

        }

        [HttpPost("Attendance/addAttendance")]
        public async Task<IActionResult> AddStudentAsync([FromBody] AttendanceCredentials credentials)
        {

            Attendance student = new Attendance
            {
                Student_id = credentials.Student_id,
                Status = credentials.Status,
                Date = credentials.Date
            };

            _dbContext.tbl_attendance.Add(student);
            await _dbContext.SaveChangesAsync();
            return Ok("Inserted!");
        }

        [HttpDelete("Attendance/resetAttendance/{date}")]
        public IActionResult DeleteRecordsByDate(DateTime date)
        {
            var records = _dbContext.tbl_attendance
                .Where(r => r.Date.Date == date.Date)
                .ToList();

            if (records.Count == 0)
            {
                return NotFound();
            }

            _dbContext.tbl_attendance.RemoveRange(records);
            _dbContext.SaveChanges();

            return NoContent();
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> GetStudent(int id)
        {
            if (_dbContext.vw_students == null)
            {
                return NotFound();
            }
            var student = await _dbContext.vw_students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            return student;
        }


        [HttpGet("Parent/{parent_id}")]
        public async Task<ActionResult<IEnumerable<ParentStudent>>> GetParentStudent(int parent_id)
        {
            var students = await _dbContext.view_parent_student.Where(s => s.Parent_id == parent_id).ToListAsync();

            if (students == null)
            {
                return NotFound();
            }

            return students;
        }

        [HttpPost("addStudent")]
        public async Task<IActionResult> AddStudentAsync([FromBody] StudentCredentials credentials)
        {
            
            StudentProfile student = new StudentProfile
            {
                Name = credentials.name,
                Surname = credentials.surname,
                Gender = credentials.gender,
                Blood_type = credentials.blood_type,
                Birth_date = credentials.birth_date,
                Parent_1 = credentials.parent_1,
                Parent_number_1 = credentials.parent_1,
                Parent_2= credentials.parent_2,
                Parent_number_2 = credentials.parent_2,
                Address = credentials.address,
                Notes = credentials.notes,
                Img = credentials.img
            };
            
            if (student.Gender != 'M' &&
                student.Gender != 'F' &&
                student.Gender != 'E' &&
                student.Gender != 'K') return NotFound();

            _dbContext.tbl_student.Add(student);
            await _dbContext.SaveChangesAsync();
            return Ok("Inserted!");
        }

        [HttpPut("updateStudent/{id}")]
        public async Task<IActionResult> UpdateStudentAsync(int id, [FromBody] StudentCredentials credentials)
        {
            var student = await _dbContext.tbl_student.FindAsync(id);
            if (student == null) return NotFound();

            student.Name = credentials.name;
            student.Surname = credentials.surname;
            student.Gender = credentials.gender;
            student.Blood_type = credentials.blood_type;
            student.Birth_date = credentials.birth_date;
            student.Parent_1 = credentials.parent_1;
            student.Parent_number_1 = credentials.parent_number_1;
            student.Parent_2 = credentials.parent_2;
            student.Parent_number_2 = credentials.parent_number_2;
            student.Address = credentials.address;
            student.Notes = credentials.notes;
            student.Img = credentials.img;

            if (student.Gender != 'M' &&
                student.Gender != 'F' &&
                student.Gender != 'E' &&
                student.Gender != 'K') return NotFound();

            _dbContext.tbl_student.Update(student);
            await _dbContext.SaveChangesAsync();
            return Ok("Updated!");
        }

        [HttpPut("updateParentId/{id}/{parent_id}")]
        public async Task<IActionResult> UpdateParentIdAsync(int id, int parent_id)
        {
            var student = await _dbContext.tbl_student.Where(n => n.ID == id).ToListAsync();
            if (student == null || student.Count == 0)
            {
                return NotFound();
            }
            foreach (var std in student)
            {
                std.Parent_id = parent_id;
                _dbContext.tbl_student.Update(std);
            }
            await _dbContext.SaveChangesAsync();
            return Ok("Updated!");
        }

        public class StudentCredentials
        {
            public string name { get; set; }
            public string surname { get; set; }
            public char gender { get; set; }
            public string blood_type { get; set; } = "";
            public DateTime birth_date { get; set; } = DateTime.Today;
            public string parent_1 { get; set; } = "";
            public string parent_number_1 { get; set; } = "";
            public string parent_2 { get; set; } = "";
            public string parent_number_2 { get; set; } = "";
            public string address { get; set; } = "";
            public string notes { get; set; } = "";
            public string img { get; set; } = "";
        }

        public class AttendanceCredentials
        {
            public int Student_id { get; set; }
            public int Status { get; set; }
            public DateTime Date { get; set; }
        }
    }
}