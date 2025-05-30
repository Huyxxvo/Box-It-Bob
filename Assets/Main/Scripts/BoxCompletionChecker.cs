using UnityEngine;

[System.Serializable]
public class Box
{
    public string boxName;
    public ItemType[] acceptedTypes;  // Multiple item types accepted by this box
    public int totalItemsRequired;    // Total items needed to complete the box
    public AudioSource completionSound;  // Sound to play on completion

    [HideInInspector]
    public int currentCount = 0;
    [HideInInspector]
    public bool isCompleted = false;
}

public class BoxCompletionChecker : MonoBehaviour
{
    public Box[] boxes;

    // Call this method whenever an item is successfully dropped and placed
    public void RegisterItem(ItemType itemType)
    {
        foreach (Box box in boxes)
        {
            if (box.isCompleted)
                continue;  // Skip if already completed

            // Check if this box accepts the dropped item type
            if (System.Array.IndexOf(box.acceptedTypes, itemType) >= 0)
            {
                box.currentCount++;
                Debug.Log($"{itemType} added to {box.boxName} ({box.currentCount}/{box.totalItemsRequired})");

                if (box.currentCount >= box.totalItemsRequired)
                {
                    box.isCompleted = true;
                    Debug.Log($"{box.boxName} is completed!");

                    if (box.completionSound != null)
                        box.completionSound.Play();
                }
            }
        }
    }
}
