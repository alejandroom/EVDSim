using System.Collections;
using System;

namespace edvsim.quest.objective
{
	public class ParallelObjective : Objective {

		public ArrayList objectives;
		
		public bool showCompletedObjectives;
		public bool showTitle;
		
		public ParallelObjective(ArrayList objectives, string description, string descriptionCompleted, bool showCompletedObjectives, bool showTitle){
			this.objectives = objectives;
			this.description = description;
			this.showCompletedObjectives = showCompletedObjectives;
			this.showTitle = showTitle;
			this.descriptionCompleted = descriptionCompleted;
		}
		
		override public void activate (){
			if(!active){
				active = true;
				done = false;
				foreach (Objective o in objectives) {
					o.activate();
					o.Done += new Objective.DoneHandler(finishedObjective);
				}
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
			if (active && !done && allObjectivesComplete()){
				done = true;
				deactivate ();
				reportDone(this);
			}
		}

		private bool allObjectivesComplete(){
			bool allComplete = true;
			foreach (Objective o in objectives) {
				allComplete = allComplete && o.isDone();
			}
			return allComplete;
		}

		public override string ToString(){
			string desc = getCompleteDescription() + "\n{";
			foreach (Objective o in objectives) {
				desc += "\n"+o.ToString();
			}
			desc += "\n}";
			return desc;
		}
	}
	
	public class ParallelObjectiveBuilder {
		private ArrayList objectives;
		private bool showTitle;
		private bool showCompletedObjectives;
		private string descriptionComp;

		public ParallelObjectiveBuilder(){
			objectives = new ArrayList ();
			showTitle = true;
			showCompletedObjectives = true;
			descriptionComp = "";
		}
		
		public ParallelObjectiveBuilder objective(Objective o){
			if(!objectives.Contains(o))
				objectives.Add (o);
			
			return this;
		}
		
		public ParallelObjectiveBuilder title(bool title){
			this.showTitle = title;
			
			return this;
		}
		
		public ParallelObjectiveBuilder completedObjectives(bool co){
			this.showCompletedObjectives = co;
			
			return this;
		}
		
		public ParallelObjectiveBuilder descriptionCompleted(string descriptionCompleted){
			this.descriptionComp = descriptionCompleted;
			
			return this;
		}
		
		public ParallelObjective build(string description){
			return new ParallelObjective(objectives, description, descriptionComp, showCompletedObjectives, showTitle);
		}
	}
}
