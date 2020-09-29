using System;
using UnityEngine;
using UnityEngine.UI;
using Button = UnityEngine.UI.Button;
using Image = UnityEngine.UI.Image;
using Object = UnityEngine.Object;
using Toggle = UnityEngine.UI.Toggle;

namespace ThreeDISevenZeroR.Stylist
{
    public static class StyleUtils
    {
        public static void Apply(this ObjectStyleResolver<ButtonStyleData> style, params Button[] buttons) => 
            ApplyStyle(style, buttons, ApplyButtonStyle);

        public static void Apply(this ObjectStyleResolver<ImageStyleData> style, params Image[] images) =>
            ApplyStyle(style, images, Apply);
        
        public static void Apply(this ObjectStyleResolver<TextStyleData> style, params Text[] texts) =>
            ApplyStyle(style, texts, ApplyTextStyle);
        
        public static void Apply(this ObjectStyleResolver<GraphicStyleData> style, params Graphic[] texts) =>
            ApplyStyle(style, texts, ApplyGraphicStyle);
        
        public static void Apply(this ObjectStyleResolver<ToggleStyleData> style, params Toggle[] toggles) =>
            ApplyStyle(style, toggles, ApplyToggleStyle);

        public static void Apply(this ObjectStyleResolver<ScrollRectStyleData> style, params ScrollRect[] scrolls) =>
            ApplyStyle(style, scrolls, ApplyScrollRectStyle);
        
        public static void Apply(this ObjectStyleResolver<ScrollbarStyleData> style, params Scrollbar[] scrolls) =>
            ApplyStyle(style, scrolls, ApplyScrollbarStyle);

        private static void ApplyGraphicStyle(this ObjectStyleResolver<GraphicStyleData> style, Graphic graphic)
        {
            style.Visit(graphic, "Graphic");
            
            graphic.color = style.Resolve("Color", d => d.color);
            graphic.material = style.Resolve("Material", d => d.material);

            ApplyShadow(style, graphic);
        }

        private static void Apply(ObjectStyleResolver<ImageStyleData> style, Image image)
        {
            style.Visit(image, "Image");
            
            image.sprite = style.Resolve("Sprite", d => d.sprite);
            image.preserveAspect = style.Resolve("Preserve Aspect", d => d.preserveAspect);
            image.fillCenter = style.Resolve("Fill Center", d => d.fillCenter);
            image.useSpriteMesh = style.Resolve("Use Sprite Mesh", d => d.useSpriteMesh);
            image.pixelsPerUnitMultiplier = style.Resolve("Pixels Per Unit Multiplier", d => d.pixelsPerUnitMultiplier);
            
            ApplyGraphicStyle(style.As<GraphicStyleData>(), image);
        }

        private static void ApplyTextStyle(ObjectStyleResolver<TextStyleData> style, Text text)
        {
            style.Visit(text, "Text");
            
            var fontValue = style.Resolve("Font", d => d.font);
            
            if (fontValue.value)
                text.font = fontValue;

            text.fontStyle = style.Resolve("Font Style", d => d.fontStyle);
            text.fontSize = style.Resolve("Font Size", d => d.fontSize);
            text.lineSpacing = style.Resolve("Line Spacing", d => d.lineSpacing);
            text.supportRichText = style.Resolve("Rich Text", d => d.richText);
            text.alignment = style.Resolve("Alignment", d => d.alignment);
            text.alignByGeometry = style.Resolve("Align by Geometry", d => d.alignByGeometry);
            text.horizontalOverflow = style.Resolve("Horizontal Overflow", d => d.horizontalOverflow);
            text.verticalOverflow = style.Resolve("Vertical Overflow", d => d.verticalOverflow);
            text.resizeTextForBestFit = style.Resolve("Best Fit", d => d.bestFit);
            text.resizeTextMinSize = style.Resolve("Min Size (Best fit)", d => d.bestFitMinSize);
            text.resizeTextMaxSize = style.Resolve("Max Size (Best fit)", d => d.bestFitMaxSize);
            
            ApplyGraphicStyle(style.As<GraphicStyleData>(), text);
        }

        private static void ApplyButtonStyle(ObjectStyleResolver<ButtonStyleData> style, Button button)
        {
            ApplySelectableStyle(style.As<SelectableStyleData>(), button);
        }
        
        private static void ApplyScrollRectStyle(ObjectStyleResolver<ScrollRectStyleData> style, ScrollRect scroll)
        {
            style.Visit(scroll, "ScrollRect");

            scroll.movementType = style.Resolve("Movement Type", d => d.movementType);
            scroll.elasticity = style.Resolve("Elasticity", d => d.elasticity);
            scroll.inertia = style.Resolve("Inertia", d => d.inertia);
            scroll.decelerationRate = style.Resolve("Deceleration Rate", d => d.decelerationRate);
            scroll.scrollSensitivity = style.Resolve("Scroll Sensitivity", d => d.scrollSensitivity);
        }

        private static void ApplyScrollbarStyle(ObjectStyleResolver<ScrollbarStyleData> style, Scrollbar scroll)
        {
            ApplySelectableStyle(style.As<SelectableStyleData>(), scroll);
        }

        private static void ApplyToggleStyle(ObjectStyleResolver<ToggleStyleData> style, Toggle toggle)
        {
            style.Visit(toggle, "Toggle");

            toggle.toggleTransition = style.Resolve("Toggle Transition", t => t.toggleTransition);
            
            ApplySelectableStyle(style.As<SelectableStyleData>(), toggle);
        }

        private static void ApplySelectableStyle(ObjectStyleResolver<SelectableStyleData> style, Selectable button)
        {
            style.Visit(button, "Selectable");

            var transition = style.Resolve("Transition", d => d.transition);
            button.transition = transition;

            switch (transition.value)
            {
                case Selectable.Transition.ColorTint:
                {
                    var colorBlock = button.colors;
                    colorBlock.normalColor = style.Resolve("Normal color", d => d.colorNormal);
                    colorBlock.highlightedColor = style.Resolve("Highlighted color", d => d.colorHighlighted);
                    colorBlock.pressedColor = style.Resolve("Pressed color", d => d.colorPressed);
                    colorBlock.selectedColor = style.Resolve("Selected color", d => d.colorSelected);
                    colorBlock.disabledColor = style.Resolve("Disabled color", d => d.colorDisabled); 
                    colorBlock.colorMultiplier = style.Resolve("Color multiplier", d => d.colorMultiplier);
                    colorBlock.fadeDuration = style.Resolve("Color fade duration", d => d.colorFadeDuration);
                    button.colors = colorBlock;
                    break;
                }
                case Selectable.Transition.SpriteSwap:
                {
                    var stateBlock = button.spriteState;
                    stateBlock.selectedSprite = style.Resolve("Selected sprite", d => d.spriteSelected);
                    stateBlock.highlightedSprite = style.Resolve("Highlighted sprite", d => d.spriteHighlighted);
                    stateBlock.pressedSprite = style.Resolve("Pressed sprite", d => d.spritePressed);
                    stateBlock.disabledSprite = style.Resolve("Disabled sprite", d => d.spriteDisabled);
                    button.spriteState = stateBlock;
                    break;
                }
            }
        }

        private static void ApplyShadow(ObjectStyleResolver<GraphicStyleData> style, Graphic graphicObject)
        {
            var gameObject = graphicObject.gameObject;
            var shadow = gameObject.GetComponent<Shadow>();
            
            style.Visit(graphicObject, "Shadow");

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

        private static void ApplyLayout(ObjectStyleResolver<ElementStyleData> style, MonoBehaviour behaviour)
        {
            ApplyLayoutElement(style, behaviour);
            ApplyContentSizeFitter(style, behaviour);
        }

        private static void ApplyLayoutElement(ObjectStyleResolver<ElementStyleData> style, MonoBehaviour behaviour)
        {
            style.Visit(behaviour, "Layout element");
            
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

        private static void ApplyContentSizeFitter(ObjectStyleResolver<ElementStyleData> style, MonoBehaviour behaviour)
        {
            style.Visit(behaviour, "Content size fitter");
            
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

        private static void ApplyStyle<S, T>(ObjectStyleResolver<S> style, 
            T[] data, Action<ObjectStyleResolver<S>, T> apply)
            where S : new()
            where T : MonoBehaviour
        {
            foreach (var d in data)
            {
                if(!d)
                    continue;

                var isVisited = style.IsObjectVisited(d);

                apply(style, d);

                if (!isVisited)
                    ApplyLayout(style.As<ElementStyleData>(), d);
            }
        }
    }
}