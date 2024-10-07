namespace NedoAkinatorLibrary1145.Model
{
    public record HistoryRecord
    { 
        public int Id { get; set; }
        public int IdCharacter { get; init; }
        public CharacterRecord Character { get; init; }
    }  
}
