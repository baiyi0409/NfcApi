namespace NfcApi.Models
{
    public class ResultModel<T>
    {
        /// <summary>
        /// 返回编码
        /// </summary>
        public int code { get; set; }

        /// <summary>
        /// 返回信息
        /// </summary>
        public string message { get; set; }

        /// <summary>
        /// 返回数据
        /// </summary>
        public T data { get; set; }
    }
}
