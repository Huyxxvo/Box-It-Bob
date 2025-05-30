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
        allImages.AddRange(FindObjectsOfType<RandomizeOnceOnEnter>());
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
