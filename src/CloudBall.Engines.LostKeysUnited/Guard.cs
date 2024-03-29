﻿using System;
using System.Diagnostics;

namespace CloudBall.Engines.LostKeysUnited
{
	/// <summary>Supplies input parameter guarding.</summary>
	public static class Guard
	{
		/// <summary>Guards the parameter if not null, otherwise throws an argument (null) exception.</summary>
		/// <typeparam name="T">
		/// The type to guard, can not be a structure.
		/// </typeparam>
		/// <param name="param">
		/// The parameter to guard.
		/// </param>
		/// <param name="paramName">
		/// The name of the parameter.
		/// </param>
		[DebuggerStepThrough]
		public static T NotNull<T>(T param, string paramName) where T : class
		{
			if (object.ReferenceEquals(param, null))
			{
				throw new ArgumentNullException(paramName);
			}
			return param;
		}

		/// <summary>Guards the parameter if not null or an empty string, otherwise throws an argument (null) exception.</summary>
		/// <param name="param">
		/// The parameter to guard.
		/// </param>
		/// <param name="paramName">
		/// The name of the parameter.
		/// </param>
		[DebuggerStepThrough]
		public static string NotNullOrEmpty(string param, string paramName)
		{
			NotNull(param, paramName);
			if (string.Empty.Equals(param))
			{
				throw new ArgumentException("Should not be an empty string.", paramName);
			}
			return param;
		}
	}
}
