using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
	//public GameObject itemBar;
	public GameObject[] slots;
	private int[] quantity;
	
    // Start is called before the first frame update
    void Start()
    {
		for (int i = 0; i < numSlots; i++){
			quantity[i] = 0;
		}
    }

	void addItem(int slotNum, GameObject item){
		if (quantity[i] >= item.maxNumber){
			return;
		}
		slots[i] = item;
		quantity[i] += 1;
	}

	void useItem(int i){
		if (quantity <= 0){
			return;
		}
		slots[i].cast();
		if (quantity[i] > 1){
			quantity[i] -=1;
		}
		else{
			quantity = 0;
			Destroy(slots[i]);
		}
	}	

}
