using UnityEngine;
using System.Collections;

using edvsim.activable;

public class Trigger : Activable {
	
	public Activable act;

	private new bool active = false;

	void OnTriggerEnter(Collider other) {
		if(active)
			act.activate ();
	}
	
	public override bool activate(){
		active = true;
		return true;
	}
	
	public override bool deactivate(){
		active = false;
		return true;
	}
}
