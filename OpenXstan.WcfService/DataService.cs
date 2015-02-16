using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.ServiceModel;
using OpenXstan.Common;
using OpenXstan.Common.WcfService.Contracts;

namespace OpenXstan.WcfService
{
    [Serializable]
    [ServiceBehavior(Name = "DataService", Namespace = "Productservice")] 
    public class DataService : IDataService
    {
        public bool ValidateUser(XposureUser user)
        {
            var returnValue = 0;
            using (SqlConnection connection = new SqlConnection(user.ConnectionString))
            {
                connection.Open();
                using (var cmd = new SqlCommand("SP_ValidateUsers", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@UserName", SqlDbType.VarChar).Value = user.UserName;
                    cmd.Parameters.Add("@Password", SqlDbType.VarChar).Value = user.Password;

                    SqlParameter statusParam = new SqlParameter();
                    statusParam.ParameterName = "@Status";
                    statusParam.DbType = DbType.Int32;
                    statusParam.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(statusParam);

                    cmd.ExecuteNonQuery();
                    returnValue = Convert.ToInt32(cmd.Parameters["@Status"].Value);
                }
            }
            return returnValue == 0 ? false : true;
        }

        public Status ValidateXposureUser(SecurityDetail security)
        {
            var returnValue = 0;
            if (security.SecurityToken != null && security.SecurityToken.ToUpper() != "SHIBI143")
            {
                //return new Status() { Valid = false };
                throw new FaultException("You are not Authorized to access this operation");
            }

            using (SqlConnection connection = new SqlConnection(security.ConnectionString))
            {
                connection.Open();
                using (var cmd = new SqlCommand("SP_ValidateUsers", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@UserName", SqlDbType.VarChar).Value = security.UserName;
                    cmd.Parameters.Add("@Password", SqlDbType.VarChar).Value = security.Password;

                    SqlParameter statusParam = new SqlParameter();
                    statusParam.ParameterName = "@Status";
                    statusParam.DbType = DbType.Int32;
                    statusParam.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(statusParam);

                    cmd.ExecuteNonQuery();
                    returnValue = Convert.ToInt32(cmd.Parameters["@Status"].Value);
                }
            }
            var status =  returnValue == 0 ? false : true;
            return new Status() { Valid = status };
        }

        public bool ValidateUser(XposureUser user, bool ignoreValidation)
        {
            return true;
        }
        public List<Product> GetProduct(XposureUser user, string id)
        {
            var sqlString = string.Format("SELECT * FROM Product WHERE ProductId='{0}'", id);

            if (string.IsNullOrEmpty(id))
                sqlString = "SELECT * FROM Product";

            var product = new List<Product>();

            using (SqlConnection connection = new SqlConnection(user.ConnectionString))
            {
                connection.Open();
                using (var cmd = new SqlCommand(sqlString, connection))
                {
                    cmd.CommandType = CommandType.Text;

                    var sqlReader = cmd.ExecuteReader();

                    while (sqlReader.Read())
                    {
                        product.Add(new Product()
                        {
                            Id = Convert.ToString(sqlReader.GetValue(1)),
                            Title = Convert.ToString(sqlReader.GetValue(2)),
                            Description = Convert.ToString(sqlReader.GetValue(3)),
                            StartPrice = Convert.ToDecimal(sqlReader.GetValue(4)),
                            CurrentPrice = Convert.ToDecimal(sqlReader.GetValue(5)),
                            StartTime = Convert.ToDateTime(sqlReader.GetValue(6)),
                            EndTime = Convert.ToDateTime(sqlReader.GetValue(7)),
                            CategoryID = Convert.ToInt32(sqlReader.GetValue(8)),
                        });
                    }
                }
            }
            return product;
        }

        public bool SaveProduct(XposureUser user, Product product)
        {
            var returnValue = 0;
            using (SqlConnection connection = new SqlConnection(user.ConnectionString))
            {
                connection.Open();
                using (var cmd = new SqlCommand("SP_SaveProduct", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@ProductId", SqlDbType.VarChar).Value = product.Id;
                    cmd.Parameters.Add("@Title", SqlDbType.VarChar).Value = product.Title;
                    cmd.Parameters.Add("@Description", SqlDbType.VarChar).Value = product.Description;
                    cmd.Parameters.Add("@StartPrice", SqlDbType.Decimal).Value = product.StartPrice;
                    cmd.Parameters.Add("@StartTime", SqlDbType.VarChar).Value = product.StartTime;
                    cmd.Parameters.Add("@Status", SqlDbType.Int).Direction = ParameterDirection.Output;

                    returnValue = cmd.ExecuteNonQuery();
                }
            }
            return returnValue == 0 ? false : true;
        }
        public bool NotOperationContract()
        {
            return true;
        }
    }
}
