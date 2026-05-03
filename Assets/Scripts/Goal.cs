using UnityEngine;

public class Goal : MonoBehaviour
{
   private void OnTriggerEnter2D(Collider2D collision)
{
    if (collision.CompareTag("Player"))
    {
        Idle_Controller player = collision.GetComponent<Idle_Controller>();
        player.Win();

        GameManager.instance.WinGame();
    }
}
}