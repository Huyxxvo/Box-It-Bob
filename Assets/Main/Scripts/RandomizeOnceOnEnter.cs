using UnityEngine;
using UnityEngine.EventSystems;

public class RandomizeOnceOnEnter : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public RectTransform jumbleZone;
    public ZoneManager manager;

    private RectTransform rectTransform;
    private bool wasInZoneLastFrame = false;
    private bool isDragging = false;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        bool isInZone = IsInsideZone();

        if (isInZone && !wasInZoneLastFrame)
        {
            manager?.OnEnterZone(this);
        }
        else if (!isInZone && wasInZoneLastFrame)
        {
            manager?.OnExitZone(this);
        }

        wasInZoneLastFrame = isInZone;
    }

    public void RandomizeNow()
    {
        Rect zoneRect = GetWorldRect(jumbleZone);
        Vector2 randomPos = new Vector2(
            Random.Range(zoneRect.xMin, zoneRect.xMax),
            Random.Range(zoneRect.yMin, zoneRect.yMax)
        );
        rectTransform.position = randomPos;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        isDragging = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.position += (Vector3)eventData.delta;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        isDragging = false;
        manager?.TryCheckWin(); // Check for win after releasing
    }

    public bool IsInsideZone()
    {
        Vector2 globalPos = rectTransform.position;
        return GetWorldRect(jumbleZone).Contains(globalPos);
    }

    private Rect GetWorldRect(RectTransform rt)
    {
        Vector3[] corners = new Vector3[4];
        rt.GetWorldCorners(corners);
        float x = corners[0].x;
        float y = corners[0].y;
        float width = corners[2].x - x;
        float height = corners[2].y - y;
        return new Rect(x, y, width, height);
    }
}
