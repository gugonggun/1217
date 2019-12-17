using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T>: MonoBehaviour where T : MonoBehaviour
{
	private static T mInstance;

	public static T Instance
	{
		get
		{
			if(mInstance == null)
			{
				mInstance = FindObjectOfType<T>();
			}
			return mInstance;
		}
	}

	virtual protected void Awake()
	{
		if(mInstance == null)
		{
			mInstance = this.GetComponent<T>();
			Initialize();
		}
	}

	virtual protected void Start()
	{
		//Initialize();
	}

	// Use this function instead "void Awake()" in MonoBehaviour Class. And Call this Func on GameManager's Awake Function
	virtual protected void Initialize() { }
}