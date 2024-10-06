using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Globalization;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class CampSystem
{
    public List<Mercenaries> MercenariesList = new List<Mercenaries>();
    public Mercenaries[] tempMercenariesList;
    
    public List<Node> worldMapNodeList = new List<Node>();
    public Node[] tempWorldMapNodeList;

    public List<GameObject> nodeGameObjecgts  = new List<GameObject>();
    public NodeFile[] tempWorldMapNodeFile;

    public List<weapon> weaponStorehouseList = new List<weapon>();
    public weapon[] tempWeaponStorehouseList;

    public Mercenaries[] mercenariesSquad = new Mercenaries[3];

    public float C_Coin = -1;
    public float Kroan = -1;
    public float popularity = -1;

    public List<Item> inspactAllItemPlayerHave = new List<Item>();

    public void syncTempData_ForSave()
    {
        tempMercenariesList = MercenariesList.ToArray();
        tempWorldMapNodeList = worldMapNodeList.ToArray();//這行基本沒用了
        tempWeaponStorehouseList = weaponStorehouseList.ToArray();


        //物件轉Node list
        List<NodeFile> nodeFiles = new List<NodeFile>();

        foreach (Node eachNode in worldMapNodeList)
        {
            NodeFile swapFile = new NodeFile();

            swapFile.MapXNum = eachNode.MapXNum;
            swapFile.MapYNum = eachNode.MapYNum;

            swapFile.NodeTransformX = eachNode.NodeTransformX;
            swapFile.NodeTransformY = eachNode.NodeTransformY;

            swapFile.chunkInfo = eachNode.chunkInfo;
            swapFile.chunkViolentEnergyInfo = eachNode.chunkViolentEnergyInfo;
            swapFile.ChunkInfoArrayCol = eachNode.ChunkInfoArrayCol;

            swapFile.nodeType  = eachNode.nodeType;
            swapFile.nodeFaction = eachNode.nodeFaction;
            swapFile.nodeName = eachNode.nodeName;

            swapFile.NodeSort = eachNode.NodeSort;

            List<int> linkerInt = new List<int>();
            foreach (Node linkingNodes in eachNode.linkingNodes)
            {
                linkerInt.Add(linkingNodes.NodeSort);
            }
            swapFile.linkingNodesSort = linkerInt.ToArray();

            nodeFiles.Add(swapFile);
        }
        tempWorldMapNodeFile = nodeFiles.ToArray();
    }
    public void syncTempData_ForLoad() {
        MercenariesList = tempMercenariesList.ToList<Mercenaries>();
        worldMapNodeList = tempWorldMapNodeList.ToList<Node>();
        weaponStorehouseList = tempWeaponStorehouseList.ToList<weapon>();
    }

    public void EditCharacterPattenOpen()
    {

    }
}