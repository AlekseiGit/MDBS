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
    /// Interaction logic for AnswerWindow.xaml
    /// </summary>
    public partial class AnswerWindow : Window
    {
        public AnswerWindow()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        public string TherapyPlan
        {
            get { return TherapyPlanBox.Text; }
        }

        public Guid ParentMessageId { get; set; }

        public Guid PatientId { get; set; }

        public string PatientName { get; set; }

        public Guid FromId { get; set; }

        public string FromName { get; set; }

        public Guid ToId { get; set; }

        ///<summary>
        /// Ответ филиалу на запрос
        ///</summary>
        private void AnswerMessage_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(this.TherapyPlan))
            {
                MessageBox.Show("План лечения пациента не заполнен!");
                return;
            }

            var core = new CoreFunc();
            core.AnswerMessage(
                this.TherapyPlan,
                this.ParentMessageId,
                this.PatientId,
                this.FromId,
                this.ToId);

            this.DialogResult = true;
        }

        ///<summary>
        /// Предзаполнение формы (От кого, пациент...)
        ///</summary>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.ToBox.Items.Add(FromName);
            this.ToBox.SelectedValue = FromName;
            this.PatientBox.Text = PatientName;
        }
    }
}