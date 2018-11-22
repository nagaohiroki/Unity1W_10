using UnityEngine;
using UnityEngine.UI;
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
	[SerializeField]
	Text mTelop;
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
	// ------------------------------------------------------------------------
	/// @brief リセット
	// ------------------------------------------------------------------------
	void Reset()
	{
		int index = Mathf.Clamp(mCurrentSceneIndex, 0, mScenes.Count - 1);
		if(index < 0)
		{
			return;
		}
		SceneManager.LoadScene(mScenes[index]);
	}
	// ------------------------------------------------------------------------
	/// @brief クリア
	// ------------------------------------------------------------------------
	void Clear()
	{
		++mCurrentSceneIndex;
		if(mCurrentSceneIndex >= mScenes.Count)
		{
			mCurrentSceneIndex = 0;
		}
		mTelop.gameObject.SetActive(true);
		mTelop.text = "CLEAR!!";
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
			if(transform.childCount >= 10)
			{
				Clear();
			}
		}
	}
}
