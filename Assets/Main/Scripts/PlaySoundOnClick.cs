using UnityEngine;
using UnityEngine.EventSystems;

public class PlaySoundOnPointerDown : MonoBehaviour, IPointerDownHandler
{
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (audioSource != null)
        {
            audioSource.Play();
        }
    }
}
