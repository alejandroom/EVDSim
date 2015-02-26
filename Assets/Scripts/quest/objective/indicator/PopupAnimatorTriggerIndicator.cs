using UnityEngine;
using UnityEngine.UI;
using System.Collections;

using edvsim.activable;

namespace edvsim.quest.objective.indicator
{
	public class PopupAnimatorTriggerIndicator : ObjectiveIndicator {
		
		private Activable[] acts;

		private bool onTrigger;

		private PopUpController puc;

		private LiteralManager lm;

		private ChatController cc;

		private ArrayList messages = new ArrayList();

		private int pos = 0;


		public Animator animator;

		void Start () {
			lm = LiteralManager.getInstance ();
			onTrigger = false;
			active = false;
			foreach(Activable act in acts)
				act.deactivate ();
			puc = GameObject.FindObjectOfType<PopUpController> ();
			cc = GameObject.FindObjectOfType<ChatController> ();
		}

		public void setMessage(string message){
			messages.Add (message);
		}

		void Awake() {
			acts = this.GetComponentsInChildren<Activable> ();
		}
		
		override protected void internalActivate () {
			if(!active){
				onTrigger = false;
				active = true;
				foreach(Activable act in acts)
					act.activate ();
			}
		}
		
		override public void deactivate () {
			if (active) {
				active = false;
				onTrigger = false;
				foreach(Activable act in acts)
					act.deactivate ();
				puc.hidePopup();
			}
		}
		
		void OnTriggerStay(Collider other) {
			if(active && !onTrigger){
				onTrigger = true;
				puc.showPopup(lm.getValue(description), "F");
			}
		}
		
		void OnTriggerExit(Collider other) {
			if(onTrigger){
				onTrigger = false;
				puc.hidePopup();
			}
		}
		
		new void Update () {
			base.Update ();
			if (onTrigger && Input.GetKey(KeyCode.F)) {
				foreach(Activable act in acts)
					act.deactivate ();
				if(messages.Count != 0){
					cc.writeMessage(lm.getValue((messages[pos] as string)));
					pos = pos + 1 % messages.Count;
				}
				reportDone(this);
				animator.Play("Talk");
			}
		}
	}
}