using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
	private Inventory inventory;
	public Item item;

    void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
    }

	void OnTriggerEntered2D(Collider2D other){
		if (other.CompareTag("Player")){
			for (int i = 0; i < inventory.slots.Length; i++){
				//add item
				if (inventory.quantity[i] != 0 && inventory.slots[i].type != item.type){
					continue;
				}
				if (inventory.quantity[i] < item.maxNumber){
					inventory.addItem(i, item);
					Destroy(gameObject);
					break;
				}
			}
		}
	}

}
