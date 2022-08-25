---
标题：Tuyin IR语言指令集
描述：对 中间码 使用有用的资料和其他资源。
---
 # Tuyin IR语言指令集

## Opcodes

|名称|堆栈变化|操作数类型|控制流|说明|
|-----|--------|------------|-------|------------------------------------|
|add|-1|none|next|将两个值相加并将结果推送到计算堆栈上。|
|and|-1|none|next|计算两个值的按位"与"并将结果推送到计算堆栈上。|
|beg|-2|uint32|branch|如果两个值相等，则将控制转移到目标指令。|
|bge|-2|uint32|branch|如果第一个值大于或等于第二个值，则将控制转移到目标指令。|
|bgt|-2|uint32|branch| 如果第一个值大于第二个值，则将控制转移到目标指令。|
|ble|-2|uint32|branch| 如果第一个值小于或等于第二个值，则将控制转移到目标指令。|
|blt|-2|uint32|branch| 如果第一个值小于第二个值，则将控制转移到目标指令。|
|br|0|uint32|jump| 如果第一个值小于第二个值，则将控制转移到目标指令。|
|brfalse|-1|uint32|jump| 如果 value 为 false、空引用或零，则将控制转移到目标指令。|
|brtrue|-1|uint32|jump| 如果 value 为 true、非空或非零，则将控制转移到目标指令。|
|call|0|uint32|call| 调用方法 参数:调用方法传递的参数数量。|
|ceq|-1|none|next| 比较两个值。 如果这两个值相等，则将整数值 1 (int32) 推送到计算堆栈上；否则，将 0 (int32) 推送到计算堆栈上。|
|cgt|-1|none|next| 比较两个值。 如果第一个值大于第二个值，则将整数值 1 (int32) 推送到计算堆栈上；反之，将 0 (int32) 推送到计算堆栈上。|
|clt|-1|none|next| 比较两个值。 如果第一个值小于第二个值，则将整数值 1 (int32) 推送到计算堆栈上；反之，将 0 (int32) 推送到计算堆栈上。|
|div|-1|none|next| 将两个值相除并将结果作为浮点 Number 推送到计算堆栈上。|
|ldstr|1|string|next| 将字符串推送到计算堆栈上。|
|ldarg|1|uint32|next| 将索引 x 参数推送到计算堆栈上。|
|ldarg.0|1|none|next| 将索引 0 参数推送到计算堆栈上。|
|ldarg.1|1|none|next| 将索引 1 参数推送到计算堆栈上。|
|ldarg.2|1|none|next| 将索引 2 参数推送到计算堆栈上。|
|ldarg.3|1|none|next| 将索引 3 参数推送到计算堆栈上。|
|ldloc|1|uint32|next| 将索引 x 区域变量推送到计算堆栈上。|
|ldloc.0|1|none|next| 将索引 0 区域变量推送到计算堆栈上。|
|ldloc.1|1|none|next| 将索引 1 区域变量推送到计算堆栈上。|
|ldloc.2|1|none|next| 将索引 2 区域变量推送到计算堆栈上。|
|ldloc.3|1|none|next| 将索引 3 区域变量推送到计算堆栈上。|
|ldc.r4|1|float32|next| 将 x 作为 Float32 推送到计算堆栈上。|
|ldc.r8|1|float64|next| 将 x 作为 Float64 推送到计算堆栈上。|
|ldc.l|1|int64|next| 将 x 作为 Int64 推送到计算堆栈上。|
|ldc|1|int32|next| 将 x 作为 Int32 推送到计算堆栈上。|
|ldc.m1|1|int32|next| 将Int32类型 -1 作为 Number 推送到计算堆栈上。|
|ldc.0|1|int32|next| 将Int32类型 -1 作为 Number 推送到计算堆栈上。|
|ldc.1|1|int32|next| 将Int32类型 -1 作为 Number 推送到计算堆栈上。|
|ldc.2|1|int32|next| 将Int32类型 -1 作为 Number 推送到计算堆栈上。|
|ldc.3|1|int32|next| 将Int32类型 -1 作为 Number 推送到计算堆栈上。|
|ldc.4|1|int32|next| 将Int32类型 -1 作为 Number 推送到计算堆栈上。|
|ldc.5|1|int32|next| 将Int32类型 -1 作为 Number 推送到计算堆栈上。|
|ldc.6|1|int32|next| 将Int32类型 -1 作为 Number 推送到计算堆栈上。|
|ldc.7|1|int32|next| 将Int32类型 -1 作为 Number 推送到计算堆栈上。|
|ldc.8|1|int32|next| 将Int32类型 -1 作为 Number 推送到计算堆栈上。|
|ldc.9|1|int32|next| 将Int32类型 -1 作为 Number 推送到计算堆栈上。|
|mul|-1|none|next| 将两个值相乘并将结果推送到计算堆栈上。|
|neg|0|none|next| 对一个值执行求反并将结果推送到计算堆栈上。|
|nop|0|none|next| 如果修补操作码，则填充空间。 尽管可能消耗处理周期，但未执行任何有意义的操作。|
|not|0|none|next| 计算堆栈顶部整数值的按位求补并将结果作为相同的类型推送到计算堆栈上。|
|or|-1|none|next| 计算位于堆栈顶部的两个整数值的按位求补并将结果推送到计算堆栈上。|
|peek|1|none|next| 复制堆栈顶层值并推入堆栈。|
|pop|-1|none|next| 移除当前位于计算堆栈顶部的值。|
|rem|-1|none|next| 将两个值相除并将余数推送到计算堆栈上。|
|ret|0|none|next| 从当前方法返回，并将返回值（如果存在）从调用方的计算堆栈推送到被调用方的计算堆栈上。|
|shl|-1|none|next| 将整数值左移（用零填充）指定的位数，并将结果推送到计算堆栈上。|
|shr|-1|none|next| 将整数值右移（保留符号）指定的位数，并将结果推送到计算堆栈上。|
|sub|-1|none|next| 用计算堆栈中的值赋值给指定索引处的区域变量。|
|throw|-1|none|throw| 引发当前位于计算堆栈上的异常对象。|
|xor|-1|none|next| 计算位于计算堆栈顶部的两个值的按位异或，并且将结果推送到计算堆栈上。|
|breakpoint|0|none|break| 暂停程序并，通知（如果被调试器捕获进程）当前位于计算堆栈上地址。|

## 函数

|名称|堆栈变化|操作数类型|控制流|说明|
|-----|--------|------------|-------|------------------------------------|
|@tuyin.block.start|
|@tuyin.block.end|
|@tuyin.document.seq