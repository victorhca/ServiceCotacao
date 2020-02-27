using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ServiceCotacao.Api
{
    public class SendActions
    {
        public string GetConversao(int DeMoedaIso, int ParaMoedaIso, double Valor, string Data) {
            try {
                string caminho = string.Format(@"https://www3.bcb.gov.br/bc_moeda/rest/converter/{0}/1/{1}/{2}/{3}", Valor, DeMoedaIso, ParaMoedaIso, Data);

                var requisicaoWeb = WebRequest.CreateHttp(caminho);
                requisicaoWeb.Method = "GET";

                using (var resposta = requisicaoWeb.GetResponse()) {
                    var dados = resposta.GetResponseStream();
                    StreamReader reader = new StreamReader(dados);
                    string objResponse = reader.ReadToEnd();
                    var xml = new XmlDocument();
                    xml.LoadXml(objResponse);
                    var valor = xml.GetElementsByTagName("valor-convertido").Item(0).InnerXml.ToString().Replace("\\\\", "\\");
                    return valor;
                }
            } catch (Exception ex) {
                return "";
            }
        }
    }
}
