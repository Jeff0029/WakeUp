using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using Photon.Pun;

[RequireComponent(typeof(StudioEventEmitter))]
public class BackgroundMusic : MonoBehaviourPunCallbacks
{
    public const string ROO_EVENT_PATH = "event:/";
    public float fuzzynessInTime = 0.6f;
    public float fuzzynessOutTime = 1;
    StudioEventEmitter emitter;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        emitter = GetComponent<StudioEventEmitter>();
        
    }

    public IEnumerator LerpParam(string paramName, float transitionTime = 0, float startVal=0, float endVal=1)
    {
        float progress = 0;
        while (progress < 1)
        {
            emitter.SetParameter(paramName, Mathf.Lerp(startVal, endVal, progress));
            progress += Time.deltaTime / transitionTime;
            yield return null;
        }
        
        yield return null;
    }

    public override void OnJoinedRoom()
    {
        StartCoroutine(LerpParam("Fuzzyness", fuzzynessInTime));
    }

    public override void OnLeftRoom()
    {
        StartCoroutine(LerpParam("Fuzzyness", fuzzynessOutTime, 1, 0));
    }

}
