using UnityEngine;
using UnityEngine.UI;

namespace ThreeDISevenZeroR.Stylist
{
    [CreateAssetMenu(menuName = ElementStyle.MenuCategory + nameof(Text), order = 0)]
    public class TextStyle : ElementStyle<TextStyleData> { }
}