﻿// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this repository root for details.

using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using Metalama.Framework.Eligibility;
using System;
using System.Collections.Generic;

namespace Metalama.Extensions.Multicast;

public abstract class OverrideFieldOrPropertyMulticastAspect : MulticastAspect, IAspect<IFieldOrProperty>
{
    protected OverrideFieldOrPropertyMulticastAspect() : base( MulticastTargets.Field | MulticastTargets.Property ) { }

    public void BuildEligibility( IEligibilityBuilder<IFieldOrProperty> builder )
    {
        this.BuildEligibility( builder.DeclaringType() );

        builder.ExceptForInheritance().MustBeNonAbstract();
        builder.MustBeExplicitlyDeclared();
        builder.MustSatisfy( d => d is not IField { Writeability: Writeability.None }, d => $"{d} must not be a constant" );
    }

    public void BuildAspect( IAspectBuilder<IFieldOrProperty> builder )
    {
        var getterTemplateSelector = new GetterTemplateSelector(
            "get_" + nameof(this.OverrideProperty),
            "get_" + nameof(this.OverrideEnumerableProperty),
            "get_" + nameof(this.OverrideEnumeratorProperty) );

        builder.Advice.OverrideAccessors( builder.Target, getterTemplateSelector, "set_" + nameof(this.OverrideProperty) );
    }

    [Template]
    public abstract dynamic? OverrideProperty
    {
        get;
        set;
    }

    [Template( IsEmpty = true )]
    public virtual IEnumerable<dynamic?> OverrideEnumerableProperty => throw new NotSupportedException();

    [Template( IsEmpty = true )]
    public virtual IEnumerator<dynamic?> OverrideEnumeratorProperty => throw new NotSupportedException();
}