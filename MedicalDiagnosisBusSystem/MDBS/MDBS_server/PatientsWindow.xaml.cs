﻿using System;
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
        public PatientsWindow()
        {
            InitializeComponent();

            List<Patient> patients = new List<Patient>();
            var core = new CoreFunc();
            patients = core.GetPatients();
            PatientGrid.ItemsSource = patients;
        }

        public void PatientGridColumnsGenerated(object sender, EventArgs e)
        {
            PatientGrid.Columns[0].Visibility = Visibility.Collapsed;

            PatientGrid.Columns[1].Header = "Пациент";
            PatientGrid.Columns[2].Header = "Пол";
            PatientGrid.Columns[3].Header = "Вес";
            PatientGrid.Columns[4].Header = "Дата рождения";
            PatientGrid.Columns[5].Header = "Медицинская карта";
            PatientGrid.Columns[6].Header = "Текущее лечение";
            PatientGrid.Columns[7].Header = "Информация";
            PatientGrid.Columns[8].Header = "Доп. информация";

            PatientGrid.Columns[1].Width = 120;
            PatientGrid.Columns[2].Width = 50;
            PatientGrid.Columns[3].Width = 50;
            PatientGrid.Columns[4].Width = 120;
            PatientGrid.Columns[5].Width = 120;
            PatientGrid.Columns[6].Width = 150;
            PatientGrid.Columns[7].Width = 150;
            PatientGrid.Columns[8].Width = 150;
        }
    }
}