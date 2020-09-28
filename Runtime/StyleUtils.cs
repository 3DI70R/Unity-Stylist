using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace ThreeDISevenZeroR.Stylist
{
    public static class StyleUtils
    {
        public static List<ResolvedStyle> ApplyStyle(LayoutClaimer claimer, 
            StyleResolver<ButtonStyleData> style, params Button[] buttons) => 
            ApplyStyle(claimer, style, buttons, ApplyButtonStyle);

        public static List<ResolvedStyle> ApplyStyle(LayoutClaimer claimer, 
            StyleResolver<ImageStyleData> style, params Image[] images) =>
            ApplyStyle(claimer, style, images, ApplyImageStyle);
        
        public static List<ResolvedStyle> ApplyStyle(LayoutClaimer claimer, 
            StyleResolver<TextStyleData> style, params Text[] texts) =>
            ApplyStyle(claimer, style, texts, ApplyTextStyle);
        
        public static List<ResolvedStyle> ApplyStyle(LayoutClaimer claimer, 
            StyleResolver<GraphicStyleData> style, params Graphic[] texts) =>
            ApplyStyle(claimer, style, texts, ApplyGraphicStyle);

        private static void ApplyGraphicStyle(LoggingResolver<GraphicStyleData> style, Graphic graphic)
        {
            graphic.color = style.Resolve(d => d.color);
            graphic.material = style.Resolve(d => d.material);

            ApplyShadow(style, graphic);
        }

        private static void ApplyImageStyle(LoggingResolver<ImageStyleData> style, Image image)
        {
            image.sprite = style.Resolve(d => d.sprite);
            
            ApplyGraphicStyle(style.As<GraphicStyleData>(), image);
        }

        private static void ApplyTextStyle(LoggingResolver<TextStyleData> style, Text text)
        {
            var fontValue = style.Resolve(d => d.font);
            
            if (fontValue.value)
                text.font = fontValue;

            text.fontStyle = style.Resolve(d => d.fontStyle);
            text.fontSize = style.Resolve(d => d.fontSize);
            text.lineSpacing = style.Resolve(d => d.lineSpacing);
            text.supportRichText = style.Resolve(d => d.richText);
            text.alignment = style.Resolve(d => d.alignment);
            text.alignByGeometry = style.Resolve(d => d.alignByGeometry);
            text.horizontalOverflow = style.Resolve(d => d.horizontalOverflow);
            text.verticalOverflow = style.Resolve(d => d.verticalOverflow);
            text.resizeTextForBestFit = style.Resolve(d => d.bestFit);
            text.resizeTextMinSize = style.Resolve(d => d.bestFitMinSize);
            text.resizeTextMaxSize = style.Resolve(d => d.bestFitMaxSize);
            
            ApplyGraphicStyle(style.As<GraphicStyleData>(), text);
        }

        private static void ApplyButtonStyle(LoggingResolver<ButtonStyleData> style, Button button)
        {
            ApplySelectableStyle(style.As<SelectableStyleData>(), button);
        }

        private static void ApplySelectableStyle(LoggingResolver<SelectableStyleData> style, Selectable button)
        {
            var colorBlock = button.colors;
            colorBlock.normalColor = style.Resolve(d => d.colorNormal);
            colorBlock.highlightedColor = style.Resolve(d => d.colorHighlighted);
            colorBlock.pressedColor = style.Resolve(d => d.colorPressed);
            colorBlock.selectedColor = style.Resolve(d => d.colorSelected);
            colorBlock.disabledColor = style.Resolve(d => d.colorDisabled); 
            colorBlock.colorMultiplier = style.Resolve(d => d.colorMultiplier);
            colorBlock.fadeDuration = style.Resolve(d => d.colorFadeDuration);
            button.colors = colorBlock;
        }

        private static void ApplyShadow(LoggingResolver<GraphicStyleData> style, Graphic graphicObject)
        {
            var gameObject = graphicObject.gameObject;
            var shadow = gameObject.GetComponent<Shadow>();

            switch (style.Resolve(d => d.shadowType).value)
            {
                case GraphicStyleData.ShadowType.None:
                    if (shadow)
                    {
                        Object.DestroyImmediate(shadow);
                        shadow = null;
                    }

                    break;

                case GraphicStyleData.ShadowType.Shadow:
                    if (!shadow || shadow.GetType() != typeof(Shadow))
                    {
                        Object.DestroyImmediate(shadow);
                        shadow = gameObject.AddComponent<Shadow>();
                    }

                    break;

                case GraphicStyleData.ShadowType.Outline:
                    if (!shadow || shadow.GetType() != typeof(Outline))
                    {
                        Object.DestroyImmediate(shadow);
                        shadow = gameObject.AddComponent<Outline>();
                    }

                    break;
            }

            if (!shadow)
                return;

            shadow.effectColor = style.Resolve(d => d.shadowColor);
            shadow.effectDistance = style.Resolve(d => d.shadowDistance);
        }

        private static void ApplyLayout(LoggingResolver<ElementStyleData> style, MonoBehaviour behaviour)
        {
            ApplyLayoutElement(style, behaviour);
            ApplyContentSizeFitter(style, behaviour);
        }

        private static void ApplyLayoutElement(LoggingResolver<ElementStyleData> style, MonoBehaviour behaviour)
        {
            var minWidth = style.Resolve(d => d.layout.minWidth);
            var minHeight = style.Resolve(d => d.layout.minHeight);
            var preferredWidth = style.Resolve(d => d.layout.preferredWidth);
            var preferredHeight = style.Resolve(d => d.layout.preferredHeight);
            var flexibleWidth = style.Resolve(d => d.layout.flexibleWidth);
            var flexibleHeight = style.Resolve(d => d.layout.flexibleHeight);

            var useLayout = minWidth.IsAssigned || minHeight.IsAssigned || 
                            preferredWidth.IsAssigned || preferredHeight.IsAssigned || 
                            flexibleWidth.IsAssigned || flexibleHeight.IsAssigned;

            if (behaviour.UpdateComponent<LayoutElement>(useLayout, out var layoutElement))
            {
                layoutElement.minWidth = minWidth;
                layoutElement.minHeight = minHeight;
                layoutElement.preferredWidth = preferredWidth;
                layoutElement.preferredHeight = preferredHeight;
                layoutElement.flexibleWidth = flexibleWidth;
                layoutElement.flexibleHeight = flexibleHeight;
            }
        }

        private static void ApplyContentSizeFitter(LoggingResolver<ElementStyleData> style, MonoBehaviour behaviour)
        {
            var horizontalFit = style.Resolve(d => d.layout.horizontalFit);
            var verticalFit = style.Resolve(d => d.layout.verticalFit);

            var useFitter = horizontalFit.IsAssigned || verticalFit.IsAssigned;
            
            if (behaviour.UpdateComponent<ContentSizeFitter>(useFitter, out var fitter))
            {
                fitter.horizontalFit = horizontalFit;
                fitter.verticalFit = verticalFit;
            }
        }

        private static bool UpdateComponent<T>(this MonoBehaviour behaviour, bool isActive, out T component)
            where T : Component
        {
            component = behaviour.GetComponent<T>();

            if (isActive)
            {
                if (!component)
                    component = behaviour.gameObject.AddComponent<T>();

                return true;
            }

            Object.DestroyImmediate(component);
            component = null;
            return false;
        }
        
        // -- Resolver logic --------------------

        public static void AddTo(this List<ResolvedStyle> styles, List<ResolvedStyle> list)
        {
            list.AddRange(styles);
        }
        
        public static StyleResolver<T> CreateResolver<T>(this ElementStyle style) 
            where T : new()
        {
            return new StyleResolver<object>(style).As<T>();
        }

        private static LoggingResolver<T> Log<T>(this StyleResolver<T> resolver, MonoBehaviour target)
            where T : new()
        {
            return new LoggingResolver<T>(resolver, target);
        }
        
        private static List<ResolvedStyle> ApplyStyle<S, T>(LayoutClaimer claimer, StyleResolver<S> style, 
            T[] data, Action<LoggingResolver<S>, T> apply)
            where S : new()
            where T : MonoBehaviour
        {
            var result = new List<ResolvedStyle>();

            foreach (var d in data)
            {
                if(!d)
                    continue;
                
                var logger = style.Log(d);
                apply(logger, d);

                if (claimer.ClaimLayout(d))
                    ApplyLayout(logger.As<ElementStyleData>(), d);

                result.Add(logger.GetResult());
            }

            return result;
        }

        /// <summary>
        /// Tracks used game objects, so Image doesn't overwrite Button's layout settings if placed in same hierarchy
        /// </summary>
        public class LayoutClaimer
        {
            private readonly HashSet<GameObject> behaviorSet 
                = new HashSet<GameObject>();
            
            public bool ClaimLayout(MonoBehaviour b)
            {
                return behaviorSet.Add(b.gameObject);
            }
        }

        private readonly struct LoggingResolver<T>
            where T : new()
        {
            private readonly StyleResolver<T> resolver;
            private readonly List<ResolvedProperty> properties;
            private readonly MonoBehaviour behaviour;

            private LoggingResolver(StyleResolver<T> resolver, 
                List<ResolvedProperty> properties, 
                MonoBehaviour target)
            {
                this.resolver = resolver;
                this.properties = properties;
                this.behaviour = target;
            }
            
            public LoggingResolver(StyleResolver<T> resolver, MonoBehaviour behaviour)
            {
                this.resolver = resolver;
                this.behaviour = behaviour;
                properties = new List<ResolvedProperty>();
            }

            public LoggingResolver<TNew> As<TNew>() where TNew : new() => 
                new LoggingResolver<TNew>(resolver.As<TNew>(), properties, behaviour);

            public StyleProperty<P> Resolve<P>(Expression<Func<T, StyleProperty<P>>> propertyGetter)
            {
                var memberName = (propertyGetter.Body is MemberExpression memberExpression)
                    ? memberExpression.Member.Name
                    : "unknown";

                var property = resolver.Resolve(propertyGetter.Compile());
                properties.Add(new ResolvedProperty
                {
                    name = memberName,
                    value = property.value.value,
                    style = property.style
                });
                
                return property.value;
            }

            public ResolvedStyle GetResult() => new ResolvedStyle
            {
                target = behaviour,
                properties = properties
            };
        }
    }
}