using System;
using System.Collections.Generic;
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
        private static readonly Stack<HashSet<object>> hashSetPool = new Stack<HashSet<object>>();
        private static readonly HashSet<object> visitedObjectsSet = new HashSet<object>();
        
        public static void Apply(ButtonStyleData style, params Button[] buttons) => 
            ApplyStyle(style, buttons, ApplyButtonStyle);

        public static void Apply(ImageStyleData style, params Image[] images) =>
            ApplyStyle(style, images, Apply);
        
        public static void Apply(TextStyleData style, params Text[] texts) =>
            ApplyStyle(style, texts, ApplyTextStyle);
        
        public static void Apply(GraphicStyleData style, params Graphic[] texts) =>
            ApplyStyle(style, texts, ApplyGraphicStyle);
        
        public static void Apply(ToggleStyleData style, params Toggle[] toggles) =>
            ApplyStyle(style, toggles, ApplyToggleStyle);

        public static void Apply(ScrollRectStyleData style, params ScrollRect[] scrolls) =>
            ApplyStyle(style, scrolls, ApplyScrollRectStyle);
        
        public static void Apply(ScrollbarStyleData style, params Scrollbar[] scrolls) =>
            ApplyStyle(style, scrolls, ApplyScrollbarStyle);

        public static void ClearTrackedObjects()
        {
            visitedObjectsSet.Clear();
        }

        private static void ApplyGraphicStyle(GraphicStyleData style, Graphic graphic)
        {
            graphic.color = style.color;
            graphic.material = style.material;

            ApplyShadow(style, graphic);
        }

        private static void Apply(ImageStyleData style, Image image)
        {
            image.sprite = style.sprite;
            image.preserveAspect = style.preserveAspect;
            image.fillCenter = style.fillCenter;
            image.useSpriteMesh = style.useSpriteMesh;
            image.pixelsPerUnitMultiplier = style.pixelsPerUnitMultiplier;
            
            ApplyGraphicStyle(style, image);
        }

        private static void ApplyTextStyle(TextStyleData style, Text text)
        {
            var fontValue = style.font.resolvedValue;
            
            if (fontValue)
                text.font = fontValue;

            text.fontStyle = style.fontStyle;
            text.fontSize = style.fontSize;
            text.lineSpacing = style.lineSpacing;
            text.supportRichText = style.richText;
            text.alignment = style.alignment;
            text.alignByGeometry = style.alignByGeometry;
            text.horizontalOverflow = style.horizontalOverflow;
            text.verticalOverflow = style.verticalOverflow;
            text.resizeTextForBestFit = style.bestFit;
            text.resizeTextMinSize = style.bestFitMinSize;
            text.resizeTextMaxSize = style.bestFitMaxSize;
            
            ApplyGraphicStyle(style, text);
        }

        private static void ApplyButtonStyle(ButtonStyleData style, Button button)
        {
            ApplySelectableStyle(style, button);
        }
        
        private static void ApplyScrollRectStyle(ScrollRectStyleData style, ScrollRect scroll)
        {
            scroll.movementType = style.movementType;
            scroll.elasticity = style.elasticity;
            scroll.inertia = style.inertia;
            scroll.decelerationRate = style.decelerationRate;
            scroll.scrollSensitivity = style.scrollSensitivity;
        }

        private static void ApplyScrollbarStyle(ScrollbarStyleData style, Scrollbar scroll)
        {
            ApplySelectableStyle(style, scroll);
        }

        private static void ApplyToggleStyle(ToggleStyleData style, Toggle toggle)
        {
            toggle.toggleTransition = style.toggleTransition;
            
            ApplySelectableStyle(style, toggle);
        }

        private static void ApplySelectableStyle(SelectableStyleData style, Selectable button)
        {
            button.transition = style.transition;

            switch (button.transition)
            {
                case Selectable.Transition.ColorTint:
                {
                    var colorBlock = button.colors;
                    var originalBlock = colorBlock;
                    colorBlock.normalColor = style.colorNormal;
                    colorBlock.highlightedColor = style.colorHighlighted;
                    colorBlock.pressedColor = style.colorPressed;
                    colorBlock.selectedColor = style.colorSelected;
                    colorBlock.disabledColor = style.colorDisabled; 
                    colorBlock.colorMultiplier = style.colorMultiplier;
                    colorBlock.fadeDuration = style.colorFadeDuration;
                    
                    if(!colorBlock.Equals(originalBlock))
                        button.colors = colorBlock;
                    
                    break;
                }
                case Selectable.Transition.SpriteSwap:
                {
                    var spriteBlock = button.spriteState;
                    var originalBlock = spriteBlock;
                    spriteBlock.selectedSprite = style.spriteSelected;
                    spriteBlock.highlightedSprite = style.spriteHighlighted;
                    spriteBlock.pressedSprite = style.spritePressed;
                    spriteBlock.disabledSprite = style.spriteDisabled;
                    
                    if (!spriteBlock.Equals(originalBlock))
                        button.spriteState = spriteBlock;
                    
                    break;
                }
            }
        }

        private static void ApplyShadow(GraphicStyleData style, Graphic graphicObject)
        {
            var gameObject = graphicObject.gameObject;
            var shadow = gameObject.GetComponent<Shadow>();

            switch (style.shadowType.resolvedValue)
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

            shadow.effectColor = style.shadowColor;
            shadow.effectDistance = style.shadowDistance;
        }

        private static void ApplyLayout(ElementStyleData style, MonoBehaviour behaviour)
        {
            if (visitedObjectsSet.Add(style))
            {
                ApplyLayoutElement(style, behaviour);
                ApplyContentSizeFitter(style, behaviour);
            }
        }

        private static void ApplyLayoutElement(ElementStyleData style, MonoBehaviour behaviour)
        {
            var minWidth = style.minWidth;
            var minHeight = style.minHeight;
            var preferredWidth = style.preferredWidth;
            var preferredHeight = style.preferredHeight;
            var flexibleWidth = style.flexibleWidth;
            var flexibleHeight = style.flexibleHeight;

            // LayoutElement uses -1 values as disabled
            var useLayout = minWidth >= 0 || minHeight >= 0 || 
                            preferredWidth >= 0 || preferredHeight >= 0 || 
                            flexibleWidth >= 0 || flexibleHeight >= 0;

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

        private static void ApplyContentSizeFitter(ElementStyleData style, MonoBehaviour behaviour)
        {
            var horizontalFit = style.horizontalFit;
            var verticalFit = style.verticalFit;

            var useFitter = horizontalFit != ContentSizeFitter.FitMode.Unconstrained || 
                            verticalFit != ContentSizeFitter.FitMode.Unconstrained;
            
            if (behaviour.UpdateComponent<ContentSizeFitter>(useFitter, out var fitter))
            {
                fitter.horizontalFit = horizontalFit;
                fitter.verticalFit = verticalFit;
            }
        }

        private static bool UpdateComponent<T>(this MonoBehaviour behaviour, bool isActive, out T component)
            where T : Component
        {
            behaviour.TryGetComponent(out component);

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

        private static void ApplyStyle<S, T>(S style, T[] data, Action<S, T> apply)
            where S : ElementStyleData, new()
            where T : MonoBehaviour
        {
            foreach (var d in data)
            {
                if(!d)
                    continue;

                apply(style, d);
                ApplyLayout(style, d);
            }
        }

        public static HashSet<object> GetPooledHashSet()
        {
            return (hashSetPool.Count > 0)
                ? hashSetPool.Pop()
                : new HashSet<object>();
        }

        public static void ReturnHashSet(HashSet<object> pooledHashSet)
        {
            pooledHashSet.Clear();
            hashSetPool.Push(pooledHashSet);
        }
    }
}