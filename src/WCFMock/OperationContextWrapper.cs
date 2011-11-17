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
	/// <summary>
	/// Provides an implementation of <see cref="IOperationContext"/> that 
	/// wraps the underlying runtime <see cref="OperationContext"/>.
	/// </summary>
	public class OperationContextWrapper : IOperationContext
	{
		OperationContext context;

		public OperationContextWrapper(OperationContext context)
		{
			this.context = context;
		}

		public event EventHandler OperationCompleted;

		T IOperationContext.GetCallbackChannel<T>()
		{
			return context.GetCallbackChannel<T>();
		}

		void IOperationContext.SetTransactionComplete()
		{
			context.SetTransactionComplete();
		}

		IContextChannel IOperationContext.Channel
		{
			get { return context.Channel; }
		}

		IExtensionCollection<OperationContext> IOperationContext.Extensions
		{
			get { return context.Extensions; }
		}

		bool IOperationContext.HasSupportingTokens
		{
			get { return context.HasSupportingTokens; }
		}

		ServiceHostBase IOperationContext.Host
		{
			get { return context.Host; }
		}

		MessageHeaders IOperationContext.IncomingMessageHeaders
		{
			get { return context.IncomingMessageHeaders; }
		}

		MessageProperties IOperationContext.IncomingMessageProperties
		{
			get { return context.IncomingMessageProperties; }
		}

		MessageVersion IOperationContext.IncomingMessageVersion
		{
			get { return context.IncomingMessageVersion; }
		}

		InstanceContext IOperationContext.InstanceContext
		{
			get { return context.InstanceContext; }
		}

		bool IOperationContext.IsUserContext
		{
			get { return context.IsUserContext; }
		}

		MessageHeaders IOperationContext.OutgoingMessageHeaders
		{
			get { return context.OutgoingMessageHeaders; }
		}

		MessageProperties IOperationContext.OutgoingMessageProperties
		{
			get { return context.OutgoingMessageProperties; }
		}

		RequestContext IOperationContext.RequestContext
		{
			get
			{
				return context.RequestContext;
			}
			set
			{
				context.RequestContext = value;
			}
		}

		IServiceSecurityContext IOperationContext.ServiceSecurityContext
		{
			get { return new ServiceSecurityContextWrapper(context.ServiceSecurityContext); }
		}

		string IOperationContext.SessionId
		{
			get { return context.SessionId; }
		}

		ICollection<SupportingTokenSpecification> IOperationContext.SupportingTokens
		{
			get { return context.SupportingTokens; }
		}
	}
}
