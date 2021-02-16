using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid
{
    public int width { get; private set; }
    public int height { get; private set; }
    Vector2 size;

    Node[,] nodes;

    public Grid(int width, int height) {
        this.width = width;
        this.height = height;
        size = new Vector2(width,height);
        nodes = new Node[width,height];

        CreateNodes();
    }

    public void CreateNodes() {
        for(int x = 0; x < width; x++) {
            for(int y = 0; y < height; y++) {
                nodes[x, y] = new Node(this, x, y);
            }
        }        
    }

    internal Node GetNode(Vector2Int pos) {
        return GetNode(pos.x, pos.y);
    }

    public Node GetNode(int x, int y) {
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
                if(x!=0 && y != 0) {
                    if(IsOnMap(node.position.x, node.position.y)) {
                        result.Add(GetNode(node.position + new Vector2Int(x, y)));
                    }
                }
            }
        }
        return result;
    }

    public bool IsOnMap(int x, int y) {
        return x < width && y < height;
    }
}
