using UnityEngine;
using System.Collections;

public class GIGI : MonoBehaviour {
	Vector3 initPos;
	Vector3 initScale;
	void Start()
	{
		initPos = gameObject.GetComponent<Transform>().position;
		initScale = gameObject.GetComponent<Transform>().localScale;
	}
	public void RestartPos()
	{
		this.gameObject.GetComponent<Transform>().position = initPos;
		this.gameObject.GetComponent<Transform>().localScale = initScale;
	}
}
