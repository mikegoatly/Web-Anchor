using System.Net.Http;

using WebAnchor.Tests.RequestFactory.Transformation.Transformers.NoCache.Fixtures;
using WebAnchor.Tests.TestUtils;

using Xunit;

namespace WebAnchor.Tests.RequestFactory.Transformation.Transformers.NoCache
{
    public class NoCacheTests : WebAnchorTest
    {
        [Fact]
        public void TestWithNoCacheAttributeOnApiLevel()
        {
            TestTheRequest<IApiWithNoCacheOnApiLevel>(
                api => api.Get(),
                req =>
                    {
                        Assert.Equal(HttpMethod.Get, req.Method);
                        Assert.Contains("_=", req.RequestUri.ToString());
                    });
        }

        [Fact]
        public void TestOnMethodThatDoesNotHaveNoCacheAttribute()
        {
            TestTheRequest<IApiWithBothCachedAndNonCachedMethods>(
                api => api.Cached(),
                req =>
                {
                    Assert.Equal(HttpMethod.Get, req.Method);
                    Assert.True(!req.RequestUri.ToString().Contains("_="));
                });
        }

        [Fact]
        public void TestOnMethodThatDoHaveNoCacheAttribute()
        {
            TestTheRequest<IApiWithBothCachedAndNonCachedMethods>(
                api => api.NotCached(),
                req =>
                {
                    Assert.Equal(HttpMethod.Get, req.Method);
                    Assert.Contains("_=", req.RequestUri.ToString());
                });
        }
    }
}