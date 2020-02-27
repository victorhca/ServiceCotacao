﻿using ServiceCotacao.Api;
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
                    foreach (var cot in lstdCotacao) {

                        int i = 0;
                        string result = "";
                        var dtFormatada = DateTime.Now.ToString("yyyy-MM-dd");

                        result = new SendActions().GetConversao(cot.DeMoedaId, cot.ParaMoedaId, cot.Valor, dtFormatada);

                        /*Em casos de feriados ou dependendo do horário o banco central não disponibiliza cotações, então criei 
                         * o while que tenta mais 5 vezes reduzindo um dia a cada tentativa e assim pegar a última cotação.*/
                        while (result == "") {
                            i++;
                            if (i == 6)
                                break;
                            dtFormatada = DateTime.Parse(dtFormatada).AddDays(-1).ToString("yyyy-MM-dd");
                            result = new SendActions().GetConversao(cot.DeMoedaId, cot.ParaMoedaId, cot.Valor, dtFormatada);
                        }
                    }
                    verifica = false;
                }
            } catch (Exception ex) {
            }
        }
    }
}
