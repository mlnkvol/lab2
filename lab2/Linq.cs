using System.Xml.Linq;

namespace lab2
{
    internal class Linq : IStrategy
    {
        public List<Scientists> Search(Scientists scientists)
        {
            List<Scientists> results = new List<Scientists>();
            var doc = XDocument.Load(@"C:\Users\melni\OneDrive\Рабочий стол\KNU\xml.xml");
            var result = from obj in doc.Descendants("scientists")
                         where
                         (
                         (obj.Attribute("FullName").Value.Equals(scientists.FullName) || scientists.FullName == null) &&
                         (obj.Attribute("Faculty").Value.Equals(scientists.Faculty) || scientists.Faculty == null) &&
                         (obj.Attribute("Department").Value.Equals(scientists.Department) || scientists.Department == null) &&
                         (obj.Attribute("Position").Value.Equals(scientists.Position) || scientists.Position == null) &&
                         (obj.Attribute("Salary").Value.Equals(scientists.Salary) || scientists.Salary == null) &&
                         (obj.Attribute("JobExperience").Value.Equals(scientists.JobExperience) || scientists.JobExperience == null) 
                         )
                         select new
                         {
                             fullname = (string)obj.Attribute("FullName"),
                             faculty = (string)obj.Attribute("Faculty"),
                             department = (string)obj.Attribute("Department"),
                             position = (string)obj.Attribute("Position"),
                             salary = (string)obj.Attribute("Salary"),
                             jobExperience = (string)obj.Attribute("JobExperience")
                         };

            foreach (var s in result)
            {
                Scientists myScientists = new Scientists();
                myScientists.FullName = s.fullname;
                myScientists.Faculty = s.faculty;
                myScientists.Department = s.department;
                myScientists.Position = s.position;
                myScientists.Salary = s.salary;
                myScientists.JobExperience = s.jobExperience;
                results.Add(myScientists);
            }
            return results;
        }
    }
}