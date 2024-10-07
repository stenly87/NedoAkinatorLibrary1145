namespace NedoAkinatorLibrary1145.Model
{
    public record QuestionRecord
    { 
        public int Id { get; init; }
        public string? Text { get; init; }
        public double Rank { get; set; }

        public QuestionRecord(int id, string text)
        {
            Id = id;
            Text = text;
        }
    } 
}




