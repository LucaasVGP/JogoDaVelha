namespace JogoDaVelha
{
	internal class Jogador
	{
		public string Nome { get; set; }
		public int QtdeMovimento { get; set; }
		public List<Movimento> Movimentos { get; set; } = new List<Movimento>();
		public int Ordem { get; set; }
	}
}
