using System;
using System.ComponentModel.DataAnnotations;

namespace Api.CatalogoDeJogos.Model.InputModel
{
    public class GameInputModel
    {
        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "O nome do jogo deve conter entre 3 e 100 caracteres")]
        public string Name { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "O nome da produtora deve conter entre 3 e 100 caracteres")]
        public string Production { get; set; }
        [Required]
        [Range(1, 1000, ErrorMessage = "O jogo deve ter o valor entre 1 e 1000 reais")]
        public double Price { get; set; }
    }
}
