using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CrystalCrushing : MonoBehaviour
{

	// Comment/Uncomment when needed (i.e. if another script needs to know when the crystal is crushed)
	//public static bool crystalCrushed { get; set; }
	bool crystalCrushed;

	float crushToTeleportDelay;

    // Start is called before the first frame update
    void Start()
    {
		gameObject.transform.parent = null;
		DontDestroyOnLoad(gameObject);
	}

    // Update is called once per frame
    void Update()
    {
		if (Input.GetAxis("CrushCrystal") == 1 && !crystalCrushed)
		{
			crystalCrushed = true;
			crushToTeleportDelay = 2f;
		}
		else if(crushToTeleportDelay > 0)
		{
			crushToTeleportDelay -= Time.deltaTime;
		}
		else if(crushToTeleportDelay <= 0 && crystalCrushed && SceneManager.GetActiveScene() == SceneManager.GetSceneByBuildIndex(0))
		{
			CrushCrystal();
		}
    }

	// Anything that happens when the crystal is crushed goes here
	void CrushCrystal()
	{
		SceneManager.LoadScene("BossRoom");
	}
}
