using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Text;

namespace CocShop.Core.MessageHandler
{
    public class MessageHandler
    {
        private static Dictionary<string, string> _message;
        private static Dictionary<string, string> _errMessage;

        /// <summary>
        /// Get error message
        /// </summary>
        private static Dictionary<string, string> GetErrMessage
        {
            get
            {
                if (_errMessage == null || _errMessage.Count == 0)
                {
                    _errMessage = new Dictionary<string, string>();
                    var assembly = Assembly.GetExecutingAssembly();
                    var rm = new ResourceManager(assembly.GetName().Name + ".MessageHandler.ErrMessage", assembly);

                    foreach (var resource in rm.GetResourceSet(new CultureInfo("en"), true, true))
                    {
                        var r = (DictionaryEntry)resource;
                        _errMessage.Add(r.Key.ToString(), r.Value.ToString());
                    }
                }
                return _errMessage;
            }
        }

        public static string CustomErrMessage(string msgCode, params object[] arguments)
        {
            string msg = GetErrMessage[msgCode];
            return string.Format(msg, arguments);
        }
        /// <summary>
        /// Get error message
        /// </summary>
        private static Dictionary<string, string> GetMessage
        {
            get
            {
                if (_message == null || _message.Count == 0)
                {
                    _message = new Dictionary<string, string>();
                    var assembly = Assembly.GetExecutingAssembly();
                    var rm = new ResourceManager(assembly.GetName().Name + ".MessageHandler.Message", assembly);

                    foreach (var resource in rm.GetResourceSet(new CultureInfo("en"), true, true))
                    {
                        var r = (DictionaryEntry)resource;
                        _message.Add(r.Key.ToString(), r.Value.ToString());
                    }
                }
                return _message;
            }
        }

        public static string CustomMessage(string msgCode, params object[] arguments)
        {
            string msg = GetMessage[msgCode];
            return string.Format(msg, arguments);
        }
    }
}
