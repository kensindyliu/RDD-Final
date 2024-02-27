using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventEntities;
using System.Data.SqlClient;
using System.Data;
using System.Runtime.InteropServices;

namespace EventOperation
{
    //implement get,update,add,delete records operations of table Events
    public class Events
    {
        public List<Event> GetEvents(int eventID = -1)
        {

            using (SqlConnection connection = new SqlConnection(Invariants.Connectionstring))
            {
                try
                {
                    List<Event> events = new List<Event>();
                    connection.Open();
                    string query = "Select * from Events";
                    if (eventID != -1)
                        query += $" where EventID = {eventID}";
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
                                    Event et = new Event(
                                        (int)row["EventID"],
                                        (string)row["EventName"],
                                        (DateTime)row["EventDate"],
                                        (string)row["EventLocation"]
                                        );
                                    events.Add(et);
                                }
                            }
                        }
                    }
                    return events;
                }
                catch
                {
                    return null;
                }
            }
        }

        public bool UpdateEvent(int eventID, string oldEventName, DateTime oldEventDate, string oldEventLocation,
                                    string newEventName, DateTime newEventDate, string newEventLocation, out string errMsg)
        {
            errMsg = "";
            using (SqlConnection connection = new SqlConnection(Invariants.Connectionstring))
            {
                try
                {
                    using (SqlCommand command = new SqlCommand("usp_UpdateEvents", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Add parameters
                        command.Parameters.AddWithValue("@eventID", eventID);
                        command.Parameters.AddWithValue("@oldEventName", oldEventName);
                        command.Parameters.AddWithValue("@oldEventDate", oldEventDate);
                        command.Parameters.AddWithValue("@oldEventLocation", oldEventLocation);
                        command.Parameters.AddWithValue("@newEventName", newEventName);
                        command.Parameters.AddWithValue("@newEventDate", newEventDate);
                        command.Parameters.AddWithValue("@newEventLocation", newEventLocation);

                        // Output parameter for result
                        SqlParameter resultParameter = new SqlParameter("@Result", SqlDbType.Int);
                        resultParameter.Direction = ParameterDirection.Output;
                        command.Parameters.Add(resultParameter);

                        connection.Open();
                        command.ExecuteNonQuery();

                        // Access the result after the execution
                        int result = (int) command.Parameters["@Result"].Value;
                        if(result > 0)
                            return true;
                        else
                            return false;
                    }
                }
                catch (Exception ex) 
                {
                    errMsg = ex.Message;
                    return false;
                }
            }
        }

        public bool DeleteEvent(int eventId, out string errMessage)
        {
            errMessage = "";
            using (SqlConnection connection = new SqlConnection(Invariants.Connectionstring))
            {
                try
                {
                    using (SqlCommand command = new SqlCommand("usp_DeleteEvent", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Add parameters
                        command.Parameters.AddWithValue("@eventID", eventId);

                        // Output parameter for result
                        SqlParameter resultParameter = new SqlParameter("@isSucceed", SqlDbType.Int);
                        resultParameter.Direction = ParameterDirection.Output;
                        command.Parameters.Add(resultParameter);

                        connection.Open();
                        command.ExecuteNonQuery();
                        
                        // Access the result after the execution
                        int result = (int)command.Parameters["@isSucceed"].Value;
                        if (result > 0)
                            return true;
                        else
                            return false;
                    }
                }
                catch (Exception ex)
                {
                    errMessage = "Failed to delete the event:\n" + ex.Message;
                    return false;
                }
            }
        }

        public bool AddANewEvent(string newEventName, DateTime newEventDate, string newEventLocation)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(Invariants.Connectionstring))
                {
                    string query = $"insert into Events values ('{newEventName}' ,'{newEventDate}','{newEventLocation}')";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        connection.Open();
                        int effectRow = command.ExecuteNonQuery();
                        if (effectRow > 0)
                        {
                            return true;
                        }
                        return false;
                    }
                }
            } catch (Exception ex)
            {
                return false;
            }
            
        }
    }
}
