using System.Collections;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float mouseSensitivity = 2f; // Чувствительность мыши
    public float verticalRotationLimit = 90f; // Ограничение вертикального вращения
    public float edgeThreshold = 20f; // Порог для границ экрана

    private float _verticalRotation = 0f; // Вертикальное вращение камеры

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
        // Получаем положение курсора
        Vector3 mousePos = Input.mousePosition;

        // Вращаем камеру только если курсор близок к границе экрана
        if (mousePos.x < edgeThreshold || mousePos.x > Screen.width - edgeThreshold)
        {
            // Получаем ввод от мыши
            float mouseX = Input.GetAxis("MouseX") * mouseSensitivity;
            transform.parent.Rotate(Vector3.up * mouseX); // Вращаем игрока по оси Y
        }

        if (mousePos.y < edgeThreshold || mousePos.y > Screen.height - edgeThreshold)
        {
            float mouseY = Input.GetAxis("MouseY") * mouseSensitivity;
            _verticalRotation -= mouseY;

            // Ограничиваем вертикальное вращение
            _verticalRotation = Mathf.Clamp(_verticalRotation, -verticalRotationLimit, verticalRotationLimit);
            transform.localRotation = Quaternion.Euler(_verticalRotation, 0f, 0f); // Вращаем камеру по оси X
        }
    }
}