using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class CrystalCrushing : MonoBehaviour
{

	// Comment/Uncomment when needed (i.e. if another script needs to know when the crystal is crushed)
	//public static bool crystalCrushed { get; set; }
	bool crystalCrushed;

    [SerializeField]
	private AudioClip crystalAudioClip;
    [SerializeField]
    private AudioSource audioSource;

	public ParticleSystem crystalParticles;

	float crushToTeleportDelay;

    // Start is called before the first frame update
    void Start()
    {
		crystalParticles = GameObject.Find("CrystalPS").GetComponent<ParticleSystem>();
		crystalAudioClip = (AudioClip)AssetDatabase.LoadAssetAtPath("Assets/Bone Rush/Imported Assets/Sounds Files/SFX_GP_CrushCrystal.wav", typeof(AudioClip));
        audioSource = GetComponent<AudioSource>();

        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("SCN_Level_Boss"))
        {
            Debug.Log("in boss room");
            //Destroy(gameObject);
        }
        else
        {
            Debug.Log("not in boss room");
        }
    }

    // Update is called once per frame
    void Update()
    {
		if (Input.GetAxis("CrushCrystal") == 1 && !crystalCrushed)
		{
			CrushCrystal();
		}


		if (crushToTeleportDelay > 0)
		{
			crushToTeleportDelay -= Time.deltaTime;
		}
		else if (crushToTeleportDelay <= 0 && crystalCrushed && SceneManager.GetActiveScene() != SceneManager.GetSceneByName("SCN_Level_Boss"))
		{
            CrushCrystal();
		}
    }

	// Anything that happens when the crystal is crushed goes here
	public void CrushCrystal()
	{
		if (crystalCrushed == false && SceneManager.GetActiveScene() != SceneManager.GetSceneByName("SCN_Level_Boss"))
		{
            // Triggers CrystalCrush event in FMOD
            audioSource.PlayOneShot(crystalAudioClip);
            // AudioSource.PlayClipAtPoint(crystalAudioClip, transform.position);
            crystalCrushed = true;
            gameObject.transform.parent = null;
			crystalParticles.Play();
			crushToTeleportDelay = 4.5f;
		}
		else if (SceneManager.GetActiveScene() != SceneManager.GetSceneByName("SCN_Level_Boss"))
        {
            SceneManager.LoadScene("SCN_Level_Boss");
		}
	}
}
