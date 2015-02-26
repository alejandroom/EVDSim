using UnityEngine;
using System.Collections;

namespace edvsim.activable
{
	public class ActivableParticleSystem : Activable {

		private ParticleSystem[] systems;

		void Awake () {
			systems = this.GetComponentsInChildren<ParticleSystem> ();
			
			foreach (ParticleSystem system in systems){
				Rotator rot = system.gameObject.AddComponent<Rotator> ();
				rot.speed = 1.0f;
				rot.rotation = new Vector3 (0, 0, 1);
			}
		}
		
		public override bool activate(){
			foreach (ParticleSystem system in systems)
				system.Play ();
			return true;
		}
		
		public override bool deactivate(){
			foreach (ParticleSystem system in systems)
				system.Stop ();
			return true;
		}
	}
}