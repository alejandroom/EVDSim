using UnityEngine;
using System.Collections;
using System;

using edvsim.quest.objective.indicator;

namespace edvsim.quest.objective
{
	public class BasicObjective : Objective {
		private ArrayList objectiveIndicators;
		private string popupMessage;

		public BasicObjective(ArrayList objectiveIndicators, string description, string descriptionCompleted, string popupMessage){
			this.objectiveIndicators = objectiveIndicators;
			this.description = description;
			this.descriptionCompleted = descriptionCompleted;
			this.popupMessage = popupMessage;
		}

		override public void activate () {
			if(!active){
				active = true;
				done = false;
				foreach(ObjectiveIndicator o in objectiveIndicators){
					o.description = popupMessage;
					o.activate();
					o.Done += new ObjectiveIndicator.DoneHandler(finishedObjective);
				}
			}
		}
		
		override public void deactivate () {
			if(active){
				active = false;
				foreach(ObjectiveIndicator o in objectiveIndicators){
					o.deactivate();
					o.Done -= new ObjectiveIndicator.DoneHandler(finishedObjective);
				}
			}
		}
		
		void finishedObjective(ObjectiveIndicator o, EventArgs e) {
			if (active && !done) {
				done = true;
				deactivate ();
				reportDone(this);

			}
		}
		
		public override string ToString(){
			return getCompleteDescription();
		}
	}

	public class BasicObjectiveBuilder {
		private ArrayList objectiveIndicators;
		private string descriptionCompl;
		private string popupMessage;
		
		public BasicObjectiveBuilder(){
			objectiveIndicators = new ArrayList ();
			descriptionCompl = "";
			popupMessage = "";
		}
		
		public BasicObjectiveBuilder indicator(ObjectiveIndicator o){
			if(!objectiveIndicators.Contains(o))
				objectiveIndicators.Add (o);
			
			return this;
		}
		
		public BasicObjectiveBuilder message(string o){
			this.popupMessage = o;
			
			return this;
		}
		
		public BasicObjectiveBuilder descriptionCompleted(string descriptionCompleted){
			this.descriptionCompl = descriptionCompleted;
			return this;
		}
		
		public BasicObjective build(string description){
			return new BasicObjective(objectiveIndicators, description, descriptionCompl, popupMessage);
		}
	}
}