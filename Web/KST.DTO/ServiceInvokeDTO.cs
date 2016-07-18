using KST.Core;
using KST.Util;
using Newtonsoft.Json;

namespace KST.DTO
{
    /// <summary>
    /// Interface invoke result
    /// </summary>
    public class ServiceInvokeDTO<T>
    {
        /// <summary>
        /// 接口调用的返回代码   1:调用成功   非1：调用失败
        /// </summary>
        [JsonProperty("code")]
        public InvokeCode Code { get; set; }

        /// <summary>
        /// 接口调用返回提示信息
        /// </summary>
        [JsonProperty("msg")]
        public string Message { get; set; }

        /// <summary>
        /// 接口调用返回的结果数据
        /// </summary>
        [JsonProperty("data")]
        public T Data { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        private ServiceInvokeDTO()
        {

        }

        /// <summary>
        /// Create an instance with invoke code and its description and default value of type.
        /// </summary>
        public ServiceInvokeDTO(InvokeCode code)
        {
            this.Code = code;
            this.Message = code.GetDescription();
            this.Data = default(T);
        }

        /// <summary>
        /// Create an instance with invoke code with its message and data.
        /// </summary>
        public ServiceInvokeDTO(InvokeCode code, T data)
        {
            this.Code = code;
            this.Message = code.GetDescription();
            this.Data = data;
        }

        /// <summary>
        ///  Create an instance with code、message and data.
        /// </summary>
        public ServiceInvokeDTO(InvokeCode code, string message, T data)
        {
            this.Code = code;
            this.Message = message;
            this.Data = data;
        }
    }

    /// <summary>
    /// Interface invoke result
    /// </summary>
    public class ServiceInvokeDTO
    {
        /// <summary>
        /// 接口调用的返回代码   1:调用成功   非1：调用失败
        /// </summary>
        [JsonProperty("code")]
        public InvokeCode Code { get; set; }

        /// <summary>
        /// 接口调用返回提示信息
        /// </summary>
        [JsonProperty("msg")]
        public string Message { get; set; }

        /// <summary>
        /// 接口调用返回的结果数据
        /// </summary>
        [JsonProperty("data")]
        public string Data { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        private ServiceInvokeDTO()
        {

        }

        /// <summary>
        /// Create an instance with invoke code and its description.
        /// </summary>
        public ServiceInvokeDTO(InvokeCode code)
        {
            this.Code = code;
            this.Message = code.GetDescription();
            this.Data = string.Empty;
        }

        /// <summary>
        ///  Create an instance with code and message.
        /// </summary>
        public ServiceInvokeDTO(InvokeCode code, string message)
        {
            this.Code = code;
            this.Message = message;
        }
    }
}
