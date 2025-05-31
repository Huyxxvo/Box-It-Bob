using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ZoneManager : MonoBehaviour
{
    public string winSceneName = "WinScene";

    private HashSet<RandomizeOnceOnEnter> imagesInZone = new HashSet<RandomizeOnceOnEnter>();
    private List<RandomizeOnceOnEnter> allImages = new List<RandomizeOnceOnEnter>();

    void Start()
    {
        // Updated to use the new method
        allImages.AddRange(FindObjectsByType<RandomizeOnceOnEnter>(FindObjectsSortMode.None));
        foreach (var image in allImages)
        {
            image.manager = this;
        }
    }

    public void OnEnterZone(RandomizeOnceOnEnter image)
    {
        imagesInZone.Add(image);
    }

    public void OnExitZone(RandomizeOnceOnEnter image)
    {
        imagesInZone.Remove(image);
    }

    public void TryCheckWin()
    {
        if (imagesInZone.Count == 0)
        {
            SceneManager.LoadScene(winSceneName);
        }
    }
}
