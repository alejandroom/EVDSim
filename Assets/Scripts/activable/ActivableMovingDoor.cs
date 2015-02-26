using UnityEngine;
using System.Collections;

namespace edvsim.activable
{
	public class ActivableMovingDoor : Activable  {

		public Vector3 moveDirection;
		public float speed;
		public float movingTime;

		private float openingCD;
		private bool opening;
		private bool open;

		void Start () {
			open = false;
			opening = false;
		}

		void Update () {
			if(!open){
				if (opening && openingCD + movingTime < Time.realtimeSinceStartup){
					opening = false;
					open = true;
				}
				if (opening){
					this.transform.position += moveDirection * speed;
				}
			}
		}
		
		public override bool activate(){
			if(!open){
				opening = true;
				openingCD = Time.realtimeSinceStartup;
			}
			return !open;
		}
		
		public override bool deactivate(){
			opening = false;
			return true;
		}
	}
}