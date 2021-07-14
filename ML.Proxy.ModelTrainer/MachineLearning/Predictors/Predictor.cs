using Microsoft.ML;
using System;
using System.Collections.Generic;
using System.IO;
using ML.Proxy.DataModels;
using ML.Proxy.DataModels.ONNX;

namespace ML.Proxy.ModelTrainer.MachineLearning.Predictors
{
    public class Predictor
    {
        private readonly MLContext _mlContext;

        private ITransformer _model;

        public Predictor()
        {
            // Durch das Setzen einer fixen Zahl, wird der Output deterministisch
            // (Bedeutet: Man erhält auch bei mehreren Durchläufen und Trainings stets
            // dasselbe Ergebnis)
            _mlContext = new MLContext(111);
        }

        public NetworkAttackPrediction Predict<T>(T newSample) where T : class
        {
            LoadModel<T>();

            var predictionEngine = _mlContext.Model
                .CreatePredictionEngine<T, NetworkAttackPrediction>(_model);

            return predictionEngine.Predict(newSample);
        }

        public NetworkAttackPredictionOnnxOutput PredictWithOnnx<T>(T newSample) where T : class
        {
            LoadOnnxModel<T>();

            var predictionEngine = _mlContext.Model
                .CreatePredictionEngine<T, NetworkAttackPredictionOnnxOutput>(_model);

            return predictionEngine.Predict(newSample);
        }

        private void LoadModel<T>()
        {
            var ModelPath = Path.Combine(AppContext.BaseDirectory, $"{typeof(T).Name}-classification.zip");

            if (!File.Exists(ModelPath))
            {
                throw new FileNotFoundException($"File {ModelPath} does not exist.");
            }

            using (var stream = new FileStream(ModelPath, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                _model = _mlContext.Model.Load(stream, out _);
            }

            if (_model == null)
            {
                throw new Exception($"Failed to load Model.");
            }
        }

        private void LoadOnnxModel<T>() where T : class
        {
            var ModelPath = Path.Combine(AppContext.BaseDirectory, $"{typeof(T).Name}-classification.onnx");

            if (!File.Exists(ModelPath))
            {
                throw new FileNotFoundException($"File {ModelPath} does not exist.");
            }

            var data = _mlContext.Data.LoadFromEnumerable(new List<T>());

            _model = _mlContext.Transforms.ApplyOnnxModel(modelFile: ModelPath).Fit(data);

            if (_model == null)
            {
                throw new Exception($"Failed to load Model.");
            }
        }
    }
}
