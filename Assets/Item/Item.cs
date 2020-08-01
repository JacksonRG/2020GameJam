using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
	public int maxNumber;
	public string type;
	private GameObject knight;

	void Start(){
        knight = GameObject.FindGameObjectWithTag("Knight").GetComponent<Inventory>();
	}

	void cast(){
		switch(type){
			case "Heal.25":
				knight.health += knight.maxHealth/4;
				if (knight.maxHealth < knight.health) {
					knight.health = knight.maxHealth;
				}
				break;
			case "Heal.5":
				knight.health += knight.maxHealth/2;
				if (knight.maxHealth < knight.health) {
					knight.health = knight.maxHealth;
				}
				break;
			case "Heal.5":
				knight.health += knight.maxHealth/2;
				if (knight.maxHealth < knight.health) {
					knight.health = knight.maxHealth;
				}
				break;
		}
		

	}

}
