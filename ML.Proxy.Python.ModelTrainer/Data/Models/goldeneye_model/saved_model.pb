֏
??
B
AssignVariableOp
resource
value"dtype"
dtypetype?
N
Cast	
x"SrcT	
y"DstT"
SrcTtype"
DstTtype"
Truncatebool( 
8
Const
output"dtype"
valuetensor"
dtypetype
.
Identity

input"T
output"T"	
Ttype
e
MergeV2Checkpoints
checkpoint_prefixes
destination_prefix"
delete_old_dirsbool(?

NoOp
M
Pack
values"T*N
output"T"
Nint(0"	
Ttype"
axisint 
C
Placeholder
output"dtype"
dtypetype"
shapeshape:
@
ReadVariableOp
resource
value"dtype"
dtypetype?
o
	RestoreV2

prefix
tensor_names
shape_and_slices
tensors2dtypes"
dtypes
list(type)(0?
l
SaveV2

prefix
tensor_names
shape_and_slices
tensors2dtypes"
dtypes
list(type)(0?
?
Select
	condition

t"T
e"T
output"T"	
Ttype
H
ShardedFilename
basename	
shard

num_shards
filename
f
SimpleMLCreateModelResource
model_handle"
	containerstring "
shared_namestring ?
?
SimpleMLInferenceOpWithHandle
numerical_features
boolean_features
categorical_int_features'
#categorical_set_int_features_values1
-categorical_set_int_features_row_splits_dim_1	1
-categorical_set_int_features_row_splits_dim_2	
model_handle
dense_predictions
dense_col_representation"
dense_output_dimint(0?
D
#SimpleMLLoadModelFromPathWithHandle
model_handle
path?
?
StatefulPartitionedCall
args2Tin
output2Tout"
Tin
list(type)("
Tout
list(type)("	
ffunc"
configstring "
config_protostring "
executor_typestring ?
@
StaticRegexFullMatch	
input

output
"
patternstring
m
StaticRegexReplace	
input

output"
patternstring"
rewritestring"
replace_globalbool(
?
StridedSlice

input"T
begin"Index
end"Index
strides"Index
output"T"	
Ttype"
Indextype:
2	"

begin_maskint "
end_maskint "
ellipsis_maskint "
new_axis_maskint "
shrink_axis_maskint 
N

StringJoin
inputs*N

output"
Nint(0"
	separatorstring 
?
VarHandleOp
resource"
	containerstring "
shared_namestring "
dtypetype"
shapeshape"#
allowed_deviceslist(string)
 ?
9
VarIsInitializedOp
resource
is_initialized
?"serve*2.5.02v2.5.0-rc3-213-ga4dfb8d1a718??
h

is_trainedVarHandleOp*
_output_shapes
: *
dtype0
*
shape: *
shared_name
is_trained
a
is_trained/Read/ReadVariableOpReadVariableOp
is_trained*
_output_shapes
: *
dtype0

?
SimpleMLCreateModelResourceSimpleMLCreateModelResource*
_output_shapes
: *E
shared_name64simple_ml_model_66aeb0c9-ce71-4522-9a16-619106ea948a
W
asset_path_initializerPlaceholder*
_output_shapes
: *
dtype0*
shape: 
?
VariableVarHandleOp*
_class
loc:@Variable*
_output_shapes
: *
dtype0*
shape: *
shared_name
Variable
a
)Variable/IsInitialized/VarIsInitializedOpVarIsInitializedOpVariable*
_output_shapes
: 
R
Variable/AssignAssignVariableOpVariableasset_path_initializer*
dtype0
]
Variable/Read/ReadVariableOpReadVariableOpVariable*
_output_shapes
: *
dtype0
^
totalVarHandleOp*
_output_shapes
: *
dtype0*
shape: *
shared_nametotal
W
total/Read/ReadVariableOpReadVariableOptotal*
_output_shapes
: *
dtype0
^
countVarHandleOp*
_output_shapes
: *
dtype0*
shape: *
shared_namecount
W
count/Read/ReadVariableOpReadVariableOpcount*
_output_shapes
: *
dtype0
b
total_1VarHandleOp*
_output_shapes
: *
dtype0*
shape: *
shared_name	total_1
[
total_1/Read/ReadVariableOpReadVariableOptotal_1*
_output_shapes
: *
dtype0
b
count_1VarHandleOp*
_output_shapes
: *
dtype0*
shape: *
shared_name	count_1
[
count_1/Read/ReadVariableOpReadVariableOpcount_1*
_output_shapes
: *
dtype0
Y
asset_path_initializer_1Placeholder*
_output_shapes
: *
dtype0*
shape: 
?

Variable_1VarHandleOp*
_class
loc:@Variable_1*
_output_shapes
: *
dtype0*
shape: *
shared_name
Variable_1
e
+Variable_1/IsInitialized/VarIsInitializedOpVarIsInitializedOp
Variable_1*
_output_shapes
: 
X
Variable_1/AssignAssignVariableOp
Variable_1asset_path_initializer_1*
dtype0
a
Variable_1/Read/ReadVariableOpReadVariableOp
Variable_1*
_output_shapes
: *
dtype0
Y
asset_path_initializer_2Placeholder*
_output_shapes
: *
dtype0*
shape: 
?

Variable_2VarHandleOp*
_class
loc:@Variable_2*
_output_shapes
: *
dtype0*
shape: *
shared_name
Variable_2
e
+Variable_2/IsInitialized/VarIsInitializedOpVarIsInitializedOp
Variable_2*
_output_shapes
: 
X
Variable_2/AssignAssignVariableOp
Variable_2asset_path_initializer_2*
dtype0
a
Variable_2/Read/ReadVariableOpReadVariableOp
Variable_2*
_output_shapes
: *
dtype0
Y
asset_path_initializer_3Placeholder*
_output_shapes
: *
dtype0*
shape: 
?

Variable_3VarHandleOp*
_class
loc:@Variable_3*
_output_shapes
: *
dtype0*
shape: *
shared_name
Variable_3
e
+Variable_3/IsInitialized/VarIsInitializedOpVarIsInitializedOp
Variable_3*
_output_shapes
: 
X
Variable_3/AssignAssignVariableOp
Variable_3asset_path_initializer_3*
dtype0
a
Variable_3/Read/ReadVariableOpReadVariableOp
Variable_3*
_output_shapes
: *
dtype0
Y
asset_path_initializer_4Placeholder*
_output_shapes
: *
dtype0*
shape: 
?

Variable_4VarHandleOp*
_class
loc:@Variable_4*
_output_shapes
: *
dtype0*
shape: *
shared_name
Variable_4
e
+Variable_4/IsInitialized/VarIsInitializedOpVarIsInitializedOp
Variable_4*
_output_shapes
: 
X
Variable_4/AssignAssignVariableOp
Variable_4asset_path_initializer_4*
dtype0
a
Variable_4/Read/ReadVariableOpReadVariableOp
Variable_4*
_output_shapes
: *
dtype0
a
ReadVariableOpReadVariableOpVariable^Variable/Assign*
_output_shapes
: *
dtype0
?
StatefulPartitionedCallStatefulPartitionedCallReadVariableOpSimpleMLCreateModelResource*
Tin
2*
Tout
2*
_collective_manager_ids
 *
_output_shapes
: * 
_read_only_resource_inputs
 *0
config_proto 

CPU

GPU2*0J 8? *#
fR
__inference_<lambda>_23062
?
NoOpNoOp^StatefulPartitionedCall^Variable/Assign^Variable_1/Assign^Variable_2/Assign^Variable_3/Assign^Variable_4/Assign
?

ConstConst"/device:CPU:0*
_output_shapes
: *
dtype0*?	
value?	B?	 B?	
?
_learner_params
	_features
_is_trained
	optimizer
loss

_model
regularization_losses
trainable_variables
		variables

	keras_api

signatures
 
 
FD
VARIABLE_VALUE
is_trained&_is_trained/.ATTRIBUTES/VARIABLE_VALUE
 
 
)
_input_builder
_compiled_model
 
 

0
?
layer_metrics
regularization_losses

layers
trainable_variables
metrics
layer_regularization_losses
non_trainable_variables
		variables
 
N
_feature_name_to_idx
	_init_ops
#categorical_str_to_int_hashmaps

_model_loader
 
 

0
1
 

0
 
 
 
 

_all_files

_done_file
4
	total
	count
	variables
	keras_api
D
	total
	 count
!
_fn_kwargs
"	variables
#	keras_api
#
0
$1
%2
&3
'4
 
OM
VARIABLE_VALUEtotal4keras_api/metrics/0/total/.ATTRIBUTES/VARIABLE_VALUE
OM
VARIABLE_VALUEcount4keras_api/metrics/0/count/.ATTRIBUTES/VARIABLE_VALUE

0
1

	variables
QO
VARIABLE_VALUEtotal_14keras_api/metrics/1/total/.ATTRIBUTES/VARIABLE_VALUE
QO
VARIABLE_VALUEcount_14keras_api/metrics/1/count/.ATTRIBUTES/VARIABLE_VALUE
 

0
 1

"	variables
 
 
 
 
?
serving_default_bwd_pkt_len_stdPlaceholder*'
_output_shapes
:?????????*
dtype0*
shape:?????????
?
serving_default_flow_iat_meanPlaceholder*'
_output_shapes
:?????????*
dtype0*
shape:?????????

serving_default_flow_iat_minPlaceholder*'
_output_shapes
:?????????*
dtype0	*
shape:?????????
~
serving_default_fwd_iat_minPlaceholder*'
_output_shapes
:?????????*
dtype0	*
shape:?????????
?
StatefulPartitionedCall_1StatefulPartitionedCallserving_default_bwd_pkt_len_stdserving_default_flow_iat_meanserving_default_flow_iat_minserving_default_fwd_iat_minSimpleMLCreateModelResource*
Tin	
2		*
Tout
2*
_collective_manager_ids
 *'
_output_shapes
:?????????* 
_read_only_resource_inputs
 *0
config_proto 

CPU

GPU2*0J 8? *,
f'R%
#__inference_signature_wrapper_22896
O
saver_filenamePlaceholder*
_output_shapes
: *
dtype0*
shape: 
?
StatefulPartitionedCall_2StatefulPartitionedCallsaver_filenameis_trained/Read/ReadVariableOptotal/Read/ReadVariableOpcount/Read/ReadVariableOptotal_1/Read/ReadVariableOpcount_1/Read/ReadVariableOpConst*
Tin
	2
*
Tout
2*
_collective_manager_ids
 *
_output_shapes
: * 
_read_only_resource_inputs
 *0
config_proto 

CPU

GPU2*0J 8? *'
f"R 
__inference__traced_save_23126
?
StatefulPartitionedCall_3StatefulPartitionedCallsaver_filename
is_trainedtotalcounttotal_1count_1*
Tin

2*
Tout
2*
_collective_manager_ids
 *
_output_shapes
: * 
_read_only_resource_inputs
 *0
config_proto 

CPU

GPU2*0J 8? **
f%R#
!__inference__traced_restore_23151??
?
?
 __inference__wrapped_model_22690
bwd_pkt_len_std
flow_iat_mean
flow_iat_min	
fwd_iat_min	
random_forest_model_1_22686
identity??-random_forest_model_1/StatefulPartitionedCall?
-random_forest_model_1/StatefulPartitionedCallStatefulPartitionedCallbwd_pkt_len_stdflow_iat_meanflow_iat_minfwd_iat_minrandom_forest_model_1_22686*
Tin	
2		*
Tout
2*
_collective_manager_ids
 *'
_output_shapes
:?????????* 
_read_only_resource_inputs
 *0
config_proto 

CPU

GPU2*0J 8? *
fR
__inference_call_226852/
-random_forest_model_1/StatefulPartitionedCall?
IdentityIdentity6random_forest_model_1/StatefulPartitionedCall:output:0.^random_forest_model_1/StatefulPartitionedCall*
T0*'
_output_shapes
:?????????2

Identity"
identityIdentity:output:0*(
_construction_contextkEagerRuntime*a
_input_shapesP
N:?????????:?????????:?????????:?????????: 2^
-random_forest_model_1/StatefulPartitionedCall-random_forest_model_1/StatefulPartitionedCall:X T
'
_output_shapes
:?????????
)
_user_specified_namebwd_pkt_len_std:VR
'
_output_shapes
:?????????
'
_user_specified_nameflow_iat_mean:UQ
'
_output_shapes
:?????????
&
_user_specified_nameflow_iat_min:TP
'
_output_shapes
:?????????
%
_user_specified_namefwd_iat_min
?

?
5__inference_random_forest_model_1_layer_call_fn_22808
bwd_pkt_len_std
flow_iat_mean
flow_iat_min	
fwd_iat_min	
unknown
identity??StatefulPartitionedCall?
StatefulPartitionedCallStatefulPartitionedCallbwd_pkt_len_stdflow_iat_meanflow_iat_minfwd_iat_minunknown*
Tin	
2		*
Tout
2*
_collective_manager_ids
 *'
_output_shapes
:?????????* 
_read_only_resource_inputs
 *0
config_proto 

CPU

GPU2*0J 8? *Y
fTRR
P__inference_random_forest_model_1_layer_call_and_return_conditional_losses_227932
StatefulPartitionedCall?
IdentityIdentity StatefulPartitionedCall:output:0^StatefulPartitionedCall*
T0*'
_output_shapes
:?????????2

Identity"
identityIdentity:output:0*(
_construction_contextkEagerRuntime*a
_input_shapesP
N:?????????:?????????:?????????:?????????: 22
StatefulPartitionedCallStatefulPartitionedCall:X T
'
_output_shapes
:?????????
)
_user_specified_namebwd_pkt_len_std:VR
'
_output_shapes
:?????????
'
_user_specified_nameflow_iat_mean:UQ
'
_output_shapes
:?????????
&
_user_specified_nameflow_iat_min:TP
'
_output_shapes
:?????????
%
_user_specified_namefwd_iat_min
?
?
__inference_<lambda>_23062
staticregexreplace_input>
:simple_ml_simplemlloadmodelfrompathwithhandle_model_handle
identity??-simple_ml/SimpleMLLoadModelFromPathWithHandle?
StaticRegexReplaceStaticRegexReplacestaticregexreplace_input*
_output_shapes
: *
patterndone*
rewrite 2
StaticRegexReplace?
-simple_ml/SimpleMLLoadModelFromPathWithHandle#SimpleMLLoadModelFromPathWithHandle:simple_ml_simplemlloadmodelfrompathwithhandle_model_handleStaticRegexReplace:output:0*
_output_shapes
 2/
-simple_ml/SimpleMLLoadModelFromPathWithHandleS
ConstConst*
_output_shapes
: *
dtype0*
valueB
 *  ??2
Const?
IdentityIdentityConst:output:0.^simple_ml/SimpleMLLoadModelFromPathWithHandle*
T0*
_output_shapes
: 2

Identity"
identityIdentity:output:0*(
_construction_contextkEagerRuntime*
_input_shapes
: : 2^
-simple_ml/SimpleMLLoadModelFromPathWithHandle-simple_ml/SimpleMLLoadModelFromPathWithHandle: 

_output_shapes
: 
?*
?
P__inference_random_forest_model_1_layer_call_and_return_conditional_losses_22735

inputs
inputs_1
inputs_2	
inputs_3	
inference_op_model_handle
identity??inference_op{
strided_slice/stackConst*
_output_shapes
:*
dtype0*
valueB"        2
strided_slice/stack
strided_slice/stack_1Const*
_output_shapes
:*
dtype0*
valueB"       2
strided_slice/stack_1
strided_slice/stack_2Const*
_output_shapes
:*
dtype0*
valueB"      2
strided_slice/stack_2?
strided_sliceStridedSliceinputsstrided_slice/stack:output:0strided_slice/stack_1:output:0strided_slice/stack_2:output:0*
Index0*
T0*#
_output_shapes
:?????????*

begin_mask*
end_mask*
shrink_axis_mask2
strided_slice_
CastCastinputs_2*

DstT0*

SrcT0	*'
_output_shapes
:?????????2
Cast
strided_slice_1/stackConst*
_output_shapes
:*
dtype0*
valueB"        2
strided_slice_1/stack?
strided_slice_1/stack_1Const*
_output_shapes
:*
dtype0*
valueB"       2
strided_slice_1/stack_1?
strided_slice_1/stack_2Const*
_output_shapes
:*
dtype0*
valueB"      2
strided_slice_1/stack_2?
strided_slice_1StridedSliceCast:y:0strided_slice_1/stack:output:0 strided_slice_1/stack_1:output:0 strided_slice_1/stack_2:output:0*
Index0*
T0*#
_output_shapes
:?????????*

begin_mask*
end_mask*
shrink_axis_mask2
strided_slice_1c
Cast_1Castinputs_3*

DstT0*

SrcT0	*'
_output_shapes
:?????????2
Cast_1
strided_slice_2/stackConst*
_output_shapes
:*
dtype0*
valueB"        2
strided_slice_2/stack?
strided_slice_2/stack_1Const*
_output_shapes
:*
dtype0*
valueB"       2
strided_slice_2/stack_1?
strided_slice_2/stack_2Const*
_output_shapes
:*
dtype0*
valueB"      2
strided_slice_2/stack_2?
strided_slice_2StridedSlice
Cast_1:y:0strided_slice_2/stack:output:0 strided_slice_2/stack_1:output:0 strided_slice_2/stack_2:output:0*
Index0*
T0*#
_output_shapes
:?????????*

begin_mask*
end_mask*
shrink_axis_mask2
strided_slice_2
strided_slice_3/stackConst*
_output_shapes
:*
dtype0*
valueB"        2
strided_slice_3/stack?
strided_slice_3/stack_1Const*
_output_shapes
:*
dtype0*
valueB"       2
strided_slice_3/stack_1?
strided_slice_3/stack_2Const*
_output_shapes
:*
dtype0*
valueB"      2
strided_slice_3/stack_2?
strided_slice_3StridedSliceinputs_1strided_slice_3/stack:output:0 strided_slice_3/stack_1:output:0 strided_slice_3/stack_2:output:0*
Index0*
T0*#
_output_shapes
:?????????*

begin_mask*
end_mask*
shrink_axis_mask2
strided_slice_3?
stackPackstrided_slice:output:0strided_slice_3:output:0strided_slice_1:output:0strided_slice_2:output:0*
N*
T0*'
_output_shapes
:?????????*

axis2
stackU
ConstConst*
_output_shapes
:  *
dtype0*
value
B  2
ConstY
Const_1Const*
_output_shapes
:  *
dtype0*
value
B  2	
Const_1q
RaggedConstant/valuesConst*
_output_shapes
: *
dtype0*
valueB 2
RaggedConstant/valuesv
RaggedConstant/ConstConst*
_output_shapes
:*
dtype0	*
valueB	R 2
RaggedConstant/Constz
RaggedConstant/Const_1Const*
_output_shapes
:*
dtype0	*
valueB	R 2
RaggedConstant/Const_1?
inference_opSimpleMLInferenceOpWithHandlestack:output:0Const:output:0Const_1:output:0RaggedConstant/values:output:0RaggedConstant/Const:output:0RaggedConstant/Const_1:output:0inference_op_model_handle*-
_output_shapes
:?????????:*
dense_output_dim2
inference_op
strided_slice_4/stackConst*
_output_shapes
:*
dtype0*
valueB"       2
strided_slice_4/stack?
strided_slice_4/stack_1Const*
_output_shapes
:*
dtype0*
valueB"       2
strided_slice_4/stack_1?
strided_slice_4/stack_2Const*
_output_shapes
:*
dtype0*
valueB"      2
strided_slice_4/stack_2?
strided_slice_4StridedSlice inference_op:dense_predictions:0strided_slice_4/stack:output:0 strided_slice_4/stack_1:output:0 strided_slice_4/stack_2:output:0*
Index0*
T0*'
_output_shapes
:?????????*

begin_mask*
end_mask2
strided_slice_4{
IdentityIdentitystrided_slice_4:output:0^inference_op*
T0*'
_output_shapes
:?????????2

Identity"
identityIdentity:output:0*(
_construction_contextkEagerRuntime*a
_input_shapesP
N:?????????:?????????:?????????:?????????: 2
inference_opinference_op:O K
'
_output_shapes
:?????????
 
_user_specified_nameinputs:OK
'
_output_shapes
:?????????
 
_user_specified_nameinputs:OK
'
_output_shapes
:?????????
 
_user_specified_nameinputs:OK
'
_output_shapes
:?????????
 
_user_specified_nameinputs
?*
?
__inference_call_22685

inputs
inputs_1
inputs_2	
inputs_3	
inference_op_model_handle
identity??inference_op{
strided_slice/stackConst*
_output_shapes
:*
dtype0*
valueB"        2
strided_slice/stack
strided_slice/stack_1Const*
_output_shapes
:*
dtype0*
valueB"       2
strided_slice/stack_1
strided_slice/stack_2Const*
_output_shapes
:*
dtype0*
valueB"      2
strided_slice/stack_2?
strided_sliceStridedSliceinputsstrided_slice/stack:output:0strided_slice/stack_1:output:0strided_slice/stack_2:output:0*
Index0*
T0*#
_output_shapes
:?????????*

begin_mask*
end_mask*
shrink_axis_mask2
strided_slice_
CastCastinputs_2*

DstT0*

SrcT0	*'
_output_shapes
:?????????2
Cast
strided_slice_1/stackConst*
_output_shapes
:*
dtype0*
valueB"        2
strided_slice_1/stack?
strided_slice_1/stack_1Const*
_output_shapes
:*
dtype0*
valueB"       2
strided_slice_1/stack_1?
strided_slice_1/stack_2Const*
_output_shapes
:*
dtype0*
valueB"      2
strided_slice_1/stack_2?
strided_slice_1StridedSliceCast:y:0strided_slice_1/stack:output:0 strided_slice_1/stack_1:output:0 strided_slice_1/stack_2:output:0*
Index0*
T0*#
_output_shapes
:?????????*

begin_mask*
end_mask*
shrink_axis_mask2
strided_slice_1c
Cast_1Castinputs_3*

DstT0*

SrcT0	*'
_output_shapes
:?????????2
Cast_1
strided_slice_2/stackConst*
_output_shapes
:*
dtype0*
valueB"        2
strided_slice_2/stack?
strided_slice_2/stack_1Const*
_output_shapes
:*
dtype0*
valueB"       2
strided_slice_2/stack_1?
strided_slice_2/stack_2Const*
_output_shapes
:*
dtype0*
valueB"      2
strided_slice_2/stack_2?
strided_slice_2StridedSlice
Cast_1:y:0strided_slice_2/stack:output:0 strided_slice_2/stack_1:output:0 strided_slice_2/stack_2:output:0*
Index0*
T0*#
_output_shapes
:?????????*

begin_mask*
end_mask*
shrink_axis_mask2
strided_slice_2
strided_slice_3/stackConst*
_output_shapes
:*
dtype0*
valueB"        2
strided_slice_3/stack?
strided_slice_3/stack_1Const*
_output_shapes
:*
dtype0*
valueB"       2
strided_slice_3/stack_1?
strided_slice_3/stack_2Const*
_output_shapes
:*
dtype0*
valueB"      2
strided_slice_3/stack_2?
strided_slice_3StridedSliceinputs_1strided_slice_3/stack:output:0 strided_slice_3/stack_1:output:0 strided_slice_3/stack_2:output:0*
Index0*
T0*#
_output_shapes
:?????????*

begin_mask*
end_mask*
shrink_axis_mask2
strided_slice_3?
stackPackstrided_slice:output:0strided_slice_3:output:0strided_slice_1:output:0strided_slice_2:output:0*
N*
T0*'
_output_shapes
:?????????*

axis2
stackU
ConstConst*
_output_shapes
:  *
dtype0*
value
B  2
ConstY
Const_1Const*
_output_shapes
:  *
dtype0*
value
B  2	
Const_1q
RaggedConstant/valuesConst*
_output_shapes
: *
dtype0*
valueB 2
RaggedConstant/valuesv
RaggedConstant/ConstConst*
_output_shapes
:*
dtype0	*
valueB	R 2
RaggedConstant/Constz
RaggedConstant/Const_1Const*
_output_shapes
:*
dtype0	*
valueB	R 2
RaggedConstant/Const_1?
inference_opSimpleMLInferenceOpWithHandlestack:output:0Const:output:0Const_1:output:0RaggedConstant/values:output:0RaggedConstant/Const:output:0RaggedConstant/Const_1:output:0inference_op_model_handle*-
_output_shapes
:?????????:*
dense_output_dim2
inference_op
strided_slice_4/stackConst*
_output_shapes
:*
dtype0*
valueB"       2
strided_slice_4/stack?
strided_slice_4/stack_1Const*
_output_shapes
:*
dtype0*
valueB"       2
strided_slice_4/stack_1?
strided_slice_4/stack_2Const*
_output_shapes
:*
dtype0*
valueB"      2
strided_slice_4/stack_2?
strided_slice_4StridedSlice inference_op:dense_predictions:0strided_slice_4/stack:output:0 strided_slice_4/stack_1:output:0 strided_slice_4/stack_2:output:0*
Index0*
T0*'
_output_shapes
:?????????*

begin_mask*
end_mask2
strided_slice_4{
IdentityIdentitystrided_slice_4:output:0^inference_op*
T0*'
_output_shapes
:?????????2

Identity"
identityIdentity:output:0*(
_construction_contextkEagerRuntime*a
_input_shapesP
N:?????????:?????????:?????????:?????????: 2
inference_opinference_op:O K
'
_output_shapes
:?????????
 
_user_specified_nameinputs:OK
'
_output_shapes
:?????????
 
_user_specified_nameinputs:OK
'
_output_shapes
:?????????
 
_user_specified_nameinputs:OK
'
_output_shapes
:?????????
 
_user_specified_nameinputs
?+
?
P__inference_random_forest_model_1_layer_call_and_return_conditional_losses_22992
inputs_bwd_pkt_len_std
inputs_flow_iat_mean
inputs_flow_iat_min	
inputs_fwd_iat_min	
inference_op_model_handle
identity??inference_op{
strided_slice/stackConst*
_output_shapes
:*
dtype0*
valueB"        2
strided_slice/stack
strided_slice/stack_1Const*
_output_shapes
:*
dtype0*
valueB"       2
strided_slice/stack_1
strided_slice/stack_2Const*
_output_shapes
:*
dtype0*
valueB"      2
strided_slice/stack_2?
strided_sliceStridedSliceinputs_bwd_pkt_len_stdstrided_slice/stack:output:0strided_slice/stack_1:output:0strided_slice/stack_2:output:0*
Index0*
T0*#
_output_shapes
:?????????*

begin_mask*
end_mask*
shrink_axis_mask2
strided_slicej
CastCastinputs_flow_iat_min*

DstT0*

SrcT0	*'
_output_shapes
:?????????2
Cast
strided_slice_1/stackConst*
_output_shapes
:*
dtype0*
valueB"        2
strided_slice_1/stack?
strided_slice_1/stack_1Const*
_output_shapes
:*
dtype0*
valueB"       2
strided_slice_1/stack_1?
strided_slice_1/stack_2Const*
_output_shapes
:*
dtype0*
valueB"      2
strided_slice_1/stack_2?
strided_slice_1StridedSliceCast:y:0strided_slice_1/stack:output:0 strided_slice_1/stack_1:output:0 strided_slice_1/stack_2:output:0*
Index0*
T0*#
_output_shapes
:?????????*

begin_mask*
end_mask*
shrink_axis_mask2
strided_slice_1m
Cast_1Castinputs_fwd_iat_min*

DstT0*

SrcT0	*'
_output_shapes
:?????????2
Cast_1
strided_slice_2/stackConst*
_output_shapes
:*
dtype0*
valueB"        2
strided_slice_2/stack?
strided_slice_2/stack_1Const*
_output_shapes
:*
dtype0*
valueB"       2
strided_slice_2/stack_1?
strided_slice_2/stack_2Const*
_output_shapes
:*
dtype0*
valueB"      2
strided_slice_2/stack_2?
strided_slice_2StridedSlice
Cast_1:y:0strided_slice_2/stack:output:0 strided_slice_2/stack_1:output:0 strided_slice_2/stack_2:output:0*
Index0*
T0*#
_output_shapes
:?????????*

begin_mask*
end_mask*
shrink_axis_mask2
strided_slice_2
strided_slice_3/stackConst*
_output_shapes
:*
dtype0*
valueB"        2
strided_slice_3/stack?
strided_slice_3/stack_1Const*
_output_shapes
:*
dtype0*
valueB"       2
strided_slice_3/stack_1?
strided_slice_3/stack_2Const*
_output_shapes
:*
dtype0*
valueB"      2
strided_slice_3/stack_2?
strided_slice_3StridedSliceinputs_flow_iat_meanstrided_slice_3/stack:output:0 strided_slice_3/stack_1:output:0 strided_slice_3/stack_2:output:0*
Index0*
T0*#
_output_shapes
:?????????*

begin_mask*
end_mask*
shrink_axis_mask2
strided_slice_3?
stackPackstrided_slice:output:0strided_slice_3:output:0strided_slice_1:output:0strided_slice_2:output:0*
N*
T0*'
_output_shapes
:?????????*

axis2
stackU
ConstConst*
_output_shapes
:  *
dtype0*
value
B  2
ConstY
Const_1Const*
_output_shapes
:  *
dtype0*
value
B  2	
Const_1q
RaggedConstant/valuesConst*
_output_shapes
: *
dtype0*
valueB 2
RaggedConstant/valuesv
RaggedConstant/ConstConst*
_output_shapes
:*
dtype0	*
valueB	R 2
RaggedConstant/Constz
RaggedConstant/Const_1Const*
_output_shapes
:*
dtype0	*
valueB	R 2
RaggedConstant/Const_1?
inference_opSimpleMLInferenceOpWithHandlestack:output:0Const:output:0Const_1:output:0RaggedConstant/values:output:0RaggedConstant/Const:output:0RaggedConstant/Const_1:output:0inference_op_model_handle*-
_output_shapes
:?????????:*
dense_output_dim2
inference_op
strided_slice_4/stackConst*
_output_shapes
:*
dtype0*
valueB"       2
strided_slice_4/stack?
strided_slice_4/stack_1Const*
_output_shapes
:*
dtype0*
valueB"       2
strided_slice_4/stack_1?
strided_slice_4/stack_2Const*
_output_shapes
:*
dtype0*
valueB"      2
strided_slice_4/stack_2?
strided_slice_4StridedSlice inference_op:dense_predictions:0strided_slice_4/stack:output:0 strided_slice_4/stack_1:output:0 strided_slice_4/stack_2:output:0*
Index0*
T0*'
_output_shapes
:?????????*

begin_mask*
end_mask2
strided_slice_4{
IdentityIdentitystrided_slice_4:output:0^inference_op*
T0*'
_output_shapes
:?????????2

Identity"
identityIdentity:output:0*(
_construction_contextkEagerRuntime*a
_input_shapesP
N:?????????:?????????:?????????:?????????: 2
inference_opinference_op:_ [
'
_output_shapes
:?????????
0
_user_specified_nameinputs/bwd_pkt_len_std:]Y
'
_output_shapes
:?????????
.
_user_specified_nameinputs/flow_iat_mean:\X
'
_output_shapes
:?????????
-
_user_specified_nameinputs/flow_iat_min:[W
'
_output_shapes
:?????????
,
_user_specified_nameinputs/fwd_iat_min
?+
?
P__inference_random_forest_model_1_layer_call_and_return_conditional_losses_22954
inputs_bwd_pkt_len_std
inputs_flow_iat_mean
inputs_flow_iat_min	
inputs_fwd_iat_min	
inference_op_model_handle
identity??inference_op{
strided_slice/stackConst*
_output_shapes
:*
dtype0*
valueB"        2
strided_slice/stack
strided_slice/stack_1Const*
_output_shapes
:*
dtype0*
valueB"       2
strided_slice/stack_1
strided_slice/stack_2Const*
_output_shapes
:*
dtype0*
valueB"      2
strided_slice/stack_2?
strided_sliceStridedSliceinputs_bwd_pkt_len_stdstrided_slice/stack:output:0strided_slice/stack_1:output:0strided_slice/stack_2:output:0*
Index0*
T0*#
_output_shapes
:?????????*

begin_mask*
end_mask*
shrink_axis_mask2
strided_slicej
CastCastinputs_flow_iat_min*

DstT0*

SrcT0	*'
_output_shapes
:?????????2
Cast
strided_slice_1/stackConst*
_output_shapes
:*
dtype0*
valueB"        2
strided_slice_1/stack?
strided_slice_1/stack_1Const*
_output_shapes
:*
dtype0*
valueB"       2
strided_slice_1/stack_1?
strided_slice_1/stack_2Const*
_output_shapes
:*
dtype0*
valueB"      2
strided_slice_1/stack_2?
strided_slice_1StridedSliceCast:y:0strided_slice_1/stack:output:0 strided_slice_1/stack_1:output:0 strided_slice_1/stack_2:output:0*
Index0*
T0*#
_output_shapes
:?????????*

begin_mask*
end_mask*
shrink_axis_mask2
strided_slice_1m
Cast_1Castinputs_fwd_iat_min*

DstT0*

SrcT0	*'
_output_shapes
:?????????2
Cast_1
strided_slice_2/stackConst*
_output_shapes
:*
dtype0*
valueB"        2
strided_slice_2/stack?
strided_slice_2/stack_1Const*
_output_shapes
:*
dtype0*
valueB"       2
strided_slice_2/stack_1?
strided_slice_2/stack_2Const*
_output_shapes
:*
dtype0*
valueB"      2
strided_slice_2/stack_2?
strided_slice_2StridedSlice
Cast_1:y:0strided_slice_2/stack:output:0 strided_slice_2/stack_1:output:0 strided_slice_2/stack_2:output:0*
Index0*
T0*#
_output_shapes
:?????????*

begin_mask*
end_mask*
shrink_axis_mask2
strided_slice_2
strided_slice_3/stackConst*
_output_shapes
:*
dtype0*
valueB"        2
strided_slice_3/stack?
strided_slice_3/stack_1Const*
_output_shapes
:*
dtype0*
valueB"       2
strided_slice_3/stack_1?
strided_slice_3/stack_2Const*
_output_shapes
:*
dtype0*
valueB"      2
strided_slice_3/stack_2?
strided_slice_3StridedSliceinputs_flow_iat_meanstrided_slice_3/stack:output:0 strided_slice_3/stack_1:output:0 strided_slice_3/stack_2:output:0*
Index0*
T0*#
_output_shapes
:?????????*

begin_mask*
end_mask*
shrink_axis_mask2
strided_slice_3?
stackPackstrided_slice:output:0strided_slice_3:output:0strided_slice_1:output:0strided_slice_2:output:0*
N*
T0*'
_output_shapes
:?????????*

axis2
stackU
ConstConst*
_output_shapes
:  *
dtype0*
value
B  2
ConstY
Const_1Const*
_output_shapes
:  *
dtype0*
value
B  2	
Const_1q
RaggedConstant/valuesConst*
_output_shapes
: *
dtype0*
valueB 2
RaggedConstant/valuesv
RaggedConstant/ConstConst*
_output_shapes
:*
dtype0	*
valueB	R 2
RaggedConstant/Constz
RaggedConstant/Const_1Const*
_output_shapes
:*
dtype0	*
valueB	R 2
RaggedConstant/Const_1?
inference_opSimpleMLInferenceOpWithHandlestack:output:0Const:output:0Const_1:output:0RaggedConstant/values:output:0RaggedConstant/Const:output:0RaggedConstant/Const_1:output:0inference_op_model_handle*-
_output_shapes
:?????????:*
dense_output_dim2
inference_op
strided_slice_4/stackConst*
_output_shapes
:*
dtype0*
valueB"       2
strided_slice_4/stack?
strided_slice_4/stack_1Const*
_output_shapes
:*
dtype0*
valueB"       2
strided_slice_4/stack_1?
strided_slice_4/stack_2Const*
_output_shapes
:*
dtype0*
valueB"      2
strided_slice_4/stack_2?
strided_slice_4StridedSlice inference_op:dense_predictions:0strided_slice_4/stack:output:0 strided_slice_4/stack_1:output:0 strided_slice_4/stack_2:output:0*
Index0*
T0*'
_output_shapes
:?????????*

begin_mask*
end_mask2
strided_slice_4{
IdentityIdentitystrided_slice_4:output:0^inference_op*
T0*'
_output_shapes
:?????????2

Identity"
identityIdentity:output:0*(
_construction_contextkEagerRuntime*a
_input_shapesP
N:?????????:?????????:?????????:?????????: 2
inference_opinference_op:_ [
'
_output_shapes
:?????????
0
_user_specified_nameinputs/bwd_pkt_len_std:]Y
'
_output_shapes
:?????????
.
_user_specified_nameinputs/flow_iat_mean:\X
'
_output_shapes
:?????????
-
_user_specified_nameinputs/flow_iat_min:[W
'
_output_shapes
:?????????
,
_user_specified_nameinputs/fwd_iat_min
?

?
5__inference_random_forest_model_1_layer_call_fn_22740
bwd_pkt_len_std
flow_iat_mean
flow_iat_min	
fwd_iat_min	
unknown
identity??StatefulPartitionedCall?
StatefulPartitionedCallStatefulPartitionedCallbwd_pkt_len_stdflow_iat_meanflow_iat_minfwd_iat_minunknown*
Tin	
2		*
Tout
2*
_collective_manager_ids
 *'
_output_shapes
:?????????* 
_read_only_resource_inputs
 *0
config_proto 

CPU

GPU2*0J 8? *Y
fTRR
P__inference_random_forest_model_1_layer_call_and_return_conditional_losses_227352
StatefulPartitionedCall?
IdentityIdentity StatefulPartitionedCall:output:0^StatefulPartitionedCall*
T0*'
_output_shapes
:?????????2

Identity"
identityIdentity:output:0*(
_construction_contextkEagerRuntime*a
_input_shapesP
N:?????????:?????????:?????????:?????????: 22
StatefulPartitionedCallStatefulPartitionedCall:X T
'
_output_shapes
:?????????
)
_user_specified_namebwd_pkt_len_std:VR
'
_output_shapes
:?????????
'
_user_specified_nameflow_iat_mean:UQ
'
_output_shapes
:?????????
&
_user_specified_nameflow_iat_min:TP
'
_output_shapes
:?????????
%
_user_specified_namefwd_iat_min
?	
?
#__inference_signature_wrapper_22896
bwd_pkt_len_std
flow_iat_mean
flow_iat_min	
fwd_iat_min	
unknown
identity??StatefulPartitionedCall?
StatefulPartitionedCallStatefulPartitionedCallbwd_pkt_len_stdflow_iat_meanflow_iat_minfwd_iat_minunknown*
Tin	
2		*
Tout
2*
_collective_manager_ids
 *'
_output_shapes
:?????????* 
_read_only_resource_inputs
 *0
config_proto 

CPU

GPU2*0J 8? *)
f$R"
 __inference__wrapped_model_226902
StatefulPartitionedCall?
IdentityIdentity StatefulPartitionedCall:output:0^StatefulPartitionedCall*
T0*'
_output_shapes
:?????????2

Identity"
identityIdentity:output:0*(
_construction_contextkEagerRuntime*a
_input_shapesP
N:?????????:?????????:?????????:?????????: 22
StatefulPartitionedCallStatefulPartitionedCall:X T
'
_output_shapes
:?????????
)
_user_specified_namebwd_pkt_len_std:VR
'
_output_shapes
:?????????
'
_user_specified_nameflow_iat_mean:UQ
'
_output_shapes
:?????????
&
_user_specified_nameflow_iat_min:TP
'
_output_shapes
:?????????
%
_user_specified_namefwd_iat_min
?
?
5__inference_random_forest_model_1_layer_call_fn_22916
inputs_bwd_pkt_len_std
inputs_flow_iat_mean
inputs_flow_iat_min	
inputs_fwd_iat_min	
unknown
identity??StatefulPartitionedCall?
StatefulPartitionedCallStatefulPartitionedCallinputs_bwd_pkt_len_stdinputs_flow_iat_meaninputs_flow_iat_mininputs_fwd_iat_minunknown*
Tin	
2		*
Tout
2*
_collective_manager_ids
 *'
_output_shapes
:?????????* 
_read_only_resource_inputs
 *0
config_proto 

CPU

GPU2*0J 8? *Y
fTRR
P__inference_random_forest_model_1_layer_call_and_return_conditional_losses_227932
StatefulPartitionedCall?
IdentityIdentity StatefulPartitionedCall:output:0^StatefulPartitionedCall*
T0*'
_output_shapes
:?????????2

Identity"
identityIdentity:output:0*(
_construction_contextkEagerRuntime*a
_input_shapesP
N:?????????:?????????:?????????:?????????: 22
StatefulPartitionedCallStatefulPartitionedCall:_ [
'
_output_shapes
:?????????
0
_user_specified_nameinputs/bwd_pkt_len_std:]Y
'
_output_shapes
:?????????
.
_user_specified_nameinputs/flow_iat_mean:\X
'
_output_shapes
:?????????
-
_user_specified_nameinputs/flow_iat_min:[W
'
_output_shapes
:?????????
,
_user_specified_nameinputs/fwd_iat_min
?
?
__inference__traced_save_23126
file_prefix)
%savev2_is_trained_read_readvariableop
$
 savev2_total_read_readvariableop$
 savev2_count_read_readvariableop&
"savev2_total_1_read_readvariableop&
"savev2_count_1_read_readvariableop
savev2_const

identity_1??MergeV2Checkpoints?
StaticRegexFullMatchStaticRegexFullMatchfile_prefix"/device:CPU:**
_output_shapes
: *
pattern
^s3://.*2
StaticRegexFullMatchc
ConstConst"/device:CPU:**
_output_shapes
: *
dtype0*
valueB B.part2
Constl
Const_1Const"/device:CPU:**
_output_shapes
: *
dtype0*
valueB B
_temp/part2	
Const_1?
SelectSelectStaticRegexFullMatch:output:0Const:output:0Const_1:output:0"/device:CPU:**
T0*
_output_shapes
: 2
Selectt

StringJoin
StringJoinfile_prefixSelect:output:0"/device:CPU:**
N*
_output_shapes
: 2

StringJoinZ

num_shardsConst*
_output_shapes
: *
dtype0*
value	B :2

num_shards
ShardedFilename/shardConst"/device:CPU:0*
_output_shapes
: *
dtype0*
value	B : 2
ShardedFilename/shard?
ShardedFilenameShardedFilenameStringJoin:output:0ShardedFilename/shard:output:0num_shards:output:0"/device:CPU:0*
_output_shapes
: 2
ShardedFilename?
SaveV2/tensor_namesConst"/device:CPU:0*
_output_shapes
:*
dtype0*?
value?B?B&_is_trained/.ATTRIBUTES/VARIABLE_VALUEB4keras_api/metrics/0/total/.ATTRIBUTES/VARIABLE_VALUEB4keras_api/metrics/0/count/.ATTRIBUTES/VARIABLE_VALUEB4keras_api/metrics/1/total/.ATTRIBUTES/VARIABLE_VALUEB4keras_api/metrics/1/count/.ATTRIBUTES/VARIABLE_VALUEB_CHECKPOINTABLE_OBJECT_GRAPH2
SaveV2/tensor_names?
SaveV2/shape_and_slicesConst"/device:CPU:0*
_output_shapes
:*
dtype0*
valueBB B B B B B 2
SaveV2/shape_and_slices?
SaveV2SaveV2ShardedFilename:filename:0SaveV2/tensor_names:output:0 SaveV2/shape_and_slices:output:0%savev2_is_trained_read_readvariableop savev2_total_read_readvariableop savev2_count_read_readvariableop"savev2_total_1_read_readvariableop"savev2_count_1_read_readvariableopsavev2_const"/device:CPU:0*
_output_shapes
 *
dtypes

2
2
SaveV2?
&MergeV2Checkpoints/checkpoint_prefixesPackShardedFilename:filename:0^SaveV2"/device:CPU:0*
N*
T0*
_output_shapes
:2(
&MergeV2Checkpoints/checkpoint_prefixes?
MergeV2CheckpointsMergeV2Checkpoints/MergeV2Checkpoints/checkpoint_prefixes:output:0file_prefix"/device:CPU:0*
_output_shapes
 2
MergeV2Checkpointsr
IdentityIdentityfile_prefix^MergeV2Checkpoints"/device:CPU:0*
T0*
_output_shapes
: 2

Identitym

Identity_1IdentityIdentity:output:0^MergeV2Checkpoints*
T0*
_output_shapes
: 2

Identity_1"!

identity_1Identity_1:output:0*!
_input_shapes
: : : : : : : 2(
MergeV2CheckpointsMergeV2Checkpoints:C ?

_output_shapes
: 
%
_user_specified_namefile_prefix:

_output_shapes
: :

_output_shapes
: :

_output_shapes
: :

_output_shapes
: :

_output_shapes
: :

_output_shapes
: 
?+
?
P__inference_random_forest_model_1_layer_call_and_return_conditional_losses_22846
bwd_pkt_len_std
flow_iat_mean
flow_iat_min	
fwd_iat_min	
inference_op_model_handle
identity??inference_op{
strided_slice/stackConst*
_output_shapes
:*
dtype0*
valueB"        2
strided_slice/stack
strided_slice/stack_1Const*
_output_shapes
:*
dtype0*
valueB"       2
strided_slice/stack_1
strided_slice/stack_2Const*
_output_shapes
:*
dtype0*
valueB"      2
strided_slice/stack_2?
strided_sliceStridedSlicebwd_pkt_len_stdstrided_slice/stack:output:0strided_slice/stack_1:output:0strided_slice/stack_2:output:0*
Index0*
T0*#
_output_shapes
:?????????*

begin_mask*
end_mask*
shrink_axis_mask2
strided_slicec
CastCastflow_iat_min*

DstT0*

SrcT0	*'
_output_shapes
:?????????2
Cast
strided_slice_1/stackConst*
_output_shapes
:*
dtype0*
valueB"        2
strided_slice_1/stack?
strided_slice_1/stack_1Const*
_output_shapes
:*
dtype0*
valueB"       2
strided_slice_1/stack_1?
strided_slice_1/stack_2Const*
_output_shapes
:*
dtype0*
valueB"      2
strided_slice_1/stack_2?
strided_slice_1StridedSliceCast:y:0strided_slice_1/stack:output:0 strided_slice_1/stack_1:output:0 strided_slice_1/stack_2:output:0*
Index0*
T0*#
_output_shapes
:?????????*

begin_mask*
end_mask*
shrink_axis_mask2
strided_slice_1f
Cast_1Castfwd_iat_min*

DstT0*

SrcT0	*'
_output_shapes
:?????????2
Cast_1
strided_slice_2/stackConst*
_output_shapes
:*
dtype0*
valueB"        2
strided_slice_2/stack?
strided_slice_2/stack_1Const*
_output_shapes
:*
dtype0*
valueB"       2
strided_slice_2/stack_1?
strided_slice_2/stack_2Const*
_output_shapes
:*
dtype0*
valueB"      2
strided_slice_2/stack_2?
strided_slice_2StridedSlice
Cast_1:y:0strided_slice_2/stack:output:0 strided_slice_2/stack_1:output:0 strided_slice_2/stack_2:output:0*
Index0*
T0*#
_output_shapes
:?????????*

begin_mask*
end_mask*
shrink_axis_mask2
strided_slice_2
strided_slice_3/stackConst*
_output_shapes
:*
dtype0*
valueB"        2
strided_slice_3/stack?
strided_slice_3/stack_1Const*
_output_shapes
:*
dtype0*
valueB"       2
strided_slice_3/stack_1?
strided_slice_3/stack_2Const*
_output_shapes
:*
dtype0*
valueB"      2
strided_slice_3/stack_2?
strided_slice_3StridedSliceflow_iat_meanstrided_slice_3/stack:output:0 strided_slice_3/stack_1:output:0 strided_slice_3/stack_2:output:0*
Index0*
T0*#
_output_shapes
:?????????*

begin_mask*
end_mask*
shrink_axis_mask2
strided_slice_3?
stackPackstrided_slice:output:0strided_slice_3:output:0strided_slice_1:output:0strided_slice_2:output:0*
N*
T0*'
_output_shapes
:?????????*

axis2
stackU
ConstConst*
_output_shapes
:  *
dtype0*
value
B  2
ConstY
Const_1Const*
_output_shapes
:  *
dtype0*
value
B  2	
Const_1q
RaggedConstant/valuesConst*
_output_shapes
: *
dtype0*
valueB 2
RaggedConstant/valuesv
RaggedConstant/ConstConst*
_output_shapes
:*
dtype0	*
valueB	R 2
RaggedConstant/Constz
RaggedConstant/Const_1Const*
_output_shapes
:*
dtype0	*
valueB	R 2
RaggedConstant/Const_1?
inference_opSimpleMLInferenceOpWithHandlestack:output:0Const:output:0Const_1:output:0RaggedConstant/values:output:0RaggedConstant/Const:output:0RaggedConstant/Const_1:output:0inference_op_model_handle*-
_output_shapes
:?????????:*
dense_output_dim2
inference_op
strided_slice_4/stackConst*
_output_shapes
:*
dtype0*
valueB"       2
strided_slice_4/stack?
strided_slice_4/stack_1Const*
_output_shapes
:*
dtype0*
valueB"       2
strided_slice_4/stack_1?
strided_slice_4/stack_2Const*
_output_shapes
:*
dtype0*
valueB"      2
strided_slice_4/stack_2?
strided_slice_4StridedSlice inference_op:dense_predictions:0strided_slice_4/stack:output:0 strided_slice_4/stack_1:output:0 strided_slice_4/stack_2:output:0*
Index0*
T0*'
_output_shapes
:?????????*

begin_mask*
end_mask2
strided_slice_4{
IdentityIdentitystrided_slice_4:output:0^inference_op*
T0*'
_output_shapes
:?????????2

Identity"
identityIdentity:output:0*(
_construction_contextkEagerRuntime*a
_input_shapesP
N:?????????:?????????:?????????:?????????: 2
inference_opinference_op:X T
'
_output_shapes
:?????????
)
_user_specified_namebwd_pkt_len_std:VR
'
_output_shapes
:?????????
'
_user_specified_nameflow_iat_mean:UQ
'
_output_shapes
:?????????
&
_user_specified_nameflow_iat_min:TP
'
_output_shapes
:?????????
%
_user_specified_namefwd_iat_min
?+
?
P__inference_random_forest_model_1_layer_call_and_return_conditional_losses_22884
bwd_pkt_len_std
flow_iat_mean
flow_iat_min	
fwd_iat_min	
inference_op_model_handle
identity??inference_op{
strided_slice/stackConst*
_output_shapes
:*
dtype0*
valueB"        2
strided_slice/stack
strided_slice/stack_1Const*
_output_shapes
:*
dtype0*
valueB"       2
strided_slice/stack_1
strided_slice/stack_2Const*
_output_shapes
:*
dtype0*
valueB"      2
strided_slice/stack_2?
strided_sliceStridedSlicebwd_pkt_len_stdstrided_slice/stack:output:0strided_slice/stack_1:output:0strided_slice/stack_2:output:0*
Index0*
T0*#
_output_shapes
:?????????*

begin_mask*
end_mask*
shrink_axis_mask2
strided_slicec
CastCastflow_iat_min*

DstT0*

SrcT0	*'
_output_shapes
:?????????2
Cast
strided_slice_1/stackConst*
_output_shapes
:*
dtype0*
valueB"        2
strided_slice_1/stack?
strided_slice_1/stack_1Const*
_output_shapes
:*
dtype0*
valueB"       2
strided_slice_1/stack_1?
strided_slice_1/stack_2Const*
_output_shapes
:*
dtype0*
valueB"      2
strided_slice_1/stack_2?
strided_slice_1StridedSliceCast:y:0strided_slice_1/stack:output:0 strided_slice_1/stack_1:output:0 strided_slice_1/stack_2:output:0*
Index0*
T0*#
_output_shapes
:?????????*

begin_mask*
end_mask*
shrink_axis_mask2
strided_slice_1f
Cast_1Castfwd_iat_min*

DstT0*

SrcT0	*'
_output_shapes
:?????????2
Cast_1
strided_slice_2/stackConst*
_output_shapes
:*
dtype0*
valueB"        2
strided_slice_2/stack?
strided_slice_2/stack_1Const*
_output_shapes
:*
dtype0*
valueB"       2
strided_slice_2/stack_1?
strided_slice_2/stack_2Const*
_output_shapes
:*
dtype0*
valueB"      2
strided_slice_2/stack_2?
strided_slice_2StridedSlice
Cast_1:y:0strided_slice_2/stack:output:0 strided_slice_2/stack_1:output:0 strided_slice_2/stack_2:output:0*
Index0*
T0*#
_output_shapes
:?????????*

begin_mask*
end_mask*
shrink_axis_mask2
strided_slice_2
strided_slice_3/stackConst*
_output_shapes
:*
dtype0*
valueB"        2
strided_slice_3/stack?
strided_slice_3/stack_1Const*
_output_shapes
:*
dtype0*
valueB"       2
strided_slice_3/stack_1?
strided_slice_3/stack_2Const*
_output_shapes
:*
dtype0*
valueB"      2
strided_slice_3/stack_2?
strided_slice_3StridedSliceflow_iat_meanstrided_slice_3/stack:output:0 strided_slice_3/stack_1:output:0 strided_slice_3/stack_2:output:0*
Index0*
T0*#
_output_shapes
:?????????*

begin_mask*
end_mask*
shrink_axis_mask2
strided_slice_3?
stackPackstrided_slice:output:0strided_slice_3:output:0strided_slice_1:output:0strided_slice_2:output:0*
N*
T0*'
_output_shapes
:?????????*

axis2
stackU
ConstConst*
_output_shapes
:  *
dtype0*
value
B  2
ConstY
Const_1Const*
_output_shapes
:  *
dtype0*
value
B  2	
Const_1q
RaggedConstant/valuesConst*
_output_shapes
: *
dtype0*
valueB 2
RaggedConstant/valuesv
RaggedConstant/ConstConst*
_output_shapes
:*
dtype0	*
valueB	R 2
RaggedConstant/Constz
RaggedConstant/Const_1Const*
_output_shapes
:*
dtype0	*
valueB	R 2
RaggedConstant/Const_1?
inference_opSimpleMLInferenceOpWithHandlestack:output:0Const:output:0Const_1:output:0RaggedConstant/values:output:0RaggedConstant/Const:output:0RaggedConstant/Const_1:output:0inference_op_model_handle*-
_output_shapes
:?????????:*
dense_output_dim2
inference_op
strided_slice_4/stackConst*
_output_shapes
:*
dtype0*
valueB"       2
strided_slice_4/stack?
strided_slice_4/stack_1Const*
_output_shapes
:*
dtype0*
valueB"       2
strided_slice_4/stack_1?
strided_slice_4/stack_2Const*
_output_shapes
:*
dtype0*
valueB"      2
strided_slice_4/stack_2?
strided_slice_4StridedSlice inference_op:dense_predictions:0strided_slice_4/stack:output:0 strided_slice_4/stack_1:output:0 strided_slice_4/stack_2:output:0*
Index0*
T0*'
_output_shapes
:?????????*

begin_mask*
end_mask2
strided_slice_4{
IdentityIdentitystrided_slice_4:output:0^inference_op*
T0*'
_output_shapes
:?????????2

Identity"
identityIdentity:output:0*(
_construction_contextkEagerRuntime*a
_input_shapesP
N:?????????:?????????:?????????:?????????: 2
inference_opinference_op:X T
'
_output_shapes
:?????????
)
_user_specified_namebwd_pkt_len_std:VR
'
_output_shapes
:?????????
'
_user_specified_nameflow_iat_mean:UQ
'
_output_shapes
:?????????
&
_user_specified_nameflow_iat_min:TP
'
_output_shapes
:?????????
%
_user_specified_namefwd_iat_min
?
?
5__inference_random_forest_model_1_layer_call_fn_22906
inputs_bwd_pkt_len_std
inputs_flow_iat_mean
inputs_flow_iat_min	
inputs_fwd_iat_min	
unknown
identity??StatefulPartitionedCall?
StatefulPartitionedCallStatefulPartitionedCallinputs_bwd_pkt_len_stdinputs_flow_iat_meaninputs_flow_iat_mininputs_fwd_iat_minunknown*
Tin	
2		*
Tout
2*
_collective_manager_ids
 *'
_output_shapes
:?????????* 
_read_only_resource_inputs
 *0
config_proto 

CPU

GPU2*0J 8? *Y
fTRR
P__inference_random_forest_model_1_layer_call_and_return_conditional_losses_227352
StatefulPartitionedCall?
IdentityIdentity StatefulPartitionedCall:output:0^StatefulPartitionedCall*
T0*'
_output_shapes
:?????????2

Identity"
identityIdentity:output:0*(
_construction_contextkEagerRuntime*a
_input_shapesP
N:?????????:?????????:?????????:?????????: 22
StatefulPartitionedCallStatefulPartitionedCall:_ [
'
_output_shapes
:?????????
0
_user_specified_nameinputs/bwd_pkt_len_std:]Y
'
_output_shapes
:?????????
.
_user_specified_nameinputs/flow_iat_mean:\X
'
_output_shapes
:?????????
-
_user_specified_nameinputs/flow_iat_min:[W
'
_output_shapes
:?????????
,
_user_specified_nameinputs/fwd_iat_min
?
[
-__inference_yggdrasil_model_path_tensor_23036
staticregexreplace_input
identity?
StaticRegexReplaceStaticRegexReplacestaticregexreplace_input*
_output_shapes
: *
patterndone*
rewrite 2
StaticRegexReplace^
IdentityIdentityStaticRegexReplace:output:0*
T0*
_output_shapes
: 2

Identity"
identityIdentity:output:0*(
_construction_contextkEagerRuntime*
_input_shapes
: : 

_output_shapes
: 
?*
?
P__inference_random_forest_model_1_layer_call_and_return_conditional_losses_22793

inputs
inputs_1
inputs_2	
inputs_3	
inference_op_model_handle
identity??inference_op{
strided_slice/stackConst*
_output_shapes
:*
dtype0*
valueB"        2
strided_slice/stack
strided_slice/stack_1Const*
_output_shapes
:*
dtype0*
valueB"       2
strided_slice/stack_1
strided_slice/stack_2Const*
_output_shapes
:*
dtype0*
valueB"      2
strided_slice/stack_2?
strided_sliceStridedSliceinputsstrided_slice/stack:output:0strided_slice/stack_1:output:0strided_slice/stack_2:output:0*
Index0*
T0*#
_output_shapes
:?????????*

begin_mask*
end_mask*
shrink_axis_mask2
strided_slice_
CastCastinputs_2*

DstT0*

SrcT0	*'
_output_shapes
:?????????2
Cast
strided_slice_1/stackConst*
_output_shapes
:*
dtype0*
valueB"        2
strided_slice_1/stack?
strided_slice_1/stack_1Const*
_output_shapes
:*
dtype0*
valueB"       2
strided_slice_1/stack_1?
strided_slice_1/stack_2Const*
_output_shapes
:*
dtype0*
valueB"      2
strided_slice_1/stack_2?
strided_slice_1StridedSliceCast:y:0strided_slice_1/stack:output:0 strided_slice_1/stack_1:output:0 strided_slice_1/stack_2:output:0*
Index0*
T0*#
_output_shapes
:?????????*

begin_mask*
end_mask*
shrink_axis_mask2
strided_slice_1c
Cast_1Castinputs_3*

DstT0*

SrcT0	*'
_output_shapes
:?????????2
Cast_1
strided_slice_2/stackConst*
_output_shapes
:*
dtype0*
valueB"        2
strided_slice_2/stack?
strided_slice_2/stack_1Const*
_output_shapes
:*
dtype0*
valueB"       2
strided_slice_2/stack_1?
strided_slice_2/stack_2Const*
_output_shapes
:*
dtype0*
valueB"      2
strided_slice_2/stack_2?
strided_slice_2StridedSlice
Cast_1:y:0strided_slice_2/stack:output:0 strided_slice_2/stack_1:output:0 strided_slice_2/stack_2:output:0*
Index0*
T0*#
_output_shapes
:?????????*

begin_mask*
end_mask*
shrink_axis_mask2
strided_slice_2
strided_slice_3/stackConst*
_output_shapes
:*
dtype0*
valueB"        2
strided_slice_3/stack?
strided_slice_3/stack_1Const*
_output_shapes
:*
dtype0*
valueB"       2
strided_slice_3/stack_1?
strided_slice_3/stack_2Const*
_output_shapes
:*
dtype0*
valueB"      2
strided_slice_3/stack_2?
strided_slice_3StridedSliceinputs_1strided_slice_3/stack:output:0 strided_slice_3/stack_1:output:0 strided_slice_3/stack_2:output:0*
Index0*
T0*#
_output_shapes
:?????????*

begin_mask*
end_mask*
shrink_axis_mask2
strided_slice_3?
stackPackstrided_slice:output:0strided_slice_3:output:0strided_slice_1:output:0strided_slice_2:output:0*
N*
T0*'
_output_shapes
:?????????*

axis2
stackU
ConstConst*
_output_shapes
:  *
dtype0*
value
B  2
ConstY
Const_1Const*
_output_shapes
:  *
dtype0*
value
B  2	
Const_1q
RaggedConstant/valuesConst*
_output_shapes
: *
dtype0*
valueB 2
RaggedConstant/valuesv
RaggedConstant/ConstConst*
_output_shapes
:*
dtype0	*
valueB	R 2
RaggedConstant/Constz
RaggedConstant/Const_1Const*
_output_shapes
:*
dtype0	*
valueB	R 2
RaggedConstant/Const_1?
inference_opSimpleMLInferenceOpWithHandlestack:output:0Const:output:0Const_1:output:0RaggedConstant/values:output:0RaggedConstant/Const:output:0RaggedConstant/Const_1:output:0inference_op_model_handle*-
_output_shapes
:?????????:*
dense_output_dim2
inference_op
strided_slice_4/stackConst*
_output_shapes
:*
dtype0*
valueB"       2
strided_slice_4/stack?
strided_slice_4/stack_1Const*
_output_shapes
:*
dtype0*
valueB"       2
strided_slice_4/stack_1?
strided_slice_4/stack_2Const*
_output_shapes
:*
dtype0*
valueB"      2
strided_slice_4/stack_2?
strided_slice_4StridedSlice inference_op:dense_predictions:0strided_slice_4/stack:output:0 strided_slice_4/stack_1:output:0 strided_slice_4/stack_2:output:0*
Index0*
T0*'
_output_shapes
:?????????*

begin_mask*
end_mask2
strided_slice_4{
IdentityIdentitystrided_slice_4:output:0^inference_op*
T0*'
_output_shapes
:?????????2

Identity"
identityIdentity:output:0*(
_construction_contextkEagerRuntime*a
_input_shapesP
N:?????????:?????????:?????????:?????????: 2
inference_opinference_op:O K
'
_output_shapes
:?????????
 
_user_specified_nameinputs:OK
'
_output_shapes
:?????????
 
_user_specified_nameinputs:OK
'
_output_shapes
:?????????
 
_user_specified_nameinputs:OK
'
_output_shapes
:?????????
 
_user_specified_nameinputs
?
K
__inference__creator_23041
identity??SimpleMLCreateModelResource?
SimpleMLCreateModelResourceSimpleMLCreateModelResource*
_output_shapes
: *E
shared_name64simple_ml_model_66aeb0c9-ce71-4522-9a16-619106ea948a2
SimpleMLCreateModelResource?
IdentityIdentity*SimpleMLCreateModelResource:model_handle:0^SimpleMLCreateModelResource*
T0*
_output_shapes
: 2

Identity"
identityIdentity:output:0*(
_construction_contextkEagerRuntime*
_input_shapes 2:
SimpleMLCreateModelResourceSimpleMLCreateModelResource
?+
?
__inference_call_23030
inputs_bwd_pkt_len_std
inputs_flow_iat_mean
inputs_flow_iat_min	
inputs_fwd_iat_min	
inference_op_model_handle
identity??inference_op{
strided_slice/stackConst*
_output_shapes
:*
dtype0*
valueB"        2
strided_slice/stack
strided_slice/stack_1Const*
_output_shapes
:*
dtype0*
valueB"       2
strided_slice/stack_1
strided_slice/stack_2Const*
_output_shapes
:*
dtype0*
valueB"      2
strided_slice/stack_2?
strided_sliceStridedSliceinputs_bwd_pkt_len_stdstrided_slice/stack:output:0strided_slice/stack_1:output:0strided_slice/stack_2:output:0*
Index0*
T0*#
_output_shapes
:?????????*

begin_mask*
end_mask*
shrink_axis_mask2
strided_slicej
CastCastinputs_flow_iat_min*

DstT0*

SrcT0	*'
_output_shapes
:?????????2
Cast
strided_slice_1/stackConst*
_output_shapes
:*
dtype0*
valueB"        2
strided_slice_1/stack?
strided_slice_1/stack_1Const*
_output_shapes
:*
dtype0*
valueB"       2
strided_slice_1/stack_1?
strided_slice_1/stack_2Const*
_output_shapes
:*
dtype0*
valueB"      2
strided_slice_1/stack_2?
strided_slice_1StridedSliceCast:y:0strided_slice_1/stack:output:0 strided_slice_1/stack_1:output:0 strided_slice_1/stack_2:output:0*
Index0*
T0*#
_output_shapes
:?????????*

begin_mask*
end_mask*
shrink_axis_mask2
strided_slice_1m
Cast_1Castinputs_fwd_iat_min*

DstT0*

SrcT0	*'
_output_shapes
:?????????2
Cast_1
strided_slice_2/stackConst*
_output_shapes
:*
dtype0*
valueB"        2
strided_slice_2/stack?
strided_slice_2/stack_1Const*
_output_shapes
:*
dtype0*
valueB"       2
strided_slice_2/stack_1?
strided_slice_2/stack_2Const*
_output_shapes
:*
dtype0*
valueB"      2
strided_slice_2/stack_2?
strided_slice_2StridedSlice
Cast_1:y:0strided_slice_2/stack:output:0 strided_slice_2/stack_1:output:0 strided_slice_2/stack_2:output:0*
Index0*
T0*#
_output_shapes
:?????????*

begin_mask*
end_mask*
shrink_axis_mask2
strided_slice_2
strided_slice_3/stackConst*
_output_shapes
:*
dtype0*
valueB"        2
strided_slice_3/stack?
strided_slice_3/stack_1Const*
_output_shapes
:*
dtype0*
valueB"       2
strided_slice_3/stack_1?
strided_slice_3/stack_2Const*
_output_shapes
:*
dtype0*
valueB"      2
strided_slice_3/stack_2?
strided_slice_3StridedSliceinputs_flow_iat_meanstrided_slice_3/stack:output:0 strided_slice_3/stack_1:output:0 strided_slice_3/stack_2:output:0*
Index0*
T0*#
_output_shapes
:?????????*

begin_mask*
end_mask*
shrink_axis_mask2
strided_slice_3?
stackPackstrided_slice:output:0strided_slice_3:output:0strided_slice_1:output:0strided_slice_2:output:0*
N*
T0*'
_output_shapes
:?????????*

axis2
stackU
ConstConst*
_output_shapes
:  *
dtype0*
value
B  2
ConstY
Const_1Const*
_output_shapes
:  *
dtype0*
value
B  2	
Const_1q
RaggedConstant/valuesConst*
_output_shapes
: *
dtype0*
valueB 2
RaggedConstant/valuesv
RaggedConstant/ConstConst*
_output_shapes
:*
dtype0	*
valueB	R 2
RaggedConstant/Constz
RaggedConstant/Const_1Const*
_output_shapes
:*
dtype0	*
valueB	R 2
RaggedConstant/Const_1?
inference_opSimpleMLInferenceOpWithHandlestack:output:0Const:output:0Const_1:output:0RaggedConstant/values:output:0RaggedConstant/Const:output:0RaggedConstant/Const_1:output:0inference_op_model_handle*-
_output_shapes
:?????????:*
dense_output_dim2
inference_op
strided_slice_4/stackConst*
_output_shapes
:*
dtype0*
valueB"       2
strided_slice_4/stack?
strided_slice_4/stack_1Const*
_output_shapes
:*
dtype0*
valueB"       2
strided_slice_4/stack_1?
strided_slice_4/stack_2Const*
_output_shapes
:*
dtype0*
valueB"      2
strided_slice_4/stack_2?
strided_slice_4StridedSlice inference_op:dense_predictions:0strided_slice_4/stack:output:0 strided_slice_4/stack_1:output:0 strided_slice_4/stack_2:output:0*
Index0*
T0*'
_output_shapes
:?????????*

begin_mask*
end_mask2
strided_slice_4{
IdentityIdentitystrided_slice_4:output:0^inference_op*
T0*'
_output_shapes
:?????????2

Identity"
identityIdentity:output:0*(
_construction_contextkEagerRuntime*a
_input_shapesP
N:?????????:?????????:?????????:?????????: 2
inference_opinference_op:_ [
'
_output_shapes
:?????????
0
_user_specified_nameinputs/bwd_pkt_len_std:]Y
'
_output_shapes
:?????????
.
_user_specified_nameinputs/flow_iat_mean:\X
'
_output_shapes
:?????????
-
_user_specified_nameinputs/flow_iat_min:[W
'
_output_shapes
:?????????
,
_user_specified_nameinputs/fwd_iat_min
?
,
__inference__destroyer_23054
identityP
ConstConst*
_output_shapes
: *
dtype0*
value	B :2
ConstQ
IdentityIdentityConst:output:0*
T0*
_output_shapes
: 2

Identity"
identityIdentity:output:0*(
_construction_contextkEagerRuntime*
_input_shapes 
?
?
__inference__initializer_23049
staticregexreplace_input>
:simple_ml_simplemlloadmodelfrompathwithhandle_model_handle
identity??-simple_ml/SimpleMLLoadModelFromPathWithHandle?
StaticRegexReplaceStaticRegexReplacestaticregexreplace_input*
_output_shapes
: *
patterndone*
rewrite 2
StaticRegexReplace?
-simple_ml/SimpleMLLoadModelFromPathWithHandle#SimpleMLLoadModelFromPathWithHandle:simple_ml_simplemlloadmodelfrompathwithhandle_model_handleStaticRegexReplace:output:0*
_output_shapes
 2/
-simple_ml/SimpleMLLoadModelFromPathWithHandleP
ConstConst*
_output_shapes
: *
dtype0*
value	B :2
Const?
IdentityIdentityConst:output:0.^simple_ml/SimpleMLLoadModelFromPathWithHandle*
T0*
_output_shapes
: 2

Identity"
identityIdentity:output:0*(
_construction_contextkEagerRuntime*
_input_shapes
: : 2^
-simple_ml/SimpleMLLoadModelFromPathWithHandle-simple_ml/SimpleMLLoadModelFromPathWithHandle: 

_output_shapes
: 
?
?
!__inference__traced_restore_23151
file_prefix%
assignvariableop_is_trained:
 "
assignvariableop_1_total: "
assignvariableop_2_count: $
assignvariableop_3_total_1: $
assignvariableop_4_count_1: 

identity_6??AssignVariableOp?AssignVariableOp_1?AssignVariableOp_2?AssignVariableOp_3?AssignVariableOp_4?
RestoreV2/tensor_namesConst"/device:CPU:0*
_output_shapes
:*
dtype0*?
value?B?B&_is_trained/.ATTRIBUTES/VARIABLE_VALUEB4keras_api/metrics/0/total/.ATTRIBUTES/VARIABLE_VALUEB4keras_api/metrics/0/count/.ATTRIBUTES/VARIABLE_VALUEB4keras_api/metrics/1/total/.ATTRIBUTES/VARIABLE_VALUEB4keras_api/metrics/1/count/.ATTRIBUTES/VARIABLE_VALUEB_CHECKPOINTABLE_OBJECT_GRAPH2
RestoreV2/tensor_names?
RestoreV2/shape_and_slicesConst"/device:CPU:0*
_output_shapes
:*
dtype0*
valueBB B B B B B 2
RestoreV2/shape_and_slices?
	RestoreV2	RestoreV2file_prefixRestoreV2/tensor_names:output:0#RestoreV2/shape_and_slices:output:0"/device:CPU:0*,
_output_shapes
::::::*
dtypes

2
2
	RestoreV2g
IdentityIdentityRestoreV2:tensors:0"/device:CPU:0*
T0
*
_output_shapes
:2

Identity?
AssignVariableOpAssignVariableOpassignvariableop_is_trainedIdentity:output:0"/device:CPU:0*
_output_shapes
 *
dtype0
2
AssignVariableOpk

Identity_1IdentityRestoreV2:tensors:1"/device:CPU:0*
T0*
_output_shapes
:2

Identity_1?
AssignVariableOp_1AssignVariableOpassignvariableop_1_totalIdentity_1:output:0"/device:CPU:0*
_output_shapes
 *
dtype02
AssignVariableOp_1k

Identity_2IdentityRestoreV2:tensors:2"/device:CPU:0*
T0*
_output_shapes
:2

Identity_2?
AssignVariableOp_2AssignVariableOpassignvariableop_2_countIdentity_2:output:0"/device:CPU:0*
_output_shapes
 *
dtype02
AssignVariableOp_2k

Identity_3IdentityRestoreV2:tensors:3"/device:CPU:0*
T0*
_output_shapes
:2

Identity_3?
AssignVariableOp_3AssignVariableOpassignvariableop_3_total_1Identity_3:output:0"/device:CPU:0*
_output_shapes
 *
dtype02
AssignVariableOp_3k

Identity_4IdentityRestoreV2:tensors:4"/device:CPU:0*
T0*
_output_shapes
:2

Identity_4?
AssignVariableOp_4AssignVariableOpassignvariableop_4_count_1Identity_4:output:0"/device:CPU:0*
_output_shapes
 *
dtype02
AssignVariableOp_49
NoOpNoOp"/device:CPU:0*
_output_shapes
 2
NoOp?

Identity_5Identityfile_prefix^AssignVariableOp^AssignVariableOp_1^AssignVariableOp_2^AssignVariableOp_3^AssignVariableOp_4^NoOp"/device:CPU:0*
T0*
_output_shapes
: 2

Identity_5?

Identity_6IdentityIdentity_5:output:0^AssignVariableOp^AssignVariableOp_1^AssignVariableOp_2^AssignVariableOp_3^AssignVariableOp_4*
T0*
_output_shapes
: 2

Identity_6"!

identity_6Identity_6:output:0*
_input_shapes
: : : : : : 2$
AssignVariableOpAssignVariableOp2(
AssignVariableOp_1AssignVariableOp_12(
AssignVariableOp_2AssignVariableOp_22(
AssignVariableOp_3AssignVariableOp_32(
AssignVariableOp_4AssignVariableOp_4:C ?

_output_shapes
: 
%
_user_specified_namefile_prefix"?L
saver_filename:0StatefulPartitionedCall_2:0StatefulPartitionedCall_38"
saved_model_main_op

NoOp*>
__saved_model_init_op%#
__saved_model_init_op

NoOp*?
serving_default?
K
bwd_pkt_len_std8
!serving_default_bwd_pkt_len_std:0?????????
G
flow_iat_mean6
serving_default_flow_iat_mean:0?????????
E
flow_iat_min5
serving_default_flow_iat_min:0	?????????
C
fwd_iat_min4
serving_default_fwd_iat_min:0	?????????>
output_12
StatefulPartitionedCall_1:0?????????tensorflow/serving/predict2"

asset_path_initializer:0done2)

asset_path_initializer_1:0	header.pb27

asset_path_initializer_2:0random_forest_header.pb24

asset_path_initializer_3:0nodes-00000-of-000012,

asset_path_initializer_4:0data_spec.pb:?U
?
_learner_params
	_features
_is_trained
	optimizer
loss

_model
regularization_losses
trainable_variables
		variables

	keras_api

signatures
(__call__
)_default_save_signature
**&call_and_return_all_conditional_losses
+call
,yggdrasil_model_path_tensor"?
_tf_keras_model?{"name": "random_forest_model_1", "trainable": true, "expects_training_arg": true, "dtype": "float32", "batch_input_shape": null, "must_restore_from_config": false, "class_name": "RandomForestModel", "config": {"layer was saved without config": true}, "is_graph_network": false, "save_spec": {"bwd_pkt_len_std": {"class_name": "TypeSpec", "type_spec": "tf.TensorSpec", "serialized": [{"class_name": "TensorShape", "items": [null, 1]}, "float32", "bwd_pkt_len_std"]}, "flow_iat_min": {"class_name": "TypeSpec", "type_spec": "tf.TensorSpec", "serialized": [{"class_name": "TensorShape", "items": [null, 1]}, "int64", "flow_iat_min"]}, "fwd_iat_min": {"class_name": "TypeSpec", "type_spec": "tf.TensorSpec", "serialized": [{"class_name": "TensorShape", "items": [null, 1]}, "int64", "fwd_iat_min"]}, "flow_iat_mean": {"class_name": "TypeSpec", "type_spec": "tf.TensorSpec", "serialized": [{"class_name": "TensorShape", "items": [null, 1]}, "float32", "flow_iat_mean"]}}, "keras_version": "2.5.0", "backend": "tensorflow", "model_config": {"class_name": "RandomForestModel"}, "training_config": {"loss": null, "metrics": [[{"class_name": "MeanMetricWrapper", "config": {"name": "accuracy", "dtype": "float32", "fn": "binary_accuracy"}, "shared_object_id": 0}]], "weighted_metrics": null, "loss_weights": null, "optimizer_config": {"class_name": "RMSprop", "config": {"name": "RMSprop", "learning_rate": 0.001, "decay": 0.0, "rho": 0.9, "momentum": 0.0, "epsilon": 1e-07, "centered": false}}}}
 "
trackable_dict_wrapper
 "
trackable_list_wrapper
:
 2
is_trained
"
	optimizer
 "
trackable_dict_wrapper
G
_input_builder
_compiled_model"
_generic_user_object
 "
trackable_list_wrapper
 "
trackable_list_wrapper
'
0"
trackable_list_wrapper
?
layer_metrics
regularization_losses

layers
trainable_variables
metrics
layer_regularization_losses
non_trainable_variables
		variables
(__call__
)_default_save_signature
**&call_and_return_all_conditional_losses
&*"call_and_return_conditional_losses"
_generic_user_object
,
-serving_default"
signature_map
l
_feature_name_to_idx
	_init_ops
#categorical_str_to_int_hashmaps"
_generic_user_object
S
_model_loader
._create_resource
/_initialize
0_destroy_resourceR 
 "
trackable_dict_wrapper
 "
trackable_list_wrapper
.
0
1"
trackable_list_wrapper
 "
trackable_list_wrapper
'
0"
trackable_list_wrapper
 "
trackable_dict_wrapper
 "
trackable_list_wrapper
 "
trackable_dict_wrapper
>

_all_files

_done_file"
_generic_user_object
?
	total
	count
	variables
	keras_api"?
_tf_keras_metric?{"class_name": "Mean", "name": "loss", "dtype": "float32", "config": {"name": "loss", "dtype": "float32"}, "shared_object_id": 1}
?
	total
	 count
!
_fn_kwargs
"	variables
#	keras_api"?
_tf_keras_metric?{"class_name": "MeanMetricWrapper", "name": "accuracy", "dtype": "float32", "config": {"name": "accuracy", "dtype": "float32", "fn": "binary_accuracy"}, "shared_object_id": 0}
C
0
$1
%2
&3
'4"
trackable_list_wrapper
* 
:  (2total
:  (2count
.
0
1"
trackable_list_wrapper
-
	variables"
_generic_user_object
:  (2total
:  (2count
 "
trackable_dict_wrapper
.
0
 1"
trackable_list_wrapper
-
"	variables"
_generic_user_object
*
*
*
*
?2?
5__inference_random_forest_model_1_layer_call_fn_22740
5__inference_random_forest_model_1_layer_call_fn_22906
5__inference_random_forest_model_1_layer_call_fn_22916
5__inference_random_forest_model_1_layer_call_fn_22808?
???
FullArgSpec)
args!?
jself
jinputs

jtraining
varargs
 
varkw
 
defaults?
p 

kwonlyargs? 
kwonlydefaults? 
annotations? *
 
?2?
 __inference__wrapped_model_22690?
???
FullArgSpec
args? 
varargsjargs
varkw
 
defaults
 

kwonlyargs? 
kwonlydefaults
 
annotations? *???
???
<
bwd_pkt_len_std)?&
bwd_pkt_len_std?????????
8
flow_iat_mean'?$
flow_iat_mean?????????
6
flow_iat_min&?#
flow_iat_min?????????	
4
fwd_iat_min%?"
fwd_iat_min?????????	
?2?
P__inference_random_forest_model_1_layer_call_and_return_conditional_losses_22954
P__inference_random_forest_model_1_layer_call_and_return_conditional_losses_22992
P__inference_random_forest_model_1_layer_call_and_return_conditional_losses_22846
P__inference_random_forest_model_1_layer_call_and_return_conditional_losses_22884?
???
FullArgSpec)
args!?
jself
jinputs

jtraining
varargs
 
varkw
 
defaults?
p 

kwonlyargs? 
kwonlydefaults? 
annotations? *
 
?2?
__inference_call_23030?
???
FullArgSpec)
args!?
jself
jinputs

jtraining
varargs
 
varkw
 
defaults?
p 

kwonlyargs? 
kwonlydefaults
 
annotations? *
 
?2?
-__inference_yggdrasil_model_path_tensor_23036?
???
FullArgSpec
args?
jself
varargs
 
varkw
 
defaults
 

kwonlyargs? 
kwonlydefaults
 
annotations? *? 
?B?
#__inference_signature_wrapper_22896bwd_pkt_len_stdflow_iat_meanflow_iat_minfwd_iat_min"?
???
FullArgSpec
args? 
varargs
 
varkwjkwargs
defaults
 

kwonlyargs? 
kwonlydefaults
 
annotations? *
 
?2?
__inference__creator_23041?
???
FullArgSpec
args? 
varargs
 
varkw
 
defaults
 

kwonlyargs? 
kwonlydefaults
 
annotations? *? 
?2?
__inference__initializer_23049?
???
FullArgSpec
args? 
varargs
 
varkw
 
defaults
 

kwonlyargs? 
kwonlydefaults
 
annotations? *? 
?2?
__inference__destroyer_23054?
???
FullArgSpec
args? 
varargs
 
varkw
 
defaults
 

kwonlyargs? 
kwonlydefaults
 
annotations? *? 6
__inference__creator_23041?

? 
? "? 8
__inference__destroyer_23054?

? 
? "? >
__inference__initializer_23049?

? 
? "? ?
 __inference__wrapped_model_22690????
???
???
<
bwd_pkt_len_std)?&
bwd_pkt_len_std?????????
8
flow_iat_mean'?$
flow_iat_mean?????????
6
flow_iat_min&?#
flow_iat_min?????????	
4
fwd_iat_min%?"
fwd_iat_min?????????	
? "3?0
.
output_1"?
output_1??????????
__inference_call_23030????
???
???
C
bwd_pkt_len_std0?-
inputs/bwd_pkt_len_std?????????
?
flow_iat_mean.?+
inputs/flow_iat_mean?????????
=
flow_iat_min-?*
inputs/flow_iat_min?????????	
;
fwd_iat_min,?)
inputs/fwd_iat_min?????????	
p 
? "???????????
P__inference_random_forest_model_1_layer_call_and_return_conditional_losses_22846????
???
???
<
bwd_pkt_len_std)?&
bwd_pkt_len_std?????????
8
flow_iat_mean'?$
flow_iat_mean?????????
6
flow_iat_min&?#
flow_iat_min?????????	
4
fwd_iat_min%?"
fwd_iat_min?????????	
p 
? "%?"
?
0?????????
? ?
P__inference_random_forest_model_1_layer_call_and_return_conditional_losses_22884????
???
???
<
bwd_pkt_len_std)?&
bwd_pkt_len_std?????????
8
flow_iat_mean'?$
flow_iat_mean?????????
6
flow_iat_min&?#
flow_iat_min?????????	
4
fwd_iat_min%?"
fwd_iat_min?????????	
p
? "%?"
?
0?????????
? ?
P__inference_random_forest_model_1_layer_call_and_return_conditional_losses_22954????
???
???
C
bwd_pkt_len_std0?-
inputs/bwd_pkt_len_std?????????
?
flow_iat_mean.?+
inputs/flow_iat_mean?????????
=
flow_iat_min-?*
inputs/flow_iat_min?????????	
;
fwd_iat_min,?)
inputs/fwd_iat_min?????????	
p 
? "%?"
?
0?????????
? ?
P__inference_random_forest_model_1_layer_call_and_return_conditional_losses_22992????
???
???
C
bwd_pkt_len_std0?-
inputs/bwd_pkt_len_std?????????
?
flow_iat_mean.?+
inputs/flow_iat_mean?????????
=
flow_iat_min-?*
inputs/flow_iat_min?????????	
;
fwd_iat_min,?)
inputs/fwd_iat_min?????????	
p
? "%?"
?
0?????????
? ?
5__inference_random_forest_model_1_layer_call_fn_22740????
???
???
<
bwd_pkt_len_std)?&
bwd_pkt_len_std?????????
8
flow_iat_mean'?$
flow_iat_mean?????????
6
flow_iat_min&?#
flow_iat_min?????????	
4
fwd_iat_min%?"
fwd_iat_min?????????	
p 
? "???????????
5__inference_random_forest_model_1_layer_call_fn_22808????
???
???
<
bwd_pkt_len_std)?&
bwd_pkt_len_std?????????
8
flow_iat_mean'?$
flow_iat_mean?????????
6
flow_iat_min&?#
flow_iat_min?????????	
4
fwd_iat_min%?"
fwd_iat_min?????????	
p
? "???????????
5__inference_random_forest_model_1_layer_call_fn_22906????
???
???
C
bwd_pkt_len_std0?-
inputs/bwd_pkt_len_std?????????
?
flow_iat_mean.?+
inputs/flow_iat_mean?????????
=
flow_iat_min-?*
inputs/flow_iat_min?????????	
;
fwd_iat_min,?)
inputs/fwd_iat_min?????????	
p 
? "???????????
5__inference_random_forest_model_1_layer_call_fn_22916????
???
???
C
bwd_pkt_len_std0?-
inputs/bwd_pkt_len_std?????????
?
flow_iat_mean.?+
inputs/flow_iat_mean?????????
=
flow_iat_min-?*
inputs/flow_iat_min?????????	
;
fwd_iat_min,?)
inputs/fwd_iat_min?????????	
p
? "???????????
#__inference_signature_wrapper_22896????
? 
???
<
bwd_pkt_len_std)?&
bwd_pkt_len_std?????????
8
flow_iat_mean'?$
flow_iat_mean?????????
6
flow_iat_min&?#
flow_iat_min?????????	
4
fwd_iat_min%?"
fwd_iat_min?????????	"3?0
.
output_1"?
output_1?????????L
-__inference_yggdrasil_model_path_tensor_23036?

? 
? "? 