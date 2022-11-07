﻿// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this repository root for details.

using Metalama.Extensions.Multicast.AspectTests.Exclude_LeafNode;
using Metalama.Framework.Aspects;

#if TEST_OPTIONS
// @Include(_Tagging.cs)
#endif

[assembly: MyAspect( "1" )]

namespace Metalama.Extensions.Multicast.AspectTests.Exclude_LeafNode
{
    [AttributeUsage( AttributeTargets.Assembly | AttributeTargets.Method, AllowMultiple = true )]
    public class MyAspect : OverrideMethodMulticastAspect
    {
        private readonly string _tag;

        public MyAspect( string tag )
        {
            this._tag = tag;
        }

        public override dynamic? OverrideMethod()
        {
            Console.WriteLine( $"Overridden: {this._tag}" );

            return meta.Proceed();
        }
    }

    // <target>
    public class C
    {
        [MyAspect( "2", AttributeExclude = true )]
        public void M() { }
    }
}