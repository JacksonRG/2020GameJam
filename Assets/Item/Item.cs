﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
	public int maxNumber;
	public string type;
	//private Knight knight;

	void Start(){
        //knight = FindGameObjectWithTag("Knight").GetComponent<Inventory>();
	}

	public void cast(){
		switch(type){
			case "Heal.25":
				//knight.health += knight.maxHealth*.25f;
				//if (knight.maxHealth < knight.health) {
					//knight.health = knight.maxHealth;
				//}
				break;
			case "Heal.5":
				//knight.health += knight.maxHealth*.5f;
				//if (knight.maxHealth < knight.health) {
					//knight.health = knight.maxHealth;
				//}
				
				break;
			case "Heal.75":
				//knight.health += knight.maxHealth/0.75f;
				//if (knight.maxHealth < knight.health) {
					//knight.health = knight.maxHealth;
				//}
				break;
		}
		

	}

}
