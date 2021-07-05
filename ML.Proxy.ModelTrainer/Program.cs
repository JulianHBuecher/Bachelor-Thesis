using Microsoft.ML.Data;
using ML.Proxy.ModelTrainer.MachineLearning.Common;
using ML.Proxy.ModelTrainer.MachineLearning.Models;
using ML.Proxy.ModelTrainer.MachineLearning.Predictors;
using ML.Proxy.ModelTrainer.MachineLearning.Trainers;
using System;
using System.Collections.Generic;
using System.IO;
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
            var newSampleGoldenEye = new GoldenEyeTrafficData
            {
                BwdPktLenStd = 486.0f,
                FlowIATMin = 3f,
                FwdIATMin = 282f,
                FlowIATMean = 856762.714285714f,
            };
            var newSampleSlowloris = new SlowlorisTrafficData
            {
                FlowDuration = 29115114f,
                BwdIATMean = 14557555.5f,
                FwdIATMin = 150f,
                FwdIATMean = 9705037.33333333f,
            };
            var newSampleLOIC = new LOICTrafficData
            {
                BwdPktLenStd = 482.0f,
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
    }
}
