using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class count : MonoBehaviour
{


    public TMP_Text uiText;
    public TMP_Text inicio;
    public TMP_Text final;
    public int contador=12;
    float time=10f;
    // Start is called before the first frame update
    void Start()
    {
             uiText.text="Enemies:"+contador.ToString();
            final.enabled=false;
    }

    // Update is called once per frame
    void Update(){
    if(time>0){
        time-=Time.deltaTime;
        if(time<=0){
            inicio.enabled=false;
}
}
    
       if(Input.GetKeyDown("space")){
        contador--;
            uiText.text="Enemies:"+contador.ToString();

       } 
    if (contador<=0){
        uiText.enabled=false;
        final.enabled=true;
    }
}

}
