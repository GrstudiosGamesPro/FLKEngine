using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FLKEngine.Components
{
    public class ConsoleDebug
    {
        public List<string> Logs = new List<string>();

        public void Log (string message)
        {
            Logs.Add ("Log: " + message);
        }

        public void Error (string message)
        {
            Logs.Add ("Error: " + message);
        }

        public void Advertising (string message)
        {
            Logs.Add ("Advertising: " + message);
        }

        public void ClearConsole()
        {
            Logs.Clear();
        }
    }
}
