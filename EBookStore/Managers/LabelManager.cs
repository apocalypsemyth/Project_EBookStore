using EBookStore.Helpers;
using EBookStore.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace EBookStore.Managers
{
    public class LabelManager
    {
        public LabelModel GetLabel(string keyword)
        {
            string whereCondition = "";
            if (!string.IsNullOrWhiteSpace(keyword))
                whereCondition = "WHERE LabelName LIKE '%'+@Keyword+'%'";

            string connectString = ConfigHelper.GetConnectionString();
            string commandText =
                $@" 
                    SELECT *
                    FROM Labels 
                    {whereCondition}
                ";
            try
            {
                using (SqlConnection connect = new SqlConnection(connectString))
                {
                    using (SqlCommand command = new SqlCommand(commandText, connect))
                    {
                        if (!string.IsNullOrWhiteSpace(keyword))
                        {
                            command.Parameters.AddWithValue("@Keyword", keyword);
                        }

                        connect.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        LabelModel model = new LabelModel();
                        while (reader.Read())
                        {
                            model = this.BuildLabelModel(reader);
                        }

                        return model;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog("LabelManager.GetLabel", ex);
                throw;
            }
        }
        
        public void CreateLabel(LabelModel model)
        {
            string connectString = ConfigHelper.GetConnectionString();
            string commandText =
               @"   
                    INSERT INTO Labels
                        (LabelID, LabelName)
                    VALUES
                        (@LabelID, @LabelName) 
               ";

            try
            {
                using (SqlConnection connect = new SqlConnection(connectString))
                {
                    using (SqlCommand command = new SqlCommand(commandText, connect))
                    {
                        model.LabelID = Guid.NewGuid();

                        command.Parameters.AddWithValue("@LabelID", model.LabelID);
                        command.Parameters.AddWithValue("@LabelName", model.LabelName);

                        connect.Open();
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog("LabelManager.CreateLabel", ex);
                throw;
            }
        }

        private LabelModel BuildLabelModel(SqlDataReader reader)
        {
            return new LabelModel()
            {
                LabelID = (Guid)reader["LabelID"],
                LabelName = reader["LabelName"] as string,
            };
        }
    }
}