﻿using System.Collections.Generic;
using UnityEngine;

namespace Gamelogic
{
	/**
		A response curve with outputs of Vector3.

		@version1_2
	*/
	public class ResponseCurveVector3:ResponseCurveBase<Vector3>
	{
		public ResponseCurveVector3(IEnumerable<float> inputSamples, IEnumerable<Vector3> outputSamples) : base(inputSamples, outputSamples)
		{
		}

		protected override Vector3 Lerp(Vector3 outputSampleMin, Vector3 outputSampleMax, float t)
		{
			return Vector3.Lerp(outputSampleMin, outputSampleMax, t);
		}
	}
}