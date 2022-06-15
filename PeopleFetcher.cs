using System;
using System.Collections.Generic;
using System.Net;
using Newtonsoft.Json.Linq;

namespace CatWorx.BadgeMaker
{
    class PeopleFetcher
    {
        public static List<Employee> GetEmployees()
        {
            List<Employee> employees = new List<Employee>();
            while (true)
            {
                // Move the initial prompt inside the loop, so it repeats for each employee
                Console.WriteLine("Enter first name (leave empty to exit): ");

                // change input to firstName
                string firstName = Console.ReadLine();
                if (firstName == "")
                {
                    break;
                }

                // add a Console.ReadLine() for each value
                Console.Write("Enter last name: ");
                string lastName = Console.ReadLine();
                Console.Write("Enter ID: ");
                int id = Int32.Parse(Console.ReadLine());
                Console.Write("Enter Photo URL:");
                string photoUrl = Console.ReadLine();
                Employee currentEmployee =
                    new Employee(firstName, lastName, id, photoUrl);
                employees.Add (currentEmployee);
            }

            return employees;
        }

        public static List<Employee> GetFromAPI()
        {
            List<Employee> employees = new List<Employee>();

            using (WebClient client = new WebClient())
            {
                // Image example
                string response =
                    client
                        .DownloadString("https://randomuser.me/api/?results=10&nat=us&inc=name,id,picture");

                JObject obj = JObject.Parse(response);
                JArray jarr = (JArray) obj["results"];
                foreach (var token in jarr)
                {
                    Employee emp =
                        new Employee(token.SelectToken("name.first").ToString(),
                            token.SelectToken("name.last").ToString(),
                            Int32
                                .Parse(token
                                    .SelectToken("id.value")
                                    .ToString()
                                    .Replace("-", "")),
                            token.SelectToken("picture.large").ToString());
                    employees.Add (emp);
                }
            }

            return employees;
        }
    }
}
