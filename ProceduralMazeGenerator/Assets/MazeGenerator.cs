using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MazeGenerator : MonoBehaviour
{
    [SerializeField] private Cell _selectedCellType;
    [SerializeField] private GameObject _groundCubeRef;
    [SerializeField] private int _mazeWidth = 20;
    [SerializeField] private int _mazeHeight = 20;

    private List<List<Cell>> _mazeCells = new List<List<Cell>>();

    private Stack<Cell> _cellsToBacktrack = new Stack<Cell>();
    // Start is called before the first frame update
    void Start()
    {
        //instantiate the MazeCells
        _mazeCells = createMazeCells(transform);
        
        //move the Maze in place, reactive to the chosen size
        transform.position = new Vector3(-_mazeWidth / 2, 0, -_mazeHeight / 2);
        _groundCubeRef.transform.localPosition = new Vector3(_mazeWidth / 2 -.5f, -.6f, _mazeHeight / 2 - .5f);
        _groundCubeRef.transform.localScale = new Vector3(_mazeWidth, .2f, _mazeHeight);
        
        //generate the maze, with a given start point
        startCubeMazeGeneration(0,0);
    }

    #region methods of the CubeMaze generation algorithm
    
    
    private void startCubeMazeGeneration(int x, int z)
    {
        CubeCell startCell = (CubeCell)_mazeCells[x][z];
        // visit Cell & deactivate the entry walls
        startCell.visitCell();
        _cellsToBacktrack.Push(startCell);
        hideCubeCellWallAtBorder(startCell);

        CubeCell nextCell = selectRandomNeighbourCubeCell(startCell);
        visitNextCubeCell(startCell,nextCell);
        chooseRandomExitCubeCell();
    }
    
    private void visitNextCubeCell(CubeCell lastCell, CubeCell thisCell)
    {
        thisCell.visitCell();
        _cellsToBacktrack.Push(thisCell);
        hideCubeCellWalls(lastCell,thisCell);
        CubeCell nextCell = selectRandomNeighbourCubeCell(thisCell);
        
        //if no unvisitedCell is found, continue with backtracked cells
        if (nextCell == null)
        {
            while (nextCell == null && _cellsToBacktrack.Count != 0)
            {
                thisCell = (CubeCell)_cellsToBacktrack.Pop();
                nextCell = selectRandomNeighbourCubeCell(thisCell);
            }
            if (_cellsToBacktrack.Count == 0) return;
        }
        visitNextCubeCell(thisCell,nextCell);
    }

    private CubeCell selectRandomNeighbourCubeCell(CubeCell cell)
    {
        List<CubeCell> neighbourCells = new List<CubeCell>();
        int x = (int)cell.transform.localPosition.x;
        int z = (int)cell.transform.localPosition.z;
        
        if (x != 0 && !_mazeCells[x-1][z].isVisited())neighbourCells.Add((CubeCell)_mazeCells[x-1][z]); 
        else if(x !=  _mazeWidth-1 && !_mazeCells[x+1][z].isVisited())neighbourCells.Add((CubeCell)_mazeCells[x+1][z]);
        if(z != 0 && !_mazeCells[x][z-1].isVisited()) neighbourCells.Add((CubeCell)_mazeCells[x][z-1]); 
        else if(z != _mazeHeight-1 && !_mazeCells[x][z+1].isVisited()) neighbourCells.Add((CubeCell)_mazeCells[x][z+1]);

        if (neighbourCells.Count == 0) return null;
        return neighbourCells[Random.Range(0, neighbourCells.Count)];

    }
    private void chooseRandomExitCubeCell()
    {
        int xEnd = _mazeWidth-1;
        int zEnd = _mazeHeight-1;
        if (Random.Range(0, 2) == 0) xEnd = Random.Range(0, _mazeWidth-1);
        else zEnd = Random.Range(0, _mazeHeight-1);
        
        CubeCell endCell = (CubeCell)_mazeCells[xEnd][zEnd];
        hideCubeCellWallAtBorder(endCell);
    }
    
    #endregion 
    
    #region helper methods
    private List<List<Cell>> createMazeCells(Transform parent)
    {
        List<List<Cell>> cells = new List<List<Cell>>();

        for (int x = 0; x < _mazeWidth; x++)
        {
            cells.Add(new List<Cell>());
            for (int z = 0; z < _mazeHeight; z++)
            {
                cells[x].Add(Instantiate(_selectedCellType, new Vector3(x, 0, z), new Quaternion(0, 0, 0, 0), parent));
                
            }
        }
        return cells;
    }

    private void hideCubeCellWallAtBorder(CubeCell cell)
    {
        if (cell.transform.localPosition.x == 0) cell.deactivateWest();
        else if(cell.transform.localPosition.x ==  _mazeWidth-1) cell.deactivateEast();
        if(cell.transform.localPosition.z == 0) cell.deactivateSouth();
        else if(cell.transform.localPosition.z == _mazeHeight-1) cell.deactivateNorth();
    }
    
    private void hideCubeCellWalls(CubeCell cell, CubeCell connectedCell)
    {
        if (cell.transform.localPosition.x < connectedCell.transform.localPosition.x)
        {
            cell.deactivateEast();
            connectedCell.deactivateWest();
        }
        if (cell.transform.localPosition.x > connectedCell.transform.localPosition.x)
        {
            cell.deactivateWest();
            connectedCell.deactivateEast();
        }
        if (cell.transform.localPosition.z < connectedCell.transform.localPosition.z)
        {
            cell.deactivateNorth();
            connectedCell.deactivateSouth();
        }
        if (cell.transform.localPosition.z > connectedCell.transform.localPosition.z)
        {
            cell.deactivateSouth();
            connectedCell.deactivateNorth();
        }
    }
    
    #endregion
}
