using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeCell : Cell
{
    [SerializeField] private GameObject _wallNorth;
    [SerializeField] private GameObject _wallEast;
    [SerializeField] private GameObject _wallSouth;
    [SerializeField] private GameObject _wallWest;
    
    public void deactivateNorth()
    {
        _wallNorth.SetActive(false);
    }
    public void deactivateEast()
    {
        _wallEast.SetActive(false);
    }
    public void deactivateSouth()
    {
        _wallSouth.SetActive(false);
    }
    public void deactivateWest()
    {
        _wallWest.SetActive(false);
    }
}
