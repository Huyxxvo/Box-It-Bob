using UnityEngine;

public class DisableSoundWhenNotDraggable : MonoBehaviour
{
    private PlaySoundOnPointerDown soundScript;
    private Behaviour dragScript;  // generic type for scripts inheriting from Behaviour

    void Start()
    {
        soundScript = GetComponent<PlaySoundOnPointerDown>();

        // Replace "DragAndDrop" below with the actual drag script name on your towel
        dragScript = GetComponent("DraggableItem") as Behaviour;

        if (soundScript == null)
            Debug.LogWarning("PlaySoundOnPointerDown script not found.");

        if (dragScript == null)
            Debug.LogWarning("Drag script not found. Check the script name in DisableSoundWhenNotDraggable.");
    }

    void Update()
    {
        if (dragScript != null && soundScript != null)
        {
            if (!dragScript.enabled && soundScript.enabled)
            {
                soundScript.enabled = false;
                Debug.Log("Sound disabled because dragging is disabled.");
            }
        }
    }
}
