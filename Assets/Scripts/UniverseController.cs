using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.ImageEffects;

public class UniverseController : MonoBehaviour
{

    //dialogue stuff
    int logNumber = 0;
    public string[] tutorialText;
    public GameObject[] tutorial;

    int toggle = 0;


    public GameObject sun;
    public float lightRotationA = 307.456f;
    public float lightRotationB = 31;
    
	public static bool Swapping
	{
		get; private set;
	}

	[Header("Swap Effect Stuff")]
	
    [SerializeField]
	private AnimationCurve _vingetteCurve;
	[SerializeField]
	private AnimationCurve _saturation;
	[SerializeField]
	private Camera _camera;
	[SerializeField]
	private AnimationCurve _fov;
	[SerializeField]
	private AnimationCurve _timeScale;

	private AudioSource _audio;
	private bool _swapTiggered;
	private readonly float _swapTime = 0.85f;

	void Awake()
	{
		_audio = GetComponent<AudioSource>();
        sun.transform.eulerAngles = new Vector3(lightRotationA, 0, 0);

	}

	public void SwapUniverses()
	{
		Swapping = true;
		StartCoroutine(SwapAsync());
	}

	void Update()
	{
		if (!Swapping && Input.GetMouseButtonDown(0))
		{
			StartCoroutine(SwapAsync());
		}
	}

	
	IEnumerator SwapAsync()
	{
        FindObjectOfType<DragBehavior>().FinishedRotating();

		Swapping = true;
		_swapTiggered = false;

		_audio.PlayOneShot(_audio.clip);

		for (float t = 0; t < 1.0f; t += Time.unscaledDeltaTime * 1.2f)
		{
			_camera.fieldOfView = _fov.Evaluate(t);
			
            FindObjectOfType<ColorCorrectionCurves>().saturation = _saturation.Evaluate(t);
            FindObjectOfType<VignetteAndChromaticAberration>().intensity = _vingetteCurve.Evaluate(t);

			Time.timeScale = _timeScale.Evaluate(t);

			if (t > _swapTime && !_swapTiggered)
			{
				_swapTiggered = true;
                Shift();
			}

			yield return null;
		}

		// technically a huge lag spike could cause this to be missed in the coroutine so double check it here.
		if (!_swapTiggered)
		{
			_swapTiggered = true;
            Shift();

		}

	    _camera.fieldOfView = _fov.Evaluate(1.0f);

		
        FindObjectOfType<ColorCorrectionCurves>().saturation = 1;
        FindObjectOfType<VignetteAndChromaticAberration>().intensity = 0;

		Time.timeScale = 1.0f;

		Swapping = false;
	}


    public void Shift() {
    
        toggle = 1 - toggle;

        if (toggle == 1) {
            gameObject.transform.parent.position = gameObject.transform.parent.position + Vector3.right * 100;
            ResetCurrentMovementCubeState();
            //Debug.Log("Universe B");
            sun.transform.eulerAngles = new Vector3(lightRotationB, 0, 0);

            //dialogue text
   
            if(tutorial != null && tutorial.Length > 0 && logNumber <= tutorial.Length-1) {
                tutorial[logNumber].GetComponent<Animator>().Play("Active");
                StartCoroutine(tutorial[logNumber].transform.FindChild("UIElementsPanel").FindChild("Text").GetComponent<Typing>().TypeIn(tutorialText[logNumber]));
            }
           

        } else if (toggle == 0) {
            gameObject.transform.parent.position = gameObject.transform.parent.position + Vector3.right * -100;
            ResetCurrentMovementCubeState();
            //Debug.Log("Universe A");
            sun.transform.eulerAngles = new Vector3(lightRotationA, 0, 0);

            //disable dialogue text
            if(tutorial.Length > 0 && logNumber <= tutorial.Length-1) {
                tutorial[logNumber].GetComponent<Animator>().Play("Inactive");
            }
            if(tutorial.Length > 0 && logNumber <= tutorial.Length-1) {
                logNumber ++;
            }
        }
    }


    public void ResetCurrentMovementCubeState() {
        if(FindObjectOfType<Movement>().currentNavpoint != null) {
            FindObjectOfType<Movement>().currentNavpoint.GetComponent<MovementLantern>().isCurrentLantern = false;
            FindObjectOfType<Movement>().currentNavpoint.GetComponent<BoxCollider>().enabled = true;
            FindObjectOfType<Movement>().currentNavpoint.GetComponent<MeshRenderer>().enabled = true;
        }
    }
}
