using UnityEngine;
using System.Collections;
using System;

namespace edvsim.quest.objective.indicator
{
	public abstract class ObjectiveIndicator : MonoBehaviour {
		public string code;
		public string description;
		protected new bool active;
		
		public event DoneHandler Done;
		public delegate void DoneHandler(ObjectiveIndicator o, EventArgs e);

		private bool activating;
		private float cd;
		private float timeUntilActivate = 0.1f;

		abstract protected void internalActivate ();
		abstract public void deactivate ();

		public void activate(){
			if (!activating) {
				activating = true;
				cd = Time.realtimeSinceStartup;
			}
		}
		
		public void Update(){
			if(activating && cd + timeUntilActivate < Time.realtimeSinceStartup){
				activating = false;
				internalActivate();
			}
		}

		public void reportDone (ObjectiveIndicator oi) {
			Done (oi, null);
		}
	}
}
