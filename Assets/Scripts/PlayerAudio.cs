using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    public AudioSource audioSource;

    [Header("Sounds")]
    public AudioClip footstepSound;
    public AudioClip jumpSound;
    public AudioClip deathSound;

    private bool isWalking = false;

    void Update()
    {
        float x = Input.GetAxis("Horizontal");

        // 👣 เสียงเดิน
        if (Mathf.Abs(x) > 0.1f)
        {
            if (!isWalking)
            {
                audioSource.clip = footstepSound;
                audioSource.loop = true;
                audioSource.Play();
                isWalking = true;
            }
        }
        else
        {
            if (isWalking)
            {
                audioSource.Stop();
                isWalking = false;
            }
        }
    }

    // 🦘 กระโดด
    public void PlayJumpSound()
    {
        audioSource.PlayOneShot(jumpSound);
    }

    // 💀 ตาย
    public void PlayDeathSound()
    {
        audioSource.PlayOneShot(deathSound);
    }
}