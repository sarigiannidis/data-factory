// --------------------------------------------------------------------------------
// <copyright file="WeightedValue.cs" company="Michalis Sarigiannidis">
// Copyright 2019 © Michalis Sarigiannidis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// --------------------------------------------------------------------------------

namespace Df.Numeric
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;

    [JsonObject(IsReference = false)]
    public readonly struct WeightedValue<TValue>
        : IEquatable<WeightedValue<TValue>>,
        IComparable<WeightedValue<TValue>>
        where TValue : IComparable, IComparable<TValue>, IEquatable<TValue>
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public const float DEFAULTWEIGHT = 1.0f;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public TValue Value { get; }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public float Weight { get; }

        public WeightedValue(TValue value, float weight)
        {
            Value = value;
            Weight = Check.GreaterThanOrEqual(nameof(weight), weight, 0);
        }

        public WeightedValue(TValue value)
            : this(value, DEFAULTWEIGHT)
        {
        }

        public static TValue FromWeightedValue(WeightedValue<TValue> weightedValue) =>
            weightedValue.Value;

        public static implicit operator TValue(WeightedValue<TValue> weightedValue) =>
            FromWeightedValue(weightedValue);

        public static implicit operator WeightedValue<TValue>(TValue value) =>
            ToWeightedValue(value);

        public static implicit operator WeightedValue<TValue>(JToken jToken) =>
            new WeightedValue<TValue>(jToken.Value<TValue>(nameof(Value)), jToken.Value<float>(nameof(Weight)));

        public static bool operator !=(WeightedValue<TValue> left, WeightedValue<TValue> right) =>
            !(left == right);

        public static bool operator <(WeightedValue<TValue> left, WeightedValue<TValue> right) =>
            left.CompareTo(right) < 0;

        public static bool operator <=(WeightedValue<TValue> left, WeightedValue<TValue> right) =>
            left.CompareTo(right) <= 0;

        public static bool operator ==(WeightedValue<TValue> left, WeightedValue<TValue> right) =>
            left.Equals(right);

        public static bool operator >(WeightedValue<TValue> left, WeightedValue<TValue> right) =>
            left.CompareTo(right) > 0;

        public static bool operator >=(WeightedValue<TValue> left, WeightedValue<TValue> right) =>
            left.CompareTo(right) >= 0;

        public static WeightedValue<TValue> ToWeightedValue(TValue value) =>
            new WeightedValue<TValue>(value);

        public int CompareTo(WeightedValue<TValue> other)
        {
            var valueComparer = Comparer<TValue>.Default;
            var result = valueComparer.Compare(Value, other.Value);
            return result == 0 ? Weight.CompareTo(other.Weight) : result;
        }

        public void Deconstruct(out TValue value, out float weight)
        {
            value = Value;
            weight = Weight;
        }

        public override bool Equals(object obj) =>
            obj is WeightedValue<TValue> o && Equals(o);

        public bool Equals(WeightedValue<TValue> other) =>
            EqualityComparer<TValue>.Default.Equals(Value, other.Value) && other.Weight == Weight;

        public override int GetHashCode() =>
            HashCode.Combine(Value, Weight);

        public override string ToString() =>
            "Value = {0}, Weight = {1}".FormatInvariant(Value, Weight);
    }
}