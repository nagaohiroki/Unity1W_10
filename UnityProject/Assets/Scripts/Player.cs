using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Player: MonoBehaviour
{
	[SerializeField]
	Rigidbody mRigidbody;
	[SerializeField]
	GameObject mCameraHandle;
	[SerializeField]
	Text mTelop;
	int mSceneIndex;
	// ------------------------------------------------------------------------
	/// @brief 初回更新
	// ------------------------------------------------------------------------
	void Start()
	{
		mSceneIndex = SceneManager.GetActiveScene().buildIndex;
	}
	// ------------------------------------------------------------------------
	/// @brief 更新
	// ------------------------------------------------------------------------
	void Update()
	{
		// 移動
		if(mRigidbody != null)
		{
			var v = Vector3.zero;
			v.z = -Input.GetAxis("Horizontal");
			mRigidbody.AddTorque(v, ForceMode.VelocityChange);
		}
		// カメラ更新
		if(mCameraHandle != null)
		{
			mCameraHandle.transform.position = transform.position;
		}
		// リセット
		if(Input.GetKeyDown(KeyCode.S))
		{
			Restart();
		}
		// 死亡
		if(transform.position.y < -10.0f)
		{
			Restart();
		}
	}
	// ------------------------------------------------------------------------
	/// @brief 再スタート
	// ------------------------------------------------------------------------
	void Restart()
	{
		SceneManager.LoadScene(mSceneIndex);
	}
	// ------------------------------------------------------------------------
	/// @brief クリア
	// ------------------------------------------------------------------------
	void Clear()
	{
		++mSceneIndex;
		if(mSceneIndex >= SceneManager.sceneCountInBuildSettings)
		{
			mSceneIndex = 0;
		}
		if(mTelop != null)
		{
			mTelop.gameObject.SetActive(true);
			mTelop.text = "CLEAR!!";
		}
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
			if(transform.childCount >= 9)
			{
				Clear();
			}
		}
	}
}
