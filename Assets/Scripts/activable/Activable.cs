using UnityEngine;
using System.Collections;

namespace edvsim.activable
{
	public abstract class Activable : MonoBehaviour{
		public abstract bool activate();	
		public abstract bool deactivate();	
	}
}
