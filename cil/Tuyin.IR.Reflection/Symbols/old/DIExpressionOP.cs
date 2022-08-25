namespace Tuyin.IR.Reflection.Symbols.old
{
    public enum DIExpressionOP
    {
        DW_OP_deref, // 取消引用表达式堆栈的顶部。
        DW_OP_plus, // 从表达式堆栈中弹出最后两个条目，将它们加在一起并将结果附加到表达式堆栈中。
        DW_OP_minus, //  从表达式堆栈中弹出最后两个条目，从倒数第二个条目中减去最后一个条目并将结果附加到表达式堆栈中。
        DW_OP_plus_uconst, // , 93添加93到工作表达。
        DW_OP_LLVM_fragment, // , 16, 8 从工作表达式中指定变量片段的偏移量和大小（分别在此处16和8此处）。请注意，与 DW_OP_bit_piece 不同的是，偏移量是描述所描述的源变量内的位置。
        DW_OP_LLVM_convert, // , 16, DW_ATE_signed指定表达式堆栈顶部要转换为的位大小和编码（16和DW_ATE_signed这里分别是）。映射到DW_OP_convert引用从提供的值构造的基类型的操作。
        DW_OP_LLVM_tag_offset, // , tag_offset指定应选择性地将内存标记应用于指针。内存标签以实现定义的方式从给定的标签偏移量派生而来。
        DW_OP_swap, //  交换顶部的两个堆栈条目。
        DW_OP_xderef, // 提供扩展的解引用机制。堆栈顶部的条目被视为地址。第二个堆栈条目被视为地址空间标识符。
        DW_OP_stack_value, // 标记一个常数值。
        DW_OP_LLVM_entry_value, // , N可能只出现在 MIR 和DIExpression. 在 DWARF 中，将 aDBG_VALUE 绑定DIExpression(DW_OP_LLVM_entry_value到寄存器的指令降低为 a ，将函数进入时寄存器的值压入堆栈。接下来的 操作将是 块参数的一部分。例如，指定一个表达式，其中调试值指令的值/地址操作数的入口值被压入堆栈，并添加123。由于框架限制，目前只能为1。DW_OP_entry_value [reg](N - 1)DW_OP_entry_value!DIExpression(DW_OP_LLVM_entry_value, 1, DW_OP_plus_uconst, 123, DW_OP_stack_value)N
        //该操作由LiveDebugValuespass引入，它仅将其应用于整个函数中未修改的函数参数。支持仅限于简单的寄存器位置描述，或作为间接位置（例如，当结构体通过指向调用者中临时副本的指针按值传递给被调用者时）。AsmPrinter当调用站点参数值 ( DW_AT_call_site_parameter_value) 表示为参数的入口值时，入口值 op 也由传递引入 。
        DW_OP_LLVM_arg, // , N用于引用多个值的调试内在函数，例如计算两个寄存器之和的值。这总是与值的有序列表结合使用，这样 引用与列表一起使用的将评估为 。此值列表应由包含的内在/指令提供。DW_OP_LLVM_arg, NN``th element in that list. For example, ``!DIExpression(DW_OP_LLVM_arg, 0, DW_OP_LLVM_arg, 1, DW_OP_minus, DW_OP_stack_value)(%reg1, %reg2)%reg1 - reg2
        DW_OP_breg, // (或DW_OP_bregx) 表示指定寄存器的提供的带符号偏移量上的内容。操作码仅由AsmPrinter描述调用站点参数值的传递生成， 这需要两个寄存器的表达式。
        DW_OP_push_object_address, // 推送对象的地址，该地址可以作为后续计算中的描述符。此操作码可用于计算具有数组描述符的 fortran 可分配数组的边界。
        DW_OP_over, // 复制当前在堆栈顶部的堆栈中的第二个条目。此操作码可用于计算 Fortran 假定秩数组的边界，该数组在运行时具有已知秩且当前维数隐式为堆栈的第一个元素。
        DW_OP_LLVM_implicit_pointer, // 它指定取消引用的值。它可用于表示已优化但其指向的值已知的指针变量。此运算符是必需的，因为它在表示和规范（操作数的数量和类型）方面与 DWARF 运算符 DW_OP_implicit_pointer 不同，并且以后不能用作多级。
    }
}
