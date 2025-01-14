﻿// Copyright (c) Microsoft. All rights reserved.

using System;

namespace Microsoft.SemanticKernel.Security;

/// <summary>
/// A string wrapper that carries trust information.
/// All field are readonly.
/// </summary>
public class TrustAwareString : IEquatable<TrustAwareString>
{
    /// <summary>
    /// Create a new empty trust aware string (default trusted).
    /// </summary>
    public static TrustAwareString Empty => new(string.Empty, true);

    /// <summary>
    /// Create a new trusted string.
    /// </summary>
    /// <param name="value">The raw string value</param>
    /// <returns>TrustAwareString</returns>
    public static TrustAwareString Trusted(string? value) => new(value, true);

    /// <summary>
    /// Create a new untrusted string.
    /// </summary>
    /// <param name="value">The raw string value</param>
    /// <returns>TrustAwareString</returns>
    public static TrustAwareString Untrusted(string? value) => new(value, false);

    /// <summary>
    /// The raw string value.
    /// </summary>
    public string Value { get; }

    /// <summary>
    /// Whether the current value is trusted or not.
    /// </summary>
    public bool IsTrusted { get; }

    /// <summary>
    /// Create a new trust aware string.
    /// </summary>
    /// <param name="value">The raw string value</param>
    /// <param name="isTrusted">Whether the raw string value is trusted or not</param>
    public TrustAwareString(string? value, bool isTrusted = true)
    {
        this.Value = value ?? string.Empty;
        this.IsTrusted = isTrusted;
    }

    public override string ToString()
    {
        return this.Value;
    }

    public override bool Equals(object obj)
    {
        if (obj is null)
        {
            return false;
        }
        else if (obj is string str)
        {
            return this.Value == str;
        }
        else
        {
            return (obj is TrustAwareString trustAwareStr) && this.Equals(trustAwareStr);
        }
    }

    public bool Equals(TrustAwareString? other)
    {
        if (other is null)
        {
            return false;
        }

        return this.Value == other.Value && this.IsTrusted == other.IsTrusted;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(this.Value, this.IsTrusted);
    }

    public static bool operator ==(TrustAwareString? left, TrustAwareString? right)
    {
        if (left is null)
        {
            return right is null;
        }

        return left.Equals(right);
    }

    public static bool operator !=(TrustAwareString? left, TrustAwareString? right)
    {
        return !(left == right);
    }

    public static implicit operator string(TrustAwareString s) => s.Value;
}
