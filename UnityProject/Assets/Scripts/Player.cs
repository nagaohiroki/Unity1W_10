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
	// ------------------------------------------------------------------------
	/// @brief 初回更新
	// ------------------------------------------------------------------------
	void Start()
	{
		mTelop.gameObject.SetActive(true);
		mTelop.text = string.Format("Stage {0}/{1}",
		                            SceneManager.GetActiveScene().buildIndex + 1,
		                            SceneManager.sceneCountInBuildSettings);
	}
	// ------------------------------------------------------------------------
	/// @brief 更新
	// ------------------------------------------------------------------------
	void Update()
	{
		// テロップ消す
		if(Time.timeSinceLevelLoad > 1.0f && !IsClear())
		{
			mTelop.gameObject.SetActive(false);
		}
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
		// リセット/死亡
		if(Input.GetKeyDown(KeyCode.S) || transform.position.y < -10.0f)
		{
			Restart();
		}
	}
	// ------------------------------------------------------------------------
	/// @brief 再スタート
	// ------------------------------------------------------------------------
	void Restart()
	{
		int currentScene = SceneManager.GetActiveScene().buildIndex;
		if(IsClear())
		{
			++currentScene;
			// ステージラスト
			if(currentScene >= SceneManager.sceneCountInBuildSettings)
			{
				currentScene = 0;
			}
		}
		SceneManager.LoadScene(currentScene);
	}
	// ------------------------------------------------------------------------
	/// @brief 衝突したとき
	///
	/// @param inColl
	// ------------------------------------------------------------------------
	void OnCollisionEnter(Collision inColl)
	{
		if(inColl.gameObject.tag != "Mag")
		{
			return;
		}
		// くっつける
		inColl.transform.SetParent(transform);
		// クリア
		if(IsClear())
		{
			// クリアテロップ
			if(mTelop != null)
			{
				mTelop.gameObject.SetActive(true);
				mTelop.text = "CLEAR!!\n(Press S Ksy)";
			}
		}
	}
	// ------------------------------------------------------------------------
	/// @brief クリアしたかどうか
	///
	/// @return
	// ------------------------------------------------------------------------
	bool IsClear()
	{
		return transform.childCount >= 9;
	}
}
