using ServiceCotacao.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCotacao.Actions
{
    public class Email {

        public string Usuario { get; set; }
        public string Senha { get; set; }
        public string Servidor { get; set; }
        public string Porta { get; set; }
        public bool EnableSsl { get; set; }

        public Email GetCnfgEmailBase() {
            Email email = new Email();
            try {
                var xml = new ConfigXmlDocument();
                xml.Load($@"{AppDomain.CurrentDomain.BaseDirectory}ConfigSrv.xml");

                email.Usuario = xml.GetElementsByTagName("Usuario").Item(0).InnerXml.ToString();
                email.Senha = xml.GetElementsByTagName("Senha").Item(0).InnerXml.ToString();
                email.Servidor = xml.GetElementsByTagName("Servidor").Item(0).InnerXml.ToString();
                email.Porta = xml.GetElementsByTagName("Porta").Item(0).InnerXml.ToString();
                email.EnableSsl = bool.Parse(xml.GetElementsByTagName("EnableSsl").Item(0).InnerXml.ToString().Replace("Y", "true").Replace("N", "false"));

            } catch (Exception) { }
            return email;
        }

        public void SendEmailResultQuotation(Cotacao cotacao, string valorConvertido, string dataCotacao) {
            Email remetente = GetCnfgEmailBase();
            using (var mail = new MailMessage()) {
                using (var SmtpServer = new SmtpClient(remetente.Servidor)) {

                    SmtpServer.Port = int.Parse(remetente.Porta);
                    SmtpServer.EnableSsl = remetente.EnableSsl;
                    SmtpServer.Credentials = new System.Net.NetworkCredential(remetente.Usuario, remetente.Senha);

                    mail.Sender = new MailAddress(remetente.Usuario, "Serviço de Cotação");
                    mail.From = new MailAddress(remetente.Usuario, "Serviço de Cotação");
                    mail.To.Add(cotacao.Email);
                    mail.Subject = "Cotação Solicitada";
                    mail.IsBodyHtml = true;
                    mail.Body = string.Format(@"Olá!<br><br>
                                                Sua solicitação de cotação está concluída. Veja a baixo o resultado:<br><br>
                                                <b>Detalhes da Solicitação</b><br>
                                                <b>De:</b> {0}<br>
                                                <b>Para:</b> {1}<br>
                                                <b>Valor:</b> {2}<br><br>
                                                <b>Resultado</b><br>
                                                <b>Valor convertido:</b> {3}<br>
                                                <b>Data da cotação:</b> {4}", cotacao.DeMoedaCode, cotacao.ParaMoedaCode, cotacao.Valor, valorConvertido, dataCotacao);

                    SmtpServer.Send(mail);
                }
            }
        }
    }
}
