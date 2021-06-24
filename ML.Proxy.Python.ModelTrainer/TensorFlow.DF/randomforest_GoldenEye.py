import pandas as pd
import sklearn as skl
import tensorflow as tf
import tensorflow_decision_forests as tfdf
import numpy as np
import os
import tensorflow as tf
import math
from wurlitzer import sys_pipes


data_LOIC = pd.read_csv('./Data/Tuesday-20-02-2018_LOIC-Attack.csv')
data_LOIC.rename(columns={
    'Bwd Pkt Len Std':'bwd_pkt_len_std',
    'Pkt Size Avg':'pkt_size_avg',
    'Flow Duration':'flow_duration',
    'Flow IAT Std':'flow_iat_std',
    'Label':'label'}, 
    inplace=True)

data_Slowloris = pd.read_csv('./Data/Thursday-15-02-2018_Slowloris-Attack.csv')
data_Slowloris.rename(columns={
    'Flow Duration':'flow_duration',
    'Bwd IAT Mean':'bwd_iat_mean',
    'Fwd IAT Min':'fwd_iat_min',
    'Fwd IAT Mean':'fwd_iat_mean',
    'Label':'label'
},
inplace=True)

data_GoldenEye = pd.read_csv('./Data/Thursday-15-02-2018_GoldenEye-Attack.csv')
data_GoldenEye.rename(columns={
    'Bwd Pkt Len Std':'bwd_pkt_len_std',
    'Flow IAT Min':'flow_iat_min',
    'Fwd IAT Min':'fwd_iat_min',
    'Flow IAT Mean':'flow_iat_mean',
    'Label':'label'
},
inplace=True)

# training_data_LOIC = data_LOIC.sample(frac=0.8, random_state=25)
# testing_data_LOIC = data_LOIC.drop(training_data_LOIC.index)

# training_data_Slowloris = data_Slowloris.sample(frac=0.8, random_state=25)
# testing_data_Slowloris = data_Slowloris.drop(training_data_Slowloris.index)

# training_data_GoldenEye = data_GoldenEye.sample(frac=0.8, random_state=25)
# testing_data_GoldenEye = data_GoldenEye.drop(training_data_GoldenEye.index)

label = 'label'

def split_dataset(dataset,  test_ratio=0.30):
    """Splits a panda dataframe in two dataframes."""
    test_indices = np.random.rand(len(dataset)) < test_ratio
    return dataset[~test_indices], dataset[test_indices]

training_data_LOIC, testing_data_LOIC = split_dataset(data_LOIC)
training_data_Slowloris, testing_data_Slowloris = split_dataset(data_Slowloris)
training_data_GoldenEye, testing_data_GoldenEye = split_dataset(data_GoldenEye)

print("{} examples in training, {} examples for testing.".format(
    len(training_data_LOIC), len(testing_data_LOIC)))

# Converting Panda Dataframe into Tensorflow Dataset
training_dataset_LOIC = tfdf.keras.pd_dataframe_to_tf_dataset(training_data_LOIC, label=label)
testing_dataset_LOIC = tfdf.keras.pd_dataframe_to_tf_dataset(testing_data_LOIC, label=label)

# Creating the Random Forest Model
model = tfdf.keras.RandomForestModel()

# Train the model
with sys_pipes():
    model.fit(x=training_dataset_LOIC)

evaluation = model.evaluate(testing_dataset_LOIC, return_dict=True)

print()

for name, value in evaluation.items():
    print(f"{name}: {value:.4f}")

tfdf.model_plotter.plot_model_in_colab(model, tree_idx=0, max_depth=3)

model.summary()
