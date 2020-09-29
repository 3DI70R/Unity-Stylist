using UnityEngine;
using UnityEngine.UI;

namespace ThreeDISevenZeroR.Stylist
{
    [CreateAssetMenu(menuName = ElementStyle.MenuCategory + nameof(Button), order = 50)]
    public class ButtonStyle : ElementStyle<ButtonStyleData> { }
}