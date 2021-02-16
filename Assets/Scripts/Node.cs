using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    Grid grid;

    public Vector2Int position;

    // Pathfinding
    public int Hcost;
    public int Gcost;
    public int Fcost;

    public Node previousNode;

    public Node(Grid grid, Vector2Int position) {
        this.grid = grid;
        this.position = position;
        this.previousNode = null;
    }

    public Node(Grid grid, int x, int y) {
        this.grid = grid;
        this.position = new Vector2Int(x,y);
        this.previousNode = null;
    }

    internal int CalculateFcost() {
        Fcost = Gcost + Hcost;
        return Fcost;
    }
}
