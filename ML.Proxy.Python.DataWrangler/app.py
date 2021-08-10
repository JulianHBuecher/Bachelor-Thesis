import pandas as pd
import os

from pandas.core.frame import DataFrame

# Distinct values of Tuesday DataFrame:
# ['Benign' 'DDoS attacks-LOIC-HTTP']
# Distinct values of Wednesday DataFrame:
# ['Benign' 'DDOS attack-LOIC-UDP' 'DDOS attack-HOIC']
# Distinct values of Thursday DataFrame:
# ['Benign' 'DoS attacks-GoldenEye' 'DoS attacks-Slowloris']
# Distinct values of Friday DataFrame:
# ['Benign' 'DoS attacks-SlowHTTPTest' 'DoS attacks-Hulk' 'Label']

data_path = "Data"
optimized_data_path = "Optimized"

def rename_columns(df: DataFrame):
    df_columns = df.columns.values.tolist()
    column_dict = { }

    for n in df_columns:
        old_value = str(n)
        new_value = old_value.lower()
        new_value = new_value.replace(" ","_")
        column_dict[old_value] = new_value

    df.rename(columns=column_dict,inplace=True)
    
    return df

# Reading dataset with following attacks:
# - DDoS Attack LOIC-HTTP (Tuesday-20-02-2018) von 10:12 bis 11:17
# - DDoS-LOIC-UDP (Tuesday-20-02-2018) von 13:13 bis 13:32
# Features: B.Packet Len Std; Avg Packet Size; Flow Duration; Flow IAT Std
# Features from Prop-File: Bwd Pkt Len Std; Pkt Size Avg; Flow Duration; Flow IAT Std
loic_data_path = os.path.join(data_path, 'Tuesday-20-02-2018_TrafficForML_CICFlowMeter.csv')
print("Begin Reading LOIC Dataset...")
chunk_df = pd.read_csv(loic_data_path, chunksize=100000)
df_tuesday = pd.concat(chunk_df)
print("\nLOIC Dataset successfull loaded...")
df_tuesday_loic = df_tuesday[[
    'Bwd Pkt Len Std',
    'Pkt Size Avg',
    'Flow Duration',
    'Flow IAT Std',
    'Label']].copy()

df_tuesday_loic = rename_columns(df_tuesday_loic)

print("Begin Transformation of LOIC Label...")
# Replace the string values with numeric values
df_tuesday_loic['label'] = df_tuesday_loic['label'].replace({'Benign': 0, 'DDoS attacks-LOIC-HTTP': 1}, value=None)
print("\nTransformation completed!")
print("\nWriting optimized file to directory...")
optimized_loic_data_path = os.path.join(data_path, optimized_data_path, 'Tuesday-20-02-2018_LOIC-Attack.csv')
df_tuesday_loic.to_csv(optimized_loic_data_path, index=False)
print("\nData Wrangling successful!")

# Reading dataset with following attacks:
# - DoS-GoldenEye (Thursday-15-02-2018) von 9:26 bis 10:09
# - DoS-Slowloris (Thursday-15-02-2018) von 10:59 bis 11:40
# Features:
# - GoldenEye: B.Packet Len Std; Flow IAT Min; Fwd IAT Min; Flow IAT Mean
# - Features of Prop-File: Bwd Pkt Len Std; Flow IAT Min; Fwd IAT Min; Flow IAT Mean
# - Slowloris: Flow Duration; F.IAT Min; B.IAT Mean; F.IAT Mean 
# - Features of Prop-File: Flow Duration; Fwd IAT Min; Bwd IAT Mean; Fwd IAT Mean 
goldeneye_slowloris_data_path = os.path.join(data_path, 'Thursday-15-02-2018_TrafficForML_CICFlowMeter.csv')
print("\nBegin Reading GoldenEye and Slowloris Dataset...")
thursday_chunk = pd.read_csv(goldeneye_slowloris_data_path, chunksize=100000)
print("\nGoldenEye and Slowloris Dataset successful loaded...")
df_thursday = pd.concat(thursday_chunk)

df_thursday_goldeneye = df_thursday[[
    'Bwd Pkt Len Std',
    'Flow IAT Min',
    'Fwd IAT Min',
    'Flow IAT Mean',
    'Label'
]].copy()

df_thursday_goldeneye = rename_columns(df_thursday_goldeneye)

df_thursday_goldeneye = df_thursday_goldeneye[df_thursday_goldeneye.label != 'DoS attacks-Slowloris']

# Replace the string values with numeric values
df_thursday_goldeneye['label'] = df_thursday_goldeneye['label'].replace({'Benign': 0, 'DoS attacks-GoldenEye': 1}, value=None)

df_thursday_slowloris = df_thursday[[
    'Flow Duration',
    'Bwd IAT Mean',
    'Fwd IAT Min',
    'Fwd IAT Mean',
    'Label'
]].copy()

df_thursday_slowloris = rename_columns(df_thursday_slowloris)

df_thursday_slowloris = df_thursday_slowloris[df_thursday_slowloris.label != 'DoS attacks-GoldenEye']

# Replace the string values with numeric values
df_thursday_slowloris['label'] = df_thursday_slowloris['label'].replace({'Benign': 0, 'DoS attacks-Slowloris': 1}, value=None)

optimized_goldeneye_data_path = os.path.join(data_path,optimized_data_path,'Thursday-15-02-2018_GoldenEye-Attack.csv')
optimized_slowloris_data_path = os.path.join(data_path,optimized_data_path,'Thursday-15-02-2018_Slowloris-Attack.csv')
df_thursday_goldeneye.to_csv(optimized_goldeneye_data_path, index=False)
df_thursday_slowloris.to_csv(optimized_slowloris_data_path, index=False)


