namespace LOB.Util.Contract.Messaging {
    public interface IMessage {
        string Message { get; set; }
        string Description { get; set; }
    }

    public enum MessageType {
        Ok,
        Info,
        Warn,
        Error
    }
}