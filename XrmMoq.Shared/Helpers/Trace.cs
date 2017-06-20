using System.Diagnostics;
using Microsoft.Xrm.Sdk;
using Moq;

namespace XrmMoq.Helpers
{
    internal static class Trace
    {
        /// <summary>
        /// Helper method to create the fake TracingService
        /// </summary>
        /// <param name="tracingServiceMock">The tracing service mock.</param>
        /// <returns>Mock ITracingService.</returns>
        public static Mock<ITracingService> CreateTracingService(Mock<ITracingService> tracingServiceMock)
        {
            tracingServiceMock.Setup(t => t.Trace(It.IsAny<string>(), It.IsAny<object[]>())).Callback<string, object[]>(WriteTrace);

            return tracingServiceMock;
        }

        /// <summary>
        /// Writes the trace to the test Output.
        /// </summary>
        /// <param name="s">Text to write.</param>
        /// <param name="o">Obejct to write.</param>
        public static void WriteTrace(string s, object[] o)
        {
            Debug.WriteLine(s);
        }
    }
}