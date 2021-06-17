import pandas as pa

# Read the optimized datasets
df_thursday_goldeneye = pa.read_csv('.\Data\Optimized\Thursday-15-02-2018_GoldenEye-Attack.csv')
df_thursday_slowloris = pa.read_csv('.\Data\Optimized\Thursday-15-02-2018_Slowloris-Attack.csv')
df_tuesday_loic = pa.read_csv('.\Data\Optimized\Tuesday-20-02-2018_LOIC-Attack.csv')

# Replace the string values with numeric values
df_thursday_goldeneye['Label'] = df_thursday_goldeneye['Label'].replace({'Benign': 0, 'DoS attacks-GoldenEye': 1}, value=None)
df_thursday_slowloris['Label'] = df_thursday_slowloris['Label'].replace({'Benign': 0, 'DoS attacks-Slowloris': 1}, value=None)
df_tuesday_loic['Label'] = df_tuesday_loic['Label'].replace({'Benign': 0, 'DDoS attacks-LOIC-HTTP': 1}, value=None)

# Write the content back to files
df_thursday_goldeneye.to_csv('.\Data\Relabeled\Thursday-15-02-2018_GoldenEye-Attack.csv', index=False)
df_thursday_slowloris.to_csv('.\Data\Relabeled\Thursday-15-02-2018_Slowloris-Attack.csv', index=False)
df_tuesday_loic.to_csv('.\Data\Relabeled\Tuesday-20-02-2018_LOIC-Attack.csv', index=False)