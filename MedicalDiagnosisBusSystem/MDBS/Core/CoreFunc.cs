using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using DataModels;

namespace Core
{
    public class CoreFunc
    {
        //public static string ConnectionString = @"Server=tcp:iprs.ru,49172;Database=MDBS;User Id=mdbs;Password=1pa73%od9;";
        //public static string ConnectionString = @"Server=tcp:95.163.84.111,49172;Database=MDBS;User Id=mdbs;Password=1pa73%od9;";
        public static string ConnectionString = @"Server=tcp:95.163.84.111,49172;Database=MDBS_TEST;User Id=mdbs;Password=1pa73%od9;";
        //public static string ConnectionString = @"Data Source=DESKTOP-73ON2N0\SQLEXPRESS;Initial Catalog=MDBS;Integrated Security=SSPI;";


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

        ///<summary>
        /// Метод возвращает входящие сообщения для текущего пользователя
        ///</summary>
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

        ///<summary>
        /// Метод возвращает исходящие сообщения для текущего пользователя
        ///</summary>
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

        ///<summary>
        /// Метод возвращает сообщения в процессе отправки для текущего пользователя
        ///</summary>
        public List<Message> GetSendingMessages()
        {
            List<Message> messages = new List<Message>();

            SqlCommand sqlCmd = new SqlCommand();

            sqlCmd.CommandText = "dbo.p_get_sending_messages";
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
                msg.PatientID = (Guid)row["PatientID"];
                msg.PatientName = row["PatientName"].ToString();
                msg.From = (Guid)row["From"];
                msg.FromName = row["FromName"].ToString();
                msg.MessageDate = ((DateTime)row["MessageDate"]).ToString("dd.MM.yy  HH:mm");
                msg.Status = (int)row["Status"];

                messages.Add(msg);
            }

            DBConnection.Close();

            return messages;
        }

        ///<summary>
        /// Метод возвращает сообщения требующие ответа для текущего пользователя
        ///</summary>
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

        ///<summary>
        /// Метод возвращает архивные сообщения для текущего пользователя
        ///</summary>
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

        ///<summary>
        /// Метод возвращает историю сообщений по пациенту, входящий параметр - id сообщения (пациент берется из этого сообщения)
        ///</summary>
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
                dlg.PatientName = row["MedicalCardNumber"].ToString();
                dlg.From = (Guid)row["From"];
                dlg.FromName = row["FromName"].ToString();
                dlg.MessageDate = ((DateTime)row["MessageDate"]).ToString("dd.MM.yy  HH:mm");

                dialog.Add(dlg);
            }

            DBConnection.Close();

            return dialog;
        }

        ///<summary>
        /// Метод отправки сообщения (запроса в центр)
        /// messageId - id сообщения, info - инфо, diagnosis - диагноз,
        /// patientNumber - номер карты пациента, userId - от кого, toId - кому
        ///</summary>
        public void SendMessage(
            Guid messageId,
            string info,
            string diagnosis,
            string patientNumber,
            Guid userId,
            Guid toId,
            MessageContainer[] MC)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.CommandText = "dbo.p_send_message";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@message_id", SqlDbType.UniqueIdentifier);
                cmd.Parameters.Add("@info", SqlDbType.NVarChar, 4000);
                cmd.Parameters.Add("@diagnosis", SqlDbType.NVarChar, 4000);
                cmd.Parameters.Add("@patient_number", SqlDbType.NVarChar, 100);
                cmd.Parameters.Add("@user_id", SqlDbType.UniqueIdentifier);
                cmd.Parameters.Add("@to_id", SqlDbType.UniqueIdentifier);

                cmd.Parameters["@message_id"].Value = messageId;
                cmd.Parameters["@info"].Value = info;
                cmd.Parameters["@diagnosis"].Value = diagnosis;
                cmd.Parameters["@patient_number"].Value = patientNumber;
                cmd.Parameters["@user_id"].Value = userId;
                cmd.Parameters["@to_id"].Value = toId;

                for (int i=0; i<10; i++)
                {
                    if (MC[i] != null)
                    {
                        byte[] key = ASCIIEncoding.ASCII.GetBytes("key12");
                        RC4 encoder = new RC4(key);

                        var enc = encoder.Encode(MC[i].ImgsData, MC[i].ImgsData.Length);
                        var checksumm = Convert.ToBase64String(enc);

                        string sql = "declare @attachment_id uniqueidentifier = newid() " +
                            "insert into dbo.[Attachments]([ID], [MessageID], [Data], [Comment]) " +
                            "values(@attachment_id, @message_id, null, '') " +
                            "insert into dbo.[AttachmentsQueue] ([MessageID], [AttachmentID], [FileName], [Checksumm], [Status]) " +
                            "values(@message_id, @attachment_id, @file_name, @checksumm, 0)";

                        SqlCommand command = new SqlCommand(sql, connection);
                        command.Parameters.AddWithValue("@message_id", messageId);
                        command.Parameters.AddWithValue("@file_name", MC[i].ImgsDataPath);
                        command.Parameters.AddWithValue("@checksumm", checksumm);
                        command.ExecuteNonQuery();
                    }
                }

                cmd.ExecuteNonQuery();
            }
        }

        ///<summary>
        /// Метод отправки сообщений из очереди
        ///</summary>
        public void SendMessages(Guid UserID)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                string sql = "EXEC [dbo].[p_attachments] " + 
                    "select top(1) aq.* " +
                    "from dbo.[Message] m (nolock) " +
                    "inner join dbo.[Attachments] a (nolock) on a.MessageID = m.ID " +
                    "inner join dbo.[AttachmentsQueue] aq (nolock) on aq.[AttachmentID] = a.[ID] " +
                    "where m.[From] = @user_id and aq.[Status] = 0";
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@user_id", UserID);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Guid attachmentID = reader.GetGuid(2);
                    string dataPath = reader.GetValue(3).ToString();

                    SendAttachments(File.ReadAllBytes(dataPath), attachmentID);
                }

                connection.Close();
            }
        }

        ///<summary>
        /// Метод отправки вложений
        ///</summary>
        public void SendAttachments(byte[] data, Guid attachmentID)
        {
            byte[] key = ASCIIEncoding.ASCII.GetBytes("key12");
            RC4 encoder = new RC4(key);

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                string sql = "EXEC [dbo].[p_attachments] " +
                    "update a " +
                    "set a.[Data] = @data " +
                    "from dbo.[Attachments] a " +
                    "where a.[ID] = @attachment_id";
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@data", encoder.Encode(data, data.Length));
                //command.Parameters.AddWithValue("@data", data);
                command.Parameters.AddWithValue("@attachment_id", attachmentID);
                command.ExecuteNonQuery();

                connection.Close();
            }
        }

        ///<summary>
        /// Метод отправки ответа филиалу
        /// therapyPlan - план лечения, parentMessageId - сообщение на которое приходит ответ,
        /// patientId - id пациента, userId - от кого, toId - кому
        ///</summary>
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

                connection.Close();
            }
        }

        ///<summary>
        /// Метод возвращает информацию по пациенту,
        /// входной параметр - id пациента
        ///</summary>
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
                //patientInfo.FullName = row["FullName"].ToString();
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

                DateTime now = DateTime.Today;
                int age = now.Year - ((DateTime)row["BirthDate"]).Year;
                if ((DateTime)row["BirthDate"] > now.AddYears(-age)) age--;
                patientInfo.Age = age.ToString();
                
                patientInfo.BirthDate = ((DateTime)row["BirthDate"]).ToString("yyyy-MM-dd");
                patientInfo.MedicalCardNumber = row["MedicalCardNumber"].ToString();
                patientInfo.CurrentTherapy = row["CurrentTherapy"].ToString();
                patientInfo.IllStart = ((DateTime)row["IllStart"]).ToString("yyyy-MM-dd");
                patientInfo.UsedDrugs = row["UsedDrugs"].ToString();
                patientInfo.RemissionPeriod = row["RemissionPeriod"].ToString();
                patientInfo.LastExacerbation = ((DateTime)row["LastExacerbation"]).ToString("yyyy-MM-dd");
                patientInfo.AppliedTherapy = row["AppliedTherapy"].ToString();
                patientInfo.SurveyResults = row["SurveyResults"].ToString();
                patientInfo.Info = row["Info"].ToString();
                patientInfo.Note = row["Note"].ToString();
            }

            DBConnection.Close();

            return patientInfo;
        }

        ///<summary>
        /// Метод обновляет информацию по пациенту,
        /// входный параметры - id пациента и информация по нему
        ///</summary>
        public void EditPatientInfo(
            Guid patientId,
            int sex,
            int weight,
            string drugsCount,
            DateTime birthDate,
            string medicalCardNumber,
            string currentTherapy,
            DateTime illStart,
            string usedDrugs,
            string remissionPeriod,
            DateTime lastExacerbation,
            string appliedTherapy,
            string surveyResults,
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
                //cmd.Parameters.Add("@fullName", SqlDbType.NVarChar, 200);
                cmd.Parameters.Add("@sex", SqlDbType.Int);
                cmd.Parameters.Add("@weight", SqlDbType.Int);
                cmd.Parameters.Add("@drugsCount", SqlDbType.NVarChar, 10);
                cmd.Parameters.Add("@birthDate", SqlDbType.DateTime);
                cmd.Parameters.Add("@medicalCardNumber", SqlDbType.NVarChar, 100);
                cmd.Parameters.Add("@currentTherapy", SqlDbType.NVarChar, 4000);
                cmd.Parameters.Add("@illStart", SqlDbType.DateTime);
                cmd.Parameters.Add("@usedDrugs", SqlDbType.NVarChar, 4000);
                cmd.Parameters.Add("@remissionPeriod", SqlDbType.NVarChar, 4000);
                cmd.Parameters.Add("@lastExacerbation", SqlDbType.DateTime);
                cmd.Parameters.Add("@appliedTherapy", SqlDbType.NVarChar, 4000);
                cmd.Parameters.Add("@surveyResults", SqlDbType.NVarChar, 4000);
                cmd.Parameters.Add("@info", SqlDbType.NVarChar, 4000);
                cmd.Parameters.Add("@note", SqlDbType.NVarChar, 4000);

                cmd.Parameters["@patient_id"].Value = patientId;
                //cmd.Parameters["@fullName"].Value = fullName;
                cmd.Parameters["@sex"].Value = sex;
                cmd.Parameters["@weight"].Value = weight;
                cmd.Parameters["@drugsCount"].Value = drugsCount;
                cmd.Parameters["@birthDate"].Value = birthDate;
                cmd.Parameters["@medicalCardNumber"].Value = medicalCardNumber;
                cmd.Parameters["@currentTherapy"].Value = currentTherapy;
                cmd.Parameters["@illStart"].Value = illStart;
                cmd.Parameters["@usedDrugs"].Value = usedDrugs;
                cmd.Parameters["@remissionPeriod"].Value = remissionPeriod;
                cmd.Parameters["@lastExacerbation"].Value = lastExacerbation;
                cmd.Parameters["@appliedTherapy"].Value = appliedTherapy;
                cmd.Parameters["@surveyResults"].Value = surveyResults;
                cmd.Parameters["@info"].Value = info;
                cmd.Parameters["@note"].Value = note;

                cmd.ExecuteNonQuery();

                connection.Close();
            }
        }

        ///<summary>
        /// Метод возвращает вложения к выбранному сообщению,
        /// входной параметр - id сообщения
        ///</summary>
        public List<Attachments> GetAttachments(Guid message_id)
        {            
            List<Attachments> attachments = new List<Attachments>();

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                string sql = "select a.* from dbo.[Attachments] a (nolock) where a.[MessageID] = @message_id and a.[Data] is not null";
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@message_id", message_id);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    byte[] key = ASCIIEncoding.ASCII.GetBytes("key12");
                    RC4 decoder = new RC4(key);

                    Guid id = reader.GetGuid(0);
                    Guid messageID = reader.GetGuid(1);
                    byte[] data = (byte[])reader.GetValue(2);
                    string comment = reader.GetString(3);

                    Attachments attachment = new Attachments(id, messageID, decoder.Decode(data, data.Length), comment);
                    //Attachments attachment = new Attachments(id, messageID, data, comment);
                    attachments.Add(attachment);
                }

                connection.Close();
            }

            return attachments;
        }

        ///<summary>
        /// Метод меняет статус сообщения на прочитанное,
        /// входной параметр - id сообщения
        ///</summary>
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

        ///<summary>
        /// Метод возвращает список пользователей соответствующего филиала
        /// входной параметр - id пользователя
        ///</summary>
        public List<User> GetUsers(Guid userID)
        {
            List<User> users = new List<User>();

            SqlCommand sqlCmd = new SqlCommand();

            sqlCmd.CommandText = "dbo.p_get_users";
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Connection = DBConnection;

            sqlCmd.Parameters.Add("@user_id", SqlDbType.UniqueIdentifier);
            sqlCmd.Parameters["@user_id"].Value = userID;

            DBConnection.Open();

            DataTable table = new DataTable();
            table.Load(sqlCmd.ExecuteReader());

            foreach (DataRow row in table.Rows)
            {
                User user = new User();

                user.ID = (Guid)row["ID"];
                user.FullName = row["FullName"].ToString();
                user.DocNumber = row["DocNumber"].ToString();

                users.Add(user);
            }

            DBConnection.Close();

            return users;
        }

        ///<summary>
        /// Метод возвращает список пациентов, доступных текущему пользователю
        /// входной параметр - id пользователя
        ///</summary>
        public List<Patient> GetPatients(Guid userID)
        {
            List<Patient> patients = new List<Patient>();

            SqlCommand sqlCmd = new SqlCommand();

            sqlCmd.CommandText = "dbo.p_get_patients";
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Connection = DBConnection;

            sqlCmd.Parameters.Add("@user_id", SqlDbType.UniqueIdentifier);
            sqlCmd.Parameters["@user_id"].Value = userID;

            DBConnection.Open();

            DataTable table = new DataTable();
            table.Load(sqlCmd.ExecuteReader());

            foreach (DataRow row in table.Rows)
            {
                Patient patient = new Patient();

                patient.ID = (Guid)row["ID"];
                //patient.FullName = row["FullName"].ToString();
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
                patient.IllStart = ((DateTime)row["IllStart"]).ToString("yyyy-MM-dd");
                patient.UsedDrugs = row["UsedDrugs"].ToString();
                patient.RemissionPeriod = row["RemissionPeriod"].ToString();
                patient.LastExacerbation = ((DateTime)row["LastExacerbation"]).ToString("yyyy-MM-dd");
                patient.AppliedTherapy = row["AppliedTherapy"].ToString();
                patient.SurveyResults = row["SurveyResults"].ToString();
                patient.Info = row["Info"].ToString();
                patient.Note = row["Note"].ToString();

                patients.Add(patient);
            }

            DBConnection.Close();

            return patients;
        }

        ///<summary>
        /// Метод поиска пациентов по номеру карты,
        /// входные параметры - id пользователя и номер карты
        ///</summary>
        public List<Patient> GetPatientsByNumber(Guid userID, string cardNumber)
        {
            List<Patient> patients = new List<Patient>();

            SqlCommand sqlCmd = new SqlCommand();

            sqlCmd.CommandText = "dbo.p_get_patients_by_number";
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Connection = DBConnection;

            sqlCmd.Parameters.Add("@user_id", SqlDbType.UniqueIdentifier);
            sqlCmd.Parameters.Add("@num", SqlDbType.NVarChar, 100);
            sqlCmd.Parameters["@user_id"].Value = userID;
            sqlCmd.Parameters["@num"].Value = cardNumber;

            DBConnection.Open();

            DataTable table = new DataTable();
            table.Load(sqlCmd.ExecuteReader());

            foreach (DataRow row in table.Rows)
            {
                Patient patient = new Patient();

                patient.ID = (Guid)row["ID"];
                //patient.FullName = row["FullName"].ToString();
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
                patient.IllStart = ((DateTime)row["IllStart"]).ToString("MM-yyyy");
                patient.UsedDrugs = row["UsedDrugs"].ToString();
                patient.RemissionPeriod = row["RemissionPeriod"].ToString();
                patient.LastExacerbation = ((DateTime)row["LastExacerbation"]).ToString("MM-yyyy");
                patient.AppliedTherapy = row["AppliedTherapy"].ToString();
                patient.SurveyResults = row["SurveyResults"].ToString();
                patient.Info = row["Info"].ToString();
                patient.Note = row["Note"].ToString();

                patients.Add(patient);
            }

            DBConnection.Close();

            return patients;
        }

        ///<summary>
        /// Метод создания пациента,
        /// входные параметры - инфо по пациенту
        ///</summary>
        public void CreatePatient(
            int sex,
            int weight,
            string drugsCount,
            DateTime birthDate,
            string medicalCardNumber,
            string currentTherapy,
            DateTime illStart,
            string usedDrugs,
            string remissionPeriod,
            DateTime lastExacerbation,
            string appliedTherapy,
            string surveyResults,
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

                //cmd.Parameters.Add("@fullName", SqlDbType.NVarChar, 200);
                cmd.Parameters.Add("@sex", SqlDbType.Int);
                cmd.Parameters.Add("@weight", SqlDbType.Int);
                cmd.Parameters.Add("@drugsCount", SqlDbType.NVarChar, 10);
                cmd.Parameters.Add("@birthDate", SqlDbType.DateTime);
                cmd.Parameters.Add("@medicalCardNumber", SqlDbType.NVarChar, 100);
                cmd.Parameters.Add("@currentTherapy", SqlDbType.NVarChar, 4000);
                cmd.Parameters.Add("@illStart", SqlDbType.DateTime);
                cmd.Parameters.Add("@usedDrugs", SqlDbType.NVarChar, 4000);
                cmd.Parameters.Add("@remissionPeriod", SqlDbType.NVarChar, 4000);
                cmd.Parameters.Add("@lastExacerbation", SqlDbType.DateTime);
                cmd.Parameters.Add("@appliedTherapy", SqlDbType.NVarChar, 4000);
                cmd.Parameters.Add("@surveyResults", SqlDbType.NVarChar, 4000);
                cmd.Parameters.Add("@info", SqlDbType.NVarChar, 4000);
                cmd.Parameters.Add("@note", SqlDbType.NVarChar, 200);

                //cmd.Parameters["@fullName"].Value = fullName;
                cmd.Parameters["@sex"].Value = sex;
                cmd.Parameters["@weight"].Value = weight;
                cmd.Parameters["@drugsCount"].Value = drugsCount;
                cmd.Parameters["@birthDate"].Value = birthDate;
                cmd.Parameters["@medicalCardNumber"].Value = medicalCardNumber;
                cmd.Parameters["@currentTherapy"].Value = currentTherapy;
                cmd.Parameters["@illStart"].Value = illStart;
                cmd.Parameters["@usedDrugs"].Value = usedDrugs;
                cmd.Parameters["@remissionPeriod"].Value = remissionPeriod;
                cmd.Parameters["@lastExacerbation"].Value = lastExacerbation;
                cmd.Parameters["@appliedTherapy"].Value = appliedTherapy;
                cmd.Parameters["@surveyResults"].Value = surveyResults;
                cmd.Parameters["@info"].Value = info;
                cmd.Parameters["@note"].Value = note;

                cmd.ExecuteNonQuery();

                connection.Close();
            }
        }

        ///<summary>
        /// Метод создания пользователя,
        /// входные параметры - инфо по пользователю
        ///</summary>
        public void CreateUser(
            string fullName,
            string docNumber,
            string passwordHash)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.CommandText = "dbo.p_new_user";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@fullName", SqlDbType.NVarChar, 100);
                cmd.Parameters.Add("@docNumber", SqlDbType.NVarChar, 100);
                cmd.Parameters.Add("@passwordHash", SqlDbType.NVarChar, 200);

                cmd.Parameters["@fullName"].Value = fullName;
                cmd.Parameters["@docNumber"].Value = docNumber;
                cmd.Parameters["@passwordHash"].Value = passwordHash;

                cmd.ExecuteNonQuery();

                connection.Close();
            }
        }

        ///<summary>
        /// Метод удаляет выбранного пациента,
        /// входной параметр - id пациента
        ///</summary>
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

        ///<summary>
        /// Метод возвращает системную информацию по текущему пользователю,
        /// входной параметр - id пользователя
        ///</summary>
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

        ///<summary>
        /// Метод генерации нового номера доктора
        /// входные параметры: UserID - id пользователя
        ///</summary>
        public string NewDocNumber(Guid UserID)
        {
            string docNumber = "";

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.CommandText = "dbo.p_get_docnumber";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@user_id", SqlDbType.UniqueIdentifier);
                cmd.Parameters.Add("@new_docnumber", SqlDbType.NVarChar, 100);

                cmd.Parameters["@user_id"].Value = UserID;
                cmd.Parameters["@new_docnumber"].Direction = ParameterDirection.Output;

                cmd.ExecuteNonQuery();

                docNumber = cmd.Parameters["@new_docnumber"].Value.ToString();

                connection.Close();
            }

            return docNumber;
        }

        ///<summary>
        /// Метод проверки/авторизации пользователя,
        /// входные параметры: login - логин, passwordHash - хэш, docStatus - статус пользователя (филиал/центр)
        ///</summary>
        public User CheckUser(string login, string passwordHash, int docStatus)
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
                cmd.Parameters.Add("@docStatus", SqlDbType.Int);
                cmd.Parameters.Add("@user_id", SqlDbType.UniqueIdentifier);
                cmd.Parameters.Add("@user_name", SqlDbType.NVarChar, 200);
                cmd.Parameters.Add("@user_num", SqlDbType.NVarChar, 100);

                cmd.Parameters["@login"].Value = login;
                cmd.Parameters["@passwordHash"].Value = passwordHash;
                cmd.Parameters["@docStatus"].Value = docStatus;
                cmd.Parameters["@user_id"].Direction = ParameterDirection.Output;
                cmd.Parameters["@user_name"].Direction = ParameterDirection.Output;
                cmd.Parameters["@user_num"].Direction = ParameterDirection.Output;

                cmd.ExecuteNonQuery();

                CurrentUser.ID = (Guid)cmd.Parameters["@user_id"].Value;
                CurrentUser.FullName = cmd.Parameters["@user_name"].Value.ToString();
                CurrentUser.DocNumber = cmd.Parameters["@user_num"].Value.ToString();

                connection.Close();
            }

            return CurrentUser;
        }
    }
}