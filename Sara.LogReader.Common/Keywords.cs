using System.Collections.Generic;

namespace Sara.LogReader.Common
{
    public static class Keywords
    {
        public const string ALL = "ALL";
        public const string DATETIMEFORMAT = "MM/dd/yyyy hh:mm:ss.fff tt";
        public const string NA = "NA";
        public const string NETWORK_DIRECTION = "Network Direction";
        public const string NETWORK = "Network";
        public const string SEND_ABBR = "--->";
        public const string RECEIVE_ABBR = "<---";
        public const string SEND_BLOCKING_ABBR = "X--->";
        public const string SEND_TEXT = "Send";
        public const string RECEIVE_TEXT = "Receive";
        public const string SEND_BLOCKING_TEXT = "Send Blocking";
        public const string START = "Start";
        public const string STOP = "Stop";
        public const string TYPE = "Type";
        public const string TIME = "Time";
        public const string DATETIME = "DateTime";
        public const string DATETIME_UPPER = "DATETIME";

        public const string HOSTNAME = "Host";
        public const string IP = "Ip";
        public const string NETWORK_MESSAGE_NAME = "Network Message Name";

        public const string RECEIVER_IP = "ReceiverIP";
        public const string RECEIVER_HOST = "ReceiverHost";
        public const string RECEIVER_PORT = "ReceiverPort";
        public const string SENDER_IP = "SenderIP";
        public const string SENDER_HOST = "SenderHost";
        public const string SENDER_PORT = "SenderPort";
        public const string OTHER = "OTHER";
        public const string FILE_VALUES = "File Values";
        public const string NO_FILE = "No File";


        /// <summary>
        /// Returns a list of 
        /// </summary>
        /// <returns></returns>
        public static List<string> GetKeyWords()
        {
            return new List<string>
            {
                DATETIME,
                HOSTNAME,
                IP,
                NETWORK_MESSAGE_NAME,
                RECEIVER_IP,
                RECEIVER_HOST,
                RECEIVER_PORT,
                SENDER_IP,
                SENDER_HOST,
                SENDER_PORT,
                SEND_TEXT,
                SEND_BLOCKING_TEXT,
                RECEIVE_TEXT,
                NETWORK_DIRECTION,
                SOURCE_TYPE
            };
        }

        public static List<string> GetOperators()
        {
            return new List<string>
            {
                EQUAL,
                NOT_EQUAL
            };
        }

        public const string EQUAL = "=";
        public const string NOT_EQUAL = "!=";

        public const string SOURCE_TYPE = "Source Type";
        public const string SOURCE_TYPE_UNKNOWN = "UNKNOWN";
        public const string SOURCE_TYPE_ANY = "ALL";
    }
}
