using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public Vector2Int position = new Vector2Int();
    public bool isOccupied = false;

    public bool canHide;

    // Pathfinding
    public int Hcost;
    public int Gcost;
    public int Fcost;

    public Node previousNode;
    public TextMesh debugText;

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

    internal bool CalculateCanHide(Entity hideFrom) {
        float distance = Vector2.Distance(position, hideFrom.pos);
        Vector3 virutualPos = new Vector3(position.x, 0.35f, position.y);
        Vector3 virtualTarget = new Vector3(hideFrom.pos.x, 0.35f, hideFrom.pos.y);
        Vector3 dir = (virtualTarget - virutualPos).normalized;

        //Debug.Log($"Checking ray between {virutualPos} and {virtualTarget}");

        RaycastHit hit;
        if(Physics.Raycast(virutualPos, dir, out hit, distance+0.1f)) {
            //Debug.DrawRay(virutualPos, dir * hit.distance, Color.yellow);
            //Debug.Log($"Did Hit {hit.collider.gameObject.name}");
            canHide = true;
        } else {
            //Debug.DrawRay(virutualPos, dir * 1000, Color.white);
            //Debug.Log("Did not Hit");
            canHide = false;
        }
        UpdateDebug();
        return canHide;
    }

    internal void UpdateDebug() {
        if(canHide) {
            debugText.text = $"Hide";
        } else {
            debugText.text = $"";
        }
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
