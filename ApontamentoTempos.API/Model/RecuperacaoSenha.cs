using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApontamentoTempos.API.Model
{
    public class RecuperacaoSenha
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public Guid UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
        public DateTime Data { get; set; }
        public bool Usado { get; set; }

        public void Validar()
        {
            if (this.Id == Guid.Empty)
            {
                throw new ApplicationException("Id inválido!");
            }

            if (this.UsuarioId == Guid.Empty)
            {
                throw new ApplicationException("Usuário inválido!");
            }
            else
            {
                if (this.Usuario.Id == Guid.Empty)
                {
                    throw new ApplicationException("Usuário inválido!");
                }
            }

            if (this.Data <= new DateTime(1753, 01, 01))
            {
                throw new ApplicationException("Data inválida!");
            }
        }
    }
}
