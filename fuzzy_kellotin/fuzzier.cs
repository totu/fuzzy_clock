using System;

namespace fuzzy_kellotin
{
  class Fuzzier
  {
    private string fuzzyness;
    private string[] fuzziesH = new string[] { "yksi", "kaksi", "kolme", "neljä", "viisi", "kuusi", "seitsemän", "kahdeksan", "yhdeksän", "kymmenen", "yksitoista", "kaksitoista" };
    private string[] fuzziesM = new string[] { "viisi", "kymmenen", "viisitoista", "kaksikymmentä", "kaksikymmentäviisi", "puoli", "" };
    
    public Fuzzier() { }

    public Fuzzier(string[] h, string[] m)
    {
      fuzziesH = h;
      fuzziesM = m;
    }

    public string FuzzyUp(int hour, int minutes)
    {
      var m = Convert.ToInt32(Math.Round(minutes / 5.0) * 5);
      fuzzyness = " ";

      if (m > 30 && minutes < 57)
      {
        hour = hour + 1;
        fuzzyness = " vaille ";
      }
      else if (m == 30)
      {
        hour = hour + 1;
      }
      else if (m < 30 && minutes > 3)
      {
        fuzzyness = " yli ";
      }
      return Minutes(m) + fuzzyness + Hour(hour);
    }

    private string Hour(int hour)
    {
      string fuzzy = "error";
      if (hour > 12) hour = hour - 12;
      if (hour == 0) fuzzy = fuzziesH[11];
      else fuzzy = fuzziesH[hour - 1];
      return fuzzy;
    }

    private string Minutes(int minutes)
    {
      string fuzzy = "error";

      switch (minutes)
      {
        case 5:
        case 55:
          fuzzy = fuzziesM[0];
          break;
        case 10:
        case 50:
          fuzzy = fuzziesM[1];
          break;
        case 15:
        case 45:
          fuzzy = fuzziesM[2];
          break;
        case 20:
        case 40:
          fuzzy = fuzziesM[3];
          break;
        case 25:
        case 35:
          fuzzy = fuzziesM[4];
          break;
        case 30:
          fuzzy = fuzziesM[5];
          break;
        case 60:
        case 0:
          fuzzy = fuzziesM[6];
          break;
      }
      return fuzzy;
    }
  }
}
