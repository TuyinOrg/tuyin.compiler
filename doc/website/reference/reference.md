---
标题：Tuyin IR引用文献
描述：对 中间码 使用有用的资料和其他资源。
---
 # Tuyin IR引用文献

- A Graph-Based Higher-Order Intermediate Representation 一种基于图形的高阶中间表示
	Roland Leissa, Marcel Koester, and Sebastian Hack
- Simple and Efficient Construction of Static Single Assignment Form 简单高效地构建静态单一赋值表单
	Matthias Braun, Sebastian Buchwald, Sebastian Hack, Roland Leissa, Christoph Mallon and Andreas Zwinkau
- A Simple, Fast Dominance Algorithm 简单、快速的优势算法
	Keith D. Cooper, Timothy J. Harvey and Ken Kennedy
- Fast Half Float Conversions 快速半浮点数转换
	Jeroen van der Zijp
- Identifying Loops In Almost Linear Time 在几乎线性时间内识别环路
	G. Ramalingam
- 带子图的状态机MFA构建 
	王颉
- 基于MFA的数学运算与类型推导
	王颉
- 基于黏菌觅食算法的空间着色法,用于寄存器分配
	王颉
- 三数之和


如果消除分支是你的目标，那么你可能希望考虑数学，或者一些非可移植的解决方案。
请考虑以下示例：

if (a < b)
    y = C;
else
    y = D;

这可以重写为...

x = -(a < b);   /* x = -1 if a < b, x = 0 if a >= b */
x &= (C - D);   /* x = C - D if a < b, x = 0 if a >= b */
x += D;         /* x = C if a < b, x = D if a >= b */

