using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

/// <summary>
/// Adapter used in development requiring a HttpClient.
/// Using the adapter in place of the actual HttpClient allows requests to be mocked.
/// </summary>
public class HttpClientAdapter : IHttpClient, IDisposable
{
    /// <summary>
    /// Initializes a new instance of the <see cref="HttpClientAdapter"/> class for use in development or testing.
    /// </summary>
    public HttpClientAdapter()
    {
        _httpClient = new HttpClient();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="HttpClientAdapter"/> class for use in development or testing.
    /// </summary>
    /// <param name="handler">Custom message handler.</param>
    public HttpClientAdapter(HttpMessageHandler handler)
    {
        _httpClient = new HttpClient(handler);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="HttpClientAdapter"/> class for use in development or testing.
    /// </summary>
    /// <param name="handler">Custom message handler.</param>
    /// <param name="disposeHandler">Dispose.</param>
    public HttpClientAdapter(HttpMessageHandler handler, bool disposeHandler)
    {
        _httpClient = new HttpClient(handler, disposeHandler);
    }

    private readonly HttpClient _httpClient;

    #region Properties
    /// <summary>
    /// Gets or sets the base address of Uniform Resource Identifier (URI) of the Internet resource used when sending requests.
    /// </summary>
    /// <value>The base address of Uniform Resource Identifier (URI) of the Internet resource used when sending requests.</value>
    public virtual Uri BaseAddress { get; set; }

    /// <summary>
    /// Gets the headers which should be sent with each request.
    /// </summary>
    /// <value>The headers which should be sent with each request.</value>
    public virtual HttpRequestHeaders DefaultRequestHeaders { get; }

    /// <summary>
    /// Gets or sets the number of milliseconds to wait before the request times out.
    /// </summary>
    /// <value>The number of milliseconds to wait before the request times out.</value>
    public virtual TimeSpan Timeout { get; set; }

    /// <summary>
    /// Gets or sets the maximum number of bytes to buffer when reading the response content.
    /// </summary>
    /// <value>The maximum number of bytes to buffer when reading the response content. The default value for this property is 2 gigabytes.</value>
    public virtual long MaxResponseContentBufferSize { get; set; }
    #endregion

    #region Methods
    /// <summary>
    /// Cancel all pending requests on this instance.
    /// </summary>
    public virtual void CancelPendingRequests()
    {
        _httpClient.CancelPendingRequests();
    }

    /// <summary>
    /// Send a DELETE request to the specified Uri as an asynchronous operation.
    /// </summary>
    /// <param name="requestUri">The Uri the request is sent to.</param>
    /// <returns>The task object representing the asynchronous operation.</returns>
    public virtual Task<HttpResponseMessage> DeleteAsync(string requestUri)
    {
        return _httpClient.DeleteAsync(requestUri);
    }

    /// <summary>
    /// Send a DELETE request to the specified Uri with a cancellation token as an asynchronous operation.
    /// </summary>
    /// <param name="requestUri">The Uri the request is sent to.</param>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
    /// <returns>The task object representing the asynchronous operation.</returns>
    public virtual Task<HttpResponseMessage> DeleteAsync(string requestUri, CancellationToken cancellationToken)
    {
        return _httpClient.DeleteAsync(requestUri, cancellationToken);
    }

    /// <summary>
    /// Send a DELETE request to the specified Uri with a cancellation token as an asynchronous operation.
    /// </summary>
    /// <param name="requestUri">The Uri the request is sent to.</param>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
    /// <returns>The task object representing the asynchronous operation.</returns>
    public virtual Task<HttpResponseMessage> DeleteAsync(Uri requestUri, CancellationToken cancellationToken)
    {
        return _httpClient.DeleteAsync(requestUri, cancellationToken);
    }

    /// <summary>
    /// Send a DELETE request to the specified Uri as an asynchronous operation.
    /// </summary>
    /// <param name="requestUri">The Uri the request is sent to.</param>
    /// <returns>The task object representing the asynchronous operation.</returns>
    public virtual Task<HttpResponseMessage> DeleteAsync(Uri requestUri)
    {
        return _httpClient.DeleteAsync(requestUri);
    }

    /// <summary>
    /// Send a GET request to the specified Uri with an HTTP completion option and a cancellation token as an asynchronous operation.
    /// </summary>
    /// <param name="requestUri">The Uri the request is sent to.</param>
    /// <param name="completionOption">An HTTP completion option value that indicates when the operation should be considered completed.</param>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
    /// <returns>The task object representing the asynchronous operation.</returns>
    public virtual Task<HttpResponseMessage> GetAsync(string requestUri, HttpCompletionOption completionOption, CancellationToken cancellationToken)
    {
        return _httpClient.GetAsync(requestUri, completionOption, cancellationToken);
    }

    /// <summary>
    /// Send a GET request to the specified Uri with an HTTP completion option and a cancellation token as an asynchronous operation.
    /// </summary>
    /// <param name="requestUri">The Uri the request is sent to.</param>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
    /// <returns>The task object representing the asynchronous operation.</returns>
    public virtual Task<HttpResponseMessage> GetAsync(Uri requestUri, CancellationToken cancellationToken)
    {
        return _httpClient.GetAsync(requestUri, cancellationToken);
    }

    /// <summary>
    /// Send a GET request to the specified Uri with an HTTP completion option and a cancellation token as an asynchronous operation.
    /// </summary>
    /// <param name="requestUri">The Uri the request is sent to.</param>
    /// <param name="completionOption">An HTTP completion option value that indicates when the operation should be considered completed.</param>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
    /// <returns>The task object representing the asynchronous operation.</returns>
    public virtual Task<HttpResponseMessage> GetAsync(Uri requestUri, HttpCompletionOption completionOption, CancellationToken cancellationToken)
    {
        return _httpClient.GetAsync(requestUri, completionOption, cancellationToken);
    }

    /// <summary>
    /// Send a GET request to the specified Uri with an HTTP completion option and a cancellation token as an asynchronous operation.
    /// </summary>
    /// <param name="requestUri">The Uri the request is sent to.</param>
    /// <param name="completionOption">An HTTP completion option value that indicates when the operation should be considered completed.</param>
    /// <returns>The task object representing the asynchronous operation.</returns>
    public virtual Task<HttpResponseMessage> GetAsync(Uri requestUri, HttpCompletionOption completionOption)
    {
        return _httpClient.GetAsync(requestUri, completionOption);
    }

    /// <summary>
    /// Send a GET request to the specified Uri with an HTTP completion option and a cancellation token as an asynchronous operation.
    /// </summary>
    /// <param name="requestUri">The Uri the request is sent to.</param>
    /// <param name="completionOption">An HTTP completion option value that indicates when the operation should be considered completed.</param>
    /// <returns>The task object representing the asynchronous operation.</returns>
    public virtual Task<HttpResponseMessage> GetAsync(string requestUri, HttpCompletionOption completionOption)
    {
        return _httpClient.GetAsync(requestUri, completionOption);
    }

    /// <summary>
    /// Send a GET request to the specified Uri with an HTTP completion option and a cancellation token as an asynchronous operation.
    /// </summary>
    /// <param name="requestUri">The Uri the request is sent to.</param>
    /// <returns>The task object representing the asynchronous operation.</returns>
    public virtual Task<HttpResponseMessage> GetAsync(Uri requestUri)
    {
        return _httpClient.GetAsync(requestUri);
    }

    /// <summary>
    /// Send a GET request to the specified Uri with an HTTP completion option and a cancellation token as an asynchronous operation.
    /// </summary>
    /// <param name="requestUri">The Uri the request is sent to.</param>
    /// <returns>The task object representing the asynchronous operation.</returns>
    public virtual Task<HttpResponseMessage> GetAsync(string requestUri)
    {
        return _httpClient.GetAsync(requestUri);
    }

    /// <summary>
    /// Send a GET request to the specified Uri with an HTTP completion option and a cancellation token as an asynchronous operation.
    /// </summary>
    /// <param name="requestUri">The Uri the request is sent to.</param>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
    /// <returns>The task object representing the asynchronous operation.</returns>
    public virtual Task<HttpResponseMessage> GetAsync(string requestUri, CancellationToken cancellationToken)
    {
        return _httpClient.GetAsync(requestUri, cancellationToken);
    }

    /// <summary>
    /// Send a GET request to the specified Uri and return the response body as a byte array in an asynchronous operation.
    /// </summary>
    /// <param name="requestUri">The Uri the request is sent to.</param>
    /// <returns>The task object representing the asynchronous operation.</returns>
    public virtual Task<byte[]> GetByteArrayAsync(string requestUri)
    {
        return _httpClient.GetByteArrayAsync(requestUri);
    }

    /// <summary>
    /// Send a GET request to the specified Uri and return the response body as a byte array in an asynchronous operation.
    /// </summary>
    /// <param name="requestUri">The Uri the request is sent to.</param>
    /// <returns>The task object representing the asynchronous operation.</returns>
    public virtual Task<byte[]> GetByteArrayAsync(Uri requestUri)
    {
        return _httpClient.GetByteArrayAsync(requestUri);
    }

    /// <summary>
    /// Send a GET request to the specified Uri and return the response body as a byte array in an asynchronous operation.
    /// </summary>
    /// <param name="requestUri">The Uri the request is sent to.</param>
    /// <returns>The task object representing the asynchronous operation.</returns>
    public virtual Task<Stream> GetStreamAsync(Uri requestUri)
    {
        return _httpClient.GetStreamAsync(requestUri);
    }

    /// <summary>
    /// Send a GET request to the specified Uri and return the response body as a byte array in an asynchronous operation.
    /// </summary>
    /// <param name="requestUri">The Uri the request is sent to.</param>
    /// <returns>The task object representing the asynchronous operation.</returns>
    public virtual Task<Stream> GetStreamAsync(string requestUri)
    {
        return _httpClient.GetStreamAsync(requestUri);
    }

    /// <summary>
    /// Send a GET request to the specified Uri and return the response body as a string in an asynchronous operation.
    /// </summary>
    /// <param name="requestUri">The Uri the request is sent to.</param>
    /// <returns>The task object representing the asynchronous operation.</returns>
    public virtual Task<string> GetStringAsync(string requestUri)
    {
        return _httpClient.GetStringAsync(requestUri);
    }

    /// <summary>
    /// Send a GET request to the specified Uri and return the response body as a string in an asynchronous operation.
    /// </summary>
    /// <param name="requestUri">The Uri the request is sent to.</param>
    /// <returns>The task object representing the asynchronous operation.</returns>
    public virtual Task<string> GetStringAsync(Uri requestUri)
    {
        return _httpClient.GetStringAsync(requestUri);
    }

    /// <summary>
    /// Send a POST request with a cancellation token as an asynchronous operation.
    /// </summary>
    /// <param name="requestUri">The Uri the request is sent to.</param>
    /// <param name="content">The HTTP request content sent to the server.</param>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
    /// <returns>The task object representing the asynchronous operation.</returns>
    public virtual Task<HttpResponseMessage> PostAsync(string requestUri, HttpContent content, CancellationToken cancellationToken)
    {
        return _httpClient.PostAsync(requestUri, content, cancellationToken);
    }

    /// <summary>
    /// Send a POST request with a cancellation token as an asynchronous operation.
    /// </summary>
    /// <param name="requestUri">The Uri the request is sent to.</param>
    /// <param name="content">The HTTP request content sent to the server.</param>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
    /// <returns>The task object representing the asynchronous operation.</returns>
    public virtual Task<HttpResponseMessage> PostAsync(Uri requestUri, HttpContent content, CancellationToken cancellationToken)
    {
        return _httpClient.PostAsync(requestUri, content, cancellationToken);
    }

    /// <summary>
    /// Send a POST request with a cancellation token as an asynchronous operation.
    /// </summary>
    /// <param name="requestUri">The Uri the request is sent to.</param>
    /// <param name="content">The HTTP request content sent to the server.</param>
    /// <returns>The task object representing the asynchronous operation.</returns>
    public virtual Task<HttpResponseMessage> PostAsync(Uri requestUri, HttpContent content)
    {
        return _httpClient.PostAsync(requestUri, content);
    }

    /// <summary>
    /// Send a POST request with a cancellation token as an asynchronous operation.
    /// </summary>
    /// <param name="requestUri">The Uri the request is sent to.</param>
    /// <param name="content">The HTTP request content sent to the server.</param>
    /// <returns>The task object representing the asynchronous operation.</returns>
    public virtual Task<HttpResponseMessage> PostAsync(string requestUri, HttpContent content)
    {
        return _httpClient.PostAsync(requestUri, content);
    }

    /// <summary>
    /// Send a PUT request with a cancellation token as an asynchronous operation.
    /// </summary>
    /// <param name="requestUri">The Uri the request is sent to.</param>
    /// <param name="content">The HTTP request content sent to the server.</param>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
    /// <returns>The task object representing the asynchronous operation.</returns>
    public virtual Task<HttpResponseMessage> PutAsync(string requestUri, HttpContent content, CancellationToken cancellationToken)
    {
        return _httpClient.PutAsync(requestUri, content, cancellationToken);
    }

    /// <summary>
    /// Send a PUT request with a cancellation token as an asynchronous operation.
    /// </summary>
    /// <param name="requestUri">The Uri the request is sent to.</param>
    /// <param name="content">The HTTP request content sent to the server.</param>
    /// <returns>The task object representing the asynchronous operation.</returns>
    public virtual Task<HttpResponseMessage> PutAsync(string requestUri, HttpContent content)
    {
        return _httpClient.PutAsync(requestUri, content);
    }

    /// <summary>
    /// Send a PUT request with a cancellation token as an asynchronous operation.
    /// </summary>
    /// <param name="requestUri">The Uri the request is sent to.</param>
    /// <param name="content">The HTTP request content sent to the server.</param>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
    /// <returns>The task object representing the asynchronous operation.</returns>
    public virtual Task<HttpResponseMessage> PutAsync(Uri requestUri, HttpContent content, CancellationToken cancellationToken)
    {
        return _httpClient.PutAsync(requestUri, content, cancellationToken);
    }

    /// <summary>
    /// Send a PUT request with a cancellation token as an asynchronous operation.
    /// </summary>
    /// <param name="requestUri">The Uri the request is sent to.</param>
    /// <param name="content">The HTTP request content sent to the server.</param>
    /// <returns>The task object representing the asynchronous operation.</returns>
    public virtual Task<HttpResponseMessage> PutAsync(Uri requestUri, HttpContent content)
    {
        return _httpClient.PutAsync(requestUri, content);
    }

    /// <summary>
    /// Send an HTTP request as an asynchronous operation.
    /// </summary>
    /// <param name="request">The HTTP request message to send.</param>
    /// <returns>The task object representing the asynchronous operation.</returns>
    public virtual Task<HttpResponseMessage> SendAsync(HttpRequestMessage request)
    {
        return _httpClient.SendAsync(request);
    }

    /// <summary>
    /// Send an HTTP request as an asynchronous operation.
    /// </summary>
    /// <param name="request">The HTTP request message to send.</param>
    /// <param name="cancellationToken">The cancellation token to cancel operation.</param>
    /// <returns>The task object representing the asynchronous operation.</returns>
    public virtual Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        return _httpClient.SendAsync(request, cancellationToken);
    }

    /// <summary>
    /// Send an HTTP request as an asynchronous operation.
    /// </summary>
    /// <param name="request">The HTTP request message to send.</param>
    /// <param name="completionOption">When the operation should complete (as soon as a response is available or after reading the whole response content).</param>
    /// <returns>The task object representing the asynchronous operation.</returns>
    public virtual Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, HttpCompletionOption completionOption)
    {
        return _httpClient.SendAsync(request, completionOption);
    }

    /// <summary>
    /// Send an HTTP request as an asynchronous operation.
    /// </summary>
    /// <param name="request">The HTTP request message to send.</param>
    /// <param name="completionOption">When the operation should complete (as soon as a response is available or after reading the whole response content).</param>
    /// <param name="cancellationToken">The cancellation token to cancel operation.</param>
    /// <returns>The task object representing the asynchronous operation.</returns>
    public virtual Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, HttpCompletionOption completionOption, CancellationToken cancellationToken)
    {
        return _httpClient.SendAsync(request, completionOption, cancellationToken);
    }

    public virtual void Dispose()
    {
        _httpClient.Dispose();
    }
    #endregion
}

internal interface IHttpClient
{
    void CancelPendingRequests();
    Task<HttpResponseMessage> DeleteAsync(string requestUri);
    Task<HttpResponseMessage> DeleteAsync(string requestUri, CancellationToken cancellationToken);
    Task<HttpResponseMessage> DeleteAsync(Uri requestUri, CancellationToken cancellationToken);
    Task<HttpResponseMessage> DeleteAsync(Uri requestUri);
    Task<HttpResponseMessage> GetAsync(string requestUri, HttpCompletionOption completionOption, CancellationToken cancellationToken);
    Task<HttpResponseMessage> GetAsync(Uri requestUri, CancellationToken cancellationToken);
    Task<HttpResponseMessage> GetAsync(Uri requestUri, HttpCompletionOption completionOption, CancellationToken cancellationToken);
    Task<HttpResponseMessage> GetAsync(Uri requestUri, HttpCompletionOption completionOption);
    Task<HttpResponseMessage> GetAsync(string requestUri, HttpCompletionOption completionOption);
    Task<HttpResponseMessage> GetAsync(Uri requestUri);
    Task<HttpResponseMessage> GetAsync(string requestUri);
    Task<HttpResponseMessage> GetAsync(string requestUri, CancellationToken cancellationToken);
    Task<byte[]> GetByteArrayAsync(string requestUri);
    Task<byte[]> GetByteArrayAsync(Uri requestUri);
    Task<Stream> GetStreamAsync(Uri requestUri);
    Task<Stream> GetStreamAsync(string requestUri);
    Task<string> GetStringAsync(string requestUri);
    Task<string> GetStringAsync(Uri requestUri);
    Task<HttpResponseMessage> PostAsync(string requestUri, HttpContent content, CancellationToken cancellationToken);
    Task<HttpResponseMessage> PostAsync(Uri requestUri, HttpContent content, CancellationToken cancellationToken);
    Task<HttpResponseMessage> PostAsync(Uri requestUri, HttpContent content);
    Task<HttpResponseMessage> PostAsync(string requestUri, HttpContent content);
    Task<HttpResponseMessage> PutAsync(string requestUri, HttpContent content, CancellationToken cancellationToken);
    Task<HttpResponseMessage> PutAsync(string requestUri, HttpContent content);
    Task<HttpResponseMessage> PutAsync(Uri requestUri, HttpContent content, CancellationToken cancellationToken);
    Task<HttpResponseMessage> PutAsync(Uri requestUri, HttpContent content);
    Task<HttpResponseMessage> SendAsync(HttpRequestMessage request);
    Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken);
    Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, HttpCompletionOption completionOption);
    Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, HttpCompletionOption completionOption, CancellationToken cancellationToken);
}