using Microsoft.AspNetCore.Razor.TagHelpers;
using SistemaAdocaoAnimais.Models;

namespace SistemaAdocaoAnimais.TagHelpers
{
    [HtmlTargetElement("status-badge")]
    public class StatusBadgeTagHelper : TagHelper
    {
        public StatusAdocao Valor { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var (texto, cor) = Valor switch
            {
                StatusAdocao.Disponivel => ("Disponível", "success"),
                StatusAdocao.EmProcesso => ("Em processo", "warning text-dark"),
                StatusAdocao.Adotado => ("Adotado", "primary"),
                _ => ("Indefinido", "secondary")
            };

            output.TagName = "span";
            output.Attributes.SetAttribute("class", $"badge bg-{cor}");
            output.Content.SetContent(texto);
        }
    }
}