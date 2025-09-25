using Orleans.Serialization;
using Orleans.Serialization.Cloning;
using System.Text.Json;

public class JsonElementCopier : IDeepCopier<JsonElement>
{
    public JsonElement DeepCopy(JsonElement input, CopyContext context)
    {
        // Create a copy using raw JSON text
        return JsonDocument.Parse(input.GetRawText()).RootElement;
    }
}
