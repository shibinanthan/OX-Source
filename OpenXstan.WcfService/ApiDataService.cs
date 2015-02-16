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
using System.ServiceModel.Activation;

namespace OpenXstan.WcfService
{
    [Serializable]
    [ServiceBehavior(Name = "ApiDataService", Namespace = "ProductApiservice")]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class ApiDataService : IApiDataService
    {
        public bool ValidateUser(string userName)
        {
            var returnValue = 0;
            using (SqlConnection connection = new SqlConnection(@"Server=(localdb)\v11.0;Integrated Security=true;AttachDbFileName=E:\Mahesh\Practices\OpenXstan\OpenXstan.Web\App_Data\Xposure.mdf;"))
            {
                connection.Open();
                using (var cmd = new SqlCommand("SP_ValidateUsers", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@UserName", SqlDbType.VarChar).Value = userName;
                    cmd.Parameters.Add("@Password", SqlDbType.VarChar).Value = "shibi143";

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

        public List<Product> GetProduct(string id) 
        {
            var sqlString = string.Format("SELECT * FROM Product WHERE ProductId='{0}'", id);

            if (string.IsNullOrEmpty(id))
                sqlString = "SELECT * FROM Product";

            var product = new List<Product>();

            using (SqlConnection connection = new SqlConnection(@"Server=(localdb)\v11.0;Integrated Security=true;AttachDbFileName=E:\Mahesh\Practices\OpenXstan\OpenXstan.Web\App_Data\Xposure.mdf;"))
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
                            EndTime = Convert.ToDateTime(sqlReader.GetValue(7))
                        });
                    }
                }
            }
            return product;
        }
    }
}
