using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CellCube : Cell
{
    [SerializeField] private GameObject _wallNorth;
    [SerializeField] private GameObject _wallEast;
    [SerializeField] private GameObject _wallSouth;
    [SerializeField] private GameObject _wallWest;
    
    

    public void deactivateWallNorth()
    {
        _wallNorth.SetActive(false);
    }
    
    public void deactivateWallEast()
    {
        _wallEast.SetActive(false);
    }
    
    public void deactivateWallSouth()
    {
        _wallSouth.SetActive(false);
    }
    
    public void deactivateWallWest()
    {
        _wallWest.SetActive(false);
    }
    
    
}
