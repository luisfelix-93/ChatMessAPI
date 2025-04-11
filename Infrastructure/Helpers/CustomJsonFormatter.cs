using System.Text.Json;
using Serilog.Events;
using Serilog.Formatting;

namespace ChatMessAPI.Infrastructure.Helpers
{
    public class CustomJsonFormatter : ITextFormatter
    {
        public void Format(LogEvent logEvent, TextWriter output)
        {
            using var stream = new MemoryStream();
            using var writer = new Utf8JsonWriter(stream);

            writer.WriteStartObject();

            // Adciona o campo Mensagem manualmente
            writer.WriteString("Message", logEvent.RenderMessage());

            // Adciona os outros campos, exceto MensageTemplate
            foreach (var property in logEvent.Properties)
            {
                if (property.Key != "MessageTemplate")
                {
                    writer.WritePropertyName(property.Key);
                    writer.WriteStringValue(property.Value.ToString());
                }
            }

            writer.WriteEndObject();
            writer.Flush();

            output.Write(System.Text.Encoding.UTF8.GetString(stream.ToArray()));
        }
    }
}
