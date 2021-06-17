import pandas as pa

# Distinct values of Thursday DataFrame:
# ['Benign' 'DoS attacks-GoldenEye' 'DoS attacks-Slowloris']

df_thursday = pa.read_csv('.\Data\Optimized\Thursday-15-02-2018_GoldenEye&Slowloris-Attack.csv')

df_thursday_goldeneye = df_thursday[[
    'Bwd Pkt Len Std',
    'Flow IAT Min',
    'Fwd IAT Min',
    'Flow IAT Mean',
    'Label'
]].copy()

df_thursday_goldeneye = df_thursday_goldeneye[df_thursday_goldeneye.Label != 'DoS attacks-Slowloris']

df_thursday_slowloris = df_thursday[[
    'Flow Duration',
    'Bwd IAT Mean',
    'Fwd IAT Min',
    'Fwd IAT Mean',
    'Label'
]].copy()

df_thursday_slowloris = df_thursday_slowloris[df_thursday_slowloris.Label != 'DoS attacks-GoldenEye']

df_thursday_goldeneye.to_csv('.\Data\Optimized\Thursday-15-02-2018_GoldenEye-Attack.csv', index=False)
df_thursday_slowloris.to_csv('.\Data\Optimized\Thursday-15-02-2018_Slowloris-Attack.csv', index=False)