using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class GetSkin : MonoBehaviour
{
	public GameObject player64x32;
	public GameObject player64x64;
	public GameObject playerSlim;
	public Text usernameText;
	private Texture2D skin;
	private string username;
	private string lastVerifiedUsername;
	public void Refresh() {
		StartCoroutine(GetTexture());
	}

	IEnumerator GetTexture() {
		username = usernameText.text;
		UnityWebRequest www = UnityWebRequestTexture.GetTexture("https://minotar.net/skin/" + username);
		yield return www.SendWebRequest();

		if(www.isNetworkError || www.isHttpError) {
			Debug.Log("Skin not found!");
		}
		else {
			lastVerifiedUsername = username;
			skin = ((DownloadHandlerTexture)www.downloadHandler).texture;
			skin.filterMode = FilterMode.Point;
			if (skin.height == 64) {
				if (skin.GetPixel (50, 44).a == 0) {
					for (int i = 0; i < 6; i++) {
						playerSlim.transform.GetChild (i).gameObject.GetComponent<Renderer> ().material.mainTexture = skin;
					}
					playerSlim.SetActive (true);
					player64x64.SetActive (false); 
					player64x32.SetActive (false);
				} else {
					for (int i = 0; i < 6; i++) {
						player64x64.transform.GetChild (i).gameObject.GetComponent<Renderer> ().material.mainTexture = skin;
					}
					player64x64.SetActive (true); 
					player64x32.SetActive (false);
					playerSlim.SetActive (false);
				}
			} else {
				for (int i = 0; i < 6; i++) {
					player64x32.transform.GetChild (i).gameObject.GetComponent<Renderer> ().material.mainTexture = skin;
				}
				player64x32.SetActive (true); 
				player64x64.SetActive (false); 
				playerSlim.SetActive (false); 
			}
		}
	}

	public void Download() {
		Application.OpenURL("https://minotar.net/download/" + lastVerifiedUsername);
	}
}
