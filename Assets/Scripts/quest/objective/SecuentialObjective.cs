using UnityEngine;
using System.Collections;
using System;

namespace edvsim.quest.objective
{
	public class SecuentialObjective : Objective {
		
		public ArrayList objectives;
		private int actualObjective;
		
		public bool showCompletedObjectives;
		public bool showTitle;
		public bool showFutureObjectives;
		
		public SecuentialObjective(ArrayList objectives, string description, string descriptionCompleted, bool showCompletedObjectives, bool showTitle, bool showFutureObjectives){
			this.objectives = objectives;
			this.description = description;
			this.showCompletedObjectives = showCompletedObjectives;
			this.showTitle = showTitle;
			this.descriptionCompleted = descriptionCompleted;
			this.showFutureObjectives = showFutureObjectives;
		}

		override public void activate (){
			if(!active){
				active = true;
				done = false;
				foreach (Objective o in objectives) {
					o.deactivate();
					o.Done += new Objective.DoneHandler(finishedObjective);
				}
				actualObjective = 0;
				(objectives[actualObjective] as Objective).activate();
			}
		}
		
		override public void deactivate (){
			if(active){
				active = false;
				foreach (Objective o in objectives) {
					o.deactivate();
					o.Done -= new Objective.DoneHandler(finishedObjective);
				}
			}
		}
		
		void finishedObjective(Objective o, EventArgs e) {
			if (active && !done){
				if((objectives[actualObjective] as Objective).Equals(o)){
					actualObjective++;
					if(actualObjective==objectives.Count){
						done = true;
						deactivate ();
						reportDone(this);
					}else{
						(objectives[actualObjective] as Objective).activate();
					}
				}
			}
		}
		
		public override string ToString(){
			string desc = getCompleteDescription() + "\n{";
			foreach (Objective o in objectives) {
				if((objectives[actualObjective] as Objective).Equals(o)){
					desc += "\nACTUAL:"+o.ToString();
				}else{
					desc += "\n"+o.ToString();
				}
			}
			desc += "\n}";
			return desc;
		}
	}
	
	public class SecuentialObjectiveBuilder {
		private ArrayList objectives;
		private bool showTitle;
		private bool showCompletedObjectives;
		private bool showFutureObjectives;
		private string descriptionComp;
		
		public SecuentialObjectiveBuilder(){
			objectives = new ArrayList ();
			showTitle = true;
			showCompletedObjectives = true;
			showFutureObjectives = true;
			descriptionComp = "";
		}
		
		public SecuentialObjectiveBuilder objective(Objective o){
			if(!objectives.Contains(o))
				objectives.Add (o);
			
			return this;
		}
		
		public SecuentialObjectiveBuilder title(bool title){
			this.showTitle = title;
			
			return this;
		}
		
		public SecuentialObjectiveBuilder completedObjectives(bool co){
			this.showCompletedObjectives = co;
			
			return this;
		}
		
		public SecuentialObjectiveBuilder futureObjectives(bool co){
			this.showFutureObjectives = co;
			
			return this;
		}
		
		public SecuentialObjectiveBuilder descriptionCompleted(string descriptionCompleted){
			this.descriptionComp = descriptionCompleted;
			
			return this;
		}
		
		public SecuentialObjective build(string description){
			return new SecuentialObjective(objectives, description, descriptionComp, showCompletedObjectives, showTitle, showFutureObjectives);
		}
	}
}