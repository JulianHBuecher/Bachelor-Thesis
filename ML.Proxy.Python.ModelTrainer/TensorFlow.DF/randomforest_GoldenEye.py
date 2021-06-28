import pandas as pd
import tensorflow_decision_forests as tfdf
import numpy as np
from wurlitzer import sys_pipes
import matplotlib.pyplot as plt

from IPython.core.magic import register_line_magic
from IPython.display import Javascript

###############################################################################
# Random Forest Classification Model (TensorFlow)                             #
# For GoldenEye Dataset                                                       #
# Based on the Implementation of:                                             #
# https://mljar.com/blog/feature-importance-in-random-forest/                 #
###############################################################################

# Checking the version of installed TensorFlow Decision Forest
print(f"Found TensorFlow Decision Forests v{tfdf.__version__}")

data_GoldenEye = pd.read_csv('./Data/Thursday-15-02-2018_GoldenEye-Attack.csv')
data_GoldenEye.rename(columns={
    'Bwd Pkt Len Std':'bwd_pkt_len_std',
    'Flow IAT Min':'flow_iat_min',
    'Fwd IAT Min':'fwd_iat_min',
    'Flow IAT Mean':'flow_iat_mean',
    'Label':'label'
},
inplace=True)

label = 'label'

def split_dataset(dataset,  test_ratio=0.30):
    """Splits a panda dataframe in two dataframes."""
    test_indices = np.random.rand(len(dataset)) < test_ratio
    return dataset[~test_indices], dataset[test_indices]

training_data_GoldenEye, testing_data_GoldenEye = split_dataset(data_GoldenEye)

print("{} examples in training, {} examples for testing.".format(
    len(training_data_GoldenEye), len(testing_data_GoldenEye)))

# Converting Panda Dataframe into Tensorflow Dataset
print("Converting Panda Dataframe into TensorFlow Dataset: ")
training_dataset_GoldenEye = tfdf.keras.pd_dataframe_to_tf_dataset(training_data_GoldenEye, label=label)
testing_dataset_GoldenEye = tfdf.keras.pd_dataframe_to_tf_dataset(testing_data_GoldenEye, label=label)

# Creating the Random Forest Model
model = tfdf.keras.RandomForestModel()
model.compile(metrics=["accuracy"])

# Train the model
print("Training the Model: ")
with sys_pipes():
    model.fit(x=training_dataset_GoldenEye)

print("Evaluating the Model: ")
evaluation = model.evaluate(testing_dataset_GoldenEye, return_dict=True)

print()

for name, value in evaluation.items():
    print(f"{name}: {value:.4f}")

# Saving the Model for later reusage
model.save("../Models/goldeneye_model")

# Plotting the first tree of the Decision Forest
tfdf.model_plotter.plot_model_in_colab(model, tree_idx=0, max_depth=3)

model.summary()

# Showing the feature importances
model.make_inspector().variable_importances()

logs = model.make_inspector().training_logs()
plt.figure(figsize=(12,4))

plt.subplot(1,2,1)
plt.plot([log.num_trees for log in logs], [log.evaluation.accuracy for log in logs])
plt.xlabel("Number of trees")
plt.ylabel("Accuracy (out-of-bag)")

plt.subplot(1,2,2)
plt.plot([log.num_trees for log in logs], [log.evaluation.loss for log in logs])
plt.xlabel("Number of trees")
plt.ylabel("Logloss (out-of-bag)")

plt.savefig('../Data/Visualized/GoldenEye_Model.png',bbox_inches="tight")
plt.clf()
