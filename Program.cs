namespace Task_3
{
    class Instructor
    {
        public int InstructorId { get; set; }
        public string Name { get; set; }
        public string Specialization { get; set; }

        public string PrintDetails()
        {
            return $"ID: {InstructorId}, Name: {Name}, Specialization: {Specialization}";
        }
    }

    class Course
    {
        public int CourseId { get; set; }
        public string Title { get; set; }
        public Instructor Instructor { get; set; }

        public string PrintDetails()
        {
            string instructorName = Instructor != null ? Instructor.Name : "No Instructor";
            return $"Course ID: {CourseId}, Title: {Title}, Instructor: {instructorName}";
        }
    }

    class Student
    {
        public int StudentId { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public List<Course> Courses { get; set; } = new List<Course>();

        public bool Enroll(Course course)
        {
            if (!Courses.Contains(course))
            {
                Courses.Add(course);
                return true;
            }
            return false;
        }

        public string PrintDetails()
        {
            string courseTitles = Courses.Count > 0 ? string.Join(", ", Courses.ConvertAll(c => c.Title)) : "No Courses Enrolled";
            return $"ID: {StudentId}, Name: {Name}, Age: {Age}, Courses: {courseTitles}";
        }
    }

    class SchoolStudentManager
    {
        public List<Student> Students { get; set; } = new List<Student>();
        public List<Course> Courses { get; set; } = new List<Course>();
        public List<Instructor> Instructors { get; set; } = new List<Instructor>();

        public bool AddStudent(Student student)
        {
            if (FindStudent(student.StudentId) == null)
            {
                Students.Add(student);
                return true;
            }
            return false;
        }

        public bool AddCourse(Course course)
        {
            if (FindCourse(course.CourseId) == null)
            {
                Courses.Add(course);
                return true;
            }
            return false;
        }

        public bool AddInstructor(Instructor instructor)
        {
            if (FindInstructor(instructor.InstructorId) == null)
            {
                Instructors.Add(instructor);
                return true;
            }
            return false;
        }

        public Student FindStudent(int studentId)
        {
            return Students.Find(s => s.StudentId == studentId);
        }

        public Course FindCourse(int courseId)
        {
            return Courses.Find(c => c.CourseId == courseId);
        }

        public Instructor FindInstructor(int instructorId)
        {
            return Instructors.Find(i => i.InstructorId == instructorId);
        }

        public bool EnrollStudentInCourse(int studentId, int courseId)
        {
            Student student = FindStudent(studentId);
            Course course = FindCourse(courseId);
            if (student != null && course != null)
            {
                return student.Enroll(course);
            }
            return false;
        }
        public bool IsStudentEnrolledInCourse(int studentId, int courseId)
        {
            Student student = FindStudent(studentId);
            Course course = FindCourse(courseId);
            if (student != null && course != null)
            {
                return student.Courses.Contains(course);
            }
            return false;
        }
        public string GetInstructorNameByCourseTitle(string courseTitle)
        {
            Course course = Courses.Find(c => c.Title.Equals(courseTitle, StringComparison.OrdinalIgnoreCase));
            return course?.Instructor?.Name ?? "Course or Instructor not found";
        }
        public bool UpdateStudent(int studentId, string newName, int newAge)
        {
            Student student = FindStudent(studentId);
            if (student != null)
            {
                student.Name = newName;
                student.Age = newAge;
                return true;
            }
            return false;
        }
        public bool DeleteStudent(int studentId)
        {
            Student student = FindStudent(studentId);
            if (student != null)
            {
                Students.Remove(student);
                return true;
            }
            return false;
        }
    }

    class Program
    {
        static void Main()
        {
            SchoolStudentManager manager = new SchoolStudentManager();
            string choice;

            do
            {
                Console.WriteLine("\n!~~~~~!Student Management System!~~~~~!");
                Console.WriteLine("1. Add Student");
                Console.WriteLine("2. Add Instructor");
                Console.WriteLine("3. Add Course");
                Console.WriteLine("4. Enroll Student in Course");
                Console.WriteLine("5. Show All Students");
                Console.WriteLine("6. Show All Courses");
                Console.WriteLine("7. Show All Instructors");
                Console.WriteLine("8. Find Student by ID");
                Console.WriteLine("9. Find Course by ID");
                Console.WriteLine("10. Exit");
                Console.WriteLine("11. Check if Student Enrolled in Specific Course");
                Console.WriteLine("12. Get Instructor Name by Course Title");
                Console.WriteLine("13. Update Student Information");
                Console.WriteLine("14. Delete Student");
                Console.Write("Enter your choice: ");
                choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.Write("Enter Student ID: ");
                        int sid = int.Parse(Console.ReadLine());
                        Console.Write("Enter Student Name: ");
                        string sname = Console.ReadLine();
                        Console.Write("Enter Age: ");
                        int age = int.Parse(Console.ReadLine());
                        Student s = new Student { StudentId = sid, Name = sname, Age = age };
                        if (manager.AddStudent(s))
                            Console.WriteLine("Student added successfully.");
                        else
                            Console.WriteLine("Student ID already exists!");
                        break;

                    case "2":
                        Console.Write("Enter Instructor ID: ");
                        int iid = int.Parse(Console.ReadLine());
                        Console.Write("Enter Instructor Name: ");
                        string iname = Console.ReadLine();
                        Console.Write("Enter Specialization: ");
                        string spec = Console.ReadLine();
                        Instructor ins = new Instructor { InstructorId = iid, Name = iname, Specialization = spec };
                        if (manager.AddInstructor(ins))
                            Console.WriteLine("Instructor added successfully.");
                        else
                            Console.WriteLine("Instructor ID already exists!");
                        break;

                    case "3":
                        Console.Write("Enter Course ID: ");
                        int cid = int.Parse(Console.ReadLine());
                        Console.Write("Enter Course Title: ");
                        string title = Console.ReadLine();
                        Console.Write("Enter Instructor ID for this course: ");
                        int instId = int.Parse(Console.ReadLine());
                        Instructor courseInstructor = manager.FindInstructor(instId);
                        if (courseInstructor != null)
                        {
                            Course c = new Course { CourseId = cid, Title = title, Instructor = courseInstructor };
                            if (manager.AddCourse(c))
                                Console.WriteLine("Course added successfully.");
                            else
                                Console.WriteLine("Course ID already exists!");
                        }
                        else
                        {
                            Console.WriteLine("Instructor not found!");
                        }
                        break;

                    case "4":
                        Console.Write("Enter Student ID: ");
                        int esid = int.Parse(Console.ReadLine());
                        Console.Write("Enter Course ID: ");
                        int ecid = int.Parse(Console.ReadLine());
                        if (manager.EnrollStudentInCourse(esid, ecid))
                            Console.WriteLine("Student enrolled successfully.");
                        else
                            Console.WriteLine("Enrollment failed. Check IDs!");
                        break;

                    case "5":
                        Console.WriteLine("\n    All Students   ");
                        foreach (var st in manager.Students)
                            Console.WriteLine(st.PrintDetails());
                        break;

                    case "6":
                        Console.WriteLine("\n    All Courses    ");
                        foreach (var co in manager.Courses)
                            Console.WriteLine(co.PrintDetails());
                        break;

                    case "7":
                        Console.WriteLine("\n    All Instructors   ");
                        foreach (var inst in manager.Instructors)
                            Console.WriteLine(inst.PrintDetails());
                        break;

                    case "8":
                        Console.Write("Enter Student ID to search: ");
                        int fid = int.Parse(Console.ReadLine());
                        var foundStudent = manager.FindStudent(fid);
                        if (foundStudent != null)
                            Console.WriteLine(foundStudent.PrintDetails());
                        else
                            Console.WriteLine("Student not found.");
                        break;

                    case "9":
                        Console.Write("Enter Course ID to search: ");
                        int fcid = int.Parse(Console.ReadLine());
                        var foundCourse = manager.FindCourse(fcid);
                        if (foundCourse != null)
                            Console.WriteLine(foundCourse.PrintDetails());
                        else
                            Console.WriteLine("Course not found.");
                        break;

                    case "11":
                        Console.Write("Enter Student ID: ");
                        int stId = int.Parse(Console.ReadLine());
                        Console.Write("Enter Course ID: ");
                        int crId = int.Parse(Console.ReadLine());
                        bool enrolled = manager.IsStudentEnrolledInCourse(stId, crId);
                        Console.WriteLine(enrolled ? "Yes, student is enrolled." : "No, student is NOT enrolled.");
                        break;

                    case "12":
                        Console.Write("Enter Course Title: ");
                        string ct = Console.ReadLine();
                        Console.WriteLine("Instructor: " + manager.GetInstructorNameByCourseTitle(ct));
                        break;

                    case "13":
                        Console.Write("Enter Student ID to update: ");
                        int upId = int.Parse(Console.ReadLine());
                        Console.Write("Enter new name: ");
                        string newName = Console.ReadLine();
                        Console.Write("Enter new age: ");
                        int newAge = int.Parse(Console.ReadLine());
                        if (manager.UpdateStudent(upId, newName, newAge))
                            Console.WriteLine("Student updated successfully.");
                        else
                            Console.WriteLine("Student not found.");
                        break;

                    case "14":
                        Console.Write("Enter Student ID to delete: ");
                        int delId = int.Parse(Console.ReadLine());
                        if (manager.DeleteStudent(delId))
                            Console.WriteLine("Student deleted successfully.");
                        else
                            Console.WriteLine("Student not found.");
                        break;

                    case "10":
                        Console.WriteLine("Goodbye!");
                        break;

                    default:
                        Console.WriteLine("Invalid option, please try again.");
                        break;
                }

            } while (choice != "10");
        }
    }
}
