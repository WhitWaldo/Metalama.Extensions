﻿// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this repository root for details.

using Metalama.Extensions.Architecture.Aspects;
using Metalama.Extensions.Architecture.AspectTests.InternalsOnlyAccessibleFrom.AllowedNs;

namespace Metalama.Extensions.Architecture.AspectTests.InternalsOnlyAccessibleFrom
{
    namespace AllowedNs
    {
        [InternalsCanOnlyBeUsedFrom( CurrentNamespace = true )]
        public class ConstrainedClass
        {
            public static void PublicMethod() { }

            protected static void ProtectedMethod() { }

            internal static void InternalMethod() { }

            protected internal static void InternalProtectedMethod() { }

            private protected static void PrivateProtectedMethod() { }
        }

        internal class AllowedClass
        {
            public static void Method()
            {
                // All usages are allowed.
                ConstrainedClass.PublicMethod();
                ConstrainedClass.InternalProtectedMethod();
                ConstrainedClass.InternalMethod();
            }
        }
    }

    internal class ForbiddenClassWithAllowedCalls
    {
        public static void AllowedCalls()
        {
            // This call should be allowed because it is public.
            ConstrainedClass.PublicMethod();
        }
    }

    internal class ForbiddenClassWithForbiddenCalls
    {
        public static void ForbiddenCalls()
        {
            // These calls should be forbidden.
            ConstrainedClass.InternalMethod();
            ConstrainedClass.InternalProtectedMethod();
        }
    }

    internal class DerivedClassWithAllowedCalls : ConstrainedClass
    {
        public static void Method()
        {
            // These calls should be allowed.
            PublicMethod();
            InternalProtectedMethod();
            ProtectedMethod();
        }
    }

    internal class DerivedClassWithForbiddenCalls : ConstrainedClass
    {
        public static void Method()
        {
            // These calls should be forbidden.
            InternalMethod();
            PrivateProtectedMethod();
        }
    }
}