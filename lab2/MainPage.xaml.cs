using System.Xml.Xsl;
using System.Xml;

namespace lab2
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            GetAllScientists();
        }

        private void GetAllScientists()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(@"C:\Users\melni\OneDrive\Рабочий стол\KNU\xml.xml");
            XmlElement xRoot = doc.DocumentElement;
            XmlNodeList childNodes = xRoot.SelectNodes("scientists");

            for (int i = 0; i < childNodes.Count; i++)
            {
                XmlNode n = childNodes.Item(i);
                if (!FullNamePicker.Items.Contains(n.SelectSingleNode("@FullName").Value))
                {
                    FullNamePicker.Items.Add(n.SelectSingleNode("@FullName").Value);
                }
                if (!FacultyPicker.Items.Contains(n.SelectSingleNode("@Faculty").Value))
                {
                    FacultyPicker.Items.Add(n.SelectSingleNode("@Faculty").Value);
                }
                if (!DepartmentPicker.Items.Contains(n.SelectSingleNode("@Department").Value))
                {
                    DepartmentPicker.Items.Add(n.SelectSingleNode("@Department").Value);
                }
                if (!PositionPicker.Items.Contains(n.SelectSingleNode("@Position").Value))
                {
                    PositionPicker.Items.Add(n.SelectSingleNode("@Position").Value);
                }
                if (!SalaryPicker.Items.Contains(n.SelectSingleNode("@Salary").Value))
                {
                    SalaryPicker.Items.Add(n.SelectSingleNode("@Salary").Value);
                }
                if (!JobExperiencePicker.Items.Contains(n.SelectSingleNode("@JobExperience").Value))
                {
                    JobExperiencePicker.Items.Add(n.SelectSingleNode("@JobExperience").Value);
                }
            }
        }

        private void OnSearchBtnClicked(object sender, EventArgs e)
        {
            editor.Text = string.Empty;

            lab2.Scientists scientists = GetSelectedParameters();
            lab2.IStrategy analyzer = GetSelectedAnalyzer();
            PerformSearch(scientists, analyzer);
        }
        private lab2.Scientists GetSelectedParameters()
        {
            lab2.Scientists scientists = new lab2.Scientists();

            if (FullNameCheckBox.IsChecked)
            {
                scientists.FullName = FullNamePicker.SelectedItem.ToString();
            }
            if (FacultyCheckBox.IsChecked)
            {
                scientists.Faculty = FacultyPicker.SelectedItem.ToString();
            }
            if (DepartmentCheckBox.IsChecked)
            {
                scientists.Department = DepartmentPicker.SelectedItem.ToString();
            }
            if (PositionCheckBox.IsChecked)
            {
                scientists.Position = PositionPicker.SelectedItem.ToString();
            }
            if (SalaryCheckBox.IsChecked)
            {
                scientists.Salary = SalaryPicker.SelectedItem.ToString();
            }
            if (JobExperienceCheckBox.IsChecked)
            {
                scientists.JobExperience = JobExperiencePicker.SelectedItem.ToString();
            }

            return scientists;
        }
        private lab2.IStrategy GetSelectedAnalyzer()
        {
            lab2.IStrategy analyzer = new lab2.Sax(); //за замовчуванням

            if (DomRadioButton.IsChecked)
            {
                analyzer = new lab2.Dom();
            }
            if (LinqRadioButton.IsChecked)
            {
                analyzer = new lab2.Linq();
            }

            return analyzer;
        }
        private void PerformSearch(lab2.Scientists scientists, lab2.IStrategy analyzer)
        {
            lab2.Searcher search = new(scientists, analyzer);
            List<lab2.Scientists> results = search.SearchAlgorithm();
            foreach (lab2.Scientists s in results)
            {
                editor.Text += "П.І.П: " + s.FullName + "\n";
                editor.Text += "Факультет: " + s.Faculty + "\n";
                editor.Text += "Департамент: " + s.Department + "\n";
                editor.Text += "Посада: " + s.Position + "\n";
                editor.Text += "Оклад: " + s.Salary + "\n";
                editor.Text += "Досвід роботи: " + s.JobExperience + "\n";
                editor.Text += "\n";
            }
        }

        private void OnClearBtnClicked(object sender, EventArgs e)
        {
            editor.Text = "";
            SaxRadioButton.IsChecked = false;
            DomRadioButton.IsChecked = false;
            LinqRadioButton.IsChecked = false;
            FullNameCheckBox.IsChecked = false;
            FacultyCheckBox.IsChecked = false;
            DepartmentCheckBox.IsChecked = false;
            PositionCheckBox.IsChecked = false;
            SalaryCheckBox.IsChecked = false;
            JobExperienceCheckBox.IsChecked = false;
            FullNamePicker.SelectedItem = null;
            FacultyPicker.SelectedItem = null;
            DepartmentPicker.SelectedItem = null;
            PositionPicker.SelectedItem = null;
            SalaryPicker.SelectedItem = null;
            JobExperiencePicker.SelectedItem = null;
        }

        private void OnTransformToHTMLBtnClicked(object sender, EventArgs e)
        {
            XslCompiledTransform xct = LoadXSLT();
            string xmlPath = @"C:\Users\melni\OneDrive\Рабочий стол\KNU\xml.xml";
            string htmlPath = @"C:\Users\melni\OneDrive\Рабочий стол\KNU\html.html";

            XsltArgumentList xslArgs = CreateXSLTArguments();

            TransformXMLToHTML(xct, xslArgs, xmlPath, htmlPath);
        }
        private XslCompiledTransform LoadXSLT()
        {
            XslCompiledTransform xct = new XslCompiledTransform();
            xct.Load(@"C:\Users\melni\OneDrive\Рабочий стол\KNU\xsl.xsl");
            return xct;
        }
        private XsltArgumentList CreateXSLTArguments()
        {
            XsltArgumentList xslArgs = new XsltArgumentList();

            string fullName = FullNameCheckBox.IsChecked ? FullNamePicker.SelectedItem.ToString() : null;
            string faculty = FacultyCheckBox.IsChecked ? FacultyPicker.SelectedItem.ToString() : null;
            string department = DepartmentCheckBox.IsChecked ? DepartmentPicker.SelectedItem.ToString() : null;
            string position = PositionCheckBox.IsChecked ? PositionPicker.SelectedItem.ToString() : null;
            string salary = SalaryCheckBox.IsChecked ? SalaryPicker.SelectedItem.ToString() : null;
            string jobExperience = JobExperienceCheckBox.IsChecked ? JobExperiencePicker.SelectedItem.ToString() : null;

            if (fullName != null)
            {
                xslArgs.AddParam("fullName", "", fullName);
            }
            if (faculty != null)
            {
                xslArgs.AddParam("faculty", "", faculty);
            }
            if (department != null)
            {
                xslArgs.AddParam("department", "", department);
            }
            if (position != null)
            {
                xslArgs.AddParam("position", "", position);
            }
            if (salary != null)
            {
                xslArgs.AddParam("salary", "", salary);
            }
            if (jobExperience != null)
            {
                xslArgs.AddParam("jobExperience", "", jobExperience);
            }

            return xslArgs;
        }
        private void TransformXMLToHTML(XslCompiledTransform xct, XsltArgumentList xslArgs, string xmlPath, string htmlPath)
        {
            using (XmlReader xr = XmlReader.Create(xmlPath))
            {
                using (XmlWriter xw = XmlWriter.Create(htmlPath))
                {
                    xct.Transform(xr, xslArgs, xw);
                }
            }
        }
        private async void OnExitBtnClicked(object sender, EventArgs e)
        {
            var result = await Application.Current.MainPage.DisplayAlert("Вихід", "Чи дійсно ви хочете завершити роботу з програмою?", "Так", "Ні");
            if (result)
            {
                System.Diagnostics.Process.GetCurrentProcess().CloseMainWindow();
            }
        }

        private void Editor_TextChanged(object sender, TextChangedEventArgs e)
        {
            int textLength = editor.Text.Length;
            int fontSize = CalculateFontSize(textLength);
            editor.FontSize = fontSize;
        }

        private int CalculateFontSize(int textLength)
        {
            if (textLength < 100)
            {
                return 18;
            }
            else if (textLength < 500)
            {
                return 14;
            }
            else
            {
                return 10;
            }
        }
    }
}