using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
public class Box : MonoBehaviour
{
	[SerializeField]
	Rigidbody mRigidbody;
	[SerializeField]
	GameObject mCameraHandle;
	[SerializeField]
	List<string> mScenes;
	int mCurrentSceneIndex;
	// ------------------------------------------------------------------------
	/// @brief 更新
	// ------------------------------------------------------------------------
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
		if(Input.GetKeyDown(KeyCode.S))
		{
			Reset();
		}
	}
	void Reset()
	{
		SceneManager.LoadScene(mScenes[mCurrentSceneIndex]);
	}
	// ------------------------------------------------------------------------
	/// @brief 衝突したとき
	///
	/// @param inColl
	// ------------------------------------------------------------------------
	void OnCollisionEnter(Collision inColl)
	{
		if(inColl.gameObject.tag == "Mag")
		{
			inColl.transform.SetParent(transform);
		}
	}
}
