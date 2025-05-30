using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public ItemType itemType;

    private Vector3 originalPosition;
    private Transform originalParent;
    private Canvas canvas;

    // Added reference to BoxCompletionChecker
    private BoxCompletionChecker boxCompletionChecker;

    void Start()
    {
        originalPosition = transform.position;
        originalParent = transform.parent;
        canvas = GetComponentInParent<Canvas>();

        // Find BoxCompletionChecker in the scene (assumes there is one)
        boxCompletionChecker = FindObjectOfType<BoxCompletionChecker>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        transform.SetParent(canvas.transform);
    }

    public void OnDrag(PointerEventData eventData)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.transform as RectTransform,
            eventData.position,
            canvas.worldCamera,
            out Vector2 localPoint
        );
        transform.localPosition = localPoint;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        DropArea dropArea = GetClosestDropArea();
        if (dropArea != null && dropArea.acceptedType == itemType)
        {
            transform.position = dropArea.transform.position;
            enabled = false;

            // Notify BoxCompletionChecker when item is placed successfully
            if (boxCompletionChecker != null)
            {
                boxCompletionChecker.RegisterItem(itemType);
            }
        }
        else
        {
            transform.position = originalPosition;
            transform.SetParent(originalParent);
        }
    }

    private DropArea GetClosestDropArea()
    {
        DropArea[] allAreas = FindObjectsByType<DropArea>(FindObjectsSortMode.None);
        foreach (DropArea area in allAreas)
        {
            RectTransform rt = area.GetComponent<RectTransform>();
            if (RectTransformUtility.RectangleContainsScreenPoint(rt, Input.mousePosition, canvas.worldCamera))
            {
                return area;
            }
        }
        return null;
    }
}
