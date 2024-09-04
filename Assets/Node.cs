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

    private void Start()
    {
        calculateBoxSpace();
    }

    public swapInspactNodeArray[] ChunkInfoArrayCol;
    public void Update()
    {
        //優先優化
        //calculateBoxSpace();
    }

    public int NodeSort = -1;//在預設序列中的位置

    public int MapXNum = 5;
    public int MapYNum = 5;

    public int chunkX = 1;
    public int chunkY = 1;

    public int[][] chunkInfo;
    public int[][] chunkViolentEnergyInfo;
    //public chunk[][] chunkItself;

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

    public void calculateBoxSpace()
    {
        BoxSpaceProvide = 0;
        foreach (swapInspactNodeArray chunkArray in ChunkInfoArrayCol)
        {
            foreach (chunk chunkLoading in chunkArray.chunkRow)
            {
                BoxSpaceProvide += chunkLoading.itemFieldProvideNumber;
                chunkLoading.itemExtend();
            }
        }
    }
    /*
    public void setDefult()
    {
        foreach (NodeFileChunkIntArrayRow[] colChunk in ChunkInfoArrayCol)//列
        {
            foreach (chunk rowChunk in colChunk)//行
            {
                rowChunk.setDefult();
            }
        }
    }*/
    //互動功能往下寫
    public int BoxSpaceProvide = 0;
    public void calculateBoxSpaceProvide()
    {

    }
}


[System.Serializable]
public class NodeFile
{
    public int NodeSort = -1;

    public int MapXNum = 5;
    public int MapYNum = 5;
    public int[][] chunkInfo;
    public int[][] chunkViolentEnergyInfo;

    public int[] linkingNodesSort;//連結中的節點

    public swapInspactNodeArray[] ChunkInfoArrayCol;

    public string nodeType = "noCertainType";//if belong to player 可控制
    public string nodeFaction = "noFaction";
    public string nodeName = "Defult Node Name";

    public void drawLinkLine()
    {

    }

}
[System.Serializable]
public class swapInspactNodeArray
{
    public chunk[] chunkRow = new chunk[3];
}
