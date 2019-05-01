using System;

namespace BinaryFilesReader.Extensions
{
	public static class TypeExtensions
	{
		/// <summary>
		/// Checks whether the provided type is a simple type (ValueType or a string).
		/// </summary>
		/// <param name="type">Type to check.</param>
		/// <returns>True of the type is Value type or a string, false otherwise.</returns>
		public static bool IsSimpleType(this Type type)
		{
			return type.IsValueType || type == typeof(string);
		}

		/// <summary>
		/// Converts the object to an instance of the specified type.
		/// </summary>
		/// <param name="type">Resulting type.</param>
		/// <param name="value">Value to convert.</param>
		/// <param name="error">Error message.</param>
		/// <returns>Instance of an object if the conversion was successful, null otherwise.</returns>
		public static object ConvertObject(this Type type, object value, out string error)
		{
			object instance;
			try
			{
				instance = Convert.ChangeType(value, type ?? throw new InvalidOperationException());
			}
			catch (Exception e)
			{
				error = e.Message + e.InnerException?.Message;
				return null;
			}

			error = string.Empty;
			return instance;
		}
	}
}