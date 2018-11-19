using UnityEngine;
public class Player : MonoBehaviour
{
	[SerializeField]
	Rigidbody mRigidbody;
	void Update()
	{
		var v = Vector3.zero;
		v.z = -Input.GetAxis("Horizontal");
		mRigidbody.AddTorque(v, ForceMode.VelocityChange);
	}
}
