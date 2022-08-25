using System;
using System.Globalization;

[Serializable]
public struct BigInteger :
        IComparable,
        IComparable<int>, IComparable<BigInteger>,
        IEquatable<int>, IEquatable<BigInteger>
{
    public readonly static BigInteger Zero = 0;
    public readonly static BigInteger One = 1;
    public readonly static BigInteger MinusOne = -1;

    public bool IsPowerOfTwo
    {
        get
        {
            var sign = Sgn(this);
            if (bits == null)
                return (sign & (sign - 1)) == 0 && sign != 0;

            if (sign != 1)
                return false;

            int iu = Length - 1;
            if ((bits[iu] & (bits[iu] - 1)) != 0)
                return false;
            while (--iu >= 0)
            {
                if (bits[iu] != 0)
                    return false;
            }
            return true;
        }
    }

    public bool IsZero => this == Zero;

    public bool IsOne => this == One;

    public bool IsEven { get { return bits == null ? (Sign & 1) == 0 : (bits[0] & 1) == 0; } }

    private readonly uint[] bits;
    private readonly int length;
    private readonly bool sign;

    public int Sign => Sgn(this);

    public int Length => length;

    internal BigInteger(uint[] bits, int length, bool sign)
    {
        // no leading zeros; and zero has no sign
        length = Bits.Length(bits, length);
        sign = sign && length > 0;

        this.bits = bits;
        this.length = length;
        this.sign = sign;
    }

#pragma warning disable CA1707 // Identifiers should not contain underscores
#pragma warning disable CA2225 // Operator overloads have named alternates

    /// <summary>
    /// Defines an implicit conversion of a signed 32-bit integer to an
    /// BigInteger.
    /// </summary>
    /// <param name="value">The value to convert to an BigInteger.</param>
    /// <returns>
    /// An BigInteger that contains the value of the value parameter.
    /// </returns>
    public static implicit operator BigInteger(int value)
    {
        var num = Bits.Abs(value);
        var bits = new uint[] { num };

        return new BigInteger(bits, 1, value < 0);
    }

    /// <summary>
    /// Defines an implicit conversion of a unsigned 32-bit integer to an
    /// BigInteger.
    /// </summary>
    /// <param name="value">The value to convert to an BigInteger.</param>
    /// <returns>
    /// An BigInteger that contains the value of the value parameter.
    /// </returns>
    public static implicit operator BigInteger(uint value)
    {
        var num = value;
        var bits = new uint[] { num };

        return new BigInteger(bits, 1, false);
    }

    /// <summary>
    /// Defines an implicit conversion of a signed 64-bit integer to an
    /// BigInteger.
    /// </summary>
    /// <param name="value">The value to convert to an BigInteger.</param>
    /// <returns>
    /// An BigInteger that contains the value of the value parameter.
    /// </returns>
    public static implicit operator BigInteger(long value)
    {
        var num = Bits.Abs(value);
        var bits = new uint[] { (uint)num, (uint)(num >> 32) };

        return new BigInteger(bits, 2, value < 0);
    }

    /// <summary>
    /// Defines an implicit conversion of a unsigned 64-bit integer to an
    /// BigInteger.
    /// </summary>
    /// <param name="value">The value to convert to an BigInteger.</param>
    /// <returns>
    /// An BigInteger that contains the value of the value parameter.
    /// </returns>
    public static implicit operator BigInteger(ulong value)
    {
        var num = value;
        var bits = new uint[] { (uint)num, (uint)(num >> 32) };

        return new BigInteger(bits, 2, false);
    }

    /// <summary>
    /// Defines an explicit conversion of an BigInteger to a signed 32-bit
    /// integer.
    /// </summary>
    /// <param name="value">
    /// The value to convert to a signed 32-bit integer.
    /// </param>
    /// <returns>
    /// A signed 32-bit integer that contains the value of the value
    /// parameter.
    /// </returns>
    public static explicit operator int(BigInteger value)
    {
        var num = value.length != 0
            ? value.bits[0] : 0;
        if (num > 0x80000000)
            throw new OverflowException();
        if (num == 0x80000000 && !value.sign)
            throw new OverflowException();
        if (value.sign)
            num = ~num + 1;
        return (int)num;
    }

    /// <summary>
    /// Defines an explicit conversion of an BigInteger to a signed 64-bit
    /// integer.
    /// </summary>
    /// <param name="value">
    /// The value to convert to a signed 64-bit integer.
    /// </param>
    /// <returns>
    /// A signed 64-bit integer that contains the value of the value
    /// parameter.
    /// </returns>
    public static explicit operator long(BigInteger value)
    {
        var num = value.length != 0
            ? (ulong)value.bits[0] : 0;
        if (value.length > 1)
            num |= (ulong)value.bits[1] << 32;
        if (num > 0x8000000000000000)
            throw new OverflowException();
        if (num == 0x8000000000000000 && !value.sign)
            throw new OverflowException();
        if (value.sign)
            num = ~num + 1;
        return (long)num;
    }

#pragma warning restore CA1707 // Identifiers should not contain underscores
#pragma warning restore CA2225 // Operator overloads have named alternates

    /// <summary>
    /// Performs a bitwise AND operation on an BigInteger and a signed 32-bit
    /// integer.
    /// </summary>
    /// <param name="right">The second value.</param>
    /// <returns>The result of the bitwise AND operation.</returns>
    public BigInteger BitwiseAnd(int right)
    {
        var leftBits = sign
            ? Bits.TwosComplement(bits, length) : bits;
        var rightPad = right < 0
            ? 0xFFFFFFFF : 0x00000000;

        var result = Calc.And(leftBits, length, (uint)right, rightPad);
        var signed = sign & (right < 0);
        if (signed)
            result = Bits.TwosComplement(result, result.Length);

        return new BigInteger(result, result.Length, signed);
    }

    /// <summary>
    /// Performs a bitwise AND operation on an BigInteger and a signed 32-bit
    /// integer.
    /// </summary>
    /// <param name="left">The first value.</param>
    /// <param name="right">The second value.</param>
    /// <returns>The result of the bitwise AND operation.</returns>
    public static BigInteger operator &(BigInteger left, int right)
    {
        return left.BitwiseAnd(right);
    }

    /// <summary>
    /// Performs a bitwise AND operation on two Integers.
    /// </summary>
    /// <param name="right">The second value.</param>
    /// <returns>The result of the bitwise AND operation.</returns>
    public BigInteger BitwiseAnd(BigInteger right)
    {
        var leftBits = sign
            ? Bits.TwosComplement(bits, length) : bits;
        var leftPad = sign
            ? 0xFFFFFFFF : 0x00000000;
        var rightBits = right.sign
            ? Bits.TwosComplement(right.bits, right.length) : right.bits;
        var rightPad = right.sign
            ? 0xFFFFFFFF : 0x00000000;

        var result = length < right.length
            ? Calc.And(rightBits, right.length, leftBits, length, leftPad)
            : Calc.And(leftBits, length, rightBits, right.length, rightPad);
        var signed = sign & right.sign;
        if (signed)
            result = Bits.TwosComplement(result, result.Length);

        return new BigInteger(result, result.Length, signed);
    }

    /// <summary>
    /// Performs a bitwise AND operation on two Integers.
    /// </summary>
    /// <param name="left">The first value.</param>
    /// <param name="right">The second value.</param>
    /// <returns>The result of the bitwise AND operation.</returns>
    public static BigInteger operator &(BigInteger left, BigInteger right)
    {
        return left.BitwiseAnd(right);
    }

    /// <summary>
    /// Performs a bitwise OR operation on an BigInteger and a signed 32-bit
    /// integer.
    /// </summary>
    /// <param name="right">The second value.</param>
    /// <returns>The result of the bitwise OR operation.</returns>
    public BigInteger BitwiseOr(int right)
    {
        var leftBits = sign
            ? Bits.TwosComplement(bits, length) : bits;
        var rightPad = right < 0
            ? 0xFFFFFFFF : 0x00000000;

        var result = Calc.Or(leftBits, length, (uint)right, rightPad);
        var signed = sign | (right < 0);
        if (signed)
            result = Bits.TwosComplement(result, result.Length);

        return new BigInteger(result, result.Length, signed);
    }

    /// <summary>
    /// Performs a bitwise OR operation on an BigInteger and a signed 32-bit
    /// integer.
    /// </summary>
    /// <param name="left">The first value.</param>
    /// <param name="right">The second value.</param>
    /// <returns>The result of the bitwise OR operation.</returns>
    public static BigInteger operator |(BigInteger left, int right)
    {
        return left.BitwiseOr(right);
    }

    /// <summary>
    /// Performs a bitwise OR operation on two Integers.
    /// </summary>
    /// <param name="right">The second value.</param>
    /// <returns>The result of the bitwise OR operation.</returns>
    public BigInteger BitwiseOr(BigInteger right)
    {
        var leftBits = sign
            ? Bits.TwosComplement(bits, length) : bits;
        var leftPad = sign
            ? 0xFFFFFFFF : 0x00000000;
        var rightBits = right.sign
            ? Bits.TwosComplement(right.bits, right.length) : right.bits;
        var rightPad = right.sign
            ? 0xFFFFFFFF : 0x00000000;

        var result = length < right.length
            ? Calc.Or(rightBits, right.length, leftBits, length, leftPad)
            : Calc.Or(leftBits, length, rightBits, right.length, rightPad);
        var signed = sign | right.sign;
        if (signed)
            result = Bits.TwosComplement(result, result.Length);

        return new BigInteger(result, result.Length, signed);
    }

    /// <summary>
    /// Performs a bitwise OR operation on two Integers.
    /// </summary>
    /// <param name="left">The first value.</param>
    /// <param name="right">The second value.</param>
    /// <returns>The result of the bitwise OR operation.</returns>
    public static BigInteger operator |(BigInteger left, BigInteger right)
    {
        return left.BitwiseOr(right);
    }

    /// <summary>
    /// Performs a bitwise XOR operation on an BigInteger and a signed 32-bit
    /// integer.
    /// </summary>
    /// <param name="right">The second value.</param>
    /// <returns>The result of the bitwise XOR operation.</returns>
    public BigInteger Xor(int right)
    {
        var leftBits = sign
            ? Bits.TwosComplement(bits, length) : bits;
        var rightPad = right < 0
            ? 0xFFFFFFFF : 0x00000000;

        var result = Calc.Xor(leftBits, length, (uint)right, rightPad);
        var signed = sign ^ (right < 0);
        if (signed)
            result = Bits.TwosComplement(result, result.Length);

        return new BigInteger(result, result.Length, signed);
    }

    /// <summary>
    /// Performs a bitwise XOR operation on an BigInteger and a signed 32-bit
    /// integer.
    /// </summary>
    /// <param name="left">The first value.</param>
    /// <param name="right">The second value.</param>
    /// <returns>The result of the bitwise XOR operation.</returns>
    public static BigInteger operator ^(BigInteger left, int right)
    {
        return left.Xor(right);
    }

    /// <summary>
    /// Performs a bitwise XOR operation on two Integers.
    /// </summary>
    /// <param name="right">The second value.</param>
    /// <returns>The result of the bitwise XOR operation.</returns>
    public BigInteger Xor(BigInteger right)
    {
        var leftBits = sign
            ? Bits.TwosComplement(bits, length) : bits;
        var leftPad = sign
            ? 0xFFFFFFFF : 0x00000000;
        var rightBits = right.sign
            ? Bits.TwosComplement(right.bits, right.length) : right.bits;
        var rightPad = right.sign
            ? 0xFFFFFFFF : 0x00000000;

        var result = length < right.length
            ? Calc.Xor(rightBits, right.length, leftBits, length, leftPad)
            : Calc.Xor(leftBits, length, rightBits, right.length, rightPad);
        var signed = sign ^ right.sign;
        if (signed)
            result = Bits.TwosComplement(result, result.Length);

        return new BigInteger(result, result.Length, signed);
    }

    /// <summary>
    /// Performs a bitwise XOR operation on two Integers.
    /// </summary>
    /// <param name="left">The first value.</param>
    /// <param name="right">The second value.</param>
    /// <returns>The result of the bitwise XOR operation.</returns>
    public static BigInteger operator ^(BigInteger left, BigInteger right)
    {
        return left.Xor(right);
    }

    /// <summary>
    /// Returns the bitwise one's complement of an BigInteger.
    /// </summary>
    /// <returns>The bitwise one's complement.</returns>
    public BigInteger OnesComplement()
    {
        var selfBits = sign
            ? Bits.TwosComplement(bits, length) : bits;

        var result = Bits.OnesComplement(selfBits, length);
        var signed = !sign;
        if (signed)
            result = Bits.TwosComplement(result, result.Length);

        return new BigInteger(result, result.Length, signed);
    }

    /// <summary>
    /// /// Returns the bitwise one's complement of an BigInteger.
    /// </summary>
    /// <param name="value">An BigInteger.</param>
    /// <returns>The bitwise one's complement.</returns>
    public static BigInteger operator ~(BigInteger value)
    {
        return value.OnesComplement();
    }

    /// <summary>
    /// Shifts an BigInteger a specified number of bits to the right.
    /// </summary>
    /// <param name="shift">
    /// The number of bits to shift to the right.
    /// </param>
    /// <returns>
    /// An BigInteger that has been shifted to the right by the specified
    /// number of bits.
    /// </returns>
    public BigInteger RightShift(int shift)
    {
        var selfBits = sign
            ? Bits.TwosComplement(bits, length) : bits;
        var shiftPad = sign
            ? 0xFFFFFFFF : 0x00000000;

        var result = Calc.Shift(selfBits, length, -shift, shiftPad);
        if (sign)
            result = Bits.TwosComplement(result, result.Length);

        return new BigInteger(result, result.Length, sign);
    }

    /// <summary>
    /// Shifts an BigInteger a specified number of bits to the right.
    /// </summary>
    /// <param name="value">The value whose bits are to be shifted.</param>
    /// <param name="shift">
    /// The number of bits to shift to the right.
    /// </param>
    /// <returns>
    /// An BigInteger that has been shifted to the right by the specified
    /// number of bits.
    /// </returns>
    public static BigInteger operator >>(BigInteger value, int shift)
    {
        return value.RightShift(shift);
    }

    /// <summary>
    /// Shifts an BigInteger a specified number of bits to the left.
    /// </summary>
    /// <param name="shift">
    /// The number of bits to shift to the left.
    /// </param>
    /// <returns>
    /// An BigInteger that has been shifted to the left by the specified
    /// number of bits.
    /// </returns>
    public BigInteger LeftShift(int shift)
    {
        var selfBits = sign
            ? Bits.TwosComplement(bits, length) : bits;
        var shiftPad = sign
            ? 0xFFFFFFFF : 0x00000000;

        var result = Calc.Shift(selfBits, length, shift, shiftPad);
        if (sign)
            result = Bits.TwosComplement(result, result.Length);

        return new BigInteger(result, result.Length, sign);
    }

    /// <summary>
    /// Shifts an BigInteger a specified number of bits to the left.
    /// </summary>
    /// <param name="value">The value whose bits are to be shifted.</param>
    /// <param name="shift">
    /// The number of bits to shift to the left.
    /// </param>
    /// <returns>
    /// An BigInteger that has been shifted to the left by the specified
    /// number of bits.
    /// </returns>
    public static BigInteger operator <<(BigInteger value, int shift)
    {
        return value.LeftShift(shift);
    }

    /// <summary>
    /// Returns a value that indicates whether an BigInteger and a specified
    /// object have the same value.
    /// </summary>
    /// <param name="obj">The object to compare.</param>
    /// <returns>
    /// true, if the obj parameter is an BigInteger or a type capable of
    /// conversion to an BigInteger, and its value is equal to the BigInteger;
    /// otherwise, false.
    /// </returns>
    public override bool Equals(object obj)
    {
        if (obj == null)
            return false;
        if (obj is int)
            return Equals((int)obj);
        if (obj is BigInteger)
            return Equals((BigInteger)obj);
        return false;
    }

    /// <summary>
    /// Returns a value that indicates whether an BigInteger and a signed
    /// 32-bit integer have the same value.
    /// </summary>
    /// <param name="other">The signed 32-bit integer to compare.</param>
    /// <returns>
    /// true, if the signed 32-bit integer and the BigInteger have the same
    /// value; otherwise, false.
    /// </returns>
    public bool Equals(int other)
    {
        return CompareTo(other) == 0;
    }

    /// <summary>
    /// Returns a value that indicates whether an BigInteger and a specified
    /// BigInteger have the same value.
    /// </summary>
    /// <param name="other">The BigInteger to compare.</param>
    /// <returns>
    /// true, if the BigInteger and other have the same value; otherwise,
    /// false.
    /// </returns>
    public bool Equals(BigInteger other)
    {
        return CompareTo(other) == 0;
    }

    /// <summary>
    /// Returns a value that indicates whether an BigInteger and a signed
    /// 32-bit integer have the same value.
    /// </summary>
    /// <param name="left">The first value to compare.</param>
    /// <param name="right">The second value to compare.</param>
    /// <returns>
    /// true, if the left and right parameters have the same value;
    /// otherwise, false.
    /// </returns>
    public static bool operator ==(BigInteger left, int right)
    {
        return left.CompareTo(right) == 0;
    }

    /// <summary>
    /// Returns a value that indicates whether an BigInteger and another
    /// BigInteger have the same value.
    /// </summary>
    /// <param name="left">The first value to compare.</param>
    /// <param name="right">The second value to compare.</param>
    /// <returns>
    /// true, if the left and right parameters have the same value;
    /// otherwise, false.
    /// </returns>
    public static bool operator ==(BigInteger left, BigInteger right)
    {
        return left.CompareTo(right) == 0;
    }

    /// <summary>
    /// Returns a value that indicates whether an BigInteger and a signed
    /// 32-bit integer have different values.
    /// </summary>
    /// <param name="left">The first value to compare.</param>
    /// <param name="right">The second value to compare.</param>
    /// <returns>
    /// true, if the left and right parameters have different values;
    /// otherwise, false.
    /// </returns>
    public static bool operator !=(BigInteger left, int right)
    {
        return left.CompareTo(right) != 0;
    }

    /// <summary>
    /// Returns a value that indicates whether an BigInteger and another
    /// BigInteger have different values.
    /// </summary>
    /// <param name="left">The first value to compare.</param>
    /// <param name="right">The second value to compare.</param>
    /// <returns>
    /// true, if the left and right parameters have different values;
    /// otherwise, false.
    /// </returns>
    public static bool operator !=(BigInteger left, BigInteger right)
    {
        return left.CompareTo(right) != 0;
    }

    /// <summary>
    /// Compares the BigInteger to a specified object and returns an integer
    /// that indicates whether the value of the BigInteger is less than, equal
    /// to, or greater than the value of the specified object.
    /// </summary>
    /// <param name="obj">The object to compare.</param>
    /// <returns>
    /// An integer that indicates the relationship of the BigInteger to the
    /// obj parameter.
    /// </returns>
    public int CompareTo(object obj)
    {
        if (obj == null)
            throw new ArgumentNullException(nameof(obj));
        if (obj is int)
            return CompareTo((int)obj);
        if (obj is BigInteger)
            return CompareTo((BigInteger)obj);
        throw new ArgumentOutOfRangeException(nameof(obj));
    }

    /// <summary>
    /// Compares the BigInteger to a signed 32-bit integer and returns an
    /// integer that indicates whether the value of the BigInteger is less
    /// than, equal to, or greater than the value of the signed 32-bit
    /// integer.
    /// </summary>
    /// <param name="other">The signed 32-bit integer to compare.</param>
    /// <returns>
    /// An integer that indicates the relationship of the BigInteger to the
    /// other parameter.
    /// </returns>
    public int CompareTo(int other)
    {
        if (sign && other >= 0)
            return -1;
        if (!sign && other < 0)
            return 1;

        var result = Bits.Compare(bits, length, Bits.Abs(other));
        if (sign)
            result = -result;

        return result;
    }

    /// <summary>
    /// Compares the BigInteger to another BigInteger and returns an integer that
    /// indicates whether the value of the BigInteger is less than, equal to,
    /// or greater than the value of the BigInteger.
    /// </summary>
    /// <param name="other">The BigInteger to compare.</param>
    /// <returns>
    /// An integer that indicates the relationship of the BigInteger to the
    /// other parameter.
    /// </returns>
    public int CompareTo(BigInteger other)
    {
        if (sign && !other.sign)
            return -1;
        if (!sign && other.sign)
            return 1;

        var result = Bits.Compare(bits, length, other.bits, other.length);
        if (sign)
            result = -result;

        return result;
    }

    /// <summary>
    /// Returns a value that indicates whether an BigInteger is less than a
    /// signed 32-bit integer.
    /// </summary>
    /// <param name="left">The first value to compare.</param>
    /// <param name="right">The second value to compare.</param>
    /// <returns>
    /// true, if left is less than right; otherwise, false.
    /// </returns>
    public static bool operator <(BigInteger left, int right)
    {
        return left.CompareTo(right) < 0;
    }

    /// <summary>
    /// Returns a value that indicates whether an BigInteger is less than
    /// another BigInteger.
    /// </summary>
    /// <param name="left">The first value to compare.</param>
    /// <param name="right">The second value to compare.</param>
    /// <returns>
    /// true, if left is less than right; otherwise, false.
    /// </returns>
    public static bool operator <(BigInteger left, BigInteger right)
    {
        return left.CompareTo(right) < 0;
    }

    /// <summary>
    /// Returns a value that indicates whether an BigInteger is less than or
    /// equal to a signed 32-bit integer.
    /// </summary>
    /// <param name="left">The first value to compare.</param>
    /// <param name="right">The second value to compare.</param>
    /// <returns>
    /// true, if left is less than or equal to right; otherwise, false.
    /// </returns>
    public static bool operator <=(BigInteger left, int right)
    {
        return left.CompareTo(right) <= 0;
    }

    /// <summary>
    /// Returns a value that indicates whether an BigInteger is less than or
    /// equal to another BigInteger.
    /// </summary>
    /// <param name="left">The first value to compare.</param>
    /// <param name="right">The second value to compare.</param>
    /// <returns>
    /// true, if left is less than or equal to right; otherwise, false.
    /// </returns>
    public static bool operator <=(BigInteger left, BigInteger right)
    {
        return left.CompareTo(right) <= 0;
    }

    /// <summary>
    /// Returns a value that indicates whether an BigInteger is greater than a
    /// signed 32-bit integer.
    /// </summary>
    /// <param name="left">The first value to compare.</param>
    /// <param name="right">The second value to compare.</param>
    /// <returns>
    /// true, if left is greater than right; otherwise, false.
    /// </returns>
    public static bool operator >(BigInteger left, int right)
    {
        return left.CompareTo(right) > 0;
    }

    /// <summary>
    /// Returns a value that indicates whether an BigInteger is greater than
    /// another BigInteger.
    /// </summary>
    /// <param name="left">The first value to compare.</param>
    /// <param name="right">The second value to compare.</param>
    /// <returns>
    /// true, if left is greater than right; otherwise, false.
    /// </returns>
    public static bool operator >(BigInteger left, BigInteger right)
    {
        return left.CompareTo(right) > 0;
    }

    /// <summary>
    /// Returns a value that indicates whether an BigInteger is greater than
    /// or equal to a signed 32-bit integer.
    /// </summary>
    /// <param name="left">The first value to compare.</param>
    /// <param name="right">The second value to compare.</param>
    /// <returns>
    /// true, if left is greater than or equal to right; otherwise, false.
    /// </returns>
    public static bool operator >=(BigInteger left, int right)
    {
        return left.CompareTo(right) >= 0;
    }

    /// <summary>
    /// Returns a value that indicates whether an BigInteger is greater than
    /// or equal to another BigInteger.
    /// </summary>
    /// <param name="left">The first value to compare.</param>
    /// <param name="right">The second value to compare.</param>
    /// <returns>
    /// true, if left is greater than or equal to right; otherwise, false.
    /// </returns>
    public static bool operator >=(BigInteger left, BigInteger right)
    {
        return left.CompareTo(right) >= 0;
    }

    /// <summary>
    /// Returns the value of an BigInteger.
    /// </summary>
    /// <returns>The value of the BigInteger.</returns>
    public BigInteger Plus()
    {
        return new BigInteger(bits, length, sign);
    }

    /// <summary>
    /// Returns the value of an BigInteger.
    /// </summary>
    /// <param name="value">An BigInteger.</param>
    /// <returns>The value of the value parameter.</returns>
    public static BigInteger operator +(BigInteger value)
    {
        return value.Plus();
    }

    /// <summary>
    /// Increments an BigInteger by 1.
    /// </summary>
    /// <returns>An BigInteger that has been incremented by 1.</returns>
    public BigInteger Increment()
    {
        return Add(1);
    }

    /// <summary>
    /// Increments an BigInteger by 1.
    /// </summary>
    /// <param name="value">The value to increment.</param>
    /// <returns>An BigInteger that has been incremented by 1.</returns>
    public static BigInteger operator ++(BigInteger value)
    {
        return value.Increment();
    }

    /// <summary>
    /// Adds the values of an BigInteger and a signed 32-bit integer.
    /// </summary>
    /// <param name="right">The value to add.</param>
    /// <returns>The sum of the BigInteger and right.</returns>
    public BigInteger Add(int right)
    {
        if (length == 0)
            return ((BigInteger)right).Plus();
        if (sign != (right < 0))
            return Negate().Subtract(right).Negate();

        var result = Calc.Add(bits, length, Bits.Abs(right));
        return new BigInteger(result, result.Length, sign);
    }

    /// <summary>
    /// Adds the values of an BigInteger and a signed 32-bit integer.
    /// </summary>
    /// <param name="left">The first value to add.</param>
    /// <param name="right">The second value to add.</param>
    /// <returns>The sum of left and right.</returns>
    public static BigInteger operator +(BigInteger left, int right)
    {
        return left.Add(right);
    }

    /// <summary>
    /// Adds the values of an BigInteger and another BigInteger.
    /// </summary>
    /// <param name="right">The value to add.</param>
    /// <returns>The sum of the BigInteger and right.</returns>
    public BigInteger Add(BigInteger right)
    {
        if (right.length == 0)
            return Plus();
        if (sign != right.sign)
            return Subtract(right.Negate());

        var result = length < right.length
            ? Calc.Add(right.bits, right.length, bits, length)
            : Calc.Add(bits, length, right.bits, right.length);

        return new BigInteger(result, result.Length, sign);
    }

    /// <summary>
    /// Adds the values of an BigInteger and another BigInteger.
    /// </summary>
    /// <param name="left">The first value to add.</param>
    /// <param name="right">The second value to add.</param>
    /// <returns>The sum of left and right.</returns>
    public static BigInteger operator +(BigInteger left, BigInteger right)
    {
        return left.Add(right);
    }

    /// <summary>
    /// Negates an BigInteger value.
    /// </summary>
    /// <returns>An BigInteger that has been negated.</returns>
    public BigInteger Negate()
    {
        return new BigInteger(bits, length, !sign);
    }

    /// <summary>
    /// Negates an BigInteger value.
    /// </summary>
    /// <param name="value">The value to negate.</param>
    /// <returns>An BigInteger that has been negated.</returns>
    public static BigInteger operator -(BigInteger value)
    {
        return value.Negate();
    }

    /// <summary>
    /// Decrements an BigInteger by 1.
    /// </summary>
    /// <returns>An BigInteger that has been decremented by 1.</returns>
    public BigInteger Decrement()
    {
        return Subtract(1);
    }

    /// <summary>
    /// Decrements an BigInteger by 1.
    /// </summary>
    /// <param name="value">The value to decrement.</param>
    /// <returns>An BigInteger that has been decremented by 1.</returns>
    public static BigInteger operator --(BigInteger value)
    {
        return value.Decrement();
    }

    /// <summary>
    /// Subtracts a signed 32-bit integer from an BigInteger.
    /// </summary>
    /// <param name="right">The value to subtract.</param>
    /// <returns>
    /// The result of subtracting right from the BigInteger.
    /// </returns>
    public BigInteger Subtract(int right)
    {
        if (length == 0)
            return ((BigInteger)right).Negate();
        if (sign != (right < 0))
            return Negate().Add(right).Negate();
        if (length == 1 && bits[0] < Bits.Abs(right))
            return new BigInteger(new uint[] { Bits.Abs(right)
                    - bits[0] }, 1, !sign);

        var result = Calc.Subtract(bits, length, Bits.Abs(right));
        return new BigInteger(result, result.Length, sign);
    }

    /// <summary>
    /// Subtracts a signed 32-bit integer from an BigInteger.
    /// </summary>
    /// <param name="left">The value to subtract from.</param>
    /// <param name="right">The value to subtract.</param>
    /// <returns>The result of subtracting right from left.</returns>
    public static BigInteger operator -(BigInteger left, int right)
    {
        return left.Subtract(right);
    }

    /// <summary>
    /// Subtracts an BigInteger from another BigInteger.
    /// </summary>
    /// <param name="right">The value to subtract.</param>
    /// <returns>
    /// The result of subtracting right from the BigInteger.
    /// </returns>
    public BigInteger Subtract(BigInteger right)
    {
        if (right.length == 0)
            return Plus();
        if (sign != right.sign)
            return Add(right.Negate());

        var diff = Bits.Compare(bits, length, right.bits, right.length);
        var result = diff < 0
            ? Calc.Subtract(right.bits, right.length, bits, length)
            : Calc.Subtract(bits, length, right.bits, right.length);

        return new BigInteger(result, result.Length, diff < 0 ? !sign : sign);
    }

    /// <summary>
    /// Subtracts an BigInteger from another BigInteger.
    /// </summary>
    /// <param name="left">The value to subtract from.</param>
    /// <param name="right">The value to subtract.</param>
    /// <returns>The result of subtracting right from left.</returns>
    public static BigInteger operator -(BigInteger left, BigInteger right)
    {
        return left.Subtract(right);
    }

    /// <summary>
    /// Multiplies the values of an BigInteger and a signed 32-bit integer.
    /// </summary>
    /// <param name="right">The value to multiply with.</param>
    /// <returns>The product of the BigInteger and right.</returns>
    public BigInteger Multiply(int right)
    {
        var result = Calc.Multiply(bits, length, Bits.Abs(right));
        return new BigInteger(result, result.Length, sign ^ (right < 0));
    }

    /// <summary>
    /// Multiplies the values of an BigInteger and a signed 32-bit integer.
    /// </summary>
    /// <param name="left">The first value to multiply.</param>
    /// <param name="right">The second value to multiply.</param>
    /// <returns>The product of left and right.</returns>
    public static BigInteger operator *(BigInteger left, int right)
    {
        return left.Multiply(right);
    }

    /// <summary>
    /// Multiplies the values of an BigInteger and another BigInteger.
    /// </summary>
    /// <param name="right">The value to multiply with.</param>
    /// <returns>The product of the BigInteger and right.</returns>
    public BigInteger Multiply(BigInteger right)
    {
        var result = bits == right.bits
            ? Calc.Square(bits, length)
            : Calc.Multiply(bits, length, right.bits, right.length);
        return new BigInteger(result, result.Length, sign ^ right.sign);
    }

    /// <summary>
    /// Multiplies the values of an BigInteger and another BigInteger.
    /// </summary>
    /// <param name="left">The first value to multiply.</param>
    /// <param name="right">The second value to multiply.</param>
    /// <returns>The product of left and right.</returns>
    public static BigInteger operator *(BigInteger left, BigInteger right)
    {
        return left.Multiply(right);
    }

    /// <summary>
    /// Divides an BigInteger by a signed 32-bit integer.
    /// </summary>
    /// <param name="right">The value to divide by.</param>
    /// <returns>The integral result of the division.</returns>
    public BigInteger Divide(int right)
    {
        if (right == 0)
            throw new DivideByZeroException();

        var remainder = default(uint);
        var result = Calc.Divide(bits, length, Bits.Abs(right),
            out remainder);
        return new BigInteger(result, result.Length, sign ^ (right < 0));
    }

    /// <summary>
    /// Divides an BigInteger by a signed 32-bit integer.
    /// </summary>
    /// <param name="left">The value to be divided.</param>
    /// <param name="right">The value to divide by.</param>
    /// <returns>The integral result of the division.</returns>
    public static BigInteger operator /(BigInteger left, int right)
    {
        return left.Divide(right);
    }

    /// <summary>
    /// Divides an BigInteger by another BigInteger.
    /// </summary>
    /// <param name="right">The value to divide by.</param>
    /// <returns>The integral result of the division.</returns>
    public BigInteger Divide(BigInteger right)
    {
        if (right.length == 0)
            throw new DivideByZeroException();
        if (length < right.length)
            return new BigInteger(null, 0, false);

        var remainder = default(uint[]);
        var result = Calc.Divide(bits, length, right.bits, right.length,
            out remainder);
        return new BigInteger(result, result.Length, sign ^ right.sign);
    }

    /// <summary>
    /// Divides an BigInteger by another BigInteger.
    /// </summary>
    /// <param name="left">The value to be divided.</param>
    /// <param name="right">The value to divide by.</param>
    /// <returns>The integral result of the division.</returns>
    public static BigInteger operator /(BigInteger left, BigInteger right)
    {
        return left.Divide(right);
    }

    /// <summary>
    /// Returns the remainder that results from dividing an BigInteger by a
    /// signed 32-bit integer.
    /// </summary>
    /// <param name="right">The value to divide by.</param>
    /// <returns>The remainder that results from the division.</returns>
    public int Remainder(int right)
    {
        if (right == 0)
            throw new DivideByZeroException();

        var result = Calc.Remainder(bits, length, Bits.Abs(right));
        if (sign)
            result = ~result + 1;

        return (int)result;
    }

    /// <summary>
    /// Returns the remainder that results from dividing an BigInteger by a
    /// signed 32-bit integer.
    /// </summary>
    /// <param name="left">The value to be divided.</param>
    /// <param name="right">The value to divide by.</param>
    /// <returns>The remainder that results from the division.</returns>
    public static int operator %(BigInteger left, int right)
    {
        return left.Remainder(right);
    }

    /// <summary>
    /// Returns the remainder that results from dividing an BigInteger by
    /// another BigInteger.
    /// </summary>
    /// <param name="right">The value to divide by.</param>
    /// <returns>The remainder that results from the division.</returns>
    public BigInteger Remainder(BigInteger right)
    {
        if (right.length == 0)
            throw new DivideByZeroException();
        if (length < right.length)
            return new BigInteger(bits, length, sign);

        var remainder = default(uint[]);
        var result = Calc.Divide(bits, length, right.bits, right.length,
            out remainder);
        return new BigInteger(remainder, remainder.Length, sign);
    }

    /// <summary>
    /// Returns the remainder that results from dividing an BigInteger by
    /// another BigInteger.
    /// </summary>
    /// <param name="left">The value to be divided.</param>
    /// <param name="right">The value to divide by.</param>
    /// <returns>The remainder that results from the division.</returns>
    public static BigInteger operator %(BigInteger left, BigInteger right)
    {
        return left.Remainder(right);
    }

    /// <summary>
    /// Converts an BigInteger to its equivalent string representation.
    /// </summary>
    /// <returns>The string representation of the BigInteger.</returns>
    public override string ToString()
    {
        return this.ToDecimalString();
    }

    /// <summary>
    /// Converts the string representation of a number to its BigInteger
    /// equivalent.
    /// </summary>
    /// <param name="value">
    /// A string that contains the number to convert.
    /// </param>
    /// <returns>
    /// A value that is equivalent to the number specified in the value
    /// parameter.
    /// </returns>
    public static BigInteger Parse(string value)
    {
        return FromDecimalString(value);
    }

    /// <summary>
    /// Returns the hash code for an BigInteger.
    /// </summary>
    /// <returns>A signed 32-bit integer hash code.</returns>
    public override int GetHashCode()
    {
        var hash = 0U;
        for (var i = 0U; i < length; i++)
        {
            hash += i;
            hash *= 2143;
            hash ^= bits[i];
        }
        if (sign)
            hash = ~hash + 1;
        return (int)hash;
    }

    #region Methods

    public static int Compare(BigInteger left, BigInteger right)
    {
        return left.CompareTo(right);
    }

    /// <summary>
    /// 取得最大公约数
    /// </summary>
    public static BigInteger GreatestCommonDivisor(BigInteger left, BigInteger right)
    {
        if (Sgn(left) == 0) return BigInteger.Abs(right);
        if (Sgn(right) == 0) return BigInteger.Abs(left);

        return Gcd(left, right);
    }

    public static BigInteger Negate(BigInteger value)
    {
        return value.Negate();
    }

    public static BigInteger Remainder(BigInteger left, BigInteger right)
    {
        return left.Remainder(right);
    }

    /// <summary>
    /// Gets a number that indicates the sign of an BigInteger.
    /// </summary>
    /// <param name="value">The BigInteger.</param>
    /// <returns>A number that indicates the sign of the BigInteger.</returns>
    public static int Sgn(BigInteger value)
    {
        return value.CompareTo(0);
    }

    /// <summary>
    /// Gets the absolute value of an BigInteger.
    /// </summary>
    /// <param name="value">The BigInteger.</param>
    /// <returns>The absolute value of the BigInteger.</returns>
    public static BigInteger Abs(BigInteger value)
    {
        return value < 0 ? -value : value;
    }

    /// <summary>
    /// Returns the smaller of two Integers.
    /// </summary>
    /// <param name="left">The first value to compare.</param>
    /// <param name="right">The second value to compare.</param>
    /// <returns>
    /// The left or right parameter, whichever is smaller.
    /// </returns>
    public static BigInteger Min(BigInteger left, BigInteger right)
    {
        return left < right ? left : right;
    }

    /// <summary>
    /// Returns the larger of two Integers.
    /// </summary>
    /// <param name="left">The first value to compare.</param>
    /// <param name="right">The second value to compare.</param>
    /// <returns>
    /// The left or right parameter, whichever is larger.
    /// </returns>
    public static BigInteger Max(BigInteger left, BigInteger right)
    {
        return left < right ? right : left;
    }

    /// <summary>
    /// Finds the greatest common divisor of two Integers.
    /// </summary>
    /// <param name="left">The first value.</param>
    /// <param name="right">The second value.</param>
    /// <returns>The greatest common divisor of left and right.</returns>
    public static BigInteger Gcd(BigInteger left, BigInteger right)
    {
        var a = Abs(left);
        var b = Abs(right);

        if (a < b)
            return Lehmer.Gcd(b, a);
        return Lehmer.Gcd(a, b);
    }

    /// <summary>
    /// Finds the least common multiple of two Integers.
    /// </summary>
    /// <param name="left">The first value.</param>
    /// <param name="right">The second value.</param>
    /// <returns>The least common multiple of left and right.</returns>
    public static BigInteger Lcm(BigInteger left, BigInteger right)
    {
        return Abs(left * right) / Gcd(left, right);
    }

    /// <summary>
    /// Performs modulus inversion on an BigInteger.
    /// </summary>
    /// <param name="value">The BigInteger to inverse.</param>
    /// <param name="modulus">The BigInteger by which to divide.</param>
    /// <returns>The result of inversing the BigInteger; if any.</returns>
    public static BigInteger ModInv(BigInteger value, BigInteger modulus)
    {
        if (modulus < 1)
            throw new ArgumentOutOfRangeException(nameof(modulus));

        if (Abs(value) >= modulus)
            value = value % modulus;

        if (value < 0)
        {
            value = value + modulus;
            var result = Lehmer.Inv(modulus, value);
            if (result != 0)
                result = result - modulus;
            return result;
        }

        return Lehmer.Inv(modulus, value);
    }

    /// <summary>
    /// Raises an BigInteger to the power of a specified value.
    /// </summary>
    /// <param name="value">
    /// The BigInteger to raise to the exponent power.
    /// </param>
    /// <param name="power">The exponent to raise the BigInteger by.</param>
    /// <returns>
    /// The result of raising the BigInteger to the exponent power.
    /// </returns>
    public static BigInteger Pow(BigInteger value, int power)
    {
        if (power < 0)
            throw new ArgumentOutOfRangeException(nameof(power));

        BigInteger result = 1;

        while (power != 0)
        {
            if (power % 2 == 1)
                result = value * result;
            value = value * value;
            power = power >> 1;
        }

        return result;
    }

    /// <summary>
    /// Returns the logarithm of a specified BigInteger in a specified base.
    /// </summary>
    /// <param name="value">
    /// The BigInteger whose logarithm is to be found.
    /// </param>
    /// <param name="baseValue">The base of the logarithm.</param>
    /// <returns>The result of finding the logarithm.</returns>
    public static double Log(BigInteger value, double baseValue = 2.7182818284590451)
    {
        if (value < 0)
            return double.NaN;
        if (baseValue < 0)
            return double.NaN;

#pragma warning disable RECS0018

        if (baseValue == 0)
            return value != 1 ? double.NaN : 0;
        if (double.IsPositiveInfinity(baseValue))
            return value != 1 ? double.NaN : 0;
        if (double.IsNaN(baseValue))
            return double.NaN;
        if (baseValue == 1)
            return double.NaN;

#pragma warning restore RECS0018

        if (value == 0)
            return baseValue < 1
                ? double.PositiveInfinity
                : double.NegativeInfinity;

        // extract most significant bits
        var h = (ulong)value.bits[value.length - 1];
        var m = (ulong)(value.length > 1
            ? value.bits[value.length - 2] : 0);
        var l = (ulong)(value.length > 2
            ? value.bits[value.length - 3] : 0);

        // combine with bit count (log2)
        var z = Bits.LeadingZeros((uint)h);
        var y = value.length * 32 - z - 1.0;
        var x = (h << 32 + z) | (m << z) | (l >> 32 - z);

        return (y + Math.Log(x, 2) - 63) / Math.Log(baseValue, 2);
    }

    public static Double Log10(BigInteger value)
    {
        return BigInteger.Log(value, 10);
    }

    /// <summary>
    /// Performs modulus division on an BigInteger raised to the power of
    /// another BigInteger.
    /// </summary>
    /// <param name="value">
    /// The BigInteger to raise to the exponent power.
    /// </param>
    /// <param name="power">The exponent to raise the BigInteger by.</param>
    /// <param name="modulus">The BigInteger by which to divide.</param>
    /// <returns>
    /// The result of raising the BigInteger to the exponent power.
    /// </returns>
    public static BigInteger ModPow(BigInteger value, BigInteger power,
                                 BigInteger modulus)
    {
        if (power < 0)
            throw new ArgumentOutOfRangeException(nameof(power));
        if (modulus < 1)
            throw new ArgumentOutOfRangeException(nameof(modulus));

        if (power == 0)
            return modulus == 1 ? 0 : 1;

        var barrett = Barrett.Begin(modulus);

        var v = new BigInteger[256];
        v[0] = 1;
        v[1] = barrett.Reduce(value);
        v[2] = barrett.Reduce(v[1] * v[1]);

        for (var j = 4; j < v.Length; j *= 2)
            v[j] = barrett.Reduce(v[j / 2] * v[j / 2]);
        for (var i = 3; i < v.Length; i += 2)
        {
            v[i] = barrett.Reduce(v[i - 1] * v[1]);
            for (var j = i * 2; j < v.Length; j *= 2)
                v[j] = barrett.Reduce(v[j / 2] * v[j / 2]);
        }

        var p = power.ToByteArray();

        BigInteger result = v[p[p.Length - 1]];
        for (var i = p.Length - 2; i >= 0; i--)
        {
            for (var j = 0; j < 8; j++)
                result = barrett.Reduce(result * result);
            result = barrett.Reduce(result * v[p[i]]);
        }

        return result;
    }

    /// <summary>
    /// Divides an BigInteger value by another, returns the result, and
    /// returns the remainder in an output parameter.
    /// </summary>
    /// <param name="left">The value to be divided.</param>
    /// <param name="right">The value to divide by.</param>
    /// <param name="remainder">
    /// The remainder that results from the division.
    /// </param>
    /// <returns>The integral result of the division.</returns>
    public static BigInteger DivRem(BigInteger left, BigInteger right,
                                 out BigInteger remainder)
    {
        if (right.length == 0)
            throw new DivideByZeroException();
        if (left.length < right.length)
        {
            remainder = left;
            return new BigInteger(null, 0, false);
        }

        var remain = default(uint[]);
        var result = Calc.Divide(left.bits, left.length,
            right.bits, right.length, out remain);
        remainder = new BigInteger(remain, remain.Length, left.sign);
        return new BigInteger(result, result.Length, left.sign ^ right.sign);
    }

    #endregion

    #region Convert

    /// <summary>
    /// Returns the specified BigInteger as an array of bytes.
    /// </summary>
    /// <param name="value">The number to convert.</param>
    /// <returns>An array of bytes.</returns>
    public byte[] ToByteArray()
    {
        if (this.length == 0)
            return new byte[] { 0 };

        var bits = this.length * 32
            - Bits.LeadingZeros(this.bits[this.length - 1]) + 1;
        var bytes = new byte[(bits + 7) / 8];
        for (var i = 0; i < (bits + 6) / 8; i++)
        {
            var shift = (i % 4) * 8;
            bytes[i] = (byte)(this.bits[i / 4] >> shift);
        }
        if (this.sign)
            bytes = Bits.TwosComplement(bytes, bytes.Length);

        return bytes;
    }

    /// <summary>
    /// Returns an BigInteger converted from an array of bytes.
    /// </summary>
    /// <param name="value">An array of bytes.</param>
    /// <returns>An BigInteger formed by an array of bytes.</returns>
    public static BigInteger FromByteArray(byte[] value)
    {
        if (value == null)
            throw new ArgumentNullException(nameof(value));

        var sign = value.Length != 0
            && (value[value.Length - 1] & 0x80) != 0;
        if (sign)
            value = Bits.TwosComplement(value, value.Length);
        var bits = new uint[(value.Length + 3) / 4];
        for (var i = 0; i < value.Length; i++)
        {
            var shift = (i % 4) * 8;
            bits[i / 4] |= (uint)(value[i] << shift);
        }

        return new BigInteger(bits, bits.Length, sign);
    }

    /// <summary>
    /// Returns the specified BigInteger as a hex string.
    /// </summary>
    /// <param name="value">The number to convert.</param>
    /// <returns>A hex string.</returns>
    public string ToHexString()
    {
        var bytes = ToByteArray();

        var digits = new string[bytes.Length];
        for (var i = 0; i < bytes.Length; i++)
            digits[i] = bytes[bytes.Length - i - 1].ToString("X2",
                CultureInfo.InvariantCulture);
        var trivialSign = (digits[0][0] == '0' && digits[0][1] < '8')
            || (digits[0][0] == 'F' && digits[0][1] >= '8');
        if (trivialSign)
            digits[0] = digits[0].Substring(1);

        return string.Concat(digits);
    }

    /// <summary>
    /// Returns an BigInteger converted from a hex string.
    /// </summary>
    /// <param name="value">A hex string.</param>
    /// <returns>An BigInteger formed by a hex string.</returns>
    public static BigInteger FromHexString(string value)
    {
        if (value == null)
            throw new ArgumentNullException(nameof(value));
        if (value.Length == 0)
            throw new ArgumentOutOfRangeException(nameof(value));

        var sign = value[0] >= '8' ? "F" : "0";
        if (value.Length % 2 == 1)
            value = sign + value;
        var bytes = new byte[value.Length / 2];
        for (var i = 0; i < bytes.Length; i++)
            bytes[i] = byte.Parse(value.Substring(value.Length
                - (i + 1) * 2, 2), NumberStyles.AllowHexSpecifier,
                CultureInfo.InvariantCulture);

        return FromByteArray(bytes);
    }

    /// <summary>
    /// Returns the specified BigInteger as a decimal string.
    /// </summary>
    /// <param name="value">The number to convert.</param>
    /// <returns>A decimal string.</returns>
    public string ToDecimalString()
    {
        if (this.length == 0)
            return "0";

        var result = "";
        var bits = this.bits;
        var length = this.length;
        while (length > 0)
        {
            var r = 0U;
            bits = Calc.Divide(bits, length, 1000000000U, out r);
            result = r.ToString("000000000", CultureInfo.InvariantCulture)
                + result;
            length = Bits.Length(bits, length);
        }
        result = result.TrimStart('0');
        if (this.sign)
            result = '-' + result;

        return result;
    }

    /// <summary>
    /// Returns an BigInteger converted from a decimal string.
    /// </summary>
    /// <param name="value">A decimal string.</param>
    /// <returns>An BigInteger formed by a decimal string.</returns>
    public static BigInteger FromDecimalString(string value)
    {
        if (value == null)
            throw new ArgumentNullException(nameof(value));
        if (value.Length == 0)
            throw new ArgumentOutOfRangeException(nameof(value));

        var sign = value[0] == '-';
        if (sign)
            value = value.Substring(1);
        var result = default(uint[]);
        var resultLength = 0;
        while (value.Length > 0)
        {
            var length = value.Length < 9 ? value.Length : 9;
            var chunk = value.Substring(0, length);
            result = Calc.Multiply(result, resultLength,
                (uint)Math.Pow(10, length));
            resultLength = Bits.Length(result, result.Length);
            result = Calc.Add(result, resultLength,
                uint.Parse(chunk, NumberStyles.None,
                CultureInfo.InvariantCulture));
            resultLength = Bits.Length(result, result.Length);
            value = value.Substring(length);
        }

        return new BigInteger(result, resultLength, sign);
    }

    #endregion

    #region Classes

    static class Bits
    {
        public static int Length(uint[] value, int length)
        {
            while (length > 0 && value[length - 1] == 0)
                --length;

            return length;
        }

        public static int LeadingZeros(uint value)
        {
            if (value == 0)
                return 32;

            var count = 0;
            if ((value & 0xFFFF0000) == 0)
            {
                count += 16;
                value = value << 16;
            }
            if ((value & 0xFF000000) == 0)
            {
                count += 8;
                value = value << 8;
            }
            if ((value & 0xF0000000) == 0)
            {
                count += 4;
                value = value << 4;
            }
            if ((value & 0xC0000000) == 0)
            {
                count += 2;
                value = value << 2;
            }
            if ((value & 0x80000000) == 0)
            {
                count += 1;
            }

            return count;
        }

        public static int Compare(uint[] left, int leftLength,
                                  uint right)
        {
            if (leftLength == 0)
            {
                if (right == 0)
                    return 0;
                return -1;
            }

            if (leftLength > 1)
                return 1;

            if (left[0] < right)
                return -1;
            if (left[0] > right)
                return 1;

            return 0;
        }

        public static int Compare(uint[] left, int leftLength,
                                  uint[] right, int rightLength)
        {
            if (leftLength < rightLength)
                return -1;
            if (leftLength > rightLength)
                return 1;

            for (var i = leftLength - 1; i >= 0; i--)
            {
                if (left[i] < right[i])
                    return -1;
                if (left[i] > right[i])
                    return 1;
            }

            return 0;
        }

        public static int Compare(uint[] left, int leftLength,
                                  uint[] right, int rightLength,
                                  int rightOffset)
        {
            if (leftLength < rightLength + rightOffset)
                return -1;
            if (leftLength > rightLength + rightOffset)
                return 1;

            for (var i = leftLength - 1; i >= rightOffset; i--)
            {
                if (left[i] < right[i - rightOffset])
                    return -1;
                if (left[i] > right[i - rightOffset])
                    return 1;
            }
            for (var i = rightOffset - 1; i >= 0; i--)
            {
                if (left[i] < 0)
                    return -1;
                if (left[i] > 0)
                    return 1;
            }

            return 0;
        }

        public static uint[] OnesComplement(uint[] value, int length)
        {
            if (length == 0)
                return new uint[] { 0xFFFFFFFF };

            var result = new uint[length];
            for (var i = 0; i < length; i++)
                result[i] = ~value[i];

            return result;
        }

        public static uint[] TwosComplement(uint[] value, int length)
        {
            var result = new uint[length];

            var carry = 1UL;
            for (var i = 0; i < length; i++)
            {
                var digit = (uint)~value[i] + carry;
                result[i] = (uint)digit;
                carry = digit >> 32;
            }
            if (carry != 0)
            {
                result = new uint[length + 1];
                result[length] = 1;
            }

            return result;
        }

        public static byte[] TwosComplement(byte[] value, int length)
        {
            var result = new byte[length];

            var carry = 1U;
            for (var i = 0; i < length; i++)
            {
                var digit = (byte)~value[i] + carry;
                result[i] = (byte)digit;
                carry = digit >> 8;
            }
            if (carry != 0)
            {
                result = new byte[length + 1];
                result[length] = 1;
            }

            return result;
        }

        public static ulong Abs(long value)
        {
            var mask = (ulong)(value >> 63);
            return ((ulong)value ^ mask) - mask;
        }

        public static uint Abs(int value)
        {
            var mask = (uint)(value >> 31);
            return ((uint)value ^ mask) - mask;
        }
    }

    sealed class Barrett
    {
        readonly uint[] m;
        readonly int ml;

        readonly uint[] u;
        readonly int ul;

        readonly int L;

        Barrett(BigInteger modulus, BigInteger R, BigInteger mu)
        {
            m = modulus.bits;
            ml = modulus.length;

            u = mu.bits;
            ul = mu.length;

            L = R.length;
        }

        public static Barrett Begin(BigInteger modulus)
        {
            var bits = new uint[modulus.length * 2 + 1];
            bits[bits.Length - 1] = 1;
            var R = new BigInteger(bits, bits.Length, false);

            return new Barrett(modulus, R, R / modulus);
        }

        public BigInteger Reduce(BigInteger value)
        {
            var v = value.bits;
            var vl = value.length;

            if (vl >= L)
            {
                Calc.Divide(v, vl, m, ml, out v);
                return new BigInteger(v, v.Length, value.sign);
            }

            var l1 = vl - ml + 1;
            var q1 = l1 > 0
                ? Calc.Multiply(v, l1, ml - 1, u, ul, 0)
                : new uint[] { };

            var l2 = Bits.Length(q1, q1.Length) - ml - 1;
            var q2 = l2 > 0
                ? Calc.Multiply(q1, l2, ml + 1, m, ml, 0)
                : new uint[] { };

            var q = q2;
            var ql = Bits.Length(q, q.Length);

            var r = Calc.Subtract(v, vl > ml + 1 ? ml + 1 : vl,
                                  q, ql > ml + 1 ? ml + 1 : ql);
            var rl = Bits.Length(r, r.Length);

            while (Bits.Compare(r, rl, m, ml) >= 0)
            {
                Calc.SubtractSelf(r, rl, m, ml);
                rl = Bits.Length(r, r.Length);
            }

            return new BigInteger(r, rl, value.sign);
        }
    }

    /// <remarks>
    /// BigInteger arrays may be null or bigger than necessary, appropriate length
    /// parameters must be used. The left parameter, if any, is always assumed
    /// as the more lengthy one. No checks, beware!
    /// </remarks>
    static class Calc
    {
        public static uint[] And(uint[] left, int leftLength,
                                 uint right, uint rightPad)
        {
            if (leftLength == 0)
                return new uint[] { };

            var bits = new uint[leftLength];
            bits[0] = left[0] & right;
            for (var i = 1; i < leftLength; i++)
                bits[i] = left[i] & rightPad;

            return bits;
        }

        public static uint[] And(uint[] left, int leftLength,
                                 uint[] right, int rightLength,
                                 uint rightPad)
        {
            var bits = new uint[leftLength];
            for (var i = 0; i < rightLength; i++)
                bits[i] = left[i] & right[i];
            for (var i = rightLength; i < leftLength; i++)
                bits[i] = left[i] & rightPad;

            return bits;
        }

        public static uint[] Or(uint[] left, int leftLength,
                                uint right, uint rightPad)
        {
            if (leftLength == 0)
                return new uint[] { right };

            var bits = new uint[leftLength];
            bits[0] = left[0] | right;
            for (var i = 1; i < leftLength; i++)
                bits[i] = left[i] | rightPad;

            return bits;
        }

        public static uint[] Or(uint[] left, int leftLength,
                                uint[] right, int rightLength,
                                uint rightPad)
        {
            var bits = new uint[leftLength];
            for (var i = 0; i < rightLength; i++)
                bits[i] = left[i] | right[i];
            for (var i = rightLength; i < leftLength; i++)
                bits[i] = left[i] | rightPad;

            return bits;
        }

        public static uint[] Xor(uint[] left, int leftLength,
                                 uint right, uint rightPad)
        {
            if (leftLength == 0)
                return new uint[] { right };

            var bits = new uint[leftLength];
            bits[0] = left[0] ^ right;
            for (var i = 1; i < leftLength; i++)
                bits[i] = left[i] ^ rightPad;

            return bits;
        }

        public static uint[] Xor(uint[] left, int leftLength,
                                 uint[] right, int rightLength,
                                 uint rightPad)
        {
            var bits = new uint[leftLength];
            for (var i = 0; i < rightLength; i++)
                bits[i] = left[i] ^ right[i];
            for (var i = rightLength; i < leftLength; i++)
                bits[i] = left[i] ^ rightPad;

            return bits;
        }

        public static uint[] Shift(uint[] value, int length,
                                   int shift, uint pad)
        {
            if (length == 0)
                return new uint[] { pad };

            if (shift < 0)
            {
                // big shifts move entire blocks
                var leapShift = -shift / 32;
                if (length <= leapShift)
                    return new uint[] { pad };
                var tinyShift = -shift % 32;

                // shifts the bits to the right
                var bits = new uint[length - leapShift];
                if (tinyShift == 0)
                {
                    for (var i = 0; i < bits.Length; i++)
                        bits[i] = value[i + leapShift];
                }
                else
                {
                    for (var i = 0; i < bits.Length - 1; i++)
                        bits[i] = (value[i + leapShift] >> tinyShift)
                            | (value[i + leapShift + 1] << (32 - tinyShift));
                    bits[bits.Length - 1] = (pad << (32 - tinyShift))
                        | (value[length - 1] >> tinyShift);
                }

                return bits;
            }
            if (shift > 0)
            {
                // big shifts move entire blocks
                var leapShift = shift / 32;
                var tinyShift = shift % 32;

                // shifts the bits to the left
                var bits = new uint[length + leapShift + 1];
                if (tinyShift == 0)
                {
                    for (var i = leapShift; i < bits.Length - 1; i++)
                        bits[i] = value[i - leapShift];
                    bits[bits.Length - 1] = pad;
                }
                else
                {
                    for (var i = leapShift + 1; i < bits.Length - 1; i++)
                    {
                        bits[i] = (value[i - leapShift] << tinyShift)
                            | (value[i - leapShift - 1] >> (32 - tinyShift));
                    }
                    bits[leapShift] = value[0] << tinyShift;
                    bits[bits.Length - 1] = (pad << tinyShift)
                        | (value[length - 1] >> (32 - tinyShift));
                }

                return bits;
            }
            else
            {
                // no shift at all...
                var bits = new uint[length];
                for (var i = 0; i < length; i++)
                    bits[i] = value[i];

                return bits;
            }
        }

        public static uint[] Add(uint[] left, int leftLength,
                                 uint right)
        {
            if (leftLength == 0)
                return new uint[] { right };

            var bits = new uint[leftLength + 1];

            // first operation
            var digit = (long)left[0] + right;
            bits[0] = (uint)digit;
            var carry = digit >> 32;

            // adds the bits
            for (var i = 1; i < leftLength; i++)
            {
                digit = left[i] + carry;
                bits[i] = (uint)digit;
                carry = digit >> 32;
            }
            bits[bits.Length - 1] = (uint)carry;

            return bits;
        }

        public static uint[] Add(uint[] left, int leftLength,
                                 uint[] right, int rightLength)
        {
            var bits = new uint[leftLength + 1];
            var carry = 0L;

            // adds the bits
            for (var i = 0; i < rightLength; i++)
            {
                var digit = (left[i] + carry) + right[i];
                bits[i] = (uint)digit;
                carry = digit >> 32;
            }
            for (var i = rightLength; i < leftLength; i++)
            {
                var digit = left[i] + carry;
                bits[i] = (uint)digit;
                carry = digit >> 32;
            }
            bits[bits.Length - 1] = (uint)carry;

            return bits;
        }

        public static void AddSelf(uint[] left, int leftLength,
                                   uint[] right, int rightLength)
        {
            var carry = 0L;

            // adds the bits
            for (var i = 0; i < rightLength; i++)
            {
                var digit = (left[i] + carry) + right[i];
                left[i] = (uint)digit;
                carry = digit >> 32;
            }
            for (var i = rightLength; carry != 0 && i < leftLength; i++)
            {
                var digit = left[i] + carry;
                left[i] = (uint)digit;
                carry = digit >> 32;
            }
        }

        public static void AddSelf(uint[] left, int leftLength,
                                   uint[] right, int rightLength,
                                   int rightOffset)
        {
            var bound = rightOffset + rightLength;
            var carry = 0L;

            // adds the bits
            for (var i = rightOffset; i < bound; i++)
            {
                var digit = (left[i] + carry) + right[i - rightOffset];
                left[i] = (uint)digit;
                carry = digit >> 32;
            }
            for (var i = bound; carry != 0 && i < leftLength; i++)
            {
                var digit = left[i] + carry;
                left[i] = (uint)digit;
                carry = digit >> 32;
            }
        }

        static uint[] AddFold(uint[] value,
                              int leftLength, int leftOffset,
                              int rightLength, int rightOffset)
        {
            var bits = new uint[leftLength + 1];
            var carry = 0L;

            // adds the bits
            for (var i = 0; i < rightLength; i++)
            {
                var digit = (value[i + leftOffset] + carry)
                    + value[i + rightOffset];
                bits[i] = (uint)digit;
                carry = digit >> 32;
            }
            for (var i = rightLength; i < leftLength; i++)
            {
                var digit = value[i + leftOffset] + carry;
                bits[i] = (uint)digit;
                carry = digit >> 32;
            }
            bits[bits.Length - 1] = (uint)carry;

            return bits;
        }

        public static uint[] Subtract(uint[] left, int leftLength,
                                      uint right)
        {
            if (leftLength == 0)
                return new uint[] { };

            var bits = new uint[leftLength];

            // first operation
            var digit = (long)left[0] - right;
            bits[0] = (uint)digit;
            var carry = digit >> 32;

            // subtracts the bits
            for (var i = 1; i < leftLength; i++)
            {
                digit = left[i] + carry;
                bits[i] = (uint)digit;
                carry = digit >> 32;
            }

            return bits;
        }

        public static uint[] Subtract(uint[] left, int leftLength,
                                      uint[] right, int rightLength)
        {
            var bits = new uint[leftLength];
            var carry = 0L;

            // subtracts the bits
            for (var i = 0; i < rightLength; i++)
            {
                var digit = (left[i] + carry) - right[i];
                bits[i] = (uint)digit;
                carry = digit >> 32;
            }
            for (var i = rightLength; i < leftLength; i++)
            {
                var digit = left[i] + carry;
                bits[i] = (uint)digit;
                carry = digit >> 32;
            }

            return bits;
        }

        public static void SubtractSelf(uint[] left, int leftLength,
                                        uint[] right, int rightLength)
        {
            var carry = 0L;

            // subtract the bits
            for (var i = 0; i < rightLength; i++)
            {
                var digit = (left[i] + carry) - right[i];
                left[i] = (uint)digit;
                carry = digit >> 32;
            }
            for (var i = rightLength; carry != 0 && i < leftLength; i++)
            {
                var digit = left[i] + carry;
                left[i] = (uint)digit;
                carry = digit >> 32;
            }
        }

        public static void SubtractSelf(uint[] left, int leftLength,
                                        uint[] right, int rightLength,
                                        int rightOffset)
        {
            var bound = rightOffset + rightLength;
            var carry = 0L;

            // subtract the bits
            for (var i = rightOffset; i < bound; i++)
            {
                var digit = (left[i] + carry) - right[i - rightOffset];
                left[i] = (uint)digit;
                carry = digit >> 32;
            }
            for (var i = bound; carry != 0 && i < leftLength; i++)
            {
                var digit = left[i] + carry;
                left[i] = (uint)digit;
                carry = digit >> 32;
            }
        }

        static void SubtractCore(uint[] value,
                                 int leftLength, int leftOffset,
                                 int rightLength, int rightOffset,
                                 uint[] core, int coreLength)
        {
            var carry = 0L;

            // subtract the bits
            for (var i = 0; i < rightLength; i++)
            {
                var digit = (core[i] + carry)
                    - value[i + leftOffset]
                    - value[i + rightOffset];
                core[i] = (uint)digit;
                carry = digit >> 32;
            }
            for (var i = rightLength; i < leftLength; i++)
            {
                var digit = (core[i] + carry)
                    - value[i + leftOffset];
                core[i] = (uint)digit;
                carry = digit >> 32;
            }
            for (var i = leftLength; carry != 0 && i < coreLength; i++)
            {
                var digit = core[i] + carry;
                core[i] = (uint)digit;
                carry = digit >> 32;
            }
        }

        public static uint[] Square(uint[] value, int valueLength)
        {
            return Square(value, valueLength, 0);
        }

#if DEBUG

        const int SquareThreshold = 128 / 32;

#else

            const int SquareThreshold = 1024 / 32;

#endif

        public static uint[] Square(uint[] value, int valueLength,
                                    int valueOffset)
        {
            var bits = new uint[valueLength * 2];

            Square(value, valueLength, valueOffset, bits, 0);

            return bits;
        }

        static void Square(uint[] value, int valueLength,
                           int valueOffset,
                           uint[] bits, int bitsOffset)
        {
            if (valueLength < SquareThreshold)
            {
                // squares the bits
                for (var i = 0; i < valueLength; i++)
                {
                    var carry = 0UL;
                    for (var j = 0; j < i; j++)
                    {
                        var digit1 = bits[i + j + bitsOffset] + carry;
                        var digit2 = (ulong)value[j + valueOffset]
                            * (ulong)value[i + valueOffset];
                        bits[i + j + bitsOffset] =
                            (uint)(digit1 + (digit2 << 1));
                        carry = (digit2 + (digit1 >> 1)) >> 31;
                    }
                    var digit = (ulong)value[i + valueOffset]
                        * (ulong)value[i + valueOffset] + carry;
                    bits[i * 2 + bitsOffset] = (uint)digit;
                    bits[i * 2 + 1 + bitsOffset] = (uint)(digit >> 32);
                }
            }
            else
            {
                // divide & conquer
                var n = valueLength / 2;

                var lowOffset = valueOffset;
                var lowLength = n;
                var highOffset = valueOffset + n;
                var highLength = valueLength - n;

                Square(value, lowLength, lowOffset, bits, bitsOffset);
                Square(value, highLength, highOffset, bits, bitsOffset + n * 2);

                var fold = AddFold(value, highLength, highOffset,
                                          lowLength, lowOffset);
                var foldLength = fold[fold.Length - 1] == 0
                               ? fold.Length - 1 : fold.Length;

                var core = Square(fold, foldLength);
                SubtractCore(bits, highLength * 2, bitsOffset + n * 2,
                                   lowLength * 2, bitsOffset,
                                   core, core.Length);

                // merge the result
                AddSelf(bits, bits.Length, core, core.Length, bitsOffset + n);
            }
        }

        public static uint[] Multiply(uint[] left, int leftLength,
                                      uint right)
        {
            var bits = new uint[leftLength + 1];

            // multiplies the bits
            var carry = 0UL;
            for (var j = 0; j < leftLength; j++)
            {
                var digits = (ulong)left[j] * right + carry;
                bits[j] = (uint)digits;
                carry = digits >> 32;
            }
            bits[leftLength] = (uint)carry;

            return bits;
        }

        public static uint[] Multiply(uint[] left, int leftLength,
                                      uint[] right, int rightLength)
        {
            return Multiply(left, leftLength, 0, right, rightLength, 0);
        }

#if DEBUG

        const int MultiplyThreshold = 128 / 32;

#else

            const int MultiplyThreshold = 1024 / 32;

#endif

        public static uint[] Multiply(uint[] left, int leftLength,
                                      int leftOffset,
                                      uint[] right, int rightLength,
                                      int rightOffset)
        {
            var bits = new uint[leftLength + rightLength];

            Multiply(left, leftLength, leftOffset,
                     right, rightLength, rightOffset,
                     bits, 0);

            return bits;
        }

        static void Multiply(uint[] left, int leftLength,
                             int leftOffset,
                             uint[] right, int rightLength,
                             int rightOffset,
                             uint[] bits, int bitsOffset)
        {
            if (leftLength < MultiplyThreshold || rightLength < MultiplyThreshold)
            {
                // multiplies the bits
                for (var i = 0; i < rightLength; i++)
                {
                    var carry = 0UL;
                    for (var j = 0; j < leftLength; j++)
                    {
                        var digits = bits[i + j + bitsOffset] + carry
                            + (ulong)left[j + leftOffset]
                            * (ulong)right[i + rightOffset];
                        bits[i + j + bitsOffset] = (uint)digits;
                        carry = digits >> 32;
                    }
                    bits[i + leftLength + bitsOffset] = (uint)carry;
                }
            }
            else
            {
                // divide & conquer
                var n = (leftLength < rightLength
                      ? leftLength : rightLength) / 2;

                // x = (x_1 << n) + x_0
                var leftLowOffset = leftOffset;
                var leftLowLength = n;
                var leftHighOffset = leftOffset + n;
                var leftHighLength = leftLength - n;

                // y = (y_1 << n) + y_0
                var rightLowOffset = rightOffset;
                var rightLowLength = n;
                var rightHighOffset = rightOffset + n;
                var rightHighLength = rightLength - n;

                // z_0 = x_0 * y_0
                Multiply(left, leftLowLength, leftLowOffset,
                         right, rightLowLength, rightLowOffset,
                         bits, bitsOffset);

                // z_2 = x_1 * y_1
                Multiply(left, leftHighLength, leftHighOffset,
                         right, rightHighLength, rightHighOffset,
                         bits, bitsOffset + n * 2);

                // z_x = x_1 + x_0
                var leftFold = AddFold(left, leftHighLength, leftHighOffset,
                                       leftLowLength, leftLowOffset);
                var leftFoldLength = leftFold[leftFold.Length - 1] == 0
                                   ? leftFold.Length - 1 : leftFold.Length;

                // z_y = y_1 + y_0
                var rightFold = AddFold(right, rightHighLength, rightHighOffset,
                                        rightLowLength, rightLowOffset);
                var rightFoldLength = rightFold[rightFold.Length - 1] == 0
                                    ? rightFold.Length - 1 : rightFold.Length;

                // z_1 = z_x * z_y - z_0 - z_2
                var core = Multiply(leftFold, leftFoldLength,
                                    rightFold, rightFoldLength);
                SubtractCore(bits,
                    leftHighLength + rightHighLength, bitsOffset + n * 2,
                    leftLowLength + rightLowLength, bitsOffset,
                    core, core.Length);

                // merge the result
                AddSelf(bits, bits.Length, core, core.Length, bitsOffset + n);
            }
        }

        public static uint[] Divide(uint[] left, int leftLength,
                                    uint right, out uint remainder)
        {
            var bits = new uint[leftLength];

            // divides the bits
            var carry = 0UL;
            for (var i = leftLength - 1; i >= 0; i--)
            {
                var value = (carry << 32) | left[i];
                bits[i] = (uint)(value / right);
                carry = value % right;
            }
            remainder = (uint)carry;

            return bits;
        }

        public static uint Remainder(uint[] left, int leftLength,
                                     uint right)
        {
            // divides the bits
            var carry = 0UL;
            for (var i = leftLength - 1; i >= 0; i--)
            {
                var value = (carry << 32) | left[i];
                carry = value % right;
            }

            return (uint)carry;
        }

        public static uint[] Divide(uint[] left, int leftLength,
                                    uint[] right, int rightLength,
                                    out uint[] remainder)
        {
            var bits = new uint[leftLength - rightLength + 1];

            // get more bits into the highest bit block
            var shifted = Bits.LeadingZeros(right[rightLength - 1]);
            left = Shift(left, leftLength, shifted, 0);
            right = Shift(right, rightLength, shifted, 0);

            // measure again (after shift...)
            leftLength = left[left.Length - 1] == 0
                ? left.Length - 1 : left.Length;

            // these values are useful
            var divHi = right[rightLength - 1];
            var divLo = rightLength > 1 ? right[rightLength - 2] : 0;
            var guess = new uint[rightLength + 1];
            var guessLength = 0;
            var delta = 0;

            // sub the divisor
            do
            {
                delta = Bits.Compare(left, leftLength,
                    right, rightLength, leftLength - rightLength);
                if (delta >= 0)
                {
                    ++bits[leftLength - rightLength];
                    SubtractSelf(left, leftLength,
                        right, rightLength, leftLength - rightLength);
                }
            }
            while (delta > 0);

            // divides the rest of the bits
            for (var i = leftLength - 1; i >= rightLength; i--)
            {
                // first guess for the current bit of the quotient
                var leftHi = (left[i - 1] | ((ulong)left[i] << 32));
                var digits = leftHi / divHi;
                if (digits > 0xFFFFFFFF)
                    digits = 0xFFFFFFFF;

                // the guess may be a little bit to big
                var check = divHi * digits + ((divLo * digits) >> 32);
                if (check > leftHi)
                    --digits;

                // the guess may be still a little bit to big
                do
                {
                    MultiplyDivisor(right, rightLength, digits, guess);
                    guessLength = guess[guess.Length - 1] == 0
                        ? guess.Length - 1 : guess.Length;
                    delta = Bits.Compare(left, left[i] != 0 ? i + 1 : i,
                        guess, guessLength, i - rightLength);
                    if (delta < 0)
                        --digits;
                }
                while (delta < 0);

                // we have the bit!
                SubtractSelf(left, i + 1,
                    guess, guessLength, i - rightLength);
                bits[i - rightLength] = (uint)digits;
            }

            // repair the cheated shift
            remainder = Shift(left, rightLength, -shifted, 0);

            return bits;
        }

        static void MultiplyDivisor(uint[] left, int leftLength,
                                    ulong right, uint[] bits)
        {
            // multiplies the bits
            var carry = 0UL;
            for (var j = 0; j < leftLength; j++)
            {
                var digits = left[j] * right + carry;
                bits[j] = (uint)digits;
                carry = digits >> 32;
            }
            bits[leftLength] = (uint)carry;
        }
    }

    static class Lehmer
    {
        public static BigInteger Gcd(BigInteger x, BigInteger y)
        {
            // retrieve private copy (!)
            var xBits = Calc.Shift(x.bits, x.length, 0, 0);
            var yBits = Calc.Shift(y.bits, y.length, 0, 0);

            var result = Gcd(xBits, x.length, yBits, y.length);
            return new BigInteger(result, result.Length, false);
        }

        public static BigInteger Inv(BigInteger x, BigInteger y)
        {
            // retrieve private copy (!)
            var xBits = Calc.Shift(x.bits, x.length, 0, 0);
            var yBits = Calc.Shift(y.bits, y.length, 0, 0);

            var result = Inv(xBits, x.length, yBits, y.length);
            if (result[result.Length - 1] != 0)
                Calc.AddSelf(result, result.Length, x.bits, x.length);
            return new BigInteger(result, result.Length, false);
        }

        static uint[] Gcd(uint[] xBits, int xLength,
                          uint[] yBits, int yLength)
        {
            while (yLength > 1)
            {
                var x = 0L;
                var y = 0L;

                // extract most significant bits
                Values(xBits, xLength, yBits, yLength, out x, out y);

                var a = 1L; var b = 0L;
                var c = 0L; var d = 1L;

                // Lehmer's guessing
                while (y + c != 0 && y + d != 0)
                {
                    var q = (x + a) / (y + c);
                    var qc = (x + b) / (y + d);

                    if (q != qc)
                        break;

                    var r = a - q * c;
                    var s = b - q * d;
                    var t = x - q * y;

                    a = c; c = r;
                    b = d; d = s;
                    x = y; y = t;
                }

                if (b == 0)
                {
                    // Euclid's step
                    var tBits = default(uint[]);
                    Calc.Divide(xBits, xLength, yBits, yLength,
                        out tBits);

                    xBits = yBits; xLength = yLength;
                    yBits = tBits; yLength = Bits.Length(tBits, tBits.Length);
                }
                else
                {
                    // Lehmer's step
                    var length = yLength;
                    MulAdd(a, b, c, d, xBits, yBits, length);

                    xLength = Bits.Length(xBits, length);
                    yLength = Bits.Length(yBits, length);
                }
            }

            // ordinary algorithm
            if (yLength == 1)
            {
                // Euclid's step
                var y = Calc.Remainder(xBits, xLength, yBits[0]);
                var x = yBits[0];

                while (y != 0)
                {
                    // Euclid's step
                    var t = x % y;
                    x = y;
                    y = t;
                }

                return new uint[] { x };
            }

            // trim it!
            return Calc.Shift(xBits, xLength, 0, 0);
        }

        static uint[] Inv(uint[] xBits, int xLength,
                          uint[] yBits, int yLength)
        {
            // reserve one digit for the sign!
            var iBits = new uint[xLength + 1];
            var jBits = new uint[xLength + 1];
            jBits[0] = 1;

            while (yLength > 1)
            {
                var x = 0L;
                var y = 0L;

                // extract most significant bits
                Values(xBits, xLength, yBits, yLength, out x, out y);

                var a = 1L; var b = 0L;
                var c = 0L; var d = 1L;

                // Lehmer's guessing
                while (y + c != 0 && y + d != 0)
                {
                    var q = (x + a) / (y + c);
                    var qc = (x + b) / (y + d);

                    if (q != qc)
                        break;

                    var r = a - q * c;
                    var s = b - q * d;
                    var t = x - q * y;

                    a = c; c = r;
                    b = d; d = s;
                    x = y; y = t;
                }

                if (b == 0)
                {
                    // Euclid's step
                    var tBits = default(uint[]);
                    var qBits = Calc.Divide(xBits, xLength, yBits, yLength,
                        out tBits);

                    xBits = yBits; xLength = yLength;
                    yBits = tBits; yLength = Bits.Length(tBits, tBits.Length);

                    // Enhanced Euclid's step
                    var uBits = iBits;
                    var vBits = Calc.Multiply(jBits, jBits.Length,
                                              qBits, qBits.Length);
                    Calc.SubtractSelf(iBits, iBits.Length,
                                      vBits, iBits.Length);
                    iBits = jBits; jBits = uBits;
                }
                else
                {
                    // Lehmer's step
                    var length = yLength;
                    MulAdd(a, b, c, d, xBits, yBits, length);

                    xLength = Bits.Length(xBits, length);
                    yLength = Bits.Length(yBits, length);

                    // Enhanced Lehmer's step
                    MulAdd(a, b, c, d, iBits, jBits, jBits.Length);
                }
            }

            // ordinary algorithm
            if (yLength == 1)
            {
                // Euclid's step
                var y = default(uint);
                var qBits = Calc.Divide(xBits, xLength, yBits[0], out y);
                var x = yBits[0];

                // Enhanced Euclid's step
                var uBits = iBits;
                var vBits = Calc.Multiply(jBits, jBits.Length,
                                          qBits, qBits.Length);
                Calc.SubtractSelf(iBits, iBits.Length,
                                  vBits, iBits.Length);
                iBits = jBits; jBits = uBits;

                while (y != 0)
                {
                    // Euclid's step
                    var q = x / y;
                    var t = x % y;
                    x = y;
                    y = t;

                    // Enhanced Euclid's step
                    uBits = iBits;
                    vBits = Calc.Multiply(jBits, jBits.Length, q);
                    Calc.SubtractSelf(iBits, iBits.Length,
                                      vBits, iBits.Length);
                    iBits = jBits; jBits = uBits;
                }

                if (x == 1)
                    return iBits;
            }

            return new uint[] { 0 };
        }

        static void Values(uint[] xBits, int xLength,
                           uint[] yBits, int yLength,
                           out long x, out long y)
        {
            var xh = 0UL; var xm = 0UL; var xl = 0UL;
            var yh = 0UL; var ym = 0UL; var yl = 0UL;

            // has low digit?
            var hasLo = xLength > 2;

            xh = xBits[xLength - 1];
            xm = xBits[xLength - 2];
            if (hasLo) xl = xBits[xLength - 3];

            // arrange the bits
            switch (xLength - yLength)
            {
                case 0:
                    yh = yBits[yLength - 1];
                    ym = yBits[yLength - 2];
                    if (hasLo) yl = yBits[yLength - 3];
                    break;

                case 1:
                    ym = yBits[yLength - 1];
                    if (hasLo) yl = yBits[yLength - 2];
                    break;

                case 2:
                    if (hasLo) yl = yBits[yLength - 1];
                    break;
            }

            // use all the bits but two
            var z = Bits.LeadingZeros((uint)xh);

            x = (long)(((xh << 32 + z) | (xm << z) | (xl >> 32 - z)) >> 2);
            y = (long)(((yh << 32 + z) | (ym << z) | (yl >> 32 - z)) >> 2);
        }

        static void MulAdd(long a, long b, long c, long d,
                           uint[] x, uint[] y, int length)
        {
            var xCarry = 0L;
            var yCarry = 0L;
            for (var i = 0; i < length; i++)
            {
                var xDigits = a * x[i] + b * y[i] + xCarry;
                var yDigits = c * x[i] + d * y[i] + yCarry;
                x[i] = (uint)xDigits;
                y[i] = (uint)yDigits;
                xCarry = xDigits >> 32;
                yCarry = yDigits >> 32;
            }
        }
    }

    #endregion
}