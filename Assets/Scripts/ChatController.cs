using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ChatController : MonoBehaviour {
	
	public Image chatBox;
	public Text chat;
	
	private Color initialChatBoxColor;
	private Color initialChatColor;
	
	private float fadeTime = 5.0f;
	private float fadeCD;

	void Start () {
		chat.text = "";
		initialChatBoxColor = chatBox.color;
		initialChatColor = chat.color;
		fade ();
	}

	public void writeMessage(string message){
		if(message != null && message != ""){
			chat.text = message;
			fadeCD = Time.realtimeSinceStartup;
			unfade();
		}
	}

	void Update(){
		if (fadeCD + fadeTime < Time.realtimeSinceStartup) {
			fade();
		}
	}
	
	private void fade(){
		chatBox.color = new Color (initialChatBoxColor.r, initialChatBoxColor.g, initialChatBoxColor.b, 0.2f);
		chat.color = new Color (initialChatColor.r, initialChatColor.g, initialChatColor.b, 0.2f);
	}
	
	private void unfade(){
		chatBox.color = initialChatBoxColor;
		chat.color = initialChatColor;
	}
}
