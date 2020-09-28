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

        public StyleResolver<TNew> As<TNew>()
            where TNew : new()
        {
            return new StyleResolver<TNew>(dataEnumerator);
        }

        public StyleResolver<TNew> As<TNew>(Func<T, StyleReference<TNew>> nestedStyleGetter)
            where TNew : ElementStyleData, new()
        {
            var currentEnumerator = dataEnumerator;

            return new StyleResolver<TNew>(enumerator => currentEnumerator((asset, data) =>
            {
                if (!(data is T tData)) 
                    return false;
                
                var style = nestedStyleGetter(tData);
                return EnumerateStyle(asset, style.overrides, style.inherits, enumerator);
            }));
        }

        public PropertyInfo<P> Resolve<P>(Func<T, StyleProperty<P>> propertyGetter)
        {
            StyleProperty<P> resolvedValue = default;
            ElementStyle resolvedAsset = null;
            var isResolved = false;

            dataEnumerator((asset, data) =>
            {
                if (data is T tData)
                {
                    var property = propertyGetter(tData);

                    switch (property.type)
                    {
                        case PropertyApplyType.Assigned:
                            resolvedValue = property;
                            resolvedAsset = asset;
                            isResolved = true;
                            return true;
                        
                        case PropertyApplyType.Clear:
                            return true;
                    }
                }

                return false;
            });

            return new PropertyInfo<P>
            {
                value = isResolved ? resolvedValue : propertyGetter(defaultInstance),
                style = resolvedAsset,
            };
        }

        private static bool EnumerateStyle(ElementStyle asset, object overrides, ElementStyle[] inherits,
            Func<ElementStyle, object, bool> enumerator)
        {
            if (enumerator(asset, overrides))
                return true;

            for (var i = inherits.Length - 1; i >= 0; i--)
            {
                var style = inherits[i];

                if (EnumerateStyle(style, style.Overrides, style.Inherits, enumerator))
                    return true;
            }

            return false;
        }
    }
}