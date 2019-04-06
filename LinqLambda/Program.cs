using System;
using System.Globalization;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using LinqLambda.Entities;

namespace LinqLambda
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter full file path: ");
            string patch = Console.ReadLine();

            List<Employee> list = new List<Employee>();
            try
            {
                using (StreamReader sr = File.OpenText(patch))
                {
                    while (!sr.EndOfStream)
                    {
                        string[] fileds = sr.ReadLine().Split(',');
                        string name = fileds[0];
                        string email = fileds[1];
                        double salary = double.Parse(fileds[2], CultureInfo.InvariantCulture);
                        list.Add(new Employee(name, email, salary));
                    }
                }

                Console.Write("Enter salary: ");
                double inSalary = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);

                //Mostrar, em ordem alfabética, o email dos funcionários cujo salário seja superior a um dado valor fornecido pelo usuário.
                var personEmail = list.Where(p => p.Salary > inSalary).OrderBy(p => p.Email).Select(p => p.Email).DefaultIfEmpty();
                Console.WriteLine("Email of people whose salary is more than " + inSalary.ToString("F2", CultureInfo.InvariantCulture) + ":");
                foreach (string email in personEmail)
                {
                    Console.WriteLine(email);
                }

                //Mostrar soma dos salários dos funcionários cujo nome começa com a letra 'M'.
                var sumPeopleSalary = list.Where(p => p.Name[0] == 'M').Sum( p => p.Salary);
                Console.WriteLine("Sum of salary of people whose name starts with 'M': " + sumPeopleSalary.ToString("F2", CultureInfo.InvariantCulture));
            }
            catch (IOException e)
            {
                Console.WriteLine("An error ocurred:");
                Console.WriteLine(e.Message);
            }

        }
    }
}
