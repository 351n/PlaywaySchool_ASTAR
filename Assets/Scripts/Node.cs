using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public Vector2Int position = new Vector2Int();

    // Pathfinding
    public int Hcost;
    public int Gcost;
    public int Fcost;

    public Node previousNode;

    public Node(Grid grid, Vector2Int position) {
        this.position = position;
        this.previousNode = null;
    }

    public Node(Grid grid, int x, int y) {
        this.position = new Vector2Int(x,y);
        this.previousNode = null;
    }

    internal int CalculateFcost() {
        Fcost = Gcost + Hcost;
        return Fcost;
    }
}
