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

using System.Collections.Generic;
using System.ServiceModel.Channels;
using System.ServiceModel.Security;

namespace System.ServiceModel
{
	public interface IOperationContext
	{
		// Events
		event EventHandler OperationCompleted;

		// Methods
		T GetCallbackChannel<T>();
		void SetTransactionComplete();

		// Properties
		IContextChannel Channel { get; }
		IExtensionCollection<OperationContext> Extensions { get; }
		bool HasSupportingTokens { get; }
		ServiceHostBase Host { get; }
		MessageHeaders IncomingMessageHeaders { get; }
		MessageProperties IncomingMessageProperties { get; }
		MessageVersion IncomingMessageVersion { get; }
		InstanceContext InstanceContext { get; }
		bool IsUserContext { get; }
		MessageHeaders OutgoingMessageHeaders { get; }
		MessageProperties OutgoingMessageProperties { get; }
		RequestContext RequestContext { get; set; }
		IServiceSecurityContext ServiceSecurityContext { get; }
		string SessionId { get; }
		ICollection<SupportingTokenSpecification> SupportingTokens { get; }
	}


}
