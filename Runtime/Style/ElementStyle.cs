﻿using System;
using System.Collections.Generic;
using UnityEngine;

namespace ThreeDISevenZeroR.Stylist
{
    public abstract class ElementStyle : ScriptableObject
    {
        protected const string MenuCategory = "UI Style (Stylist)/";
        
#pragma warning disable 649
        [SerializeField] private InheritedStyles inherits;
#pragma warning restore 649
        
        public ElementStyle[] Inherits => inherits.array;
        public abstract ElementStyleData Overrides { get; }
        
        private readonly Dictionary<Type, object> resolvedObjectsCache 
            = new Dictionary<Type, object>();
        
        public D Resolve<D>() where D : ElementStyleData, new()
        {
            var type = typeof(D);

            if (Application.isPlaying && resolvedObjectsCache.TryGetValue(type, out var cachedValue))
                return (D) cachedValue;

            var newInstance = new D();
            newInstance.Resolve(new StyleResolver<ElementStyleData>(this));
            resolvedObjectsCache[type] = newInstance;
            return newInstance;
        }
        
#if UNITY_EDITOR
        public abstract void ResolveSelf();
#endif
    }
    
    public abstract class ElementStyle<T> : ElementStyle
        where T : ElementStyleData
    {
#pragma warning disable 649
        [FlattenAttribute]
        [SerializeField] private T overrides;
#pragma warning restore 649
        
        public override ElementStyleData Overrides => overrides;

#if UNITY_EDITOR
        public override void ResolveSelf()
        {
            overrides.Resolve(new StyleResolver<ElementStyleData>(this));
        }
#endif
    }
}