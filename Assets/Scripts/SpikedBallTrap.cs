using UnityEngine;
using System.Collections.Generic;

public class SpikedBallTrap : MonoBehaviour
{
    [Header("References")]
    public GameObject metalChain;
    public GameObject spikedBall;

    [Header("Damage")]
    public int damage = 1;

    [Header("Settings")]
    public bool closedLoop = true;
    public float radius = 5f;
    public float anglesRange = 60f;
    public float startingAngle = 0f;
    public float rotationSpeed = 2f;

    [Header("Easing")]
    public AnimationCurve rotationEase = AnimationCurve.Linear(0, 0, 1, 1);

    private float currentAngle;
    private float direction = 1f;
    private float t = 0f;

    // กันโดนซ้ำในเฟรมเดียว
    private HashSet<GameObject> hitThisFrame = new HashSet<GameObject>();

    void LateUpdate()
    {
        // reset ทุกเฟรม
        hitThisFrame.Clear();
    }

    void Update()
    {
        // ===== Movement =====
        if (closedLoop)
        {
            currentAngle += rotationSpeed * Time.deltaTime * direction;
        }
        else
        {
            t += Time.deltaTime * rotationSpeed * direction;
            if (t >= 1f || t <= 0f) direction *= -1f;

            float eased = rotationEase.Evaluate(Mathf.Clamp01(t));
            currentAngle = startingAngle + (eased * anglesRange) - (anglesRange / 2f);
        }

        float rad = currentAngle * Mathf.Deg2Rad;
        Vector3 ballPos = transform.position + new Vector3(
            Mathf.Sin(rad) * radius,
            -Mathf.Cos(rad) * radius,
            0f
        );

        spikedBall.transform.position = ballPos;

        UpdateChain();
    }

    void UpdateChain()
    {
        int linkCount = metalChain.transform.childCount;

        for (int i = 0; i < linkCount; i++)
        {
            float lerp = (float)(i + 1) / (linkCount + 1);

            metalChain.transform.GetChild(i).position = Vector3.Lerp(
                transform.position,
                spikedBall.transform.position,
                lerp
            );
        }
    }

    // ===== DAMAGE (เหมือนกระสุน) =====
    void OnTriggerEnter2D(Collider2D coll)
    {
        if (!coll.CompareTag("Player")) return;

        // กันโดนซ้ำในเฟรมเดียว
        if (hitThisFrame.Contains(coll.gameObject)) return;

        hitThisFrame.Add(coll.gameObject);

        GameManager.instance.TakeDamage(damage);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}