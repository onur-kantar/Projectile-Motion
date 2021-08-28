using System.Collections;
using UnityEngine;

public class Ball : MonoBehaviour
{
    Vector3 startPoint;
    Vector3 endPoint;
    float arcHeight;
    float speed;
    bool bounce;
    public void Jump(Vector3 startPoint, Vector3 endPoint, float arcHeight, float speed, bool bounce)
    {
        this.startPoint = startPoint;
        this.endPoint = endPoint;
        this.arcHeight = arcHeight;
        this.speed = speed;
        this.bounce = bounce;

        StartCoroutine(JumpCoroutine());
    }

    IEnumerator JumpCoroutine()
    {
        float progress = 0;
        float distance = Vector3.Distance(startPoint, endPoint);
        float stepScale = speed / distance;
        while (progress != 1.0f)
        {
            progress = Mathf.Min(progress + Time.deltaTime * stepScale, 1.0f);
            float parabola = 1.0f - 4.0f * (progress - 0.5f) * (progress - 0.5f);
            Vector3 nextPos = Vector3.Lerp(startPoint, endPoint, progress);
            nextPos.y += parabola * arcHeight;
            transform.LookAt(nextPos, transform.forward);
            transform.position = nextPos;

            yield return null;
        }
        Arrived();
    }

    void Arrived()
    {
        if (bounce)
        {
            arcHeight /= 2;
            if (arcHeight > 0.1f)
            {
                Vector3 tempEndPoint = endPoint;
                tempEndPoint = (endPoint - startPoint) / 2 + endPoint;
                startPoint = endPoint;
                endPoint = tempEndPoint;
                Jump(startPoint, endPoint, arcHeight, speed, bounce);
                return;
            }
        }
        gameObject.SetActive(false);
    }
}
