using UnityEngine;

public class ScaleToCoverCamera : MonoBehaviour
{
    void Start()
    {
        float cameraWidth = Camera.main.orthographicSize * 2 * Camera.main.aspect;
        float cameraHeight = Camera.main.orthographicSize * 2;
        float scaleX = 9.6f / cameraWidth;
        float scaleY = 9.6f / cameraHeight;
        float scale = Mathf.Max(scaleX, scaleY);
        transform.localScale = new Vector3(scale, scale, 1f);
    }
}
