using Microsoft.ML.Data;
using ML.Proxy.ModelTrainer.MachineLearning.Common;
using ML.Proxy.ModelTrainer.MachineLearning.Predictors;
using ML.Proxy.ModelTrainer.MachineLearning.Trainers;
using ML.Proxy.DataModels;
using ML.Proxy.DataModels.ONNX;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

/*
* ML.NET Random Forest Algorithm Trainer
* Based on the implementation of:
* https://rubikscode.net/2021/03/01/machine-learning-with-ml-net-random-forest/ 
*/

namespace ML.Proxy.ModelTrainer
{
    class Program
    {
        static void Main(string[] args)
        {
            var newSamplesGoldenEye = new List<GoldenEyeTrafficData>
            {
                // true
                new GoldenEyeTrafficData
                {
                    //BwdPktLenStd = 486.0f,
                    FlowIATMin = 3f,
                    FwdIATMin = 282f,
                    FlowIATMean = 856762.714285714f,
                },
                // false
                new GoldenEyeTrafficData
                {
                    //BwdPktLenStd = 0.0f,
                    FlowIATMin = 56320514f,
                    FwdIATMin = 56320514f,
                    FlowIATMean = 56320570.5f,
                },
                // false
                new GoldenEyeTrafficData
                {
                    //BwdPktLenStd = 430.0986044197f,
                    FlowIATMin = 3f,
                    FwdIATMin = 3f,
                    FlowIATMean = 188317.142857143f,
                },
                // true
                new GoldenEyeTrafficData
                {
                    //BwdPktLenStd = 561.1844616523f,
                    FlowIATMin = 6f,
                    FwdIATMin = 23f,
                    FlowIATMean = 4583.5714285714f,
                }
            };
            var newSamplesSlowloris = new List<SlowlorisTrafficData> 
            {
                // true
                new SlowlorisTrafficData
                {
                    FlowDuration = 99999803f,
                    //BwdIATMean = 99999801.0f,
                    FwdIATMin = 99999801f,
                    FwdIATMean = 99999801.0f,
                },
                // true
                new SlowlorisTrafficData
                {
                    FlowDuration = 9f,
                    //BwdIATMean = 0.0f,
                    FwdIATMin = 0f,
                    FwdIATMean = 0.0f,
                },
                // false
                new SlowlorisTrafficData
                {
                    FlowDuration = 5236352f,
                    //BwdIATMean = 31271.3333333333f,
                    FwdIATMin = 46f,
                    FwdIATMean = 1047270.4f,
                },
                // false
                new SlowlorisTrafficData
                {
                    FlowDuration = 116042452f,
                    //BwdIATMean = 1056011.29245283f,
                    FwdIATMin = 28f,
                    FwdIATMean = 5274656.90909091f,
                }
            };
            var newSamplesLOIC = new List<LOICTrafficData>
            {
                // true
                new LOICTrafficData 
                {
                    //BwdPktLenStd = 482.0f,
                    PktSizeAvg = 140.5714286f,
                    FlowDuration = 1001475f,
                    FlowIATStd = 408593.2899f,
                },
                // false
                new LOICTrafficData
                {
                    //BwdPktLenStd = 364.1864907f,
                    PktSizeAvg = 123.68f,
                    FlowDuration = 2036636f,
                    FlowIATStd = 104030.9467f,
                },
                // true
                new LOICTrafficData
                {
                    //BwdPktLenStd = 0.0f,
                    PktSizeAvg = 0.0f,
                    FlowDuration = 9220277f,
                    FlowIATStd = 0.0f,
                },
                // false
                new LOICTrafficData
                {
                    //BwdPktLenStd = 0.0f,
                    PktSizeAvg = 62.5f,
                    FlowDuration = 322f,
                    FlowIATStd = 0.0f,
                }
            };

            // true
            var newSampleGoldenEye = new GoldenEyeTrafficData
            {
                //BwdPktLenStd = 486.0f,
                FlowIATMin = 3f,
                FwdIATMin = 282f,
                FlowIATMean = 856762.714285714f,
            };
            // true
            var newSampleSlowloris = new SlowlorisTrafficData
            {
                FlowDuration = 3f,
                //BwdIATMean = 0.0f,
                FwdIATMin = 0f,
                FwdIATMean = 0.0f,
            };
            // true
            var newSampleLOIC = new LOICTrafficData
            {
                //BwdPktLenStd = 482.0f,
                PktSizeAvg = 140.5714286f,
                FlowDuration = 1001475f,
                FlowIATStd = 408593.2899f,
            };

            var trainers = new List<ITrainerBase>
            {
                new RandomForestTrainer(2,5),
                new RandomForestTrainer(5,10),
                new RandomForestTrainer(10,20),
                new RandomForestTrainer(20,40)
            };

            trainers.ForEach(t => TrainEvaluatePredict(t, newSampleGoldenEye, @"..\..\..\Data\Thursday-15-02-2018_GoldenEye-Attack.csv"));
            trainers.ForEach(t => TrainEvaluatePredict(t, newSampleSlowloris, @"..\..\..\Data\Thursday-15-02-2018_Slowloris-Attack.csv"));
            trainers.ForEach(t => TrainEvaluatePredict(t, newSampleLOIC, @"..\..\..\Data\Tuesday-20-02-2018_LOIC-Attack.csv"));

            LoadAndPredictWithOnnx(newSamplesGoldenEye);
            LoadAndPredictWithOnnx(newSamplesSlowloris);
            LoadAndPredictWithOnnx(newSamplesLOIC);
        }

        static void TrainEvaluatePredict<T>(ITrainerBase trainer, T newSample, string csvPath) where T : class
        {
            var filePath = @"..\..\..\Result.txt";

            trainer.Fit<T>($"{csvPath}");

            var modelMetrics = trainer.Evaluate();

            trainer.Save<T>();
            trainer.SaveAsOnnx<T>();

            var predictor = new Predictor();
            var prediction = predictor.Predict(newSample);

            if (!File.Exists(filePath))
            {
                using (StreamWriter sw = new StreamWriter(filePath))
                {
                    PrintMetrics(trainer, newSample, prediction, sw, modelMetrics);
                }
            }
            else
            {
                using (StreamWriter sw = new StreamWriter(filePath, append: true))
                {

                    PrintMetrics(trainer, newSample, prediction, sw, modelMetrics);
                }
            }
            
        }

        // Load the ONNX model and make a prediction
        static void LoadAndPredictWithOnnx<T>(List<T> newSamples) where T : class
        {
            var filePath = @"..\..\..\Result-ONNX-Prediction.txt";

            var predictor = new Predictor();
            foreach(var sample in newSamples)
            {
                var prediction = predictor.PredictWithOnnx(sample);

                if (!File.Exists(filePath))
                {
                    using (StreamWriter sw = new StreamWriter(filePath))
                    {
                        PrintPredictionResults(sample, prediction, sw);
                    }
                }
                else
                {
                    using (StreamWriter sw = new StreamWriter(filePath, append: true))
                    {
                        PrintPredictionResults(sample, prediction, sw);
                    }
                }
            }
        }

        // Pretty-print BinaryClassificationMetrics objects.
        private static void PrintMetrics<T>(ITrainerBase trainer, T sample, NetworkAttackPrediction prediction, StreamWriter sw, BinaryClassificationMetrics metrics)
        {
            Console.SetOut(sw);
            Console.WriteLine("*************************************************************");
            Console.WriteLine($" { trainer.Name } for { typeof(T).Name }-Attack");
            Console.WriteLine("*************************************************************");

            Console.WriteLine($"Accuracy: {metrics.Accuracy:F2}");
            Console.WriteLine($"AUC: {metrics.AreaUnderRocCurve:F2}");
            Console.WriteLine($"F1 Score: {metrics.F1Score:F2}");
            Console.WriteLine($"Negative Precision: " +
                $"{metrics.NegativePrecision:F2}");

            Console.WriteLine($"Negative Recall: {metrics.NegativeRecall:F2}");
            Console.WriteLine($"Positive Precision: " +
                $"{metrics.PositivePrecision:F2}");

            Console.WriteLine($"Positive Recall: {metrics.PositiveRecall:F2}\n");
            Console.WriteLine(metrics.ConfusionMatrix.GetFormattedConfusionTable());

            Console.WriteLine("*************************************************************");
            Console.WriteLine($"Prediction: {prediction.PredictedLabel:#.##}");
            Console.WriteLine("*************************************************************");
            Console.SetOut(Console.Out);
        }

        // Print Results of Predictions from ONNX Models
        private static void PrintPredictionResults<T>(T sample, NetworkAttackPredictionOnnxOutput prediction, StreamWriter sw) where T : class
        {
            Console.SetOut(sw);
            Console.WriteLine("*************************************************************");
            Console.WriteLine($" Prediction of ONNX-Model for { typeof(T).Name }-Attack Data");
            Console.WriteLine($" Prediction: {prediction.PredictedLabel.First():#.##}");
            Console.WriteLine("*************************************************************");
            Console.SetOut(Console.Out);
        }
    }
}
