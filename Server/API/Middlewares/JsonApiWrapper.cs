using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace API.Middlewares
{
    /// <summary>
    /// JsonApiWrapper middle-ware class
    /// It handles all responses in order to maintain consistence under api:json
    /// specification. Strictly preserves data structure into a common response
    /// wrapper in order to avoid inconsistency. 
    /// The specification and style-guide can be found at http://jsonapi.org/
    /// </summary>
    public class JsonApiWrapper
    {
        /// <summary>
        /// API version class member
        /// </summary>
        private String ApiVersion;

        /// <summary>
        /// API name class member
        /// </summary>
        private String ApiName;

        /// <summary>
        /// Request processing task member
        /// </summary>
        private readonly RequestDelegate Next;

        /// <summary>
        /// Initializes a new JsonApiWrapper Middle-ware instance.
        /// </summary>
        /// <param name="version">API version to be shown in all responses</param>
        /// <param name="name">API name to be shown in all responses</param>
        /// <param name="requestDelegate">A task that represents the completion of request processing</param>
        public JsonApiWrapper(string version, string name, RequestDelegate requestDelegate)
        {
            this.ApiVersion = version;
            this.ApiName = name;
            this.Next = requestDelegate;
        }

        /// <summary>
        /// Response post-processing handler.
        /// </summary>
        /// <param name="context">HttpContext object</param>
        /// <returns></returns>
        public async Task Invoke(HttpContext context)
        {
            Stream responseBody = context.Response.Body;

            using (MemoryStream memoryStream = new MemoryStream())
            {
                context.Response.Body = memoryStream;

                await this.Next(context);

                memoryStream.Position = 0;
                string responseString = new StreamReader(memoryStream).ReadToEnd();
                string wrappedResponse = this.Wrap(responseString, context);
                byte[] responseBytes = Encoding.UTF8.GetBytes(wrappedResponse);

                context.Response.Headers["Content-type"] = "application/vnd.api+json";
                context.Response.Body = responseBody;
                await context.Response.Body.WriteAsync(responseBytes, 0, responseBytes.Length);
            }
        }

        /// <summary>
        /// Wraps body content into json:api specification.
        /// </summary>
        /// <param name="originalBody">Original body content</param>
        /// <param name="context">HttpContext object</param>
        /// <returns>Wrapped JSON string</returns>
        private String Wrap(string originalBody, HttpContext context)
        {
            dynamic response;
            var isObj = false;
            if (this.IsJsonString<dynamic>(originalBody))
            {
                response = JsonConvert.DeserializeObject<dynamic>(originalBody);
                isObj = true;
            }
            else
                response = originalBody;

            object wrapper;
            if (this.IsSuccessResponse(context.Response.StatusCode))
                wrapper = this.DataWrap(response);
            else
            {
                if (isObj)
                {
                    wrapper = response;
                }
                else
                {
                    wrapper = this.ErrorWrap(response, context);
                }

            }

            string newBody = JsonConvert.SerializeObject(wrapper, new JsonSerializerSettings()
            {
                Formatting = Formatting.Indented,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                NullValueHandling = NullValueHandling.Ignore,
                ContractResolver = new DefaultContractResolver()
                {
                    NamingStrategy = new CamelCaseNamingStrategy()
                    {
                        ProcessDictionaryKeys = true
                    }
                }
            });

            return newBody;
        }

        /// <summary>
        /// Retrieves api:json's api root object
        /// </summary>
        /// <returns>API object info</returns>
        private Object ApiInfo()
        {
            return new
            {
                version = this.ApiVersion,
                name = this.ApiName
            };
        }

        /// <summary>
        /// Rewrites original body content for success wrapping.
        /// </summary>
        /// <param name="response">Response body</param>
        /// <returns>Formatted object</returns>
        private Object DataWrap(dynamic response)
        {
            if (response != null && ((Type)response.GetType()).Name == "String")
            {
                var str = (string)response.ToString();
                return new
                {
                    message = str.TrimStart('"').TrimEnd('"'),
                };
            }

            return new
            {
                data = response
            };
        }

        /// <summary>
        /// Rewrites original body content for error wrapping.
        /// </summary>
        /// <param name="response">Response body</param>
        /// <returns>Formatted object</returns>
        private Object ErrorWrap(string response, HttpContext context)
        {
            string reason = String.IsNullOrWhiteSpace(response) ? this.StatusCodeMessage(context.Response.StatusCode) : response;

            return new
            {
                errorCode = context.Response.StatusCode,
                message = reason
            };
        }

        /// <summary>
        /// Determines whether string is a valid JSON (then, serialize) or not.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        private Boolean IsJsonString<T>(string text)
        {
            text = text.Trim();
            if ((text.StartsWith("{") && text.EndsWith("}")) || (text.StartsWith("[") && text.EndsWith("]")))
            {
                try
                {
                    var obj = JsonConvert.DeserializeObject<T>(text);
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Determine whether a response is a success or not by its status code.
        /// </summary>
        /// <param name="statusCode"></param>
        /// <returns></returns>
        private Boolean IsSuccessResponse(int statusCode)
        {
            return (statusCode >= 200 && statusCode < 299);
        }

        private String StatusCodeMessage(int statusCode)
        {
            switch (statusCode)
            {
                case 400:
                    return "Bad request.";
                case 401:
                    return "Unauthorized access.";
                case 402:
                    return "Payment required.";
                case 403:
                    return "Forbidden access.";
                case 404:
                    return "Resource not found.";
                case 405:
                    return "Method not allowed.";
                case 406:
                    return "Not acceptable.";
                case 407:
                    return "Proxy authentication required.";
                case 408:
                    return "Request timeout.";
                case 409:
                    return "Conflict";
                case 410:
                    return "Resource is gone.";
                case 411:
                    return "Length is required.";
                case 500:
                    return "Internal server error.";
                case 501:
                    return "Not implemented.";
                case 502:
                    return "Bad gateway.";
                case 503:
                    return "Service unavailable.";
                case 504:
                    return "Gateway timeout.";
                case 505:
                    return "HTTP version not supported.";
            }
            return "";
        }

    }
}
