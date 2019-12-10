using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Grid
{
    [HideInInspector]
    public Node startNode;
    [HideInInspector]
    public Node finishNode;

    private Node[,] NodeArray;
    private int gridSizeX;
    private int gridSizeY;

    public void CreateGrid(int[,] map)
    {
        gridSizeX = map.GetLength(0);
        gridSizeY = map.GetLength(1);
        NodeArray = new Node[gridSizeX, gridSizeY];
        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                bool Wall = true;
                if (map[x, y] != -1)
                    Wall = false;

                Vector3 worldPoint = Main.Instance.GetCellPosition(x, y);
                NodeArray[x, y] = new Node(Wall, worldPoint, x, y);
                
                if (worldPoint == Main.Instance.firstPathPos)
                    startNode = NodeArray[x, y];
                if (worldPoint == Main.Instance.lastPathPos)
                    finishNode = NodeArray[x, y];
            }
        }
    }

    public Node GetNodeByPosition(Vector3 position)
    {
        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                if (NodeArray[x, y].vPosition == position)
                    return NodeArray[x, y];
            }
        }
        return null;
    }

    public List<Node> GetAllNeighboringNodesByPos(Vector3 position, int range)
    {
        Node currentNode = GetNodeByPosition(position);
        List<Node> res = GetNeighboringNodes(currentNode, range);
        res.AddRange(GetDiagonalNeighboringNodes(currentNode, range));
        return res;
    }

    public List<Node> GetNeighboringNodes(Node a_NeighborNode, int range = 1)
    {
        List<Node> NeighborList = new List<Node>();
        int icheckX;
        int icheckY;

        for (int i = 1; i < range + 1; i++)
        {
        //Check the right side of the current node.
            icheckX = a_NeighborNode.iGridX + i;
            icheckY = a_NeighborNode.iGridY;
            if (icheckX >= 0 && icheckX < gridSizeX)
            {
                if (icheckY >= 0 && icheckY < gridSizeY)
                {
                    NeighborList.Add(NodeArray[icheckX, icheckY]);
                }
            }
            //Check the Left side of the current node.
            icheckX = a_NeighborNode.iGridX - i;
            icheckY = a_NeighborNode.iGridY;
            if (icheckX >= 0 && icheckX < gridSizeX)
            {
                if (icheckY >= 0 && icheckY < gridSizeY)
                {
                    NeighborList.Add(NodeArray[icheckX, icheckY]);
                }
            }
            //Check the Top side of the current node.
            icheckX = a_NeighborNode.iGridX;
            icheckY = a_NeighborNode.iGridY + i;
            if (icheckX >= 0 && icheckX < gridSizeX)
            {
                if (icheckY >= 0 && icheckY < gridSizeY)
                {
                    NeighborList.Add(NodeArray[icheckX, icheckY]);
                }
            }
            //Check the Bottom side of the current node.
            icheckX = a_NeighborNode.iGridX;
            icheckY = a_NeighborNode.iGridY - i;
            if (icheckX >= 0 && icheckX < gridSizeX)
            {
                if (icheckY >= 0 && icheckY < gridSizeY)
                {
                    NeighborList.Add(NodeArray[icheckX, icheckY]);
                }
            }
        }

        return NeighborList;
    }

    public List<Node> GetDiagonalNeighboringNodes(Node a_NeighborNode, int range = 1)
    {
        List<Node> NeighborList = new List<Node>();
        int icheckX;
        int icheckY;

        for (int i = 1; i < range + 1; i++)
        {
            icheckX = a_NeighborNode.iGridX + i;
            icheckY = a_NeighborNode.iGridY + i;
            if (icheckX >= 0 && icheckX < gridSizeX)
            {
                if (icheckY >= 0 && icheckY < gridSizeY)
                {
                    NeighborList.Add(NodeArray[icheckX, icheckY]);
                }
            }

            icheckX = a_NeighborNode.iGridX - i;
            icheckY = a_NeighborNode.iGridY - i;
            if (icheckX >= 0 && icheckX < gridSizeX)
            {
                if (icheckY >= 0 && icheckY < gridSizeY)
                {
                    NeighborList.Add(NodeArray[icheckX, icheckY]);
                }
            }

            icheckX = a_NeighborNode.iGridX - i;
            icheckY = a_NeighborNode.iGridY + i;
            if (icheckX >= 0 && icheckX < gridSizeX)
            {
                if (icheckY >= 0 && icheckY < gridSizeY)
                {
                    NeighborList.Add(NodeArray[icheckX, icheckY]);
                }
            }

            icheckX = a_NeighborNode.iGridX + i;
            icheckY = a_NeighborNode.iGridY - i;
            if (icheckX >= 0 && icheckX < gridSizeX)
            {
                if (icheckY >= 0 && icheckY < gridSizeY)
                {
                    NeighborList.Add(NodeArray[icheckX, icheckY]);
                }
            }
        }

        return NeighborList;
    }
}
