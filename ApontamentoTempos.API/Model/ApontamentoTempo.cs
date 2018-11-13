using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApontamentoTempos.API.Model
{
    public class ApontamentoTempo
    {
        public enum EnAtividade
        {
            Desenvolvimento = 0,
            Email = 1,
            Planejamento = 2,
            LerEscreverDocumento = 3,
            //Reunião
            ComunicacaoFormal = 4,
            //Telefonemas, mensagens
            ComunicacaoInformal = 5,
            //Navegação usada para fins de código
            NavegacaoFormal = 6,
            NavegacaoInformal = 7,
            Outros = 8,
        }

        public ApontamentoTempo()
        {
            this.Atividade = EnAtividade.Desenvolvimento;
        }

        [Key]
        public Guid Id { get; set; }
        [Required]
        public Guid UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
        [Required]
        public Guid ProjetoId { get; set; }
        public Projeto Projeto { get; set; }
        public DateTime Data { get; set; }
        [Required]
        public string Issue { get; set; }
        [Required]
        public decimal Tempo { get; set; }
        [Required]
        public bool Produtivo { get; set; }
        public EnAtividade Atividade { get; set; }
        [Column(TypeName = "text")]
        public string Observacao { get; set; }

        public void Validate()
        {
            if (this.Id == Guid.Empty)
            {
                throw new ApplicationException("Id inválido!");
            }

            if (this.UsuarioId == Guid.Empty)
            {
                throw new ApplicationException("Usuario inválido!");
            }

            if (this.ProjetoId == Guid.Empty)
            {
                throw new ApplicationException("Projeto inválido!");
            }

            if (string.IsNullOrEmpty(this.Issue))
            {
                throw new ApplicationException("Issue inválido!");
            }
            else
            {
                if (string.IsNullOrEmpty(this.Issue.Trim()))
                {
                    throw new ApplicationException("Issue inválido!");
                }
            }

            if (this.Tempo <= 0)
            {
                throw new ApplicationException("Tempo inválido!");
            }

            if (this.Data <= new DateTime(1753, 01, 01))
            {
                throw new ApplicationException("Data inválida!");
            }
        }
    }
}
