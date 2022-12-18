using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HingeJoint : MonoBehaviour {
    public JointLimits limits;

    // Use this for initialization
    void Start ()
    {
        HingeJoint hinge = GetComponent<HingeJoint>();
        JointLimits limits = hinge.limits;
        limits.min = -145;
        limits.max = 0;
        //hinge.limits = limits;
        //hinge.useLimits = true;
	}
}
