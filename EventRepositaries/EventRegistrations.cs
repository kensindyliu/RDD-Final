using EventEntities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventOperation
{
    //implement get,update,add,delete records operations of table EventRegistrations
    public class EventRegistrations
    {
        public List<EventRegistration> GetEventRegistrations(int eventID)
        {
            List<EventRegistration> eventRegistrations = new List<EventRegistration>();

            using (SqlConnection connection = new SqlConnection(Invariants.Connectionstring))
            {
                try
                {
                    string query = $@"select er.*, es.EventName
                            from EventRegistrations er
                            join Events es on es.EventID = er.EventID
                            where er.EventID = {eventID}";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            DataTable dataTable = new DataTable();
                            adapter.Fill(dataTable);

                            if (dataTable.Rows.Count > 0)
                            {
                                foreach (DataRow row in dataTable.Rows)
                                {
                                    EventRegistration er = new EventRegistration()
                                    {
                                        RegistrationID = (int)row["RegistrationID"],
                                        EventID = (int)row["EventID"],
                                        EventName = (string)row["EventName"],
                                        ParticipantName = (string)row["ParticipantName"],
                                        ParticipantEmail = (string)row["ParticipantEmail"],
                                        RegistrationDate = (DateTime)row["RegistrationDate"]
                                    };
                                    eventRegistrations.Add(er);
                                }
                            }
                        }
                    }
                }
                catch
                {
                    return eventRegistrations;
                }
            }

            return eventRegistrations;
        }

        public bool DeleteARegistration(int registrationID)
        {
            using (SqlConnection connection = new SqlConnection(Invariants.Connectionstring))
            {
                string query = $"delete from EventRegistrations where RegistrationID = {registrationID}";
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.ExecuteNonQuery();
                        return true;
                    }
                }
                catch
                {
                    return false;
                }
            }
        }

        public bool UpdateARegistration(int RegistrationID, string ParticipantName, string ParticipantEmail)
        {
            using (SqlConnection connection = new SqlConnection(Invariants.Connectionstring))
            {
                string query = $@"update EventRegistrations
                                    set ParticipantName = '{ParticipantName}',
	                                    ParticipantEmail = '{ParticipantEmail}'
                                    where RegistrationID = {RegistrationID}";
                
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.ExecuteNonQuery();
                        return true;
                    }
                }
                catch
                {
                    return false;
                }
            }
        }

        public bool AddANewParticipant(int eventID, string newParticipantName,
                                string newParticipantEmail)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(Invariants.Connectionstring))
                {
                    string query = $"insert into EventRegistrations values ({eventID},'{newParticipantName}','{newParticipantEmail}', GETDATE())";
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        int effectRows = command.ExecuteNonQuery();
                        if (effectRows > 0)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }

                    }
                }
            }
            catch (Exception ex) 
            {
                return false ; 
            }
        }
    }
}
