using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;
public class loadLevelCore : MonoBehaviour
{
	[HideInInspector]
	private SimpleLevelDictionary[] _LevelGeometryDictonary = new SimpleLevelDictionary[0];
	public SimpleLevelDictionary[] LevelGeometryDictonary {
		set { _LevelGeometryDictonary = value; createLevelDictionary(); }
		get { return _LevelGeometryDictonary; }
	}
	[HideInInspector]
	public SimpleLevel SimpleLevel;
	[HideInInspector]
	public Dictionary<Color32, GameObject> LevelGeometryDictonaryReal = new Dictionary<Color32, GameObject>();

	void Start()
	{
		// createLevelDictionary();
	}

	public void createLevelDictionary()
	{
		LevelGeometryDictonaryReal.Clear();

		foreach (SimpleLevelDictionary geodic in LevelGeometryDictonary)
		{
			LevelGeometryDictonaryReal.Add(geodic.colorKey, geodic.prefab);
		}
	}

	public void loadingLevel()
	{
		int height = SimpleLevel.TextureFile.height;
		int width = SimpleLevel.TextureFile.width;
		for (int x = 0; x < width; x++)
		{
			for (int y = 0; y < height; y++)
			{
				Color32 color = SimpleLevel.getPixelColor(x, y);
				createTile(color, new Vector3(x, y, 0));
			}
		}
	}

	public void clearLevel()
	{
		while (transform.childCount > 0)
		{
			Transform c = transform.GetChild(0);
			c.SetParent(null); // become Batman
			Destroy(c.gameObject); // become Joker
		}
	}

	public void createTile(Color32 color, Vector3 pos)
	{
		if (LevelGeometryDictonaryReal.ContainsKey(color))
		{
			GameObject go = (GameObject)Instantiate(LevelGeometryDictonaryReal[color], pos, Quaternion.identity);
			go.transform.SetParent(this.transform);
		}
		else if (color.a != 0)
		{
			Debug.LogError("Error Color not Found in Dictionary:" + color + " At Position: " + pos);
		}
	}

	void loadLevelAndProess()
	{
		clearLevel();
		createLevelDictionary();
		loadingLevel();
	}
	public void rebuildLevel () {
		clearLevel();
		loadingLevel();
	}
}

[System.Serializable]
public class SimpleLevel
{
	[SerializeField]
	private string _textureFilePaths;
	[SerializeField]
	public string textureFilePaths
	{
		set
		{
			if (value != "")
			{
				TextureFile = loadTexture2D(value);
			}

			_textureFilePaths = value;
		}
		get { return _textureFilePaths; }
	}
	public Texture2D TextureFile;

	private List<List<Color32>> allPixelsCached;


	public SimpleLevel () {}

	public SimpleLevel(string texturePath) {
		textureFilePaths = texturePath;
		loadTexture2D(textureFilePaths);
	}
	public SimpleLevel(Texture2D Texture)
	{
		TextureFile = Texture;
	}

	Texture2D loadTexture2D(string Path)
	{
		if (Path == _textureFilePaths)
		{
			return TextureFile;
		}
		string filePath = Application.dataPath + "/StreamingAssets/" + Path;
		byte[] textureBytes = File.ReadAllBytes(filePath);
		Texture2D Leveltexture = new Texture2D(2, 2);
		Leveltexture.LoadImage(textureBytes);
		return Leveltexture;
	}

	public List<List<Color32>> getLineBasedPixels()
	{
		if (allPixelsCached != null)
		{
			return allPixelsCached;
		}
		Color32[] allPixels = TextureFile.GetPixels32();
		int width = TextureFile.width;
		int height = TextureFile.height;
		List<List<Color32>> list = new List<List<Color32>>();
		for (int x = 0; x < width; x++)
		{
			List<Color32> currentList = new List<Color32>();
			for (int y = 0; y < height; y++)
			{
				currentList.Add(allPixels[x + (y * width)]);
			}
			list.Add(currentList);
		}
		allPixelsCached = list;
		return list;
	}

	public Color32 getPixelColor(int x, int y)
	{
		List<List<Color32>> allPixels = getLineBasedPixels();
		return allPixels[x][y];
	}
}

[System.Serializable]
public class SimpleLevelDictionary
{
	public string Name;
	public GameObject prefab;
	public Color32 colorKey;
}