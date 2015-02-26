using UnityEngine;
using System.Collections;

using edvsim.activable;

namespace edvsim.quest.objective.indicator
{
	public class TriggerIndicator : ObjectiveIndicator {

		private Activable[] acts;
		
		void Start () {
			foreach(Activable act in acts)
				act.deactivate ();
		}
		
		void Awake() {
			active = false;
			acts = this.GetComponentsInChildren<Activable> ();
		}
		
		override protected void internalActivate () {
			if(!active){
				active = true;
				foreach(Activable act in acts)
					act.activate ();
			}
		}
		
		override public void deactivate () {
			if (active) {
				active = false;
				foreach(Activable act in acts)
					act.deactivate ();
			}
		}
		
		void OnTriggerStay(Collider other) {
			if(active){
				foreach(Activable act in acts)
					act.deactivate ();
				reportDone (this);
			}
		}
	}
}
