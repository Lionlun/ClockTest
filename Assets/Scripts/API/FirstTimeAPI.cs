using System;

public class FirstTimeAPI
{
	public string Abbreviation { get; set; }
	public string ClientIP { get; set; }
	public DateTime Datetime { get; set; }
	public int DayOfWeek { get; set; }
	public int DayOfYear { get; set; }
	public bool Dst { get; set; }
	public object DstFrom { get; set; }
	public int DstOffset { get; set; }
	public object DstUntil { get; set; }
	public int RawOffset { get; set; }
	public string Timezone { get; set; }
	public int UnixTime { get; set; }
	public DateTime UtcDatetime { get; set; }
	public string UtcOffset { get; set; }
	public int WeekNumber { get; set; }
}
