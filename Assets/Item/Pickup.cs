using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
	private GameObject inventory;
	public GameObject item;

    void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
    }

	void OnTriggerEntered2D(Collider2D other){
		if (other.CompareTag("Player")){
			for (int i = 0; i < inventory.slots.size; i++){
				//add item
				if (inventory.quantity[i] != 0 && inventory.slots[i] != itemTag){
					continue;
				}
				if (inventory.quantity[i] < item.maxNumber){
					inventory.addItem(i, itemTag);
					Destroy(gameObject);
					break;
				}
			}
		}
	}

}
