using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Web.Interfaces;
using System.Text;
using Rhino.Mocks;

namespace NNS.Authentication.OAuth2.TestClient
{

    class Helpers
    {

        internal static IWebOperationContext GetWebcontext(String method = "GET")
        {
            var webcontext = MockRepository.GenerateStub<IWebOperationContext>();
            webcontext.Stub(x => x.OutgoingResponse).Return(MockRepository.GenerateStub<IOutgoingWebResponseContext>());
            webcontext.Stub(x => x.IncomingRequest).Return(MockRepository.GenerateStub<IIncomingWebRequestContext>());
            webcontext.IncomingRequest.UriTemplateMatch = new UriTemplateMatch();
            webcontext.IncomingRequest.UriTemplateMatch.BaseUri = new Uri("http://example.com/datagridservice/");
            webcontext.IncomingRequest.Stub(x => x.Method).Return(method);
            return webcontext;
        }
    }
}
