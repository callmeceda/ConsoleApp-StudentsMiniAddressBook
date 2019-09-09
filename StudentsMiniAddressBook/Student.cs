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
        private static bool LettersOrDigits(string s)
        {
            foreach (char letter in s)
            {
                if (char.IsLetterOrDigit(letter))
                {
                    return true;
                }
            }
            return false;
        }


        public string StudentID
        {
            get { return id; }
            set
            {
                if (value != "" || LettersOrDigits(value) || value != null)
                {
                    id = value;
                }
                else
                {
                    id = "Input value not valid.";
                    throw new Exception(id);
                }
            }
        }


        public string StudentName;
        public string StudentEmail;

        //It is private because it's not allowed to be available for change from outside the class.
        //It's also nullable type because it will be checked if the user has entered any kind of value/input
        private int? subjects;
        //Helper Method. Checks if given int value for specific property is null(has no any kind of value)
        private bool isItNullable(int? val)
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
                if (value > 0 && value <= 10 && isItNullable(value))
                {
                    subjects = value;
                }
                else
                    throw new Exception("Input value is not valid.");

            }
        }
        //Cheking if user input is correct for each of the class data members that are variables.
        private static string validateInput(string input)
        {
            if (input == "" || input == null)
            {
                return "Input value is not valid. Cannot be empty.";
            }
            return "";
        }

        //Constructor
        public Student(string id, string name, string email, int subjects)
        {
            //Validating input for ContactName.
            if (validateInput(name) != "")
            {
                throw new Exception(validateInput(name));
            }

            //Validating input for ContactEmail.
            else if (validateInput(email) != "")
            {
                throw new Exception(validateInput(email));
            }

            StudentName = name.ToLower();
            StudentEmail = email;
            StudentID = id;
            SubjectsLeft = subjects;
        }
    }
}