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
using DataModels;
using Core;

namespace MDBS_server
{
    /// <summary>
    /// Interaction logic for PatientsWindow.xaml
    /// </summary>
    public partial class PatientsWindow : Window
    {
        List<Patient> Patients = new List<Patient>();
        CoreFunc Core = new CoreFunc();
        Guid UserID;

        public PatientsWindow(Guid userId)
        {
            InitializeComponent();

            UserID = userId;
            Patients = Core.GetPatients(UserID);
            PatientGrid.ItemsSource = Patients;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        public void PatientGridColumnsGenerated(object sender, EventArgs e)
        {
            PatientGrid.Columns[0].Visibility = Visibility.Collapsed;

            PatientGrid.Columns[1].Header = "Пациент";
            PatientGrid.Columns[2].Header = "Пол";
            PatientGrid.Columns[3].Header = "Вес";
            PatientGrid.Columns[4].Header = "Кол-во таблеток";
            PatientGrid.Columns[5].Header = "Дата рождения";
            PatientGrid.Columns[6].Header = "Медицинская карта";
            PatientGrid.Columns[7].Header = "Текущее лечение";
            PatientGrid.Columns[8].Header = "Информация";
            PatientGrid.Columns[9].Header = "Доп. информация";

            PatientGrid.Columns[1].Width = 120;
            PatientGrid.Columns[2].Width = 50;
            PatientGrid.Columns[3].Width = 50;
            PatientGrid.Columns[4].Width = 120;
            PatientGrid.Columns[5].Width = 120;
            PatientGrid.Columns[6].Width = 120;
            PatientGrid.Columns[7].Width = 150;
            PatientGrid.Columns[8].Width = 150;
            PatientGrid.Columns[9].Width = 150;

            DataGridTextColumn curTherapyColumn = PatientGrid.Columns[7] as DataGridTextColumn;
            DataGridTextColumn infoColumn = PatientGrid.Columns[8] as DataGridTextColumn;
            DataGridTextColumn noteColumn = PatientGrid.Columns[9] as DataGridTextColumn;

            Style style = PatientGrid.Resources["wordWrapStyle"] as Style;

            curTherapyColumn.ElementStyle = style;
            curTherapyColumn.EditingElementStyle = style;

            infoColumn.ElementStyle = style;
            infoColumn.EditingElementStyle = style;

            noteColumn.ElementStyle = style;
            noteColumn.EditingElementStyle = style;
        }

        public void NewPatient(object sender, EventArgs e)
        {
            NewPatientWindow newPatientWindow = new NewPatientWindow();

            if (newPatientWindow.ShowDialog() == true)
            {
                MessageBox.Show("Пациент создан!");
                Patients = Core.GetPatients(UserID);
                PatientGrid.ItemsSource = Patients;
            }
            else
            {
            }
        }

        public void EditPatient(object sender, EventArgs e)
        {
            if (PatientGrid.SelectedItems.Count == 1)
            {
                var patient = PatientGrid.SelectedItems[0] as Patient;

                EditPatientWindow editPatientWindow = new EditPatientWindow(patient.ID);

                if (editPatientWindow.ShowDialog() == true)
                {
                    MessageBox.Show("Информация о пациенте изменена!");
                    Patients = Core.GetPatients(UserID);
                    PatientGrid.ItemsSource = Patients;
                }
                else
                {
                }
            }
            else
            {
                MessageBox.Show("Пациент не выбран!");
            }
        }

        public void DeletePatient(object sender, EventArgs e)
        {
            if (PatientGrid.SelectedItems.Count == 1)
            {
                var patient = PatientGrid.SelectedItems[0] as Patient;

                MessageBoxResult result = MessageBox.Show("Удалить выбранного пациента?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    MessageBox.Show("Пациент удален!");
                    Core.DeletePatient(patient.ID);
                    Patients = Core.GetPatients(UserID);
                    PatientGrid.ItemsSource = Patients;
                }
            }
            else
            {
                MessageBox.Show("Пациент не выбран!");
            }
        }
    }
}