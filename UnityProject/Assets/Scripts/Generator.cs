using UnityEngine;

public class Generator : MonoBehaviour
{
	[SerializeField]
	GameObject mBall;

	// Update is called once per frame
	void Update()
	{
		if(Input.GetButtonDown("Fire1"))
		{
			Generate();
		}
	}
	void Generate()
	{
		var ball = Instantiate(mBall);
		// var rigid = ball.GetComponent<Rigidbody>();
		ball.transform.position = transform.position;
		ball.SetActive(true);
	//	rigid.MovePosition(transform.position);
	}
}
