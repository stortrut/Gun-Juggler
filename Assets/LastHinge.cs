using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastHinge : MonoBehaviour
{
    Rigidbody2D rigidbody2;
    HingeJoint2D hingeJoint2;
    // Start is called before the first frame update
    void Start()
    {
        transform.localPosition = new Vector3 (-1.0f, 5.44f, 0.0f);
      var  box = transform.root.gameObject;
      rigidbody2 = GetComponent<Rigidbody2D>();
      hingeJoint2 = box.GetComponent<HingeJoint2D>();
      hingeJoint2.connectedBody = rigidbody2;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
