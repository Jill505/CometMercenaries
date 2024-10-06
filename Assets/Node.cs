using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

[System.Serializable]
public class Node : MonoBehaviour
{
    //基礎常數
    public float intervalX = 2f;
    public float intervalY = 2f;

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
        //editingNode();
    }

    public int NodeSort = -1;//在預設序列中的位置

    public int MapXNum = 5;
    public int MapYNum = 5;

    public float NodeTransformX;
    public float NodeTransformY;

    public int chunkX = 1;
    public int chunkY = 1;

    public int[][] chunkInfo;
    public int[][] chunkViolentEnergyInfo;
    //public chunk[][] chunkItself;

    public List<Node> linkingNodes = new List<Node>();
    
    public string nodeType = "noCertainType";//if belong to player 可控制
    public string nodeFaction = "noFaction";
    public string nodeName = "Defult Node Name";
    public string nodePictureName = "Defult Picture Name";

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

    public void editingNode()
    {
        NodeTransformX = gameObject.transform.position.x;
        NodeTransformY = gameObject.transform.position.y;
    }

    public void NodeBeenClick()
    {
        //open a patten and pass the data into the pattenCore
    }

    public Vector3 origionPos = new Vector3(0,0,0);
    public void RanderChunksForVBF()
    {
        int countA = 0 ;
        foreach (swapInspactNodeArray chunkArray in ChunkInfoArrayCol)
        {
            int countB = 0;
            foreach (chunk chunkLoading in chunkArray.chunkRow)
            {
                //生成chunk們至實體世界 
                GameObject chunkObject = new GameObject("Chunk_C"+countA+"R"+countB);
                chunkObject.transform.position = new Vector2(origionPos.x + (intervalX*countA),origionPos.y + (intervalY * countB));
                SpriteRenderer spriteRenderer;
                spriteRenderer = chunkObject.AddComponent<SpriteRenderer>();

                chunk_VBF theChunkInfo;
                theChunkInfo = chunkObject.AddComponent<chunk_VBF>();
                theChunkInfo.chunkX = countB;
                theChunkInfo.chunkY = countA;

                //同步資料過去
                theChunkInfo.chunkType = ChunkInfoArrayCol[countA].chunkRow[countB].chunkType;

                chunkObject.AddComponent<BoxCollider2D>();
                chunkObject.tag = "chunk";
                BoxCollider2D col2d = chunkObject.GetComponent<BoxCollider2D>();
                col2d.size = new Vector2(2, 2);

                //依照chunk資料載入不同資料 這邊先寫個defult擋一下
                spriteRenderer.sprite = Resources.Load<Sprite>("defultNodePicture");

                if (theChunkInfo.chunkType == "entry")
                {
                    spriteRenderer.color = Color.green;
                }

                countB++;
            }
            countA++;
        }
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

    public int chunkX = 1;
    public int chunkY = 1;

    public float NodeTransformX;
    public float NodeTransformY;

    public int[] linkingNodesSort;//連結中的節點

    public swapInspactNodeArray[] ChunkInfoArrayCol;
    public chunkInfos[] chunkAlldata;
    public chunkInfo alreadyLoadingChunkInfo;

    public string nodeType = "noCertainType";//if belong to player 可控制
    public string nodeFaction = "noFaction";
    public string nodeName = "Defult Node Name";
    public string nodePictureName = "Defult Picture Name";

    public void drawLinkLine()
    {

    }

}
[System.Serializable]
public class swapInspactNodeArray
{
    public chunk[] chunkRow = new chunk[3];
}
