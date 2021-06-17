using CsvHelper;
using Microsoft.ML;
using ML.Proxy.ModelTrainer.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ML.Proxy.ModelTrainer
{
    class Program
    {
        //static readonly string _dataPath = Path.Combine(Environment.CurrentDirectory, "..\\..\\..\\Data", "product-sales.csv");
        //static readonly string _dataPath = Path.Combine(Environment.CurrentDirectory, "..\\..\\..\\Data", "Thuesday-20-02-2018_TrafficForML_CICFlowMeter.csv");
        const int _docsize = 36;

        static void Main(string[] args)
        {
            // Starting Point
            // Initializes all ML.NET operations
            MLContext mlContext = new MLContext();

            // Getting Knowledge about the CSV File
            //InspectCsvFile("D:\\TestEnvironmentPKCE\\ML.Proxy.ModelTrainer\\Data\\Thuesday-20-02-2018_TrafficForML_CICFlowMeter.csv");

            var csv = ReadInCsvFileAndReturnRecords<dynamic>("D:\\TestEnvironmentPKCE\\ML.Proxy.ModelTrainer\\Data\\Thuesday-20-02-2018_TrafficForML_CICFlowMeter.csv");
            var records = csv.records.ToList();

            for (var i = 0; i < 1000; i++)
            {
                Console.WriteLine((string)records[i]);
            }


            // Loading the training data
            //IDataView dataView = mlContext.Data.LoadFromTextFile<NetworkTrafficData>(path: _dataPath, hasHeader: true, separatorChar: ',');
            //IDataView dataView = mlContext.Data.LoadFromTextFile<ProductSalesData>(path: _dataPath, hasHeader: true, separatorChar: ',');

            //DetectSpike(mlContext, _docsize, dataView);

        }

        static IDataView CreateEmptyDataView(MLContext mlContext)
        {
            // Create empty DataView. Just needed for the schema to call Fit() for the time series transforms
            IEnumerable<ProductSalesData> enumerableData = new List<ProductSalesData>();
            return mlContext.Data.LoadFromEnumerable(enumerableData);
        }


        static void DetectSpike(MLContext mlContext, int docSize, IDataView productSales)
        {
            var iidSpikeEstimator = mlContext.Transforms.DetectIidSpike(
                outputColumnName: nameof(ProductSalesPrediction.Prediction), 
                inputColumnName: nameof(ProductSalesData.numSales), 
                confidence: 95.0, 
                pvalueHistoryLength: docSize / 4);

            ITransformer iidSpikeTransform = iidSpikeEstimator.Fit(CreateEmptyDataView(mlContext));

            var transformedData = iidSpikeTransform.Transform(productSales);

            var predictions = mlContext.Data.CreateEnumerable<ProductSalesPrediction>(transformedData, reuseRowObject: false);

            Console.WriteLine("Alert\tScore\tP-Value");

            foreach (var p in predictions)
            {
                var results = $"{p.Prediction[0]}\t{p.Prediction[1]:f2}\t{p.Prediction[2]:F2}";

                if (p.Prediction[0] == 1)
                {
                    results += " <-- Spike detected";
                }

                Console.WriteLine(results);
            }
            Console.WriteLine("");
        }

        static (string[] headers, IEnumerable<T> records) ReadInCsvFileAndReturnRecords<T>(string _dataPath)
        {
            //using (var reader = new StreamReader(_dataPath))
            //using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            //{
            //    return (csv.HeaderRecord, csv.GetRecords<T>());
            //}
            var reader = new StreamReader(_dataPath);
            var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
            
            return (csv.HeaderRecord, csv.GetRecords<T>());
        }

        static int CountRowsInRecords<T>(IEnumerable<T> records)
        {
            var entries = 0;
            foreach (var r in records)
            {
                entries++;
            }
            return entries;
        }

        static void InspectCsvFile(string _dataPath)
        {
            var csv = ReadInCsvFileAndReturnRecords<dynamic>(_dataPath);

            var listEntries = CountRowsInRecords(csv.records);

            WriteConfigToPropertyFile(csv.headers, listEntries);
        }

        static void WriteConfigToPropertyFile(string[] headers, int listEntries)
        {
            var fileContent = new List<string> { $"Reading Process started at: {DateTime.Now.ToString("hh:mm:ss.fff")}\n\n" };

            var timer = new Stopwatch();
            timer.Start();

            timer.Stop();
            fileContent.AddRange(new List<string>
            {
                $"Reading process ended at {DateTime.Now.ToString("hh:mm:ss.fff")}. Required Time: {timer.Elapsed}",
                $"The document has {listEntries} entries\n\n",
                "The following headers are present: \n\n",
            });

            foreach (var header in headers)
            {
                fileContent.Add($"{header}; ");
            }
            File.WriteAllLines("FilePropertiesOfMLFile.txt", fileContent);
        }

        static void WriteGivenRowsToFile<T>(IEnumerable<T> records, string[] headers)
        {
            var fileContent = new List<string>();
        }
    }
}
