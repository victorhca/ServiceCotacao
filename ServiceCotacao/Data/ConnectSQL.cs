using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCotacao.Data
{
    public class ConnectSQL : IDisposable
    {
        private SqlConnection Connection { get; set; }
        private SqlCommand Command { get; set; }
        private SqlDataReader DataReader { get; set; }

        public SqlConnection Connect() {
            string connetionString = null;
            SqlConnection connection;
            var xml = new ConfigXmlDocument();
            xml.Load($@"{AppDomain.CurrentDomain.BaseDirectory}ConfigSrv.xml");
            connetionString = xml.GetElementsByTagName("connectDb").Item(0).InnerXml.ToString().Replace("\\\\", "\\");
            connection = new SqlConnection(connetionString);

            return connection;
        }

        public SqlDataReader ExecuteQuery(string query) {
            Connection = Connect();
            try {
                Connection.Open();
                Command = new SqlCommand(query, Connection);
                Command.CommandTimeout = 600;
                DataReader = Command.ExecuteReader();

                return DataReader;
            } catch (Exception ex) {
                return null;
            }
        }

        public string QueryGetCotacoes() {
            string query = string.Format(@"SELECT Id
                                        ,DeMoedaCode
                                        ,DeMoedaId
                                        ,ParaMoedaCode
                                        ,ParaMoedaId
                                        ,Valor
                                        ,Email
                                        ,ProcessSrv
                                        FROM Quotations
                                        Where ProcessSrv <> 'Y'");
            return query;
        }

        public void Dispose() {
            Connection = null;
            Command = null;
            DataReader = null;
            GC.Collect();
        }
    }
}
