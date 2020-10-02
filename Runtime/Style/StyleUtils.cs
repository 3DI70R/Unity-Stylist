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

        public delegate void StyleApplyFunc<S, T>(S style, UIReference<T> reference) 
            where T : MonoBehaviour;

        private static readonly StyleApplyFunc<ButtonStyleData, Button> applyButton = ApplyButtonStyle;
        private static readonly StyleApplyFunc<ImageStyleData, Image> applyImage = ApplyImageStyle;
        private static readonly StyleApplyFunc<TextStyleData, Text> applyText = ApplyTextStyle;
        private static readonly StyleApplyFunc<GraphicStyleData, Graphic> applyGraphic = ApplyGraphicStyle;
        private static readonly StyleApplyFunc<ToggleStyleData, Toggle> applyToggle = ApplyToggleStyle;
        private static readonly StyleApplyFunc<ScrollRectStyleData, ScrollRect> applyScrollRect = ApplyScrollRectStyle;
        private static readonly StyleApplyFunc<ScrollbarStyleData, Scrollbar> applyScrollBar = ApplyScrollbarStyle;

        public static void Apply(ButtonStyleData style, UIReference<Button> buttons) => 
            ApplyStyle(style, buttons, applyButton);
        
        public static void Apply(ImageStyleData style, UIReference<Image> images) => 
            ApplyStyle(style, images, applyImage);
        
        public static void Apply(TextStyleData style, UIReference<Text> texts) => 
            ApplyStyle(style, texts, applyText);
        
        public static void Apply(GraphicStyleData style, UIReference<Graphic> texts) => 
            ApplyStyle(style, texts, applyGraphic);
        
        public static void Apply(ToggleStyleData style, UIReference<Toggle> toggles) => 
            ApplyStyle(style, toggles, applyToggle);
        
        public static void Apply(ScrollRectStyleData style, UIReference<ScrollRect> scrolls) => 
            ApplyStyle(style, scrolls, applyScrollRect);
        
        public static void Apply(ScrollbarStyleData style, UIReference<Scrollbar> scrolls) => 
            ApplyStyle(style, scrolls, applyScrollBar);

        public static void ClearTrackedObjects()
        {
            visitedObjectsSet.Clear();
        }

        private static void ApplyGraphicStyle<T>(GraphicStyleData style, UIReference<T> reference)
            where T : Graphic
        {
            reference.value.color = style.color;
            reference.value.material = style.material;

            ApplyShadow(style, reference);
        }

        private static void ApplyImageStyle<T>(ImageStyleData style, UIReference<T> reference)
            where T : Image
        {
            reference.value.sprite = style.sprite;
            reference.value.preserveAspect = style.preserveAspect;
            reference.value.fillCenter = style.fillCenter;
            reference.value.useSpriteMesh = style.useSpriteMesh;
            reference.value.pixelsPerUnitMultiplier = style.pixelsPerUnitMultiplier;
            
            ApplyGraphicStyle(style, reference);
        }

        private static void ApplyTextStyle<T>(TextStyleData style, UIReference<T> reference)
            where T : Text
        {
            var fontValue = style.font.resolvedValue;
            
            if (fontValue)
                reference.value.font = fontValue;

            reference.value.fontStyle = style.fontStyle;
            reference.value.fontSize = style.fontSize;
            reference.value.lineSpacing = style.lineSpacing;
            reference.value.supportRichText = style.richText;
            reference.value.alignment = style.alignment;
            reference.value.alignByGeometry = style.alignByGeometry;
            reference.value.horizontalOverflow = style.horizontalOverflow;
            reference.value.verticalOverflow = style.verticalOverflow;
            reference.value.resizeTextForBestFit = style.bestFit;
            reference.value.resizeTextMinSize = style.bestFitMinSize;
            reference.value.resizeTextMaxSize = style.bestFitMaxSize;
            
            ApplyGraphicStyle(style, reference);
        }

        private static void ApplyButtonStyle<T>(ButtonStyleData style, UIReference<T> reference)
            where T : Button
        {
            ApplySelectableStyle(style, reference);
        }
        
        private static void ApplyScrollRectStyle<T>(ScrollRectStyleData style, UIReference<T> reference)
            where T : ScrollRect
        {
            reference.value.movementType = style.movementType;
            reference.value.elasticity = style.elasticity;
            reference.value.inertia = style.inertia;
            reference.value.decelerationRate = style.decelerationRate;
            reference.value.scrollSensitivity = style.scrollSensitivity;
        }

        private static void ApplyScrollbarStyle<T>(ScrollbarStyleData style, UIReference<T> reference)
            where T : Scrollbar
        {
            ApplySelectableStyle(style, reference);
        }

        private static void ApplyToggleStyle<T>(ToggleStyleData style, UIReference<T> reference)
            where T : Toggle
        {
            reference.value.toggleTransition = style.toggleTransition;
            
            ApplySelectableStyle(style, reference);
        }

        private static void ApplySelectableStyle<T>(SelectableStyleData style, UIReference<T> reference)
            where T : Selectable
        {
            reference.value.transition = style.transition;

            switch (reference.value.transition)
            {
                case Selectable.Transition.ColorTint:
                {
                    var colorBlock = reference.value.colors;
                    var originalBlock = colorBlock;
                    colorBlock.normalColor = style.colorNormal;
                    colorBlock.highlightedColor = style.colorHighlighted;
                    colorBlock.pressedColor = style.colorPressed;
                    colorBlock.selectedColor = style.colorSelected;
                    colorBlock.disabledColor = style.colorDisabled; 
                    colorBlock.colorMultiplier = style.colorMultiplier;
                    colorBlock.fadeDuration = style.colorFadeDuration;
                    
                    if(!colorBlock.Equals(originalBlock))
                        reference.value.colors = colorBlock;
                    
                    break;
                }
                case Selectable.Transition.SpriteSwap:
                {
                    var spriteBlock = reference.value.spriteState;
                    var originalBlock = spriteBlock;
                    spriteBlock.selectedSprite = style.spriteSelected;
                    spriteBlock.highlightedSprite = style.spriteHighlighted;
                    spriteBlock.pressedSprite = style.spritePressed;
                    spriteBlock.disabledSprite = style.spriteDisabled;
                    
                    if (!spriteBlock.Equals(originalBlock))
                        reference.value.spriteState = spriteBlock;
                    
                    break;
                }
            }
        }

        private static void ApplyShadow<T>(GraphicStyleData style, UIReference<T> reference)
            where T : Graphic
        {
            var gameObject = reference.value.gameObject;

            switch (style.shadowType.resolvedValue)
            {
                case GraphicStyleData.ShadowType.None:
                    if (reference.shadowComponent)
                    {
                        Object.DestroyImmediate(reference.shadowComponent);
                        reference.shadowComponent = null;
                    }

                    break;

                case GraphicStyleData.ShadowType.Shadow:
                    if (!reference.shadowComponent || reference.shadowComponent.GetType() != typeof(Shadow))
                    {
                        Object.DestroyImmediate(reference.shadowComponent);
                        reference.shadowComponent = gameObject.AddComponent<Shadow>();
                    }

                    break;

                case GraphicStyleData.ShadowType.Outline:
                    if (!reference.shadowComponent || reference.shadowComponent.GetType() != typeof(Outline))
                    {
                        Object.DestroyImmediate(reference.shadowComponent);
                        reference.shadowComponent = gameObject.AddComponent<Outline>();
                    }

                    break;
            }

            if (!reference.shadowComponent)
                return;
            
            // shadow marks layout as dirty on value update, don't update without changes
            if(reference.shadowComponent.effectColor != style.shadowColor) 
                reference.shadowComponent.effectColor = style.shadowColor;
            
            if(reference.shadowComponent.effectDistance != style.shadowDistance) 
                reference.shadowComponent.effectDistance = style.shadowDistance;

            if(reference.shadowComponent.useGraphicAlpha != style.shadowUseGraphicAlpha) 
                reference.shadowComponent.useGraphicAlpha = style.shadowUseGraphicAlpha;
        }

        private static void ApplyLayout<T>(ElementStyleData style, UIReference<T> behaviour)
            where T : MonoBehaviour
        {
            if (visitedObjectsSet.Add(style))
            {
                ApplyLayoutElement(style, behaviour);
                ApplyContentSizeFitter(style, behaviour);
            }
        }

        private static void ApplyLayoutElement<T>(ElementStyleData style, UIReference<T> reference)
            where T : MonoBehaviour
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

            if (UpdateComponent(ref reference.layoutElement, useLayout))
            {
                reference.layoutElement.minWidth = minWidth;
                reference.layoutElement.minHeight = minHeight;
                reference.layoutElement.preferredWidth = preferredWidth;
                reference.layoutElement.preferredHeight = preferredHeight;
                reference.layoutElement.flexibleWidth = flexibleWidth;
                reference.layoutElement.flexibleHeight = flexibleHeight;
            }
        }

        private static void ApplyContentSizeFitter<T>(ElementStyleData style, UIReference<T> reference)
            where T : MonoBehaviour
        {
            var horizontalFit = style.horizontalFit;
            var verticalFit = style.verticalFit;

            var useFitter = horizontalFit != ContentSizeFitter.FitMode.Unconstrained || 
                            verticalFit != ContentSizeFitter.FitMode.Unconstrained;
            
            if (UpdateComponent(ref reference.sizeFitter, useFitter))
            {
                reference.sizeFitter.horizontalFit = horizontalFit;
                reference.sizeFitter.verticalFit = verticalFit;
            }
        }

        private static bool UpdateComponent<T>(ref T behaviour, bool isActive)
            where T : Component
        {
            if (isActive)
            {
                if (!behaviour)
                    behaviour = behaviour.gameObject.AddComponent<T>();

                return true;
            }

            if (behaviour)
            {
                Object.DestroyImmediate(behaviour);
                behaviour = null;
            }
            
            return false;
        }

        private static void ApplyStyle<S, T>(S style, UIReference<T> data, StyleApplyFunc<S, T> apply)
            where S : ElementStyleData, new()
            where T : MonoBehaviour
        {
            if(!data.value)
                return;

            apply(style, data);
            ApplyLayout(style, data);
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