using Microsoft.ML;
using ML.Proxy.ModelTrainer.MachineLearning.Models;
using System;
using System.IO;

namespace ML.Proxy.ModelTrainer.MachineLearning.Predictors
{
    public class Predictor
    {
        protected static string ModelPath => Path.Combine(AppContext.BaseDirectory, "classification.mdl");
        private readonly MLContext _mlContext;

        private ITransformer _model;

        public Predictor()
        {
            _mlContext = new MLContext(111);
        }

        public NetworkAttackPrediction Predict<T>(T newSample) where T : class
        {
            LoadModel();

            var predictionEngine = _mlContext.Model
                .CreatePredictionEngine<T, NetworkAttackPrediction>(_model);

            return predictionEngine.Predict(newSample);        
        }

        private void LoadModel()
        {
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
    }
}
