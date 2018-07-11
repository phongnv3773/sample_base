using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ss.Core
{
    public class LogHelper
    {
        private static TraceListener _listener;

        //TODO use when listen sytem
        public static Action<string> ListeningLogDebug { get; set; }

        public static void LogDebug(object obj)
        {
            // Get call stack
            StackTrace stackTrace = new StackTrace();
            string nameFile = DateTime.Now.ToString("yyyy-MM-dd");
            string pathFileLogDebug = ConfigurationManager.AppSettings["log.system"] + @"\" + nameFile + ".txt";

            if (File.Exists(pathFileLogDebug))
            {
                if (_listener == null)
                {
                    // Create trace listener.
                    _listener = new DelimitedListTraceListener(pathFileLogDebug);

                    // Add listener.
                    Debug.Listeners.Add(_listener);
                }
            }
            else
            {
                Debug.Listeners.Clear();
                // Create trace listener.
                _listener = new DelimitedListTraceListener(pathFileLogDebug);
                // Add listener.
                Debug.Listeners.Add(_listener);
            }


            // Write and flush.
            Debug.Write(DateTime.Now.ToString("yyyy-MM-ddTHH:mm:sszzz"));
            Debug.Indent();
            Debug.WriteLine(string.Format("[Methold Name: {0} ] ===> {1}", stackTrace.GetFrame(1).GetMethod().Name, obj.ToString()));
            Debug.Unindent();
            Debug.Flush();

            // listening 
            if (!string.IsNullOrEmpty(obj.ToString()) && ListeningLogDebug != null)
            {
                ListeningLogDebug(string.Format(DateTime.Now.ToString("yyyy-MM-ddTHH:mm:sszzz") + string.Format("[Methold Name: {0} ] ===> {1}", stackTrace.GetFrame(1).GetMethod().Name, obj.ToString()) ));
            }

        }
    }
}
