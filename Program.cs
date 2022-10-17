using System.Globalization;

namespace TestProj5Linq{
    class Program{
        static void Main(string[] args){
            string fileName;
            double salary;
            List<Employee> employees = new List<Employee>();

            // Reading the file
            System.Console.Write("Enter the name of file: ");
            fileName = Console.ReadLine()!;
            try{
                using(StreamReader sr = File.OpenText(fileName)){
                    while(!sr.EndOfStream){
                        string[] line = sr.ReadLine()!.Split(',');
                        // line[0] = name, line[1] = email, line[2] = salary
                        employees.Add(new Employee(line[0], line[1], double.Parse(line[2], CultureInfo.InvariantCulture)));
                    }
                }
            }
            catch(IOException e){
                System.Console.WriteLine(e.Message);
            }
            
            // Choosing the salary to be compared
            System.Console.Write("Enter salary: ");
            salary = double.Parse(Console.ReadLine()!, CultureInfo.InvariantCulture);
            var emailPeopleSalaryBiggerthan =
                from p in employees
                where p.Salary > salary
                orderby p.Email
                select p.Email;
            foreach(string emp in emailPeopleSalaryBiggerthan){
                System.Console.WriteLine(emp);
            }

            System.Console.Write("Sum of salary of people whose name starts with 'M': ");
            var salaryPeopleStartWithM = 
                from p in employees
                where p.Name[0] == 'M'
                select p.Salary;
            var sum = salaryPeopleStartWithM.Sum();
            System.Console.Write(sum.ToString("F2", CultureInfo.InvariantCulture));
        }
    }
}