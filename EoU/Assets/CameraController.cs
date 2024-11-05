using System.Collections;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float mouseSensitivity = 2f; // ���������������� ����
    public float verticalRotationLimit = 90f; // ����������� ������������� ��������
    public float edgeThreshold = 20f; // ����� ��� ������ ������

    private float _verticalRotation = 0f; // ������������ �������� ������

    public IEnumerator Shake(float duration, float magnitude)
    {
        Vector3 originalPosition = transform.localPosition;

        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = new Vector3(x, y, originalPosition.z);

            elapsed += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = originalPosition;
    }

    void Update()
    {
        // �������� ��������� �������
        Vector3 mousePos = Input.mousePosition;

        // ������� ������ ������ ���� ������ ������ � ������� ������
        if (mousePos.x < edgeThreshold || mousePos.x > Screen.width - edgeThreshold)
        {
            // �������� ���� �� ����
            float mouseX = Input.GetAxis("MouseX") * mouseSensitivity;
            transform.parent.Rotate(Vector3.up * mouseX); // ������� ������ �� ��� Y
        }

        if (mousePos.y < edgeThreshold || mousePos.y > Screen.height - edgeThreshold)
        {
            float mouseY = Input.GetAxis("MouseY") * mouseSensitivity;
            _verticalRotation -= mouseY;

            // ������������ ������������ ��������
            _verticalRotation = Mathf.Clamp(_verticalRotation, -verticalRotationLimit, verticalRotationLimit);
            transform.localRotation = Quaternion.Euler(_verticalRotation, 0f, 0f); // ������� ������ �� ��� X
        }
    }
}