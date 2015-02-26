using UnityEngine;
using System.Collections;
using System;

using edvsim.quest.objective;

namespace edvsim.quest
{
	public class Quest {
		public string description;
		public bool active;
		public bool done;

		public event DoneHandler Done;
		public delegate void DoneHandler(Quest o, EventArgs e);

		private Objective objective;

		public Quest(Objective o, string description){
			this.objective = o;
			this.description = description;
			this.active = false;
		}
		
		public void start(){
			if (!active) {
				active = true;
				done = false;
				objective.activate();
				objective.Done += new Objective.DoneHandler(finishedObjective);
			}
		}
		
		void finishedObjective(Objective o, EventArgs e) {
			if (active && !done) {
				done = true;
				Done(this, null);
			}
		}
		
		public override string ToString(){
			if(done)
				return "Finished Quest: " + description;
			return "Quest:"+description+"\n"+objective.ToString();
		}

		public Objective getObjective(){
			return objective;
		}
	}
}
