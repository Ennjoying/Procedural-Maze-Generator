using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class UIcontroller : MonoBehaviour
{
    [SerializeField] private GameObject _mazeGenRef;
    [SerializeField] private Camera _cameraRef;
    //[SerializeField] private GameObject _planeRef;
    [SerializeField] private TMP_InputField _inputWidth;
    [SerializeField] private Slider _sliderWidth;
    [SerializeField] private TMP_InputField _inputHeight;
    [SerializeField] private Slider _sliderHeight;
    [SerializeField] private TMP_InputField _inputDistance;
    [SerializeField] private Slider _sliderDistance;
    [SerializeField] private TMP_InputField _inputCamSpeed;
    [SerializeField] private Slider _sliderCamSpeed;

    private bool lockSize = false;
    private MazeGenerator _mazeGenScriptRef;
    private GameObject _currentMaze;

    private void Start()
    {
        _currentMaze = Instantiate(_mazeGenRef, new Vector3(0, 0, 0), Quaternion.identity);
        _mazeGenScriptRef = _currentMaze.GetComponent<MazeGenerator>();
        
        _cameraRef.transform.position = new Vector3(0,_sliderDistance.value, 0);
        //_planeRef.transform.position = new Vector3(0,-2, 0);
    }

    public void LockSize()
    {
        lockSize = !lockSize;
    }

    public void onSlideHeight()
    {
        _inputHeight.text= _sliderHeight.value.ToString();
        _mazeGenScriptRef.MazeHeight = (int)_sliderHeight.value;
    }
    

    public void onValueChangeHeight()
    {
        float value = float.Parse(_inputHeight.text);
        if (value > _sliderHeight.maxValue) value = _sliderHeight.maxValue;
        else if (value < _sliderHeight.minValue) value = _sliderHeight.minValue; 
        _inputHeight.text = value.ToString();
        _sliderHeight.value = value;
        _mazeGenScriptRef.MazeHeight = (int)value;
        if (lockSize)
        {
            _inputWidth.text = value.ToString();
            _sliderWidth.value = value;
            _mazeGenScriptRef.MazeWidth = (int)value;
        }

    }
    public void onSlideWidth()
    {
        _inputWidth.text= _sliderWidth.value.ToString();
        _mazeGenScriptRef.MazeWidth = (int)_sliderWidth.value;

    }
    public void onValueChangeWidth()
    {
        float value = float.Parse(_inputWidth.text);
        if (value > _sliderWidth.maxValue) value = _sliderWidth.maxValue;
        else if (value < _sliderWidth.minValue) value = _sliderWidth.minValue; 
        _inputWidth.text = value.ToString();
        _sliderWidth.value = value;
        _mazeGenScriptRef.MazeWidth = (int)value;
        if (lockSize)
        {
            _inputHeight.text = value.ToString();
            _sliderHeight.value = value;
            _mazeGenScriptRef.MazeHeight = (int)value;
        }
    }
    public void onSlideDistance()
    {
        _inputDistance.text= _sliderDistance.value.ToString();
        _cameraRef.transform.position = new Vector3(0,_sliderDistance.value, 0);
        //s_cameraRef.transform.position += cameraOffset;
    }

    public void onValueChangeDistance()
    {
        float value = float.Parse(_inputDistance.text);
        if (value > 230) value = 230;
        else if (value < 20) value = 20;
        _inputDistance.text = value.ToString();
        _sliderDistance.value = value;
        //_cameraRef.transform.position= new Vector3(0,value, 0) + cameraOffset;
        _cameraRef.transform.position = new Vector3(0,_sliderDistance.value, 0);
        //_cameraRef.transform.position += cameraOffset;
    }

    public void onSlideSpeed()
    {
        _inputCamSpeed.text = _sliderCamSpeed.value.ToString();
        _cameraRef.GetComponent<cameraMovement>()._cameraSpeed = _sliderCamSpeed.value;
        //s_cameraRef.transform.position += cameraOffset;
    }
    public void onValueChangeSpeed()
    {
        float value = float.Parse(_inputCamSpeed.text);
        if (value > _sliderCamSpeed.maxValue) value = _sliderCamSpeed.maxValue;
        else if (value < _sliderCamSpeed.minValue) value = _sliderCamSpeed.minValue;
        _inputCamSpeed.text = value.ToString();
        _cameraRef.GetComponent<cameraMovement>()._cameraSpeed = value;
        
    }

    public void generateMaze()
    {
        Destroy(_currentMaze);
        _currentMaze = Instantiate(_mazeGenRef, new Vector3(0, 0, 0), Quaternion.identity);
        _mazeGenScriptRef = _currentMaze.GetComponent<MazeGenerator>();
        _mazeGenScriptRef.StartByUI((int)_sliderWidth.value, (int)_sliderHeight.value);
    }
}
