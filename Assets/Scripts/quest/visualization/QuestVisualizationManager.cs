using UnityEngine;
using UnityEngine.UI;
using System.Collections;

using edvsim.quest.manager;
using edvsim.quest.objective;

namespace edvsim.quest.visualization
{
	public class QuestVisualizationManager : MonoBehaviour {
		
		public Transform contentPanel;
		public GameObject questTitle;
		public GameObject basicObjective;
		public GameObject multipleObjective;
		public Color objectiveDone;
		public Color objectiveNotDone;

		private ArrayList panels;

		private GameManager qm;
		private LiteralManager lm;

		void Start () {
			qm = GameObject.FindObjectOfType<GameManager> ();
			panels = new ArrayList ();
			lm = LiteralManager.getInstance ();
		}

		void Update () {
			generateQuestPanel (qm.getActiveQuests ());
		}

		private void generateQuestPanel(ArrayList quests){
			destroyPanels ();
			foreach (Quest q in quests) {
				GameObject questPanel = Instantiate(questTitle) as GameObject;
				Text text = questPanel.GetComponentInChildren<Text>();
				text.text = lm.getValue(q.description);
				questPanel.transform.SetParent (contentPanel);
				panels.Add(questPanel);

				addObjective(q.getObjective(), questPanel);
			}
		}

		private void destroyPanels(){
			foreach (GameObject go in panels) {
				Destroy(go);
			}
			panels.Clear ();
		}
		
		private GameObject generateBasicObjectivePanel(Objective o){
			GameObject questObjectivePanel = Instantiate(basicObjective) as GameObject;
			Text text = questObjectivePanel.GetComponentInChildren<Text>();
			if(o.isDone()){
				text.color = objectiveDone;
				text.text = lm.getValue(o.getDescriptionCompleted());
			}else{
				text.color = objectiveNotDone;
				text.text = lm.getValue(o.getDescription());
			}
			if(o.isActive())
				text.fontStyle = FontStyle.Bold;
			else
				text.fontStyle = FontStyle.Normal;
			
			return questObjectivePanel;
		}
		
		private GameObject generateMultipleObjectivePanel(Objective o){
			GameObject questObjectivePanel = Instantiate(multipleObjective) as GameObject;
			Text text = questObjectivePanel.GetComponentInChildren<Text>();
			if(o.isDone()){
				text.color = objectiveDone;
				text.text = lm.getValue(o.getDescriptionCompleted());
			}else{
				text.color = objectiveNotDone;
				text.text = lm.getValue(o.getDescription());
			}
			if(o.isActive())
				text.fontStyle = FontStyle.Bold;
			else
				text.fontStyle = FontStyle.Normal;
			
			if (o is ParallelObjective && !(o as ParallelObjective).showTitle)
				Destroy (text);
			if (o is SecuentialObjective && !(o as SecuentialObjective).showTitle)
				Destroy (text);
			
			return questObjectivePanel;
		}

		private void addObjective(Objective o, GameObject panel){
			if (o is ParallelObjective) {
				ParallelObjective po = (o as ParallelObjective);
				GameObject questObjectivePanel = generateMultipleObjectivePanel(po);
				questObjectivePanel.transform.SetParent (panel.transform);
				panels.Add(questObjectivePanel);
				foreach  (Objective ob in po.objectives){
					if((po.showCompletedObjectives && ob.isDone()) || !ob.isDone())
						addObjective(ob, questObjectivePanel);
				}
			}else if (o is SecuentialObjective) {
				SecuentialObjective so = (o as SecuentialObjective);
				GameObject questObjectivePanel = generateMultipleObjectivePanel(so);
				questObjectivePanel.transform.SetParent (panel.transform);
				panels.Add(questObjectivePanel);
				foreach  (Objective ob in so.objectives){
					if((so.showCompletedObjectives && ob.isDone()) || !ob.isDone())
						addObjective(ob, questObjectivePanel);
					if(!ob.isDone() && !so.showFutureObjectives)
						break;
				}
			}else if(o is BasicObjective) {
				BasicObjective bo = (o as BasicObjective);

				GameObject questObjectivePanel = generateBasicObjectivePanel(bo);
				questObjectivePanel.transform.SetParent (panel.transform);
				panels.Add(questObjectivePanel);
			}
		}
	}
}