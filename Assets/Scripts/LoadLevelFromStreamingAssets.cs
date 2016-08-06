using UnityEngine;
using System.Collections;

public class LoadLevelFromStreamingAssets : MonoBehaviour {

	public SimpleLevelDictionary[] LevelGeometryDictonary;

	public string LevelPath;

	// Use this for initialization
	void Start () {
		loadLevelCore levelLoadedCore = GetComponent<loadLevelCore>();
		if (levelLoadedCore == null) {
			gameObject.AddComponent <loadLevelCore>();
			levelLoadedCore = GetComponent<loadLevelCore>();
		};
		SimpleLevel Level = new SimpleLevel(LevelPath);
		//Level.textureFilePaths = LevelPath;
		levelLoadedCore.LevelGeometryDictonary = LevelGeometryDictonary;
		levelLoadedCore.SimpleLevel = Level;
		SendMessage("loadLevelAndProess");
	}
}
