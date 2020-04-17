using System;
using System.Collections.Generic;
using System.Linq;
using SisKiai.DataBase;
using SisKiai.Regras.Dto;
using System.Data.Objects;

namespace SisKiai.Regras
{
    public class Regras
    {
        public List<DtoTelas> GetTelasPorNome(string nome)
        {
            var db = new bancodadosEntities();
            var q = from i in db.TELAS
                    where i.NOME_TELA.Contains(nome.Trim())
                    select new DtoTelas()
                    {
                        IdTelas = i.IDTELAS,
                        Nome = i.NOME_TELA,
                        Endereco = i.ENDERECO_TELAS
                    };
            return q.ToList();
        }

        public void DelLoginAcesso(string idAcesso)
        {
            Dao dados = new Dao();
            int id = int.Parse(idAcesso);
            dados.DelLoginAcesso(id);
        }

        public void DelGraduacaoFiliado(string idGraduacaoFiliado)
        {
            Dao dados = new Dao();
            int id = int.Parse(idGraduacaoFiliado);
            dados.DelGraduacaoFiliado(id);
        }

        public long SetCompeticao(DtoCompeticao item)
        {
            Dao set = new Dao();
            COMPETICAO dados = new COMPETICAO();
            dados.ID_COMPETICAO = item.IdCompeticao;
            dados.DESCRICAO_COMPETICAO = item.DescricaoCompeticao;
            dados.DATA_COMPETICAO = item.DataCompeticao;
            dados.DATA_LIMITE_INSCRICAO = item.DataLimiteInscricao;
            dados.ID_CIDADE = item.IdCidade;
            dados.ENDERECO_COMPETICAO = item.EnderecoCompeticao;
            dados.CEP_COMPETICAO = item.Cep;
            dados.ESPORTE_ID_ESPORTE = item.IdEsporte;
            dados.NOME_COMPETICAO = item.NomeCompeticao;
            dados.RESPONSAVEL = item.NomeResponsavel;
            dados.TELEFONE_CELULAR = item.TelefoneResponsavel;
            dados.VALOR_COMPETICAO = item.ValorCompeticao;
            dados.STATUS_COMPETICAO = item.StatusCompeticao;
            dados.PERMITE_NAO_FILIADO = item.PermiteNaoFiliado;
            dados.VALOR_RANKING_1_COLOCADO = item.Primeiro;
            dados.VALOR_RANKING_2_COLOCADO = item.Segundo;
            dados.VALOR_RANKING_3_COLOCADO = item.Terceiro;
            dados.VALOR_RANKING_4_COLOCADO = item.Quarto;
            dados.VALOR_RANKING_5_COLOCADO = item.Quinto;
            dados.VALOR_RANKING_PARTICIPACAO = (int)item.Participacao;
            dados.PARTICIPA_RANKING = item.ContapRanking;
            dados.IMAGEM = item.imagem;
            set.SetCompeticao(dados);
            return dados.ID_COMPETICAO;

        }

        public long SetFiliado(DtoFiliado dados)
        {
            Dao set = new Dao();
            FILIADO item = new FILIADO();
            item.ID_FILIADO = dados.IdFiliado;
            item.NUMERO_REGISTRO_FILIADO = dados.NumeroRegistro;
            item.NOME_FILIADO = dados.NomeFiliado;
            item.ENDERECO_FILIADO = dados.EnderecoFiliado;
            item.CPF_FILIADO = dados.CpfFiliado;
            item.RG_FILIADO = dados.RgFiliado;
            item.CIDADE_ID_CIDADE = dados.IdCidade;
            item.CEP_CIDADE = dados.CepFiliado;
            item.DATA_NASCIMENTO_FILIADO = dados.DataNascimento;
            item.IDADE_FILIADO = dados.IdadeFiliado;
            item.PESO_FILIADO = dados.Peso;
            item.ALTURA_FILIADO = dados.Altura;
            item.EMAIL_FILIADO = dados.EmailFiliado;
            item.SEXO_FILIADO = dados.SexoFiliado;
            item.TELEFONE_CELULAR_FILIADO = dados.TelefoneCelular;
            item.TELEFONE_FIXO_FILIADO = dados.TelefoneFixo;
            item.ASSOCIACAO_ID_ASSOCIACAO = dados.IdAssociacao;
            item.STATUS_FILIADO = dados.StatusFiliado;
            item.IMAGEM = dados.imagem;
            set.SetFiliado(item);

            return item.ID_FILIADO;
        }

        public void SetEstado(Dto.DtoEstado estado)
        {
            Dao set = new Dao();
            set.SetEstado(estado.IdEstado, estado.NomeEstado, estado.SiglaEstado);
        }

        public List<Dto.DtoEstado> GetAllEstado()
        {
            var db = new bancodadosEntities();
            var q = from i in db.ESTADO
                    select new Dto.DtoEstado()
                    {
                        IdEstado = i.ID_ESTADO,
                        NomeEstado = i.NOME_ESTADO,
                        SiglaEstado = i.SIGLA_ESTADO
                    };
            return q.ToList();
        }

        public void DelEstado(string idEstado)
        {
            Dao dados = new Dao();
            dados.DelEstao(int.Parse(idEstado));
        }

        public Dto.DtoEstado GetEstadoPorId(int idEstado)
        {
            var db = new bancodadosEntities();
            var q = from i in db.ESTADO
                    where i.ID_ESTADO == idEstado
                    select new Dto.DtoEstado()
                    {
                        IdEstado = i.ID_ESTADO,
                        NomeEstado = i.NOME_ESTADO,
                        SiglaEstado = i.SIGLA_ESTADO
                    };
            return q.FirstOrDefault();
        }

        public void SetCidade(Dto.DtoCidade cidade)
        {
            Dao dados = new Dao();
            CIDADE item = new CIDADE();
            item.ID_CIDADE = cidade.IdCidade;
            item.NOME_CIDADE = cidade.NomeCidade;
            item.CEP_CIDADE = cidade.CepCidade;
            item.ID_ESTADO = cidade.IdEstado;

            dados.SetCidade(item);
        }

        public Dto.DtoCidade GetCidadePorId(int idCidade)
        {
            var db = new bancodadosEntities();
            var q = from c in db.CIDADE
                    join e in db.ESTADO
                    on c.ID_ESTADO equals e.ID_ESTADO into estado
                    from e in estado.DefaultIfEmpty()
                    where c.ID_CIDADE == idCidade
                    select new Dto.DtoCidade()
                    {
                        IdCidade = c.ID_CIDADE,
                        NomeCidade = c.NOME_CIDADE,
                        CepCidade = c.CEP_CIDADE,
                        IdEstado = c.ID_ESTADO.Value,
                        SiglaEstado = e.SIGLA_ESTADO
                    };
            return q.FirstOrDefault();
        }

        public void DelCidade(string idCidade)
        {
            Dao dados = new Dao();
            dados.DelCidade(int.Parse(idCidade));
        }

        public List<Dto.DtoCidade> GetAllCidade()
        {
            var db = new bancodadosEntities();
            var q = from c in db.CIDADE
                    join e in db.ESTADO
                    on c.ID_ESTADO equals e.ID_ESTADO into estado
                    from e in estado.DefaultIfEmpty()
                    select new Dto.DtoCidade()
                    {
                        IdCidade = c.ID_CIDADE,
                        NomeCidade = c.NOME_CIDADE,
                        CepCidade = c.CEP_CIDADE,
                        IdEstado = c.ID_ESTADO.Value,
                        SiglaEstado = e.SIGLA_ESTADO
                    };
            return q.ToList();
        }

        public List<Dto.DtoCidade> GetCidadeNome(string nomeCidade)
        {
            var db = new bancodadosEntities();
            var q = from c in db.CIDADE
                    join e in db.ESTADO
                    on c.ID_ESTADO equals e.ID_ESTADO into estado
                    from e in estado.DefaultIfEmpty()
                    where c.NOME_CIDADE.StartsWith(nomeCidade)
                    select new Dto.DtoCidade()
                    {
                        IdCidade = c.ID_CIDADE,
                        NomeCidade = c.NOME_CIDADE,
                        CepCidade = c.CEP_CIDADE,
                        IdEstado = c.ID_ESTADO.Value,
                        SiglaEstado = e.SIGLA_ESTADO
                    };
            return q.ToList();
        }

        public void SetAcademia(Dto.DtoAcademia academia)
        {
            Dao set = new Dao();
            ASSOCIACAO item = new ASSOCIACAO();
            item.ID_ASSOCIACAO = academia.IdAcademia;
            item.NOME_ASSOCIACAO = academia.NomeAcademia;
            item.ENDERECO_ASSOCIACAO = academia.EnderecoAcademia;
            item.NOME_RESPONSAVEL_ASSOCIACAO = academia.ResponsavelAcademia;
            item.TELEFONE_CELULAR_ASSOCIACAO = academia.TelefoneCelularAcademia;
            item.TELEFONE_FIXO_ASSOCIACAO = academia.TelefoneFixoAcademia;
            item.CIDADE_ID_CIDADE = academia.IdCidadeAcademia;
            item.CEP_CIDADE = academia.CepAcademia;
            item.CNPJ_ASSOCIACAO = academia.CnpjAcademia;
            item.INSCRICAO_ASSOCIACAO = academia.InscriAcademia;
            item.EMAIL_ASSOCIACAO = academia.EmailAcademia;
            item.ESPORTE_ID_ESPORTE = academia.IdEsporte;
            item.SIGLA_ASSOCIACAO = academia.Sigla;

            set.SetAcademia(item);
        }

        public List<Dto.DtoAcademia> GetAllAcademia()
        {
            var db = new bancodadosEntities();
            var q = from i in db.ASSOCIACAO
                    select new Dto.DtoAcademia()
                    {
                        IdAcademia = i.ID_ASSOCIACAO,
                        NomeAcademia = i.NOME_ASSOCIACAO,
                        ResponsavelAcademia = i.NOME_RESPONSAVEL_ASSOCIACAO,
                        EnderecoAcademia = i.ENDERECO_ASSOCIACAO,
                        TelefoneCelularAcademia = i.TELEFONE_CELULAR_ASSOCIACAO,
                        TelefoneFixoAcademia = i.TELEFONE_FIXO_ASSOCIACAO,
                        EmailAcademia = i.EMAIL_ASSOCIACAO,
                        CnpjAcademia = i.CNPJ_ASSOCIACAO,
                        InscriAcademia = i.INSCRICAO_ASSOCIACAO,
                    };
            return q.ToList();
        }

        public void DelAcademia(string idAcademia)
        {
            Dao dados = new Dao();
            int id = int.Parse(idAcademia);
            dados.DelAcademia(id);
        }

        public List<Dto.DtoEsportes> GetAllEsportes()
        {
            var db = new bancodadosEntities();
            var q = from i in db.ESPORTE
                    select new Dto.DtoEsportes()
                    {
                        IdEsportes = i.ID_ESPORTE,
                        NomeEsportes = i.NOME_ESPORTE
                    };
            return q.ToList();
        }

        public void DelEsporte(string idEsporte)
        {
            Dao dados = new Dao();
            dados.DelEsporte(idEsporte);
        }

        public Dto.DtoEsportes GetEsportePorId(int id)
        {
            var db = new bancodadosEntities();
            var q = from i in db.ESPORTE
                    where i.ID_ESPORTE == id
                    select new Dto.DtoEsportes()
                    {
                        IdEsportes = i.ID_ESPORTE,
                        NomeEsportes = i.NOME_ESPORTE
                    };
            return q.FirstOrDefault();
        }

        public void SetEsporte(Dto.DtoEsportes esporte)
        {
            Dao dados = new Dao();
            ESPORTE item = new ESPORTE();
            item.ID_ESPORTE = (int)esporte.IdEsportes;
            item.NOME_ESPORTE = esporte.NomeEsportes;
            dados.SetEsporte(item);
        }

        public Dto.DtoAcademia GetAcademiaPorId(int id)
        {
            var db = new bancodadosEntities();

            var q = from i in db.ASSOCIACAO
                    where i.ID_ASSOCIACAO == id
                    select new Dto.DtoAcademia()
                    {
                        IdAcademia = i.ID_ASSOCIACAO,
                        NomeAcademia = i.NOME_ASSOCIACAO,
                        ResponsavelAcademia = i.NOME_RESPONSAVEL_ASSOCIACAO,
                        TelefoneFixoAcademia = i.TELEFONE_FIXO_ASSOCIACAO,
                        TelefoneCelularAcademia = i.TELEFONE_CELULAR_ASSOCIACAO,
                        EmailAcademia = i.EMAIL_ASSOCIACAO,
                        CnpjAcademia = i.CNPJ_ASSOCIACAO,
                        InscriAcademia = i.INSCRICAO_ASSOCIACAO,
                        EnderecoAcademia = i.ENDERECO_ASSOCIACAO,
                        IdCidadeAcademia = i.CIDADE_ID_CIDADE,
                        CepAcademia = i.CEP_CIDADE,
                        IdEsporte = i.ESPORTE_ID_ESPORTE,
                        Sigla = i.SIGLA_ASSOCIACAO
                    };
            return q.FirstOrDefault();
        }

        public void DelFiliado(string idFiliado)
        {
            Dao del = new Dao();

            del.DelFiliado(idFiliado);
        }

        public DtoFiliado GetFiliadoPorId(int id)
        {
            var db = new bancodadosEntities();

            var q = from i in db.FILIADO
                    join a in db.ASSOCIACAO
                    on i.ASSOCIACAO_ID_ASSOCIACAO equals a.ID_ASSOCIACAO into b
                    from a in b.DefaultIfEmpty()
                    join c in db.CIDADE
                    on i.CIDADE_ID_CIDADE equals c.ID_CIDADE
                    join gf in db.GRADUACAO_FILIADO
                    on i.ID_FILIADO equals gf.ID_FILIADO
                    join g in db.GRADUACAO
                    on gf.GRADUACAO_ID_GRADUACAO equals g.ID_GRADUACAO
                    where i.ID_FILIADO == id && gf.STATUS_GRADUACAO_FILIADO == true
                    select new DtoFiliado()
                    {
                        IdFiliado = i.ID_FILIADO,
                        NomeFiliado = i.NOME_FILIADO,
                        EnderecoFiliado = i.ENDERECO_FILIADO,
                        IdadeFiliado = i.IDADE_FILIADO.Value,
                        DataNascimento = i.DATA_NASCIMENTO_FILIADO.Value,
                        EmailFiliado = i.EMAIL_FILIADO,
                        TelefoneCelular = i.TELEFONE_CELULAR_FILIADO,
                        TelefoneFixo = i.TELEFONE_FIXO_FILIADO,
                        RgFiliado = i.RG_FILIADO,
                        CpfFiliado = i.CPF_FILIADO,
                        SexoFiliado = i.SEXO_FILIADO,
                        Peso = i.PESO_FILIADO.Value,
                        Altura = i.ALTURA_FILIADO.Value,
                        IdCidade = i.CIDADE_ID_CIDADE,
                        IdAssociacao = i.ASSOCIACAO_ID_ASSOCIACAO,
                        NomeAssociacao = a.NOME_ASSOCIACAO,
                        NumeroRegistro = i.NUMERO_REGISTRO_FILIADO.Value,
                        StatusFiliado = i.STATUS_FILIADO,
                        NomeCidade = c.NOME_CIDADE,
                        DescGraducacao = g.DESCRICAO_GRADUACAO,
                        IdGraduacao = g.ID_GRADUACAO,
                        imagem = i.IMAGEM
                    };

            ObjectQuery oQuery = (ObjectQuery)q;
            string cmdSQL = oQuery.ToTraceString();

            return q.FirstOrDefault();
        }

        public void DelCategoria(string idCategoria)
        {
            Dao del = new Dao();
            del.DelCategoria(idCategoria);
        }

        public DtoCategoria GetCategoriaPorId(int id)
        {
            var db = new bancodadosEntities();
            var q = from i in db.CATEGORIA
                    where i.ID_CATEGORIA == id
                    select new DtoCategoria()
                    {
                        IdCategoria = i.ID_CATEGORIA,
                        NumeroCategoria = i.NUMERO_CATEGORIA.Value,
                        DescricaoCategoria = i.DESCRICAO_CATEGORIA,
                        SexoCategoria = i.SEXO_CATEGORIA,
                        IdadeInicial = i.IDADE_INICIAL.Value,
                        IdadeFinal = i.IDADE_FINAL.Value,
                        AlturaInicial = i.ALTURA_INICIAL_CATEGORIA.Value,
                        AlturaFinal = i.ALTURA_FINAL_CATEGORIA.Value,
                        PesoInicial = i.PESO_INICIAL_CATEGORIA.Value,
                        PesoFinal = i.PESO_FINAL_CATEGORIA.Value,
                        IdGraduacaoInicial = i.GRADUACAO_ID_GRADUACAO,
                        IdGraduacaoFinal = i.GRADUACAO_FINAL.Value,
                        IdEsporte = i.ESPORTE_ID_ESPORTE,
                        StatusCategoria = i.STATUS_CATEGORIA.Value,
                        TpCompeticao = i.TIPO_COMPETICAO_ID_TIPO_COMPETICAO,
                        TpCategoria = i.TIPO_CATEGORIA
                    };
            return q.FirstOrDefault();
        }

        public List<DtoCategoria> GetAllCategoria()
        {
            var db = new bancodadosEntities();

            var q = from i in db.CATEGORIA
                    join a in db.ESPORTE
                    on i.ESPORTE_ID_ESPORTE equals a.ID_ESPORTE
                    select new DtoCategoria()
                    {
                        IdCategoria = i.ID_CATEGORIA,
                        NumeroCategoria = i.NUMERO_CATEGORIA.Value,
                        DescricaoCategoria = i.DESCRICAO_CATEGORIA,
                        SexoCategoria = i.SEXO_CATEGORIA,
                        IdadeInicial = i.IDADE_INICIAL.Value,
                        IdadeFinal = i.IDADE_FINAL.Value,
                        AlturaInicial = i.ALTURA_INICIAL_CATEGORIA.Value,
                        AlturaFinal = i.ALTURA_FINAL_CATEGORIA.Value,
                        PesoInicial = i.PESO_INICIAL_CATEGORIA.Value,
                        PesoFinal = i.PESO_FINAL_CATEGORIA.Value,
                        IdGraduacaoInicial = i.GRADUACAO_ID_GRADUACAO,
                        IdGraduacaoFinal = i.GRADUACAO_FINAL.Value,
                        IdEsporte = i.ESPORTE_ID_ESPORTE,
                        StatusCategoria = i.STATUS_CATEGORIA.Value,
                        TpCategoria = i.TIPO_CATEGORIA,
                        NmEsporte = a.NOME_ESPORTE,
                    };
            return q.ToList();
        }

        public List<DtoAcademia> GetAcademiaPorNome(string nome)
        {
            var db = new bancodadosEntities();

            var q = from i in db.ASSOCIACAO
                    where i.NOME_ASSOCIACAO.Contains(nome.Trim())
                    select new Dto.DtoAcademia()
                    {
                        IdAcademia = i.ID_ASSOCIACAO,
                        NomeAcademia = i.NOME_ASSOCIACAO,
                        ResponsavelAcademia = i.NOME_RESPONSAVEL_ASSOCIACAO,
                        TelefoneFixoAcademia = i.TELEFONE_FIXO_ASSOCIACAO,
                        TelefoneCelularAcademia = i.TELEFONE_CELULAR_ASSOCIACAO,
                        EmailAcademia = i.EMAIL_ASSOCIACAO,
                        CnpjAcademia = i.CNPJ_ASSOCIACAO,
                        InscriAcademia = i.INSCRICAO_ASSOCIACAO,
                        EnderecoAcademia = i.ENDERECO_ASSOCIACAO,
                        IdCidadeAcademia = i.CIDADE_ID_CIDADE
                    };

            return q.ToList();
        }

        public List<DtoFiliado> GetFiliadoPorNome(string nome)
        {
            var db = new bancodadosEntities();

            var q = from i in db.FILIADO
                    join a in db.ASSOCIACAO on
                    i.ASSOCIACAO_ID_ASSOCIACAO equals a.ID_ASSOCIACAO into b
                    from a in b.DefaultIfEmpty()
                    where i.NOME_FILIADO.Contains(nome.Trim())
                    select new DtoFiliado()
                    {

                        IdFiliado = i.ID_FILIADO,
                        NomeFiliado = i.NOME_FILIADO,
                        EnderecoFiliado = i.ENDERECO_FILIADO,
                        IdadeFiliado = i.IDADE_FILIADO.Value,
                        DataNascimento = i.DATA_NASCIMENTO_FILIADO.Value,
                        EmailFiliado = i.EMAIL_FILIADO,
                        TelefoneCelular = i.TELEFONE_CELULAR_FILIADO,
                        TelefoneFixo = i.TELEFONE_FIXO_FILIADO,
                        RgFiliado = i.RG_FILIADO,
                        CpfFiliado = i.CPF_FILIADO,
                        SexoFiliado = i.SEXO_FILIADO,
                        Peso = i.PESO_FILIADO.Value,
                        Altura = i.ALTURA_FILIADO.Value,
                        IdCidade = i.CIDADE_ID_CIDADE,
                        IdAssociacao = i.ASSOCIACAO_ID_ASSOCIACAO,
                        NomeAssociacao = a.NOME_ASSOCIACAO,
                        NumeroRegistro = i.NUMERO_REGISTRO_FILIADO,
                        StatusFiliado = i.STATUS_FILIADO,
                    };
            return q.ToList();
        }

        public void DelGraduacao(string idGraduacao)
        {
            Dao dados = new Dao();
            dados.DelGraduacao(idGraduacao);
        }

        public DtoGraduacao GetGraduacaoPorId(int id)
        {
            var db = new bancodadosEntities();
            var q = from i in db.GRADUACAO
                    join e in db.ESPORTE
                    on i.ESPORTE_ID_ESPORTE equals e.ID_ESPORTE
                    where i.ID_GRADUACAO == id
                    select new Dto.DtoGraduacao()
                    {
                        IdGraduacao = i.ID_GRADUACAO,
                        DescricaoGraduacao = i.DESCRICAO_GRADUACAO,
                        IdEsporte = i.ESPORTE_ID_ESPORTE,
                        NomeEsporte = e.NOME_ESPORTE
                    };
            return q.FirstOrDefault();
        }

        public List<DtoGraduacao> GetAllGraduacao()
        {
            var db = new bancodadosEntities();
            var q = from i in db.GRADUACAO
                    join e in db.ESPORTE
                    on i.ESPORTE_ID_ESPORTE equals e.ID_ESPORTE
                    select new Dto.DtoGraduacao()
                    {
                        IdGraduacao = i.ID_GRADUACAO,
                        DescricaoGraduacao = i.DESCRICAO_GRADUACAO,
                        IdEsporte = i.ESPORTE_ID_ESPORTE,
                        NomeEsporte = e.NOME_ESPORTE
                    };
            return q.ToList();
        }

        public List<DtoGraduacao> GetGraduacaoPorNome(string descricao)
        {
            var db = new bancodadosEntities();
            var q = from i in db.GRADUACAO
                    join e in db.ESPORTE
                    on i.ESPORTE_ID_ESPORTE equals e.ID_ESPORTE
                    where i.DESCRICAO_GRADUACAO.Contains(descricao)
                    select new Dto.DtoGraduacao()
                    {
                        IdGraduacao = i.ID_GRADUACAO,
                        DescricaoGraduacao = i.DESCRICAO_GRADUACAO,
                        IdEsporte = i.ESPORTE_ID_ESPORTE,
                        NomeEsporte = e.NOME_ESPORTE
                    };
            return q.ToList();
        }

        public void SetGraduacao(DtoGraduacao dados)
        {
            Dao set = new Dao();
            GRADUACAO item = new GRADUACAO();
            item.ID_GRADUACAO = dados.IdGraduacao;
            item.DESCRICAO_GRADUACAO = dados.DescricaoGraduacao;
            item.ESPORTE_ID_ESPORTE = dados.IdEsporte;
            set.SetGraduacao(item);
        }

        public List<Dto.DtoEstado> GetEstadoNome(string nome)
        {
            var db = new bancodadosEntities();
            var q = from i in db.ESTADO
                    where i.NOME_ESTADO.Contains(nome.Trim())
                    select new Dto.DtoEstado()
                    {
                        IdEstado = i.ID_ESTADO,
                        NomeEstado = i.NOME_ESTADO,
                        SiglaEstado = i.SIGLA_ESTADO
                    };
            //ObjectQuery oQuery = (ObjectQuery)q;
            //string cmdSQL = oQuery.ToTraceString();

            return q.ToList();

        }

        public List<Dto.DtoEsportes> GetEsporteNome(string nome)
        {
            var db = new bancodadosEntities();
            var q = from i in db.ESPORTE
                    where i.NOME_ESPORTE.Contains(nome.Trim())
                    select new Dto.DtoEsportes()
                    {
                        IdEsportes = i.ID_ESPORTE,
                        NomeEsportes = i.NOME_ESPORTE
                    };
            return q.ToList();
        }

        public List<DtoAcademia> GetAcademiaNome(string nome)
        {
            var db = new bancodadosEntities();
            var q = from i in db.ASSOCIACAO
                    where i.NOME_ASSOCIACAO.Contains(nome.Trim())
                    select new Dto.DtoAcademia()
                    {
                        IdAcademia = i.ID_ASSOCIACAO,
                        NomeAcademia = i.NOME_ASSOCIACAO,
                        ResponsavelAcademia = i.NOME_RESPONSAVEL_ASSOCIACAO,
                        TelefoneFixoAcademia = i.TELEFONE_FIXO_ASSOCIACAO,
                        TelefoneCelularAcademia = i.TELEFONE_CELULAR_ASSOCIACAO,
                        EmailAcademia = i.EMAIL_ASSOCIACAO,
                        CnpjAcademia = i.CNPJ_ASSOCIACAO,
                        InscriAcademia = i.INSCRICAO_ASSOCIACAO,
                        EnderecoAcademia = i.ENDERECO_ASSOCIACAO,
                        IdCidadeAcademia = i.CIDADE_ID_CIDADE
                    };
            return q.ToList();
        }

        public static bool ValidaCpf(string vrCpf)
        {
            string valor = vrCpf.Replace(".", "");
            valor = valor.Replace("-", "");
            if (valor.Length != 11)
                return false;
            bool igual = true;
            for (int i = 1; i < 11 && igual; i++)
                if (valor[i] != valor[0])
                    igual = false;
            if (igual || valor == "12345678909")
                return false;
            int[] numeros = new int[11];
            for (int i = 0; i < 11; i++)
                numeros[i] = int.Parse(valor[i].ToString());
            int soma = 0;
            for (int i = 0; i < 9; i++)
                soma += (10 - i) * numeros[i];
            int resultado = soma % 11;
            if (resultado == 1 || resultado == 0)
            {
                if (numeros[9] != 0)
                    return false;
            }
            else if (numeros[9] != 11 - resultado)
                return false;
            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += (11 - i) * numeros[i];
            resultado = soma % 11;
            if (resultado == 1 || resultado == 0)
            {
                if (numeros[10] != 0) return false;
            }
            else if (numeros[10] != 11 - resultado)
                return false;
            return true;
        }

        public static bool ValidaCnpj(string vrCnpj)
        {
            string cnpj = vrCnpj.Replace(".", "");
            cnpj = cnpj.Replace("/", "");
            cnpj = cnpj.Replace("-", "");
            int[] digitos, soma, resultado;
            int nrDig;
            string ftmt;
            bool[] cnpjOk;
            ftmt = "6543298765432";
            digitos = new int[14];
            soma = new int[2];
            soma[0] = 0;
            soma[1] = 0;
            resultado = new int[2];
            resultado[0] = 0;
            resultado[1] = 0;
            cnpjOk = new bool[2];
            cnpjOk[0] = false;
            cnpjOk[1] = false;
            try
            {
                for (nrDig = 0; nrDig < 14; nrDig++)
                {
                    digitos[nrDig] = int.Parse(
                        cnpj.Substring(nrDig, 1));
                    if (nrDig <= 11)
                        soma[0] += (digitos[nrDig] *
                          int.Parse(ftmt.Substring(nrDig + 1, 1)));
                    if (nrDig <= 12)
                        soma[1] += (digitos[nrDig] *
                          int.Parse(ftmt.Substring(nrDig, 1)));
                }
                for (nrDig = 0; nrDig < 2; nrDig++)
                {
                    resultado[nrDig] = (soma[nrDig] % 11);
                    if ((resultado[nrDig] == 0) || (resultado[nrDig] == 1))
                        cnpjOk[nrDig] = (digitos[12 + nrDig] == 0);
                    else
                        cnpjOk[nrDig] = (digitos[12 + nrDig] == (11 - resultado[nrDig]));
                }
                return (cnpjOk[0] && cnpjOk[1]);
            }
            catch
            {
                return false;
            }
        }

        public void SetCategoria(DtoCategoria categoria)
        {
            Dao dados = new Dao();
            CATEGORIA item = new CATEGORIA();
            item.ID_CATEGORIA = categoria.IdCategoria;
            item.DESCRICAO_CATEGORIA = categoria.DescricaoCategoria;
            item.ESPORTE_ID_ESPORTE = categoria.IdEsporte;
            item.GRADUACAO_ID_GRADUACAO = categoria.IdGraduacaoInicial;
            item.GRADUACAO_FINAL = categoria.IdGraduacaoFinal;
            item.PESO_INICIAL_CATEGORIA = categoria.PesoInicial;
            item.PESO_FINAL_CATEGORIA = categoria.PesoFinal;
            item.SEXO_CATEGORIA = categoria.SexoCategoria;
            item.TIPO_CATEGORIA = categoria.TpCategoria;
            item.IDADE_INICIAL = categoria.IdadeInicial;
            item.IDADE_FINAL = categoria.IdadeFinal;
            item.STATUS_CATEGORIA = categoria.StatusCategoria;
            item.ALTURA_INICIAL_CATEGORIA = categoria.AlturaInicial;
            item.ALTURA_FINAL_CATEGORIA = categoria.AlturaFinal;
            item.TIPO_COMPETICAO_ID_TIPO_COMPETICAO = categoria.TpCompeticao.Value;
            item.NUMERO_CATEGORIA = categoria.NumeroCategoria;
            dados.SetCategoria(item);
        }

        public List<DtoGraduacao> GetAllGraduacaoPorEsporte(string idEsporte)
        {
            var db = new bancodadosEntities();
            int id = int.Parse(idEsporte);

            var q = from i in db.GRADUACAO
                    where i.ESPORTE_ID_ESPORTE == id
                    select new Dto.DtoGraduacao()
                    {
                        IdGraduacao = i.ID_GRADUACAO,
                        DescricaoGraduacao = i.DESCRICAO_GRADUACAO,
                        IdEsporte = i.ESPORTE_ID_ESPORTE,
                        NomeEsporte = i.ESPORTE.NOME_ESPORTE
                    };
            return q.ToList();
        }

        public List<DtoCategoria> GetCategoriaPorNome(string nome)
        {
            var db = new bancodadosEntities();
            var q = from i in db.CATEGORIA
                    where i.DESCRICAO_CATEGORIA.Contains(nome.Trim())
                    select new DtoCategoria()
                    {
                        IdCategoria = i.ID_CATEGORIA,
                        DescricaoCategoria = i.DESCRICAO_CATEGORIA,
                        SexoCategoria = i.SEXO_CATEGORIA,
                        IdadeInicial = i.IDADE_INICIAL.Value,
                        IdadeFinal = i.IDADE_FINAL.Value,
                        AlturaInicial = i.ALTURA_INICIAL_CATEGORIA.Value,
                        AlturaFinal = i.ALTURA_FINAL_CATEGORIA.Value,
                        PesoInicial = i.PESO_INICIAL_CATEGORIA.Value,
                        PesoFinal = i.PESO_FINAL_CATEGORIA.Value,
                        IdGraduacaoInicial = i.GRADUACAO_ID_GRADUACAO,
                        IdGraduacaoFinal = i.GRADUACAO_FINAL.Value,
                        IdEsporte = i.ESPORTE_ID_ESPORTE,
                        StatusCategoria = i.STATUS_CATEGORIA.Value,
                        TpCategoria = i.TIPO_CATEGORIA,
                        NumeroCategoria = i.NUMERO_CATEGORIA.Value,
                    };
            return q.ToList();
        }

        public List<DtoTelas> GetAllTelas()
        {
            var db = new bancodadosEntities();
            var q = from i in db.TELAS
                    select new DtoTelas()
                    {
                        IdTelas = i.IDTELAS,
                        Nome = i.NOME_TELA,
                        Endereco = i.ENDERECO_TELAS
                    };
            return q.ToList();
        }

        public List<DtoFiliado> GetAllFiliado()
        {
            var db = new bancodadosEntities();
            var q = (from i in db.FILIADO
                     join a in db.ASSOCIACAO on
                     i.ASSOCIACAO_ID_ASSOCIACAO equals a.ID_ASSOCIACAO into b
                     from a in b.DefaultIfEmpty()
                     select new DtoFiliado()
                     {
                         IdFiliado = i.ID_FILIADO,
                         NomeFiliado = i.NOME_FILIADO,
                         EnderecoFiliado = i.ENDERECO_FILIADO,
                         IdadeFiliado = i.IDADE_FILIADO.Value,
                         DataNascimento = i.DATA_NASCIMENTO_FILIADO.Value,
                         EmailFiliado = i.EMAIL_FILIADO,
                         TelefoneCelular = i.TELEFONE_CELULAR_FILIADO,
                         TelefoneFixo = i.TELEFONE_FIXO_FILIADO,
                         RgFiliado = i.RG_FILIADO,
                         CpfFiliado = i.CPF_FILIADO,
                         SexoFiliado = i.SEXO_FILIADO,
                         Peso = i.PESO_FILIADO.Value,
                         Altura = i.ALTURA_FILIADO.Value,
                         IdCidade = i.CIDADE_ID_CIDADE,
                         IdAssociacao = i.ASSOCIACAO_ID_ASSOCIACAO,
                         NomeAssociacao = a.NOME_ASSOCIACAO,
                         NumeroRegistro = i.NUMERO_REGISTRO_FILIADO,
                         StatusFiliado = i.STATUS_FILIADO,
                         imagem = i.IMAGEM,
                     }).OrderBy(p => p.IdFiliado);

            return q.ToList();
        }

        public DtoGraduacaoFiliado GetGraduacaoFiliadoPorIdGraduacaoFiliado(int id)
        {
            var db = new bancodadosEntities();
            var q = from i in db.GRADUACAO_FILIADO
                    where i.ID_GRADUACAO_FILIADO == id
                    select new DtoGraduacaoFiliado()
                    {
                        IdGraduacaoFiliado = i.ID_GRADUACAO_FILIADO,
                        IdFiliado = i.ID_FILIADO,
                        DataGraduacao = i.DATA_GRADUACAO.Value,
                        Status = i.STATUS_GRADUACAO_FILIADO,
                        //IdGraduacao = i.GRADUACAO.ID_GRADUACAO,
                        //Descricao = i.GRADUACAO.DESCRICAO_GRADUACAO,

                    };
            return q.FirstOrDefault();
        }

        public long SetGraduacaoFiliado(DtoGraduacaoFiliado graduacaofiliado)
        {
            Dao dados = new Dao();
            GRADUACAO_FILIADO item = new GRADUACAO_FILIADO();

            item.ID_GRADUACAO_FILIADO = graduacaofiliado.IdGraduacaoFiliado;
            item.DATA_GRADUACAO = graduacaofiliado.DataGraduacao;
            item.GRADUACAO_ID_GRADUACAO = graduacaofiliado.IdGraduacao;
            item.STATUS_GRADUACAO_FILIADO = graduacaofiliado.Status;
            item.ID_FILIADO = graduacaofiliado.IdFiliado;
            if (item.STATUS_GRADUACAO_FILIADO == true)
            {
                AtualizarGraduacoes(item);
            }
            long id = dados.SetGraduacaoFiliado(item);
            return id;
        }

        private void AtualizarGraduacoes(GRADUACAO_FILIADO item)
        {
            List<DtoGraduacaoFiliado> list = GetGraduacaoPorIdFiliado(int.Parse(item.ID_FILIADO.ToString()));
            foreach (var l in list)
            {
                Dao set = new Dao();
                set.AlterStatusGraduacao(l.IdGraduacaoFiliado);
            }
        }

        public List<DtoGraduacaoFiliado> GetGraduacaoPorIdFiliado(int idFiliado)
        {
            var db = new bancodadosEntities();

            var q = from i in db.GRADUACAO_FILIADO
                    join g in db.GRADUACAO
                    on i.GRADUACAO_ID_GRADUACAO equals g.ID_GRADUACAO
                    where i.ID_FILIADO == idFiliado
                    select new Dto.DtoGraduacaoFiliado()
                    {
                        IdGraduacaoFiliado = i.ID_GRADUACAO_FILIADO,
                        IdFiliado = i.ID_FILIADO,
                        DataGraduacao = i.DATA_GRADUACAO.Value,
                        Status = i.STATUS_GRADUACAO_FILIADO.Value,
                        Descricao = g.DESCRICAO_GRADUACAO,
                        IdGraduacao = g.ID_GRADUACAO,
                        StatusAtivo = i.STATUS_GRADUACAO_FILIADO.Value == true ? "Ativo" : "Inativo",
                    };
            return q.ToList();
        }

        public long SetAcesso(DtoAcesso dados)
        {
            LOGIN item = new LOGIN();
            item.ID_LOGIN = dados.IdAcesso;
            item.NOME_LOGIN = dados.Nome;
            item.USUARIO_LOGIN = dados.Login;
            item.SENHA_LOGIN = dados.Senha;
            item.ADMIN_LOGIN = dados.Administrador;
            item.STATUS_LOGIN = dados.StatusLogin;
            item.ASSOCIACAO_ID_ASSOCIACAO = dados.IdAssociacao;
            Dao set = new Dao();
            set.SetAcessoLogin(item);
            return item.ID_LOGIN;
        }

        public List<DtoAcesso> GetAllLogin()
        {
            var db = new bancodadosEntities();
            var q = from i in db.LOGIN
                    select new Dto.DtoAcesso()
                    {
                        IdAcesso = i.ID_LOGIN,
                        Nome = i.NOME_LOGIN,
                        Login = i.USUARIO_LOGIN,
                        Senha = i.SENHA_LOGIN,
                        StatusLogin = i.STATUS_LOGIN.Value,
                        Administrador = i.ADMIN_LOGIN.Value
                    };
            return q.ToList();
        }

        public DtoAcesso GetLoginPorId(int id)
        {
            var db = new bancodadosEntities();

            var q = from i in db.LOGIN
                    where i.ID_LOGIN == id
                    select new Dto.DtoAcesso()
                    {
                        IdAcesso = i.ID_LOGIN,
                        Nome = i.NOME_LOGIN,
                        Login = i.USUARIO_LOGIN,
                        Senha = i.SENHA_LOGIN,
                        StatusLogin = i.STATUS_LOGIN.Value,
                        Administrador = i.ADMIN_LOGIN.Value,
                        IdAssociacao = i.ASSOCIACAO_ID_ASSOCIACAO.Value
                    };
            return q.FirstOrDefault();
        }

        public DtoAcesso ConfereUsuarioSenha(DtoAcesso login)
        {
            if (login.Login.Equals("Master") && login.Senha.Equals("1q2w3e4r@"))
            {
                DtoAcesso dados = new DtoAcesso();
                dados.IdAcesso = 0;
                dados.Nome = "Administrador";
                dados.Administrador = true;
                return dados;
            }
            else
            {
                var db = new bancodadosEntities();
                var q = from i in db.LOGIN
                        where i.USUARIO_LOGIN == login.Login
                        && i.SENHA_LOGIN == login.Senha
                        && i.STATUS_LOGIN == true
                        select new DtoAcesso()
                        {
                            IdAcesso = i.ID_LOGIN,
                            Nome = i.NOME_LOGIN,
                            Administrador = i.ADMIN_LOGIN.Value,
                            IdAssociacao = i.ASSOCIACAO_ID_ASSOCIACAO,
                        };

                return q.FirstOrDefault();
            }
        }

        public void SetTelas(DtoTelas item)
        {
            TELAS dados = new TELAS();
            dados.IDTELAS = int.Parse(item.IdTelas.ToString());
            dados.NOME_TELA = item.Nome;
            dados.ENDERECO_TELAS = item.Endereco;
            Dao set = new Dao();
            set.SetTelas(dados);
        }

        public DtoTelas GetTelasPorId(int id)
        {
            var db = new bancodadosEntities();
            var q = from i in db.TELAS
                    where i.IDTELAS == id
                    select new DtoTelas()
                    {
                        IdTelas = i.IDTELAS,
                        Nome = i.NOME_TELA,
                        Endereco = i.ENDERECO_TELAS
                    };
            return q.FirstOrDefault();
        }

        public void DelTelas(string idTelas)
        {
            Dao dados = new Dao();
            int id = int.Parse(idTelas);
            dados.DelTelas(id);
        }

        public List<DtoAcesso> GetLoginPorNome(string nome)
        {
            var db = new bancodadosEntities();
            var q = from i in db.LOGIN
                    where i.NOME_LOGIN.Contains(nome.Trim())
                    select new Dto.DtoAcesso()
                    {
                        IdAcesso = i.ID_LOGIN,
                        Nome = i.NOME_LOGIN,
                        Login = i.USUARIO_LOGIN,
                        Senha = i.SENHA_LOGIN,
                        StatusLogin = i.STATUS_LOGIN.Value,
                        Administrador = i.ADMIN_LOGIN.Value
                    };
            return q.ToList();
        }

        public void SetLoginTelas(DtoLoginTela dados)
        {
            var db = new bancodadosEntities();

            var q = from i in db.LOGIN_TELA
                    where i.LOGIN_ID_LOGIN == dados.IdLogin
                    && i.TELAS_IDTELAS == dados.IdTela
                    select i;
            if (q.Count() == 0)
            {
                LOGIN_TELA item = new LOGIN_TELA();
                item.LOGIN_ID_LOGIN = dados.IdLogin;
                item.TELAS_IDTELAS = int.Parse(dados.IdTela.ToString());

                Dao set = new Dao();
                set.SetLoginTela(item);
            }
        }

        public List<DtoLoginTela> GetTelasLiberadasPorIdLogin(long idAcesso)
        {
            var db = new bancodadosEntities();

            var q = (from i in db.LOGIN_TELA
                     join t in db.TELAS
                     on i.TELAS_IDTELAS equals t.IDTELAS
                     where i.LOGIN_ID_LOGIN == idAcesso
                     select new Dto.DtoLoginTela()
                     {
                         IdLogin = i.LOGIN_ID_LOGIN,
                         IdLoginTela = i.IDLOGINTELA,
                         IdTela = i.TELAS_IDTELAS,
                         NomeTela = t.NOME_TELA,
                         EnderecoTela = t.ENDERECO_TELAS
                     }).Distinct().OrderBy(p => p.EnderecoTela);
            return q.ToList();
        }

        public void DelLoginTelas(string idTela, string idLogin)
        {
            long idTela1 = int.Parse(idTela);
            long idLogin1 = int.Parse(idLogin);
            Dao del = new Dao();
            del.DelLonginTelas(idTela1, idLogin1);
        }

        public List<DtoCategoria> GetAllCategoriaPorIdEsporte(int id)
        {
            var db = new bancodadosEntities();
            var q = from i in db.CATEGORIA
                    where i.ESPORTE_ID_ESPORTE == id
                    select new DtoCategoria()
                    {
                        IdCategoria = i.ID_CATEGORIA,
                        DescricaoCategoria = i.DESCRICAO_CATEGORIA,
                        SexoCategoria = i.SEXO_CATEGORIA,
                        IdadeInicial = i.IDADE_INICIAL.Value,
                        IdadeFinal = i.IDADE_FINAL.Value,
                        AlturaInicial = i.ALTURA_INICIAL_CATEGORIA.Value,
                        AlturaFinal = i.ALTURA_FINAL_CATEGORIA.Value,
                        PesoInicial = i.PESO_INICIAL_CATEGORIA.Value,
                        PesoFinal = i.PESO_FINAL_CATEGORIA.Value,
                        IdGraduacaoInicial = i.GRADUACAO_ID_GRADUACAO,
                        IdGraduacaoFinal = i.GRADUACAO_FINAL.Value,
                        IdEsporte = i.ESPORTE_ID_ESPORTE,
                        StatusCategoria = i.STATUS_CATEGORIA.Value,
                    };
            return q.ToList();
        }

        public void SetCategoriaCompeticao(DtoCategoriasCompeticao item)
        {
            CATEGORIA_COMPETICAO cate = new CATEGORIA_COMPETICAO();
            cate.ID_CATEGORIA = item.IdCategoria;
            cate.ID_COMPETICAO = item.IdCompeticao;
            cate.ESPORTE_ID_ESPORTE = item.IdEsporte;// COMPETICAO_ESPORTE_ID_ESPORTE = item.IdEsporte;
            cate.CATEGORIA_FINALIZADA = item.Categoria_Finalizada;
            Dao set = new Dao();
            set.SetCategoriaCompeticao(cate);
        }

        public List<DtoCompeticao> GetAllCompeticao()
        {
            var db = new bancodadosEntities();
            var q = from i in db.COMPETICAO
                    select new DtoCompeticao()
                    {
                        IdCompeticao = i.ID_COMPETICAO,
                        IdEsporte = i.ESPORTE_ID_ESPORTE,
                        NomeCompeticao = i.NOME_COMPETICAO,
                        NomeResponsavel = i.RESPONSAVEL,
                        DescricaoCompeticao = i.DESCRICAO_COMPETICAO,
                        TelefoneResponsavel = i.TELEFONE_CELULAR,
                        EnderecoCompeticao = i.ENDERECO_COMPETICAO,
                        DataCompeticao = i.DATA_COMPETICAO.Value,
                        DataLimiteInscricao = i.DATA_LIMITE_INSCRICAO.Value,
                        StatusCompeticao = i.STATUS_COMPETICAO.Value,
                        IdCidade = i.ID_CIDADE.Value,
                        Cep = i.CEP_COMPETICAO,
                        ValorCompeticao = i.VALOR_COMPETICAO.Value,
                        imagem = i.IMAGEM,
                    };
            return q.ToList();
        }

        public List<DtoCompeticao> GetAllCompeticaoAtiva()
        {
            var db = new bancodadosEntities();
            DateTime data = DateTime.Now.AddDays(-1);
            var q = from i in db.COMPETICAO
                    join c in db.CIDADE on i.ID_CIDADE equals c.ID_CIDADE
                    where i.STATUS_COMPETICAO == true && i.DATA_LIMITE_INSCRICAO >= data
                    select new DtoCompeticao()
                    {
                        IdCompeticao = i.ID_COMPETICAO,
                        IdEsporte = i.ESPORTE_ID_ESPORTE,
                        NomeCompeticao = i.NOME_COMPETICAO,
                        NomeResponsavel = i.RESPONSAVEL,
                        DescricaoCompeticao = i.DESCRICAO_COMPETICAO,
                        TelefoneResponsavel = i.TELEFONE_CELULAR,
                        EnderecoCompeticao = i.ENDERECO_COMPETICAO,
                        DataCompeticao = i.DATA_COMPETICAO.Value,
                        DataLimiteInscricao = i.DATA_LIMITE_INSCRICAO.Value,
                        StatusCompeticao = i.STATUS_COMPETICAO.Value,
                        IdCidade = i.ID_CIDADE.Value,
                        NomeCidade = c.NOME_CIDADE,
                        Cep = i.CEP_COMPETICAO,
                        ValorCompeticao = i.VALOR_COMPETICAO.Value,
                        PermiteNaoFiliado = i.PERMITE_NAO_FILIADO,
                        PermiteNaoFiliadoDescricao = i.PERMITE_NAO_FILIADO == true ? "Permite inscrição de Atletas não filiados" : "Não permite inscrição de Atletas não filiados",
                        imagem = i.IMAGEM
                    };
            return q.ToList();
        }

        public List<DtoCompeticao> GetAllCompeticaoAtiva10Dias()
        {
            DateTime data = DateTime.Now.AddDays(-10);
            var db = new bancodadosEntities();
            var q = from i in db.COMPETICAO
                    where i.STATUS_COMPETICAO == true
                    select new DtoCompeticao()
                    {
                        IdCompeticao = i.ID_COMPETICAO,
                        IdEsporte = i.ESPORTE_ID_ESPORTE,
                        NomeCompeticao = i.NOME_COMPETICAO,
                        NomeResponsavel = i.RESPONSAVEL,
                        DescricaoCompeticao = i.DESCRICAO_COMPETICAO,
                        TelefoneResponsavel = i.TELEFONE_CELULAR,
                        EnderecoCompeticao = i.ENDERECO_COMPETICAO,
                        DataCompeticao = i.DATA_COMPETICAO.Value,
                        DataLimiteInscricao = i.DATA_LIMITE_INSCRICAO.Value,
                        StatusCompeticao = i.STATUS_COMPETICAO.Value,
                        IdCidade = i.ID_CIDADE.Value,
                        Cep = i.CEP_COMPETICAO,
                        ValorCompeticao = i.VALOR_COMPETICAO.Value,
                        PermiteNaoFiliado = i.PERMITE_NAO_FILIADO,
                        PermiteNaoFiliadoDescricao = i.PERMITE_NAO_FILIADO == true ? "Permite inscrição de Atletas não filiados" : "Não permite inscrição de Atletas não filiados",
                        imagem = i.IMAGEM
                    };
            return q.ToList();
        }

        public void DelCompeticao(string idCompeticao)
        {
            long idcompeticao = int.Parse(idCompeticao);

            Dao del = new Dao();
            del.DelCompeticao(idcompeticao);
        }

        public DtoCompeticao GetCompeticaoPorId(int id)
        {
            var db = new bancodadosEntities();
            var q = from i in db.COMPETICAO
                    join c in db.CIDADE
                    on i.ID_CIDADE equals c.ID_CIDADE
                    where i.ID_COMPETICAO == id
                    select new DtoCompeticao()
                    {
                        IdCompeticao = i.ID_COMPETICAO,
                        IdEsporte = i.ESPORTE_ID_ESPORTE,
                        NomeCompeticao = i.NOME_COMPETICAO,
                        NomeResponsavel = i.RESPONSAVEL,
                        DescricaoCompeticao = i.DESCRICAO_COMPETICAO,
                        TelefoneResponsavel = i.TELEFONE_CELULAR,
                        EnderecoCompeticao = i.ENDERECO_COMPETICAO,
                        DataCompeticao = i.DATA_COMPETICAO.Value,
                        DataLimiteInscricao = i.DATA_LIMITE_INSCRICAO.Value,
                        StatusCompeticao = i.STATUS_COMPETICAO.Value,
                        IdCidade = i.ID_CIDADE.Value,
                        Cep = i.CEP_COMPETICAO,
                        ValorCompeticao = i.VALOR_COMPETICAO.Value,
                        NomeCidade = c.NOME_CIDADE,
                        PermiteNaoFiliado = i.PERMITE_NAO_FILIADO,
                        PermiteNaoFiliadoDescricao = i.PERMITE_NAO_FILIADO == true ? "Permite inscrição de Atletas não filiados" : "Não permite inscrição de Atletas não filiados",
                        Primeiro = i.VALOR_RANKING_1_COLOCADO.Value,
                        Segundo = i.VALOR_RANKING_2_COLOCADO.Value,
                        Terceiro = i.VALOR_RANKING_3_COLOCADO.Value,
                        Quarto = i.VALOR_RANKING_4_COLOCADO.Value,
                        Quinto = i.VALOR_RANKING_5_COLOCADO.Value,
                        imagem = i.IMAGEM,
                        Participacao = i.VALOR_RANKING_PARTICIPACAO.Value,
                        ContapRanking = i.PARTICIPA_RANKING
                    };
            return q.FirstOrDefault();
        }

        public List<DtoCompeticao> GetCompeticaoPorNome(string nome)
        {
            var db = new bancodadosEntities();
            var q = from i in db.COMPETICAO
                    where i.NOME_COMPETICAO.Contains(nome.Trim()) && i.STATUS_COMPETICAO == true
                    select new DtoCompeticao()
                    {
                        IdCompeticao = i.ID_COMPETICAO,
                        IdEsporte = i.ESPORTE_ID_ESPORTE,
                        NomeCompeticao = i.NOME_COMPETICAO,
                        NomeResponsavel = i.RESPONSAVEL,
                        DescricaoCompeticao = i.DESCRICAO_COMPETICAO,
                        TelefoneResponsavel = i.TELEFONE_CELULAR,
                        EnderecoCompeticao = i.ENDERECO_COMPETICAO,
                        DataCompeticao = i.DATA_COMPETICAO.Value,
                        DataLimiteInscricao = i.DATA_LIMITE_INSCRICAO.Value,
                        StatusCompeticao = i.STATUS_COMPETICAO.Value,
                        IdCidade = i.ID_CIDADE.Value,
                        Cep = i.CEP_COMPETICAO,
                        ValorCompeticao = i.VALOR_COMPETICAO.Value,
                        PermiteNaoFiliadoDescricao = i.PERMITE_NAO_FILIADO == true ? "Permite inscrição de Atletas não filiados" : "Não permite inscrição de Atletas não filiados",
                    };
            return q.ToList();
        }

        public int CalculaIdade(DateTime dtNascimento)
        {

            //TimeSpan com a data atual menos data do niversário
            TimeSpan ts = DateTime.Today - dtNascimento;
            //Converter TimeSpan para DateTime
            //Como o new DateTime() retorna 01/01/0001 00:00:00
            //vou ter que remover um ano .AddYears(- 1) e um dia .AddDays(-1) para ter a data exata.
            DateTime idade = (new DateTime() + ts).AddYears(-1).AddDays(-1);

            //Idade em anos
            return idade.Year;
        }

        public void CalculaIdadeFiliados()
        {
            var db = new bancodadosEntities();

            var q = from filiados in db.FILIADO
                    select new DtoFiliado()
                    {
                        IdFiliado = filiados.ID_FILIADO,
                        NomeFiliado = filiados.NOME_FILIADO,
                        DataNascimento = filiados.DATA_NASCIMENTO_FILIADO.Value,
                        IdadeFiliado = filiados.IDADE_FILIADO.Value
                    };
            Dao set = new Dao();
            foreach (var i in q)
            {
                int idade = 0;
                try
                {
                    idade = CalculaIdade(i.DataNascimento);
                }
                catch (Exception ex)
                {
                    throw new Exception(i.NomeFiliado + " - " + ex.Message);
                }
                if (idade != i.IdadeFiliado)
                {
                    FILIADO alterar = new FILIADO();
                    alterar.ID_FILIADO = i.IdFiliado;
                    alterar.NOME_FILIADO = i.NomeFiliado;
                    alterar.DATA_NASCIMENTO_FILIADO = i.DataNascimento;
                    alterar.IDADE_FILIADO = idade;
                    set.SetIdadeFiliado(alterar);
                }
            }
        }

        public List<DtoLoginTela> GetAllTelasLogin()
        {
            var db = new bancodadosEntities();

            var q = (from i in db.TELAS
                     select new Dto.DtoLoginTela()
                     {
                         NomeTela = i.NOME_TELA,
                         EnderecoTela = i.ENDERECO_TELAS
                     }).Distinct().OrderBy(p => p.EnderecoTela);
            return q.ToList();
        }

        public List<DtoFiliado> GetAllFiliadoPorIdAssociacao(int idAssociacao)
        {
            var db = new bancodadosEntities();
            var q = from i in db.FILIADO
                    join a in db.ASSOCIACAO on
                    i.ASSOCIACAO_ID_ASSOCIACAO equals a.ID_ASSOCIACAO into b
                    from a in b.DefaultIfEmpty()
                    where a.ID_ASSOCIACAO == idAssociacao
                    select new DtoFiliado()
                    {
                        IdFiliado = i.ID_FILIADO,
                        NomeFiliado = i.NOME_FILIADO,
                        EnderecoFiliado = i.ENDERECO_FILIADO,
                        IdadeFiliado = i.IDADE_FILIADO.Value,
                        DataNascimento = i.DATA_NASCIMENTO_FILIADO.Value,
                        EmailFiliado = i.EMAIL_FILIADO,
                        TelefoneCelular = i.TELEFONE_CELULAR_FILIADO,
                        TelefoneFixo = i.TELEFONE_FIXO_FILIADO,
                        RgFiliado = i.RG_FILIADO,
                        CpfFiliado = i.CPF_FILIADO,
                        SexoFiliado = i.SEXO_FILIADO,
                        Peso = i.PESO_FILIADO.Value,
                        Altura = i.ALTURA_FILIADO.Value,
                        IdCidade = i.CIDADE_ID_CIDADE,
                        IdAssociacao = i.ASSOCIACAO_ID_ASSOCIACAO,
                        NomeAssociacao = a.NOME_ASSOCIACAO,
                        StatusFiliado = i.STATUS_FILIADO,
                        NumeroRegistro = i.NUMERO_REGISTRO_FILIADO,
                        imagem = i.IMAGEM,
                    };

            return q.ToList();
        }

        public List<DtoFiliado> GetAllFiliadoPorIdAssociacao(int idAssociacao, string statusFiliado)
        {
            var db = new bancodadosEntities();
            var q = from i in db.FILIADO
                    join a in db.ASSOCIACAO on
                    i.ASSOCIACAO_ID_ASSOCIACAO equals a.ID_ASSOCIACAO into b
                    from a in b.DefaultIfEmpty()
                    where a.ID_ASSOCIACAO == idAssociacao && i.STATUS_FILIADO == statusFiliado
                    select new DtoFiliado()
                    {
                        IdFiliado = i.ID_FILIADO,
                        NomeFiliado = i.NOME_FILIADO,
                        NumeroRegistro = i.NUMERO_REGISTRO_FILIADO.Value,
                        EnderecoFiliado = i.ENDERECO_FILIADO,
                        IdadeFiliado = i.IDADE_FILIADO.Value,
                        DataNascimento = i.DATA_NASCIMENTO_FILIADO.Value,
                        EmailFiliado = i.EMAIL_FILIADO,
                        TelefoneCelular = i.TELEFONE_CELULAR_FILIADO,
                        TelefoneFixo = i.TELEFONE_FIXO_FILIADO,
                        RgFiliado = i.RG_FILIADO,
                        CpfFiliado = i.CPF_FILIADO,
                        SexoFiliado = i.SEXO_FILIADO,
                        Peso = i.PESO_FILIADO.Value,
                        Altura = i.ALTURA_FILIADO.Value,
                        IdCidade = i.CIDADE_ID_CIDADE,
                        IdAssociacao = i.ASSOCIACAO_ID_ASSOCIACAO,
                        NomeAssociacao = a.NOME_ASSOCIACAO
                    };

            return q.ToList();
        }

        public List<DtoTipoCompeticao> GetAllTipoCompeticao()
        {
            var db = new bancodadosEntities();
            var q = from i in db.TIPO_COMPETICAO
                    join e in db.ESPORTE
                    on i.ESPORTE_ID_ESPORTE equals e.ID_ESPORTE
                    select new DtoTipoCompeticao()
                    {
                        IdTipoCompeticao = i.ID_TIPO_COMPETICAO,
                        DescricaoCompeticao = i.DESCRICAO_COMPETICAO,
                        IdEsporte = i.ESPORTE_ID_ESPORTE,
                        NomeEsporte = e.NOME_ESPORTE
                    };
            return q.ToList();
        }

        public void SetTipoCompeticao(DtoTipoCompeticao tipoCompeticao)
        {
            TIPO_COMPETICAO dados = new TIPO_COMPETICAO();
            dados.ID_TIPO_COMPETICAO = tipoCompeticao.IdTipoCompeticao;
            dados.DESCRICAO_COMPETICAO = tipoCompeticao.DescricaoCompeticao;
            dados.ESPORTE_ID_ESPORTE = tipoCompeticao.IdEsporte;
            Dao set = new Dao();
            set.SetTipoCompeticao(dados);
        }

        public void DelTipoCompeticao(string idTipoCompeticao)
        {
            Dao del = new Dao();
            del.DelTipoCompeticao(idTipoCompeticao);
        }

        public DtoTipoCompeticao GetTipoCompeticaoPorId(int id)
        {
            var db = new bancodadosEntities();
            var q = from i in db.TIPO_COMPETICAO
                    where i.ID_TIPO_COMPETICAO == id
                    select new DtoTipoCompeticao()
                    {
                        IdTipoCompeticao = i.ID_TIPO_COMPETICAO,
                        DescricaoCompeticao = i.DESCRICAO_COMPETICAO,
                        IdEsporte = i.ESPORTE_ID_ESPORTE
                    };
            return q.FirstOrDefault();
        }

        public List<DtoTipoCompeticao> GetTipoCompeticaoPorNome(string descricao)
        {
            var db = new bancodadosEntities();
            var q = from i in db.TIPO_COMPETICAO
                    where i.DESCRICAO_COMPETICAO == descricao
                    select new DtoTipoCompeticao()
                    {
                        IdTipoCompeticao = i.ID_TIPO_COMPETICAO,
                        DescricaoCompeticao = i.DESCRICAO_COMPETICAO,
                        IdEsporte = i.ESPORTE_ID_ESPORTE
                    };
            return q.ToList();
        }

        public List<DtoTipoCompeticao> GetAllTpCompeticaoPorEsporte(string idEsporte)
        {
            long idesporte = long.Parse(idEsporte);
            var db = new bancodadosEntities();
            var q = from i in db.TIPO_COMPETICAO
                    where i.ESPORTE_ID_ESPORTE == idesporte
                    select new DtoTipoCompeticao()
                    {
                        IdTipoCompeticao = i.ID_TIPO_COMPETICAO,
                        DescricaoCompeticao = i.DESCRICAO_COMPETICAO,
                        IdEsporte = i.ESPORTE_ID_ESPORTE
                    };
            return q.ToList();
        }

        public List<DtoCategoriasCompeticao> GetCompeticaoCategorias(string idCompeticao)
        {
            long idcompeticao = long.Parse(idCompeticao);
            var db = new bancodadosEntities();

            var q = from i in db.CATEGORIA_COMPETICAO
                    join c in db.CATEGORIA
                    on i.ID_CATEGORIA equals c.ID_CATEGORIA
                    where i.ID_COMPETICAO == idcompeticao
                    select new DtoCategoriasCompeticao()
                    {
                        IdCategoria = i.ID_CATEGORIA.Value,
                        IdCategoriaCompeticao = i.ID_CATEGORIA_COMPETICAO,
                        IdCompeticao = i.ID_COMPETICAO.Value,
                        IdEsporte = i.ESPORTE_ID_ESPORTE,// COMPETICAO_ESPORTE_ID_ESPORTE,
                        NomeCategoria = c.DESCRICAO_CATEGORIA
                    };
            return q.ToList();
        }

        public void DelCategoriaComepticao(string idCategoria, string idCompeticao)
        {
            long idcategoria = int.Parse(idCategoria);
            long idcompeticao = int.Parse(idCompeticao);

            Dao del = new Dao();
            del.DelCategoriaComepticao(idcategoria, idcompeticao);
        }

        public DtoGraduacaoFiliado GetGraduacaoAtivaPorIdFiliado(long idFiliado, bool statusGraduacaoFiliado)
        {

            var db = new bancodadosEntities();

            var q = from i in db.GRADUACAO_FILIADO
                    join g in db.GRADUACAO
                    on i.GRADUACAO_ID_GRADUACAO equals g.ID_GRADUACAO
                    where i.ID_FILIADO == idFiliado && i.STATUS_GRADUACAO_FILIADO == statusGraduacaoFiliado
                    select new Dto.DtoGraduacaoFiliado()
                    {
                        IdGraduacaoFiliado = i.ID_GRADUACAO_FILIADO,
                        IdFiliado = i.ID_FILIADO,
                        DataGraduacao = i.DATA_GRADUACAO.Value,
                        Status = i.STATUS_GRADUACAO_FILIADO.Value,
                        Descricao = g.DESCRICAO_GRADUACAO
                    };
            return q.FirstOrDefault();

        }

        public List<DtoCategoriaAtleta> GetCategoriaPorAtelta(DtoDadosAtleta atleta, int idCompeticao)
        {
            if (atleta.TipoInscricao.Equals("IND"))
                return Individual(atleta, idCompeticao);
            else
                return Equipe(atleta, idCompeticao);
        }

        private List<DtoCategoriaAtleta> Equipe(DtoDadosAtleta atleta, int idCompeticao)
        {
            var db = new bancodadosEntities();

            var q = (from categoria in db.CATEGORIA
                     join categoriaCompeticao in db.CATEGORIA_COMPETICAO
                     on categoria.ID_CATEGORIA equals categoriaCompeticao.ID_CATEGORIA
                     join competicao in db.COMPETICAO
                     on categoriaCompeticao.ID_COMPETICAO equals competicao.ID_COMPETICAO
                     where categoriaCompeticao.ID_COMPETICAO == idCompeticao && categoria.TIPO_CATEGORIA == "2"
                     select new DtoCategoria()
                     {
                         NumeroCategoria = categoria.NUMERO_CATEGORIA,
                         IdCategoria = categoria.ID_CATEGORIA,
                         IdCategoriaCompeticao = categoriaCompeticao.ID_CATEGORIA_COMPETICAO,
                         DescricaoCategoria = categoria.DESCRICAO_CATEGORIA,
                         AlturaInicial = categoria.ALTURA_INICIAL_CATEGORIA.Value,
                         AlturaFinal = categoria.ALTURA_FINAL_CATEGORIA.Value,
                         PesoInicial = categoria.PESO_INICIAL_CATEGORIA.Value,
                         PesoFinal = categoria.PESO_FINAL_CATEGORIA.Value,
                         IdGraduacaoInicial = categoria.GRADUACAO_ID_GRADUACAO,
                         IdGraduacaoFinal = categoria.GRADUACAO_FINAL.Value,
                         IdadeInicial = categoria.IDADE_INICIAL.Value,
                         IdadeFinal = categoria.IDADE_FINAL.Value,
                         SexoCategoria = categoria.SEXO_CATEGORIA,
                         TpCompeticao = categoria.TIPO_COMPETICAO_ID_TIPO_COMPETICAO,
                         //TpCompeticaoDescricao = categoria.TIPO_COMPETICAO.DESCRICAO_COMPETICAO,
                         IdEsporte = categoria.ESPORTE_ID_ESPORTE,
                         TpCategoria = categoria.TIPO_CATEGORIA
                     }).OrderBy(p => p.TpCompeticao);

            List<DtoCategoriaAtleta> ListCategoriaAtleta = new List<DtoCategoriaAtleta>();
            foreach (var lst in q)
            {
                if (lst.SexoCategoria == atleta.SexoAtleta || lst.SexoCategoria == "A")
                {
                    DtoCategoriaAtleta categoriaAtleta = new DtoCategoriaAtleta();
                    categoriaAtleta.IdCategoriaCompeticao = lst.IdCategoriaCompeticao;
                    string genero = lst.SexoCategoria == "M" ? "Masculino" : (lst.SexoCategoria == "F" ? "Feminino" : "Masculino e Feminino");
                    string tipoCompeticao = lst.TpCompeticaoDescricao;
                    categoriaAtleta.NomeCategoria = lst.NumeroCategoria + " - " + lst.TpCompeticaoDescricao + " - " + lst.DescricaoCategoria + " - " + genero;
                    if (atleta.IdAtleta != null)
                        categoriaAtleta.IdFiliado = long.Parse(atleta.IdAtleta.ToString());
                    categoriaAtleta.NomeFiliado = atleta.NomeAtleta;
                    ListCategoriaAtleta.Add(categoriaAtleta);
                }
            }
            return ListCategoriaAtleta.ToList();
        }

        private static List<DtoCategoriaAtleta> Individual(DtoDadosAtleta atleta, int idCompeticao)
        {
            var db = new bancodadosEntities();

            var q = (from categoria in db.CATEGORIA
                     join categoriaCompeticao in db.CATEGORIA_COMPETICAO
                     on categoria.ID_CATEGORIA equals categoriaCompeticao.ID_CATEGORIA
                     join competicao in db.COMPETICAO
                     on categoriaCompeticao.ID_COMPETICAO equals competicao.ID_COMPETICAO
                     where categoriaCompeticao.ID_COMPETICAO == idCompeticao && categoria.TIPO_CATEGORIA == "1"
                     select new DtoCategoria()
                     {
                         NumeroCategoria = categoria.NUMERO_CATEGORIA,
                         IdCategoria = categoria.ID_CATEGORIA,
                         IdCategoriaCompeticao = categoriaCompeticao.ID_CATEGORIA_COMPETICAO,
                         DescricaoCategoria = categoria.DESCRICAO_CATEGORIA,
                         AlturaInicial = categoria.ALTURA_INICIAL_CATEGORIA.Value,
                         AlturaFinal = categoria.ALTURA_FINAL_CATEGORIA.Value,
                         PesoInicial = categoria.PESO_INICIAL_CATEGORIA.Value,
                         PesoFinal = categoria.PESO_FINAL_CATEGORIA.Value,
                         IdGraduacaoInicial = categoria.GRADUACAO_ID_GRADUACAO,
                         IdGraduacaoFinal = categoria.GRADUACAO_FINAL.Value,
                         IdadeInicial = categoria.IDADE_INICIAL.Value,
                         IdadeFinal = categoria.IDADE_FINAL.Value,
                         SexoCategoria = categoria.SEXO_CATEGORIA,
                         TpCompeticao = categoria.TIPO_COMPETICAO_ID_TIPO_COMPETICAO,
                         TpCompeticaoDescricao = categoria.TIPO_COMPETICAO.DESCRICAO_COMPETICAO,
                         IdEsporte = categoria.ESPORTE_ID_ESPORTE,
                         TpCategoria = categoria.TIPO_CATEGORIA
                     }).OrderBy(p => p.TpCompeticao);

            List<DtoCategoriaAtleta> ListCategoriaAtleta = new List<DtoCategoriaAtleta>();
            foreach (var lst in q)
            {

                if (lst.IdadeInicial <= atleta.IdadeAtleta && lst.IdadeFinal >= atleta.IdadeAtleta)
                {
                    if (lst.AlturaInicial <= decimal.Parse(atleta.AlturaAtleta) && lst.AlturaFinal >= decimal.Parse(atleta.AlturaAtleta))
                    {
                        if (lst.PesoInicial <= decimal.Parse(atleta.PesoAtleta) && lst.PesoFinal >= decimal.Parse(atleta.PesoAtleta))
                        {
                            if (lst.SexoCategoria == atleta.SexoAtleta || lst.SexoCategoria == "A")
                            {
                                if (lst.IdGraduacaoInicial <= long.Parse(atleta.GraduacaoAtleta) && lst.IdGraduacaoFinal >= long.Parse(atleta.GraduacaoAtleta))
                                {
                                    DtoCategoriaAtleta categoriaAtleta = new DtoCategoriaAtleta();
                                    categoriaAtleta.IdCategoriaCompeticao = lst.IdCategoriaCompeticao;
                                    string genero = lst.SexoCategoria == "M" ? "Masculino" : (lst.SexoCategoria == "F" ? "Feminino" : "Masculino e Feminino");
                                    string tipoCompeticao = lst.TpCompeticaoDescricao;
                                    categoriaAtleta.NomeCategoria = lst.NumeroCategoria + " - " + lst.TpCompeticaoDescricao + " - " + lst.DescricaoCategoria + " - " + genero;
                                    if (atleta.IdAtleta != null)
                                        categoriaAtleta.IdFiliado = long.Parse(atleta.IdAtleta.ToString());
                                    categoriaAtleta.NomeFiliado = atleta.NomeAtleta;
                                    ListCategoriaAtleta.Add(categoriaAtleta);
                                }
                            }
                        }
                    }
                }
            }
            return ListCategoriaAtleta.ToList();
        }

        public void BloquearFiliado(int idFiliado)
        {
            Dao dados = new Dao();
            dados.BloquearFiliado(idFiliado);
        }

        public void LiberarFiliado(int idFiliado)
        {
            Dao dados = new Dao();
            dados.LiberarFiliado(idFiliado);
        }

        public long SetInscricao(DtoInscricao dados)
        {
            INSCRICAO_COMPETICAO_ASSOCIACAO item = new INSCRICAO_COMPETICAO_ASSOCIACAO();
            item.ID_INSCRICAO_COMPETICAO = dados.IdInscricao;
            item.ASSOCIACAO_ID_ASSOCIACAO = dados.IdAssociacao;
            item.LOGIN_ID_LOGIN = dados.IdLogin;
            item.STATUS_INSCRICAO = dados.StatusInscricao;
            item.DATA_INSCRICAO = dados.DataInscricao;
            item.COMPETICAO_ID_COMPETICAO = dados.IdCompeticao;
            item.ESPORTE_ID_ESPORTE = dados.IdEsporte;
            Dao set = new Dao();
            set.SetInscricao(item);

            return item.ID_INSCRICAO_COMPETICAO;
        }

        public long SetInscriAtleta(DtoInscricaoAtleta dados)
        {
            INSCRICAO_DO_ATLETA item = new INSCRICAO_DO_ATLETA();
            if (dados.IdInscricaoAtleta != null && dados.IdInscricaoAtleta != 0)
                item.ID_INSCRICAO_ATLETA = dados.IdInscricaoAtleta;
            if (dados.IdAtleta != null && dados.IdAtleta != 0)
                item.FILIADO_ID_FILIADO = dados.IdAtleta.Value;
            item.NOME_ATLETA = dados.NomeAtleta;
            item.INSCRICAO_COMPETICAO_ASSOCIACAO_ID_INSCRICAO_COMPETICAO = dados.IdInscricao;
            item.TIPO_INSCRICAO = dados.TipoInscricao;
            item.COMPETICAO_ID_COMPETICAO = dados.IdCompeticao.Value;
            item.ASSOCIACAO_ID_ASSOCIACAO = dados.IdAssociacao.Value;

            Dao set = new Dao();
            set.SetInscriAtleta(item);

            return item.ID_INSCRICAO_ATLETA;
        }

        public long SetInscriAtletaCategoria(DtoInscricaoAtleta dados, long idInscriAtleta)
        {
            INSCRICAO_CATEGORIA_ATLETA item = new INSCRICAO_CATEGORIA_ATLETA();
            item.CATEGORIA_COMPETICAO_ID_CATEGORIA_COMPETICAO = dados.IdCategoria;
            item.INSCRICAO_DO_ATLETA_ID_INSCRICAO_ATLETA = idInscriAtleta;

            Dao set = new Dao();
            set.SetInscriAtletaCategoria(item);

            return item.ID_INSCRICAO_CATEGORIA;
        }

        public List<DtoInscricaoAtleta> GetInscritoPorCompeticaoeAssociacao(int idAssociacao, int idInscrCompeticao)
        {
            var db = new bancodadosEntities();
            var q = (from inscricaoCompeticao in db.INSCRICAO_COMPETICAO_ASSOCIACAO
                     join inscricaoFiliado in db.INSCRICAO_DO_ATLETA
                         on inscricaoCompeticao.ID_INSCRICAO_COMPETICAO equals inscricaoFiliado.INSCRICAO_COMPETICAO_ASSOCIACAO_ID_INSCRICAO_COMPETICAO
                     join inscricaoCategoria in db.INSCRICAO_CATEGORIA_ATLETA
                         on inscricaoFiliado.ID_INSCRICAO_ATLETA equals inscricaoCategoria.INSCRICAO_DO_ATLETA_ID_INSCRICAO_ATLETA
                     join categoriaCompeticao in db.CATEGORIA_COMPETICAO
                         on inscricaoCategoria.CATEGORIA_COMPETICAO_ID_CATEGORIA_COMPETICAO equals categoriaCompeticao.ID_CATEGORIA_COMPETICAO
                     join categoria in db.CATEGORIA
                         on categoriaCompeticao.ID_CATEGORIA equals categoria.ID_CATEGORIA
                     where
                     inscricaoCompeticao.ASSOCIACAO_ID_ASSOCIACAO == idAssociacao
                     && inscricaoCompeticao.ID_INSCRICAO_COMPETICAO == idInscrCompeticao
                     select new DtoInscricaoAtleta()
                     {
                         IdAtleta = inscricaoFiliado.FILIADO_ID_FILIADO,
                         //NumeroRegistro = inscricaoFiliado.FILIADO.NUMERO_REGISTRO_FILIADO,
                         //IdCategoria = inscricaoCategoria.CATEGORIA_COMPETICAO.CATEGORIA.ID_CATEGORIA,
                         NomeAtleta = inscricaoFiliado.NOME_ATLETA,
                         NomeCategoria = categoria.DESCRICAO_CATEGORIA,
                         IdInscricaoAtleta = inscricaoFiliado.ID_INSCRICAO_ATLETA,
                         //TipoCompeticao = categoria.TIPO_COMPETICAO.DESCRICAO_COMPETICAO,
                         IdInscricao = idInscrCompeticao,
                         NumeroCategoria = categoria.NUMERO_CATEGORIA,
                     }).OrderBy(p => p.IdInscricaoAtleta);
            return q.ToList();
        }

        public long GetVerificaInscricaoAberta(int IdAssociacao, int IdCompeticao)
        {
            var db = new bancodadosEntities();
            var item = db.INSCRICAO_COMPETICAO_ASSOCIACAO.FirstOrDefault(p => p.ASSOCIACAO_ID_ASSOCIACAO == IdAssociacao && p.COMPETICAO_ID_COMPETICAO == IdCompeticao);

            if (item == null)
                return 0;
            else
                return item.ID_INSCRICAO_COMPETICAO;
        }

        public DtoInscricao GetInscritoPorId(long idInscricao)
        {
            var db = new bancodadosEntities();
            var q = from i in db.INSCRICAO_COMPETICAO_ASSOCIACAO
                    join c in db.COMPETICAO
                    on i.COMPETICAO_ID_COMPETICAO equals c.ID_COMPETICAO
                    where i.ID_INSCRICAO_COMPETICAO == idInscricao
                    select new DtoInscricao()
                    {
                        IdInscricao = i.ID_INSCRICAO_COMPETICAO,
                        IdAssociacao = i.ASSOCIACAO_ID_ASSOCIACAO,
                        IdCompeticao = i.COMPETICAO_ID_COMPETICAO,
                        NomeCompeticao = c.NOME_COMPETICAO,
                        DataInscricao = i.DATA_INSCRICAO.Value,
                    };
            return q.FirstOrDefault();
        }

        public List<DtoInscricao> GetUltimasInscricoes(int IdAssociacao)
        {
            var db = new bancodadosEntities();
            DateTime data = DateTime.Now.AddDays(-1);
            var q = (from i in db.INSCRICAO_COMPETICAO_ASSOCIACAO
                     join c in db.COMPETICAO
                   on i.COMPETICAO_ID_COMPETICAO equals c.ID_COMPETICAO
                     where i.ASSOCIACAO_ID_ASSOCIACAO == IdAssociacao && c.DATA_LIMITE_INSCRICAO >= data
                     && i.STATUS_INSCRICAO == true
                     select new DtoInscricao()
                     {
                         IdInscricao = i.ID_INSCRICAO_COMPETICAO,
                         IdAssociacao = i.ASSOCIACAO_ID_ASSOCIACAO,
                         IdCompeticao = i.COMPETICAO_ID_COMPETICAO,
                         NomeCompeticao = c.NOME_COMPETICAO,
                         DataInscricao = i.DATA_INSCRICAO.Value,
                         StatusInscricao = i.STATUS_INSCRICAO.Value
                     }).Take(5).OrderBy(p => p.IdInscricao);

            return q.ToList();
        }

        public void DelInscricaoAtelta(string idInscricaoAtleta, int idinscricao)
        {
            Dao del = new Dao();
            int Idatelta = int.Parse(idInscricaoAtleta);
            del.DelInscricaoAtelta(Idatelta, idinscricao);
        }

        public void DelInscricoes(string IdInscricao)
        {
            Dao del = new Dao();
            int idinscricao = int.Parse(IdInscricao);

            var db = new bancodadosEntities();
            var qFiliado = db.INSCRICAO_CATEGORIA_ATLETA.Where(p => p.ID_INSCRICAO_CATEGORIA == idinscricao);
            if (qFiliado.Count() > 0)
            {
                foreach (var q in qFiliado)
                {
                    del.DelInscricoes(idinscricao, q.INSCRICAO_DO_ATLETA_ID_INSCRICAO_ATLETA);
                }
            }
            else
                del.DelInscricoes(idinscricao, 0);
        }

        public DtoFiliado GetFiliadoPorIdSemGraduacao(int idFiliado)
        {
            var db = new bancodadosEntities();

            var q = from i in db.FILIADO
                    join a in db.ASSOCIACAO on
                    i.ASSOCIACAO_ID_ASSOCIACAO equals a.ID_ASSOCIACAO into b
                    from a in b.DefaultIfEmpty()
                    join c in db.CIDADE
                    on i.CIDADE_ID_CIDADE equals c.ID_CIDADE
                    where i.ID_FILIADO == idFiliado
                    select new DtoFiliado()
                    {
                        IdFiliado = i.ID_FILIADO,
                        NomeFiliado = i.NOME_FILIADO,
                        EnderecoFiliado = i.ENDERECO_FILIADO,
                        IdadeFiliado = i.IDADE_FILIADO.Value,
                        DataNascimento = i.DATA_NASCIMENTO_FILIADO.Value,
                        EmailFiliado = i.EMAIL_FILIADO,
                        TelefoneCelular = i.TELEFONE_CELULAR_FILIADO,
                        TelefoneFixo = i.TELEFONE_FIXO_FILIADO,
                        RgFiliado = i.RG_FILIADO,
                        CpfFiliado = i.CPF_FILIADO,
                        SexoFiliado = i.SEXO_FILIADO,
                        Peso = i.PESO_FILIADO.Value,
                        Altura = i.ALTURA_FILIADO.Value,
                        IdCidade = i.CIDADE_ID_CIDADE,
                        IdAssociacao = i.ASSOCIACAO_ID_ASSOCIACAO,
                        NomeAssociacao = a.NOME_ASSOCIACAO,
                        NumeroRegistro = i.NUMERO_REGISTRO_FILIADO.Value,
                        StatusFiliado = i.STATUS_FILIADO,
                        NomeCidade = c.NOME_CIDADE,
                    };

            return q.FirstOrDefault();
        }

        public List<Dto.DtoRanking> GetAllRanking(string ano)
        {
            var db = new bancodadosEntities();
            var query = from i in db.RANKING
                        join f in db.FILIADO
                            on i.FILIADO_ID_FILIADO equals f.ID_FILIADO
                        select new Dto.DtoRanking()
                        {
                            IdRanking = i.ID_RANKING,
                            NomeRanking = f.NOME_FILIADO,
                            PontoRanking = i.PONTOS.Value,
                            AnoRanking = i.ANO_RANKING.Value,
                            StatusRanking = i.STATUS_RANKING.Value,
                        };
            return query.ToList();
        }

        public List<DtoFiliado> GetFiliadoPorIdAssociacao(int idAssociacao)
        {
            var db = new bancodadosEntities();
            var query = from i in db.FILIADO
                        join a in db.ASSOCIACAO on
                        i.ASSOCIACAO_ID_ASSOCIACAO equals a.ID_ASSOCIACAO into b
                        from a in b.DefaultIfEmpty()
                        join c in db.CIDADE
                        on i.CIDADE_ID_CIDADE equals c.ID_CIDADE
                        where a.ID_ASSOCIACAO == idAssociacao
                        select new DtoFiliado()
                        {
                            IdFiliado = i.ID_FILIADO,
                            NomeFiliado = i.NOME_FILIADO,
                            EnderecoFiliado = i.ENDERECO_FILIADO,
                            IdadeFiliado = i.IDADE_FILIADO.Value,
                            DataNascimento = i.DATA_NASCIMENTO_FILIADO.Value,
                            EmailFiliado = i.EMAIL_FILIADO,
                            TelefoneCelular = i.TELEFONE_CELULAR_FILIADO,
                            TelefoneFixo = i.TELEFONE_FIXO_FILIADO,
                            RgFiliado = i.RG_FILIADO,
                            CpfFiliado = i.CPF_FILIADO,
                            SexoFiliado = i.SEXO_FILIADO,
                            Peso = i.PESO_FILIADO.Value,
                            Altura = i.ALTURA_FILIADO.Value,
                            IdCidade = i.CIDADE_ID_CIDADE,
                            IdAssociacao = i.ASSOCIACAO_ID_ASSOCIACAO,
                            NomeAssociacao = a.NOME_ASSOCIACAO,
                            NumeroRegistro = i.NUMERO_REGISTRO_FILIADO.Value,
                            StatusFiliado = i.STATUS_FILIADO,
                            NomeCidade = c.NOME_CIDADE,
                        };
            return query.ToList();
        }

        public Dto.DtoRanking GetVerificaRanking(long? ano, long? idFiliado)
        {
            var db = new bancodadosEntities();
            var query = from i in db.RANKING
                        join f in db.FILIADO
                            on i.FILIADO_ID_FILIADO equals f.ID_FILIADO
                        where i.ANO_RANKING == ano && i.FILIADO_ID_FILIADO == idFiliado
                        select new Dto.DtoRanking()
                        {
                            IdRanking = i.ID_RANKING,
                            IdFiliadoRanking = i.FILIADO_ID_FILIADO,
                            PontoRanking = i.PONTOS,
                            StatusRanking = i.STATUS_RANKING,
                            NomeRanking = f.NOME_FILIADO,
                            AnoRanking = i.ANO_RANKING
                        };
            return query.FirstOrDefault();
        }

        public long SetRanking(DtoRanking dados)
        {
            RANKING item = new RANKING();
            item.ANO_RANKING = dados.AnoRanking;
            item.FILIADO_ID_FILIADO = dados.IdFiliadoRanking.Value;
            item.STATUS_RANKING = dados.StatusRanking;
            item.PONTOS = dados.PontoRanking;
            Dao set = new Dao();
            set.SetRanking(item);

            return item.ID_RANKING;
        }

        public void AlterRanking(DtoRanking dados)
        {
            RANKING item = new RANKING();
            item.ID_RANKING = dados.IdRanking;
            item.ANO_RANKING = dados.AnoRanking;
            item.FILIADO_ID_FILIADO = dados.IdFiliadoRanking.Value;
            item.STATUS_RANKING = dados.StatusRanking;
            item.PONTOS = dados.PontoRanking;
            Dao alter = new Dao();
            alter.AlterRanking(item);
        }

        public void DelRanking(string idRanking)
        {
            Dao del = new Dao();
            int idranking = int.Parse(idRanking);

            del.DelRanking(idRanking);
        }

        public DtoRanking GetRankingPorId(int id)
        {
            var db = new bancodadosEntities();
            var query = from i in db.RANKING
                        join f in db.FILIADO
                            on i.FILIADO_ID_FILIADO equals f.ID_FILIADO
                        where i.ID_RANKING == id
                        select new Dto.DtoRanking()
                        {
                            IdRanking = i.ID_RANKING,
                            IdFiliadoRanking = i.FILIADO_ID_FILIADO,
                            PontoRanking = i.PONTOS,
                            StatusRanking = i.STATUS_RANKING,
                            NomeRanking = f.NOME_FILIADO,
                            AnoRanking = i.ANO_RANKING,
                            IdAssociacao = i.FILIADO.ASSOCIACAO_ID_ASSOCIACAO
                        };
            return query.FirstOrDefault();
        }

        public List<DtoCategoriasCompeticao> GetAllCategoriaPorCompeticao(string idCompeticao)
        {
            var db = new bancodadosEntities();

            long id = long.Parse(idCompeticao);
            var q = from categorias in db.CATEGORIA_COMPETICAO
                    join cat in db.CATEGORIA
                    on categorias.ID_CATEGORIA equals cat.ID_CATEGORIA
                    join inscricaoCategoria in db.INSCRICAO_CATEGORIA_ATLETA
                    on categorias.ID_CATEGORIA_COMPETICAO equals inscricaoCategoria.CATEGORIA_COMPETICAO_ID_CATEGORIA_COMPETICAO
                    where categorias.ID_COMPETICAO == id
                    group inscricaoCategoria by new { cat.NUMERO_CATEGORIA, cat.DESCRICAO_CATEGORIA, inscricaoCategoria.CATEGORIA_COMPETICAO_ID_CATEGORIA_COMPETICAO, cat.TIPO_COMPETICAO.DESCRICAO_COMPETICAO, categorias.CATEGORIA_FINALIZADA } into g
                    select new Dto.DtoCategoriasCompeticao()
                    {
                        IdCategoria = g.Key.CATEGORIA_COMPETICAO_ID_CATEGORIA_COMPETICAO,
                        NomeCategoria = g.Key.DESCRICAO_CATEGORIA,
                        TipoCompeticao = g.Key.DESCRICAO_COMPETICAO,
                        NrCategoria = g.Key.NUMERO_CATEGORIA.Value,
                        Categoria_Finalizada = g.Key.CATEGORIA_FINALIZADA.Value,
                        QteAtletasCategoria = g.Count()
                    };

            return q.ToList();
        }

        public void SetSorteio(List<DtoSorteio> list)
        {
            var db = new bancodadosEntities();
            Dao set = new Dao();
            List<INSCRICAO_CATEGORIA_ATLETA> listAtleta = new List<INSCRICAO_CATEGORIA_ATLETA>();
            foreach (var l in list)
            {
                INSCRICAO_CATEGORIA_ATLETA Atleta = new INSCRICAO_CATEGORIA_ATLETA();
                Atleta.ID_INSCRICAO_CATEGORIA = l.IdCategoria.Value;
                Atleta.INSCRICAO_DO_ATLETA_ID_INSCRICAO_ATLETA = l.IdInscriAtleta.Value;
                Atleta.CATEGORIA_COMPETICAO_ID_CATEGORIA_COMPETICAO = l.IdCategoria.Value;
                Atleta.POSICAO_SORTEIO = l.PosicaoSorteio;
                Atleta.DATA_SORTEIO = DateTime.Now;
                Atleta.RESULTADO_FINAL_CATEGORIA = l.ResultadoFinal;
                listAtleta.Add(Atleta);
            }
            set.SetSorteio(listAtleta);
        }

        public List<DtoCategoriaAtleta> GetAllAtletasPorCategoria(string idCategoria, string idCompeticao)
        {
            var db = new bancodadosEntities();
            long IdCategoria = long.Parse(idCategoria);
            long IdCompeticao = long.Parse(idCompeticao);

            var q = (from inscCategoria in db.INSCRICAO_CATEGORIA_ATLETA
                     join inscAtleta in db.INSCRICAO_DO_ATLETA
                     on inscCategoria.INSCRICAO_DO_ATLETA_ID_INSCRICAO_ATLETA equals inscAtleta.ID_INSCRICAO_ATLETA
                     where
                         //categoriaCompeticao.ID_COMPETICAO == IdCompeticao &&
                     inscCategoria.CATEGORIA_COMPETICAO_ID_CATEGORIA_COMPETICAO == IdCategoria
                     select new DtoCategoriaAtleta()
                             {
                                 IdCategoriaCompeticao = inscCategoria.CATEGORIA_COMPETICAO_ID_CATEGORIA_COMPETICAO,
                                 IdinscriaAtleta = inscAtleta.ID_INSCRICAO_ATLETA,
                                 IdFiliado = inscAtleta.FILIADO_ID_FILIADO,
                                 IdCategoriaAtleta = inscCategoria.ID_INSCRICAO_CATEGORIA,
                                 //NumRegistro = inscAtleta.FILIADO.NUMERO_REGISTRO_FILIADO,
                                 NomeFiliado = inscAtleta.NOME_ATLETA,
                                 //NomeAcademia = inscAtleta.INSCRICAO_COMPETICAO_ASSOCIACAO.ASSOCIACAO.NOME_ASSOCIACAO,
                                 SiglaAssociacao = inscAtleta.INSCRICAO_COMPETICAO_ASSOCIACAO.ASSOCIACAO.SIGLA_ASSOCIACAO,
                                 PosicaoSorteio = inscCategoria.POSICAO_SORTEIO,
                                 ResultadoCompeticao = inscCategoria.RESULTADO_FINAL_CATEGORIA,
                             }).OrderBy(p => p.PosicaoSorteio);
            return q.ToList();
        }

        public int CountListaSorteio(int idCategoria)
        {
            var db = new bancodadosEntities();

            var q = from inscCategoria in db.INSCRICAO_CATEGORIA_ATLETA
                    join inscAtleta in db.INSCRICAO_DO_ATLETA
                    on inscCategoria.INSCRICAO_DO_ATLETA_ID_INSCRICAO_ATLETA equals inscAtleta.ID_INSCRICAO_ATLETA
                    where
                        //categoriaCompeticao.ID_COMPETICAO == IdCompeticao &&
                    inscCategoria.CATEGORIA_COMPETICAO_ID_CATEGORIA_COMPETICAO == idCategoria
                    select new DtoCategoriaAtleta()
                    {
                        IdCategoriaCompeticao = inscCategoria.CATEGORIA_COMPETICAO_ID_CATEGORIA_COMPETICAO,
                        IdFiliado = inscAtleta.FILIADO_ID_FILIADO.Value,
                        NomeFiliado = inscAtleta.NOME_ATLETA,
                        PosicaoSorteio = inscCategoria.POSICAO_SORTEIO,
                    };
            return q.Count();
        }

        public List<DtoCategoriaAtleta> ListaSorteio(int idCategoria)
        {
            var db = new bancodadosEntities();

            var q = (from inscCategoria in db.INSCRICAO_CATEGORIA_ATLETA
                     join inscAtleta in db.INSCRICAO_DO_ATLETA
                     on inscCategoria.INSCRICAO_DO_ATLETA_ID_INSCRICAO_ATLETA equals inscAtleta.ID_INSCRICAO_ATLETA
                     join inscAssociacao in db.INSCRICAO_COMPETICAO_ASSOCIACAO
                     on inscAtleta.INSCRICAO_COMPETICAO_ASSOCIACAO_ID_INSCRICAO_COMPETICAO equals inscAssociacao.ID_INSCRICAO_COMPETICAO
                     where
                         //categoriaCompeticao.ID_COMPETICAO == IdCompeticao &&
                     inscCategoria.CATEGORIA_COMPETICAO_ID_CATEGORIA_COMPETICAO == idCategoria
                     select new DtoCategoriaAtleta()
                     {
                         IdCategoriaCompeticao = inscCategoria.CATEGORIA_COMPETICAO_ID_CATEGORIA_COMPETICAO,
                         IdFiliado = inscAtleta.FILIADO_ID_FILIADO.Value,
                         IdCategoriaAtleta = inscCategoria.ID_INSCRICAO_CATEGORIA,
                         //NumRegistro = inscAtleta.FILIADO.NUMERO_REGISTRO_FILIADO,
                         NomeFiliado = inscAtleta.NOME_ATLETA,
                         PosicaoSorteio = inscCategoria.POSICAO_SORTEIO,
                         ResultadoCompeticao = inscCategoria.RESULTADO_FINAL_CATEGORIA,
                         SiglaAssociacao = inscAssociacao.ASSOCIACAO.SIGLA_ASSOCIACAO != null ? inscAssociacao.ASSOCIACAO.SIGLA_ASSOCIACAO : inscAssociacao.ASSOCIACAO.NOME_ASSOCIACAO,
                     }).OrderBy(p => p.PosicaoSorteio);
            return q.ToList();
        }

        public List<DtoSorteio> SorteioEliminatoriaSimples(int idCategoria)
        {
            var db = new bancodadosEntities();
            var q = (from inscCategoria in db.INSCRICAO_CATEGORIA_ATLETA
                     join inscAtleta in db.INSCRICAO_DO_ATLETA
                     on inscCategoria.INSCRICAO_DO_ATLETA_ID_INSCRICAO_ATLETA equals inscAtleta.ID_INSCRICAO_ATLETA
                     where
                     inscCategoria.CATEGORIA_COMPETICAO_ID_CATEGORIA_COMPETICAO == idCategoria
                     select new DtoSorteio()
                     {
                         IdCategoria = inscCategoria.ID_INSCRICAO_CATEGORIA,
                         IdFiliado = inscAtleta.FILIADO_ID_FILIADO.Value,
                         IdInscriAtleta = inscAtleta.ID_INSCRICAO_ATLETA,
                         NomeAtleta = inscAtleta.NOME_ATLETA,
                         IdAssociacao = inscAtleta.INSCRICAO_COMPETICAO_ASSOCIACAO_ID_INSCRICAO_COMPETICAO,
                         PosicaoSorteio = inscCategoria.POSICAO_SORTEIO
                     }).OrderBy(p => p.IdAssociacao);

            return q.ToList();
        }

        public DtoCategoriasCompeticao GetCategoriasPorIdCategoriaIdCompeticao(int idCategoria, int idCompeticao)
        {
            var db = new bancodadosEntities();
            var q = from categoriaCompeticao in db.CATEGORIA_COMPETICAO
                    join categoria in db.CATEGORIA
                    on categoriaCompeticao.ID_CATEGORIA equals categoria.ID_CATEGORIA
                    join tipocompeticao in db.TIPO_COMPETICAO
                    on categoria.TIPO_COMPETICAO_ID_TIPO_COMPETICAO equals tipocompeticao.ID_TIPO_COMPETICAO
                    where categoriaCompeticao.ID_CATEGORIA_COMPETICAO == idCategoria
                           && categoriaCompeticao.ID_COMPETICAO == idCompeticao

                    select new DtoCategoriasCompeticao()
                    {
                        IdCategoriaCompeticao = categoriaCompeticao.ID_CATEGORIA_COMPETICAO,
                        NrCategoria = categoriaCompeticao.CATEGORIA.NUMERO_CATEGORIA.Value,
                        NomeCategoria = categoriaCompeticao.CATEGORIA.DESCRICAO_CATEGORIA,
                        TipoCompeticao = tipocompeticao.DESCRICAO_COMPETICAO,
                    };
            return q.FirstOrDefault();
        }

        public DtoFiliado GetFiliadoPorRegistro(int numeroRegistro)
        {
            var db = new bancodadosEntities();

            var q = from i in db.FILIADO
                    join a in db.ASSOCIACAO
                    on i.ASSOCIACAO_ID_ASSOCIACAO equals a.ID_ASSOCIACAO into b
                    from a in b.DefaultIfEmpty()
                    join c in db.CIDADE
                    on i.CIDADE_ID_CIDADE equals c.ID_CIDADE
                    join gf in db.GRADUACAO_FILIADO
                    on i.ID_FILIADO equals gf.ID_FILIADO
                    join g in db.GRADUACAO
                    on gf.GRADUACAO_ID_GRADUACAO equals g.ID_GRADUACAO
                    where i.NUMERO_REGISTRO_FILIADO == numeroRegistro && gf.STATUS_GRADUACAO_FILIADO == true
                    select new DtoFiliado()
                    {
                        IdFiliado = i.ID_FILIADO,
                        NomeFiliado = i.NOME_FILIADO,
                        EnderecoFiliado = i.ENDERECO_FILIADO,
                        IdadeFiliado = i.IDADE_FILIADO.Value,
                        DataNascimento = i.DATA_NASCIMENTO_FILIADO.Value,
                        EmailFiliado = i.EMAIL_FILIADO,
                        TelefoneCelular = i.TELEFONE_CELULAR_FILIADO,
                        TelefoneFixo = i.TELEFONE_FIXO_FILIADO,
                        RgFiliado = i.RG_FILIADO,
                        CpfFiliado = i.CPF_FILIADO,
                        SexoFiliado = i.SEXO_FILIADO,
                        Peso = i.PESO_FILIADO.Value,
                        Altura = i.ALTURA_FILIADO.Value,
                        IdCidade = i.CIDADE_ID_CIDADE,
                        IdAssociacao = i.ASSOCIACAO_ID_ASSOCIACAO,
                        NomeAssociacao = a.NOME_ASSOCIACAO,
                        NumeroRegistro = i.NUMERO_REGISTRO_FILIADO.Value,
                        StatusFiliado = i.STATUS_FILIADO,
                        NomeCidade = c.NOME_CIDADE,
                        DescGraducacao = g.DESCRICAO_GRADUACAO,
                        IdGraduacao = g.ID_GRADUACAO,
                    };

            return q.FirstOrDefault();
        }

        public void SetResultado(int id, int resultado)
        {
            Dao set = new Dao();
            set.SetResultadoCompeticao(id, resultado);
        }

        public List<DtoAtletasCompeticao> GetCountAllAtletasPorCompeticao(string idCompeticao)
        {
            var db = new bancodadosEntities();

            long id = long.Parse(idCompeticao);
            var q = from competicao in db.COMPETICAO
                    join inscricao in db.INSCRICAO_COMPETICAO_ASSOCIACAO
                    on competicao.ID_COMPETICAO equals inscricao.COMPETICAO_ID_COMPETICAO
                    join inscricaoAtleta in db.INSCRICAO_DO_ATLETA
                    on inscricao.ID_INSCRICAO_COMPETICAO equals inscricaoAtleta.INSCRICAO_COMPETICAO_ASSOCIACAO_ID_INSCRICAO_COMPETICAO
                    where competicao.ID_COMPETICAO == id
                    group inscricaoAtleta by new { inscricao.ASSOCIACAO_ID_ASSOCIACAO, inscricao.ASSOCIACAO.NOME_ASSOCIACAO } into g
                    select new Dto.DtoAtletasCompeticao()
                    {
                        NomeAssociacao = g.Key.NOME_ASSOCIACAO,
                        QteAtletas = g.Count()
                    };

            return q.ToList();
        }

        public List<DtoQuadroMedalhas> GetQuadroMedalhaPorCompeticao(string idCompeticao, string ordenacao, string colunaOrdenacao)
        {
            var db = new bancodadosEntities();

            long id = long.Parse(idCompeticao);
            var q = from competicao in db.COMPETICAO
                    join inscricao in db.INSCRICAO_COMPETICAO_ASSOCIACAO
                    on competicao.ID_COMPETICAO equals inscricao.COMPETICAO_ID_COMPETICAO
                    where competicao.ID_COMPETICAO == id
                    group inscricao by new { inscricao.ASSOCIACAO_ID_ASSOCIACAO, inscricao.ASSOCIACAO.NOME_ASSOCIACAO } into g
                    select new Dto.DtoQuadroMedalhas()
                    {
                        IdAssociacao = g.Key.ASSOCIACAO_ID_ASSOCIACAO,
                        NomeAssociacao = g.Key.NOME_ASSOCIACAO,
                    };
            List<DtoQuadroMedalhas> QuadroMedalhasAssociaco = new List<DtoQuadroMedalhas>();
            foreach (var l in q)
            {
                DtoQuadroMedalhas medalhas = new DtoQuadroMedalhas();
                medalhas.IdAssociacao = l.IdAssociacao;
                medalhas.NomeAssociacao = l.NomeAssociacao;
                medalhas.Primeiro = Colocados(id, l.IdAssociacao.Value, 1);
                medalhas.Segundo = Colocados(id, l.IdAssociacao.Value, 2);
                medalhas.Terceiro = Colocados(id, l.IdAssociacao.Value, 3);
                medalhas.Quarto = Colocados(id, l.IdAssociacao.Value, 4);
                medalhas.Quinto = Colocados(id, l.IdAssociacao.Value, 5);
                medalhas.TotalMedalhas = medalhas.Primeiro + medalhas.Segundo + medalhas.Terceiro + medalhas.Quarto + medalhas.Quinto;
                medalhas.TotalPontos = CalculaPontos(idCompeticao, medalhas.Primeiro.Value, medalhas.Segundo.Value, medalhas.Terceiro.Value, medalhas.Quarto.Value, medalhas.Quinto.Value);
                QuadroMedalhasAssociaco.Add(medalhas);
            }
            if (ordenacao == "Descending")
            {
                if (colunaOrdenacao.Equals("Primeiro"))
                    QuadroMedalhasAssociaco = QuadroMedalhasAssociaco.OrderByDescending(p => p.Primeiro).ToList();
                if (colunaOrdenacao.Equals("TotalMedalhas"))
                    QuadroMedalhasAssociaco = QuadroMedalhasAssociaco.OrderByDescending(p => p.TotalMedalhas).ToList();
                if (colunaOrdenacao.Equals("TotalPontos"))
                    QuadroMedalhasAssociaco = QuadroMedalhasAssociaco.OrderByDescending(p => p.TotalPontos).ToList();
            }
            else
            {
                if (colunaOrdenacao.Equals("Primeiro"))
                    QuadroMedalhasAssociaco = QuadroMedalhasAssociaco.OrderBy(p => p.Primeiro).ToList();
                if (colunaOrdenacao.Equals("TotalMedalhas"))
                    QuadroMedalhasAssociaco = QuadroMedalhasAssociaco.OrderBy(p => p.TotalMedalhas).ToList();
                if (colunaOrdenacao.Equals("TotalPontos"))
                    QuadroMedalhasAssociaco = QuadroMedalhasAssociaco.OrderBy(p => p.TotalPontos).ToList();
            }
            return QuadroMedalhasAssociaco.ToList();
        }

        private long? CalculaPontos(string idCompeticao, long primeiro, long segundo, long terceiro, long quarto, long quinto)
        {
            var db = new bancodadosEntities();
            long id = long.Parse(idCompeticao);
            var q = (from competicao in db.COMPETICAO
                     where competicao.ID_COMPETICAO == id
                     select competicao).FirstOrDefault();

            long somaPontosPrimeiro = primeiro * q.VALOR_RANKING_1_COLOCADO.Value;
            long somaPontosSegundo = segundo * q.VALOR_RANKING_2_COLOCADO.Value;
            long somaPontosTerceiro = terceiro * q.VALOR_RANKING_3_COLOCADO.Value;
            long somaPontosQuarto = quarto * q.VALOR_RANKING_4_COLOCADO.Value;
            long somaPontosQuinto = quinto * q.VALOR_RANKING_5_COLOCADO.Value;
            long SomaTotal = somaPontosPrimeiro + somaPontosSegundo + somaPontosTerceiro + somaPontosQuarto + somaPontosQuinto;
            return SomaTotal;
        }

        private long Colocados(long id, long idAssociacao, int colocacao)
        {
            var db = new bancodadosEntities();
            int primeiro = (from inscriAssociacao in db.INSCRICAO_COMPETICAO_ASSOCIACAO
                            join inscricaoAtleta in db.INSCRICAO_DO_ATLETA
                            on inscriAssociacao.ID_INSCRICAO_COMPETICAO equals inscricaoAtleta.INSCRICAO_COMPETICAO_ASSOCIACAO_ID_INSCRICAO_COMPETICAO
                            join inscriCategoriaAtleta in db.INSCRICAO_CATEGORIA_ATLETA
                             on inscricaoAtleta.ID_INSCRICAO_ATLETA equals inscriCategoriaAtleta.INSCRICAO_DO_ATLETA_ID_INSCRICAO_ATLETA
                            where inscriAssociacao.COMPETICAO_ID_COMPETICAO == id
                            && inscriAssociacao.ASSOCIACAO_ID_ASSOCIACAO == idAssociacao
                            && inscriCategoriaAtleta.RESULTADO_FINAL_CATEGORIA == colocacao
                            select inscricaoAtleta).Count();
            return primeiro;
        }

        public List<DtoCategoriasCompeticao> GetAllPorCategoriaFinalizadas(string idCompeticao)
        {
            var db = new bancodadosEntities();
            long id = long.Parse(idCompeticao);
            var q = from i in db.CATEGORIA_COMPETICAO
                    where i.ID_COMPETICAO == id && i.CATEGORIA_FINALIZADA == true
                    select new DtoCategoriasCompeticao
                    {
                        IdCategoriaCompeticao = i.ID_CATEGORIA_COMPETICAO,
                        Categoria_Finalizada = i.CATEGORIA_FINALIZADA.Value
                    };
            return q.ToList();
        }

        public void SetFinalizaCategoriaCompeticao(string idCategoria, string idCompeticao)
        {
            Dao set = new Dao();
            set.SetFinalizaCategoriaCompeticao(idCategoria, idCompeticao);
        }

        public DtoAcesso GetAcessoPorId(string id)
        {
            var db = new bancodadosEntities();
            int idacesso = int.Parse(id);
            if (idacesso == 0)
            {
                DtoAcesso dados = new DtoAcesso();
                dados.IdAcesso = 0;
                dados.Nome = "Administrador";
                dados.Administrador = true;
                return dados;
            }
            else
            {
                var q = from i in db.LOGIN
                        where i.ID_LOGIN == idacesso
                        select new DtoAcesso()
                        {
                            IdAcesso = i.ID_LOGIN,
                            Nome = i.NOME_LOGIN,
                            Administrador = i.ADMIN_LOGIN.Value,
                            IdAssociacao = i.ASSOCIACAO_ID_ASSOCIACAO,
                        };

                return q.FirstOrDefault();
            }
        }

        public List<DtoCategoriaAtleta> GetAllAtletasPorCompeticaoColocacao(long idcategoria)
        {
            var db = new bancodadosEntities();

            var q = (from inscCategoria in db.INSCRICAO_CATEGORIA_ATLETA
                     join inscAtleta in db.INSCRICAO_DO_ATLETA
                     on inscCategoria.INSCRICAO_DO_ATLETA_ID_INSCRICAO_ATLETA equals inscAtleta.ID_INSCRICAO_ATLETA
                     where
                         //categoriaCompeticao.ID_COMPETICAO == IdCompeticao &&
                     inscCategoria.CATEGORIA_COMPETICAO_ID_CATEGORIA_COMPETICAO == idcategoria
                     select new DtoCategoriaAtleta()
                     {
                         IdCategoriaCompeticao = inscCategoria.CATEGORIA_COMPETICAO_ID_CATEGORIA_COMPETICAO,
                         IdFiliado = inscAtleta.FILIADO_ID_FILIADO,
                         IdCategoriaAtleta = inscCategoria.ID_INSCRICAO_CATEGORIA,
                         //NumRegistro = inscAtleta.FILIADO.NUMERO_REGISTRO_FILIADO,
                         NomeFiliado = inscAtleta.NOME_ATLETA,
                         PosicaoSorteio = inscCategoria.POSICAO_SORTEIO,
                         ResultadoCompeticao = inscCategoria.RESULTADO_FINAL_CATEGORIA,
                         //SiglaAssociacao = inscAtleta.INSCRICAO_COMPETICAO_ASSOCIACAO.ASSOCIACAO.SIGLA_ASSOCIACAO,
                     }).OrderBy(p => p.ResultadoCompeticao);
            return q.ToList();
        }

        public List<DtoCategoriaAtleta> GetAllResultAcademiaAtletas(string IdAssociacao, string idCompeticao)
        {
            var db = new bancodadosEntities();
            long idassociacao = long.Parse(IdAssociacao);
            long idcompeticao = long.Parse(idCompeticao);
            var q = (from inscCategoria in db.INSCRICAO_CATEGORIA_ATLETA
                     join inscAtleta in db.INSCRICAO_DO_ATLETA
                     on inscCategoria.INSCRICAO_DO_ATLETA_ID_INSCRICAO_ATLETA equals inscAtleta.ID_INSCRICAO_ATLETA
                     join inscAssociacao in db.INSCRICAO_COMPETICAO_ASSOCIACAO
                     on inscAtleta.INSCRICAO_COMPETICAO_ASSOCIACAO_ID_INSCRICAO_COMPETICAO equals inscAssociacao.ID_INSCRICAO_COMPETICAO
                     join categoriaCompeticao in db.CATEGORIA_COMPETICAO
                     on inscCategoria.CATEGORIA_COMPETICAO_ID_CATEGORIA_COMPETICAO equals categoriaCompeticao.ID_CATEGORIA_COMPETICAO
                     join categoria in db.CATEGORIA
                         on categoriaCompeticao.ID_CATEGORIA equals categoria.ID_CATEGORIA
                     where
                      inscAssociacao.COMPETICAO_ID_COMPETICAO == idcompeticao &&
                     inscAssociacao.ASSOCIACAO_ID_ASSOCIACAO == idassociacao
                     select new DtoCategoriaAtleta()
                     {
                         IdCategoriaCompeticao = inscCategoria.CATEGORIA_COMPETICAO_ID_CATEGORIA_COMPETICAO,
                         IdFiliado = inscAtleta.FILIADO_ID_FILIADO,
                         IdCategoriaAtleta = inscCategoria.ID_INSCRICAO_CATEGORIA,
                         TipoCompeticao = categoria.TIPO_COMPETICAO.DESCRICAO_COMPETICAO,
                         //NumRegistro = inscAtleta.FILIADO.NUMERO_REGISTRO_FILIADO,
                         NomeFiliado = inscAtleta.NOME_ATLETA,
                         SiglaAssociacao = inscAtleta.INSCRICAO_COMPETICAO_ASSOCIACAO.ASSOCIACAO.SIGLA_ASSOCIACAO,
                         PosicaoSorteio = inscCategoria.POSICAO_SORTEIO,
                         ResultadoCompeticao = inscCategoria.RESULTADO_FINAL_CATEGORIA == 0 ? null : inscCategoria.RESULTADO_FINAL_CATEGORIA
                         }).OrderBy(p => p.NomeFiliado);
            return q.ToList();
        }

        public void DelInscricaoCompeticao(string idInscricao, string categoria, string competicao)
        {
            var db = new bancodadosEntities();
            long id = long.Parse(idInscricao);
            long idcategoria = long.Parse(categoria);
            INSCRICAO_CATEGORIA_ATLETA catCompeticao = db.INSCRICAO_CATEGORIA_ATLETA.FirstOrDefault(p => p.INSCRICAO_DO_ATLETA_ID_INSCRICAO_ATLETA == id && p.CATEGORIA_COMPETICAO_ID_CATEGORIA_COMPETICAO == idcategoria);
            db.DeleteObject(catCompeticao);
            db.SaveChanges();
        }

        public long SetCorrecaoCategoria(string categoria, string competicao, string nome, string associacao, string NumRegistro)
        {
            var db = new bancodadosEntities();
            INSCRICAO_DO_ATLETA atleta = new INSCRICAO_DO_ATLETA();
            long idcategoria = long.Parse(categoria);

            var cat = from c in db.CATEGORIA
                      join cc in db.CATEGORIA_COMPETICAO
                      on c.ID_CATEGORIA equals cc.ID_CATEGORIA
                      where cc.ID_CATEGORIA_COMPETICAO == idcategoria
                      select c.TIPO_CATEGORIA;


            if (cat.FirstOrDefault().Equals("1"))
            { atleta.TIPO_INSCRICAO = "IND"; }
            else if (cat.FirstOrDefault().Equals("2"))
            { atleta.TIPO_INSCRICAO = "EQP"; }
            long idcompeticao = long.Parse(competicao);
            long idassociacao = long.Parse(associacao);
            
            Dao set = new Dao();
            ///TODO deve ser o id inscriCompetição e não somente o Id da Associação
            atleta.INSCRICAO_COMPETICAO_ASSOCIACAO_ID_INSCRICAO_COMPETICAO = GetAInscriParticipante(idcompeticao,idassociacao);
            atleta.NOME_ATLETA = nome;
            set.SetInscriAtleta(atleta);

            return atleta.ID_INSCRICAO_ATLETA;

        }

        private long GetAInscriParticipante(long idcompeticao, long idassociacao)
        {
            var db = new bancodadosEntities();
            long q = (from i in db.INSCRICAO_COMPETICAO_ASSOCIACAO
                      where i.COMPETICAO_ID_COMPETICAO == idcompeticao && i.ASSOCIACAO_ID_ASSOCIACAO == idassociacao
                      select i.ID_INSCRICAO_COMPETICAO).FirstOrDefault();
            return q;

        }

        public List<DtoAcademia> GetAllAcademiaParticipante(long idCompeticao)
        {
            var db = new bancodadosEntities();
            var q = from i in db.INSCRICAO_COMPETICAO_ASSOCIACAO
                    where i.COMPETICAO_ID_COMPETICAO == idCompeticao
                    select new Dto.DtoAcademia()
                    {
                        IdAcademia = i.ASSOCIACAO_ID_ASSOCIACAO,
                        idInscriAssociacao = i.ID_INSCRICAO_COMPETICAO,
                        NomeAcademia = i.ASSOCIACAO.NOME_ASSOCIACAO,
                        EmailAcademia = i.ASSOCIACAO.EMAIL_ASSOCIACAO,

                    };
            return q.ToList();
        }

        public string GetMensagemFiliados()
        {
            var db = new bancodadosEntities();
            DateTime data = DateTime.Now.AddDays(-1);
            var query = from i in db.MENSAGEM_PRESIDENTE
                        where i.DATA_FINAL >= data
                        select i;

            string msg = string.Empty;

            foreach (var q in query)
            {
                msg += "-" + q.MENSAGEM_PRESIDENTE1 + "<br>";
            }
            return msg;
        }

        public string GetMensagemTodos()
        {
            var db = new bancodadosEntities();
            DateTime data = DateTime.Now.AddDays(-1);
            var query = from i in db.MENSAGEM_PRESIDENTE
                        where i.DATA_FINAL >= data
                        && i.FINALIDADE == "T"
                        select i;
            string msg = string.Empty;

            //foreach (var q in query)
            //{
            //    msg += "-" + q.MENSAGEM_PRESIDENTE1 + "<br>";
            //}
            return msg;
        }

        public List<DtoMensagem> GetMensagemTodosList()
        {
            DateTime data = DateTime.Now.AddDays(-1);
            var db = new bancodadosEntities();
            var q = (from i in db.MENSAGEM_PRESIDENTE
                     where i.DATA_FINAL >= data
                     select new DtoMensagem()
                     {
                         Id = i.ID_MENSAGEM_PRESIDENTE,
                         Mensagem = i.MENSAGEM_PRESIDENTE1,
                         Finalidade = i.FINALIDADE,
                         DataLimite = i.DATA_FINAL
                     }).OrderByDescending(x => x.Id);

            return q.ToList();
        }

        public List<DtoMensagem> GetMensagemFiliadosList()
        {
            DateTime data = DateTime.Now.AddDays(-1);
            var db = new bancodadosEntities();
            var q = (from i in db.MENSAGEM_PRESIDENTE
                     where i.DATA_FINAL >= data && i.FINALIDADE == "F"
                     select new DtoMensagem()
                     {
                         Id = i.ID_MENSAGEM_PRESIDENTE,
                         Mensagem = i.MENSAGEM_PRESIDENTE1,
                         Finalidade = i.FINALIDADE,
                         DataLimite = i.DATA_FINAL
                     }).OrderByDescending(x => x.Id);

            return q.ToList();
        }

        public DtoMensagem GetMensagemPorId(int Id)
        {
            var db = new bancodadosEntities();
            var q = from i in db.MENSAGEM_PRESIDENTE
                    where i.ID_MENSAGEM_PRESIDENTE == Id
                    select new DtoMensagem()
                    {
                        Id = i.ID_MENSAGEM_PRESIDENTE,
                        Mensagem = i.MENSAGEM_PRESIDENTE1,
                        Finalidade = i.FINALIDADE,
                        DataLimite = i.DATA_FINAL
                    };
            return q.FirstOrDefault();
        }

        public void SetMensagem(DtoMensagem dados)
        {
            MENSAGEM_PRESIDENTE msg = new MENSAGEM_PRESIDENTE();

            msg.ID_MENSAGEM_PRESIDENTE = dados.Id;
            msg.MENSAGEM_PRESIDENTE1 = dados.Mensagem;
            msg.FINALIDADE = dados.Finalidade;
            msg.DATA_FINAL = dados.DataLimite;
            Dao set = new Dao();
            set.SetMensagem(msg);
        }

        public List<DtoImg> GetImagem()
        {
            var db = new bancodadosEntities();
            var query = from i in db.IMAGENS
                        select new DtoImg()
                        {
                            Id = i.ID_IMAGENS,
                            Mensagem = i.MENSAGEM,
                            Imagem = i.IMAGENS1
                        };
            return query.ToList();
        }

        public DtoImg GetImagemPorId(int id)
        {
            var db = new bancodadosEntities();
            var query = from i in db.IMAGENS
                        where i.ID_IMAGENS == id
                        select new DtoImg()
                        {
                            Id = i.ID_IMAGENS,
                            Mensagem = i.MENSAGEM,
                            Imagem = i.IMAGENS1
                        };
            return query.FirstOrDefault();
        }

        public void DelImg(string idImagem)
        {
            Dao del = new Dao();
            del.DelImg(idImagem);
        }

        public void DelMesnsagem(string idMensagem)
        {
            Dao del = new Dao();
            del.DelMsg(idMensagem);
        }

        public void SetImagem(DtoImg imagem)
        {
            IMAGENS img = new IMAGENS();
            img.ID_IMAGENS = imagem.Id;
            img.MENSAGEM = imagem.Mensagem;
            img.IMAGENS1 = imagem.Imagem;
            Dao set = new Dao();
            set.SetImagem(img);
        }

        public void SetFinalizaCompeticao(long idCompetição)
        {
            Dao finaliza = new Dao();
            finaliza.SetFinalizaCompeticao(idCompetição);
        }

        public void AlterFiliado(DtoFiliado filiado)
        {
            Dao set = new Dao();
            FILIADO item = new FILIADO();
            item.NUMERO_REGISTRO_FILIADO = filiado.NumeroRegistro;
            item.PESO_FILIADO = filiado.Peso;
            item.ALTURA_FILIADO = filiado.Altura;
            item.IDADE_FILIADO = filiado.IdadeFiliado;

            set.AlterFiliado(item);
        }

        public List<DtoCompeticao> GetCompeticaoAtiva()
        {
            var db = new bancodadosEntities();

            var query = from i in db.COMPETICAO
                        where i.DATA_LIMITE_INSCRICAO >= DateTime.Now && i.STATUS_COMPETICAO == true
                        select new DtoCompeticao()
                        {
                            IdCompeticao = i.ID_COMPETICAO,
                            NomeCompeticao = i.NOME_COMPETICAO,
                            NomeResponsavel = i.RESPONSAVEL,
                            TelefoneResponsavel = i.TELEFONE_CELULAR,
                            imagem = i.IMAGEM,
                        };
            return query.ToList();
        }

        public List<DtoCompeticao> GetAllCompeticaFinalizadas()
        {
            var db = new bancodadosEntities();
            var q = from i in db.COMPETICAO
                    where i.STATUS_COMPETICAO == false
                    select new DtoCompeticao()
                    {
                        IdCompeticao = i.ID_COMPETICAO,
                        IdEsporte = i.ESPORTE_ID_ESPORTE,
                        NomeCompeticao = i.NOME_COMPETICAO,
                        NomeResponsavel = i.RESPONSAVEL,
                        DescricaoCompeticao = i.DESCRICAO_COMPETICAO,
                        TelefoneResponsavel = i.TELEFONE_CELULAR,
                        EnderecoCompeticao = i.ENDERECO_COMPETICAO,
                        DataCompeticao = i.DATA_COMPETICAO.Value,
                        DataLimiteInscricao = i.DATA_LIMITE_INSCRICAO.Value,
                        StatusCompeticao = i.STATUS_COMPETICAO.Value,
                        IdCidade = i.ID_CIDADE.Value,
                        Cep = i.CEP_COMPETICAO,
                        ValorCompeticao = i.VALOR_COMPETICAO.Value,
                        PermiteNaoFiliado = i.PERMITE_NAO_FILIADO,
                        PermiteNaoFiliadoDescricao = i.PERMITE_NAO_FILIADO == true ? "Permite inscrição de Atletas não filiados" : "Não permite inscrição de Atletas não filiados",
                        imagem = i.IMAGEM
                    };
            return q.ToList();
        }

        public void SetReabrirCompeticao(long idCompetição)
        {
            Dao reabrir = new Dao();
            reabrir.SetReabrirCompeticao(idCompetição);
        }

        public long GetInscricaoAtleta(string numRegistro, string competicao)
        {
            var db = new bancodadosEntities();
            long registro = long.Parse(numRegistro);
            long idcompeticao = long.Parse(competicao);
            var query = (from F in db.FILIADO
                         join inscriF in db.INSCRICAO_DO_ATLETA on F.ID_FILIADO equals inscriF.FILIADO_ID_FILIADO
                         join AssociaF in db.ASSOCIACAO on F.ASSOCIACAO_ID_ASSOCIACAO equals AssociaF.ID_ASSOCIACAO
                         join inscriA in db.INSCRICAO_COMPETICAO_ASSOCIACAO on AssociaF.ID_ASSOCIACAO equals inscriA.ASSOCIACAO_ID_ASSOCIACAO
                         join inscriA2 in db.INSCRICAO_COMPETICAO_ASSOCIACAO on inscriF.INSCRICAO_COMPETICAO_ASSOCIACAO_ID_INSCRICAO_COMPETICAO equals inscriA2.ID_INSCRICAO_COMPETICAO
                         where F.NUMERO_REGISTRO_FILIADO == registro
                         && inscriA.COMPETICAO_ID_COMPETICAO == idcompeticao
                         select inscriF).OrderByDescending(p => p.ID_INSCRICAO_ATLETA);
            if (query.Count() > 0)
                return query.FirstOrDefault().ID_INSCRICAO_ATLETA;
            else
                return 0;
        }

        public void DelGraduacaoFiliadoPorIdFiliado(string idFiliado)
        {
            Dao dados = new Dao();
            int id = int.Parse(idFiliado);
            dados.DelGraduacaoFiliadoPorIdFiliado(id);
        }



        public List<DtoInscricaoAtleta> GetInscritoPorAssociacao(int IdAcademia, int idCompeticaoSelecionada)
        {
            var db = new bancodadosEntities();
            var q = (from inscricaoCompeticao in db.INSCRICAO_COMPETICAO_ASSOCIACAO
                     join inscricaoFiliado in db.INSCRICAO_DO_ATLETA
                         on inscricaoCompeticao.ID_INSCRICAO_COMPETICAO equals inscricaoFiliado.INSCRICAO_COMPETICAO_ASSOCIACAO_ID_INSCRICAO_COMPETICAO
                     join inscricaoCategoria in db.INSCRICAO_CATEGORIA_ATLETA
                         on inscricaoFiliado.ID_INSCRICAO_ATLETA equals inscricaoCategoria.INSCRICAO_DO_ATLETA_ID_INSCRICAO_ATLETA
                     join categoriaCompeticao in db.CATEGORIA_COMPETICAO
                         on inscricaoCategoria.CATEGORIA_COMPETICAO_ID_CATEGORIA_COMPETICAO equals categoriaCompeticao.ID_CATEGORIA_COMPETICAO
                     join categoria in db.CATEGORIA
                         on categoriaCompeticao.ID_CATEGORIA equals categoria.ID_CATEGORIA
                     where
                     inscricaoCompeticao.ASSOCIACAO_ID_ASSOCIACAO == IdAcademia
                     && inscricaoCompeticao.COMPETICAO.ID_COMPETICAO == idCompeticaoSelecionada
                     select new DtoInscricaoAtleta()
                     {
                         IdAtleta = inscricaoFiliado.FILIADO_ID_FILIADO,
                         //NumeroRegistro = inscricaoFiliado.FILIADO.NUMERO_REGISTRO_FILIADO,
                         //IdCategoria = inscricaoCategoria.CATEGORIA_COMPETICAO.CATEGORIA.ID_CATEGORIA,
                         NomeAtleta = inscricaoFiliado.NOME_ATLETA,
                         NomeCategoria = categoria.DESCRICAO_CATEGORIA,
                         IdInscricaoAtleta = inscricaoFiliado.ID_INSCRICAO_ATLETA,
                         //TipoCompeticao = categoria.TIPO_COMPETICAO.DESCRICAO_COMPETICAO,
                         NumeroCategoria = categoria.NUMERO_CATEGORIA,
                     }).OrderBy(p => p.IdInscricaoAtleta);
            return q.ToList();
        }

        public void setInativarAtletas()
        {
            Dao dados = new Dao();
            var db = new bancodadosEntities();
            var query = from i in db.FILIADO
                        select i;

            dados.setIntavoFiliados(query);

        }

        public List<DtoCategoria> GetCategoriaEsporte(int idEsporte)
        {
            var db = new bancodadosEntities();

            var q = from i in db.CATEGORIA
                    join a in db.ESPORTE on i.ESPORTE_ID_ESPORTE equals a.ID_ESPORTE
                    where i.ESPORTE_ID_ESPORTE == idEsporte
                    select new DtoCategoria()
                    {
                        IdCategoria = i.ID_CATEGORIA,
                        DescricaoCategoria = i.DESCRICAO_CATEGORIA,
                        SexoCategoria = i.SEXO_CATEGORIA,
                        IdadeInicial = i.IDADE_INICIAL.Value,
                        IdadeFinal = i.IDADE_FINAL.Value,
                        AlturaInicial = i.ALTURA_INICIAL_CATEGORIA.Value,
                        AlturaFinal = i.ALTURA_FINAL_CATEGORIA.Value,
                        PesoInicial = i.PESO_INICIAL_CATEGORIA.Value,
                        PesoFinal = i.PESO_FINAL_CATEGORIA.Value,
                        IdGraduacaoInicial = i.GRADUACAO_ID_GRADUACAO,
                        IdGraduacaoFinal = i.GRADUACAO_FINAL.Value,
                        IdEsporte = i.ESPORTE_ID_ESPORTE,
                        StatusCategoria = i.STATUS_CATEGORIA.Value,
                        TpCategoria = i.TIPO_CATEGORIA,
                        NumeroCategoria = i.NUMERO_CATEGORIA.Value,
                        NmEsporte = a.NOME_ESPORTE,
                    };
            return q.ToList();
        }
    }
}
