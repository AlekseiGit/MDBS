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
        public string DrugsCount
        {
            get { return PatientDrugsCountBox.Text; }
        }
        public DateTime BirthDate { get; set; }
        public string MedicalCardNumber
        {
            get { return PatientCardBox.Text; }
        }
        public string CurrentTherapy
        {
            get { return PatientCurrentTherapyBox.Text; }
        }
        public string Info
        {
            get { return PatientInfoBox.Text; }
        }
        public string Note
        {
            get { return PatientNoteBox.Text; }
        }

        ///<summary>
        /// Вызов формы редактирования выбранного пациента
        ///</summary>
        public EditPatientWindow(Guid patientId)
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;

            PatientId = patientId;
            var patientInfo = Core.GetPatientInfo(patientId);

            //PatientNameBox.Text = patientInfo.FullName;
            PatientCardBox.Text = patientInfo.MedicalCardNumber;
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
            PatientCurrentTherapyBox.Text = patientInfo.CurrentTherapy;
            PatientDrugsCountBox.Text = patientInfo.DrugsCount;
            PatientInfoBox.Text = patientInfo.Info;
            PatientNoteBox.Text = patientInfo.Note;
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

            Core.EditPatientInfo(
                PatientId,
                this.Sex,
                this.Weight,
                this.DrugsCount,
                this.BirthDate,
                this.MedicalCardNumber,
                this.CurrentTherapy,
                this.Info,
                this.Note);

            this.DialogResult = true;
        }
    }
}