using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Plotter.Tools.GUI
{
    public class ButtonAdv : Button
    {
        public override void OnSelect(BaseEventData eventData)
        {
            InstantClearState();
        }
    }
}
