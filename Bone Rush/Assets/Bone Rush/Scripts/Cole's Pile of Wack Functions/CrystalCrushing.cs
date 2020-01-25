using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;

public class CrystalCrushing : MonoBehaviour
{

	// Comment/Uncomment when needed (i.e. if another script needs to know when the crystal is crushed)
	//public static bool crystalCrushed { get; set; }
	bool crystalCrushed;

	public AudioClip crystalAudioClip;

	public ParticleSystem crystalParticles;

	float crushToTeleportDelay;

    // Start is called before the first frame update
    void Start()
    {
		crystalParticles = GameObject.Find("CrystalPS").GetComponent<ParticleSystem>();
		crystalAudioClip = (AudioClip)AssetDatabase.LoadAssetAtPath("Assets/Bone Rush/Imported Assets/Sounds Files/SFX_GP_CrushCrystal.wav", typeof(AudioClip));
		gameObject.transform.parent = null;
		DontDestroyOnLoad(gameObject);
	}

    // Update is called once per frame
    void Update()
    {
		if (Input.GetAxis("CrushCrystal") == 1 && !crystalCrushed)
		{
			crystalCrushed = true;
			AudioSource.PlayClipAtPoint(crystalAudioClip, transform.position);
			crystalParticles.Play();
			crushToTeleportDelay = 4.5f;
		}
		else if (crushToTeleportDelay > 0)
		{
			crushToTeleportDelay -= Time.deltaTime;
		}
		else if (crushToTeleportDelay <= 0 && crystalCrushed && SceneManager.GetActiveScene() != SceneManager.GetSceneByName("BossScene"))
		{
			CrushCrystal();
		}
    }

	// Anything that happens when the crystal is crushed goes here
	void CrushCrystal()
	{
		SceneManager.LoadScene("BossScene");
	}
}
