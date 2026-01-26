using Core.Utilities.Results.Abstract;
using System;
using System.Collections.Generic;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Core.Utilities.Results.Concrete
{
	public class SuccessDataResult<T> : DataResult<T>
	{
		
		public SuccessDataResult() : base(true,default)
		{
		}

		public SuccessDataResult(T data) : base(true,data)
		{
		}

		public SuccessDataResult(T data, string message) : base(true, data, message)
		{
		}
		public SuccessDataResult(string message) : base(true, default, message)
		{
		}
	}
}
