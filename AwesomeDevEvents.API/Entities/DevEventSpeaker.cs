namespace AwesomeDevEvents.API.Entities
{
    /// <summary>
    /// 
    /// </summary>
    public class DevEventSpeaker
    {
        /// <summary>
        /// 
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string TalkTitle { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string TalkDescription { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string LinkedInProfile { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Guid DevEventId { get; set; }

    }
}