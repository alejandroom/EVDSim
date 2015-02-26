using UnityEngine;
using System.Collections;

namespace edvsim.activable
{
	public class ActivableOutline : Activable {

		private Shader originalShader;
		private Shader activeShader;

		private Renderer[] renderers;

		void Awake () {
			activeShader = Shader.Find ("Toon/Lighted Outline");
			renderers = GetComponentsInChildren<Renderer> ();
			originalShader = renderers[0].material.shader;
		}
		
		public override bool activate(){
			foreach(Renderer ren in renderers)
				ren.material.shader = activeShader;
			return true;
		}
		
		public override bool deactivate(){
			foreach(Renderer ren in renderers)
				ren.material.shader = originalShader;
			return true;
		}
	}
}