using Microsoft.ML;
using Microsoft.ML.Trainers.FastTree;
using ML.Proxy.ModelTrainer.MachineLearning.Common;

namespace ML.Proxy.ModelTrainer.MachineLearning.Trainers
{
    public class RandomForestTrainer : TrainerBase<FastForestBinaryModelParameters>
    {
        public RandomForestTrainer(int numberOfLeaves, int numberOfTrees) : base()
        {
            Name = $"Random Forest: {numberOfLeaves}-{numberOfTrees}";
            _model = MlContext.BinaryClassification.Trainers.FastForest(
                numberOfLeaves: numberOfLeaves,
                numberOfTrees: numberOfTrees);
        }
    }
}
