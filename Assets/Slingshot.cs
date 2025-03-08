using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slingshot : MonoBehaviour
{

    [Header("Inscribed")]
    public GameObject projectilePrefab;

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
        GameObject projectile = Instantiate( projectilePrefab, transform.position, projectilePrefab.transform.rotation);
        projectile.transform.position = launchPos;
        rubber.SetPosition(0, firstPoint.position);
        //rubber.SetPosition(1, projectile.position);
        rubber.SetPosition(2, secondPoint.position);

        projectile.GetComponent<Rigidbody>().isKinematic = true;
    }
}
