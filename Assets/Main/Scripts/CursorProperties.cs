using UnityEngine;

public class GlobalCursorChange : MonoBehaviour
{
    public Texture2D cursorTexture;
    public Vector2 hotspot = Vector2.zero;
    public CursorMode cursorMode = CursorMode.Auto;

    void Start()
    {
        DontDestroyOnLoad(gameObject); // This makes it stay forever across scenes!
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Cursor.SetCursor(cursorTexture, hotspot, cursorMode);
        }

        if (Input.GetMouseButtonUp(0))
        {
            Cursor.SetCursor(null, Vector2.zero, cursorMode);
        }
    }
}
