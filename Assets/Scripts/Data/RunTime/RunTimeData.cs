namespace Data.RunTime
{
    public class RunTimeData
    {
        public InGameData InGameData { get; set; } = new();
        public CameraParameters CameraParameters { get; set; } = new();
    }
}