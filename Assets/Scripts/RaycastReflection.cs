 
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
 
[RequireComponent(typeof(LineRenderer))]
public class RaycastReflection : MonoBehaviour
{
    public bool hasSpawned = false;

    public float updateFrequency = 0.01f;
    public int laserDistance;
    public string bounceTag;
    public string splitTag;
    public string spawnedBeamTag;
    public int maxBounce;
    public int maxSplit;
    private float timer = 0;
    private LineRenderer mLineRenderer;

    public int isActive = 0;
 
    // Use this for initialization
    void Start(){
        timer = 0;
        mLineRenderer = gameObject.GetComponent<LineRenderer>();
        StartCoroutine(RedrawLaser());
    }
 
    // Update is called once per frame
    void Update(){

        if (gameObject.tag != spawnedBeamTag){
            if (timer >= updateFrequency){
                timer = 0;
                //Debug.Log("Redrawing laser");
                foreach (GameObject laserSplit in GameObject.FindGameObjectsWithTag(spawnedBeamTag)) {
                    Destroy (laserSplit);
                }
 
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
 
    IEnumerator RedrawLaser(){
        //Debug.Log("Running");
        int laserSplit = 1; //How many times it got split
        int laserReflected = 1; //How many times it got reflected
        int vertexCounter = 1; //How many line segments are there
        bool loopActive = true; //Is the reflecting loop active?
 
        Vector3 laserDirection = transform.forward; //direction of the next laser
        Vector3 lastLaserPosition = transform.position; //origin of the next laser
 
        mLineRenderer.numPositions = 1;
        mLineRenderer.SetPosition(0, transform.position);
        RaycastHit hit;
 
        while (loopActive){
            //Debug.Log("Physics.Raycast(" + lastLaserPosition + ", " + laserDirection + ", out hit , " + laserDistance + ")");
            if (Physics.Raycast(lastLaserPosition, laserDirection, out hit, laserDistance)) {
                if ((hit.transform.gameObject.tag == bounceTag) || (hit.transform.gameObject.tag == splitTag)){
                    
                    //Debug.Log("Bounce");
                    laserReflected++;
                    vertexCounter += 3;
                    mLineRenderer.numPositions = vertexCounter;
                    mLineRenderer.SetPosition(vertexCounter - 3, Vector3.MoveTowards(hit.point, lastLaserPosition, 0.01f));
                    mLineRenderer.SetPosition(vertexCounter - 2, hit.point);
                    mLineRenderer.SetPosition(vertexCounter - 1, hit.point);
                    mLineRenderer.startWidth = .01f;
                    mLineRenderer.endWidth = .01f;

                    lastLaserPosition = hit.point;
                    Vector3 prevDirection = laserDirection;
                    laserDirection = Vector3.Reflect(laserDirection, hit.normal);
               
                    if (hit.transform.gameObject.tag == splitTag){
                        //Debug.Log("Split");
                        if (laserSplit >= maxSplit){
                            //Debug.Log("Max split reached.");
                        }
                        else {
                            //Debug.Log("Splitting...");
                            laserSplit++;
                            Object go = Instantiate(gameObject, hit.point, Quaternion.LookRotation(prevDirection));
                            go.name = spawnedBeamTag;
                            ((GameObject)go).tag = spawnedBeamTag;

                        }
                    }
                }

                else { //if you run into a sensor
                    if (hit.transform.gameObject.tag == "Sensor") {
                        hit.collider.gameObject.GetComponentInParent<Sensor>().Activate();

                         //if you run into anything other than a mirror, splitter, or sensor
                        laserReflected++;
                        vertexCounter++;
                        mLineRenderer.numPositions = vertexCounter;
                        //Vector3 lastPos = lastLaserPosition + (laserDirection.normalized * laserDistance);

                    
                        //Debug.Log("InitialPos " + lastLaserPosition + " Last Pos" + lastPos);
           
                        mLineRenderer.SetPosition(vertexCounter - 1, hit.point); 
                    


                        loopActive = false;
                    } 
                    
                    else {

                        if (hit.transform.gameObject.tag == "MovementSensor") {
                                hit.collider.gameObject.GetComponent<Sensor>().Activate();
                                                        Debug.Log("hit movement");

                        }
                        laserReflected++;
                        vertexCounter++;
                        mLineRenderer.numPositions = vertexCounter;
                        //Vector3 lastPos = lastLaserPosition + (laserDirection.normalized * laserDistance);
                        //Debug.Log("InitialPos " + lastLaserPosition + " Last Pos" + lastPos);
           
                        mLineRenderer.SetPosition(vertexCounter - 1, lastLaserPosition + (laserDirection.normalized * laserDistance)); 
                
                        loopActive = false;
                    }         
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






