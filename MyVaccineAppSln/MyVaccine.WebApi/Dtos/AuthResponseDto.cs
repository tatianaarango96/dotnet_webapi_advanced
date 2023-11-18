namespace MyVaccine.WebApi.Dtos
{
    public class VaccineRecordRequestDto
    {
        public string token { get; set; }
        public DateTime expirations { get; set; }
        public bool IsSuccess { get; set; }

        public string[] Errors { get; set; }

    }
}

