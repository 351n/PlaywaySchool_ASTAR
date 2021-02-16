using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    private const int STRAIGHT_COST = 10;
    private const int DIAGONAL_COST = 14;

    public Grid grid;

    void Start()
    {
        grid = new Grid(10, 10);
    }

    public List<Node> FindPath(Vector2Int start, Vector2Int end) {
        List<Node> openList = new List<Node>();
        List<Node> closedList = new List<Node>();

        Node startNode = grid.GetNode(start);
        Node endNode = grid.GetNode(end);


        for(int x = 0; x < grid.width; x++) {
            for(int y = 0; y < grid.height; y++) {
                Node node = grid.GetNode(x, y);
                node.Gcost = int.MaxValue;
                node.CalculateFcost();
            }
        }

        startNode.Gcost = 0;
        startNode.Hcost = CalculateDistance(startNode, endNode);
        startNode.CalculateFcost();

        while(openList.Count > 0) {
            Node currentNode = GetLowestFcostNode(openList);

            if(currentNode == endNode) {
                return CalculatePath(endNode);
            } else {
                openList.Remove(currentNode);
                closedList.Add(currentNode);
            }     
            
            foreach(Node n in grid.GetNeighbours(currentNode)) {
                if(closedList.Contains(n)) continue;

                int tempGcost = currentNode.Gcost + CalculateDistance(currentNode, n);
                if(tempGcost < n.Gcost) {
                    n.previousNode = currentNode;
                    n.Gcost = tempGcost;
                    n.Hcost = CalculateDistance(n, endNode);
                    n.CalculateFcost();

                    if(!openList.Contains(n)) {
                        openList.Add(n);
                    }
                }
            }
        }

        //No more nodes
        Debug.LogError("Could not find path");

        return null;
    }

    List<Node> CalculatePath(Node destination) {
        List<Node> result = new List<Node>();

        result.Add(destination);

        Node currentNode = destination;
        while(currentNode.previousNode != null) {
            result.Add(currentNode.previousNode);
            currentNode = currentNode.previousNode;
        }

        return result;
    }

    int CalculateDistance(Node a, Node b) {
        int horizontal = Mathf.Abs(b.position.x - a.position.x);
        int vertical = Mathf.Abs(b.position.y - a.position.y);
        int remaining = Mathf.Abs(horizontal-vertical);

        return DIAGONAL_COST * Mathf.Min(horizontal, vertical) + STRAIGHT_COST * remaining;
    }

    Node GetLowestFcostNode(List<Node> nodes) {        
        Node result = nodes[0];
        if(nodes.Count > 1) {
            for(int i = 1;i < nodes.Count; i++) {
                if(nodes[i].Fcost < result.Fcost) {
                    result = nodes[i];
                }
            }
        }
        return result;
    }
}
