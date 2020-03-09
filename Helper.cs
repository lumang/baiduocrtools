using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BD文字识别工具
{
    class Helper
    {
    }
    public class ResultItem
    {
        /// <summary>
        /// 
        /// </summary>
        public string request_id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string result_data { get; set; }
        /// <summary>
        /// 已完成
        /// </summary>
        public string ret_msg { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int percent { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int ret_code { get; set; }
    }

    public class Result
    {
        /// <summary>
        /// 
        /// </summary>
        public string result_data { get; set; }
        /// <summary>
        /// 已完成
        /// </summary>
        public string ret_msg { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string request_id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int percent { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int ret_code { get; set; }
    }

    public class Root2
    {
        /// <summary>
        /// 
        /// </summary>
        public Result result { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Int64 log_id { get; set; }
    }
    public class Root
    {
        /// <summary>
        /// 
        /// </summary>
        public List<ResultItem> result { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Int64 log_id { get; set; }
    }
    internal class CustomHelp
    {

    }

    public class baiduJson
    {
        public class Location
        {
            public int left
            {
                get;
                set;
            }
            public int top
            {
                get;
                set;
            }
            public int width
            {
                get;
                set;
            }
            public int height
            {
                get;
                set;
            }
        }
        public class Words_resultItem
        {
            public Location location
            {
                get;
                set;
            }
            public string words
            {
                get;
                set;
            }
        }
        public class Root
        {
            public int error_code
            {
                get;
                set;
            }
            public string error_msg
            {
                get;
                set;
            }
            public List<Words_resultItem> words_result
            {
                get;
                set;
            }
        }
    }
}
