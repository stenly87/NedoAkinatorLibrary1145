namespace NedoAkinatorLibrary1145.Model
{
    public record CharacterRecord
    {
        public CharacterRecord(int id, string? title, byte[]? image)
        {
            Id = id;
            Title = title;
            Image = image;
        }

        public int Id { get; init; }
        public string? Title { get; init; }
        public byte[]? Image { get; init; }
        public double Rank { get; set; }

    }
}
