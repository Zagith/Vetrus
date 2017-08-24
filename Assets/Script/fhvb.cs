using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fhvb : MonoBehaviour {
 public const float Y_ANGLE_MIN = -10.0f;
 public const float Y_ANGLE_MAX = 50500.0f;

  //public const float X_ANGLE_MIN = -65.0f;
 //public const float X_ANGLE_MAX = 65.0f;

 public Transform lookAt;
 public Transform camTransform;

 private Camera cam;

 public float distance = 10.0f;
 public float currentX = 0.0f;
 public float currentY = 0.0f;
 public float sensivityX = 5.0f;
 public float sensivityY = 5.0f;


 private void Start()
 {
 	camTransform = transform;
 	cam = Camera.main;

 }

 private void Update()
 {  
 	currentX += Input.GetAxis("Mouse X");
 	currentY += Input.GetAxis("Mouse Y") * (-1);
   

 	currentY = Mathf.Clamp(currentY,Y_ANGLE_MIN,Y_ANGLE_MAX);
 	//currentX = Mathf.Clamp(currentX,X_ANGLE_MIN,X_ANGLE_MAX);
 }


 private void LateUpdate()
 {
 	Vector3 dir = new Vector3(0,0,-distance);
 	Quaternion rotation = Quaternion.Euler(currentY,currentX,0);
 	camTransform.position = lookAt.position + rotation * dir;
 	camTransform.LookAt(lookAt.position);
 }

}
