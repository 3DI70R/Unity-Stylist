using System;
using System.Collections.Generic;
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
            style.BeginGroup("Graphic");
            
            graphic.color = style.Resolve("Color", d => d.color);
            graphic.material = style.Resolve("Material", d => d.material);

            ApplyShadow(style, graphic);
        }

        private static void ApplyImageStyle(LoggingResolver<ImageStyleData> style, Image image)
        {
            style.BeginGroup("Image");
            
            image.sprite = style.Resolve("Sprite", d => d.sprite);
            
            ApplyGraphicStyle(style.As<GraphicStyleData>(), image);
        }

        private static void ApplyTextStyle(LoggingResolver<TextStyleData> style, Text text)
        {
            style.BeginGroup("Text");
            
            var fontValue = style.Resolve("Font", d => d.font);
            
            if (fontValue.value)
                text.font = fontValue;

            text.fontStyle = style.Resolve("Font style", d => d.fontStyle);
            text.fontSize = style.Resolve("Font size", d => d.fontSize);
            text.lineSpacing = style.Resolve("Line spacing", d => d.lineSpacing);
            text.supportRichText = style.Resolve("Rich text", d => d.richText);
            text.alignment = style.Resolve("Alignment", d => d.alignment);
            text.alignByGeometry = style.Resolve("Align by geometry", d => d.alignByGeometry);
            text.horizontalOverflow = style.Resolve("Horizontal overflow", d => d.horizontalOverflow);
            text.verticalOverflow = style.Resolve("Vertical overflow", d => d.verticalOverflow);
            text.resizeTextForBestFit = style.Resolve("Best fit", d => d.bestFit);
            text.resizeTextMinSize = style.Resolve("Min size (Best fit)", d => d.bestFitMinSize);
            text.resizeTextMaxSize = style.Resolve("Max size (Best fit)", d => d.bestFitMaxSize);
            
            ApplyGraphicStyle(style.As<GraphicStyleData>(), text);
        }

        private static void ApplyButtonStyle(LoggingResolver<ButtonStyleData> style, Button button)
        {
            ApplySelectableStyle(style.As<SelectableStyleData>(), button);
        }

        private static void ApplySelectableStyle(LoggingResolver<SelectableStyleData> style, Selectable button)
        {
            style.BeginGroup("Color block");
            
            var colorBlock = button.colors;
            colorBlock.normalColor = style.Resolve("Normal color", d => d.colorNormal);
            colorBlock.highlightedColor = style.Resolve("Highlighted color", d => d.colorHighlighted);
            colorBlock.pressedColor = style.Resolve("Pressed color", d => d.colorPressed);
            colorBlock.selectedColor = style.Resolve("Selected color", d => d.colorSelected);
            colorBlock.disabledColor = style.Resolve("Disabled color", d => d.colorDisabled); 
            colorBlock.colorMultiplier = style.Resolve("Color multiplier", d => d.colorMultiplier);
            colorBlock.fadeDuration = style.Resolve("Color fade duration", d => d.colorFadeDuration);
            button.colors = colorBlock;
        }

        private static void ApplyShadow(LoggingResolver<GraphicStyleData> style, Graphic graphicObject)
        {
            var gameObject = graphicObject.gameObject;
            var shadow = gameObject.GetComponent<Shadow>();
            
            style.BeginGroup("Shadow");

            switch (style.Resolve("Shadow type", d => d.shadowType).value)
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

            shadow.effectColor = style.Resolve("Shadow color", d => d.shadowColor);
            shadow.effectDistance = style.Resolve("Shadow distance", d => d.shadowDistance);
        }

        private static void ApplyLayout(LoggingResolver<ElementStyleData> style, MonoBehaviour behaviour)
        {
            ApplyLayoutElement(style, behaviour);
            ApplyContentSizeFitter(style, behaviour);
        }

        private static void ApplyLayoutElement(LoggingResolver<ElementStyleData> style, MonoBehaviour behaviour)
        {
            style.BeginGroup("Layout element");
            
            var minWidth = style.Resolve("Min width", d => d.layout.minWidth);
            var minHeight = style.Resolve("Min height", d => d.layout.minHeight);
            var preferredWidth = style.Resolve("Preferred width", d => d.layout.preferredWidth);
            var preferredHeight = style.Resolve("Preferred height", d => d.layout.preferredHeight);
            var flexibleWidth = style.Resolve("Flexible width", d => d.layout.flexibleWidth);
            var flexibleHeight = style.Resolve("Flexible height", d => d.layout.flexibleHeight);

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
            style.BeginGroup("Content size fitter");
            
            var horizontalFit = style.Resolve("Horizontal fit", d => d.layout.horizontalFit);
            var verticalFit = style.Resolve("Vertical fit", d => d.layout.verticalFit);

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

        private struct LoggingResolver<T>
            where T : new()
        {
            private readonly StyleResolver<T> resolver;
            private readonly List<ResolvedProperty> properties;
            private readonly MonoBehaviour behaviour;
            private string currentGroup;

            private LoggingResolver(StyleResolver<T> resolver, 
                List<ResolvedProperty> properties, 
                MonoBehaviour target,
                string currentGroup)
            {
                this.resolver = resolver;
                this.properties = properties;
                this.behaviour = target;
                this.currentGroup = currentGroup;
            }
            
            public LoggingResolver(StyleResolver<T> resolver, MonoBehaviour behaviour)
            {
                this.resolver = resolver;
                this.behaviour = behaviour;
                properties = new List<ResolvedProperty>();
                this.currentGroup = null;
            }

            public LoggingResolver<TNew> As<TNew>() where TNew : new() => 
                new LoggingResolver<TNew>(resolver.As<TNew>(), properties, behaviour, currentGroup);

            public void BeginGroup(string name)
            {
                currentGroup = name;
            }
            
            public StyleProperty<P> Resolve<P>(String name, Func<T, StyleProperty<P>> propertyGetter)
            {
                var property = resolver.Resolve(propertyGetter);
                properties.Add(new ResolvedProperty
                {
                    name = name,
                    group = currentGroup,
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