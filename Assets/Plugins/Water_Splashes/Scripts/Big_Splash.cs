using UnityEngine;
using System.Collections;

public class Big_Splash : MonoBehaviour {


public GameObject BigSplash;

private float splashFlag = 0;


void Start ()
{

    BigSplash.SetActive(true);

}


public void ShowSplash()
{
    StartCoroutine("TriggerSplash");
}

   
	IEnumerator TriggerSplash (){
    
    splashFlag = 1;
    
    BigSplash.SetActive(true);

	yield return new WaitForSeconds (3.5f);

    BigSplash.SetActive(false);

    splashFlag = 0;

}




}