using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creature : Entity
{
    public Combat combat;

    void Start() {
        Initialize();
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

    public void Initialize() {
        base.Initialize();
        combat = GetComponent<Combat>();
    }

    public void MoveTo(Node destination) {
        this.node.Unoccupy();

        destination.Occupy();
        node = destination;
        pos = destination.position;
        transform.position = new Vector3(pos.x, 0, pos.y);
    }
}
