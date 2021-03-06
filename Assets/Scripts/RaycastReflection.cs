﻿ 
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
 
[RequireComponent(typeof(LineRenderer))]
public class RaycastReflection : MonoBehaviour{

    public GameObject laser;

	bool hasChildBeam = false;
    public bool hasSpawned = false;
    public int vertexCounter;

    public float updateFrequency = 0.01f;
    public int laserDistance;
    public string bounceTag;
    public string splitTag;
    public string spawnedBeamTag;
    public int maxBounce;
    public int maxSplit;
    private float timer = 0;
    private LineRenderer mLineRenderer;

    public List<GameObject> myChildren;

    public int isActive = 0;
 
    // Use this for initialization
    void Start(){
        timer = 0;
        mLineRenderer = gameObject.GetComponent<LineRenderer>();
        StartCoroutine(RedrawLaser());
    }
 
    // Update is called once per frame
    void FixedUpdate(){

        if (gameObject.tag != spawnedBeamTag){
            if (timer >= updateFrequency){
                timer = 0;

                //foreach (GameObject laserSplit in GameObject.FindGameObjectsWithTag(spawnedBeamTag)) {
                //    Destroy(laserSplit);
                //}

                foreach (GameObject laserSplit in myChildren) {
                    Destroy(laserSplit);
                }


                StartCoroutine (RedrawLaser ());
                                StartCoroutine (RedrawLaser ());

            }
            timer += Time.deltaTime;
        }  
    }

    public IEnumerator LaserSwitch() {
        if(isActive == 1) {
            gameObject.GetComponent<LineRenderer>().enabled = true;
            gameObject.GetComponent<RaycastReflection>().enabled = true;
        } else if (isActive == 0) {
            gameObject.GetComponent<LineRenderer>().enabled = false;
            yield return new WaitForSeconds(0.1f);

            gameObject.GetComponent<RaycastReflection>().enabled = false;
        }
    }


    private void OnDestroy() {
        foreach (GameObject myChild in myChildren) {
            Destroy(myChild);
        }
        myChildren = new List<GameObject>();
    }

    IEnumerator RedrawLaser(){
        //Debug.Log("Running");
        int laserSplit = 1; //How many times it got split
        int laserReflected = 1; //How many times it got reflected
        vertexCounter = 1; //How many line segments are there
        bool loopActive = true; //Is the reflecting loop active?
 
        Vector3 laserDirection = transform.forward; //direction of the next laser
        Vector3 lastLaserPosition = transform.position; //origin of the next laser
 
        mLineRenderer.numPositions = 1;
        mLineRenderer.SetPosition(0, transform.position);
        RaycastHit hit;
 
        //destroy children
        //foreach (GameObject myChild in myChildren) {
        //    Destroy(myChild);
        //}
        //myChildren = new List<GameObject>();

        while (loopActive){
            //Debug.Log("Physics.Raycast(" + lastLaserPosition + ", " + laserDirection + ", out hit , " + laserDistance + ")");
            if (Physics.Raycast(lastLaserPosition, laserDirection, out hit, laserDistance)) {
                if ((hit.transform.gameObject.tag == bounceTag) || (hit.transform.gameObject.tag == splitTag)){
                    
                    //play sound
                    hit.collider.gameObject.GetComponent<Scannable>().Activate();

                    //Debug.Log("Bounce");
                    laserReflected++;
                    vertexCounter += 3;
                    mLineRenderer.numPositions = vertexCounter;

                    mLineRenderer.SetPosition(vertexCounter - 3, Vector3.MoveTowards(hit.point, lastLaserPosition, 0.01f));


                    if(hit.transform.gameObject.tag == bounceTag) {
                        mLineRenderer.SetPosition(vertexCounter - 1, hit.point);

                    } else if (hit.transform.gameObject.tag == splitTag) {
                        mLineRenderer.SetPosition(vertexCounter - 1, hit.point + (laserDirection * 0.01f));

                    }


                    

                    mLineRenderer.SetPosition(vertexCounter - 2, hit.point);
                    //mLineRenderer.SetPosition(vertexCounter - 1, hit.point);
                    mLineRenderer.startWidth = .01f;
                    mLineRenderer.endWidth = .01f;


                    lastLaserPosition = hit.point;
                    Vector3 incomingDirection = laserDirection;
                    laserDirection = Vector3.Reflect(laserDirection, hit.normal);
               
                    if (hit.transform.gameObject.tag == splitTag){
                        //Debug.Log("Split");
                        if (laserSplit >= maxSplit){
                            //Debug.Log("Max split reached.");
                        }
                        else {
                            //Debug.Log("Splitting...");
                            laserSplit++;
                            Object go = Instantiate(laser, hit.point + (incomingDirection * .01f), Quaternion.LookRotation(incomingDirection));
                            go.name = spawnedBeamTag;
                            ((GameObject)go).tag = spawnedBeamTag;

                            myChildren.Add(go as GameObject);

                        }
                    }
                }

                else { //if you run into a sensor
                    if (hit.transform.gameObject.tag == "Sensor") {
                        hit.collider.gameObject.GetComponentInParent<Sensor>().Activate();
                    }


					else if(hit.transform.gameObject.tag == "MovementSensor") {
                        if(hit.collider.gameObject.GetComponent<Sensor>() != null) {
						    hit.collider.gameObject.GetComponent<Sensor>().Activate();
                        }
						//Debug.Log("Split");
                        if (laserSplit >= maxSplit){
                            //Debug.Log("Max split reached.");
                        }
                        else {
                            laserSplit++;
                            Object go = Instantiate(laser, hit.point + (laserDirection * 0.01f), Quaternion.LookRotation(laserDirection));
                            go.name = spawnedBeamTag;
                            ((GameObject)go).tag = spawnedBeamTag;


                            myChildren.Add(go as GameObject);


                        }
                    }



                     //if you run into anything other than a mirror, splitter, or sensor
                    laserReflected++;
                    vertexCounter++;
                    mLineRenderer.numPositions = vertexCounter;
                    //Vector3 lastPos = lastLaserPosition + (laserDirection.normalized * laserDistance);

                
                    //Debug.Log("InitialPos " + lastLaserPosition + " Last Pos" + lastPos);
       
                    mLineRenderer.SetPosition(vertexCounter - 1, hit.point + (laserDirection * 0.01f)); 
                   
                


                    loopActive = false;
                } 
                
                  
               
            }

            else{
  
                //Debug.Log("No Bounce");
                laserReflected++;
                vertexCounter++;
                mLineRenderer.numPositions = vertexCounter;
                //Vector3 lastPos = lastLaserPosition + (laserDirection.normalized * laserDistance);
                //Debug.Log("InitialPos " + lastLaserPosition + " Last Pos" + lastPos);
           
                mLineRenderer.SetPosition(vertexCounter - 1, lastLaserPosition + (laserDirection.normalized * laserDistance)); 
                
                loopActive = false;
            }
            if (laserReflected > maxBounce)
                loopActive = false;
        }
       
        yield return new WaitForEndOfFrame();
    }

}






