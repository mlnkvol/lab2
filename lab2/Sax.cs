using System.Xml;
using System.Xml.Linq;

namespace lab2
{
    internal class Sax : IStrategy
    {
        public List<Scientists> Search(Scientists scientists)
        {
            List<Scientists> results = new List<Scientists>();
            XmlTextReader xmlReader = new XmlTextReader(@"C:\Users\melni\OneDrive\Рабочий стол\KNU\xml.xml");
            while (xmlReader.Read())
            {
                if (xmlReader.HasAttributes)
                {
                    while (xmlReader.MoveToNextAttribute())
                    {
                        string fullName = string.Empty;
                        string faculty = string.Empty;
                        string department = string.Empty;
                        string position = string.Empty;
                        string salary = string.Empty;
                        string jobExperience = string.Empty;

                        if (xmlReader.Name.Equals("FullName") && (xmlReader.Value.Equals(scientists.FullName) || scientists.FullName == null))
                        {
                            fullName = xmlReader.Value;
                            xmlReader.MoveToNextAttribute();
                            if (xmlReader.Name.Equals("Faculty") && (xmlReader.Value.Equals(scientists.Faculty) || scientists.Faculty == null))
                            {
                                faculty = xmlReader.Value;
                                xmlReader.MoveToNextAttribute();
                                if (xmlReader.Name.Equals("Department") && (xmlReader.Value.Equals(scientists.Department) || scientists.Department == null))
                                {
                                    department = xmlReader.Value;
                                    xmlReader.MoveToNextAttribute();
                                    if (xmlReader.Name.Equals("Position") && (xmlReader.Value.Equals(scientists.Position) || scientists.Position == null))
                                    {
                                        position = xmlReader.Value;
                                        xmlReader.MoveToNextAttribute();
                                        if (xmlReader.Name.Equals("Salary") && (xmlReader.Value.Equals(scientists.Salary) || scientists.Salary == null))
                                        {
                                            salary = xmlReader.Value;
                                            xmlReader.MoveToNextAttribute();
                                            if (xmlReader.Name.Equals("JobExperience") && (xmlReader.Value.Equals(scientists.JobExperience) || scientists.JobExperience == null))
                                            {
                                                jobExperience = xmlReader.Value;
                                            }
                                        }
                                    }
                                }
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
                }
            }
            xmlReader.Close();
            return results;
        }
    }
}