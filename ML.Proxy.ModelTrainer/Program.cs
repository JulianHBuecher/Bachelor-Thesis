using Microsoft.ML.Data;
using ML.Proxy.ModelTrainer.MachineLearning.Common;
using ML.Proxy.ModelTrainer.MachineLearning.Predictors;
using ML.Proxy.ModelTrainer.MachineLearning.Trainers;
using ML.Proxy.ModelTrainer.Models;
using System;
using System.Collections.Generic;
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
                BwdPktLenStd = 486.0F,
                FlowIATMin = 6,
                FwdIATMin = 316,
                FlowIATMean = 858636.285714286F,
            };
            var newSampleSlowloris = new SlowlorisTrafficData
            {
                FlowDuration = 25737760,
                BwdIATMean = 1671248.85714286F,
                FwdIATMin = 2396,
                FwdIATMean = 1715850.66666667F,
            };
            var newSampleLOIC = new LOICTrafficData
            {
                BwdPktLenStd = 482.0F,
                PktSizeAvg = 140.5714286F,
                FlowDuration = 45577,
                FlowIATStd = 18391.67485F,
            };

            var trainers = new List<ITrainerBase>
            {
                new RandomForestTrainer(2,5),
                new RandomForestTrainer(5,10),
                new RandomForestTrainer(10,20),
                new RandomForestTrainer(20,40),
                new RandomForestTrainer(50,100)
            };

            trainers.ForEach(t => TrainEvaluatePredict(t, newSampleGoldenEye, @"..\..\..\Data\Thursday-15-02-2018_GoldenEye-Attack.csv"));
            trainers.ForEach(t => TrainEvaluatePredict(t, newSampleSlowloris, @"..\..\..\Data\Thursday-15-02-2018_Slowloris-Attack.csv"));
            trainers.ForEach(t => TrainEvaluatePredict(t, newSampleLOIC, @"..\..\..\Data\Tuesday-20-02-2018_LOIC-Attack.csv"));
        }

        static void TrainEvaluatePredict<T>(ITrainerBase trainer, T newSample, string filePath) where T : class
        {
            Console.WriteLine("*************************************************************");
            Console.WriteLine($" { trainer.Name } for { typeof(T).Name }-Attack");
            Console.WriteLine("*************************************************************");

            trainer.Fit<T>($"{filePath}");

            var modelMetrics = trainer.Evaluate();

            PrintMetrics(modelMetrics);

            trainer.Save<T>();

            var predictor = new Predictor();
            var prediction = predictor.Predict(newSample);
            Console.WriteLine("*************************************************************");
            Console.WriteLine($"Prediction: {prediction.PredictLabel:#.##}");
            Console.WriteLine("*************************************************************");
        }

        // Pretty-print BinaryClassificationMetrics objects.
        private static void PrintMetrics(BinaryClassificationMetrics metrics)
        {
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
        }
    }
}
