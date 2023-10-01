using JogoDaVelha;
using System.Text;

var tabuleiro = ValidacaoEstruturas.MontaTabuleiro();
var vitoria = false;
var ganhador = "";
var jogadorX = new Jogador { Nome = "X", QtdeMovimento = 5 };
var jogador0 = new Jogador { Nome = "O", QtdeMovimento = 4 };
var qtdeJogadas = 9;
var jogoEncerrado = false;


IniciarJogo(tabuleiro, jogadorX, jogador0);


ExibirTabuleiro(tabuleiro);
if (!vitoria) ganhador = "Empate!";
Console.WriteLine($"Resultado: {ganhador}");


void IniciarJogo(string[,] tabuleiro, Jogador x, Jogador o)
{
	Console.WriteLine("1 - Player X Maquina");
	Console.WriteLine("2 - Player X Player");
	Console.Write("Escolha uma modelidade: ");
	var resp = Console.ReadLine();
	if (resp != null && resp == "2")
	{
		while (!jogoEncerrado) PassarVez(new List<Jogador> { x, o });
	}
	else if (resp != null && resp == "1")
	{
		while (!jogoEncerrado) ExecutaMovimentoMaquina(tabuleiro, x, o);
	}
	else
	{
		Console.WriteLine("Opção Invalida");
	}
}



void ExecutaMovimentoMaquina(string[,] tabuleiro, Jogador x, Jogador o)
{
	Random random = new Random();
	var movimentoValido = false;
	var movimento = new Movimento();
	var podePassar = true;
	if (!jogoEncerrado)
	{
		do
		{
			ExibirTabuleiro(tabuleiro);
			Console.Write($"Jogada de {x.Nome}: ");
			var jogada = Console.ReadLine();
			var movimentoPlayer = ValidacaoEstruturas.CoverteMovimento(jogada!);
			if (ValidacaoEstruturas.JogadaValida(jogada!, movimentoPlayer, tabuleiro))
			{
				ExecutaMovimento(movimentoPlayer!, tabuleiro, x);
				Console.Clear();
				ExibirTabuleiro(tabuleiro);
				podePassar = true;
			}
			else
			{
				Console.WriteLine("MOVIMENTO INVALIDO!");
				EsperarEfeito(3);
				podePassar = false;

			}
			VerificaVitoria(tabuleiro);

		}
		while (!podePassar);
	}
	Console.Clear();
	if (!jogoEncerrado)
	{
		EsperarEfeito(3);
		var movimentosDisponiveis = ValidacaoEstruturas.MovimentosPossiveis(tabuleiro);
		int indiceAleatorio = random.Next(0, movimentosDisponiveis.Count);
		ExecutaMovimento(movimentosDisponiveis[indiceAleatorio], tabuleiro, o);
		VerificaVitoria(tabuleiro);
	}
}


void PassarVez(List<Jogador> jogadores)
{

	var podePassar = true;
	foreach (var j in jogadores)
	{
		if (!jogoEncerrado)
		{
			do
			{
				ExibirTabuleiro(tabuleiro);
				Console.Write($"Jogada de {j.Nome}: ");
				var jogada = Console.ReadLine();
				var movimento = ValidacaoEstruturas.CoverteMovimento(jogada!);
				if (ValidacaoEstruturas.JogadaValida(jogada!, movimento, tabuleiro))
				{
					ExecutaMovimento(movimento!, tabuleiro, j);
					Console.Clear();
					ExibirTabuleiro(tabuleiro);
					podePassar = true;
				}
				else
				{
					Console.WriteLine("MOVIMENTO INVALIDO!");
					EsperarEfeito(3);
					podePassar = false;

				}
				VerificaVitoria(tabuleiro);

			}
			while (!podePassar);
		}
		Console.Clear();
		if (qtdeJogadas <= 0) jogoEncerrado = true;
	}
}
void EsperarEfeito(int segundos)
{
	for (int segundo = 1; segundo <= segundos; segundo++)
	{
		Console.Write($". ");
		Thread.Sleep(1000);
	}
	Console.Clear();
	Console.WriteLine();
}
void ExibirTabuleiro(string[,] tabuleiro)
{
	StringBuilder table = new StringBuilder();
	for (int i = 0; i < tabuleiro.GetLength(0); i++)
	{
		for (int j = 0; j < tabuleiro.GetLength(0); j++)
		{
			table.Append(tabuleiro[i, j].ToString().PadLeft(4));
		}
		table.AppendLine();
	}

	Console.WriteLine(table.ToString());
	Console.WriteLine();
}

void ExecutaMovimento(Movimento movimento, string[,] tabuleiro, Jogador jogador)
{
	tabuleiro[movimento.Linha - 1, movimento.Coluna - 1] = jogador.Nome;
	jogador.Movimentos.Add(movimento);
	jogador.QtdeMovimento -= 1;
	qtdeJogadas -= 1;
}

void VerificaVitoria(string[,] tabuleiro)
{
	// Verificar vitória nas linhas
	for (int linha = 0; linha < 3; linha++)
	{
		if (tabuleiro[linha, 0] == tabuleiro[linha, 1] && tabuleiro[linha, 1] == tabuleiro[linha, 2])
		{
			if (tabuleiro[linha, 0] == "X")
			{
				vitoria = true;
				ganhador = "X";
				break;
			}
			else if (tabuleiro[linha, 0] == "O")
			{
				vitoria = true;
				ganhador = "O";
				break;
			}
		}
	}
	if (!vitoria)
	{
		// Verificar vitória nas colunas
		for (int coluna = 0; coluna < 3; coluna++)
		{
			if (tabuleiro[0, coluna] == tabuleiro[1, coluna] && tabuleiro[1, coluna] == tabuleiro[2, coluna])
			{
				if (tabuleiro[0, coluna] == "X")
				{
					vitoria = true;
					ganhador = "X";
				}
				else if (tabuleiro[0, coluna] == "O")
				{
					vitoria = true;
					ganhador = "O";
				}
			}
		}
	}

	if (!vitoria)
	{
		// Verificar vitória nas diagonais
		if ((tabuleiro[0, 0] == tabuleiro[1, 1] && tabuleiro[1, 1] == tabuleiro[2, 2]) ||
			(tabuleiro[0, 2] == tabuleiro[1, 1] && tabuleiro[1, 1] == tabuleiro[2, 0]))
		{
			if (tabuleiro[1, 1] == "X")
			{
				vitoria = true;
				ganhador = "X";
			}
			else if (tabuleiro[1, 1] == "O")
			{
				vitoria = true;
				ganhador = "O";
			}
		}
	}

	// Resultado
	if (vitoria)
	{
		vitoria = true;
		ganhador = $"Jogador {ganhador}";
		jogoEncerrado = true;
	}
}




