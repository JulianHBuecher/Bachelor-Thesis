import numpy as np
import pandas as pd
from sklearn.datasets import load_boston
from sklearn.model_selection import train_test_split
from sklearn.ensemble import RandomForestRegressor
from sklearn.inspection import permutation_importance
import shap
from matplotlib import pyplot as plt
import operator

plt.rcParams.update({'figure.figsize': (12.0,8.0)})
plt.rcParams.update({'font.size': 12})

df = pd.read_csv('.\Data\Thursday-15-02-2018_TrafficForML_CICFlowMeter.csv')

df_slowloris = df.drop(df[df.Label == 'DoS attacks-GoldenEye'].index)

df_slowloris['Label'] = df_slowloris['Label'].replace({'Benign': 0, 'DoS attacks-Slowloris': 1},value=None)

df_slowloris_columns = df_slowloris.columns.values.tolist()

column_dict = { }

for n in df_slowloris_columns:
    old_value = str(n)
    new_value = old_value.lower()
    new_value = new_value.replace(" ","_")
    column_dict[old_value] = new_value

df_slowloris.rename(columns=column_dict,inplace=True)

df_slowloris.to_csv('.\Data\Optimized\Thursday-15-02-2018_Slowloris-Attack.csv', index=False)

df_slowloris = df_slowloris.replace([np.inf, -np.inf], np.nan)
df_slowloris = df_slowloris.interpolate()

nan_count = df_slowloris.isna().sum().sum()
print(f"Count of NaN in Dataset: {nan_count}")

X = df_slowloris.drop(['label','timestamp'],axis=1)
y = np.array(df_slowloris[['label']]).flatten()

print(f"Benign Traffic: {int((y == 0).sum())}, Malicious Attacks: {int((y == 1).sum())}")
print(f"Rows in total: {len(y)}")

X_train, X_test, y_train, y_test = train_test_split(X,y, test_size=0.25, random_state=12)

# Fit the Random Forest Regressor
rf = RandomForestRegressor()
rf.fit(X_train, y_train)

axis_names = list(column_dict.values())
axis_names = [e for e in axis_names if e not in {'timestamp','label'}]
axis_names_array = np.array(axis_names)

plt.barh(axis_names_array, rf.feature_importances_)
plt.xlabel("Random Forest Feature Importance for Slowloris Dataset")
plt.savefig('./Data/Visualized/Slowloris/FI_Slowloris.png',bbox_inches="tight")
# plt.show()
plt.clf()

sorted_idx = rf.feature_importances_.argsort()
axis_names_array_sorted = axis_names_array[sorted_idx]
feature_importance_sorted = rf.feature_importances_[sorted_idx]

feature_dict = dict(zip(axis_names_array_sorted,feature_importance_sorted))
feature_dict_s = dict( sorted(feature_dict.items(), key=operator.itemgetter(1), reverse=True))

with open('./Data/Visualized/Slowloris/Feature_Importance_Sorted.txt','w') as file:
    for key, value in feature_dict_s.items():
        file.write('%s:\t%s\n' % (key, value))

plt.barh(axis_names_array_sorted[-10:], feature_importance_sorted[-10:])
plt.xlabel("Random Forest Sorted Feature Importance for Slowloris Dataset")
plt.savefig('./Data/Visualized/Slowloris/FIS_Slowloris.png',bbox_inches="tight")
# plt.show()
plt.clf()

# # Permutation Importance
perm_importance = permutation_importance(rf, X_test, y_test)
sorted_idx_perm = perm_importance.importances_mean.argsort()
axis_names_array_sorted = axis_names_array[sorted_idx_perm]
feature_importance_sorted = perm_importance.importances_mean[sorted_idx_perm]  
plt.barh(axis_names_array_sorted[-10:], feature_importance_sorted[-10:])
plt.xlabel("Permutation Importance for Slowloris Dataset")
plt.savefig('./Data/Visualized/Slowloris/FIS_PERM_Slowloris.png',bbox_inches="tight")
# plt.show()
plt.clf()

explainer = shap.TreeExplainer(rf)
shap_values = explainer.shap_values(X_test)
shap.summary_plot(shap_values, X_test, plot_type="bar", show=False)
plt.savefig('./Data/Visualized/Slowloris/Shap_Plot.png',bbox_inches="tight")
shap.summary_plot(shap_values, X_test,show=False)
plt.savefig('./Data/Visualized/Slowloris/Shap_Plot_FeatureValue.png',bbox_inches="tight")