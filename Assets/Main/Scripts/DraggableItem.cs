using UnityEngine;
using UnityEngine.UIElements;

public class DraggableItem : MonoBehaviour
{
    public Texture2D itemTexture;
    public ItemType itemType;

    private VisualElement dragElement;
    private Vector2 offset;
    private VisualElement originalParent;
    private Vector2 originalPosition;

    void OnEnable()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;

        dragElement = new VisualElement();
        dragElement.style.width = 80;
        dragElement.style.height = 80;
        dragElement.style.position = Position.Absolute;
        dragElement.style.left = Random.Range(100, 200);
        dragElement.style.top = Random.Range(100, 200);
        dragElement.style.backgroundImage = new StyleBackground(itemTexture);
        dragElement.AddToClassList(itemType.ToString());

        originalPosition = new Vector2(dragElement.resolvedStyle.left, dragElement.resolvedStyle.top);
        originalParent = root;

        dragElement.RegisterCallback<PointerDownEvent>(evt =>
        {
            offset = new Vector2(evt.position.x, evt.position.y) - new Vector2(dragElement.worldBound.x, dragElement.worldBound.y);
        });

        dragElement.RegisterCallback<PointerMoveEvent>(evt =>
        {
            if (evt.pressedButtons != 1) return;
            dragElement.style.left = evt.position.x - offset.x;
            dragElement.style.top = evt.position.y - offset.y;
        });

        dragElement.RegisterCallback<PointerUpEvent>(evt =>
        {
            TryDropIntoSlot(evt.position);
        });

        root.Add(dragElement);
    }

    void TryDropIntoSlot(Vector2 mousePosition)
    {
        var slots = FindObjectsByType<ItemSlot>(FindObjectsSortMode.None);
        foreach (var slot in slots)
        {
            var doc = slot.GetComponent<UIDocument>();
            var root = doc.rootVisualElement;
            var worldRect = root.worldBound;

            if (worldRect.Contains(mousePosition))
            {
                if (slot.acceptedType == itemType)
                {
                    dragElement.RemoveFromHierarchy();
                    root.Add(dragElement);
                    dragElement.style.left = 0;
                    dragElement.style.top = 0;
                    dragElement.pickingMode = PickingMode.Ignore;
                    return;
                }
            }
        }

        dragElement.RemoveFromHierarchy();
        originalParent.Add(dragElement);
        dragElement.style.left = originalPosition.x;
        dragElement.style.top = originalPosition.y;
    }
}
