namespace Trackyt.Core.Exceptions
{
	using System;

	public class BaseCoreException : Exception
    {
		public BaseCoreException()
		{

		}

		public BaseCoreException(string msg)
			: base(msg)
        {

        }
    }
}