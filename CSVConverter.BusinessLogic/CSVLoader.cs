using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;
using CsvHelper.Configuration;

namespace CSVConverter.BusinessLogic
{
    public class CSVLoader
    {
        public string FileName { get; set; }

        public CSVLoader()
        {
            FileName = "";
        }

        public CSVLoader(string fileName)
        {
            FileName = fileName;
        }

        public void Transform(string targetFile)
        {
            Transform(FileName, targetFile);
        }

        public void Transform(string sourceFile, string targetFile)
        {
            CsvConfiguration configuration = new CsvConfiguration(CultureInfo.InvariantCulture);
            configuration.Encoding = Encoding.UTF8;
            configuration.HasHeaderRecord = true;

            var records = new List<dynamic>();

            // load the csv into the list
            using (var reader = new StreamReader(sourceFile))
            {
                using (var csv = new CsvReader(reader, configuration, false))
                {
                    records = csv.GetRecords<dynamic>().ToList();
                }
            }

            foreach (var record in records)
            {
                var converter = new MainConverter(record.data1.ToString(), record.data2.ToString());
                record.data1 = converter.newString.recordOne;
                record.data2 = converter.newString.recordTwo;
            }

            // write the updated list into new CSV
            using (var writer = new StreamWriter(targetFile))
            {
                using (var csv = new CsvWriter(writer, configuration, false))
                {
                    csv.WriteRecords(records);
                }
            }
        }
    }
}
