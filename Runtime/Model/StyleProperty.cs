using System;

namespace ThreeDISevenZeroR.Stylist
{
    /// <summary>
    /// Style property<br/>
    /// Holds information about property and resolve information<br/>
    /// <br/>
    /// Implementation is very weird, but it is for performance/editor reasons<br/>
    /// - performance: less allocations<br/>
    /// - editor: custom property drawer and displaying of resolved info<br/>
    /// </summary>
    [Serializable]
    public struct StyleProperty<P>
    {
        /// <summary>
        /// Property apply type, toggle value in property drawer
        /// </summary>
        public PropertyApplyType applyType;
        
        /// <summary>
        /// Value which holds original value assigned from style
        /// </summary>
        public P ownValue;
        
        /// <summary>
        /// Value which holds resolved value, assigned by resolver
        /// </summary>
        public P resolvedValue;
        
        /// <summary>
        /// Origin asset of resolved value, assigned by resolver
        /// </summary>
        public ElementStyle resolvedAsset;

        /// <summary>
        /// Is value default or resolved from asset
        /// </summary>
        public bool IsResolved => resolvedAsset;

        /// <summary>
        /// Used in initialization, creates unassigned property with default value
        /// </summary>
        public static implicit operator StyleProperty<P>(P value) => 
            new StyleProperty<P> { ownValue = value };
        
        /// <summary>
        /// Used in property assignment, <b>returns resolved value</b>
        /// </summary>
        public static implicit operator P(StyleProperty<P> value) => 
            value.resolvedValue;
    }
}