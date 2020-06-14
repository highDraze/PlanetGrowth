using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandBounce : MonoBehaviour
{
    public bool right = false;

    void Update() {

      StartCoroutine(GoLeft());

        IEnumerator GoLeft() {

            float timePassed = 0;
            while (timePassed < 10) {

                if(transform.rotation.eulerAngles.y > 30){
                    right = true;
                }
                if (transform.rotation.eulerAngles.y < -30){
                    right = false;
                }
                if(transform.rotation.eulerAngles.y > 30 && right == false){
                    transform.Rotate( new Vector3(0, -0.001f, 0) );
                    
                } else if ( transform.rotation.eulerAngles.y < -30 && right == true){
                    transform.Rotate( new Vector3(0, +0.0001f, 0) );
                }else {
                   transform.Rotate( new Vector3(0, +0.0001f, 0) ); 
                }
               
                
                timePassed += Time.deltaTime;
                yield return null;
            }
        }  
    }
    
}
