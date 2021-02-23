using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public Vector2Int position = new Vector2Int();
    public bool isOccupied = false;

    // Pathfinding
    public int Hcost;
    public int Gcost;
    public int Fcost;

    public Node previousNode;

    public Node(Vector2Int position, bool isOccupied = false) {
        this.position = position;
        this.previousNode = null;
        this.isOccupied = isOccupied;
    }

    public Node(int x, int y, bool isOccupied = false) {
        this.position = new Vector2Int(x, y);
        this.previousNode = null;
        this.isOccupied = isOccupied;
    }

    internal int CalculateFcost() {
        Fcost = Gcost + Hcost;
        return Fcost;
    }

    private bool SetOccupyStatus(bool status) {
        if(isOccupied != status) {
            isOccupied = status;
            return true;
        }
        return false;
    }

    public void Occupy() {
        SetOccupyStatus(true);
    }

    public void Unoccupy() {
        SetOccupyStatus(false);
        //Debug.Log($"{position} Unoccupied");
    }
}
