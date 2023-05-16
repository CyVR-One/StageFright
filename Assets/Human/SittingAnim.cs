using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SittingAnim : MonoBehaviour
{
    private Animator anim;
    // Start is called before the first frame update
    IEnumerator Start()
    {
        anim = GetComponent<Animator>();
        while(true){
            yield return new WaitForSeconds(3);
            anim.SetInteger("RandomInt", Random.Range(0,5));  
            anim.SetTrigger("SittingRandom"); 
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
