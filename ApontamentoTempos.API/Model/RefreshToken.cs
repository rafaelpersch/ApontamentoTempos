using System;
using System.ComponentModel.DataAnnotations;

namespace ApontamentoTempos.API.Model
{
    public class RefreshToken
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public Guid UsuarioId { get; set; }
        public virtual Usuario Usuario { get; set; }

        public void Validar()
        {
            if (this.Id == Guid.Empty)
            {
                throw new ApplicationException("Id inválido!");
            }

            if (this.UsuarioId == Guid.Empty)
            {
                throw new ApplicationException("Usúario inválido!");
            }
        }
    }
}
