using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;


namespace StudentsMiniAddressBook
{

    class Program
    {
        private static Student newStudent;

        //private static Dictionary<string, Student> StudentDictionary = new Dictionary<string, Student>();
        private static List<Student> Students = new List<Student>();

        private static void StoreStudentsInAList(Student student)
        {
            //StudentDictionary.Add(student.StudentID, student);
            try
            {
                Students.Add(student);
                StoreStudentListInALocStorageAsTxt();
            }
            catch (Exception)
            {
                throw new Exception("Contact were not stored.");
            }

        }

        private static void StoreStudentListInALocStorageAsTxt()
        {
            //string json = JsonConvert.SerializeObject(StudentDictionary);
            string json = JsonConvert.SerializeObject(Students);
            File.WriteAllText
                (path: @"D:\C#\Exercise projects\ContactsConsoleApp\StoredStudents\StudentsList.txt",
                contents: json);
        }

        /* Every time this program runs it will first try to load string of students
         * from .txt file in loc storage(StudentsList.txt) that was previosly used to save info about students.
         * If .txt file is empty(which will be the case first time it runs) it will create new list of students
         * that it will be used to save students when we create them. */
        private static void LoadStudentListFromLocStorage()
        {
            string LoadJson = File.ReadAllText
                (path: @"D:\C#\Exercise projects\ContactsConsoleApp\StoredStudents\StudentsList.txt");

            if (LoadJson == null)
            {
                Console.WriteLine("\nIn the student list that is used for storing," +
                    " there were no students info to be found.");
                Console.WriteLine("New list will be created.");
                Console.ReadKey();
                //StudentDictionary = new Dictionary<string, Student>();*/

                Students = new List<Student>();
            }
            else
            {
                //StudentDictionary = new Dictionary<string, Student>();
                Students = JsonConvert.DeserializeObject<List<Student>>(LoadJson);

            }
        }

        //Creating a student
        private static void CreateNewStudent()
        {
            string inputID;
            do
            {
                Console.Write("Enter ID of a student: ");
                inputID = Console.ReadLine();
            } while (inputID == "");

            string inputName = "";
            do
            {
                Console.Write("Enter name of a student: ");
                inputName.ToLower();
                inputName = Console.ReadLine();
            } while (inputName == "");

            string inputEmail;
            do
            {
                Console.Write("Enter E-mail address of a student: ");
                inputEmail = Console.ReadLine();
            } while (inputEmail == "");

            int? inputSubjcetsLeftNo;

            try
            {
                do
                {
                    Console.Write("Enter number of subjects that student has to pass: ");
                    inputSubjcetsLeftNo = Convert.ToInt32(Console.ReadLine());
                } while (inputSubjcetsLeftNo < 0 || inputSubjcetsLeftNo > 10 || inputSubjcetsLeftNo == null);

                newStudent = new Student
                    (id: inputID, name: inputName.ToLower(), email: inputEmail, subjects: inputSubjcetsLeftNo.Value);
                StoreStudentsInAList(newStudent);
                StoreStudentListInALocStorageAsTxt();
            }
            catch (FormatException error)
            {
                Console.WriteLine("Type of user value is not valid for this input. Input must be text.");
                Console.WriteLine(error.Message);
                Console.ReadKey();
            }
            catch (Exception error)
            {
                Console.WriteLine("Invalid input value");
                Console.WriteLine(error.Message);
                Console.ReadKey();
            }
        }
        private static void FindStudent()
        {
            bool studentFound = false;
            bool studentLookedUp = false;
            string input = "";
            input.ToLower();
            while (!studentLookedUp && !studentFound)
            {
                do
                {
                    Console.Write("Enter name of a student that you want to find: ");
                    input = Console.ReadLine();
                    studentLookedUp = true;
                    studentFound = false;
                } while (input == "");

                foreach (Student student in Students)
                {
                    if (input == student.StudentName)
                    {
                        Console.Write
                            ("Student name: {0}\r\n" +
                            "Student ID: {1}\r\n" +
                            "Student E-mail address: {2}\r\n" +
                            "Student subjects left to pass: {3}\r\n",
                            student.StudentName, student.StudentID, student.StudentEmail, student.SubjectsLeft);
                        studentLookedUp = true;
                        studentFound = true;
                        break;
                    }
                    else
                    {
                        continue;
                    }
                }
                if (studentFound == false)
                {
                    Console.Write("Name of a student you entered was not found in list of Students \r\n");
                }
                Console.ReadLine();
            }
        }

        private static void orderByID()
        {
            for (int pass = 0; pass < Students.Count - 1; pass++)
            {
                for (int elem = 0; elem < Students.Count - 1 - pass; elem++)
                {
                    if (string.Compare(Students[elem].StudentID, Students[elem + 1].StudentID) > 0)
                    {
                        Student temp = Students[elem];
                        Students[elem] = Students[elem + 1];
                        Students[elem + 1] = temp;
                    }
                }
            }

            foreach (Student student in Students)
            {
                if (student.StudentID == null)
                {
                    continue;
                }
                else
                    Console.WriteLine
                        ("Student ID: {0}, name: {1}, email: {2}, subjects left{3}",
                        student.StudentID, student.StudentName, student.StudentEmail, student.SubjectsLeft);
            }
        }

        private static void orderByName()
        {
            for (int pass = 0; pass < Students.Count - 1; pass++)
            {
                for (int elem = 0; elem < Students.Count - 1 - pass; elem++)
                {
                    if (string.Compare(Students[elem].StudentName, Students[elem + 1].StudentName) > 0)
                    {
                        Student temp = Students[elem];
                        Students[elem] = Students[elem + 1];
                        Students[elem + 1] = temp;
                    }
                }
            }

            foreach (Student student in Students)
            {
                if (student.StudentName == null)
                {
                    continue;
                }
                else
                    Console.WriteLine
                        ("Student name: {0}, id: {1}, email: {2}, subjcets left{3}",
                        student.StudentName, student.StudentID, student.StudentEmail, student.SubjectsLeft);
            }
        }

        private static void OrderBySubjetsLeftToPass()
        {

            //Number of times it passed through list
            for (int pass = 0; pass < Students.Count - 1; pass++)
            {
                //When it goes in first passing through list, it access elemts of that list 
                for (int elem = 0; elem < Students.Count - 1 - pass; elem++)
                {
                    //Compares first two elements of list and continues to compare next with the second and so on
                    if (Students[elem].SubjectsLeft < Students[elem + 1].SubjectsLeft)
                    {
                        Student temp = Students[elem];
                        Students[elem] = Students[elem + 1];
                        Students[elem + 1] = temp;
                    }
                }
            }

            foreach (Student student in Students)
            {
                if (student.StudentName == null)
                {
                    continue;
                }
                else
                    Console.WriteLine
                        ("Subjcets lef to to pass: {0}, id: {1}, name: {2}, email{3}",
                        student.SubjectsLeft, student.StudentID, student.StudentName, student.StudentEmail);
            }
        }
        private static void DisplayStudents()
        {
            Console.Write
                ("Press 1 to Order by \"Student ID\"\n\r" +
                 "Press 2 to Order by \"Student name\"\n\r" +
                 "Press 3 to Order by \"Number of subjcets left for student to pass\" ");
            string command = Console.ReadLine();

            switch (command)
            {
                case "1":
                    orderByID();
                    Console.ReadKey();
                    break;

                case "2":
                    orderByName();
                    Console.ReadKey();
                    break;

                case "3":
                    OrderBySubjetsLeftToPass();
                    Console.ReadKey();
                    break;

                default:
                    break;
            }
        }
        /*private static bool Remove(int student)
        {
            for (student = 0; student < Students.Count - 1; student ++)
            {

            }
        }*/
        private static void DeleteStudent()
        {
            string command = "";
            int studentNo = 1;

            foreach (Student student in Students)
            {
                Console.WriteLine
                    ("{0}). Student ID: {1}, Student Name: {2}," +
                    " Student email: {3}, Subjects left to pass: {4}",
                    studentNo, student.StudentID, student.StudentName,
                    student.StudentEmail, student.SubjectsLeft);
                Console.Write("Press {0} to remove student with number {1}\r\n", studentNo, studentNo);
                if (studentNo < Students.Count)
                {
                    studentNo += 1;
                }

            }
            /*for (int i = 0; i < Students.Count - 1; i++)
            {
                Console.WriteLine
                    ("{0}). Student ID: {1}, Student Name: {2}," +
                    " Student email: {3}, Subjects left to to pass: {4}",
                    studentNo, Students[i].StudentID, Students[i].StudentName,
                    Students[i].StudentEmail, Students[i].SubjectsLeft);
                Console.Write("Press {0} to remove student with number {1}\r\n", studentNo, studentNo);
                studentNo += 1;
               
            }*/


            string name;
            command = Console.ReadLine();

            for (int i = 0; i < Students.Count - 1; i++)
            {
                if (command == (i + 1).ToString())
                {
                    name = Students[i].StudentName;
                    Students.RemoveAt(i);
                    if (!CheckForNameInList(name))
                    {
                        Console.Write("Student was removed successfully \r\n");
                        Console.ReadLine();
                        break;
                    }
                    else
                    {
                        Console.Write("Removing of student was unsuccessful. \r\n");
                        Console.ReadLine();
                        break;
                    }
                }
            }
        }

        private static bool CheckForNameInList(string name)
        {
            foreach (Student student in Students)
            {
                if (name != student.StudentName)
                {
                    return false;
                }
            }
            return true;
        }
        private static void AddressBookMenu()
        {

            bool DisplayMenu = true;

            while (DisplayMenu)
            {
                Console.WriteLine(
                    "Press 1 for \"New Student\"\n\r" +
                    "Press 2 for \"Find Student\"\n\r" +
                    "Press 3 for \"Display Students\"\n\r" +
                    "Press 4 for \"Delete Student\"\n\r" +
                    "Press 5 to Exit from Application");

                string command = Console.ReadLine();
                switch (command)
                {
                    case "1":
                        //ClearConsoleLines();
                        //TestData();
                        //CreateNewStudent();
                        break;

                    case "2":
                        //ClearConsoleLines();
                        FindStudent();
                        break;

                    case "3":
                        DisplayStudents();
                        break;

                    case "4":
                        DeleteStudent();
                        break;

                    case "5":
                        DisplayMenu = false;
                        break;

                    default:
                        break;
                }
            }
        }

        //Helper Method. Delets 
        public static void ClearConsoleLines()
        {
            int currentLine = Console.CursorTop;
            for (int i = Console.CursorTop; i <= 5; i--)
            {
                Console.Write("\r" + new string(' ', Console.WindowWidth) + "\r");
            }
        }

        private static void TestData()
        {
            string[] students = { "Marko", "Janko", "Darko", "Ranko", "Branko" };
            int NoSubj = 1;
            int id = 1;
            foreach (string names in students)
            {
                Student newStudent1 = new Student
                    (name: names, id: names + "'s ID - " + id, email: names + "'s email", subjects: NoSubj);
                StoreStudentsInAList(newStudent1);
                NoSubj += 1;
                id++;
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("-------------------- Students Mini Address Book --------------------");
            Console.WriteLine("");
            Console.WriteLine("                                 *                             ");
            Console.WriteLine("                                 *                             ");
            Console.WriteLine("                                 *                             ");

            LoadStudentListFromLocStorage();
            AddressBookMenu();
            //TestData();
            //Console.ReadKey();


        }

    }
}
