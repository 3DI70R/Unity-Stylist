using UnityEngine;
using UnityEngine.UI;

namespace ThreeDISevenZeroR.Stylist
{
    [CreateAssetMenu(menuName = ElementStyle.MenuCategory + nameof(Image), order = 0)]
    public class ImageStyle : ElementStyle<ImageStyleData> { }
}