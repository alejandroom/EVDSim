using UnityEngine;
using System.Collections.Generic;

using edvsim.quest.objective.indicator;

public class GameObjectManager : MonoBehaviour {

	private Dictionary<string, ObjectiveIndicator> indicators;

	void Awake () {
		this.indicators = getObjectiveIndicators ();
	}
	
	private Dictionary<string, ObjectiveIndicator> getObjectiveIndicators(){
		Dictionary<string, ObjectiveIndicator> inds = new Dictionary<string, ObjectiveIndicator> ();

		foreach (ObjectiveIndicator oi in GameObject.FindObjectsOfType<ObjectiveIndicator> ()) {
			inds.Add(oi.code, oi);
		}

		return inds;
	}

	public ObjectiveIndicator getIndicator(string code){
		if (!indicators.ContainsKey (code)) {
			Debug.Log("No encontrado OI de código: " + code);
			return null;
		}
		return indicators[code];
	}
}
