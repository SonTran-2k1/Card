using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScaler : MonoBehaviour
{
    public enum ChangeTypeCamera
    {
        _2D,
        _3D
    }

    public ChangeTypeCamera _changeTypeCamera;

    public float targetAspectRatio; // Tỉ lệ màn hình 16:9
    public Camera virtualCamera;
    //2d
    private float defaultOrthographicSize = 9.6f; // Kích thước mặc định cho tỉ lệ 16:9
    //3d
    private float defaultVerticalFOV = 60f; // FOV dọc mặc định cho tỉ lệ 16:9
    private void Awake()
    {
        if (_changeTypeCamera == ChangeTypeCamera._2D)
        {
            virtualCamera.orthographicSize =
                (defaultOrthographicSize / targetAspectRatio) * ((float)Screen.height / Screen.width);
        }
        else
        {
            Camera cam = Camera.main;
            float currentAspectRatio = (float)Screen.width / Screen.height;
            float scaleRatio = targetAspectRatio / currentAspectRatio;
            float fovAdjustment = Mathf.Atan(Mathf.Tan(defaultVerticalFOV * Mathf.Deg2Rad * 0.5f) * scaleRatio) * 2f *
                                  Mathf.Rad2Deg;
            cam.fieldOfView = fovAdjustment;
        }
    }
}