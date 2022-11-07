﻿// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this repository root for details.

using Metalama.Extensions.Multicast;
using Metalama.Extensions.Multicast.AspectTests;

[assembly: AddTag(
    "PublicType",
    AttributeTargetElements = MulticastTargets.Class,
    AttributeTargetTypeAttributes = MulticastAttributes.Public )]

[assembly: AddTag(
    "MethodOfPublicType",
    AttributeTargetElements = MulticastTargets.Method,
    AttributeTargetTypeAttributes = MulticastAttributes.Public )]

namespace Metalama.Extensions.Multicast.AspectTests.Filter_ByTypeAccessibity
{
    // <target>
    public class PublicClass
    {
        public void PublicMethod() { }

        internal void InternalMethod() { }
    }

    // <target>
    internal class InternalClass
    {
        public void PublicMethod() { }

        internal void InternalMethod() { }
    }
}