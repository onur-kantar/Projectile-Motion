using TMPro;
using UnityEngine;

/// <summary>
/// Ball creation takes place after the user clicks on the floor 2 times.
/// First click: The starting position of the ball.
/// Second click: The location where the ball will go.
/// Arc height, ball speed and bounce are changed in the game screen.
/// </summary>
public class BallCreator : MonoBehaviour
{
    [SerializeField] TMP_Text startPointText, endPointText;
    [SerializeField] TMP_Text arcHeighttText, speedText;
    float arcHeight, speed;
    bool bounce;
    bool firstClick;
    ObjectPooler objectPooler;
    Vector3 startPoint, endPoint;

    private void Start()
    {
        objectPooler = GetComponent<ObjectPooler>();
        firstClick = true;
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Create();
        }
    }

    void Create()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (firstClick)
            {
                startPoint = hit.point;
                firstClick = false;
                startPointText.text = startPoint.ToString();
                endPoint = Vector3.zero;
                endPointText.text = endPoint.ToString();
            }
            else
            {
                endPoint = hit.point;
                firstClick = true;
                endPointText.text = endPoint.ToString();

                if (arcHeight > 0 && speed > 0)
                {
                    GameObject ball = objectPooler.GetPooledObject("Ball");
                    if (ball != null)
                        ball.GetComponent<Ball>().Jump(startPoint, endPoint, arcHeight, speed, bounce);
                }
            }
        }
    }
    public void UpdateArchHeight(float value)
    {
        value = Mathf.Round(value * 10) / 10.0f;
        arcHeight = value;
        arcHeighttText.text = value.ToString();
    }
    public void UpdateSpeed(float value)
    {
        value = Mathf.Round(value * 10) / 10.0f;
        speed = value;
        speedText.text = value.ToString();
    }
    public void UpdateBounce(bool value)
    {
        bounce = value;
    }
}
