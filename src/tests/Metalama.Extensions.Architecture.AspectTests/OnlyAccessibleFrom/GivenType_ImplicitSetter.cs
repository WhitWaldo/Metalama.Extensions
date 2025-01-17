﻿// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this repository root for details.

using Metalama.Extensions.Architecture.Aspects;

namespace Metalama.Extensions.Architecture.AspectTests.OnlyAccessibleFrom.GivenType_ImplicitSetter
{
    internal class ConstrainedClass
    {
        public object? ConstrainedProperty
        {
            get;

            [CanOnlyBeUsedFrom( Types = new[] { typeof(AllowedClass) } )]
            set;
        }
    }

    internal class ForbiddenClass
    {
        private static void GetAndSet()
        {
            var o = new ConstrainedClass();
            var v = o.ConstrainedProperty;
            o.ConstrainedProperty = new object();
        }
    }

    internal class AllowedClass
    {
        private static void GetAndSet()
        {
            var o = new ConstrainedClass();
            var v = o.ConstrainedProperty;
            o.ConstrainedProperty = new object();
        }
    }
}