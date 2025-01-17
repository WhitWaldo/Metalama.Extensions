﻿// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this repository root for details.

using Metalama.Framework.Aspects;
using Metalama.Extensions.DependencyInjection;
using Metalama.Extensions.DependencyInjection.Tests.Advice.LazyOptional;

[assembly: AspectOrder( typeof(DependencyAttribute), typeof(MyAspect) )]

namespace Metalama.Extensions.DependencyInjection.Tests.Advice.LazyOptional;

public class MyAspect : TypeAspect
{
    [IntroduceDependency( IsLazy = true, IsRequired = false )]
    private readonly IFormatProvider _formatProvider;
}

// <target>
[MyAspect]
public class TargetClass
{
    public TargetClass() { }

    public TargetClass( int x, IFormatProvider existingParameter ) { }
}