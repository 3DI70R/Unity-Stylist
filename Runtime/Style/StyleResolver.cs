using System;

namespace ThreeDISevenZeroR.Stylist
{
    public readonly struct StyleResolver<T>
        where T : new()
    {
        private static readonly T defaultInstance = new T();
        
        private readonly Func<Func<ElementStyle, object, bool>, bool> dataEnumerator;

        private StyleResolver(Func<Func<ElementStyle, object, bool>, bool> enumerator)
        {
            dataEnumerator = enumerator;
        }

        public StyleResolver(ElementStyle style)
        {
            dataEnumerator = e => EnumerateStyle(style, style.Overrides, style.Inherits, e);
        }

        public StyleResolver<TNew> ForType<TNew>()
            where TNew : new()
        {
            return new StyleResolver<TNew>(dataEnumerator);
        }

        public void Resolve<TNew>(ref StyleReference<TNew> styleToResolve, Func<T, StyleReference<TNew>> nestedStyleGetter)
            where TNew : ElementStyleData, new()
        {
            var currentEnumerator = dataEnumerator;
            var innerResolver = new StyleResolver<ElementStyleData>(enumerator => currentEnumerator((asset, data) =>
            {
                if (data is T tData)
                {
                    var style = nestedStyleGetter(tData);
                    return EnumerateStyle(asset, style.overrides, style.inherits.array, enumerator);
                }
                
                return false;
            }));
            
            if(styleToResolve.overrides == null)
                styleToResolve.overrides = new TNew();

            styleToResolve.overrides.Resolve(innerResolver);
        }

        public void Resolve<P>(ref StyleProperty<P> propertyToResolve, Func<T, StyleProperty<P>> propertyGetter)
        {
            StyleProperty<P> resolvedValue = default;
            ElementStyle resolvedAsset = null;
            var isAssigned = false;

            dataEnumerator((asset, data) =>
            {
                if (data is T tData)
                {
                    var property = propertyGetter(tData);

                    switch (property.applyType)
                    {
                        case PropertyApplyType.Assigned:
                            resolvedValue = property;
                            resolvedAsset = asset;
                            isAssigned = true;
                            return true;
                        
                        case PropertyApplyType.Clear:
                            return true;
                    }
                }

                return false;
            });
            
            propertyToResolve.resolvedValue = isAssigned 
                ? resolvedValue.ownValue 
                : propertyGetter(defaultInstance).ownValue;
            
            propertyToResolve.resolvedAsset = resolvedAsset;
        }

        private static bool EnumerateStyle(ElementStyle asset, object overrides, ElementStyle[] inherits,
            Func<ElementStyle, object, bool> enumerator)
        {
            var visitedObjectsSet = StyleUtils.GetPooledHashSet();

            try
            {
                bool EnumerateRecursive(ElementStyle innerAsset, object innerOverrides, ElementStyle[] innerInherits)
                {
                    if (!visitedObjectsSet.Add(innerAsset))
                        return false;

                    if (innerOverrides != null && enumerator(innerAsset, innerOverrides))
                        return true;

                    if (innerInherits == null)
                        return false;

                    for (var i = innerInherits.Length - 1; i >= 0; i--)
                    {
                        var style = innerInherits[i];

                        if (!style)
                            continue;

                        if (EnumerateRecursive(style, style.Overrides, style.Inherits))
                            return true;
                    }

                    return false;
                }

                EnumerateRecursive(asset, overrides, inherits);
            }
            finally
            {
                StyleUtils.ReturnHashSet(visitedObjectsSet);
            }

            return false;
        }
    }
}