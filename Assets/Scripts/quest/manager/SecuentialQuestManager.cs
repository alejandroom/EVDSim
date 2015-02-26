using UnityEngine;
using System.Collections;
using System;

namespace edvsim.quest.manager
{
	public class SecuentialQuestManager : QuestManager{
		private bool started;
		private bool finished;
		private int actualQuest;

		private bool showAllQuests;
		
		public SecuentialQuestManager(ArrayList quests){
			this.quests = quests;
			started = false;
			finished = false;
			showAllQuests = false;
			actualQuest = 0;
		}
		
		override public void start (){
			if(!started){
				started = true;
				foreach (Quest q in quests) {
					q.Done += new Quest.DoneHandler(finishedQuest);
				}
				actualQuest = 0;
				(quests[actualQuest] as Quest).start();
			}
		}
		
		override public void finish (){
			if(started){
				finished = true;
				reportDone(this);
			}
		}
		
		override public ArrayList getActiveQuests (){
			if (showAllQuests)
				return quests;
			ArrayList activeQuests = new ArrayList ();
			if (!finished)
				activeQuests.Add (quests [actualQuest]);
			return activeQuests;
		}
		
		void finishedQuest(Quest q, EventArgs e) {
			if (started && (quests[actualQuest] as Quest).Equals(q)){
				actualQuest++;
				if(actualQuest==quests.Count){
					finish ();
				}else{
					(quests[actualQuest] as Quest).start();
				}
			}
		}

		public override string ToString(){
			if (finished)
				return "Todas las quests terminadas";
			return (quests [actualQuest] as Quest).ToString ();
		}
	}
}