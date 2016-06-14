﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

	public Text finishTimeText;
	public Image whiteFadeImage, headImage;
	public GameObject startPanel, finishPanel, wallMenuBackground;
	public Transform walls;
	public UnityStandardAssets.Characters.FirstPerson.FirstPersonController fpsController;
	float fadeToWhiteSpeed = 6f;

	private bool fadingToClear = false;
	private bool movingWalls = false;
	private bool fadingToWhite = false;
	private bool movingHead = false;
	private bool fadingHead = false;
	public float time = 0;
	private bool countingTime = false;


	public void ShowStartPanel() {
		wallMenuBackground.SetActive(true);
		startPanel.SetActive(true);
	}

	public void ShowFinishPanel() {
		wallMenuBackground.SetActive(true);
		finishPanel.SetActive(true);
		finishTimeText.gameObject.SetActive(true);
		countingTime = false;
// 		string.Format("IsLoggedIn='{0}' IsInitialized='{1}'",
// 		var floatNumber = 12.5523;
// var x = floatNumber - Math.Truncate(floatNumber);
		System.Globalization.NumberFormatInfo nfi = new System.Globalization.NumberFormatInfo();
		nfi.NumberDecimalSeparator = ":";
		// nfi.NumberDecimalDigits = 2;
		// nfi.number
		finishTimeText.text = string.Format("Поздравляем!\nВы прошли «Мою улицу»\nза " + time.ToString("F2", nfi));
	}

	public void StartTheGame() {
		wallMenuBackground.SetActive(false);
		startPanel.SetActive(false);
		fadingToClear = true;
		// StartFadeToWhite();
		movingHead = true;
		movingWalls = true;
	}
	public void Restart () {
		SceneManager.LoadScene(0);
	}

	public void StartFadeToWhite() {
		fadingToWhite = true;
		whiteFadeImage.color = new Color(1f, 1f, 1f, 0f);	
	}

	// Use this for initialization
	void Start () {
		time = 0;
	}
	
	void Update () {

		if (countingTime) {
			time += Time.deltaTime;
		}

		if (movingHead) {
			headImage.color = Color.Lerp(headImage.color, Color.white, Time.deltaTime);
			headImage.rectTransform.localPosition = Vector3.up * Mathf.Lerp(headImage.rectTransform.localPosition.y, 3.5f, Time.deltaTime);
			if (3.4f <= headImage.rectTransform.localPosition.y) {
				movingHead = false;
				fadingHead = true;
				fpsController.enabled = true;
				countingTime = true;
			}
		}else if(fadingHead) {
			headImage.color = Color.Lerp(headImage.color, Color.clear, Time.deltaTime * 5f);
			if (headImage.color.a <= 0.01f) {
				fadingHead = false;
			}
		}

		if (movingWalls) {
			walls.transform.position = Vector3.up * Mathf.Lerp(walls.transform.position.y, 0f, Time.deltaTime*0.7f);
			if (-0.05f <= walls.transform.position.y) {
				walls.transform.position = Vector3.zero;
				movingWalls = false;
			}
		}


		if (fadingToClear) {
			whiteFadeImage.color = Color.Lerp(whiteFadeImage.color, Color.clear, Time.deltaTime);
			if (whiteFadeImage.color.a <= 0.01f) {
				fadingToClear = false;
				whiteFadeImage.color = Color.clear;
			}
		}else if (fadingToWhite) {
			whiteFadeImage.color = Color.Lerp(whiteFadeImage.color, Color.white, Time.deltaTime * fadeToWhiteSpeed);
			if (0.99f <= whiteFadeImage.color.a) {
				fadingToWhite = false;
				whiteFadeImage.color = Color.white;
			}
		} 
	}
}
