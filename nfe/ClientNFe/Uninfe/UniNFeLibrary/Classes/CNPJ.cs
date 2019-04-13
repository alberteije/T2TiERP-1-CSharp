using System;
using System.Collections.Generic;
using System.Text;
using UniNFeLibrary.Exceptions;
using System.ComponentModel;

namespace UniNFeLibrary
{
    public class CNPJ 
    {
        #region Atributos
        private string mValue;
        #endregion

        #region Construtores
        private CNPJ(string cnpj)
        {
            if (cnpj.Length == 0) return;

            cnpj = Auxiliar.OnlyNumbers(cnpj, ".,-/").ToString();
            if (CNPJ.Validate(cnpj) == false) throw new ExceptionCNPJInvalido(cnpj);
            this.mValue = cnpj;
        }
        #endregion

        #region Overrides

        /// <summary>
        /// gravação de dados
        /// </summary>
        /// <param name="provider">CurrentCulture</param>
        /// <returns>somente os números</returns>
        public string ToString(IFormatProvider provider)
        {
            return Auxiliar.OnlyNumbers(mValue, ".,-").ToString();
        }

        /// <summary>
        /// Converte para string.
        /// </summary>
        /// <returns>Retorna uma string formatada para o CNPJ</returns>
        public override string ToString()
        {
            return CNPJ.FormatCNPJ(mValue);
        }

        #endregion

        #region Métodos estáticos

        #region FormatCNPJ
        /// <summary>
        /// formata o CNPJ
        /// </summary>
        /// <param name="cnpj">valor a ser formatado</param>
        /// <returns></returns>
        public static string FormatCNPJ(string cnpj)
        {
            string ret = "";
            MaskedTextProvider mascara;
            cnpj = Auxiliar.OnlyNumbers(cnpj, "-.,/").ToString();
            //cnpj
            //##.###.###/####-##
            mascara = new MaskedTextProvider(@"00\.000\.000/0000-00");
            mascara.Set(cnpj);
            ret = mascara.ToString();
            return ret;
        }
        #endregion

        #region Validate()
        /// <summary>
        /// valida o CNPJ
        /// </summary>
        /// <param name="cnpj"></param>
        /// <returns>true se for um CNPJ válido</returns>
        public static bool Validate(string cnpj)
        {
            if (string.IsNullOrEmpty(cnpj)) return false;

            try
            {
                #region Valida
                string Cnpj_1 = cnpj.Substring(0, 12);
                string Cnpj_2 = cnpj.Substring(cnpj.Length - 2);
                string Mult = "543298765432";
                string Controle = String.Empty;
                int Soma = 0;
                int Digito = 0;

                for (int j = 1; j < 3; j++)
                {

                    Soma = 0;

                    for (int i = 0; i < 12; i++)
                        Soma += int.Parse(Cnpj_1.Substring(i, 1)) * int.Parse(Mult.Substring(i, 1));

                    if (j == 2) Soma += (2 * Digito);
                    Digito = ((Soma * 10) % 11);
                    if (Digito == 10) Digito = 0;
                    Controle = Controle + Digito.ToString();
                    Mult = "654329876543";

                }

                if (Controle != Cnpj_2)
                {
                    return false;
                }
                else
                {
                    return true;
                }
                #endregion
            }
            catch
            {
                return false;
            }

        }
        #endregion

        #endregion
    }
}

namespace UniNFeLibrary.Exceptions
{
    /// <summary>
    /// CNPJ não é válido.
    /// </summary>
    public class ExceptionCNPJInvalido : Exception
    {
        private string mCnpj = "";
        public ExceptionCNPJInvalido(string cnpj)
        {
            mCnpj = cnpj;
        }

        public override string Message
        {
            get
            {
                return "O CNPJ informado não é válido\nCNPJ: " + mCnpj;
            }
        }
    }
}