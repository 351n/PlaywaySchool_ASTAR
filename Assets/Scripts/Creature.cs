using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creature : Entity
{
    public Combat combat;

    void Start() {
        Initialize();
    }

    void Update() {

    }

    public bool CanSee(Vector2Int origin, Vector2Int target) {
        float distance = Vector2.Distance(origin, target);
        Vector3 virutualPos = new Vector3(origin.x, 0.7f, origin.y);
        Vector3 virtualTarget = new Vector3(target.x, 0.7f, target.y);
        Vector3 dir = (virtualTarget - virutualPos).normalized;

        //Debug.Log($"Checking ray between {virutualPos} and {virtualTarget}");

        RaycastHit hit;
        if(Physics.Raycast(virutualPos, dir, out hit, distance + 10f)) {
            Debug.DrawRay(virutualPos, dir * hit.distance, Color.yellow);
            //Debug.Log($"Did Hit {hit.collider.gameObject.name}");
            return false;
        } else {
            Debug.DrawRay(virutualPos, dir * 1000, Color.white);
            //Debug.Log("Did not Hit");
            return true;
        }
    }

    public bool CanSee(Node targetNode) {
       return CanSee(pos, targetNode.position);
    }

    public bool CanSee(Node origin, Node targetNode) {
        return CanSee(origin.position, targetNode.position);
    }

    public void Initialize() {
        base.Initialize();
        combat = GetComponent<Combat>();
    }

    public void MoveTo(Node node) {
        this.node.Unoccupy();
        node.Occupy();
        transform.position = new Vector3(node.position.x, 0, node.position.y);
        pos = node.position;
    }
}
