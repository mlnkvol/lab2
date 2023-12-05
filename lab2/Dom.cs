using System.Xml;

namespace lab2
{
    internal class Dom : IStrategy
    {
        public List<Scientists> Search(Scientists scientists)
        {
            List<Scientists> results = new List<Scientists>();
            XmlDocument doc = new XmlDocument();
            doc.Load(@"C:\Users\melni\OneDrive\Рабочий стол\KNU\xml.xml");
            XmlNode node = doc.DocumentElement;
            foreach (XmlNode n in node.ChildNodes)
            {
                string fullName = string.Empty;
                string faculty = string.Empty;
                string department = string.Empty;
                string position = string.Empty;
                string salary = string.Empty;
                string jobExperience = string.Empty;

                foreach (XmlAttribute attribute in n.Attributes)
                {
                    if (attribute.Name.Equals("FullName") && (attribute.Value.Equals(scientists.FullName) || scientists.FullName == null))
                    {
                        fullName = attribute.Value;
                    }
                    if (attribute.Name.Equals("Faculty") && (attribute.Value.Equals(scientists.Faculty) || scientists.Faculty == null))
                    {
                        faculty = attribute.Value;
                    }
                    if (attribute.Name.Equals("Department") && (attribute.Value.Equals(scientists.Department) || scientists.Department == null))
                    {
                        department = attribute.Value;
                    }
                    if (attribute.Name.Equals("Position") && (attribute.Value.Equals(scientists.Position) || scientists.Position == null))
                    {
                        position = attribute.Value;
                    }
                    if (attribute.Name.Equals("Salary") && (attribute.Value.Equals(scientists.Salary) || scientists.Salary == null))
                    {
                        salary = attribute.Value;
                    }
                    if (attribute.Name.Equals("JobExperience") && (attribute.Value.Equals(scientists.JobExperience) || scientists.JobExperience == null))
                    {
                        jobExperience = attribute.Value;
                    }
                }

                if (fullName != "" && faculty != "" && department != "" && position != "" && salary != "" && jobExperience != "")
                {
                    Scientists myScientists = new Scientists();
                    myScientists.FullName = fullName;
                    myScientists.Faculty = faculty;
                    myScientists.Department = department;
                    myScientists.Position = position;
                    myScientists.Salary = salary;
                    myScientists.JobExperience = jobExperience;
                    results.Add(myScientists);
                }
            }
            return results;
        }
    }
}