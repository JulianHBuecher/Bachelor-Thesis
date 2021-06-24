import pandas as pd
import sklearn as skl
import tensorflow as tf
import tensorflow_decision_forests as tfdf
import numpy as np
import os
import tensorflow as tf
import math
from wurlitzer import sys_pipes
import matplotlib.pyplot as plt

from IPython.core.magic import register_line_magic
from IPython.display import Javascript

###############################################################################
# Random Forest Classification Model (TensorFlow)                             #
# For LOIC Dataset                                                            #
# Based on the Implementation of:                                             #
# https://mljar.com/blog/feature-importance-in-random-forest/                 #
###############################################################################

data_LOIC = pd.read_csv('../Data/Tuesday-20-02-2018_LOIC-Attack.csv')
data_LOIC.rename(columns={
    'Bwd Pkt Len Std':'bwd_pkt_len_std',
    'Pkt Size Avg':'pkt_size_avg',
    'Flow Duration':'flow_duration',
    'Flow IAT Std':'flow_iat_std',
    'Label':'label'}, 
    inplace=True)

label = 'label'

def split_dataset(dataset,  test_ratio=0.30):
    """Splits a panda dataframe in two dataframes."""
    test_indices = np.random.rand(len(dataset)) < test_ratio
    return dataset[~test_indices], dataset[test_indices]

training_data_LOIC, testing_data_LOIC = split_dataset(data_LOIC)

print("{} examples in training, {} examples for testing.".format(
    len(training_data_LOIC), len(testing_data_LOIC)))

# Converting Panda Dataframe into Tensorflow Dataset
print("Converting Panda Dataframe into TensorFlow Dataset: ")
training_dataset_LOIC = tfdf.keras.pd_dataframe_to_tf_dataset(training_data_LOIC, label=label)
testing_dataset_LOIC = tfdf.keras.pd_dataframe_to_tf_dataset(testing_data_LOIC, label=label)

# Creating the Random Forest Model
model = tfdf.keras.RandomForestModel()
model.compile(metrics=["accuracy"])

# Train the model
print("Training the Model: ")
with sys_pipes():
    model.fit(x=training_dataset_LOIC)

print("Evaluating the Model: ")
evaluation = model.evaluate(testing_dataset_LOIC, return_dict=True)

print()

for name, value in evaluation.items():
    print(f"{name}: {value:.4f}")


# Saving the Model for later reusage
model.save("../Models/loic_model")

# Plotting the first tree of the Random Forest
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

plt.savefig('../Data/Visualized/LOIC_Model.png',bbox_inches="tight")
plt.clf()