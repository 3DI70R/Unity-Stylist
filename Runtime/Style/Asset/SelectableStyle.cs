using UnityEngine;
using UnityEngine.UI;

namespace ThreeDISevenZeroR.Stylist
{
    [CreateAssetMenu(menuName = ElementStyle.MenuCategory + nameof(Selectable), order = 100)]
    public class SelectableStyle : ElementStyle<SelectableStyleData> { }
}