using ServiceCotacao.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCotacao.Models
{
    class Cotacao
    {
        public int Id { get; set; }
        public string DeMoedaCode { get; set; }
        public int DeMoedaId { get; set; }
        public string ParaMoedaCode { get; set; }
        public int ParaMoedaId { get; set; }
        public double Valor { get; set; }
        public string Email { get; set; }
        public string ProcessSrv { get; set; }

        public List<Cotacao> GetCotacoes() {
            Cotacao co = new Cotacao();
            var lstdCo = new List<Cotacao>();
            using (var cs = new ConnectSQL()) {
                var reader = cs.ExecuteQuery(cs.QueryGetCotacoes());
                try {
                    while (reader.Read()) {
                        co.Id = int.Parse(reader["Id"].ToString());
                        co.DeMoedaCode = reader["DeMoedaCode"].ToString();
                        co.DeMoedaId = int.Parse(reader["DeMoedaId"].ToString());
                        co.ParaMoedaCode = reader["ParaMoedaCode"].ToString();
                        co.ParaMoedaId = int.Parse(reader["ParaMoedaId"].ToString());
                        co.Valor = double.Parse(reader["Valor"].ToString());
                        co.Email = reader["Email"].ToString();
                        co.ProcessSrv = reader["ProcessSrv"].ToString();
                        lstdCo.Add(co);
                        co = new Cotacao();
                    }
                } catch (Exception ex) {

                }
                return lstdCo;
            }
        }
    }
}
