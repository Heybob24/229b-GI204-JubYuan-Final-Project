using UnityEngine;

public class InstaDeath : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        Idle_Controller player = other.GetComponent<Idle_Controller>();

        if (player != null)
        {
            player.Die();
        }
    }
}