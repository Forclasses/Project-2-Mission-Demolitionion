using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class Slingshot : MonoBehaviour
{

    [Header("Inscribed")]
    public GameObject projectilePrefab;
    public float velocityMult = 10f;

    public GameObject projLinePrefab;

    [Header("Dynamic")]
    [SerializeField] private LineRenderer rubber;
    [SerializeField] private Transform firstPoint;
    
    [SerializeField] private Transform secondPoint;

    public Vector3 launchPos;
    public GameObject projectile;

    public bool aimingMode;

    public GameObject launchPoint;

    void Awake()
    {
        //rubber.SetPosition(0, firstPoint.position);
        //rubber.SetPosition(1, projectile.position);
        //rubber.SetPosition(2, secondPoint.position);

        Transform launchPointTrans = transform.Find("LaunchPoint");
        launchPoint = launchPointTrans.gameObject;
        launchPoint.SetActive( false );
        launchPos =  launchPointTrans.position;
        //rubber.SetPosition(1, launchPos);
        

        
    }
    void OnMouseEnter() {
        //print( "Slingshot:OnMouseEnter()" );
        launchPoint.SetActive( true );
    }

    void OnMouseExit() {
        //print( "Slingshot: OnMouseExit()" );
        launchPoint.SetActive( false );
    }


    void OnMouseDown()
    {
        aimingMode = true;
        //It took me 30 mintues to figure out  that THIS WHAT THE COMMAND NEEDS TO BE 
        projectile = Instantiate( projectilePrefab , transform.position, Quaternion.identity) as GameObject;
        //GameObject projectile = Instantiate( projectilePrefab) as GameObject;
        projectile.transform.position = launchPos;
        rubber.SetPosition(0, firstPoint.position);
        //rubber.SetPosition(1, projectile.position);
        rubber.SetPosition(2, secondPoint.position);

        projectile.GetComponent<Rigidbody>().isKinematic = true;
    }

    void Update()
    {
     if (!aimingMode) return;

     Vector3 mousePos2D = Input.mousePosition;
     mousePos2D.z = -Camera.main.transform.position.z;
     Vector3 mousePos3D = Camera.main.ScreenToWorldPoint( mousePos2D );

     Vector3 mouseDelta = mousePos3D -launchPos;

     float maxMagnitude = this.GetComponent<SphereCollider>().radius;
     //pg697 need break
     if (mouseDelta.magnitude > maxMagnitude){
        mouseDelta.Normalize();
        mouseDelta *= maxMagnitude;
     }

     Vector3 projPos = launchPos + mouseDelta;
     projectile.transform.position = projPos;

     if( Input.GetMouseButtonUp(0) ){
        aimingMode = false ;
        Rigidbody projRB = projectile.GetComponent<Rigidbody>();
        projRB.isKinematic = false;
        projRB.collisionDetectionMode = CollisionDetectionMode.Continuous;
        projRB.linearVelocity = -mouseDelta * velocityMult;
        FallowCam.POI = projectile;
        Instantiate<GameObject>(projLinePrefab, projectile.transform);
        projectile = null;

     }
    }
}
