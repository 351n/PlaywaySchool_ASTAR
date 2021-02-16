using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid
{
    public int width { get; private set; }
    public int height { get; private set; }

    Node[,] nodes;

    public Grid(int width, int height) {
        this.width = width;
        this.height = height;
        nodes = new Node[width,height];

        CreateNodes();
    }

    public void CreateNodes() {
        nodes = new Node[width, height];

        for(int x = 0; x < width; x++) {
            for(int y = 0; y < height; y++) {
                nodes[x, y] = new Node(this, x, y);
            }
        }

        Debug.Log($"Nodes created {nodes.Length}");
    }

    internal Node GetNode(Vector2Int pos) {
        return GetNode(pos.x, pos.y);
    }

    public Node GetNode(int x, int y) {
        //Debug.Log($"Getting {x},{y}");
        if(IsOnMap(x, y)) {
            return nodes[x, y];
        } else {
            return null;
        }
    }

    public List<Node> GetNeighbours(Node node) {
        List<Node> result = new List<Node>();
        for(int x = -1; x <= 1; x++) {
            for (int y = -1; y <= 1; y++) {
                if(IsOnMap(node.position.x + x, node.position.y + y)) {
                    result.Add(GetNode(node.position + new Vector2Int(x, y)));
                }
            }
        }
        return result;        
    }

    public bool IsOnMap(int x, int y) {
        return x < width && x>=0 && y < height && y>=0;
    }
}
