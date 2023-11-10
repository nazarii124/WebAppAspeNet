namespace WebAppAspeNet.Models
{
    public class jokes
    {
        public int Id { get; set; }
        public string? Autor { get; set; }
        public string? JokePergunta { get; set; }
        public string? JokeResposta { get; set; }

        public jokes()
        {
            Id = 0;
            //Autor = string.Empty;
            //JokePergunta = string.Empty;
            //JokeResposta = string.Empty;
        }
    }

}
