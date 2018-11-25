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
	Color mDefaultColor;
	float mAnim;
	// ------------------------------------------------------------------------
	/// @brief 初回更新
	// ------------------------------------------------------------------------
	void Start()
	{
		ShowTelop(string.Format("Stage {0}/{1}", SceneManager.GetActiveScene().buildIndex + 1, SceneManager.sceneCountInBuildSettings));
		var render = GetComponent<Renderer>();
		mDefaultColor = render.material.color;
	}
	// ------------------------------------------------------------------------
	/// @brief 更新
	// ------------------------------------------------------------------------
	void Update()
	{
		if(IsEnding())
		{
			UpdateEnding();
			return;
		}
		UpdatePlayer();
	}
	// ------------------------------------------------------------------------
	/// @brief エンディング
	// ------------------------------------------------------------------------
	void UpdateEnding()
	{
		mAnim += Time.deltaTime * 0.5f;
		var render = GetComponent<Renderer>();
		render.material.color = Color.Lerp(mDefaultColor, Color.white, Mathf.Min(mAnim, 1.0f));
		if(mAnim < 1.0f)
		{
			return;
		}
		ShowTelop("Thanks for Playing");
		if(Input.GetKeyDown(KeyCode.S))
		{
			Restart();
		}
	}
	// ------------------------------------------------------------------------
	/// @brief プレイヤー更新
	// ------------------------------------------------------------------------
	void UpdatePlayer()
	{
		// テロップ消す
		if(Time.timeSinceLevelLoad > 1.0f && !IsClear())
		{
			if(mTelop != null)
			{
				mTelop.gameObject.SetActive(false);
			}
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
		if(!IsClear())
		{
			return;
		}
		if(mTelop == null)
		{
			return;
		}
		// クリアテロップ
		if(!IsEnding())
		{
			ShowTelop("CLEAR!!\n(Press S Ksy)");
			return;
		}
	}
	// ------------------------------------------------------------------------
	/// @brief テロップを出す
	///
	/// @param inText
	// ------------------------------------------------------------------------
	void ShowTelop(string inText)
	{
		if(mTelop == null)
		{
			return;
		}
		mTelop.gameObject.SetActive(true);
		mTelop.text = inText;
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
	// ------------------------------------------------------------------------
	/// @brief 最後のステージをクリアしたか
	///
	/// @return
	// ------------------------------------------------------------------------
	bool IsEnding()
	{
		return SceneManager.GetActiveScene().buildIndex == SceneManager.sceneCountInBuildSettings - 1 && IsClear();
	}
}
