using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;


public class SpawnController : MonoBehaviour
{
    //fields to set
    [SerializeField] private int _mazeWidth;
    [SerializeField] private int _mazeHeight;
    [SerializeField] private Color _colorWall;
    [SerializeField] private Color _colorGround;
    [SerializeField] Cell activeCell;
    //dont touch
    [SerializeField] private Material _matWall;
    [SerializeField] private Material _matGround;
    
    //Stores all nodes in the list, all connections in the linkedlist.
    private List<List<Cell>> _mazeCells = new List<List<Cell>>();
    private Stack<Cell> cellsToBacktrack = new Stack<Cell>();
    private enum CellDirection {North,East,South,West}

    void Start()
    {
        //load materials and set their color to the selected ones
        _matWall.color = _colorWall;
        _matGround.color = _colorGround;

        //instantiate cells & move them into position
        _mazeCells = instantiateCubeCells(this.gameObject, _mazeWidth, _mazeHeight);
        this.gameObject.transform.position = new Vector3(-_mazeWidth / 2, 0, -_mazeHeight / 2);
       
       //generate the maze
       
    }

    // Instatiate the mazeCells with a given Width & Height and attach it to the parent.
    // returns the cells as a 2D list.
    
    private List<List<Cell>> instantiateCubeCells(GameObject parent,int width, int height)
    {
        List<List<Cell>> graph = new List<List<Cell>>();
        for (int x = 0; x < width; x++)
        {
            graph.Add(new List<Cell>());
            for (int z = 0; z < height; z++)
            {
                graph[x].Add(Instantiate<Cell>(activeCell,new Vector3(x,0,z), new Quaternion(), parent.transform ));
            }
        }
        return graph;
    }

    private void startMazeGen()
    {
        //choose a random start point & visit first cell
        visitFirstCubeCell(Random.Range(0, _mazeCells.Count + 1), Random.Range(0, _mazeCells[0].Count + 1));
        
        //notes for the algorithm:
        //1. select random neighbourcell 
        //2. visit cell and remove walls between and add visited cell to stack for backtracking
        
        //continue until no unvisited neighbours
        //check cells in stack for unvisited neighbours
    }
    

    private void visitFirstCubeCell(int x, int z)
    {
        CellCube cell = (CellCube)_mazeCells[x][z];
        cell.visitCell();
        cellsToBacktrack.Push(cell);
        
        //deactive walls for the entry
        if(cell.transform.position.x == 0) cell.deactivateWallSouth();
        if(cell.transform.position.x == _mazeWidth) cell.deactivateWallNorth();
        if(cell.transform.position.z == 0) cell.deactivateWallWest();
        if(cell.transform.position.z == _mazeHeight) cell.deactivateWallEast();
        
        
        (CellCube nextCell, CellDirection nextDir) = chooseRandomNeighbourCubeCell(cell);
        visitNextCubeCell(cell, nextCell, nextDir);
        
    }

    private void visitNextCubeCell(CellCube cellBefore, CellCube currentCell, CellDirection currentDir)
    {
        currentCell.visitCell();
        cellsToBacktrack.Push(currentCell);
        
        //deactive walls
        switch (currentDir)
        {
            case CellDirection.North:
                cellBefore.deactivateWallNorth();
                currentCell.deactivateWallSouth();
                break;
            case CellDirection.East:
                cellBefore.deactivateWallEast();
                currentCell.deactivateWallEast();
                break;
            case CellDirection.South:
            cellBefore.deactivateWallSouth();
            currentCell.deactivateWallNorth();
                break;
            case CellDirection.West:
            cellBefore.deactivateWallWest();
            currentCell.deactivateWallEast();
                break;
        }
        
        (CellCube nextCell, CellDirection nextDir) = chooseRandomNeighbourCubeCell(currentCell);
        if (nextCell = null)
        {
            //continue here with checking cells in stack for unvisited neighbour
        }
        visitNextCubeCell(currentCell, nextCell, nextDir);
    }
    

    private (CellCube,CellDirection) chooseRandomNeighbourCubeCell(CellCube cell)
    {
        List<CellCube> neighbourCells = new List<CellCube>();
        List<CellDirection> neighbourDirections = new List<CellDirection>();
        int x = (int)cell.transform.position.x;
        int z = (int)cell.transform.position.z;
        
        if(x != _mazeWidth  && !_mazeCells[x+1][z].visited)
        {
            neighbourCells.Add((CellCube)_mazeCells[x+1][z]);
            neighbourDirections.Add(CellDirection.East);
        }

        if (z != _mazeHeight && !_mazeCells[x][z + 1].visited)
        {
            neighbourCells.Add((CellCube)_mazeCells[x][z+1]);
            neighbourDirections.Add(CellDirection.North);
        }
        if(x != 0 && !_mazeCells[x-1][z].visited)
        {
            neighbourCells.Add((CellCube)_mazeCells[x-1][z]);
            neighbourDirections.Add(CellDirection.West);
        }

        if (z != 0 && !_mazeCells[x][z - 1].visited)
        {
            neighbourCells.Add((CellCube)_mazeCells[x][z-1]);
            neighbourDirections.Add(CellDirection.South);
        }
        
        int i = Random.Range(0, neighbourCells.Count);
        return (neighbourCells[i], neighbourDirections[i]);
        
    }
}
