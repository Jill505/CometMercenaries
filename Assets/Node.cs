using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public int MapXNum = 5;
    public int MapYNum = 5;
    public int[][] chunkInfo;
    public int[][] chunkViolentEnergyInfo;

    public int nodeType = 0;
    public int nodeFaction = 0;
    public string nodeName = "Defult Node Name";

    public Node[] linkingNodes;

    public void drawLInkLine()
    {
        
    }
}
