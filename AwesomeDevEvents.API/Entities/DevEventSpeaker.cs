namespace AwesomeDevEvents.API.Entities
{
    public class DevEventSpeaker
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string TalkTitle { get; set; }

        public string TalskDescription { get; set; }
        public string LinkedInProfile { get; set; }

    }
}