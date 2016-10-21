using System;

[Serializable()]
public class ScreenshotException : System.Exception
{
	public ScreenshotException() : base() { }
    public ScreenshotException(string message) : base(message) { }
    public ScreenshotException(string message, System.Exception inner) : base(message, inner) { }

    // A constructor is needed for serialization when an
    // exception propagates from a remoting server to the client. 
    protected ScreenshotException(System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context) { }
}