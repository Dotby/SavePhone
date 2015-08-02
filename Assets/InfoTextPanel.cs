using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InfoTextPanel : MonoBehaviour
{
	int mode = 0;
	float counter;
	Color surf;
	string newText = ""; 

	void Start(){
		transform.GetComponent<Image>().color = new Color(surf.r, surf.g, surf.b, 0f);
		transform.GetComponentInChildren<Text>().color = new Color(255f, 255f, 255f, 0f);
		surf = transform.GetChild(0).transform.GetChild(0).GetComponent<Image>().color;
	}

	public void HideSelf(){
		counter = .7f;
		mode = 2;
	}

	public void ShowSelf(){
		transform.GetComponentInChildren<Text>().text = newText;
		newText = "";
		counter =.0f;
		mode = 1;
		transform.GetComponent<Image>().color = new Color(surf.r, surf.g, surf.b, 0f);
		transform.GetComponentInChildren<Text>().color = new Color(255f, 255f, 255f, 0f);
	}

	public void ShowText(string txt){
		HideSelf();
		newText = txt;
	}

	public void Reshow(){
		counter = 0f;
		mode = 1;
	}
	
	void Update()
	{
		if (mode == 1){
			if (counter < 0.7f){
				counter += 0.04f;
				transform.GetComponent<Image>().color = new Color(surf.r, surf.g, surf.b, counter);
				transform.GetComponentInChildren<Text>().color = new Color(255f, 255f, 255f, counter);
			}
			else{
				transform.GetComponentInChildren<Text>().color = new Color(255f, 255f, 255f, 1f);
				mode = 0;
			}
		}

		if (mode == 2){
			if (counter > 0f){
				counter -= 0.04f;
				transform.GetComponent<Image>().color = new Color(surf.r, surf.g, surf.b, counter);
				transform.GetComponentInChildren<Text>().color = new Color(255f, 255f, 255f, counter);
			}
			else{
				if (newText != ""){
					ShowSelf();
				}else{
					mode = 0;
				}
				//gameObject.SetActive(false);
			}
		}
	}
}