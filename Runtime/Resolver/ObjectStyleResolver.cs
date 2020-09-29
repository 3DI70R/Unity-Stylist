using System;
using System.Collections.Generic;
using UnityEngine;

namespace ThreeDISevenZeroR.Stylist
{
    /// <summary>
    /// Collects resolved type information for displaying in inspector
    /// </summary>
    public struct ObjectStyleResolver<T> 
        where T : new()
    {
        private readonly StyleResolver<T> resolver;
        private readonly List<ResolvedProperty> properties;
        private readonly HashSet<GameObject> visitedObjectSet;
        private MonoBehaviour currentBehavior;
        private string currentGroup;

        private ObjectStyleResolver(
            StyleResolver<T> resolver,
            List<ResolvedProperty> properties,
            HashSet<GameObject> objectSet,
            MonoBehaviour currentBehavior,
            string currentGroup)
        {
            this.resolver = resolver;
            this.properties = properties;
            this.visitedObjectSet = objectSet;
            this.currentBehavior = currentBehavior;
            this.currentGroup = currentGroup;
        }

        public ObjectStyleResolver(StyleResolver<T> resolver)
        {
            properties = new List<ResolvedProperty>();
            visitedObjectSet = new HashSet<GameObject>();
            
            this.resolver = resolver;
            this.currentBehavior = null;
            this.currentGroup = null;
        }

        public ObjectStyleResolver<TNew> As<TNew>()
            where TNew : new()
        {
            return new ObjectStyleResolver<TNew>(resolver.As<TNew>(), 
                properties, visitedObjectSet, currentBehavior, currentGroup);
        }

        public ObjectStyleResolver<TNew> As<TNew>(Func<T, StyleReference<TNew>> nestedStyleGetter)
            where TNew : ElementStyleData, new()
        {
            return new ObjectStyleResolver<TNew>(resolver.As(nestedStyleGetter), 
                properties, visitedObjectSet, currentBehavior, currentGroup);
        }
        
        public void Visit(MonoBehaviour behaviour, String name)
        {
            visitedObjectSet.Add(behaviour.gameObject);
            currentBehavior = behaviour;
            currentGroup = name;
        }

        public bool IsObjectVisited(MonoBehaviour b)
        {
            return visitedObjectSet.Add(b.gameObject);
        }

        public StyleProperty<P> Resolve<P>(String name, Func<T, StyleProperty<P>> propertyGetter)
        {
            var property = resolver.Resolve(propertyGetter);

            properties.Add(new ResolvedProperty
            {
                name = name,
                value = property.value.value,
                style = property.style,
                group = currentGroup,
                target = currentBehavior
            });

            return property.value;
        }

        public List<ResolvedProperty> GetResult() => properties;
    }
}