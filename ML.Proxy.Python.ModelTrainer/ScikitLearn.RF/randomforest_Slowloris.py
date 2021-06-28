import pandas as pd
import numpy as np
from sklearn.model_selection import train_test_split
from sklearn.ensemble import RandomForestRegressor
from matplotlib import pyplot as plt

###############################################################################
# Random Forest Classification Model (Scikit-Learn)                           #
# For Slowloris Dataset                                                       #
# Based on the Implementation of:                                             #
# https://mljar.com/blog/feature-importance-in-random-forest/                 #
###############################################################################

df = pd.read_csv('.\Data\Thursday-15-02-2018_Slowloris-Attack.csv')

df_slowloris_columns = df.columns.values.tolist()

column_dict = { }

for n in df_slowloris_columns:
    old_value = str(n)
    new_value = old_value.lower()
    new_value = new_value.replace(" ","_")
    column_dict[old_value] = new_value

df.rename(columns=column_dict,inplace=True)

df = df.replace([np.inf, -np.inf], np.nan)
df = df.interpolate()

nan_count = df.isna().sum().sum()
print(f"Count of NaN in Dataset: {nan_count}")

# Remove Column Label 
X = df.drop(['label'],axis=1)
y = np.array(df[['label']]).flatten()

X_train, X_test, y_train, y_test = train_test_split(X,y, test_size=0.25, random_state=12)

# Fit the Random Forest Regressor
rf = RandomForestRegressor()
rf.fit(X_train, y_train)
