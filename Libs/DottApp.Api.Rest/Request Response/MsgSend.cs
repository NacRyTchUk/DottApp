using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace DottApp.Api.Rest.Request_Response
{
    public class MsgSendRequest : BaseRequest
    {
        [JsonPropertyName("text")] public string Text { get; set; }
        [JsonPropertyName("receiverId")] public int ReceiverId { get; set; }
        [JsonPropertyName("attachment")] public string Attachment { get; set; }
        [JsonPropertyName("attachmentType")] public string AttachmentType { get; set; }
    }

    

}
