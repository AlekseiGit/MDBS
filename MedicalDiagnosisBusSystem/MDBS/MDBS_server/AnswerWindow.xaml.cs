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

        private void AnswerMessage_Click(object sender, RoutedEventArgs e)
        {
            var core = new CoreFunc();
            core.AnswerMessage(
                this.TherapyPlan,
                this.ParentMessageId,
                this.PatientId,
                this.FromId,
                this.ToId);
                //new Guid("EDEA2C24-6EE0-4F08-AFA1-AB063B6E8A91"), //ParentMessageId
                //new Guid("D2E8DE05-D963-4FBD-B6B9-257C60FB31DB"), //Patient
                //new Guid("5A239C9B-E404-4AF3-A7BD-8D1C4925781D"), //user_id
                //new Guid("C16B50F6-7588-43D2-BAC5-08819FB3FA11")); //to_id

            this.DialogResult = true;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.ToBox.Items.Add(FromName);
            this.ToBox.SelectedValue = FromName;
            this.PatientBox.Text = PatientName;
        }
    }
}