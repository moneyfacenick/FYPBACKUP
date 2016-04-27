﻿/************************************************************************************

Copyright   :   Copyright 2014 Oculus VR, LLC. All Rights reserved.

Licensed under the Oculus VR Rift SDK License Version 3.2 (the "License");
you may not use the Oculus VR Rift SDK except in compliance with the License,
which is provided at the time of installation or download, or which
otherwise accompanies this software in either electronic or hard copy form.

You may obtain a copy of the License at

http://www.oculusvr.com/licenses/LICENSE-3.2

Unless required by applicable law or agreed to in writing, the Oculus VR SDK
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.

************************************************************************************/

using UnityEngine;

/// <summary>
/// Allows you to toggle chromatic aberration correction with a gamepad button press.
/// </summary>
public class OVRChromaticAberration : MonoBehaviour
{
	/// <summary>
	/// The button that will toggle chromatic aberration correction.
	/// </summary>
	public OVRGamepadController.Button			toggleButton = OVRGamepadController.Button.X;	

	private bool								chromatic = false;

	void Start ()
	{
		// Enable/Disable Chromatic Aberration Correction.
		// NOTE: Enabling Chromatic Aberration for mobile has a large performance cost.
		OVRManager.instance.chromatic = chromatic;
	}

	void Update()
	{
		// NOTE: some of the buttons defined in OVRGamepadController.Button are not available on the Android game pad controller
		if (OVRGamepadController.GPC_GetButtonDown(toggleButton))
		{
			//*************************
			// toggle chromatic aberration correction
			//*************************
			chromatic = !chromatic;
			OVRManager.instance.chromatic = chromatic;
		}
	}

}
