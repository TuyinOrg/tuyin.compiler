using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tuyin.IR.Compiler.Target
{
    enum Errors : int
    {
        [Description("运算符'{0}'无法应用于'{1}'和'{2}'类型的操作数")]
        BinaryOperator = 201,
        [Description("运算符'{0}'无法应用于'{1}'类型的操作数")]
        UnaryOperator = 202,
        [Description("没有要中断或继续的封闭循环")]
        NoLoop = 203,
        [Description("未查找到定义：'{0}'")]
        NotDefine = 204,
        [Description("只能在函数中使用'{0}'文法")]
        InFunction = 205,
        [Description("'{0}'函数没有采用 {1} 个参数")]
        OverParamters = 206,
        [Description("表达式不是有效函数")]
        NotFunction = 207,
        [Description("无法将'{0}'转换为'{1}'类型")]
        NotConvert = 208,
        [Description("表达式不是数组类型")]
        NotArray = 209,
        [Description("该成员不包含所调用函数需要的参数'{0}'")]
        NotExsit = 210,
        [Description("表达式左边不是结构类型，无法引用成员")]
        NotStruct = 211,
        [Description("{0}已经包含了一个'{1}'的定义，不可重复定义")]
        Duplicate = 212,
        [Description("'{0}'结构的{2}'{1}'在内存布局中造成循环")]
        StructLoop = 213,
        [Description("未查找到'{0}'的定义或命名空间")]
        NoPath = 214,
        [Description("import文法中需要为定义添加一个别名")]
        NoAlias = 215,
        [Description("别名'{0}'重复定义")]
        DuplicateAlias = 216,
        [Description("'{0}'是{1},它不可以在变量表达式中使用")]
        NotDeclarator = 217,
        [Description("'{0}'不是结构类型，无法使用new表达式")]
        NotNew = 218,
        [Description("命名空间'{0}'中的路径'{1}'存在二义性，请检查是否在同一命名空间下存在相同定义或与命名空间节点冲突")]
        Ambiguity = 219,
        [Description("char表达式'{0}'的长度过短")]
        CharShort = 220,
        [Description("char表达式'{0}'的长度过长")]
        CharLong = 221,
        [Description("{0}'{1}'不是一个区域变量,无法为其赋值")]
        NotVariable = 222,
        [Description("{0}'{1}'类型不可限定为推导类型")]
        NotAllowAutoType = 223,
        [Description("{0}限定为{1}")]
        StructLimit = 224,
        [Description("未实现接口成员'{0}'")]
        NotImplemented = 225,
        [Description("无法将'{0}'转换为成员'{2}'的限定类型'{1}'")]
        NotConvertMember = 226,
        [Description("函数'{0}'并非所有的代码路径都返回值")]
        NoReturn = 227,
        [Description("'{0}'与'{1}'引用不明确")]
        UsingAmbiguity = 228,
        [Description("参数名'{0}'重复")]
        FormalDuplicate = 229,
        [Description("访问'{0}'时产生与该文件的循环引用冲突")]
        CircularUsing = 230,
        [Description("数组初始值设定项必须大于等于1")]
        ArrayLength = 231,
        [Description("结构类型'{0}'未定义字段'{0}'")]
        NotField = 232,
        [Description("未定义区域变量'{0}'")]
        NotDefineVariable = 233,
        [Description("缺少转义序列")]
        MissingEscape = 234,
        [Description("无法识别的转义序列")]
        UnrecognizedEscape = 235,
        [Description("路径'{0}'是一个命名空间，无法作为类型使用")]
        NotConvertPathToType = 236,
        [Description("函数'{0}'返回类型不明确")]
        FunctionReturnDyanmic = 237,
        [Description("'{0}'不是一个函数或类型，无法为其设定别名")]
        NoAliasDefine = 238,
        [Description("缺少参数")]
        MissParamter = 239,
        [Description("缺少return表达式")]
        MissReturnExpreesion = 240,
        [Description("区域变量'{0}'缺少有效初始设定值")]
        InvaildInitValue = 241,
        [Description("区域变量不支持修改访问声明")]
        LocalVariableNotSupportModifiiter = 242,
        [Description("表达式左边不是成员，索引或变量，无非为其赋值")]
        NotAllowAssignment = 243,
        [Description("嵌入的语句不能是声明或标记语句")]
        EmbeddedStatementLimit = 244,
        [Description("函数元数据不是常量表达式")]
        FunctionMetadataConst = 245,
        [Description("函数'{0}'已经包含了一个名为'{1}'的元数据，不可重复定义")]
        FunctionMetadataDuplicate = 246,
        [Description("无法解析的数字,可能超出{0}最大范围")]
        InvaildNumber = 247,
        [Description("match表达式default标签重复定义")]
        DuplicateMatchDefualt = 248
    }
}
