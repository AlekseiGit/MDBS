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
        //public static string ConnectionString = @"Data Source=ALEX-PC\SQLEXPRESS;Initial Catalog=MDBS;Integrated Security=SSPI;";
        public static string ConnectionString = @"Server=tcp:iprs.ru,49172;Database=MDBS;User Id=mdbs;Password=1pa73%od9;";
        public SqlConnection DBConnection;
        public Guid UserID;

        public CoreFunc(Guid userId)
        {
            DBConnection = new SqlConnection(ConnectionString);
            UserID = userId;
        }

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

            var userId = new SqlParameter("@user_id", UserID);

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
                msg.PatientID = (Guid)row["PatientID"];
                msg.PatientName = row["PatientName"].ToString();
                msg.From = (Guid)row["From"];
                //msg.To = (Guid)row["To"];
                msg.FromName = row["FromName"].ToString();
                msg.MessageDate = ((DateTime)row["MessageDate"]).ToString("dd.MM.yy  HH:mm");
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

            var userId = new SqlParameter("@user_id", UserID);

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
                msg.PatientID = (Guid)row["PatientID"];
                msg.PatientName = row["PatientName"].ToString();
                msg.From = (Guid)row["From"];
                //msg.To = (Guid)row["To"];
                msg.FromName = row["FromName"].ToString();
                msg.MessageDate = ((DateTime)row["MessageDate"]).ToString("dd.MM.yy  HH:mm");
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

            var userId = new SqlParameter("@user_id", UserID);

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
                msg.PatientID = (Guid)row["PatientID"];
                msg.PatientName = row["PatientName"].ToString();
                msg.From = (Guid)row["From"];
                //msg.To = (Guid)row["To"];
                msg.FromName = row["FromName"].ToString();
                msg.MessageDate = ((DateTime)row["MessageDate"]).ToString("dd.MM.yy  HH:mm");
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

            var userId = new SqlParameter("@user_id", UserID);

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
                msg.PatientID = (Guid)row["PatientID"];
                msg.PatientName = row["PatientName"].ToString();
                msg.From = (Guid)row["From"];
                //msg.To = (Guid)row["To"];
                msg.FromName = row["FromName"].ToString();
                msg.MessageDate = ((DateTime)row["MessageDate"]).ToString("dd.MM.yy  HH:mm");
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
                dlg.MessageDate = ((DateTime)row["MessageDate"]).ToString("dd.MM.yy  HH:mm");

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
            byte[] img_0,
            byte[] img_1,
            byte[] img_2,
            byte[] img_3,
            byte[] img_4,
            byte[] img_5,
            byte[] img_6,
            byte[] img_7,
            byte[] img_8,
            byte[] img_9)
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

                if (img_0 != null)
                {
                    cmd.Parameters.Add("@img_0", SqlDbType.Image, 1000000);
                    cmd.Parameters.Add("@img_0_send", SqlDbType.Int);
                    cmd.Parameters["@img_0"].Value = img_0;
                    cmd.Parameters["@img_0_send"].Value = 1;
                }

                if (img_1 != null)
                {
                    cmd.Parameters.Add("@img_1", SqlDbType.Image, 1000000);
                    cmd.Parameters.Add("@img_1_send", SqlDbType.Int);
                    cmd.Parameters["@img_1"].Value = img_1;
                    cmd.Parameters["@img_1_send"].Value = 1;
                }

                if (img_2 != null)
                {
                    cmd.Parameters.Add("@img_2", SqlDbType.Image, 1000000);
                    cmd.Parameters.Add("@img_2_send", SqlDbType.Int);
                    cmd.Parameters["@img_2"].Value = img_2;
                    cmd.Parameters["@img_2_send"].Value = 1;
                }

                if (img_3 != null)
                {
                    cmd.Parameters.Add("@img_3", SqlDbType.Image, 1000000);
                    cmd.Parameters.Add("@img_3_send", SqlDbType.Int);
                    cmd.Parameters["@img_3"].Value = img_3;
                    cmd.Parameters["@img_3_send"].Value = 1;
                }

                if (img_4 != null)
                {
                    cmd.Parameters.Add("@img_4", SqlDbType.Image, 1000000);
                    cmd.Parameters.Add("@img_4_send", SqlDbType.Int);
                    cmd.Parameters["@img_4"].Value = img_4;
                    cmd.Parameters["@img_4_send"].Value = 1;
                }

                if (img_5 != null)
                {
                    cmd.Parameters.Add("@img_5", SqlDbType.Image, 1000000);
                    cmd.Parameters.Add("@img_5_send", SqlDbType.Int);
                    cmd.Parameters["@img_5"].Value = img_5;
                    cmd.Parameters["@img_5_send"].Value = 1;
                }

                if (img_6 != null)
                {
                    cmd.Parameters.Add("@img_6", SqlDbType.Image, 1000000);
                    cmd.Parameters.Add("@img_6_send", SqlDbType.Int);
                    cmd.Parameters["@img_6"].Value = img_6;
                    cmd.Parameters["@img_6_send"].Value = 1;
                }

                if (img_7 != null)
                {
                    cmd.Parameters.Add("@img_7", SqlDbType.Image, 1000000);
                    cmd.Parameters.Add("@img_7_send", SqlDbType.Int);
                    cmd.Parameters["@img_7"].Value = img_7;
                    cmd.Parameters["@img_7_send"].Value = 1;
                }

                if (img_8 != null)
                {
                    cmd.Parameters.Add("@img_8", SqlDbType.Image, 1000000);
                    cmd.Parameters.Add("@img_8_send", SqlDbType.Int);
                    cmd.Parameters["@img_8"].Value = img_8;
                    cmd.Parameters["@img_8_send"].Value = 1;
                }

                if (img_9 != null)
                {
                    cmd.Parameters.Add("@img_9", SqlDbType.Image, 1000000);
                    cmd.Parameters.Add("@img_9_send", SqlDbType.Int);
                    cmd.Parameters["@img_9"].Value = img_9;
                    cmd.Parameters["@img_9_send"].Value = 1;
                }

                cmd.ExecuteNonQuery();
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
                if ((int)row["Sex"] == 1)
                {
                    patientInfo.Sex = "М";
                }
                else if ((int)row["Sex"] == 2)
                {
                    patientInfo.Sex = "Ж";
                }
                patientInfo.Weight = (int)row["Weight"];
                patientInfo.DrugsCount = row["DrugsCount"].ToString();
                patientInfo.BirthDate = ((DateTime)row["BirthDate"]).ToString("yyyy-MM-dd");
                patientInfo.MedicalCardNumber = row["MedicalCardNumber"].ToString();
                patientInfo.CurrentTherapy = row["CurrentTherapy"].ToString();
                patientInfo.Info = row["Info"].ToString();
                patientInfo.Note = row["Note"].ToString();
            }

            DBConnection.Close();

            return patientInfo;
        }

        public void EditPatientInfo(
            Guid patientId,
            string fullName,
            int sex,
            int weight,
            string drugsCount,
            DateTime birthDate,
            string medicalCardNumber,
            string currentTherapy,
            string info,
            string note)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.CommandText = "dbo.p_edit_patient";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@patient_id", SqlDbType.UniqueIdentifier);
                cmd.Parameters.Add("@fullName", SqlDbType.NVarChar, 200);
                cmd.Parameters.Add("@sex", SqlDbType.Int);
                cmd.Parameters.Add("@weight", SqlDbType.Int);
                cmd.Parameters.Add("@drugsCount", SqlDbType.NVarChar, 10);
                cmd.Parameters.Add("@birthDate", SqlDbType.DateTime);
                cmd.Parameters.Add("@medicalCardNumber", SqlDbType.NVarChar, 100);
                cmd.Parameters.Add("@currentTherapy", SqlDbType.NVarChar, 4000);
                cmd.Parameters.Add("@info", SqlDbType.NVarChar, 4000);
                cmd.Parameters.Add("@note", SqlDbType.NVarChar, 4000);

                cmd.Parameters["@patient_id"].Value = patientId;
                cmd.Parameters["@fullName"].Value = fullName;
                cmd.Parameters["@sex"].Value = sex;
                cmd.Parameters["@weight"].Value = weight;
                cmd.Parameters["@drugsCount"].Value = drugsCount;
                cmd.Parameters["@birthDate"].Value = birthDate;
                cmd.Parameters["@medicalCardNumber"].Value = medicalCardNumber;
                cmd.Parameters["@currentTherapy"].Value = currentTherapy;
                cmd.Parameters["@info"].Value = info;
                cmd.Parameters["@note"].Value = note;

                cmd.ExecuteNonQuery();
            }
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

        public void ReadMessage(Guid message_id)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                string sql = "update dbo.[Message] set [Status] = 1 where [ID] = @message_id";
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@message_id", message_id);
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        public List<Patient> GetPatients()
        {
            List<Patient> patients = new List<Patient>();

            SqlCommand sqlCmd = new SqlCommand();

            sqlCmd.CommandText = "dbo.p_get_patients";
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Connection = DBConnection;

            DBConnection.Open();

            DataTable table = new DataTable();
            table.Load(sqlCmd.ExecuteReader());

            foreach (DataRow row in table.Rows)
            {
                Patient patient = new Patient();

                patient.ID = (Guid)row["ID"];
                patient.FullName = row["FullName"].ToString();
                if ((int)row["Sex"] == 1)
                {
                    patient.Sex = "М";
                }
                else if ((int)row["Sex"] == 2)
                {
                    patient.Sex = "Ж";
                }
                patient.Weight = (int)row["Weight"];
                patient.DrugsCount = row["DrugsCount"].ToString();
                patient.BirthDate = ((DateTime)row["BirthDate"]).ToString("yyyy-MM-dd");
                patient.MedicalCardNumber = row["MedicalCardNumber"].ToString();
                patient.CurrentTherapy = row["CurrentTherapy"].ToString();
                patient.Info = row["Info"].ToString();
                patient.Note = row["Note"].ToString();

                patients.Add(patient);
            }

            DBConnection.Close();

            return patients;
        }

        public void CreatePatient(
            string fullName,
            int sex,
            int weight,
            string drugsCount,
            DateTime birthDate,
            string medicalCardNumber,
            string currentTherapy,
            string info,
            string note)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.CommandText = "dbo.p_new_patient";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@fullName", SqlDbType.NVarChar, 200);
                cmd.Parameters.Add("@sex", SqlDbType.Int);
                cmd.Parameters.Add("@weight", SqlDbType.Int);
                cmd.Parameters.Add("@drugsCount", SqlDbType.NVarChar, 10);
                cmd.Parameters.Add("@birthDate", SqlDbType.DateTime);
                cmd.Parameters.Add("@medicalCardNumber", SqlDbType.NVarChar, 100);
                cmd.Parameters.Add("@currentTherapy", SqlDbType.NVarChar, 4000);
                cmd.Parameters.Add("@info", SqlDbType.NVarChar, 4000);
                cmd.Parameters.Add("@note", SqlDbType.NVarChar, 200);

                cmd.Parameters["@fullName"].Value = fullName;
                cmd.Parameters["@sex"].Value = sex;
                cmd.Parameters["@weight"].Value = weight;
                cmd.Parameters["@drugsCount"].Value = drugsCount;
                cmd.Parameters["@birthDate"].Value = birthDate;
                cmd.Parameters["@medicalCardNumber"].Value = medicalCardNumber;
                cmd.Parameters["@currentTherapy"].Value = currentTherapy;
                cmd.Parameters["@info"].Value = info;
                cmd.Parameters["@note"].Value = note;

                cmd.ExecuteNonQuery();
            }
        }

        public void DeletePatient(Guid patient_id)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                string sql = "delete from dbo.[Patient] where [ID] = @patient_id";
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@patient_id", patient_id);
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        public SystemData GetSystemData(Guid userId)
        {
            SystemData systemData = new SystemData();

            SqlCommand sqlCmd = new SqlCommand();

            sqlCmd.CommandText = "dbo.p_get_system_data";
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Connection = DBConnection;

            var patient = new SqlParameter("@user_id", userId);

            sqlCmd.Parameters.Add(patient);

            DBConnection.Open();

            DataTable table = new DataTable();
            table.Load(sqlCmd.ExecuteReader());

            foreach (DataRow row in table.Rows)
            {
                systemData.IncomingInfo = row["incoming_info"].ToString();
                systemData.OutgoingInfo = row["outgoing_info"].ToString();
                systemData.NeedAnswerInfo = row["need_answer_info"].ToString();
                systemData.NeedAnswerDate = (DateTime)row["need_answer_date"];
            }

            DBConnection.Close();

            return systemData;
        }

        public User CheckUser(string login, string passwordHash)
        {
            User CurrentUser = new User();

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
                cmd.Parameters.Add("@user_name", SqlDbType.NVarChar, 200);
                cmd.Parameters.Add("@user_num", SqlDbType.NVarChar, 100);

                cmd.Parameters["@login"].Value = login;
                cmd.Parameters["@passwordHash"].Value = passwordHash;
                cmd.Parameters["@user_id"].Direction = ParameterDirection.Output;
                cmd.Parameters["@user_name"].Direction = ParameterDirection.Output;
                cmd.Parameters["@user_num"].Direction = ParameterDirection.Output;

                cmd.ExecuteNonQuery();

                CurrentUser.ID = (Guid)cmd.Parameters["@user_id"].Value;
                CurrentUser.FullName = cmd.Parameters["@user_name"].Value.ToString();
                CurrentUser.DocNumber = cmd.Parameters["@user_num"].Value.ToString();
            }

            return CurrentUser;
        }
    }
}