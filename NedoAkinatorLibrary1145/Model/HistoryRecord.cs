namespace NedoAkinatorLibrary1145.Model
{
    public record HistoryRecord
    {
        public HistoryRecord(int id, int? idCharacter, CharacterRecord characterRecord)
        {
            Id = id;
            IdCharacter = idCharacter;
            Character = characterRecord;
        }

        public HistoryRecord()
        {
            
        }

        public int Id { get; set; }
        public int? IdCharacter { get; init; }
        public CharacterRecord Character { get; init; }
    }  
}
