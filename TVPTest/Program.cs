using FastMember;
using PetaPoco;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TVPTest
{
    [TableName("Names"),PrimaryKey("Name_Id")]
    public class Name
    {
        public int Name_Id { get; set; }
        public string My_name { get; set; }
    }
    class Program
    {
        static void Main(string[] args)
        {
            using(var db=new Database("test"))
            {
                db.OpenSharedConnection();
                var name1=new Name { My_name = "Olo" };
                var name2=new Name { My_name = "Bolo" };
                var list = new List<Name>() { name1, name2 };
                var pocodata = PocoData.ForType(typeof(Name),new ConventionMapper());

                var reader=ObjectReader.Create(list,pocodata.UpdateColumns);
                var meta = reader.GetSchemaTable();
                //meta.TableName=""

                var command = (SqlCommand)db.Connection.CreateCommand();
                var param=command.Parameters.AddWithValue("@names", reader);
                param.SqlDbType = SqlDbType.Structured;
//                param.TypeName = "Test";
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "mytest";
                var r = command.ExecuteNonQuery();
              
                


            }
        }
    }
}
