using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

// ReSharper disable InconsistentNaming

namespace Tuyin.IR.Analysis.Utils
{
    [DataContract]
    internal struct Either<L, R> : IEquatable<Either<L, R>>
    {
        [DataMember]
        internal L Left { get; private set; }

        [DataMember]
        internal R Right { get; private set; }

        [DataMember]
        internal bool IsLeft { get; private set; }

        internal bool IsRight => !IsLeft;

        public static implicit operator Either<L, R>(L left) => new Either<L, R> { Left = left, IsLeft = true };
        public static implicit operator Either<L, R>(R right) => new Either<L, R> { Right = right, IsLeft = false };
        public static implicit operator Either<L, R>(Either<R, L> either) => either.Reverse();

        public override string ToString() => IsLeft ? $"{typeof(L).Name}: {Left}" : $"{typeof(R).Name}: {Right}";

        #region Equality members

        public bool Equals(Either<L, R> other)
        {
            return IsLeft
                ? other.IsLeft && EqualityComparer<L>.Default.Equals(Left, other.Left)
                : !other.IsLeft && EqualityComparer<R>.Default.Equals(Right, other.Right);
        }

        public override bool Equals(object obj) => obj is Either<L, R> && Equals((Either<L, R>)obj);

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = IsLeft
                    ? EqualityComparer<L>.Default.GetHashCode(Left)
                    : EqualityComparer<R>.Default.GetHashCode(Right);
                return hashCode * 397 ^ IsLeft.GetHashCode();
            }
        }

        public static bool operator ==(Either<L, R> left, Either<L, R> right) => left.Equals(right);
        public static bool operator !=(Either<L, R> left, Either<L, R> right) => !left.Equals(right);

        #endregion
    }

    internal static class EitherExtensions
    {
        public static L Left<L, R>(this Either<L, R> either, Func<R, L> right = null)
        {
            if (either.IsLeft)
                return either.Left;
            if (right != null)
                return right(either.Right);
            throw new ArgumentException($"Either is {typeof(R).Name}, but no suitable conversion to {typeof(L).Name} provided.", nameof(either));
        }

        public static R Right<L, R>(this Either<L, R> either, Func<L, R> left = null)
        {
            if (either.IsRight)
                return either.Right;
            if (left != null)
                return left(either.Left);
            throw new ArgumentException($"Either is {typeof(L).Name}, but no suitable conversion to {typeof(R).Name} provided.", nameof(either));
        }

        public static TResult Return<L, R, TResult>(this Either<L, R> either, Func<L, TResult> left, Func<R, TResult> right)
        {
            return either.IsLeft ? left(either.Left) : right(either.Right);
        }

        public static void Do<L, R>(this Either<L, R> either, Action<L> left = null, Action<R> right = null)
        {
            if (either.IsLeft)
                left?.Invoke(either.Left);
            else
                right?.Invoke(either.Right);
        }

        public static void IfLeft<L, R>(this Either<L, R> either, Action<L> left) => either.Do(left: left);
        public static void IfRight<L, R>(this Either<L, R> either, Action<R> right) => either.Do(right: right);

        public static Either<L, R> Reverse<L, R>(this Either<R, L> either)
        {
            if (either.IsLeft)
                return either.Left;
            return either.Right;
        }

        #region Mapping left either

        public static Either<NL, R> MapLeft<L, R, NL>(this Either<L, R> either, Func<L, NL> left, Action<R> right = null)
            => either.MapLeft(l => (Either<NL, R>)left(l), right);

        public static Either<NL, R> MapLeft<L, R, NL>(this Either<L, R> either, Func<L, Either<R, NL>> left, Action<R> right = null)
            => either.MapLeft(l => (Either<NL, R>)left(l), right);

        public static Either<NL, R> MapLeft<L, R, NL>(
            this Either<L, R> either,
            Func<L, Either<NL, R>> left,
            Action<R> right = null)
        {
            if (either.IsLeft)
                return left(either.Left);
            right?.Invoke(either.Right);
            return either.Right;
        }

        #endregion

        #region Mapping right either

        public static Either<L, NR> MapRight<L, R, NR>(this Either<L, R> either, Func<R, NR> right, Action<L> left = null)
            => either.MapRight(r => (Either<L, NR>)right(r), left);

        public static Either<L, NR> MapRight<L, R, NR>(this Either<L, R> either, Func<R, Either<NR, L>> right, Action<L> left = null)
            => either.MapRight(r => (Either<L, NR>)right(r), left);

        public static Either<L, NR> MapRight<L, R, NR>(
            this Either<L, R> either,
            Func<R, Either<L, NR>> right,
            Action<L> left = null)
        {
            if (either.IsRight)
                return right(either.Right);
            left?.Invoke(either.Left);
            return either.Left;
        }

        #endregion

        #region Mapping either

        public static Either<NL, NR> Map<L, R, NL, NR>(this Either<L, R> either, Func<L, NL> left, Func<R, NR> right)
            => either.Map(l => (Either<NL, NR>)left(l), r => (Either<NL, NR>)right(r));

        public static Either<NL, NR> Map<L, R, NL, NR>(this Either<L, R> either, Func<L, Either<NL, NR>> left, Func<R, NR> right)
            => either.Map(left, r => (Either<NL, NR>)right(r));

        public static Either<NL, NR> Map<L, R, NL, NR>(this Either<L, R> either, Func<L, NL> left, Func<R, Either<NR, NL>> right)
            => either.Map(l => (Either<NL, NR>)left(l), r => (Either<NL, NR>)right(r));

        public static Either<NL, NR> Map<L, R, NL, NR>(this Either<L, R> either, Func<L, Either<NR, NL>> left, Func<R, NR> right)
            => either.Map(l => (Either<NL, NR>)left(l), r => (Either<NL, NR>)right(r));

        public static Either<NL, NR> Map<L, R, NL, NR>(this Either<L, R> either, Func<L, NL> left, Func<R, Either<NL, NR>> right)
            => either.Map(l => (Either<NL, NR>)left(l), right);

        public static Either<NL, NR> Map<L, R, NL, NR>(this Either<L, R> either, Func<L, Either<NL, NR>> left,
            Func<R, Either<NR, NL>> right)
            => either.Map(left, r => (Either<NL, NR>)right(r));

        public static Either<NL, NR> Map<L, R, NL, NR>(
            this Either<L, R> either,
            Func<L, Either<NL, NR>> left,
            Func<R, Either<NL, NR>> right)
        {
            if (either.IsLeft)
                return left(either.Left);
            return right(either.Right);
        }

        #endregion

        #region Unwrapping nested either

        public static Either<L, R> Unwrap<L, R>(this Either<L, Either<L, R>> either) => either.IsLeft ? either.Left : either.Right;
        public static Either<L, R> Unwrap<L, R>(this Either<L, Either<R, L>> either) => either.IsLeft ? either.Left : either.Right;
        public static Either<L, R> Unwrap<L, R>(this Either<Either<L, R>, R> either) => either.IsLeft ? either.Left : either.Right;
        public static Either<L, R> Unwrap<L, R>(this Either<Either<R, L>, R> either) => either.IsLeft ? either.Left : either.Right;
        public static Either<L, R> Unwrap<L, R>(this Either<Either<L, R>, Either<L, R>> either) => either.IsLeft ? either.Left : either.Right;

        public static Either<L, R> Unwrap<L, R>(this Either<Either<L, R>, Either<R, L>> either)
            => either.IsLeft ? either.Left : (Either<L, R>)either.Right;

        #endregion
    }
}