import pandas as pa
import numpy as np

# Distinct values of Tuesday DataFrame:
# ['Benign' 'DDoS attacks-LOIC-HTTP']
# Distinct values of Wednesday DataFrame:
# ['Benign' 'DDOS attack-LOIC-UDP' 'DDOS attack-HOIC']
# Distinct values of Thursday DataFrame:
# ['Benign' 'DoS attacks-GoldenEye' 'DoS attacks-Slowloris']
# Distinct values of Friday DataFrame:
# ['Benign' 'DoS attacks-SlowHTTPTest' 'DoS attacks-Hulk' 'Label']

# Reading dataset with following attacks:
# - DDoS Attack LOIC-HTTP (Tuesday-20-02-2018) von 10:12 bis 11:17
# - DDoS-LOIC-UDP (Tuesday-20-02-2018) von 13:13 bis 13:32
# Features: B.Packet Len Std; Avg Packet Size; Flow Duration; Flow IAT Std
# Features from Prop-File: Bwd Pkt Len Std; Pkt Size Avg; Flow Duration; Flow IAT Std
df_tuesday = pa.read_csv('.\Data\Tuesday-20-02-2018_TrafficForML_CICFlowMeter.csv')
df_tuesday_o = df_tuesday[[
    'Bwd Pkt Len Std',
    'Pkt Size Avg',
    'Flow Duration',
    'Flow IAT Std',
    'Label']]

df_tuesday_o.to_csv('.\Data\Optimized\Tuesday-20-02-2018_LOIC-Attack.csv', index=False)

# Reading dataset with following attacks:
# - DDoS-LOIC-UDP (Wednesday-21-02-2018) von 10:09 bis 10:43
# - DDoS-HOIC (Wednesday-21-02-2018) von 14:05 bis 15:05
# df_wednesday = pa.read_csv('.\Data\Wednesday-21-02-2018_TrafficForML_CICFlowMeter.csv')

# Reading dataset with following attacks:
# - DoS-GoldenEye (Thursday-15-02-2018) von 9:26 bis 10:09
# - DoS-Slowloris (Thursday-15-02-2018) von 10:59 bis 11:40
# Features:
# - GoldenEye: B.Packet Len Std; Flow IAT Min; Fwd IAT Min; Flow IAT Mean
# - Features of Prop-File: Bwd Pkt Len Std; Flow IAT Min; Fwd IAT Min; Flow IAT Mean
# - Slowloris: Flow Duration; F.IAT Min; B.IAT Mean; F.IAT Mean 
# - Features of Prop-File: Flow Duration; Fwd IAT Min; Bwd IAT Mean; Fwd IAT Mean 
df_thursday = pa.read_csv('.\Data\Thursday-15-02-2018_TrafficForML_CICFlowMeter.csv')
df_thursday_o = df_thursday[[
    # GoldenEye
    'Bwd Pkt Len Std',
    'Flow IAT Min',
    'Fwd IAT Min',
    'Flow IAT Mean',
    # Slowloris (Fwd IAT Min is doubled)
    'Flow Duration',
    'Bwd IAT Mean',
    'Fwd IAT Mean',
    'Label'
]]
df_thursday_o.to_csv('.\Data\Optimized\Thursday-15-02-2018_GoldenEye&Slowloris-Attack.csv', index=False)

# Reading dataset with following attacks:
# - DoS-SlowHTTPTest (Friday-16-02-2018) von 10:12 bis 11:08
# - DoS-Hulk (Friday-16-02-2018) von 13:45 bis 14:19
# df_friday = pa.read_csv('.\Data\Friday-16-02-2018_TrafficForML_CICFlowMeter.csv')

