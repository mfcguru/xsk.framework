using System;
using System.Globalization;
using System.Net;

namespace X.Api.Source.Domain.BusinessRules
{
	public abstract class BusinessRulesException : Exception
	{
		public HttpStatusCode StatusCode { get; set; }

		public BusinessRulesException(HttpStatusCode statusCode) : base() { StatusCode = statusCode; }

		public BusinessRulesException(HttpStatusCode statusCode, string message) : base(message) { StatusCode = statusCode; }

		public BusinessRulesException(HttpStatusCode statusCode, string message, params object[] args)
			: base(String.Format(CultureInfo.CurrentCulture, message, args))
		{
			StatusCode = statusCode;
		}
	}
}
