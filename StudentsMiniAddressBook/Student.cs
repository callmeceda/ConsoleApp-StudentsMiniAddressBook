using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace StudentsMiniAddressBook
{
    //Class Student with StudentID, Name, Email, No of Subjects left to graduate as class data members.
    class Student
    {

        //It is private because it's not allowed to be available for change from outside the class.
        private string id;

        //Helper Method. Checks if any of the carachters in a given string is a number
        /*private static bool LettersOrDigits(string s)
        {
            foreach (char charachter in s)
            {
                if (char.IsDigit(charachter))
                {
                    return false;
                }
            }
            return true;
        }*/

        //returns true if s that is char inside of string inputName is digit, and false if is not
        private static bool LettersOrDigits(string inputName)
        {
            return inputName.Any(s => char.IsDigit(s));
        }


        public string StudentID
        {
            get { return id; }
            set
            {
                if (value != "" || value != null)
                {
                    id = value;
                }
                else
                {
                    id = "Input value for id is not valid.";
                    throw new FormatException(id);
                }
            }
        }

        private string name;

        public string StudentName
        {
            get { return name; }
            set
            {
                if (!LettersOrDigits(value) && value != null)
                {
                    name = value;
                }
                else
                {
                    name = "Input value for name is not valid, cant be a number. \n\r";
                    throw new FormatException(name);
                }
            }
        }

        //public string StudentName;
        public string StudentEmail;

        //It is private because it's not allowed to be available for change from outside the class.
        //It's also nullable type because it will be checked if the user has entered any kind of value/input
        private int? subjects;
        //Helper Method. Checks if given int value for specific property is null(has no any kind of value)
        private bool IsItNullable(int? val)
        {
            if (val.HasValue)
            {
                return true;
            }
            return false;
        }
        public int SubjectsLeft
        {
            get { return subjects.Value; }
            set
            {
                if (value >= 0 || value <= 10 && IsItNullable(value))
                {
                    subjects = value;
                }
                else
                    throw new FormatException("Input value for number of subjects left for pass is not valid.");

            }
        }
        //Cheking if user input is correct for each of the class data members that are variables.
        /*private static string validateInput(string input)
        {
            if (input == "" || input == null)
            {
                return "Input value is not valid. Cannot be empty.";
            }
            return "";
        }*/

        //If i dont initalize these 4 parameters before assaigning them to class properties, i get 
        //null reference exceptions because no value is assign to them.
        public Student()
        {
            StudentID = "";
            StudentName = "";
            StudentEmail = "";
            SubjectsLeft = 0;
        }

        //Constructor
        public Student(string id, string name, string email, int subj)
        {
            //Validating input for ContactName.
            if (string.IsNullOrWhiteSpace(name))
            {
                //throw new Exception(validateInput(name));
                Console.WriteLine("Input value is not valid. Cannot be empty.");
                Console.ReadKey();
                throw new NullReferenceException();
            }

            //Validating input for ContactEmail.
            else if (string.IsNullOrWhiteSpace(email))
            {
                //throw new Exception(validateInput(email));
                Console.WriteLine("Input value is not valid. Cannot be empty.");
                Console.ReadKey();
                throw new NullReferenceException();

            }



            //Constructor parametars must be the same name as class properties so that json deserializer can
            //make a valid student object of json properties. This way json deserializer knows how to bind json
            //properties to the constructors parametars because they share the same name ?
            //if (string.Compare(StudentName, name.ToLower(), ignoreCase: true) == 0)
            //{
                this.StudentName = name;
            //}
            this.StudentEmail = email;
            this.StudentID = id;
            this.SubjectsLeft = subj;
        }
    }
}