using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float _cameraRotationSpeed = 0.1f;

    [SerializeField] private float _minRotationVertical = -30f; 
    [SerializeField] private float _maxRotationVertical = 30f;  

    private Vector2 _touchStartPos;
    private bool _isDragging;

    private float _currentPitch; 

    void Start()
    {
        _currentPitch = transform.localEulerAngles.x;
    }

    void FixedUpdate()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            // Работаем только с правой половиной экрана
            if (touch.position.x > Screen.width / 2)
            {
                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        _touchStartPos = touch.position;
                        _isDragging = true;
                        break;

                    case TouchPhase.Moved:
                        if (_isDragging)
                        {
                            Vector2 touchDelta = touch.deltaPosition;

                            float deltaYaw = touchDelta.x * _cameraRotationSpeed;
                            transform.parent.Rotate(Vector3.up * deltaYaw);

                            float deltaPitch = -touchDelta.y * _cameraRotationSpeed;
                            _currentPitch += deltaPitch;
                            _currentPitch = Mathf.Clamp(_currentPitch, _minRotationVertical, _maxRotationVertical);

                            transform.localEulerAngles = new Vector3(_currentPitch, 0.0f, 0.0f);
                        }
                        break;

                    case TouchPhase.Ended:
                    case TouchPhase.Canceled:
                        _isDragging = false;
                        break;
                }
            }
        }
    }
}
