using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] float movementSpeed;
    [SerializeField] float rotateSpeed;
    float TargetAngleX, TargetAngleY;

    private void Update()
    {
        if (Input.GetKey(KeyCode.A))
            transform.Translate(Vector3.left * Time.deltaTime * movementSpeed);
        if (Input.GetKey(KeyCode.D))
            transform.Translate(Vector3.right * Time.deltaTime * movementSpeed);
        if (Input.GetKey(KeyCode.W))
            transform.Translate(Vector3.forward * Time.deltaTime * movementSpeed);
        if (Input.GetKey(KeyCode.S))
            transform.Translate(Vector3.back * Time.deltaTime * movementSpeed);

        if (Input.GetMouseButton(2))
        {
            TargetAngleX += Input.GetAxis("Mouse X") * rotateSpeed * Time.deltaTime;
            TargetAngleY += Input.GetAxis("Mouse Y") * rotateSpeed * Time.deltaTime;
            transform.eulerAngles = new Vector3(-TargetAngleY, TargetAngleX, 0);
        }
    }
}
