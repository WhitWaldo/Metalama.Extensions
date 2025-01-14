﻿// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this repository root for details.

using JetBrains.Annotations;
using Metalama.Extensions.Architecture.Predicates;
using Metalama.Extensions.Architecture.Validators;
using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using Metalama.Framework.Eligibility;
using System.Linq;

namespace Metalama.Extensions.Architecture.Aspects;

/// <summary>
/// Aspect that, when applied to a public type, reports a warning whenever any member of the target type is used from
/// from a different type than the ones specified by the <see cref="BaseUsageValidationAttribute.Types"/>, <see cref="BaseUsageValidationAttribute.Namespaces"/>,
/// <see cref="BaseUsageValidationAttribute.NamespaceOfTypes"/> or <see cref="BaseUsageValidationAttribute.CurrentNamespace"/> properties.
/// </summary>
[PublicAPI]
public class InternalsCanOnlyBeUsedFromAttribute : BaseUsageValidationAttribute, IAspect<INamedType>
{
    public void BuildAspect( IAspectBuilder<INamedType> builder )
    {
        if ( !this.TryCreatePredicate( builder, out var predicate ) )
        {
            return;
        }

        var validator = new ReferencePredicateValidator(
            new OrPredicate( new HasFamilyAccessPredicate(), predicate ),
            this.Description,
            this.ReferenceKinds );

        // Register a validator for all internal members.
        builder.Outbound.SelectMany(
                t => t.Members().Where( m => m.Accessibility is Accessibility.Internal or Accessibility.PrivateProtected or Accessibility.ProtectedInternal ) )
            .ValidateReferences( validator );
    }

    public void BuildEligibility( IEligibilityBuilder<INamedType> builder ) => builder.MustHaveAccessibility( Accessibility.Public );
}