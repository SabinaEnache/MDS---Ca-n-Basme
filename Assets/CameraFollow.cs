using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // obiectul pe care camera trebuie să-l urmărească
    public float rotationSpeed = 5.0f; // viteză de rotație a camerei

    void Update()
    {
        // Calculăm direcția mișcării caracterului
        Vector3 direction = target.forward;

        // Calculăm unghiul între direcția mișcării și direcția camerei
        float angle = Vector3.Angle(transform.forward, direction);

        // Rotim camera către direcția mișcării cu o anumită viteză
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), rotationSpeed * Time.deltaTime);
    }
}
