#region License

// The MIT License
//
// Copyright (c) 2009 Pablo Mariano Cibraro.
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

#endregion

using System.Net;

namespace System.ServiceModel.Web
{
	public interface IOutgoingWebResponseContext
	{
		void SetStatusAsCreated(Uri locationUri);
		void SetStatusAsNotFound();
		void SetStatusAsNotFound(string description);

		long ContentLength { get; set; }
		string ContentType { get; set; }
		string ETag { get; set; }
		WebHeaderCollection Headers { get; }
		DateTime LastModified { get; set; }
		string Location { get; set; }
		HttpStatusCode StatusCode { get; set; }
		string StatusDescription { get; set; }
		bool SuppressEntityBody { get; set; }
	}

}
