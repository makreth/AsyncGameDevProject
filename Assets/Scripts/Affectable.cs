using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Base class for objects like doors that can be affected by buttons
public abstract class Affectable : MonoBehaviour
{
	public abstract void Trigger();
}
