using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
	//public GameObject itemBar;
	public Item[] slots;
	public int[] quantity;
	
    // Start is called before the first frame update
    void Start()
    {
		for (int i = 0; i < slots.Length; i++){
			quantity[i] = 0;
		}
    }

	void Update(){
		//read input 1234
	}

	public void addItem(int i, Item item){
		if (quantity[i] >= item.maxNumber){
			return;
		}
		slots[i] = item;
		quantity[i] += 1;
	}

	public void useItem(int i){
		if (quantity[i] <= 0){
			return;
		}
		slots[i].cast();
		if (quantity[i] > 1){
			quantity[i] -=1;
		}
		else{
			quantity[i] = 0;
			Destroy(slots[i]);
		}
	}	

}
