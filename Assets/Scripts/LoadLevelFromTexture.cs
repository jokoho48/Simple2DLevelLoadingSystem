using UnityEngine;
using System.Collections;

public class LoadLevelFromTexture : MonoBehaviour
{
	public SimpleLevelDictionary[] LevelGeometryDictonary;

	public Texture2D LevelTexture;

	// Use this for initialization
	void Start()
	{
		LoadLevel levelLoadedCore = GetComponent<LoadLevel>();
		if (levelLoadedCore == null) {
			this.gameObject.AddComponent <LoadLevel>();
			levelLoadedCore = GetComponent<LoadLevel>();
		};
		SimpleLevel Level = new SimpleLevel(LevelTexture);
		//Level.TextureFile = LevelPath;
		levelLoadedCore.LevelGeometryDictonary = LevelGeometryDictonary;
		levelLoadedCore.SimpleLevel = Level;
		SendMessage("loadLevelAndProess");
	}
}
