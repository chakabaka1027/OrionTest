using UnityEngine;
 using System.Collections;
 
 public class LaserBehavior : MonoBehaviour {
 
     LineRenderer lineRenderer;
     Vector3 previousPosition;
     int i = 0;
 
     void Awake () {
         lineRenderer = GetComponent<LineRenderer> ();
         Vector3 previousPosition = this.transform.position;
     }
 
     
     void Update () {
         Lazer (previousPosition);

     }
 
     //void Laser(){

     //    RaycastHit hit;
 
     //    if(Physics.Raycast(transform.position,Vector3.forward,out hit,Mathf.Infinity)){

     //        lineRenderer.enabled = true;
             
     //        lineRenderer.SetPosition(0, transform.position); 
     //        lineRenderer.SetPosition(1, hit.point);   
 
     //        if(hit.collider.tag == "Mirrors"){
     //            Vector3 pos = Vector3.Reflect (hit.point - this.transform.position, hit.normal);
     //            lineRenderer.SetPosition(2,pos);
     //        }

             
     //    }
     //}

      void Lazer(Vector3 previousPosition){
        
        RaycastHit hit;
        if(Physics.Raycast(previousPosition,Vector3.forward,out hit,Mathf.Infinity)){

            lineRenderer.enabled = true;
             
            lineRenderer.SetPosition(i, transform.position); 
            lineRenderer.SetPosition(i+1, hit.point);   
 
            if(hit.collider.tag == "Mirrors"){
                Vector3 pos = Vector3.Reflect (hit.point - previousPosition, hit.normal);
                lineRenderer.SetPosition(i+1,pos);
                Lazer(hit.point);
            }

             
        }
         
     }


 }