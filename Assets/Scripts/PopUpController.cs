using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PopUpController : MonoBehaviour {

	public Image popupPanel;
	public Text popupText;
	public Image popupConfirmationPanel;
	public Button popupConfirmationButton;
	public Text popupConfirmationText;

	void Start () {
		hidePopup ();
	}
	
	public void hidePopup(){
		popupPanel.enabled = false;
		popupText.text = "";
		popupConfirmationPanel.enabled = false;
		popupConfirmationText.text = "";
	}
	
	public void showPopup(string text, string button){
		popupPanel.enabled = true;
		popupText.text = text;
		popupConfirmationPanel.enabled = true;
		popupConfirmationText.text = button;
	}
	
	public void showPopup(string text){
		popupPanel.enabled = true;
		popupText.text = text;
	}
}
