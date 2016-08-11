﻿/* 
 * Copyright (c) 2016, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hl7.Fhir.Specification.Navigation
{
    // [WMR 20160808] NEW

    public static class ElementNavigatorTypeExtensions
    {
        /// <summary>Enumerate all distinct element type profiles.</summary>
        /// <param name="nav">A <see cref="ElementDefinitionNavigator"/> instance.</param>
        /// <returns>A sequence of type profile resource references.</returns>
        public static IEnumerable<string> DistinctTypeProfiles(this ElementDefinitionNavigator nav)
        {
            return DistinctTypeProfiles(nav.Elements);
        }

        /// <summary>Enumerate all distinct element type profiles.</summary>
        /// <param name="elements">A <see cref="StructureDefinition.SnapshotComponent"/> or <see cref="StructureDefinition.DifferentialComponent"/> instance.</param>
        /// <returns>A sequence of type profile resource references.</returns>
        public static IEnumerable<string> DistinctTypeProfiles(this IElementList elements)
        {
            return DistinctTypeProfiles(elements.Element);
        }

        /// <summary>Enumerate all distinct element type profiles.</summary>
        /// <param name="elements">A list of <see cref="ElementDefinition"/> instances.</param>
        /// <returns>A sequence of type profile resource references.</returns>
        public static IEnumerable<string> DistinctTypeProfiles(this IList<ElementDefinition> elements)
        {
            var profiles = elements.SelectMany(e => e.Type).SelectMany(t => t.Profile);
            return profiles.OrderBy(p => p).Distinct();

        }
    }
}
