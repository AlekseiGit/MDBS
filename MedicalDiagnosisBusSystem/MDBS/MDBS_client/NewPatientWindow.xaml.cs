using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MDBS_server
{
    /// <summary>
    /// Interaction logic for NewPatientWindow.xaml
    /// </summary>
    public partial class NewPatientWindow : Window
    {
        /*public string FullName
        {
            get { return PatientNameBox.Text; }
        }*/
        public int Sex
        {
            get
            {
                if (PatientSexBox.Text == "М")
                    return 1;
                else
                    return 2;
            }
        }
        public int Weight
        {
            get
            {
                int weight = 0;

                if (int.TryParse(PatientWeightBox.Text, out weight) == true)
                    return weight;
                else
                    return 0;
            }
        }
        public DateTime BirthDate { get; set; }
        public string MedicalCardNumber
        {
            get { return PatientCardBox.Text; }
        }
        public DateTime VisitDate { get; set; }
        public string UsedDrugs
        {
            get { return PatientUsedDrugsBox.Text; }
        }
        public string RemissionPeriod
        {
            get { return PatientRemissionPeriodBox.Text; }
        }
        public DateTime LastExacerbation { get; set; }
        public string AppliedTherapy
        {
            get { return PatientAppliedTherapyBox.Text; }
        }
        public string SurveyResults
        {
            get { return PatientSurveyResultsBox.Text; }
        }
        public string Info
        {
            get { return PatientInfoBox.Text; }
        }
        public string SName
        {
            get { return PatientSNameBox.Text; }
        }
        public string FName
        {
            get { return PatientFNameBox.Text; }
        }
        public string MName
        {
            get { return PatientMNameBox.Text; }
        }

        public NewPatientWindow(string docNumber)
        {
            InitializeComponent();
            //PatientBirthDate.SelectedDate = DateTime.Today;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            PatientCardBoxPre.Text = docNumber.Substring(0, docNumber.Length - 2);
        }

        ///<summary>
        /// Сохранение выбранной даты в переменную (вспомогательный обработчик)
        ///</summary>
        private void SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (PatientBirthDate.SelectedDate.Value != null)
                this.BirthDate = PatientBirthDate.SelectedDate.Value;
            else
                this.BirthDate = DateTime.Now;
        }

        private void SelectedIllStartDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (PatientVisitDate.SelectedDate.Value != null)
                this.VisitDate = PatientVisitDate.SelectedDate.Value;
            else
                this.VisitDate = DateTime.Now;
        }

        private void SelectedLastExacerbationDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (PatientLastExacerbation.SelectedDate.Value != null)
                this.LastExacerbation = PatientLastExacerbation.SelectedDate.Value;
            else
                this.LastExacerbation = DateTime.Now;
        }

        ///<summary>
        /// Создание нового пациента
        ///</summary>
        private void CreatePatient_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(this.MedicalCardNumber))
            {
                MessageBox.Show("Номер карты пациента не заполнен!");
                return;
            }
            if (this.MedicalCardNumber.Length != 4)
            {
                MessageBox.Show("Номер карты пациента должен состоять из префикса <" + PatientCardBoxPre.Text + "> и 4 цифр порядкового номера!");
                return;
            }
            if (string.IsNullOrEmpty(this.SName))
            {
                MessageBox.Show("Фамилия пациента не заполнена!");
                return;
            }
            if (string.IsNullOrEmpty(this.FName))
            {
                MessageBox.Show("Имя пациента не заполнено!");
                return;
            }
            if (string.IsNullOrEmpty(this.MName))
            {
                MessageBox.Show("Отчество пациента не заполнено!");
                return;
            }
            if (this.PatientBirthDate.SelectedDate == null)
            {
                MessageBox.Show("Дата рождения пациента не заполнена!");
                return;
            }
            if (string.IsNullOrEmpty(this.PatientWeightBox.Text))
            {
                MessageBox.Show("Вес пациента не заполнен!");
                return;
            }
            if (this.PatientVisitDate.SelectedDate == null)
            {
                MessageBox.Show("Поле \"Дата обращения\" не заполнено!");
                return;
            }
            if (string.IsNullOrEmpty(this.PatientUsedDrugsBox.Text))
            {
                MessageBox.Show("Поле \"Когда заболел, чем лечился\" не заполнено!");
                return;
            }
            if (string.IsNullOrEmpty(this.PatientRemissionPeriodBox.Text))
            {
                MessageBox.Show("Поле \"Период ремиссии после лечения\" не заполнено!");
                return;
            }
            if (this.PatientLastExacerbation.SelectedDate == null)
            {
                MessageBox.Show("Поле \"Последнее обострение\" не заполнено!");
                return;
            }
            if (string.IsNullOrEmpty(this.PatientAppliedTherapyBox.Text))
            {
                MessageBox.Show("Поле \"Проведенное лечение\" не заполнено!");
                return;
            }

            var core = new CoreFunc();

            core.CreatePatient(
                this.Sex,
                this.Weight,
                this.BirthDate,
                PatientCardBoxPre.Text + this.MedicalCardNumber,
                this.VisitDate,
                this.UsedDrugs,
                this.RemissionPeriod,
                this.LastExacerbation,
                this.AppliedTherapy,
                this.SurveyResults,
                this.Info,
                this.SName + this.FName + this.MName + "_" + this.BirthDate.ToString("yyyyMMdd"));

            this.DialogResult = true;
        }
    }
}