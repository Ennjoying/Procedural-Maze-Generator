using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIcontroller : MonoBehaviour
{
    [SerializeField] private GameObject _mazeGenRef;
    [SerializeField] private TMP_InputField _inputWidth;
    [SerializeField] private TMP_InputField _inputHeight;
    [SerializeField] private Slider _sliderWidth;
    [SerializeField] private Slider _sliderHeight;

    private MazeGenerator _mazeGenScriptRef;
    private GameObject _currentMaze;

    private void Start()
    {
        _currentMaze = Instantiate(_mazeGenRef, new Vector3(0, 0, 0), Quaternion.identity);
        _mazeGenScriptRef = _currentMaze.GetComponent<MazeGenerator>();
    }

    void Update()
    {
        //check for changed slider or textvalue
        //apply all values
    }

    public void onSlideHeight()
    {
        _inputHeight.text= _sliderHeight.value.ToString();
        _mazeGenScriptRef.MazeHeight = (int)_sliderHeight.value;
    }
    public void onSlideWidth()
    {
        _inputWidth.text= _sliderWidth.value.ToString();
        _mazeGenScriptRef.MazeWidth = (int)_sliderWidth.value;

    }

    public void onValueChangeHeight()
    {
        _sliderHeight.value = float.Parse(_inputHeight.text);
        _mazeGenScriptRef.MazeHeight = (int)_sliderHeight.value;

    }
    public void onValueChangeWidth()
    {
        _sliderWidth.value = float.Parse(_inputWidth.text);
        _mazeGenScriptRef.MazeWidth = (int)_sliderWidth.value;
    }

    public void generateMaze()
    {
        Destroy(_currentMaze);
        _currentMaze = Instantiate(_mazeGenRef, new Vector3(0, 0, 0), Quaternion.identity);
        _mazeGenScriptRef = _currentMaze.GetComponent<MazeGenerator>();
        //_mazeGenScriptRef.StartByUI((int)_sliderWidth.value, (int)_sliderHeight.value);
        _mazeGenScriptRef.StartByUI(250, 250);
    }
}
