using UnityEngine;

public class UIButtonSound : MonoBehaviour
{
    public AudioClip clickSound;

   public void PlayClick()
{
    Debug.Log("CLICK SOUND!");
    
    if (clickSound == null)
    {
        Debug.LogError("No sound assigned!");
        return;
    }

    if (Camera.main == null)
    {
        Debug.LogError("No Main Camera!");
        return;
    }

    AudioSource.PlayClipAtPoint(clickSound, Camera.main.transform.position, 1f);
}
}