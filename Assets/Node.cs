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

    private void Start()
    {
        calculateBoxSpace();
    }

    public swapInspactNodeArray[] ChunkInfoArrayCol;
    public void Update()
    {
        //�u���u��
        //calculateBoxSpace();
    }

    public int NodeSort = -1;//�b�w�]�ǦC������m

    public int MapXNum = 5;
    public int MapYNum = 5;

    public int chunkX = 1;
    public int chunkY = 1;

    public int[][] chunkInfo;
    public int[][] chunkViolentEnergyInfo;
    //public chunk[][] chunkItself;

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
        foreach (NodeFileChunkIntArrayRow[] colChunk in ChunkInfoArrayCol)//�C
        {
            foreach (chunk rowChunk in colChunk)//��
            {
                rowChunk.setDefult();
            }
        }
    }*/
    //���ʥ\�੹�U�g
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

    public int[] linkingNodesSort;//�s�������`�I

    public swapInspactNodeArray[] ChunkInfoArrayCol;

    public string nodeType = "noCertainType";//if belong to player �i����
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
