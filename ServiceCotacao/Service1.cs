using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ServiceCotacao
{
    public partial class Service1 : ServiceBase
    {
        public static Thread _thread_startProcess;
        public static bool _statusProcess = false;
        public Service1() {
            InitializeComponent();
        }

        protected override void OnStart(string[] args) {
            try {
                _statusProcess = true;
                ThreadStart startProcess = new ThreadStart(new ControladorSrv().StartProcess);
                _thread_startProcess = new Thread(startProcess);
                _thread_startProcess.Start();
            } catch (Exception ex) {
            }
        }

        protected override void OnStop() {
        }
        public void OnDebug() {
            OnStart(null);
        }
    }
}
