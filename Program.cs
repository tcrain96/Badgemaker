using System;
using System.Collections.Generic;

namespace CatWorx.BadgeMaker
{
    class Employee
    {
        private string FirstName;

        private string LastName;

        private int Id;

        private string PhotoUrl;

        public Employee(
            string firstName,
            string lastName,
            int id,
            string photoUrl
        )
        {
            FirstName = firstName;
            LastName = lastName;
            Id = id;
            PhotoUrl = photoUrl;
        }

        public string GetName()
        {
            return FirstName + " " + LastName;
        }

        public int GetId()
        {
            return Id;
        }

        public string GetPhotoUrl()
        {
            return PhotoUrl;
        }

        public string GetCompanyName()
        {
            return "Cat Worx";
        }
    }

    class Program
    {
        static void Main(string[] args)
        {

            bool exit = false;
            while(exit == false)
            {
                Console.WriteLine("");
                Console.WriteLine("What would you like to do?");
                Console.WriteLine("A) Manually populate employee information");
                Console.WriteLine("B) Automatically populate based off an online repository");
                Console.WriteLine("C) Exit");
                string response = Console.ReadLine();
                List<Employee> employees = null;
                switch (response.ToLower())
                {
                    case "a":
                    employees = PeopleFetcher.GetEmployees();
                    break;

                    case "b":
                    Console.WriteLine("Auto generation in progress...");
                    employees = PeopleFetcher.GetFromAPI();
                    break;

                    case "c":
                    Console.WriteLine("Thank you for using my app!");
                    exit = true;
                    break;

                    default:
                    Console.WriteLine("Please enter a valid choice");
                    exit = false;
                    break;
                }

                if(employees != null)
                {
                    Util.PrintEmployees (employees);
                    Util.MakeCSV (employees);
                    Util.MakeBadges (employees);
                }
            }
        }
    }
}
