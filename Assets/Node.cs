using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Node : MonoBehaviour
{
    //Node功能載入
    public void loadNodeFile()
    {
        //Load informations
    }

    public int NodeSort = -1;//在預設序列中的位置

    public int MapXNum = 5;
    public int MapYNum = 5;
    public int[][] chunkInfo;
    public int[][] chunkViolentEnergyInfo;
    public chunk[][] chunkItself;

    public List<Node> linkingNodes = new List<Node>();
    
    public string nodeType = "noCertainType";//if belong to player 可控制
    public string nodeFaction = "noFaction";
    public string nodeName = "Defult Node Name";

    public void drawLinkLine()
    {
        
    }

    public void syncNodeFile()
    {

    }
    //互動功能往下寫
}


[System.Serializable]
public class NodeFile
{
    public int NodeSort = -1;

    public int MapXNum = 5;
    public int MapYNum = 5;
    public int[][] chunkInfo;
    public int[][] chunkViolentEnergyInfo;
    public chunk[][] chunkItself;

    public int[] linkingNodesSort;//連結中的節點

    public string nodeType = "noCertainType";//if belong to player 可控制
    public string nodeFaction = "noFaction";
    public string nodeName = "Defult Node Name";

    public void drawLinkLine()
    {

    }

}