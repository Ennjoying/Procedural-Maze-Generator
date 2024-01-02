using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpawnController : MonoBehaviour
{
    //fields to set
    [SerializeField] private int _mazeWidth;
    [SerializeField] private int _mazeHeight;
    [SerializeField] private Color _colorWall;
    [SerializeField] private Color _colorGround;
    [SerializeField] GameObject activeCell;
    //dont touch
    [SerializeField] private GameObject _mazeEmpty;
    [SerializeField] private Material _matWall;
    [SerializeField] private Material _matGround;
    
    //Stores all nodes in the list, all connections in the linkedlist.
    private List<List<GameObject>> _mazeGraph = new List<List<GameObject>>();

    void Start()
    {
        //load materials and set their color to the selected ones
        _matWall.color = _colorWall;
        _matGround.color = _colorGround;
        
        
       _mazeGraph = createCubeMaze(_mazeEmpty, _mazeWidth, _mazeHeight);
       _mazeEmpty.transform.position = new Vector3(-_mazeWidth / 2, 0, -_mazeHeight / 2);
    }
    
    void Update()
    {
        
    }
    
    // Instatiates the mazeCells with a given Width & Height and returns a 2D graph.
    private List<List<GameObject>> createCubeMaze(GameObject parent,int width, int height)
    {
        List<List<GameObject>> graph = new List<List<GameObject>>();
        for (int x = 0; x < width; x++)
        {
            graph.Add(new List<GameObject>());
            for (int y = 0; y < height; y++)
            {
                graph[x].Add(Instantiate<GameObject>(activeCell,new Vector3(x,0,y), new Quaternion(), parent.transform ));
            }
        }
        return graph;
    }
}
