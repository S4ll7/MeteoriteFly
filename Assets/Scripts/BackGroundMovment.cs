using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackGroundMovment : MonoBehaviour
{
    [SerializeField] private AnimationCurve _speed;
    [SerializeField] private Timer _timer;

    private RawImage _image;
    private float _rectY;
    private float _newUVRectY;
    private int _curentStage;

    private void Start()
    {
        _image = GetComponent<RawImage>();
        _rectY = _image.uvRect.y;
    }

    private void OnEnable()
    {
        _timer.ScoreUpdated += UpdateStage;
    }

    private void OnDisable()
    {
        _timer.ScoreUpdated -= UpdateStage;
    }

    private void UpdateStage(int value)
    {
        _curentStage = value;
    }

    private void SetUVRectY(float value)
    {
        _image.uvRect = new Rect(_image.uvRect.x, value, _image.uvRect.width, _image.uvRect.height);
    }

    private void Update()
    {
        _newUVRectY = _image.uvRect.y + _speed.Evaluate(_curentStage) * Time.deltaTime;
        if (_newUVRectY >= _rectY + 1)
        {
            SetUVRectY(_rectY);
        }
        else
        {
            SetUVRectY(_newUVRectY);
        }

    }
}
