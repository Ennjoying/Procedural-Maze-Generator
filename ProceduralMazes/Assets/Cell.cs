using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class Cell : MonoBehaviour
{
    public bool visited = false;

    public void visitCell()
    {
        visited = true;
    }

}
