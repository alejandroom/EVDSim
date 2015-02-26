using UnityEngine;
using System.Collections;
using System.IO;
using System.Xml;
using System.Collections.Generic;
using System;

using edvsim.quest.objective;
using edvsim.quest.objective.indicator;

namespace edvsim.quest
{
	public class QuestFactory {

		private GameObjectManager gom;
		private Dictionary<string, Quest>  quests;

		public QuestFactory(GameObjectManager gom){
			this.gom = gom;
			this.quests = loadQuestsFromFile ("Files/Quests/questsEVDSim");
			//this.quests = loadQuestsFromFile ("Files/Quests/questsPrueba");
		}

		public Quest build(int numQuest){
			if(quests.ContainsKey(numQuest.ToString()))
				return quests [numQuest.ToString()];

			switch (numQuest) {
			case 1:
				return buildFirstQuest();
			case 2:
				return buildSecondQuest();
			default:
				return buildFirstQuest();
			}
		}
		
		private Quest buildFirstQuest(){
			Objective firstBasicObjective = new BasicObjectiveBuilder ().indicator (gom.getIndicator ("OI1")).descriptionCompleted("Puerta abierta!").build ("Abre la puerta");
			Objective secondBasicObjective = new BasicObjectiveBuilder ().indicator (gom.getIndicator ("OI2")).build ("Accede al marcador 2");
			
			Objective mainObjective = new SecuentialObjectiveBuilder ().objective (firstBasicObjective).objective(secondBasicObjective).build("Probar objetivos secuenciales");
			
			return new Quest(mainObjective, "Primera Quest");
		}
		
		private Quest buildSecondQuest(){
			Objective firstBasicObjective = new BasicObjectiveBuilder ().indicator (gom.getIndicator ("OI3")).build ("Accede al marcador 3");
			Objective secondBasicObjective = new BasicObjectiveBuilder ().indicator (gom.getIndicator ("OI4")).build ("Accede al marcador 4");
			
			Objective mainObjective = new ParallelObjectiveBuilder ().objective (firstBasicObjective).objective(secondBasicObjective).completedObjectives(false).build("Probar objetivos paralelos");
			
			return new Quest(mainObjective, "Segunda Quest");
		}

		private Dictionary<string, Quest> loadQuestsFromFile(string file){
			Dictionary<string, Quest> quests = new Dictionary<string, Quest> ();

			TextAsset textAsset = (TextAsset) Resources.Load(file); 
			XmlDocument xmlDoc = new XmlDocument();
			xmlDoc.LoadXml(textAsset.text);

			foreach (XmlNode xmlNode in xmlDoc.DocumentElement.ChildNodes) {
				quests.Add(xmlNode.Attributes["id"].Value, processQuest(xmlNode));
			}

			return quests;
		}

		private Quest processQuest(XmlNode node){
			string questTitle = node ["title"].InnerText;
			return new Quest( processObjective(node ["objective"].FirstChild), questTitle);
		}

		private Objective processObjective(XmlNode node){
			switch (node.Name) {
			case "parallelObjective":
				return processParallelObjective(node);
			case "secuentialObjective":
				return processSecuentialObjective(node);
			case "basicObjective":
				return processBasicObjective(node);
			default:
				return null;
			}
		}
		
		private ParallelObjective processParallelObjective(XmlNode node){
			ParallelObjectiveBuilder builder = new ParallelObjectiveBuilder ();
			
			if (node ["showCompleted"] != null)
				builder.completedObjectives (Convert.ToBoolean(node ["showCompleted"].InnerText));
			
			if (node ["showTitle"] != null)
				builder.title (Convert.ToBoolean(node ["showTitle"].InnerText));

			if(node ["descriptionCompleted"] != null)
				builder.descriptionCompleted (node ["descriptionCompleted"].InnerText);

			foreach (XmlNode n in node["objectives"].ChildNodes){
				builder.objective (processObjective(n));
			}

			return builder.build (node["description"].InnerText);
		}
		
		private SecuentialObjective processSecuentialObjective(XmlNode node){
			SecuentialObjectiveBuilder builder = new SecuentialObjectiveBuilder ();
			
			if (node ["showCompleted"] != null)
				builder.completedObjectives (Convert.ToBoolean(node ["showCompleted"].InnerText));
			
			if (node ["showTitle"] != null)
				builder.title (Convert.ToBoolean(node ["showTitle"].InnerText));
			
			if (node ["showFuture"] != null)
				builder.futureObjectives (Convert.ToBoolean(node ["showFuture"].InnerText));
			
			if(node ["descriptionCompleted"] != null)
				builder.descriptionCompleted (node ["descriptionCompleted"].InnerText);
			
			foreach (XmlNode n in node["objectives"].ChildNodes){
				builder.objective (processObjective(n));
			}
			
			return builder.build (node["description"].InnerText);
		}
		
		private BasicObjective processBasicObjective(XmlNode node){
			BasicObjectiveBuilder builder = new BasicObjectiveBuilder ();
			
			if(node ["descriptionCompleted"] != null)
				builder.descriptionCompleted (node ["descriptionCompleted"].InnerText);
			
			if(node ["message"] != null)
				builder.message (node ["message"].InnerText);
			
			foreach (XmlNode n in node["indicators"].ChildNodes){
				ObjectiveIndicator oi = gom.getIndicator (n.InnerText);
				if(n.Attributes["chat"] != null && oi is PopupTriggerIndicator)
					(oi as PopupTriggerIndicator).setMessage(n.Attributes["chat"].Value);
				if(n.Attributes["chat"] != null && oi is PopupAnimatorTriggerIndicator)
					(oi as PopupAnimatorTriggerIndicator).setMessage(n.Attributes["chat"].Value);
				builder.indicator (oi);
			}

			return builder.build (node["description"].InnerText);
		}	
	}
}





