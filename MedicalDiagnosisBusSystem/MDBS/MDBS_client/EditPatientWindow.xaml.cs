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
    /// Interaction logic for EditPatientWindow.xaml
    /// </summary>
    public partial class EditPatientWindow : Window
    {
        CoreFunc Core = new CoreFunc();
        Guid PatientId;

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
        public string Complaints
        {
            get { return PatientComplaintsBox.Text; }
        }
        public string Info
        {
            get { return PatientInfoBox.Text; }
        }

        ///<summary>
        /// Вызов формы редактирования выбранного пациента
        ///</summary>
        public EditPatientWindow(Guid patientId, bool editable)
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;

            PatientId = patientId;
            var patientInfo = Core.GetPatientInfo(patientId);

            //PatientNameBox.Text = patientInfo.FullName;
            PatientCardBoxPre.Text = patientInfo.MedicalCardNumber.Substring(0, 5);
            PatientCardBox.Text = patientInfo.MedicalCardNumber.Substring(5, 4);
            if (patientInfo.Sex == "M")
            {
                PatientSexBox.SelectedIndex = 0;
            }
            else if (patientInfo.Sex == "Ж")
            {
                PatientSexBox.SelectedIndex = 1;
            }
            PatientBirthDate.SelectedDate = DateTime.ParseExact(patientInfo.BirthDate, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
            PatientWeightBox.Text = patientInfo.Weight.ToString();
            PatientUsedDrugsBox.Text = patientInfo.UsedDrugs;
            PatientRemissionPeriodBox.Text = patientInfo.RemissionPeriod;
            PatientVisitDate.SelectedDate = DateTime.ParseExact(patientInfo.VisitDate, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
            PatientLastExacerbation.SelectedDate = DateTime.ParseExact(patientInfo.LastExacerbation, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
            PatientAppliedTherapyBox.Text = patientInfo.AppliedTherapy;
            PatientSurveyResultsBox.Text = patientInfo.SurveyResults;
            PatientComplaintsBox.Text = patientInfo.Complaints;
            PatientInfoBox.Text = patientInfo.Info;

            if (editable == false)
            {
                Title = "Информация о пациенте";

                PatientCardBox.IsReadOnly = true;
                PatientSexBox.IsEnabled = false;
                PatientBirthDate.IsEnabled = false;
                PatientWeightBox.IsReadOnly = true;
                PatientUsedDrugsBox.IsReadOnly = true;
                PatientRemissionPeriodBox.IsReadOnly = true;
                PatientVisitDate.IsEnabled = false;
                PatientLastExacerbation.IsEnabled = false;
                PatientAppliedTherapyBox.IsReadOnly = true;
                PatientSurveyResultsBox.IsReadOnly = true;
                PatientComplaintsBox.IsReadOnly = true;
                PatientInfoBox.IsReadOnly = true;

                SaveBtn.Visibility = Visibility.Hidden;
            }
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
        /// Обновление информации по выбранному пациенту
        ///</summary>
        private void UpdatePatient_Click(object sender, RoutedEventArgs e)
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

            Core.EditPatientInfo(
                PatientId,
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
                this.Complaints,
                this.Info);

            this.DialogResult = true;
        }
    }
}