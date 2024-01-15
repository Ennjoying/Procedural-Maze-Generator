using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class MazeGenerator : MonoBehaviour
{
    [SerializeField] private Cell selectedCellType;
    [SerializeField] private GameObject groundCubeRef; 
    [SerializeField] private GameObject cellParentRef; 
    
    public int MazeWidth {get; set; }
    public int MazeHeight {get; set; }

    private List<List<Cell>> _mazeCells = new List<List<Cell>>();

    private Stack<Cell> _cellsToBacktrack = new Stack<Cell>();
    
    
    public void StartByUI(int width, int height)
    {
        MazeWidth = width;
        MazeHeight = height;
        SetupMaze();
        StartCubeMazeGeneration(0,0);
    }

    #region CubeMaze generation algorithm
    
    //starts and generate the Maze.
    //choose the entry point with the x & z variables
    public void StartCubeMazeGeneration(int x, int z)
    {
        CubeCell startCell = (CubeCell)_mazeCells[x][z];
        // visit Cell & deactivate the entry walls
        startCell.visitCell();
        _cellsToBacktrack.Push(startCell);
        HideCubeCellWallAtBorder(startCell);

        //iterative version visit cubecell method
        while (_cellsToBacktrack.Count > 0)
        {
            CubeCell currentCell = (CubeCell)_cellsToBacktrack.Peek();
            CubeCell nextCell = SelectRandomNeighbourCubeCell(currentCell);

            if (nextCell != null)
            {
                nextCell.visitCell();
                _cellsToBacktrack.Push(nextCell);
                HideCubeCellWalls(currentCell, nextCell);
            }
            else _cellsToBacktrack.Pop();
        }
        ChooseRandomExitCubeCell();
    }

    private CubeCell SelectRandomNeighbourCubeCell(CubeCell cell)
    {
        List<CubeCell> neighbourCells = new List<CubeCell>();
        int x = (int)cell.transform.localPosition.x;
        int z = (int)cell.transform.localPosition.z;
        
        if (x != 0 && !_mazeCells[x-1][z].isVisited())neighbourCells.Add((CubeCell)_mazeCells[x-1][z]); 
        else if(x !=  MazeWidth-1 && !_mazeCells[x+1][z].isVisited())neighbourCells.Add((CubeCell)_mazeCells[x+1][z]);
        if(z != 0 && !_mazeCells[x][z-1].isVisited()) neighbourCells.Add((CubeCell)_mazeCells[x][z-1]); 
        else if(z != MazeHeight-1 && !_mazeCells[x][z+1].isVisited()) neighbourCells.Add((CubeCell)_mazeCells[x][z+1]);

        if (neighbourCells.Count == 0) return null;
        return neighbourCells[Random.Range(0, neighbourCells.Count)];

    }
    private void ChooseRandomExitCubeCell()
    {
        int xEnd = MazeWidth-1;
        int zEnd = MazeHeight-1;
        if (Random.Range(0, 2) == 0) xEnd = Random.Range(0, MazeWidth-1);
        else zEnd = Random.Range(0, MazeHeight-1);
        
        CubeCell endCell = (CubeCell)_mazeCells[xEnd][zEnd];
        HideCubeCellWallAtBorder(endCell);
    }
    
    #endregion 
    
    #region helper methods
    
    //instantiates the maze and moves the gameobject accordingly
    public void SetupMaze()
    {
        CreateMazeCells();
        UpdateObjectSize();
    }
    
    //move the Maze in place, reactive to the chosen size
    private void UpdateObjectSize()
    {
        transform.position = new Vector3(-MazeWidth / 2, 0, -MazeHeight / 2);
        groundCubeRef.transform.localPosition = new Vector3(MazeWidth / 2, -.6f, MazeHeight / 2);
        groundCubeRef.transform.localScale = new Vector3(MazeWidth +1, .2f, MazeHeight +1);
    }
    
    //instantiate the MazeCells with current width & height
    private void CreateMazeCells()
    {
        List<List<Cell>> cells = new List<List<Cell>>();

        for (int x = 0; x < MazeWidth; x++)
        {
            cells.Add(new List<Cell>());
            for (int z = 0; z < MazeHeight; z++)
            {
                cells[x].Add(Instantiate(selectedCellType, new Vector3(x, 0, z), new Quaternion(0, 0, 0, 0), cellParentRef.transform));
            }
        }
        _mazeCells = cells;
    }

    private void HideCubeCellWallAtBorder(CubeCell cell)
    {
        if (cell.transform.localPosition.x == 0) cell.deactivateWest();
        else if(cell.transform.localPosition.x ==  MazeWidth-1) cell.deactivateEast();
        if(cell.transform.localPosition.z == 0) cell.deactivateSouth();
        else if(cell.transform.localPosition.z == MazeHeight-1) cell.deactivateNorth();
    }
    
    private void HideCubeCellWalls(CubeCell cell, CubeCell connectedCell)
    {
        //i thought about removing the outer walls of every cell and have single cubes for the outer walls,
        //but it didnt do much for the performance so i left it the way it is.
        //HideCubeCellWallAtBorder(connectedCell);
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
