using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public Vector2Int pos = new Vector2Int();
    public Node node;
    //multifield (?)

    public virtual void Initialize() {
        pos = new Vector2Int(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.z));
        if(MapController.instance.grid.IsOnMap(pos.x, pos.y)) {
            node = MapController.instance.grid.GetNode(pos);
            if(!node.isOccupied) {
                transform.position = new Vector3(pos.x, 0.5f, pos.y);
                node.Occupy();
            } else {
                Debug.LogWarning($"{name} tried to occypy {pos} but its already occupied. Destroing gameobject");
                //Destroy(this.gameObject);
            }
        } else {
            Debug.LogWarning($"{name} tried to occypy {pos} which is outside of map. Destroing gameobject");
            //Destroy(this.gameObject);
        }
    }
}
