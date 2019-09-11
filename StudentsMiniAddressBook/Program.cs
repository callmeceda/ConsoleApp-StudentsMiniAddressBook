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
        private static List<Student> newList;
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
                throw new Exception("Students were not stored.");
            }

        }

        private static void StoreStudentListInALocStorageAsTxt()
        {
            //string json = JsonConvert.SerializeObject(StudentDictionary);
            string json = JsonConvert.SerializeObject(Students);
            File.WriteAllText
                (path: @"D:\C#\Exercise projects\StudentsMiniAddressBook\StoredStudents\StudentsList.txt",
                contents: json);
        }

        /* Every time this program runs it will first try to load string of students
         * from .txt file in loc storage(StudentsList.txt) that was previosly used to save info about students.
         * If .txt file is empty(which will be the case first time it runs) it will create new list of students
         * that it will be used to save students when we create them. */
        private static void LoadStudentListFromLocStorage()
        {
            string LoadJson = File.ReadAllText
                (path: @"D:\C#\Exercise projects\StudentsMiniAddressBook\StoredStudents\StudentsList.txt");

            if (File.Exists(@"D:\C#\Exercise projects\StudentsMiniAddressBook\StoredStudents\StudentsList.txt"))
            {
                if (LoadJson == null && LoadJson == "")
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
            else
            {
                Console.Write("No file has been found on that location in local memory.");
                Console.ReadKey();
                throw new Exception();
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

            string inputName;
            do
            {
                Console.Write("Enter name of a student: ");
                inputName = Console.ReadLine();
                inputName.ToLower();
            } while (inputName == "");

            string inputEmail;
            do
            {
                Console.Write("Enter E-mail address of a student: ");
                inputEmail = Console.ReadLine();
            } while (inputEmail == "");

            int? inputSubjcetsLeftNo;
            do
            {
                Console.Write("Enter number of subjects that student has to pass: ");
                inputSubjcetsLeftNo = Convert.ToInt32(Console.ReadLine());
            } while (inputSubjcetsLeftNo < 0 || inputSubjcetsLeftNo > 10 || inputSubjcetsLeftNo == null);

            try
            {
               /* do
                {
                    Console.Write("Enter number of subjects that student has to pass: ");
                    inputSubjcetsLeftNo = Convert.ToInt32(Console.ReadLine());
                } while (inputSubjcetsLeftNo < 0 || inputSubjcetsLeftNo > 10 || inputSubjcetsLeftNo == null);
                */

                
                foreach (Student stud in Students)
                {
                    if (stud.StudentID != inputID)
                    {
                        newStudent = new Student
                            (id: inputID,
                            name: inputName.ToLower(),
                            email: inputEmail,
                            subj: inputSubjcetsLeftNo.Value);
                        StoreStudentsInAList(newStudent);
                        StoreStudentListInALocStorageAsTxt();
                        break;
                    }
                    else
                    {
                        Console.WriteLine();
                        Console.Write("There is already student with that ID \n\r" +
                            "cant create student with that student ID \n\r");
                        Console.WriteLine
                            ("ID: " + stud.StudentID + "\n\r" +
                            "name: " + stud.StudentName + "\n\r" +
                            "email: " + stud.StudentEmail + "\n\r" + 
                            "subjects left to pass: " + stud.SubjectsLeft);
                        Console.ReadKey();
                        break;
                    }
                }
                

            }
            catch (FormatException error)
            {
                Console.WriteLine("Type of user value is not valid for this input. Input must be text.");
                Console.WriteLine(error.Message);
                Console.ReadKey();
            }
        }
        private static void FindStudent()
        {
            bool studentFound = false;
            bool studentLookedUp = false;
            string input;
            //string lowerInput;
            //input.ToLower();
            while (!studentLookedUp && !studentFound)
            {
                do
                {
                    Console.Write("Enter name of a student that you want to find: ");
                    input = Console.ReadLine();
                    //lowerInput = input.ToLower();
                    studentLookedUp = true;
                    studentFound = false;
                } while (input == "");

                foreach (Student student in Students)
                {
                    if (string.Compare(input.ToLower(), student.StudentName, ignoreCase:true) == 0)
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
            /*for (int pass = 0; pass < Students.Count - 1; pass++)
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
            }*/

            newList = Students.OrderBy(s => s.StudentID).ToList();
            foreach (Student student in newList)
            {
                Console.WriteLine
                        ("Student ID: {0}, name: {1}, email: {2}, subjects left{3}",
                        student.StudentID, student.StudentName, student.StudentEmail, student.SubjectsLeft);
            }
        }

        private static void orderByName()
        {
            /*for (int pass = 0; pass < Students.Count - 1; pass++)
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
            }*/

            newList = Students.OrderBy(s => s.StudentName).ToList();
            foreach (Student student in newList)
            {
                Console.WriteLine
                        ("Student name: {0}, id: {1}, email: {2}, subjects left{3}",
                        student.StudentName, student.StudentID, student.StudentEmail, student.SubjectsLeft);
            }
        }


        private static void OrderBySubjectsLeftToPass_Descending()
        {
            /*
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
            }*/

            var subjects = from student in Students
                      orderby student.SubjectsLeft descending
                      select student;
            newList = subjects.ToList();
            foreach (Student student in newList)
            {
                Console.WriteLine
                        ("Subjects left: {0}, id: {1}, name: {2}, email: {3}",
                        student.SubjectsLeft, student.StudentID, student.StudentName, student.StudentEmail);
            }
        }
        private static void OrderBySubjectsLeftToPass_Ascending()
        {
            var subjects = from student in Students
                           orderby student.SubjectsLeft ascending
                           select student;
            newList = subjects.ToList();
            foreach (Student student in newList)
            {
                Console.WriteLine
                        ("Subjects left: {0}, id: {1}, name: {2}, email: {3}",
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
                    Console.WriteLine();
                    orderByID();
                    Console.ReadKey();
                    break;

                case "2":
                    Console.WriteLine();
                    orderByName();
                    Console.ReadKey();
                    break;

                case "3":
                    Console.WriteLine();
                    Console.Write
                        ("Press 1 for descenidng order \n\r" +
                         "Press 2 for ascending order \n\r");
                    string command2 = Console.ReadLine();
                    switch (command2)
                    {
                        case "1":
                            Console.WriteLine();
                            OrderBySubjectsLeftToPass_Descending();
                            break;

                        case "2":
                            Console.WriteLine();
                            OrderBySubjectsLeftToPass_Ascending();
                            break;
                        default:
                            break;
                    }
                    Console.ReadKey();
                    break;
                case "4":
                    Console.WriteLine();
                    OrderBySubjectsLeftToPass_Ascending();
                    Console.ReadKey();
                    break;
                default:
                    break;
            }
        }
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
                while (studentNo < Students.Count)
                {
                    studentNo++;
                    break;
                }
           }


            string name;
            command = Console.ReadLine();

            for (int i = 0; i <= Students.Count - 1; i++)
            {
                if (command == (i + 1).ToString())
                {
                    name = Students[i].StudentName;
                    Students.RemoveAt(i);
                    if (!CheckForNameInList(name))
                    {
                        Console.Write("Student was removed successfully \r\n");
                        Console.ReadLine();
                        StoreStudentListInALocStorageAsTxt();
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
                        CreateNewStudent();
                        Console.WriteLine();
                        break;

                    case "2":
                        //ClearConsoleLines();
                        FindStudent();
                        Console.WriteLine();
                        break;

                    case "3":
                        DisplayStudents();
                        Console.WriteLine();
                        break;

                    case "4":
                        DeleteStudent();
                        Console.WriteLine();
                        break;

                    case "5":
                        DisplayMenu = false;
                        break;

                    default:
                        Console.WriteLine("You must choose one of five numbers(options)");
                        Console.ReadKey();
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
            int NoSubj = 1; //number of subjects
            int id = 1;
            foreach (string names in students)
            {
                Student newStudent1 = new Student
                    (name: names, id: names + "'s ID - " + id,
                    email: names + "'s email", subj: NoSubj);
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
