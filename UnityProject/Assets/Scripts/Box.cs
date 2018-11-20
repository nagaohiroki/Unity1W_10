using UnityEngine;
public class Box : MonoBehaviour
{
	[SerializeField]
	Rigidbody mRigidbody;
	[SerializeField]
	GameObject mCameraHandle;
	void Update()
	{
		if(mRigidbody != null)
		{
			var v = Vector3.zero;
			v.z = -Input.GetAxis("Horizontal");
			mRigidbody.AddTorque(v, ForceMode.VelocityChange);
		}
		if(mCameraHandle != null)
		{
			mCameraHandle.transform.position = transform.position;
		}
	}
	void OnCollisionEnter(Collision inColl)
	{
		if(inColl.gameObject.tag == "Mag")
		{
			inColl.transform.SetParent(transform);
		}
	}
}
