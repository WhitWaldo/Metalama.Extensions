﻿// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this repository root for details.

using Metalama.Framework.Aspects;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace Metalama.Extensions.Architecture.Predicates;

/// <summary>
/// Extension methods for <see cref="ReferencePredicate"/> and <see cref="ReferencePredicateBuilder"/>.
/// </summary>
[CompileTime]
public static class ReferencePredicateExtensions
{
    /// <summary>
    /// Combines two predicates with the <c>or</c> condition. This overload accepts the second predicate as a delegate.
    /// </summary>
    public static ReferencePredicate Or( this ReferencePredicate predicate, Func<ReferencePredicateBuilder, ReferencePredicate> otherPredicate )
    {
        if ( predicate.Builder == null )
        {
            throw new InvalidOperationException( "No ReferencePredicateBuilder available." );
        }

        return new OrPredicate( predicate, otherPredicate( predicate.Builder ) );
    }

    /// <summary>
    /// Combines two predicates with the <c>or</c> condition. This overload accepts the second predicate as a <see cref="ReferencePredicate"/>.
    /// </summary>
    public static ReferencePredicate Or( this ReferencePredicate predicate, ReferencePredicate otherPredicate ) => new OrPredicate( predicate, otherPredicate );

    /// <summary>
    /// Combines two predicates with the <c>and</c> condition. This overload accepts the second predicate as a delegate.
    /// </summary>
    public static ReferencePredicate And( this ReferencePredicate predicate, Func<ReferencePredicateBuilder, ReferencePredicate> otherPredicate )
    {
        if ( predicate.Builder == null )
        {
            throw new InvalidOperationException( "No ReferencePredicateBuilder available." );
        }

        return new AndPredicate( predicate, otherPredicate( predicate.Builder ) );
    }

    /// <summary>
    /// Combines two predicates with the <c>and</c> condition. This overload accepts the second predicate as a <see cref="ReferencePredicate"/>.
    /// </summary>
    public static ReferencePredicate And( this ReferencePredicate predicate, ReferencePredicate otherPredicate )
        => new AndPredicate( predicate, otherPredicate );

    /// <summary>
    /// Inverts the given predicate.
    /// </summary>
    public static ReferencePredicate Not( this ReferencePredicate predicate ) => new NotPredicate( predicate );

    /// <summary>
    /// Returns a predicate that always evaluate to <c>true</c>.
    /// </summary>
    public static ReferencePredicate Always( this ReferencePredicateBuilder builder ) => new AlwaysPredicate( builder );

    /// <summary>
    /// Combines several other predicates with the <c>or</c> condition.
    /// </summary>
    public static ReferencePredicate Any( this ReferencePredicateBuilder builder, Func<ReferencePredicateBuilder, IEnumerable<ReferencePredicate>> predicates )
        => new AnyPredicate( predicates( builder ).ToImmutableArray(), builder );

    /// <summary>
    /// Accepts code references contained in a given namespace.
    /// </summary>
    public static ReferencePredicate Namespace( this ReferencePredicateBuilder builder, string ns ) => new ReferencingNamespacePredicate( ns, builder );

    /// <summary>
    /// Accepts code references contained in the current namespace.
    /// </summary>
    public static ReferencePredicate CurrentNamespace( this ReferencePredicateBuilder builder )
    {
        if ( builder.Namespace == null )
        {
            throw new InvalidOperationException( "There is no namespace in the current context." );
        }

        return new ReferencingNamespacePredicate( builder.Namespace, builder );
    }

    /// <summary>
    /// Accepts code references contained in the namespace of a given type.
    /// </summary>
    public static ReferencePredicate NamespaceOf( this ReferencePredicateBuilder builder, Type type )
        => new ReferencingNamespacePredicate(
            type.Namespace ?? throw new ArgumentOutOfRangeException( nameof(type), $"The type {type.FullName} has no namespace." ),
            builder );

    /// <summary>
    /// Accepts code references contained in a given type.
    /// </summary>
    public static ReferencePredicate Type( this ReferencePredicateBuilder builder, Type type ) => new ReferencingTypePredicate( type, builder );

    /// <summary>
    /// Accepts code references that are legitimate based on family access rules, but rejects code references that are legitimate according to other rules.
    /// </summary>
    public static ReferencePredicate HasFamilyAccess( this ReferencePredicateBuilder builder ) => new HasFamilyAccessPredicate( builder );
}