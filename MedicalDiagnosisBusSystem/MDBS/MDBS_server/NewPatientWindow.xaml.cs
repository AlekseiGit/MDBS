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
        public NewPatientWindow()
        {
            InitializeComponent();
        }

        public string FullName
        {
            get { return PatientNameBox.Text; }
        }
        public int Sex
        {
            get { return 1; }
        }
        public int Weight
        {
            get { return int.Parse(PatientWeightBox.Text); }
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
        //public string Note { get; set; }

        private void SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            this.BirthDate = PatientBirthDate.SelectedDate.Value;
        }

        private void CreatePatient_Click(object sender, RoutedEventArgs e)
        {
            var core = new CoreFunc();
            core.CreatePatient(
                this.FullName,
                this.Sex,
                this.Weight,
                this.BirthDate,
                this.MedicalCardNumber,
                this.CurrentTherapy,
                this.Info);

            this.DialogResult = true;
        }
    }
}
