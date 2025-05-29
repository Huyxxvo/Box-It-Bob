using UnityEngine;

public class RandomizeOnceOnEnter : MonoBehaviour
{
    public RectTransform jumbleZone;

    private RectTransform rectTransform;
    private bool wasInZoneLastFrame = false;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        bool isInZone = IsInsideZone();

        if (isInZone && !wasInZoneLastFrame)
        {
            // Just entered the zone ? randomize position
            RandomizePosition();
        }

        wasInZoneLastFrame = isInZone;
    }

    private void RandomizePosition()
    {
        Rect zoneRect = GetWorldRect(jumbleZone);
        Vector2 randomPos = new Vector2(
            Random.Range(zoneRect.xMin, zoneRect.xMax),
            Random.Range(zoneRect.yMin, zoneRect.yMax)
        );

        rectTransform.position = randomPos;
    }

    private bool IsInsideZone()
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
