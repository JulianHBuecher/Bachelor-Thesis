using Microsoft.ML.Data;

namespace ML.Proxy.ModelTrainer.MachineLearning.Common
{
    public interface ITrainerBase
    {
        string Name { get; }
        void Fit<TParam>(string trainingFileName);
        BinaryClassificationMetrics Evaluate();
        void Save();
        void Save<TParam>();
    }
}
