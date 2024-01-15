using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Cell : MonoBehaviour
{
    
    [SerializeField] private GameObject _ceiling; 
    private bool _Visited = false;
    
    public void visitCell()
    {
        _Visited = true;
        _ceiling.SetActive(false);
    }

    public bool isVisited()
    {
        return _Visited;
    }
}
