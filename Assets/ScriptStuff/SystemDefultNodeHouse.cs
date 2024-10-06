using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering.Universal;
using UnityEngine;

public class SystemDefultNodeHouse : MonoBehaviour
{
    public List<GameObject> defultNodeGameObjects = new List<GameObject>();
    public List<Node> defultNodes = new List<Node>(); //basicly useless
    public List<NodeFile> defultNodeFiles = new List<NodeFile>();
    // Start is called before the first frame update
    void Start()
    {
        nodeGiveInitialization();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void nodeGiveInitialization()
    {
        defultNodes.Clear();

        int counter = 0;
        foreach (NodeFile NF in defultNodeFiles) 
        {

            GameObject nodeObject = new GameObject("Node_" + counter);
            Node swapNode = nodeObject.AddComponent<Node>();

            defultNodeGameObjects.Add(nodeObject);

            //Node swapNode = new Node();

            swapNode.MapXNum = NF.MapXNum;
            swapNode.MapYNum = NF.MapYNum;

            swapNode.NodeTransformX = NF.NodeTransformX;
            swapNode.NodeTransformY = NF.NodeTransformY;

            swapNode.chunkX = NF.chunkX; 
            swapNode.chunkY = NF.chunkY;

            swapNode.chunkInfo = NF.chunkInfo;
            swapNode.chunkViolentEnergyInfo = NF.chunkViolentEnergyInfo;
            swapNode.ChunkInfoArrayCol = NF.ChunkInfoArrayCol;

            swapNode.NodeSort = NF.NodeSort;

            swapNode.nodeType = NF.nodeType;
            swapNode.nodeFaction = NF.nodeFaction;
            swapNode.nodeName = NF.nodeName;

            defultNodes.Add(swapNode);

            counter++;
        }

        //¨Ì·Ólinking nodes
        counter = 0;
        foreach (NodeFile NF in defultNodeFiles)
        {
            int secondCounter = 0;
            foreach (int swapLinkingNode in NF.linkingNodesSort)
            {
                //defultNodes[counter].linkingNodes[secondCounter] = defultNodes[swapLinkingNode];
                defultNodes[counter].linkingNodes.Add(defultNodes[swapLinkingNode]);
                secondCounter++;
            }
            counter++;
        }

        GameCore.Save();
    }

    public void cleanAllDefultNodeObject()
    {
        foreach (GameObject gameObj in defultNodeGameObjects)
        {
            Destroy(gameObj);
        }
        defultNodeGameObjects.Clear();
    }
}
