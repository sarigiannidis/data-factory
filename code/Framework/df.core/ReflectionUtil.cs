// --------------------------------------------------------------------------------
// <copyright file="ReflectionUtil.cs" company="Michalis Sarigiannidis">
// Copyright 2019 © Michalis Sarigiannidis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// --------------------------------------------------------------------------------

namespace Df
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    internal static class ReflectionUtil
    {
        public static Func<T> CreateDefaultInstance<T>(Type type)
        {
            var defaultConstructor = GetDefaultConstructor(type);
            return defaultConstructor == null ? (Func<T>)null : (() => (T)defaultConstructor.Invoke(null));
        }

        public static IDictionary<string, object> GetConstructorArguments(CustomAttributeData customAttributeData) =>
            customAttributeData.Constructor.GetParameters().ToDictionary(_ => _.Name, _ => customAttributeData.ConstructorArguments[_.Position].Value);

        public static ConstructorInfo GetDefaultConstructor(Type type) =>
            type.GetConstructor(BindingFlags.Instance | BindingFlags.Public, null, CallingConventions.HasThis, Array.Empty<Type>(), null);
    }
}