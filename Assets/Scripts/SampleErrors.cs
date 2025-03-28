using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Datadog.Unity;
using Datadog.Unity.Logs;
using System.Runtime.InteropServices;
using Datadog.Unity;


class NotSupportedOnCurrentPlatform : Exception{
	public NotSupportedOnCurrentPlatform() : base("That is not supported in the current platform"){}
}

public class SampleErrors : MonoBehaviour
{
	//DLL imports
#if !UNITY_EDITOR
	[DllImport("__Internal")]
	private static extern int PrintANumber();

	[DllImport("__Internal")]
	private static extern int DivideByZero(int a);

	[DllImport("__Internal")]
	private static extern void Segfault();

	[DllImport("__Internal")]
	private static extern void Abrt();

	[DllImport("__Internal")]
	private static extern void Hang();

	[DllImport("__Internal")]
	private static extern void CppException();

#endif

	List<Texture2D> _textures;

#region Logs
	public void OnLogWarning()
	{
		Debug.LogWarning("Logging warning");
	}

	public void OnLogError()
	{
		Debug.LogError("Logging error");
	}

#endregion

#region C# reports
	public void OnThrowException()
	{
		Debug.Log("OnThrowException");
		throw new Exception("C# exception here");
	}

	public void OnNullReference()
    {
		Debug.Log("OnNullReference");
		GameObject a = null;
		Debug.Log(a.name);
    }

	public void OnOOM()
	{
		Debug.Log("OnOOM");
#if !UNITY_EDITOR
		_textures = new List<Texture2D>();
		while (true)
        {
			_textures.Add(new Texture2D(1024, 1024));
        }
#else
		throw new NotSupportedOnCurrentPlatform();
#endif

	}

	public void OnForceAbort()
	{
		Debug.Log("OnForceAbort");
#if !UNITY_EDITOR
		UnityEngine.Diagnostics.Utils.ForceCrash(UnityEngine.Diagnostics.ForcedCrashCategory.Abort);
#else
		throw new NotSupportedOnCurrentPlatform();
#endif
	}

#endregion

#region C++ reports
	/// <summary>
	/// Don't do anything bad, just verify that the plugin can be correctly accessed
	/// If this doesn't work, none of the other plugin tests will either
	/// </summary>
	public void OnCppPrintHello()
	{
		Debug.Log("OnCppPrintHello");
#if !UNITY_EDITOR
		var number = PrintANumber();
		Debug.Log(String.Format("Hello Cpp! {0}", number));

#else
		throw new NotSupportedOnCurrentPlatform();
#endif
	}

	/// <summary>
	/// The source code for the plugins can be found in the "Assets/Plugins" directory.
	/// Please upload the symbol file to see the callstacks for the plugin
	/// </summary>
	public void OnCppDivideByZero()
	{
		Debug.Log("OnCppDivideByZero");
#if !UNITY_EDITOR
		var result = DivideByZero(2);
		Debug.Log(String.Format("Result: {0}", result));
#else
		throw new NotSupportedOnCurrentPlatform();
#endif

	}

	public void OnCppSegfault()
	{
		Debug.Log("OnCppHang");
#if !UNITY_EDITOR
		Segfault();
#else
		throw new NotSupportedOnCurrentPlatform();
#endif

	}

	public void OnCppAbrt()
	{
		Debug.Log("OnCppAbrt");
#if !UNITY_EDITOR
		Abrt();
#else
		throw new NotSupportedOnCurrentPlatform();
#endif
	}

	public void OnCppHang()
	{
		Debug.Log("OnCppHang");
#if !UNITY_EDITOR
		Hang();
#else
		throw new NotSupportedOnCurrentPlatform();
#endif
	}

	public void OnCppException()
	{
		Debug.Log("OnCppException");
#if !UNITY_EDITOR
		CppException();
#else
		throw new NotSupportedOnCurrentPlatform();
#endif
	}

#endregion
#region JVM reports

	public void OnJvmException()
	{
		Debug.Log("OnJvmException");
#if UNITY_ANDROID && !UNITY_EDITOR
		using (var java = new AndroidJavaObject("international.redpine.example.JvmExample")) {
			java.Call("throwJvmException");
		}
#else
		throw new NotSupportedOnCurrentPlatform();
#endif
	}

	public void OnJvmBackgroundException()
	{
		Debug.Log("OnJvmBackgroundException");
#if UNITY_ANDROID && !UNITY_EDITOR
		using (var java = new AndroidJavaObject("international.redpine.example.JvmExample")) {
			java.Call("throwBackgroundJvmException");
		}
#else
		throw new NotSupportedOnCurrentPlatform();
#endif
	}

	public void OnJvmANR()
	{
		Debug.Log("OnJvmANR");
#if UNITY_ANDROID && !UNITY_EDITOR
		using (var java = new AndroidJavaObject("international.redpine.example.JvmExample")) {
			java.Call("triggerAnr");
		}
#else
		throw new NotSupportedOnCurrentPlatform();
#endif
	
	}


#endregion

    void Start()
    {
		//ask for concent to track errors and logs
        DatadogSdk.Instance.SetTrackingConsent(TrackingConsent.Granted);
    }

}
