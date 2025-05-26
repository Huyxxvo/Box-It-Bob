using UnityEngine;
using UnityEngine.UIElements;

public class ItemSlot : MonoBehaviour
{
    public ItemType acceptedType;

    void OnEnable()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;

        var slotVisual = new VisualElement();
        slotVisual.style.width = 100;
        slotVisual.style.height = 100;
        slotVisual.style.backgroundColor = Color.gray;
        slotVisual.style.borderBottomWidth = 2;
        slotVisual.style.borderTopWidth = 2;
        slotVisual.style.borderLeftWidth = 2;
        slotVisual.style.borderRightWidth = 2;
        slotVisual.style.borderBottomColor = Color.black;
        slotVisual.style.borderTopColor = Color.black;
        slotVisual.style.borderLeftColor = Color.black;
        slotVisual.style.borderRightColor = Color.black;

        root.Add(slotVisual);
    }
}
