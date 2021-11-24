using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Base class for objects like doors that can be affected by buttons
public abstract class Affectable : MonoBehaviour
{
	protected GameObject triggeringObject;
	protected bool activeFlag;
	public abstract void Trigger();

	public void SetTriggeringObject(GameObject obj){
		triggeringObject = obj;
	}

	public GameObject GetTriggeringObject(){
		return triggeringObject;
	}
}
