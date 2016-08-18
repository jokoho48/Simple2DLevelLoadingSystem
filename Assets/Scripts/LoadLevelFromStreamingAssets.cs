using UnityEngine;
using System.Collections;

public class LoadLevelFromStreamingAssets : MonoBehaviour {

	public SimpleLevelDictionary[] LevelGeometryDictonary;

	public string LevelPath;

	// Use this for initialization
	void Start () {
		LoadLevel levelLoadedCore = GetComponent<LoadLevel>();
		if (levelLoadedCore == null) {
			gameObject.AddComponent <LoadLevel>();
			levelLoadedCore = GetComponent<LoadLevel>();
		};
		SimpleLevel Level = new SimpleLevel(LevelPath);
		//Level.textureFilePaths = LevelPath;
		levelLoadedCore.LevelGeometryDictonary = LevelGeometryDictonary;
		levelLoadedCore.SimpleLevel = Level;
		SendMessage("loadLevelAndProess");
	}
}
