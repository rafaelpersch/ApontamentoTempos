using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ApontamentoTempos.API.Model
{
    public class Usuario
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        [Column(TypeName = "varchar(64)")]
        public string Nome { get; set; }
        [Required]
        [Column(TypeName = "varchar(64)")]
        public string Email { get; set; }
        [Required]
        public string Senha { get; set; }

        public void Validar()
        {
            if (this.Id == Guid.Empty)
            {
                throw new ApplicationException("Id inválido!");
            }

            if (string.IsNullOrEmpty(this.Nome))
            {
                throw new ApplicationException("Nome inválido!");
            }
            else
            {
                if (this.Nome.Trim() == string.Empty)
                {
                    throw new ApplicationException("Nome inválido!");
                }
                else
                {
                    if (this.Nome.Length > 64)
                    {
                        throw new ApplicationException("O tamanho máximo do nome é 64 caracteres!");
                    }
                }
            }

            if (string.IsNullOrEmpty(this.Email))
            {
                throw new ApplicationException("Email inválido!");
            }
            else
            {
                if (this.Email.Trim() == string.Empty)
                {
                    throw new ApplicationException("Email inválido!");
                }
                else
                {
                    if (this.Email.Length > 64)
                    {
                        throw new ApplicationException("O tamanho máximo do e-mail é 64 caracteres!");
                    }
                }
            }

            if (string.IsNullOrEmpty(this.Senha))
            {
                throw new ApplicationException("Senha inválida!");
            }
            else
            {
                if (this.Senha.Trim() == string.Empty)
                {
                    throw new ApplicationException("Senha inválida!");
                }
            }
        }
    }
}
