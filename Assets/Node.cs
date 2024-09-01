using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Node : MonoBehaviour
{
    //Node�\����J
    public void loadNodeFile()
    {
        //Load informations
    }

    public int NodeSort = -1;//�b�w�]�ǦC������m

    public int MapXNum = 5;
    public int MapYNum = 5;
    public int[][] chunkInfo;
    public int[][] chunkViolentEnergyInfo;
    public chunk[][] chunkItself;

    public List<Node> linkingNodes = new List<Node>();
    
    public string nodeType = "noCertainType";//if belong to player �i����
    public string nodeFaction = "noFaction";
    public string nodeName = "Defult Node Name";

    public void drawLinkLine()
    {
        
    }

    public void syncNodeFile()
    {

    }
    //���ʥ\�੹�U�g
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

    public int[] linkingNodesSort;//�s�������`�I

    public string nodeType = "noCertainType";//if belong to player �i����
    public string nodeFaction = "noFaction";
    public string nodeName = "Defult Node Name";

    public void drawLinkLine()
    {

    }

}