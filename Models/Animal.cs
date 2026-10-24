
using System;
using System.ComponentModel.DataAnnotations;

namespace SistemaAdocaoAnimais.Models
{
    public class Animal
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome do animal é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome deve ter no máximo 100 caracteres.")]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "Informe a espécie do animal.")]
        public string Especie { get; set; } = string.Empty;

        public string? Raca { get; set; }

        [Range(0, 100, ErrorMessage = "A idade deve estar entre 0 e 100 anos.")]
        public int Idade { get; set; }

        public DateTime DataEntrada { get; set; } = DateTime.Today;

        [Required(ErrorMessage = "Selecione o porte do animal.")]
        public string Porte { get; set; } = string.Empty;

        public StatusAdocao Status { get; set; } = StatusAdocao.Disponivel;
    }

    public enum StatusAdocao
    {
        Disponivel,
        EmProcesso,
        Adotado
    }
}
