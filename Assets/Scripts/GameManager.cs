using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

using edvsim.quest.manager;

public class GameManager : MonoBehaviour {

	private QuestManager qm;
	private GameObjectManager gom;
	private CharacterMotor cc;
	private MouseLook[] mouseLooks;
	private LiteralManager lm;
	private PopUpController puc;

	private bool finished;

	void Start () {
		gom = GameObject.FindObjectOfType<GameObjectManager> ();
		cc = GameObject.FindObjectOfType<CharacterMotor> ();
		mouseLooks = GameObject.FindObjectsOfType<MouseLook> ();
		puc = GameObject.FindObjectOfType<PopUpController> ();
		lm = LiteralManager.getInstance ();

		qm = new QuestManagerFactory (gom).build (1);
		qm.start();
		qm.Done += new QuestManager.DoneHandler(finishedQM);
		
		puc.hidePopup ();

		activateCursor (false);
		finished = false;
	}
	
	private void finishedQM(QuestManager questManager, EventArgs e){
		activateCursor(true);
		finished = true;
		puc.showPopup(lm.getValue("exit.success.message"));
	}
	
	public ArrayList getActiveQuests(){
		return qm.getActiveQuests ();
	}
	
	void Update(){
		if(!finished){
			if (Input.GetKeyDown (KeyCode.LeftAlt) || Input.GetKeyDown (KeyCode.RightAlt)) {
				activateCursor(true);
			}
			if (Input.GetKeyUp (KeyCode.LeftAlt) || Input.GetKeyUp (KeyCode.RightAlt)) {
				activateCursor(false);
			}
		}else{
			if (Input.GetKeyUp (KeyCode.Escape)) {
				Application.LoadLevel("MainMenu");
			}
		}
	}

	private void activateCursor(bool active){
		cc.canControl = !active;
		Screen.showCursor = active;
		foreach (MouseLook ml in mouseLooks) {
			ml.enabled = !active;
		}
	}
}
