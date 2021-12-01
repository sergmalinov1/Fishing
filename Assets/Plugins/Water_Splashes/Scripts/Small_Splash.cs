using UnityEngine;
using System.Collections;

public class Small_Splash : MonoBehaviour 
{


    public GameObject SmallSplash;

    private float splashFlag = 0;


    void Start ()
    {
        SmallSplash.SetActive(false);
    }

    public void ShowSplash()
    {
        StartCoroutine("TriggerSplash");
    }


    IEnumerator TriggerSplash()
    {
       yield return new WaitForSeconds(0.2f);
        SmallSplash.SetActive(true);
        yield return new WaitForSeconds(2.3f);
        SmallSplash.SetActive(false);
    }

}