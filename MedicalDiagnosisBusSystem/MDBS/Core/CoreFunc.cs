using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModels;

namespace Core
{
    public class CoreFunc
    {
        public static string ConnectionString = @"Data Source=ALEX-PC\SQLEXPRESS;Initial Catalog=MDBS;Integrated Security=SSPI;";
        public SqlConnection DBConnection;

        public CoreFunc()
        {
            DBConnection = new SqlConnection(ConnectionString);
        }

        public List<Message> GetIncomingMessages()
        {
            List<Message> messages = new List<Message>();

            SqlCommand sqlCmd = new SqlCommand();

            sqlCmd.CommandText = "dbo.p_get_incoming_messages";
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Connection = DBConnection;

            var userId = new SqlParameter("@user_id", "5A239C9B-E404-4AF3-A7BD-8D1C4925781D");

            sqlCmd.Parameters.Add(userId);

            DBConnection.Open();

            DataTable table = new DataTable();
            table.Load(sqlCmd.ExecuteReader());

            foreach (DataRow row in table.Rows)
            {
                Message msg = new Message();

                msg.ID = (Guid)row["ID"];
                //msg.Info = row["Info"].ToString();
                //msg.Diagnosis = row["Diagnosis"].ToString();
                //msg.TherapyPlan = row["TherapyPlan"].ToString();
                //msg.ParentMessageID = (Guid)row["ParentMessageID"];
                //msg.PatientID = (Guid)row["PatientID"];
                msg.PatientName = row["PatientName"].ToString();
                //msg.From = (Guid)row["From"];
                //msg.To = (Guid)row["To"];
                msg.FromName = row["FromName"].ToString();
                msg.MessageDate = ((DateTime)row["MessageDate"]).ToString("yyyy-MM-dd   HH:mm");
                msg.Status = (int)row["Status"];

                messages.Add(msg);
            }

            DBConnection.Close();

            return messages;
        }

        public List<Message> GetOutgoingMessages()
        {
            List<Message> messages = new List<Message>();

            SqlCommand sqlCmd = new SqlCommand();

            sqlCmd.CommandText = "dbo.p_get_outgoing_messages";
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Connection = DBConnection;

            var userId = new SqlParameter("@user_id", "5A239C9B-E404-4AF3-A7BD-8D1C4925781D");

            sqlCmd.Parameters.Add(userId);

            DBConnection.Open();

            DataTable table = new DataTable();
            table.Load(sqlCmd.ExecuteReader());

            foreach (DataRow row in table.Rows)
            {
                Message msg = new Message();

                msg.ID = (Guid)row["ID"];
                //msg.Info = row["Info"].ToString();
                //msg.Diagnosis = row["Diagnosis"].ToString();
                //msg.TherapyPlan = row["TherapyPlan"].ToString();
                //msg.ParentMessageID = (Guid)row["ParentMessageID"];
                //msg.PatientID = (Guid)row["PatientID"];
                msg.PatientName = row["PatientName"].ToString();
                //msg.From = (Guid)row["From"];
                //msg.To = (Guid)row["To"];
                msg.FromName = row["FromName"].ToString();
                msg.MessageDate = ((DateTime)row["MessageDate"]).ToString("yyyy-MM-dd   HH:mm");
                msg.Status = (int)row["Status"];

                messages.Add(msg);
            }

            DBConnection.Close();

            return messages;
        }

        public List<Message> GetNeedAnswerMessages()
        {
            List<Message> messages = new List<Message>();

            SqlCommand sqlCmd = new SqlCommand();

            sqlCmd.CommandText = "dbo.p_get_need_answer_messages";
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Connection = DBConnection;

            var userId = new SqlParameter("@user_id", "5A239C9B-E404-4AF3-A7BD-8D1C4925781D");

            sqlCmd.Parameters.Add(userId);

            DBConnection.Open();

            DataTable table = new DataTable();
            table.Load(sqlCmd.ExecuteReader());

            foreach (DataRow row in table.Rows)
            {
                Message msg = new Message();

                msg.ID = (Guid)row["ID"];
                //msg.Info = row["Info"].ToString();
                //msg.Diagnosis = row["Diagnosis"].ToString();
                //msg.TherapyPlan = row["TherapyPlan"].ToString();
                //msg.ParentMessageID = (Guid)row["ParentMessageID"];
                //msg.PatientID = (Guid)row["PatientID"];
                msg.PatientName = row["PatientName"].ToString();
                //msg.From = (Guid)row["From"];
                //msg.To = (Guid)row["To"];
                msg.FromName = row["FromName"].ToString();
                msg.MessageDate = ((DateTime)row["MessageDate"]).ToString("yyyy-MM-dd   HH:mm");
                msg.Status = (int)row["Status"];

                messages.Add(msg);
            }

            DBConnection.Close();

            return messages;
        }

        public List<Message> GetArchiveMessages()
        {
            List<Message> messages = new List<Message>();

            SqlCommand sqlCmd = new SqlCommand();

            sqlCmd.CommandText = "dbo.p_get_archive_messages";
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Connection = DBConnection;

            var userId = new SqlParameter("@user_id", "5A239C9B-E404-4AF3-A7BD-8D1C4925781D");

            sqlCmd.Parameters.Add(userId);

            DBConnection.Open();

            DataTable table = new DataTable();
            table.Load(sqlCmd.ExecuteReader());

            foreach (DataRow row in table.Rows)
            {
                Message msg = new Message();

                msg.ID = (Guid)row["ID"];
                //msg.Info = row["Info"].ToString();
                //msg.Diagnosis = row["Diagnosis"].ToString();
                //msg.TherapyPlan = row["TherapyPlan"].ToString();
                //msg.ParentMessageID = (Guid)row["ParentMessageID"];
                //msg.PatientID = (Guid)row["PatientID"];
                msg.PatientName = row["PatientName"].ToString();
                //msg.From = (Guid)row["From"];
                //msg.To = (Guid)row["To"];
                msg.FromName = row["FromName"].ToString();
                msg.MessageDate = ((DateTime)row["MessageDate"]).ToString("yyyy-MM-dd   HH:mm");
                msg.Status = (int)row["Status"];

                messages.Add(msg);
            }

            DBConnection.Close();

            return messages;
        }

        public List<Dialog> GetDialog(Guid message_id)
        {
            List<Dialog> dialog = new List<Dialog>();

            SqlCommand sqlCmd = new SqlCommand();

            sqlCmd.CommandText = "dbo.p_get_dialog";
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Connection = DBConnection;

            var messageId = new SqlParameter("@message_id", message_id);

            sqlCmd.Parameters.Add(messageId);

            DBConnection.Open();

            DataTable table = new DataTable();
            table.Load(sqlCmd.ExecuteReader());

            foreach (DataRow row in table.Rows)
            {
                Dialog dlg = new Dialog();

                dlg.ID = (Guid)row["ID"];
                dlg.Info = row["Info"].ToString();
                dlg.Diagnosis = row["Diagnosis"].ToString();
                dlg.TherapyPlan = row["TherapyPlan"].ToString();
                dlg.PatientID = (Guid)row["PatientID"];
                dlg.PatientName = row["PatientName"].ToString();
                dlg.From = (Guid)row["From"];
                dlg.FromName = row["FromName"].ToString();
                dlg.MessageDate = ((DateTime)row["MessageDate"]).ToString("yyyy-MM-dd   HH:mm");

                dialog.Add(dlg);
            }

            DBConnection.Close();

            return dialog;
        }

        /*
        public class Image
        {
            public Image(int id, string filename, string title, byte[] data)
            {
                Id = id;
                FileName = filename;
                Title = title;
                Data = data;
            }
            public int Id { get; private set; }
            public string FileName { get; private set; }
            public string Title { get; private set; }
            public byte[] Data { get; private set; }
        }
        */

        public void SendMessage(
            string info,
            string diagnosis,
            string patientNumber,
            Guid userId,
            Guid toId,
            string img_one,
            string img_two,
            string img_three,
            string img_four,
            string img_five)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.CommandText = "dbo.p_send_message";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@info", SqlDbType.NVarChar, 4000);
                cmd.Parameters.Add("@diagnosis", SqlDbType.NVarChar, 4000);
                cmd.Parameters.Add("@patient_number", SqlDbType.NVarChar, 100);
                cmd.Parameters.Add("@user_id", SqlDbType.UniqueIdentifier);
                cmd.Parameters.Add("@to_id", SqlDbType.UniqueIdentifier);

                cmd.Parameters["@info"].Value = info;
                cmd.Parameters["@diagnosis"].Value = diagnosis;
                cmd.Parameters["@patient_number"].Value = patientNumber;
                cmd.Parameters["@user_id"].Value = userId;
                cmd.Parameters["@to_id"].Value = toId;

                byte[] imageData_one;
                byte[] imageData_two;
                byte[] imageData_three;
                byte[] imageData_four;
                byte[] imageData_five;

                if (img_one != "...")
                {
                    using (System.IO.FileStream fs = new System.IO.FileStream(img_one, FileMode.Open))
                    {
                        imageData_one = new byte[fs.Length];
                        fs.Read(imageData_one, 0, imageData_one.Length);

                        cmd.Parameters.Add("@img_one", SqlDbType.Image, 1000000);
                        cmd.Parameters.Add("@img_one_send", SqlDbType.Int);
                        cmd.Parameters["@img_one"].Value = imageData_one;
                        cmd.Parameters["@img_one_send"].Value = 1;
                    }
                }

                if (img_two != "...")
                {
                    using (System.IO.FileStream fs = new System.IO.FileStream(img_two, FileMode.Open))
                    {
                        imageData_two = new byte[fs.Length];
                        fs.Read(imageData_two, 0, imageData_two.Length);

                        cmd.Parameters.Add("@img_two", SqlDbType.Image, 1000000);
                        cmd.Parameters.Add("@img_two_send", SqlDbType.Int);
                        cmd.Parameters["@img_two"].Value = imageData_two;
                        cmd.Parameters["@img_two_send"].Value = 1;
                    }
                }

                if (img_three != "...")
                {
                    using (System.IO.FileStream fs = new System.IO.FileStream(img_three, FileMode.Open))
                    {
                        imageData_three = new byte[fs.Length];
                        fs.Read(imageData_three, 0, imageData_three.Length);

                        cmd.Parameters.Add("@img_three", SqlDbType.Image, 1000000);
                        cmd.Parameters.Add("@img_three_send", SqlDbType.Int);
                        cmd.Parameters["@img_three"].Value = imageData_three;
                        cmd.Parameters["@img_three_send"].Value = 1;
                    }
                }

                if (img_four != "...")
                {
                    using (System.IO.FileStream fs = new System.IO.FileStream(img_four, FileMode.Open))
                    {
                        imageData_four = new byte[fs.Length];
                        fs.Read(imageData_four, 0, imageData_four.Length);

                        cmd.Parameters.Add("@img_four", SqlDbType.Image, 1000000);
                        cmd.Parameters.Add("@img_four_send", SqlDbType.Int);
                        cmd.Parameters["@img_four"].Value = imageData_four;
                        cmd.Parameters["@img_four_send"].Value = 1;
                    }
                }

                if (img_five != "...")
                {
                    using (System.IO.FileStream fs = new System.IO.FileStream(img_five, FileMode.Open))
                    {
                        imageData_five = new byte[fs.Length];
                        fs.Read(imageData_five, 0, imageData_five.Length);

                        cmd.Parameters.Add("@img_five", SqlDbType.Image, 1000000);
                        cmd.Parameters.Add("@img_five_send", SqlDbType.Int);
                        cmd.Parameters["@img_five"].Value = imageData_five;
                        cmd.Parameters["@img_five_send"].Value = 1;
                    }
                }

                cmd.ExecuteNonQuery();

                /*
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandText = @"INSERT INTO mdbs.dbo.Attachments (MessageID, Data, Comment) VALUES (@MessageID, @Data, @Comment)";
                command.Parameters.Add("@MessageID", SqlDbType.UniqueIdentifier);
                command.Parameters.Add("@Data", SqlDbType.Image, 1000000);
                command.Parameters.Add("@Comment", SqlDbType.NVarChar, 100);

                string filename = @"C:\DBI\j.jpg";
                string title = "";
                string shortFileName = filename.Substring(filename.LastIndexOf('\\') + 1);
                byte[] imageData;
                using (System.IO.FileStream fs = new System.IO.FileStream(filename, FileMode.Open))
                {
                    imageData = new byte[fs.Length];
                    fs.Read(imageData, 0, imageData.Length);
                }
                command.Parameters["@MessageID"].Value = new Guid("31D366B4-BCE2-E711-9554-A9E995E70B55");
                command.Parameters["@Data"].Value = imageData;
                command.Parameters["@Comment"].Value = title;

                command.ExecuteNonQuery();

                connection.Close();
                */
            }
        }

        public void AnswerMessage(
            string therapyPlan,
            Guid parentMessageId,
            Guid patientId,
            Guid userId,
            Guid toId)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.CommandText = "dbo.p_answer_message";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@therapy_plan", SqlDbType.NVarChar, 4000);
                cmd.Parameters.Add("@parent_message_id", SqlDbType.UniqueIdentifier);
                cmd.Parameters.Add("@patient_id", SqlDbType.UniqueIdentifier);
                cmd.Parameters.Add("@user_id", SqlDbType.UniqueIdentifier);
                cmd.Parameters.Add("@to_id", SqlDbType.UniqueIdentifier);

                cmd.Parameters["@therapy_plan"].Value = therapyPlan;
                cmd.Parameters["@parent_message_id"].Value = parentMessageId;
                cmd.Parameters["@patient_id"].Value = patientId;
                cmd.Parameters["@user_id"].Value = userId;
                cmd.Parameters["@to_id"].Value = toId;

                cmd.ExecuteNonQuery();
            }
        }

        public Patient GetPatientInfo(Guid patientId)
        {
            Patient patientInfo = new Patient();

            SqlCommand sqlCmd = new SqlCommand();

            sqlCmd.CommandText = "dbo.p_get_patient_info";
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Connection = DBConnection;

            var patient = new SqlParameter("@patient_id", patientId);

            sqlCmd.Parameters.Add(patient);

            DBConnection.Open();

            DataTable table = new DataTable();
            table.Load(sqlCmd.ExecuteReader());

            foreach (DataRow row in table.Rows)
            {
                patientInfo.ID = (Guid)row["ID"];
                patientInfo.FullName = row["FullName"].ToString();
                patientInfo.Sex = (int)row["Sex"];
                patientInfo.BirthDate = (DateTime)row["BirthDate"];
                patientInfo.MedicalCardNumber = row["MedicalCardNumber"].ToString();
                patientInfo.Info = row["Info"].ToString();
                patientInfo.Note = row["Note"].ToString();
            }

            DBConnection.Close();

            return patientInfo;
        }

        public List<Attachments> GetAttachments(Guid message_id)
        {            
            List<Attachments> attachments = new List<Attachments>();

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                string sql = "select * from dbo.[Attachments] where [MessageID] = @message_id";
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@message_id", message_id);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Guid id = reader.GetGuid(0);
                    Guid messageID = reader.GetGuid(1);
                    byte[] data = (byte[])reader.GetValue(2);
                    string comment = reader.GetString(3);

                    Attachments attachment = new Attachments(id, messageID, data, comment);
                    attachments.Add(attachment);
                }
            }

            return attachments;
        }

        public Guid CheckUser(string login, string passwordHash)
        {
            Guid UserId = Guid.Empty;

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.CommandText = "dbo.p_check_user";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@login", SqlDbType.NVarChar, 200);
                cmd.Parameters.Add("@passwordHash", SqlDbType.NVarChar, 200);
                cmd.Parameters.Add("@user_id", SqlDbType.UniqueIdentifier);

                cmd.Parameters["@login"].Value = login;
                cmd.Parameters["@passwordHash"].Value = passwordHash;
                cmd.Parameters["@user_id"].Direction = ParameterDirection.Output;

                cmd.ExecuteNonQuery();

                UserId = (Guid)cmd.Parameters["@user_id"].Value;
            }

            return UserId;
        }
    }
}