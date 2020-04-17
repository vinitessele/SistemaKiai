using System;
using System.Linq;
using System.Collections.Generic;

namespace SisKiai.DataBase
{
    public class Dao
    {
        public void SetEstado(long id, string nome, string sigla)
        {
            var db = new bancodadosEntities();
            var dados = db.ESTADO.CreateObject();
            if (id == 0)
            {
                dados.NOME_ESTADO = nome;
                dados.SIGLA_ESTADO = sigla;
                db.AddToESTADO(dados);
            }
            else
            {
                var estado = db.ESTADO.Where(p => p.ID_ESTADO == id).FirstOrDefault();
                estado.NOME_ESTADO = nome;
                estado.SIGLA_ESTADO = sigla;
            }
            db.SaveChanges();
        }

        public void DelEstao(int id)
        {
            var db = new bancodadosEntities();
            var estado = db.ESTADO.Where(p => p.ID_ESTADO == id).FirstOrDefault();
            db.DeleteObject(estado);
            db.SaveChanges();
        }

        public void SetCidade(CIDADE item)
        {
            var db = new bancodadosEntities();
            var dados = db.CIDADE.CreateObject();

            if (item.ID_CIDADE == 0)
            {
                dados.NOME_CIDADE = item.NOME_CIDADE;
                dados.CEP_CIDADE = item.CEP_CIDADE;
                dados.ID_ESTADO = item.ID_ESTADO;
                db.AddToCIDADE(dados);
            }
            else
            {
                var cidade = db.CIDADE.FirstOrDefault(p => p.ID_CIDADE == item.ID_CIDADE);
                cidade.NOME_CIDADE = item.NOME_CIDADE;
                cidade.CEP_CIDADE = item.CEP_CIDADE;
                cidade.ID_ESTADO = item.ID_ESTADO;
            }
            db.SaveChanges();
        }

        public void DelCidade(int idCidade)
        {
            var db = new bancodadosEntities();
            var item = db.CIDADE.FirstOrDefault(p => p.ID_CIDADE == idCidade);

            db.DeleteObject(item);
            db.SaveChanges();

        }

        public void SetAcademia(ASSOCIACAO item)
        {
            var db = new bancodadosEntities();
            //var dados = db.associacao.CreateObject();

            if (item.ID_ASSOCIACAO == 0)
            {
                db.AddToASSOCIACAO(item);
            }
            else
            {
                var q = db.ASSOCIACAO.FirstOrDefault(p => p.ID_ASSOCIACAO == item.ID_ASSOCIACAO);
                q.ID_ASSOCIACAO = item.ID_ASSOCIACAO;
                q.NOME_ASSOCIACAO = item.NOME_ASSOCIACAO;
                q.ENDERECO_ASSOCIACAO = item.ENDERECO_ASSOCIACAO;
                q.NOME_RESPONSAVEL_ASSOCIACAO = item.NOME_RESPONSAVEL_ASSOCIACAO;
                q.TELEFONE_CELULAR_ASSOCIACAO = item.TELEFONE_CELULAR_ASSOCIACAO;
                q.TELEFONE_FIXO_ASSOCIACAO = item.TELEFONE_FIXO_ASSOCIACAO;
                q.CIDADE_ID_CIDADE = item.CIDADE_ID_CIDADE;
                q.CEP_CIDADE = item.CEP_CIDADE;
                q.CNPJ_ASSOCIACAO = item.CNPJ_ASSOCIACAO;
                q.INSCRICAO_ASSOCIACAO = item.INSCRICAO_ASSOCIACAO;
                q.EMAIL_ASSOCIACAO = item.EMAIL_ASSOCIACAO;
                q.SIGLA_ASSOCIACAO = item.SIGLA_ASSOCIACAO;
            }
            db.SaveChanges();
        }

        public void SetEsporte(ESPORTE item)
        {
            var db = new bancodadosEntities();
            var dados = db.ESPORTE.CreateObject();
            if (item.ID_ESPORTE == 0)
            {
                dados.NOME_ESPORTE = item.NOME_ESPORTE;
                db.AddToESPORTE(dados);
            }
            else
            {
                var q = db.ESPORTE.FirstOrDefault(p => p.ID_ESPORTE == item.ID_ESPORTE);
                q.NOME_ESPORTE = item.NOME_ESPORTE;
            }
            db.SaveChanges();
        }

        public void DelEsporte(string idEsporte)
        {
            var db = new bancodadosEntities();
            int id = int.Parse(idEsporte);
            var item = db.ESPORTE.FirstOrDefault(p => p.ID_ESPORTE == id);
            db.DeleteObject(item);
            db.SaveChanges();
        }

        public void DelAcademia(int id)
        {
            var db = new bancodadosEntities();
            var item = db.ASSOCIACAO.FirstOrDefault(p => p.ID_ASSOCIACAO == id);
            db.DeleteObject(item);
            db.SaveChanges();
        }

        public void SetGraduacao(GRADUACAO item)
        {
            var db = new bancodadosEntities();

            if (item.ID_GRADUACAO == 0)
            {
                db.AddToGRADUACAO(item);
            }
            else
            {
                var gra = db.GRADUACAO.FirstOrDefault(p => p.ID_GRADUACAO == item.ID_GRADUACAO);
                gra.DESCRICAO_GRADUACAO = item.DESCRICAO_GRADUACAO;
                gra.ESPORTE_ID_ESPORTE = item.ESPORTE_ID_ESPORTE;
            }
            db.SaveChanges();
        }

        public void DelGraduacao(string idGraduacao)
        {
            var db = new bancodadosEntities();
            long id = long.Parse(idGraduacao);
            var q = db.GRADUACAO.FirstOrDefault(p => p.ID_GRADUACAO == id);
            db.DeleteObject(q);
            db.SaveChanges();
        }

        public long SetCompeticao(COMPETICAO item)
        {
            var db = new bancodadosEntities();

            if (item.ID_COMPETICAO == 0)
            {
                db.COMPETICAO.AddObject(item);
            }
            else
            {

                var q = db.COMPETICAO.FirstOrDefault(p => p.ID_COMPETICAO == item.ID_COMPETICAO);

                q.ID_COMPETICAO = item.ID_COMPETICAO;
                q.DESCRICAO_COMPETICAO = item.DESCRICAO_COMPETICAO;
                q.DATA_COMPETICAO = item.DATA_COMPETICAO;
                q.DATA_LIMITE_INSCRICAO = item.DATA_LIMITE_INSCRICAO;
                q.ID_CIDADE = item.ID_CIDADE;
                q.ENDERECO_COMPETICAO = item.ENDERECO_COMPETICAO;
                q.CEP_COMPETICAO = item.CEP_COMPETICAO;
                q.ESPORTE_ID_ESPORTE = item.ESPORTE_ID_ESPORTE;
                q.NOME_COMPETICAO = item.NOME_COMPETICAO;
                q.RESPONSAVEL = item.RESPONSAVEL;
                q.TELEFONE_CELULAR = item.TELEFONE_CELULAR;
                q.VALOR_COMPETICAO = item.VALOR_COMPETICAO;
                q.STATUS_COMPETICAO = item.STATUS_COMPETICAO;
                q.PERMITE_NAO_FILIADO = item.PERMITE_NAO_FILIADO;
                q.VALOR_RANKING_1_COLOCADO = item.VALOR_RANKING_1_COLOCADO;
                q.VALOR_RANKING_2_COLOCADO = item.VALOR_RANKING_2_COLOCADO;
                q.VALOR_RANKING_3_COLOCADO = item.VALOR_RANKING_3_COLOCADO;
                q.VALOR_RANKING_4_COLOCADO = item.VALOR_RANKING_4_COLOCADO;
                q.VALOR_RANKING_5_COLOCADO = item.VALOR_RANKING_5_COLOCADO;
                q.VALOR_RANKING_PARTICIPACAO = item.VALOR_RANKING_PARTICIPACAO;
                q.PARTICIPA_RANKING = item.PARTICIPA_RANKING;
                q.IMAGEM = item.IMAGEM;
            }
            db.SaveChanges();
            long id = item.ID_COMPETICAO;
            return id;
        }

        public void SetCategoria(CATEGORIA categoria)
        {
            var db = new bancodadosEntities();

            if (categoria.ID_CATEGORIA == 0)
            {
                db.AddToCATEGORIA(categoria);
            }
            else
            {
                var q = db.CATEGORIA.FirstOrDefault(p => p.ID_CATEGORIA == categoria.ID_CATEGORIA);

                q.ID_CATEGORIA = categoria.ID_CATEGORIA;
                q.DESCRICAO_CATEGORIA = categoria.DESCRICAO_CATEGORIA;
                q.ESPORTE_ID_ESPORTE = categoria.ESPORTE_ID_ESPORTE;
                q.GRADUACAO_ID_GRADUACAO = categoria.GRADUACAO_ID_GRADUACAO;
                q.GRADUACAO_FINAL = categoria.GRADUACAO_FINAL;
                q.PESO_INICIAL_CATEGORIA = categoria.PESO_INICIAL_CATEGORIA;
                q.PESO_FINAL_CATEGORIA = categoria.PESO_FINAL_CATEGORIA;
                q.SEXO_CATEGORIA = categoria.SEXO_CATEGORIA;
                q.TIPO_CATEGORIA = categoria.TIPO_CATEGORIA;
                q.IDADE_INICIAL = categoria.IDADE_INICIAL;
                q.IDADE_FINAL = categoria.IDADE_FINAL;
                q.STATUS_CATEGORIA = categoria.STATUS_CATEGORIA;
                q.ALTURA_INICIAL_CATEGORIA = categoria.ALTURA_INICIAL_CATEGORIA;
                q.ALTURA_FINAL_CATEGORIA = categoria.ALTURA_FINAL_CATEGORIA;
                q.NUMERO_CATEGORIA = categoria.NUMERO_CATEGORIA;
                q.TIPO_COMPETICAO_ID_TIPO_COMPETICAO = categoria.TIPO_COMPETICAO_ID_TIPO_COMPETICAO;

            }
            db.SaveChanges();
        }

        public long SetFiliado(FILIADO item)
        {
            var db = new bancodadosEntities();

            if (item.ID_FILIADO == 0)
            {
                db.AddToFILIADO(item);
            }
            else
            {
                var q = db.FILIADO.FirstOrDefault(p => p.ID_FILIADO == item.ID_FILIADO);

                q.ID_FILIADO = item.ID_FILIADO;
                q.NUMERO_REGISTRO_FILIADO = item.NUMERO_REGISTRO_FILIADO;
                q.NOME_FILIADO = item.NOME_FILIADO;
                q.ENDERECO_FILIADO = item.ENDERECO_FILIADO;
                q.CPF_FILIADO = item.CPF_FILIADO;
                q.RG_FILIADO = item.RG_FILIADO;
                q.CIDADE_ID_CIDADE = item.CIDADE_ID_CIDADE;
                q.CEP_CIDADE = item.CEP_CIDADE;
                q.DATA_NASCIMENTO_FILIADO = item.DATA_NASCIMENTO_FILIADO;
                q.IDADE_FILIADO = item.IDADE_FILIADO;
                q.PESO_FILIADO = item.PESO_FILIADO;
                q.ALTURA_FILIADO = item.ALTURA_FILIADO;
                q.EMAIL_FILIADO = item.EMAIL_FILIADO;
                q.SEXO_FILIADO = item.SEXO_FILIADO;
                q.TELEFONE_CELULAR_FILIADO = item.TELEFONE_CELULAR_FILIADO;
                q.TELEFONE_FIXO_FILIADO = item.TELEFONE_FIXO_FILIADO;
                q.ASSOCIACAO_ID_ASSOCIACAO = item.ASSOCIACAO_ID_ASSOCIACAO;
                q.STATUS_FILIADO = item.STATUS_FILIADO;
                q.IMAGEM = item.IMAGEM;
            }
            db.SaveChanges();

            return item.ID_FILIADO;
        }

        public void DelFiliado(string idFiliado)
        {
            var db = new bancodadosEntities();
            long id = long.Parse(idFiliado);
            var item = db.FILIADO.FirstOrDefault(p => p.ID_FILIADO == id);

            db.DeleteObject(item);
            db.SaveChanges();
        }

        public void DelCategoria(string idCategoria)
        {
            var db = new bancodadosEntities();
            long id = long.Parse(idCategoria);
            var item = db.CATEGORIA.FirstOrDefault(p => p.ID_CATEGORIA == id);
            db.DeleteObject(item);
            db.SaveChanges();
        }

        public void DelGraduacaoFiliado(int id)
        {
            var db = new bancodadosEntities();
            var item = db.GRADUACAO_FILIADO.FirstOrDefault(p => p.ID_GRADUACAO_FILIADO == id);
            db.DeleteObject(item);
            db.SaveChanges();
        }

        public long SetGraduacaoFiliado(GRADUACAO_FILIADO item)
        {
            var db = new bancodadosEntities();
            if (item.ID_GRADUACAO_FILIADO == 0)
            {
                db.AddToGRADUACAO_FILIADO(item);
            }
            else
            {
                var q = db.GRADUACAO_FILIADO.FirstOrDefault(p => p.ID_GRADUACAO_FILIADO == item.ID_GRADUACAO_FILIADO);
                q.STATUS_GRADUACAO_FILIADO = item.STATUS_GRADUACAO_FILIADO;
                q.DATA_GRADUACAO = item.DATA_GRADUACAO;
            }
            db.SaveChanges();

            return item.ID_GRADUACAO_FILIADO;
        }



        public long SetAcessoLogin(LOGIN item)
        {
            var db = new bancodadosEntities();
            if (item.ID_LOGIN == 0)
            {
                db.AddToLOGIN(item);
            }
            else
            {
                var q = db.LOGIN.FirstOrDefault(p => p.ID_LOGIN == item.ID_LOGIN);
                q.ID_LOGIN = item.ID_LOGIN;
                q.NOME_LOGIN = item.NOME_LOGIN;
                q.USUARIO_LOGIN = item.USUARIO_LOGIN;
                q.SENHA_LOGIN = item.SENHA_LOGIN;
                q.STATUS_LOGIN = item.STATUS_LOGIN;
                q.ADMIN_LOGIN = item.ADMIN_LOGIN;
                q.ASSOCIACAO_ID_ASSOCIACAO = item.ASSOCIACAO_ID_ASSOCIACAO;
            }
            db.SaveChanges();

            long id = item.ID_LOGIN;
            return id;
        }

        public void DelLoginAcesso(int id)
        {
            var db = new bancodadosEntities();
            var item = db.LOGIN.FirstOrDefault(p => p.ID_LOGIN == id);

            db.DeleteObject(item);
            db.SaveChanges();
        }

        public void SetTelas(TELAS dados)
        {
            var db = new bancodadosEntities();

            if (dados.IDTELAS == 0)
            {
                db.AddToTELAS(dados);
            }
            else
            {
                var q = db.TELAS.FirstOrDefault(p => p.IDTELAS == dados.IDTELAS);
                q.NOME_TELA = dados.NOME_TELA;
                q.ENDERECO_TELAS = dados.ENDERECO_TELAS;
            }
            db.SaveChanges();
        }

        public void DelTelas(int id)
        {
            var db = new bancodadosEntities();
            var q = db.TELAS.FirstOrDefault(p => p.IDTELAS == id);
            db.DeleteObject(q);
            db.SaveChanges();
        }

        public void SetLoginTela(LOGIN_TELA item)
        {
            var db = new bancodadosEntities();

            db.AddToLOGIN_TELA(item);
            db.SaveChanges();
        }

        public void DelLonginTelas(long idTela, long idLogin)
        {
            var db = new bancodadosEntities();

            var item = db.LOGIN_TELA.FirstOrDefault(p => p.TELAS_IDTELAS == idTela && p.LOGIN_ID_LOGIN == idLogin);
            db.DeleteObject(item);
            db.SaveChanges();
        }

        public void SetCategoriaCompeticao(CATEGORIA_COMPETICAO categoria)
        {
            var db = new bancodadosEntities();
            db.AddToCATEGORIA_COMPETICAO(categoria);
            db.SaveChanges();
        }


        public void SetTipoCompeticao(TIPO_COMPETICAO dados)
        {
            var db = new bancodadosEntities();

            if (dados.ID_TIPO_COMPETICAO == 0)
            {
                db.AddToTIPO_COMPETICAO(dados);
            }
            else
            {
                var item = db.TIPO_COMPETICAO.FirstOrDefault(p => p.ID_TIPO_COMPETICAO == dados.ID_TIPO_COMPETICAO);
                item.DESCRICAO_COMPETICAO = dados.DESCRICAO_COMPETICAO;
                item.ESPORTE_ID_ESPORTE = dados.ESPORTE_ID_ESPORTE;
            }
            db.SaveChanges();
        }

        public void DelTipoCompeticao(string idTipoCompeticao)
        {
            var db = new bancodadosEntities();
            long id = long.Parse(idTipoCompeticao);
            var item = db.TIPO_COMPETICAO.FirstOrDefault(p => p.ID_TIPO_COMPETICAO == id);
            db.DeleteObject(item);
            db.SaveChanges();
        }

        public void DelCategoriaComepticao(long idcategoria, long idcompeticao)
        {
            var db = new bancodadosEntities();
            var item = db.CATEGORIA_COMPETICAO.FirstOrDefault(p => p.ID_CATEGORIA == idcategoria && p.ID_COMPETICAO == idcompeticao);
            db.DeleteObject(item);
            db.SaveChanges();
        }

        public void DelCompeticao(long idcompeticao)
        {
            var db = new bancodadosEntities();
            var item = db.COMPETICAO.FirstOrDefault(p => p.ID_COMPETICAO == idcompeticao);
            db.DeleteObject(item);
            db.SaveChanges();
        }

        public void BloquearFiliado(int idFiliado)
        {
            var db = new bancodadosEntities();
            var item = db.FILIADO.FirstOrDefault(p => p.ID_FILIADO == idFiliado);
            item.STATUS_FILIADO = "I"; //Inativo
            db.SaveChanges();
        }

        public void LiberarFiliado(int idFiliado)
        {
            var db = new bancodadosEntities();
            var item = db.FILIADO.FirstOrDefault(p => p.ID_FILIADO == idFiliado);
            if (String.IsNullOrWhiteSpace(item.NUMERO_REGISTRO_FILIADO.ToString()) || item.NUMERO_REGISTRO_FILIADO == 0)
            {
                //Adiciona um numero de registro

                long NumeroRegistro = GetUltimoNumeroRegistro();
                item.NUMERO_REGISTRO_FILIADO = NumeroRegistro + 1;
                item.STATUS_FILIADO = "A"; //Ativo
            }
            else
            {
                item.STATUS_FILIADO = "A"; //Ativo
            }
            db.SaveChanges();
        }

        private long GetUltimoNumeroRegistro()
        {
            var db = new bancodadosEntities();

            var q = (from i in db.FILIADO
                     select i).OrderByDescending(p => p.NUMERO_REGISTRO_FILIADO);

            if (String.IsNullOrWhiteSpace(q.FirstOrDefault().NUMERO_REGISTRO_FILIADO.ToString()))
                return 0;
            else
                return q.FirstOrDefault().NUMERO_REGISTRO_FILIADO.Value;
        }

        public void SetInscricao(INSCRICAO_COMPETICAO_ASSOCIACAO item)
        {
            var db = new bancodadosEntities();
            var dados = db.INSCRICAO_COMPETICAO_ASSOCIACAO.CreateObject();

            if (item.ID_INSCRICAO_COMPETICAO == 0)
            {
                db.AddToINSCRICAO_COMPETICAO_ASSOCIACAO(item);
            }
            else
            {
                var inscri = db.INSCRICAO_COMPETICAO_ASSOCIACAO.FirstOrDefault(p => p.ID_INSCRICAO_COMPETICAO == item.ID_INSCRICAO_COMPETICAO);

                inscri.ID_INSCRICAO_COMPETICAO = item.ID_INSCRICAO_COMPETICAO;
                inscri.ASSOCIACAO_ID_ASSOCIACAO = item.ASSOCIACAO_ID_ASSOCIACAO;
                inscri.LOGIN_ID_LOGIN = item.LOGIN_ID_LOGIN;
                inscri.STATUS_INSCRICAO = item.STATUS_INSCRICAO;
                inscri.DATA_INSCRICAO = item.DATA_INSCRICAO;
            }
            db.SaveChanges();
        }

        public void SetInscriAtleta(INSCRICAO_DO_ATLETA item)
        {
            var db = new bancodadosEntities();
            var dados = db.INSCRICAO_DO_ATLETA.CreateObject();

            if (item.ID_INSCRICAO_ATLETA == 0)
            {
                db.AddToINSCRICAO_DO_ATLETA(item);
            }
            else
            {
                var i = db.INSCRICAO_DO_ATLETA.FirstOrDefault(p => p.ID_INSCRICAO_ATLETA == item.ID_INSCRICAO_ATLETA);
                i.ID_INSCRICAO_ATLETA = item.ID_INSCRICAO_ATLETA;
                i.FILIADO_ID_FILIADO = item.FILIADO_ID_FILIADO;
                i.INSCRICAO_COMPETICAO_ASSOCIACAO_ID_INSCRICAO_COMPETICAO = item.INSCRICAO_COMPETICAO_ASSOCIACAO_ID_INSCRICAO_COMPETICAO;
                db.AddToINSCRICAO_DO_ATLETA(item);
            }
            db.SaveChanges();
        }

        public void SetInscriAtletaCategoria(INSCRICAO_CATEGORIA_ATLETA item)
        {
            var db = new bancodadosEntities();
            var dados = db.INSCRICAO_CATEGORIA_ATLETA.CreateObject();

            if (item.ID_INSCRICAO_CATEGORIA == 0)
            {
                db.AddToINSCRICAO_CATEGORIA_ATLETA(item);
            }
            else
            {
                item.CATEGORIA_COMPETICAO_ID_CATEGORIA_COMPETICAO = dados.CATEGORIA_COMPETICAO_ID_CATEGORIA_COMPETICAO;
                item.INSCRICAO_DO_ATLETA_ID_INSCRICAO_ATLETA = dados.INSCRICAO_DO_ATLETA_ID_INSCRICAO_ATLETA;
            }
            db.SaveChanges();
        }

        public void DelInscricaoAtelta(int idInscricaoAtelta, int idinscricaoCompeticao)
        {
            var db = new bancodadosEntities();

            var item = db.INSCRICAO_DO_ATLETA.FirstOrDefault(p => p.ID_INSCRICAO_ATLETA == idInscricaoAtelta && p.INSCRICAO_COMPETICAO_ASSOCIACAO_ID_INSCRICAO_COMPETICAO == idinscricaoCompeticao);
            DelInscricaoCategoria(item.ID_INSCRICAO_ATLETA);
            db.DeleteObject(item);
            db.SaveChanges();
        }

        private void DelInscricaoCategoria(long id)
        {
            using (bancodadosEntities db = new bancodadosEntities())
            {
                var categoria = db.INSCRICAO_CATEGORIA_ATLETA.Where(p => p.INSCRICAO_DO_ATLETA_ID_INSCRICAO_ATLETA == id);

                foreach (var iCategoria in categoria)
                {
                    db.DeleteObject(iCategoria);

                }
                db.SaveChanges();
            }

        }

        public void DelInscricoes(int idinscricao, long idInscriFiliado)
        {
            DelInscricaoCategoria(idInscriFiliado);
            DelFiliado(idinscricao);
            DelInscricao(idinscricao);
        }

        private void DelInscricao(int idinscricao)
        {
            using (bancodadosEntities db = new bancodadosEntities())
            {
                var itemInscricao = db.INSCRICAO_COMPETICAO_ASSOCIACAO.Where(p => p.ID_INSCRICAO_COMPETICAO == idinscricao);
                foreach (var lts in itemInscricao)
                {
                    db.DeleteObject(lts);
                }
                db.SaveChanges();
            }
        }

        private void DelFiliado(int idinscricao)
        {
            using (bancodadosEntities db = new bancodadosEntities())
            {
                var itemFiliado = db.INSCRICAO_DO_ATLETA.Where(p => p.INSCRICAO_COMPETICAO_ASSOCIACAO_ID_INSCRICAO_COMPETICAO == idinscricao);
                foreach (var lts in itemFiliado)
                {
                    db.DeleteObject(lts);
                }
                db.SaveChanges();
            }
        }

        public void SetRanking(RANKING item)
        {
            var db = new bancodadosEntities();
            db.AddToRANKING(item);
            db.SaveChanges();
        }

        public void AlterRanking(RANKING item)
        {
            var db = new bancodadosEntities();
            RANKING alter = db.RANKING.FirstOrDefault(p => p.ID_RANKING == item.ID_RANKING);
            alter.PONTOS = item.PONTOS;
            db.SaveChanges();
        }

        public void DelRanking(string idRanking)
        {
            var db = new bancodadosEntities();
            long id = long.Parse(idRanking);
            RANKING del = db.RANKING.FirstOrDefault(p => p.ID_RANKING == id);
            db.DeleteObject(del);
            db.SaveChanges();
        }

        public void SetSorteio(List<INSCRICAO_CATEGORIA_ATLETA> listAtleta)
        {
            var db = new bancodadosEntities();
            foreach (var i in listAtleta)
            {
                INSCRICAO_CATEGORIA_ATLETA d = db.INSCRICAO_CATEGORIA_ATLETA.FirstOrDefault(p => p.ID_INSCRICAO_CATEGORIA == i.ID_INSCRICAO_CATEGORIA);
                d.POSICAO_SORTEIO = i.POSICAO_SORTEIO;
                d.DATA_SORTEIO = i.DATA_SORTEIO;
                db.SaveChanges();
            }
        }

        public void SetResultadoCompeticao(int id, int resultado)
        {
            var db = new bancodadosEntities();
            INSCRICAO_CATEGORIA_ATLETA d = db.INSCRICAO_CATEGORIA_ATLETA.FirstOrDefault(p => p.ID_INSCRICAO_CATEGORIA == id);
            d.RESULTADO_FINAL_CATEGORIA = resultado;
            db.SaveChanges();
        }

        public void SetFinalizaCategoriaCompeticao(string idCategoria, string idCompeticao)
        {
            var db = new bancodadosEntities();
            long id = long.Parse(idCategoria);
            long idcompeticao = long.Parse(idCompeticao);
            CATEGORIA_COMPETICAO d = db.CATEGORIA_COMPETICAO.FirstOrDefault(p => p.ID_CATEGORIA_COMPETICAO == id);
            if (d.CATEGORIA_FINALIZADA.Value)
            {
                RemoveRanking(idCategoria, idcompeticao);
                d.CATEGORIA_FINALIZADA = false;
            }
            else
            {
                AdicionaRanking(idCategoria, idcompeticao);
                d.CATEGORIA_FINALIZADA = true;
            }
            db.SaveChanges();
        }

        private void RemoveRanking(string idCategoria, long idCompeticao)
        {
            var db = new bancodadosEntities();
            long id = long.Parse(idCategoria);
            var participacaoRanking = db.COMPETICAO.FirstOrDefault(p => p.ID_COMPETICAO == idCompeticao).PARTICIPA_RANKING;
            if (participacaoRanking.Value)
            {
                var pontos1 = db.COMPETICAO.FirstOrDefault(p => p.ID_COMPETICAO == idCompeticao).VALOR_RANKING_1_COLOCADO.Value;
                var pontos2 = db.COMPETICAO.FirstOrDefault(p => p.ID_COMPETICAO == idCompeticao).VALOR_RANKING_2_COLOCADO.Value;
                var pontos3 = db.COMPETICAO.FirstOrDefault(p => p.ID_COMPETICAO == idCompeticao).VALOR_RANKING_3_COLOCADO.Value;
                var pontos4 = db.COMPETICAO.FirstOrDefault(p => p.ID_COMPETICAO == idCompeticao).VALOR_RANKING_4_COLOCADO.Value;
                var pontos5 = db.COMPETICAO.FirstOrDefault(p => p.ID_COMPETICAO == idCompeticao).VALOR_RANKING_5_COLOCADO.Value;
                var pontosParticipacao = db.COMPETICAO.FirstOrDefault(p => p.ID_COMPETICAO == idCompeticao).VALOR_RANKING_PARTICIPACAO.Value;
                var idesporte = db.COMPETICAO.FirstOrDefault(p => p.ID_COMPETICAO == idCompeticao).ESPORTE_ID_ESPORTE;


                var AtletasCategoria = db.INSCRICAO_CATEGORIA_ATLETA.Where(p => p.CATEGORIA_COMPETICAO_ID_CATEGORIA_COMPETICAO == id).ToList();

                foreach (var q in AtletasCategoria)
                {
                    FILIADO filiado = db.FILIADO.FirstOrDefault(p => p.ID_FILIADO == q.INSCRICAO_DO_ATLETA.FILIADO_ID_FILIADO.Value && p.STATUS_FILIADO == "A");

                    if (filiado != null)
                    {
                        RANKING busca = db.RANKING.FirstOrDefault(p => p.FILIADO_ID_FILIADO == q.INSCRICAO_DO_ATLETA.FILIADO_ID_FILIADO && p.ANO_RANKING == DateTime.Now.Year && p.ID_ESPORTE == idesporte);
                        if (busca == null)
                        {
                            long SaldoPontos = 0;
                            if (!String.IsNullOrWhiteSpace(busca.PONTOS.ToString()))
                                SaldoPontos = busca.PONTOS.Value;
                            if (q.RESULTADO_FINAL_CATEGORIA == 5)
                                busca.PONTOS = SaldoPontos - (pontos5 + pontosParticipacao);
                            if (q.RESULTADO_FINAL_CATEGORIA == 4)
                                busca.PONTOS = SaldoPontos - (pontos4 + pontosParticipacao);
                            if (q.RESULTADO_FINAL_CATEGORIA == 3)
                                busca.PONTOS = SaldoPontos - (pontos3 + pontosParticipacao);
                            if (q.RESULTADO_FINAL_CATEGORIA == 2)
                                busca.PONTOS = SaldoPontos - (pontos2 + pontosParticipacao);
                            if (q.RESULTADO_FINAL_CATEGORIA == 1)
                                busca.PONTOS = SaldoPontos - (pontos1 + pontosParticipacao);
                            if (q.RESULTADO_FINAL_CATEGORIA == null)
                                busca.PONTOS = SaldoPontos - pontosParticipacao;
                            if (busca.PONTOS < 0)
                                busca.PONTOS = 0;
                        }
                        db.SaveChanges();
                    }
                }
            }
        }

        public void AdicionaRanking(string idCategoria, long idCompeticao)
        {
            var db = new bancodadosEntities();
            long id = long.Parse(idCategoria);
            var participacaoRanking = db.COMPETICAO.FirstOrDefault(p => p.ID_COMPETICAO == idCompeticao).PARTICIPA_RANKING;
            try
            {
                if (participacaoRanking.Value)
                {
                    var pontos1 = db.COMPETICAO.FirstOrDefault(p => p.ID_COMPETICAO == idCompeticao).VALOR_RANKING_1_COLOCADO.Value;
                    var pontos2 = db.COMPETICAO.FirstOrDefault(p => p.ID_COMPETICAO == idCompeticao).VALOR_RANKING_2_COLOCADO.Value;
                    var pontos3 = db.COMPETICAO.FirstOrDefault(p => p.ID_COMPETICAO == idCompeticao).VALOR_RANKING_3_COLOCADO.Value;
                    var pontos4 = db.COMPETICAO.FirstOrDefault(p => p.ID_COMPETICAO == idCompeticao).VALOR_RANKING_4_COLOCADO.Value;
                    var pontos5 = db.COMPETICAO.FirstOrDefault(p => p.ID_COMPETICAO == idCompeticao).VALOR_RANKING_5_COLOCADO.Value;
                    var idesporte = db.COMPETICAO.FirstOrDefault(p => p.ID_COMPETICAO == idCompeticao).ESPORTE_ID_ESPORTE;
                    var pontosParticipacao = db.COMPETICAO.FirstOrDefault(p => p.ID_COMPETICAO == idCompeticao).VALOR_RANKING_PARTICIPACAO.Value;

                    var AtletasCategoria = db.INSCRICAO_CATEGORIA_ATLETA.Where(p => p.CATEGORIA_COMPETICAO_ID_CATEGORIA_COMPETICAO == id).ToList();

                    foreach (var q in AtletasCategoria)
                    {
                        FILIADO filiado = db.FILIADO.FirstOrDefault(p => p.ID_FILIADO == q.INSCRICAO_DO_ATLETA.FILIADO_ID_FILIADO.Value && p.STATUS_FILIADO == "A");

                        if (filiado != null)
                        {
                            RANKING busca = db.RANKING.FirstOrDefault(p => p.FILIADO_ID_FILIADO == q.INSCRICAO_DO_ATLETA.FILIADO_ID_FILIADO && p.ANO_RANKING == DateTime.Now.Year && p.ID_ESPORTE == idesporte);
                            if (busca == null)
                            {
                                RANKING SetRanking = new RANKING();
                                SetRanking.ANO_RANKING = DateTime.Now.Year;
                                SetRanking.FILIADO_ID_FILIADO = q.INSCRICAO_DO_ATLETA.FILIADO_ID_FILIADO.Value;
                                SetRanking.ID_ESPORTE = int.Parse(idesporte.ToString());
                                if (q.RESULTADO_FINAL_CATEGORIA == 5)
                                    SetRanking.PONTOS = pontos5 + pontosParticipacao;
                                if (q.RESULTADO_FINAL_CATEGORIA == 4)
                                    SetRanking.PONTOS = pontos4 + pontosParticipacao;
                                if (q.RESULTADO_FINAL_CATEGORIA == 3)
                                    SetRanking.PONTOS = pontos3 + pontosParticipacao;
                                if (q.RESULTADO_FINAL_CATEGORIA == 2)
                                    SetRanking.PONTOS = pontos2 + pontosParticipacao;
                                if (q.RESULTADO_FINAL_CATEGORIA == 1)
                                    SetRanking.PONTOS = pontos1 + pontosParticipacao;
                                if (q.RESULTADO_FINAL_CATEGORIA == null)
                                    SetRanking.PONTOS = pontosParticipacao;
                                db.AddToRANKING(SetRanking);
                            }
                            else
                            {
                                long SaldoPontos = 0;
                                if (!String.IsNullOrWhiteSpace(busca.PONTOS.ToString()))
                                    SaldoPontos = busca.PONTOS.Value;
                                if (q.RESULTADO_FINAL_CATEGORIA == 5)
                                    busca.PONTOS = pontos5 + (SaldoPontos + pontosParticipacao);
                                if (q.RESULTADO_FINAL_CATEGORIA == 4)
                                    busca.PONTOS = pontos4 + (SaldoPontos + pontosParticipacao);
                                if (q.RESULTADO_FINAL_CATEGORIA == 3)
                                    busca.PONTOS = pontos3 + (SaldoPontos + pontosParticipacao);
                                if (q.RESULTADO_FINAL_CATEGORIA == 2)
                                    busca.PONTOS = pontos2 + (SaldoPontos + pontosParticipacao);
                                if (q.RESULTADO_FINAL_CATEGORIA == 1)
                                    busca.PONTOS = pontos1 + (SaldoPontos + pontosParticipacao);
                                if (q.RESULTADO_FINAL_CATEGORIA == null)
                                    busca.PONTOS = pontosParticipacao;
                            }
                            db.SaveChanges();
                        }
                    }
                }
            }
            catch (Exception ex) { }
        }

        public void SetIdadeFiliado(FILIADO alterar)
        {
            var db = new bancodadosEntities();

            var item = db.FILIADO.FirstOrDefault(p => p.ID_FILIADO == alterar.ID_FILIADO);
            item.IDADE_FILIADO = alterar.IDADE_FILIADO;
            db.SaveChanges();
        }

        public void AlterStatusGraduacao(long idGraduacaoFiliado)
        {
            var db = new bancodadosEntities();

            var item = db.GRADUACAO_FILIADO.FirstOrDefault(p => p.ID_GRADUACAO_FILIADO == idGraduacaoFiliado);
            item.STATUS_GRADUACAO_FILIADO = false;
            db.SaveChanges();
        }

        public void SetMensagem(MENSAGEM_PRESIDENTE msg)
        {
            var db = new bancodadosEntities();

            if (!string.IsNullOrWhiteSpace(msg.ID_MENSAGEM_PRESIDENTE.ToString()))
            {
                db.MENSAGEM_PRESIDENTE.AddObject(msg);
            }
            else
            {
                MENSAGEM_PRESIDENTE dados = db.MENSAGEM_PRESIDENTE.FirstOrDefault(x => x.ID_MENSAGEM_PRESIDENTE == msg.ID_MENSAGEM_PRESIDENTE);
                dados.MENSAGEM_PRESIDENTE1 = msg.MENSAGEM_PRESIDENTE1;
                dados.DATA_FINAL = msg.DATA_FINAL;
                dados.FINALIDADE = msg.FINALIDADE;
            }
            db.SaveChanges();
        }

        public void DelImg(string idImagem)
        {
            var db = new bancodadosEntities();
            long id = long.Parse(idImagem);
            IMAGENS msg = db.IMAGENS.FirstOrDefault(x => x.ID_IMAGENS == id);
            db.DeleteObject(msg);
            db.SaveChanges();
        }

        public void DelMsg(string idMensagem)
        {
            var db = new bancodadosEntities();
            long id = long.Parse(idMensagem);
            MENSAGEM_PRESIDENTE msg = db.MENSAGEM_PRESIDENTE.FirstOrDefault(x => x.ID_MENSAGEM_PRESIDENTE == id);
            db.DeleteObject(msg);
            db.SaveChanges();
        }

        public void SetImagem(IMAGENS img)
        {
            var db = new bancodadosEntities();
            db.IMAGENS.AddObject(img);

            db.SaveChanges();
        }

        public void SetFinalizaCompeticao(long idCompetição)
        {
            var db = new bancodadosEntities();

            COMPETICAO competicao = db.COMPETICAO.FirstOrDefault(p => p.ID_COMPETICAO == idCompetição);
            competicao.STATUS_COMPETICAO = false;
            db.SaveChanges();
        }

        public void AlterFiliado(FILIADO item)
        {
            var db = new bancodadosEntities();

            var i = db.FILIADO.FirstOrDefault(x => x.NUMERO_REGISTRO_FILIADO == item.NUMERO_REGISTRO_FILIADO);
            i.PESO_FILIADO = item.PESO_FILIADO;
            i.ALTURA_FILIADO = item.ALTURA_FILIADO;
            i.IDADE_FILIADO = item.IDADE_FILIADO;
            db.SaveChanges();
        }

        public void SetReabrirCompeticao(long idCompetição)
        {
            var db = new bancodadosEntities();

            COMPETICAO competicao = db.COMPETICAO.FirstOrDefault(p => p.ID_COMPETICAO == idCompetição);
            competicao.STATUS_COMPETICAO = true;
            db.SaveChanges();
        }

        public void DelGraduacaoFiliadoPorIdFiliado(int id)
        {
            var db = new bancodadosEntities();
            var q = db.GRADUACAO_FILIADO.Where(p => p.ID_FILIADO == id).ToList();
            foreach (var a in q)
            {
                db.DeleteObject(a);
                db.SaveChanges();
            }
        }

        public void setIntavoFiliados(IQueryable<FILIADO> query)
        {
            var db = new bancodadosEntities();
            foreach (var a in query)
            {
                FILIADO item = db.FILIADO.FirstOrDefault(p => p.ID_FILIADO == a.ID_FILIADO);
                item.STATUS_FILIADO = "I";
                db.SaveChanges();
            }
        }
    }
}
