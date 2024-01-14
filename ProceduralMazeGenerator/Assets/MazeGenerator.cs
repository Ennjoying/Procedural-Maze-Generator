using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MazeGenerator : MonoBehaviour
{
    [SerializeField] private Cell _selectedCellType;
    [SerializeField] private int _mazeWidth = 20;
    [SerializeField] private int _mazeHeight = 20;

    private List<List<Cell>> _mazeCells = new List<List<Cell>>();

    private Stack<Cell> _cellsToBacktrack = new Stack<Cell>();
    // Start is called before the first frame update
    void Start()
    {
        _mazeCells = createMazeCells(transform);
        transform.position = new Vector3(-_mazeWidth / 2, 0, -_mazeHeight / 2);
        GameObject ground = Instantiate(GameObject.CreatePrimitive(PrimitiveType.Cube), new Vector3(0,0,0),Quaternion.identity,
            transform);
        ground.transform.localPosition = new Vector3(_mazeWidth / 2, -.6f, _mazeHeight / 2);
        ground.transform.localScale = new Vector3(_mazeWidth, .2f, _mazeHeight);
        
        //testCells();
        startCubeMazeGeneration(0,0);
    }
    
    //note for MazeGeneration
    //1. start gen by selecting an randomized entry -> startGen method
    //2. visit a Cell, remove its walls & add it to the stack. select the next neighbour to check and repeat
    //3. if no unvisited neighbours exist, redo visitCell with the method in stack

    private void startCubeMazeGeneration(int x, int z)
    {
        CubeCell startCell = (CubeCell)_mazeCells[x][z];
        // visit Cell & deactivate the entry walls
        startCell.visitCell();
        if (x == 0) startCell.deactivateWest();
        else if(x ==  _mazeWidth-1) startCell.deactivateEast();
        if(z == 0) startCell.deactivateSouth();
        else if(z == _mazeHeight-1) startCell.deactivateNorth();
        _cellsToBacktrack.Push(startCell);
        
        //visitNextCubeCell(startCell, );
        
    }

    private void visitNextCubeCell(CubeCell lastCell, CubeCell NewCell)
    {
        
    }

    private void selectNeighbourCubeCell(CubeCell cell)
    {
        
    }
    
    private void hideCellWalls(CubeCell cell, CubeCell connectedCell)
    {
        
    }
    
    
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

    private void testCells()
    {
        for (int x = 0; x < _mazeWidth; x++)
        {
            for (int z = 0; z < _mazeHeight; z++)
            {
                CubeCell test = (CubeCell)_mazeCells[x][z];
                test.visitCell();
                test.deactivateNorth();
            }
        }
    }
    
    
}
