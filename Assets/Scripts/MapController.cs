using System;
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

        TestPathfinding();
    }

    private void Update() {
        if(Input.GetKeyUp(KeyCode.T)) {
            TestPathfinding();
        }
    }

    public void TestPathfinding() {
        Vector2Int start = new Vector2Int(UnityEngine.Random.Range(0, 10), UnityEngine.Random.Range(0, 10));
        Vector2Int end = start;

        while(end == start) {
            end = new Vector2Int(UnityEngine.Random.Range(0, 10), UnityEngine.Random.Range(0, 10));
        }

        Debug.Log($"Start {start}\nEnd {end}");

        List<Node> path = FindPath(start, end);


        if(path != null) {
            string pathString = "Path: ";
            foreach(Node n in path) {
                pathString += $"\n {n.position}";
            }
            Debug.Log(pathString);
        }
    }

    private void InitializeNodes() {
        for(int x = 0; x < grid.width; x++) {
            for(int y = 0; y < grid.height; y++) {
                Node node = grid.GetNode(x, y);
                node.Gcost = int.MaxValue;
                node.Hcost = 0;
                node.CalculateFcost();
                node.previousNode = null;
            }
        }
    }

    public List<Node> FindPath(Vector2Int start, Vector2Int end) {
        List<Node> closedList = new List<Node>();
        List<Node> openList = new List<Node>();

        Node startNode = grid.GetNode(start);
        Node endNode = grid.GetNode(end);

        openList.Add(startNode);

        InitializeNodes();

        startNode.Gcost = 0;
        startNode.Hcost = CalculateDistance(startNode, endNode);

        while(openList.Count > 0) {
            Node currentNode = GetLowestFcostNode(openList);

            if(currentNode == endNode) {
                return CalculatePath(endNode);
            } 

            openList.Remove(currentNode);
            closedList.Add(currentNode);                
            
            foreach(Node n in grid.GetNeighbours(currentNode)) {
                if(closedList.Contains(n)) continue;

                int tempGcost = currentNode.Gcost + CalculateDistance(currentNode, n);

                if(tempGcost < n.Fcost) {
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
        Debug.LogError($"Could not find path cList:{closedList.Count}");


        string pathString = "Closed list: ";
        foreach(Node n in closedList) {
            pathString += $"\n {n.position}";
        }

        Debug.Log(pathString);

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

        result.Reverse();

        return result;
    }

    int CalculateDistance(Node a, Node b) {
        //Debug.Log($"Calculating dist from {a.position} to {b.position}");
        int horizontal = Mathf.Abs(a.position.x - b.position.x);
        int vertical = Mathf.Abs(a.position.y - b.position.y);
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
