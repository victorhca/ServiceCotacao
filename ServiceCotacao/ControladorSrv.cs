using ServiceCotacao.Api;
using ServiceCotacao.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace ServiceCotacao
{
    public class ControladorSrv
    {
        public static bool verifica { get; set; } = false;
        public void StartProcess() {
            try {
                //var xml = new ConfigXmlDocument();
                //xml.Load($@"{AppDomain.CurrentDomain.BaseDirectory}ConfigSrv.xml");
                //var timerDorme = xml.GetElementsByTagName("StartNaoIniciado").Item(0).InnerXml.ToString().Replace("\\\\", "\\");
                //var timerWait = xml.GetElementsByTagName("TimeWait").Item(0).InnerXml.ToString().Replace("\\\\", "\\");

                //Timer pni = new System.Timers.Timer(int.Parse(timerDorme));
                //pni.Elapsed += new ElapsedEventHandler(timer_Conversao);
                //pni.Enabled = true;

                timer_Conversao(null, null);

                //while (true) {
                //    System.Threading.Thread.Sleep(int.Parse(timerWait));
                //}

            } catch (Exception e) {

            }
        }

        private void timer_Conversao(object sender, ElapsedEventArgs e) {
            try {
                if (verifica == false) {
                    verifica = true;

                    var lstdCotacao = new Cotacao().GetCotacoes();
                    
                    verifica = false;
                }
            } catch (Exception ex) {
            }
        }
    }
}
