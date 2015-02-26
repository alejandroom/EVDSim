using UnityEngine;
using System.Collections;
using System;

namespace edvsim.quest.manager
{
	public abstract class QuestManager {
		protected ArrayList quests;
		
		public event DoneHandler Done;
		public delegate void DoneHandler(QuestManager o, EventArgs e);

		public abstract void start ();
		public abstract void finish ();
		
		public abstract ArrayList getActiveQuests ();

		public void reportDone (QuestManager oi) {
			Done (oi, null);
		}
	}
	
	public class QuestManagerBuilder {
		private ArrayList quests;
		
		public QuestManagerBuilder(){
			quests = new ArrayList ();
		}
		
		public QuestManagerBuilder quest(Quest o){
			if(!quests.Contains(o))
				quests.Add (o);
			
			return this;
		}
		
		public SecuentialQuestManager buildSequential(){
			return new SecuentialQuestManager(quests);
		}
	}
}