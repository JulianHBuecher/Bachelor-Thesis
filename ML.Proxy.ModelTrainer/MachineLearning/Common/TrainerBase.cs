using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.ML.Trainers;
using Microsoft.ML.Transforms;
using ML.Proxy.ModelTrainer.MachineLearning.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ML.Proxy.ModelTrainer.MachineLearning.Common
{
    /// <summary>
    /// Base class for Trainers.
    /// This class exposes methods for training, evaluating and saving ML Models.
    /// Classes that inherit this class need to assign concrete model and name.
    /// </summary>
    /// <typeparam name="T">Custom Parameter for Model Data</typeparam>
    public abstract class TrainerBase<T> : ITrainerBase where T : class
    {
        // Used by class that inherits this one to add the name of the algorithm
        public string Name { get; protected set; }

        // Path where the model ist stored
        protected static string ModelPath => Path.Combine(AppContext.BaseDirectory, $"classification.mdl");

        protected readonly MLContext MlContext;

        // Contains loaded data (is split into train and test datasets)
        protected DataOperationsCatalog.TrainTestData _dataSplit;
        // Used by the child classes to define which ML-algorithm is used in this field
        protected ITrainerEstimator<BinaryPredictionTransformer<T>, T> _model;
        // Contains the trained model
        protected ITransformer _trainedModel;

        protected TrainerBase()
        {
            MlContext = new MLContext(111);
        }

        /// <summary>
        /// Train model on defined data.
        /// </summary>
        /// <param name="trainingFileName">File Name of Training Data.</param>
        public void Fit<TParam>(string trainingFileName)
        {
            if (!File.Exists(trainingFileName))
            {
                throw new FileNotFoundException($"File {trainingFileName} does not exist.");
            }

            _dataSplit = LoadAndPrepareData<TParam>(trainingFileName);
            var dataProcessPipeline = BuildDataProcessingPipeline<TParam>();
            var trainingPipeline = dataProcessPipeline.Append(_model);

            _trainedModel = trainingPipeline.Fit(_dataSplit.TrainSet);
        }

        /// <summary>
        /// Evaluate trained model.
        /// </summary>
        /// <returns>Model performance.</returns>
        public BinaryClassificationMetrics Evaluate()
        {
            var testSetTransform = _trainedModel.Transform(_dataSplit.TestSet);

            return MlContext.BinaryClassification.EvaluateNonCalibrated(testSetTransform);
        }

        /// <summary>
        /// Save Model in the file.
        /// </summary>
        public void Save()
        {
            MlContext.Model.Save(_trainedModel, _dataSplit.TrainSet.Schema, ModelPath);
        }

        public void Save<TParam>()
        {
            // Path where the model ist stored
            var modelPath = Path.Combine(AppContext.BaseDirectory, $"{typeof(TParam).Name}-classification.mdl");

            MlContext.Model.Save(_trainedModel, _dataSplit.TrainSet.Schema, modelPath);
        }

        public void SaveAsOnnx<TParam>()
        {
            // Path where the model ist stored
            using (var modelPath = File.Open(Path.Combine(AppContext.BaseDirectory, $"{typeof(TParam).Name}-classification.onnx"), FileMode.OpenOrCreate))
            {
                MlContext.Model.ConvertToOnnx(_trainedModel, _dataSplit.TrainSet, modelPath);
            }
        }

        private EstimatorChain<NormalizingTransformer> BuildDataProcessingPipeline<TParam>()
        {
            var inputColumnNames = new List<string>();

            switch (typeof(TParam).Name)
            {
                case nameof(GoldenEyeTrafficData):
                    inputColumnNames.AddRange(new List<string>
                    {
                        nameof(GoldenEyeTrafficData.BwdPktLenStd),
                        nameof(GoldenEyeTrafficData.FlowIATMin),
                        nameof(GoldenEyeTrafficData.FwdIATMin),
                        nameof(GoldenEyeTrafficData.FlowIATMean)
                    });
                    break;
                case nameof(LOICTrafficData):
                    inputColumnNames.AddRange(new List<string>
                    {
                        nameof(LOICTrafficData.BwdPktLenStd),
                        nameof(LOICTrafficData.PktSizeAvg),
                        nameof(LOICTrafficData.FlowDuration),
                        nameof(LOICTrafficData.FlowIATStd)
                    });
                    break;
                case nameof(SlowlorisTrafficData):
                    inputColumnNames.AddRange(new List<string>
                    {
                        nameof(SlowlorisTrafficData.FlowDuration),
                        nameof(SlowlorisTrafficData.BwdIATMean),
                        nameof(SlowlorisTrafficData.FwdIATMin),
                        nameof(SlowlorisTrafficData.FwdIATMean)
                    });
                    break;
                default:
                    throw new ArgumentException("The given class is not a valid data model.");
            }

            var dataProcessPipeline = MlContext.Transforms.Concatenate("Features", inputColumnNames.ToArray())
                .Append(MlContext.Transforms.NormalizeMinMax("Features", "Features"))
                .AppendCacheCheckpoint(MlContext);

            return dataProcessPipeline;
        }

        private DataOperationsCatalog.TrainTestData LoadAndPrepareData<TParam>(string trainingFileName)
        {
            var trainingDataView = MlContext.Data.LoadFromTextFile<TParam>(trainingFileName, hasHeader: true, separatorChar: ',');
            return MlContext.Data.TrainTestSplit(trainingDataView, testFraction: 0.3);
        }
    }
}
