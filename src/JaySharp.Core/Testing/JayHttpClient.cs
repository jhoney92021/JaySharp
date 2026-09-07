using System.Net;
using System.Net.Http.Headers;

namespace JaySharp.Testing;

/// <summary>
/// Provides utility methods to construct mock <see cref="HttpClient"/> instances for testing API clients without making live network calls.
/// </summary>
public static class JayHttpClient
{
    /// <summary>
    /// Creates a mock <see cref="HttpClient"/> that returns the specified response body and status code.
    /// </summary>
    /// <param name="responseBody">The string or JSON content to return in the response body.</param>
    /// <param name="statusCode">The HTTP status code to return. Defaults to <see cref="HttpStatusCode.OK"/>.</param>
    /// <param name="mediaType">The media type for the response content. Defaults to "application/json".</param>
    /// <returns>A configured <see cref="HttpClient"/> instance.</returns>
    public static HttpClient Create(string responseBody, HttpStatusCode statusCode = HttpStatusCode.OK, string mediaType = "application/json")
    {
        FakeHttpMessageHandler handler = new(responseBody, statusCode, mediaType);
        return new HttpClient(handler);
    }
}

/// <summary>
/// Internal mock HTTP message handler that intercepts requests and returns canned HTTP responses.
/// </summary>
internal class FakeHttpMessageHandler : HttpMessageHandler
{
    private readonly string _responseBody;
    private readonly HttpStatusCode _statusCode;
    private readonly string _mediaType;

    public FakeHttpMessageHandler(string responseBody, HttpStatusCode statusCode, string mediaType)
    {
        _responseBody = responseBody ?? string.Empty;
        _statusCode = statusCode;
        _mediaType = mediaType;
    }

    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        HttpResponseMessage response = new(_statusCode)
        {
            Content = new StringContent(_responseBody)
        };
        response.Content.Headers.ContentType = new MediaTypeHeaderValue(_mediaType);
        return Task.FromResult(response);
    }
}
