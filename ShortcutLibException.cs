using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ShortcutLib
{
    public class ShortcutLibException : Exception
    {
        public ShortcutLibException() : base() { }

        public ShortcutLibException(string message) : base(message) { }

        public ShortcutLibException(string message, Exception innerException) : base(message, innerException) { }

        protected ShortcutLibException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
