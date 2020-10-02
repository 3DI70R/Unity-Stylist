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

        private delegate void StyleApplyFunc<S, T>(S style, UIReference<T> reference) 
            where T : MonoBehaviour;

        private static readonly StyleApplyFunc<ButtonStyleData, Button> applyButton = ApplyButtonStyle;
        private static readonly StyleApplyFunc<ImageStyleData, Image> applyImage = ApplyImageStyle;
        private static readonly StyleApplyFunc<TextStyleData, Text> applyText = ApplyTextStyle;
        private static readonly StyleApplyFunc<GraphicStyleData, Graphic> applyGraphic = ApplyGraphicStyle;
        private static readonly StyleApplyFunc<ToggleStyleData, Toggle> applyToggle = ApplyToggleStyle;
        private static readonly StyleApplyFunc<ScrollRectStyleData, ScrollRect> applyScrollRect = ApplyScrollRectStyle;
        private static readonly StyleApplyFunc<ScrollbarStyleData, Scrollbar> applyScrollBar = ApplyScrollbarStyle;
        private static readonly StyleApplyFunc<InputFieldStyleData, InputField> applyInputField = ApplyInputFieldStyle;
        private static readonly StyleApplyFunc<SliderStyleData, Slider> applySlider = ApplySliderStyle;
        private static readonly StyleApplyFunc<DropdownStyleData, Dropdown> applyDropdown = ApplyDropdownStyle;

        public static void Apply(ButtonStyleData style, UIReference<Button> button) => 
            ApplyStyle(style, button, applyButton);
        
        public static void Apply(ImageStyleData style, UIReference<Image> image) => 
            ApplyStyle(style, image, applyImage);
        
        public static void Apply(TextStyleData style, UIReference<Text> text) => 
            ApplyStyle(style, text, applyText);
        
        public static void Apply(GraphicStyleData style, UIReference<Graphic> graphic) => 
            ApplyStyle(style, graphic, applyGraphic);
        
        public static void Apply(ToggleStyleData style, UIReference<Toggle> toggle) => 
            ApplyStyle(style, toggle, applyToggle);
        
        public static void Apply(ScrollRectStyleData style, UIReference<ScrollRect> scroll) => 
            ApplyStyle(style, scroll, applyScrollRect);
        
        public static void Apply(ScrollbarStyleData style, UIReference<Scrollbar> scroll) => 
            ApplyStyle(style, scroll, applyScrollBar);

        public static void Apply(InputFieldStyleData style, UIReference<InputField> field) =>
            ApplyStyle(style, field, applyInputField);
        
        public static void Apply(SliderStyleData style, UIReference<Slider> field) =>
            ApplyStyle(style, field, applySlider);
        
        public static void Apply(DropdownStyleData style, UIReference<Dropdown> field) =>
            ApplyStyle(style, field, applyDropdown);

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
        
        private static void ApplyInputFieldStyle<T>(InputFieldStyleData style, UIReference<T> reference)
            where T : InputField
        {
            reference.value.caretBlinkRate = style.caretBlinkRate;
            reference.value.caretWidth = style.caretWidth;
            reference.value.selectionColor = style.selectionColor;

            // use custom caret color if property set
            reference.value.customCaretColor = style.caretColor.IsResolved;
            
            ApplySelectableStyle(style, reference);
        }

        private static void ApplySliderStyle<T>(SliderStyleData style, UIReference<T> reference)
            where T : Slider
        {
            ApplySelectableStyle(style, reference);
        }
        
        private static void ApplyDropdownStyle<T>(DropdownStyleData style, UIReference<T> reference)
            where T : Dropdown
        {
            reference.value.alphaFadeSpeed = style.alphaFadeSpeed;
            
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
                    if (reference.shadow)
                    {
                        Destroy(reference.shadow);
                        reference.shadow = null;
                    }

                    break;

                case GraphicStyleData.ShadowType.Shadow:
                    if (!reference.shadow || reference.shadow.GetType() != typeof(Shadow))
                    {
                        Destroy(reference.shadow);
                        reference.shadow = gameObject.AddComponent<Shadow>();
                    }

                    break;

                case GraphicStyleData.ShadowType.Outline:
                    if (!reference.shadow || reference.shadow.GetType() != typeof(Outline))
                    {
                        Destroy(reference.shadow);
                        reference.shadow = gameObject.AddComponent<Outline>();
                    }

                    break;
            }

            if (!reference.shadow)
                return;
            
            // shadow marks layout as dirty on value update, don't update without changes
            if(reference.shadow.effectColor != style.shadowColor) 
                reference.shadow.effectColor = style.shadowColor;
            
            if(reference.shadow.effectDistance != style.shadowDistance) 
                reference.shadow.effectDistance = style.shadowDistance;

            if(reference.shadow.useGraphicAlpha != style.shadowUseGraphicAlpha) 
                reference.shadow.useGraphicAlpha = style.shadowUseGraphicAlpha;
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

            if (UpdateComponent(reference.value, ref reference.layoutElement, useLayout))
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
            
            if (UpdateComponent(reference.value, ref reference.sizeFitter, useFitter))
            {
                reference.sizeFitter.horizontalFit = horizontalFit;
                reference.sizeFitter.verticalFit = verticalFit;
            }
        }

        private static bool UpdateComponent<T>(MonoBehaviour value, ref T behaviour, bool isActive)
            where T : Component
        {
            if (isActive)
            {
                if (!behaviour)
                    behaviour = value.gameObject.AddComponent<T>();

                return true;
            }

            if (behaviour)
            {
                Destroy(behaviour);
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

        /// <summary>
        /// Destroys object in edit or play mode
        /// </summary>
        public static void Destroy(Object obj)
        {
            if (!obj)
                return;
            
            if (Application.isPlaying)
            {
                Object.Destroy(obj);
            }
            else
            {
                Object.DestroyImmediate(obj);
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