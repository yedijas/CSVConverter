using CSVConverter.BusinessLogic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace CSVConverter
{
    public partial class CSVConverterService : ServiceBase
    {
        private EventLog myEventLog;
        System.Timers.Timer myTimer;

        public CSVConverterService()
        {
            InitializeComponent();
            myEventLog = new EventLog();
            if (!EventLog.Exists("CSVConverterService"))
            {
                EventLog.CreateEventSource("CSVConverterService", "CSVConverterServiceLog");
            }
            myEventLog.Source = "CSVConverterService";
            myEventLog.Log = "CSVConverterServiceLog";
        }

        protected override void OnStart(string[] args)
        {
            myEventLog.WriteEntry("CSVConverterService Started.");

            myTimer = new System.Timers.Timer(1 * 60 * 1000); // every 1 minutes
            myTimer.Elapsed += new System.Timers.ElapsedEventHandler(runTheJobHandler);
            myTimer.Start();
        }

        private void runTheJobHandler(object sender, System.Timers.ElapsedEventArgs e)
        {
            string sourceFolder = @"D:\source";
            string destFolder = @"D:\destination";
            string archiveFolder = @"D:\archive";

            // transform each file in source folder
            foreach (string sourceFile in Directory.GetFiles(sourceFolder, "*.csv", SearchOption.TopDirectoryOnly))
            {
                CSVLoader csvLoader = new CSVLoader(sourceFile);
                csvLoader.Transform(destFolder + @"\" + Path.GetFileName(sourceFile)); // write to destination folder
                File.Move(sourceFile, archiveFolder + @"\" + Path.GetFileName(sourceFile)); // move the source file to prevent further processing
            }
        }

        protected override void OnContinue()
        {
            myEventLog.WriteEntry("CSVConverterService Continued.");
        }

        protected override void OnStop()
        {
            myEventLog.WriteEntry("CSVConverterService Stopped.");
        }
    }
}
