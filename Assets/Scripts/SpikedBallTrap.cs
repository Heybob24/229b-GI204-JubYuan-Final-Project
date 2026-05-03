using UnityEngine;

public class SpikedBallTrap : MonoBehaviour
{
    [Header("References")]
    public GameObject metalChain;
    public GameObject spikedBall;

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

    void Update()
    {
        if (closedLoop)
        {
            // Full 360 rotation
            currentAngle += rotationSpeed * Time.deltaTime * direction;
        }
        else
        {
            // Pendulum swing between angle limits
            t += Time.deltaTime * rotationSpeed * direction;
            if (t >= 1f || t <= 0f) direction *= -1f;
            float eased = rotationEase.Evaluate(Mathf.Clamp01(t));
            currentAngle = startingAngle + (eased * anglesRange) - (anglesRange / 2f);
        }

        // Position the ball
        float rad = currentAngle * Mathf.Deg2Rad;
        Vector3 ballPos = transform.position + new Vector3(
            Mathf.Sin(rad) * radius,
            -Mathf.Cos(rad) * radius,
            0f
        );
        spikedBall.transform.position = ballPos;

        // Rotate chain links between anchor and ball
        UpdateChain();
    }

    void UpdateChain()
    {
        // Position chain links evenly along the arc
        int linkCount = metalChain.transform.childCount;
        for (int i = 0; i < linkCount; i++)
        {
            float lerp = (float)(i + 1) / (linkCount + 1);
            float rad = currentAngle * Mathf.Deg2Rad;
            metalChain.transform.GetChild(i).position = Vector3.Lerp(
                transform.position,
                spikedBall.transform.position,
                lerp
            );
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}