using System.Text.RegularExpressions;

namespace JogoDaVelha
{

	public static class ValidacaoEstruturas
	{
		public static Movimento? CoverteMovimento(string jogada)
		{
			if (Regex.IsMatch(jogada, @"^[1-3]\.[1-3]$"))
			{
				string[] partes = jogada.Split('.');
				int numero1 = int.Parse(partes[0]);
				int numero2 = int.Parse(partes[1]);

				if (numero1 >= 1 && numero1 <= 3 && numero2 >= 1 && numero2 <= 3)
				{
					return new Movimento { Linha = numero1, Coluna = numero2 };
				}
				else
				{
					return null;
				}
			}
			else
			{
				return null;
			}
		}

		public static string[,] MontaTabuleiro()
		{
			var col = 3;
			var lin = 3;
			string[,] mat = new string[col, lin];
			for (int i = 0; i < col; i++)
				for (int j = 0; j < lin; j++)
					mat[i, j] = $"{i + 1}.{j + 1}";

			return mat;
		}

		public static bool MovimentoValido(Movimento movimento, string[,] tabuleiro)
		{
			if (tabuleiro[movimento.Linha - 1, movimento.Coluna - 1] == "X" || tabuleiro[movimento.Linha - 1, movimento.Coluna - 1] == "O") return false;
			else return true;
		}

		public static bool JogadaValida(string jogada, Movimento? movimento, string[,] tabuleiro)
		{
			if (string.IsNullOrEmpty(jogada)) return false;
			if (movimento == null) return false;
			if (!MovimentoValido(movimento, tabuleiro)) return false;
			return true;
		}


	}






}
