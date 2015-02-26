using UnityEngine;
using System.Collections;

public class MenuController : MonoBehaviour {

	private float cameraRotationSpeed;
	
	private float logoWidth;
	private float logoHeight;
	public Texture2D logo;
	
	private float languageWidth;
	private float languageHeight;
	public Texture2D englishTexture;
	public Texture2D spanishTexture;

	private bool spanish;
	private LiteralManager lm;

	void Awake() {
		this.cameraRotationSpeed = 5.0f;
		
		this.logoWidth = 350.0f;
		this.logoHeight = 150.0f;
		
		this.languageWidth = 100.0f;
		this.languageHeight = 50.0f;

		this.spanish = true;
		this.lm = LiteralManager.getInstance ();
		lm.setLocale ("ES");
	}

	void Start () {
	
	}

	void Update () {
		this.transform.Rotate(Vector3.up * Time.deltaTime * cameraRotationSpeed);
	}

	void OnGUI() {
		if (GUI.Button (new Rect ((Screen.width - this.logoWidth) / 2, (Screen.height - this.logoHeight) / 2, this.logoWidth, this.logoHeight), this.logo)) {
			Application.LoadLevel("EVDSim");
		}

		Texture2D languageTexture = spanish ? spanishTexture : englishTexture;

		if (GUI.Button (new Rect ((Screen.width - this.languageWidth) - (Screen.width/10), (Screen.height - this.languageHeight) - (Screen.height/10), this.languageWidth, this.languageHeight), languageTexture)) {
			lm.setLocale (spanish ? "EN" : "ES");
			spanish = !spanish;
		}
	}
}