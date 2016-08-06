using UnityEngine;
using System.Collections;
using UnityStandardAssets._2D;
public class bindCameraToPlayer : MonoBehaviour {

	// Use this for initialization
	void Start () {
		// transform.parent.gameObject.GetComponent<Camera2DFollow>().target = this.transform;
		transform.SetParent (transform.parent.parent);// become Batman
	}
}
