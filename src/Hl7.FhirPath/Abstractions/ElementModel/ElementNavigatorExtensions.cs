﻿using System.Linq;
using System.Collections.Generic;
using System;

namespace Hl7.Fhir.ElementModel
{

    public static class ElementNavigatorExtensions
    {
        public static IEnumerable<IElementNavigator> Children(this IElementNavigator navigator)
        {
            var nav = navigator.Clone();
            if (nav.MoveToFirstChild())
            {
                do
                {
                    yield return nav.Clone();
                }
                while (nav.MoveToNext());
            }
        }

        public static bool HasChildren(this IElementNavigator navigator)
        {
            var nav = navigator.Clone();

            return nav.MoveToFirstChild();
        }

        public static IEnumerable<IElementNavigator> Descendants(this IElementNavigator element)
        {
            //TODO: Don't think this is performant with these nested yields
            foreach (var child in element.Children())
            {
                yield return child;
                foreach (var grandchild in child.Descendants()) yield return grandchild;
            }
        }

        public static IEnumerable<object> Values(this IElementNavigator navigator)
        {
            var nav = navigator.Clone();
            if (nav.MoveToFirstChild())
            {
                do
                {
                    yield return nav.Value;
                }
                while (nav.MoveToNext());
            }
        }

        public static IEnumerable<object> Values(this IElementNavigator navigator, Predicate<IElementNavigator> predicate)
        {
            var nav = navigator.Clone();
            if (nav.MoveToFirstChild())
            {
                do
                {
                    if (predicate(nav)) yield return nav.Value;
                }
                while (nav.MoveToNext());
            }
        }

        public static IEnumerable<object> Values(this IElementNavigator navigator, string name)
        {
            return navigator.Values(n => n.Name == name);
        }

        public static IEnumerable<object> ChildrenValues(this IEnumerable<IElementNavigator> navigators, string name)
        {
            return navigators.SelectMany(n => n.Values(name));
        }

        public static IEnumerable<string> GetChildNames(this IElementNavigator navigator)
        {
            return navigator.Children().Select(c => c.Name).Distinct();
        }

        public static IEnumerable<IElementNavigator> GetChildrenByName(this IElementNavigator navigator, string name)
        {
            return navigator.Children().Where(c => c.Name == name);
        }

        public static IEnumerable<IElementNavigator> GetChildrenByName(this IEnumerable<IElementNavigator> navigators, string name)
        {
            return navigators.SelectMany(n => n.Children().Where(c => c.Name == name));
        }

    }
}