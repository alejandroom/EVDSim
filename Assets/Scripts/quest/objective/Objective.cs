using UnityEngine;
using System.Collections;
using System;

namespace edvsim.quest.objective
{
	public abstract class Objective {
		protected string description;
		protected string descriptionCompleted; 
		protected bool active;
		protected bool done;
		
		public event DoneHandler Done;
		public delegate void DoneHandler(Objective o, EventArgs e);

		abstract public void activate ();
		abstract public void deactivate ();
		
		public bool isDone(){
			return done;
		}

		public bool isActive(){
			return active;
		}
		
		public string getDescription() {
			return description;
		}
		
		public string getDescriptionCompleted() {
			if(descriptionCompleted == null || descriptionCompleted == "")
				return description;
			else
				return descriptionCompleted;
		}

		protected void reportDone(Objective o){
			Done (o, null);
		}
		
		public string getCompleteDescription(){
			string desc = description;
			if (active)
				desc = desc.ToUpper ();
			if (done)
				desc += " - Done";
			return desc;
		}
	}
}