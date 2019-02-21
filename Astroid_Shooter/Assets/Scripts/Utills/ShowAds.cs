using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class ShowAds : MonoBehaviour {
	ShowAds ads;
	void Awake(){
		//makes sure that there is only one instence of the player settings.
		if (ads == null) {
			DontDestroyOnLoad (gameObject);
			ads = this;
		} else if (ads != this) {
			Destroy (gameObject);
		}
	}

	public void ShowRewardedAd (){
		if(Advertisement.IsReady("rewardedVideo")){
			var options = new ShowOptions{ resultCallback = HandleShowResult };
			Advertisement.Show ("rewardedVideo", options);
		}
	}

	private void HandleShowResult(ShowResult result){
		switch (result) {
		case ShowResult.Finished:
			Debug.Log ("the ad was shown");
			
			break;
		case ShowResult.Skipped:
			Debug.Log ("the ad was skipped");
			break;
		case ShowResult.Failed:
			Debug.Log ("the ad faild to launch");
			break;
		}
	}
}

