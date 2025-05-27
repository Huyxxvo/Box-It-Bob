using UnityEngine;
using UnityEngine.UIElements;

public class ItemSlot : MonoBehaviour
{
    public ItemType acceptedType;
    public VisualElement slotVisual { get; private set; }
    public Vector2 position;

    void OnEnable()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;

        slotVisual = new VisualElement();
        slotVisual.name = "item-slot";
        slotVisual.AddToClassList("slot");
        slotVisual.style.width = 100;
        slotVisual.style.height = 100;
        slotVisual.style.position = Position.Absolute;
        slotVisual.style.left = position.x;
        slotVisual.style.top = position.y;
        slotVisual.style.backgroundColor = new Color(0.7f, 0.7f, 0.7f);
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
